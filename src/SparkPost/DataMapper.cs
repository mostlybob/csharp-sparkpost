using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    }

    public class DataMapper : IDataMapper
    {
        private readonly Dictionary<Type, MethodInfo> converters;

        public DataMapper(string version = "v1")
        {
            converters = GetTheConverters();
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

        private object GetTheValue(Type propertyType, object value)
        {
            if (propertyType != typeof(int) && converters.ContainsKey(propertyType))
                value = converters[propertyType].Invoke(this, BindingFlags.Default, null,
                    new[] {value}, CultureInfo.CurrentCulture);
            else if (value != null && propertyType.Name.EndsWith("List`1") &&
                     propertyType.GetGenericArguments().Count() == 1 &&
                     converters.ContainsKey(propertyType.GetGenericArguments().First()))
            {
                var converter = converters[propertyType.GetGenericArguments().First()];

                var list = (value as IEnumerable<object>).ToList();

                if (list.Any())
                    value = list.Select(x => converter.Invoke(this, BindingFlags.Default, null,
                        new[] {x}, CultureInfo.CurrentCulture)).ToList();
                else
                    value = null;
            }
            else if (new BooleanValueMapper().CanMap(propertyType, value))
                value = new BooleanValueMapper().Map(propertyType, value);
            else if (value is DateTimeOffset?)
                value = string.Format("{0:s}{0:zzz}", (DateTimeOffset?)value);
            else if (value is IDictionary<string, object>)
            {
                var dictionary = (IDictionary<string, object>) value;
                var newDictionary = new Dictionary<string, object>();
                foreach (var item in dictionary.Where(i => i.Value != null))
                    newDictionary[ToSnakeCase(item.Key)] = GetTheValue(item.Value.GetType(), item.Value);
                dictionary = newDictionary;
                value = dictionary.Count > 0 ? dictionary : null;
            }
            else if (value is IDictionary<string, string>)
            {
                var dictionary = (IDictionary<string, string>) value;
                value = dictionary.Count > 0 ? dictionary : null;
            }
            else if (value != null && value.GetType() != typeof(string) && value is IEnumerable)
            {
                var things = (from object thing in (IEnumerable) value
                    select GetTheValue(thing.GetType(), thing)).ToList();
                value = things.Count > 0 ? things : null;
            }
            else if (ThisIsAnAnonymousType(value))
            {
                var newValue = new Dictionary<string, object>();
                foreach (var property in value.GetType().GetProperties())
                    newValue[property.Name] = property.GetValue(value);
                value = GetTheValue(newValue.GetType(), newValue);
            }
            return value;
        }

        private static bool ThisIsAnAnonymousType(object value)
        {
            return value != null && (value.GetType().Name.Contains("AnonymousType") || value.GetType().Name.Contains("AnonType"));
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

        private static Dictionary<Type, MethodInfo> GetTheConverters()
        {
            return typeof (DataMapper).GetMethods()
                .Where(x => x.Name == "ToDictionary")
                .Where(x => x.GetParameters().Length == 1)
                .Select(x => new
                {
                    TheType = x.GetParameters().First().ParameterType,
                    TheMethod = x
                }).ToList()
                .ToDictionary(x => x.TheType, x => x.TheMethod);
        }
    }
}