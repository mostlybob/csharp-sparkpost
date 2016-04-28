using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SparkPost.ValueMappers;

namespace SparkPost
{
    public interface IDataMapper
    {
        IDictionary<string, object> ToDictionary(Transmission transmission);
        IDictionary<string, object> ToDictionary(Recipient recipient);
        IDictionary<string, object> ToDictionary(Address address);
        IDictionary<string, object> ToDictionary(Options options);
        IDictionary<string, object> ToDictionary(Content content);
        IDictionary<string, object> ToDictionary(Attachment attachment);
        IDictionary<string, object> ToDictionary(InlineImage inlineImage);
        IDictionary<string, object> ToDictionary(File file);
        IDictionary<string, object> ToDictionary(Suppression suppression);
        IDictionary<string, object> ToDictionary(Webhook webhook);
        IDictionary<string, object> ToDictionary(Subaccount subaccount);
        object GetTheValue(Type propertyType, object value);
    }

    public class DataMapper : IDataMapper
    {
        private readonly IEnumerable<IValueMapper> valueMappers;

        public DataMapper(string version = "v1")
        {
            valueMappers = new List<IValueMapper>
            {
                new MapASingleItemUsingToDictionary(this),
                new MapASetOfItemsUsingToDictionary(this),
                new BooleanValueMapper(),
                new EnumValueMapper(),
                new DateTimeOffsetValueMapper(),
                new StringObjectDictionaryValueMapper(this),
                new StringStringDictionaryValueMapper(),
                new EnumerableValueMapper(this),
                new AnonymousValueMapper(this)
            };
        }

        public virtual IDictionary<string, object> ToDictionary(Transmission transmission)
        {
            var data = new Dictionary<string, object>
            {
                ["substitution_data"] =
                    transmission.SubstitutionData != null && transmission.SubstitutionData.Keys.Any()
                        ? transmission.SubstitutionData
                        : null,
                ["recipients"] = transmission.ListId != null
                    ? (object) new Dictionary<string, object> {["list_id"] = transmission.ListId}
                    : transmission.Recipients.Select(ToDictionary)
            };

            var result = WithCommonConventions(transmission, data);

            CcHandling.SetAnyCCsInTheHeader(transmission, result);

            return result;
        }

        public virtual IDictionary<string, object> ToDictionary(Recipient recipient)
        {
            return WithCommonConventions(recipient, new Dictionary<string, object>()
            {
                ["type"] = null,
                ["substitution_data"] =
                    recipient.SubstitutionData != null && recipient.SubstitutionData.Keys.Any()
                        ? recipient.SubstitutionData
                        : null,
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Suppression suppression)
        {
            return WithCommonConventions(suppression);
        }

        public virtual IDictionary<string, object> ToDictionary(Webhook webhook)
        {
            return WithCommonConventions(webhook);
        }

        public virtual IDictionary<string, object> ToDictionary(Address address)
        {
            return WithCommonConventions(address);
        }

        public virtual IDictionary<string, object> ToDictionary(Options options)
        {
            return AnyValuesWereSetOn(options) ? WithCommonConventions(options) : null;
        }

        public virtual IDictionary<string, object> ToDictionary(Content content)
        {
            return WithCommonConventions(content);
        }

        public virtual IDictionary<string, object> ToDictionary(Attachment attachment)
        {
            return ToDictionary(attachment as File);
        }

        public virtual IDictionary<string, object> ToDictionary(InlineImage inlineImage)
        {
            return ToDictionary(inlineImage as File);
        }

        public virtual IDictionary<string, object> ToDictionary(File file)
        {
            return WithCommonConventions(file);
        }

        public IDictionary<string, object> ToDictionary(Subaccount subaccount)
        {
            return WithCommonConventions(subaccount);
        }

        private static bool AnyValuesWereSetOn(object target)
        {
            return target.GetType()
                .GetProperties()
                .Any(x => x.GetValue(target) != null);
        }

        private static IDictionary<string, object> RemoveNulls(IDictionary<string, object> dictionary)
        {
            var newDictionary = new Dictionary<string, object>();
            foreach (var key in dictionary.Keys.Where(k => dictionary[k] != null))
                newDictionary[key] = dictionary[key];
            return newDictionary;
        }

        private IDictionary<string, object> WithCommonConventions(object target, IDictionary<string, object> results = null)
        {
            if (results == null) results = new Dictionary<string, object>();
            foreach (var property in target.GetType().GetProperties())
            {
                var name = ToSnakeCase(property.Name);
                if (results.ContainsKey(name)) continue;

                results[name] = GetTheValue(property.PropertyType, property.GetValue(target));
            }
            return RemoveNulls(results);
        }

        public object GetTheValue(Type propertyType, object value)
        {
            var valueMapper = valueMappers.FirstOrDefault(x => x.CanMap(propertyType, value));
            return valueMapper == null ? value : valueMapper.Map(propertyType, value);
        }

        public static string ToSnakeCase(string input)
        {
            var regex = new Regex("[A-Z]");

            var matches = regex.Matches(input);

            for (var i = 0; i < matches.Count; i++)
                input = input.Replace(matches[i].Value, "_" + matches[i].Value.ToLower());

            if (input.StartsWith("_"))
                input = input.Substring(1, input.Length - 1);

            return input;
        }
    }
}