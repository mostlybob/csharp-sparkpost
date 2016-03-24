using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SparkPost
{
    public class DataMapper
    {
        private readonly Dictionary<Type, MethodInfo> dictionaryConverters;

        public DataMapper(string version)
        {
            dictionaryConverters = typeof (DataMapper).GetMethods()
                .Where(x => x.Name == "ToDictionary")
                .Where(x => x.GetParameters().Count() == 1)
                .Select(x => new
                {
                    TheType = x.GetParameters().First().ParameterType,
                    TheMethod = x
                }).ToList()
                .ToDictionary(x => x.TheType, x => x.TheMethod);
        }

        public virtual IDictionary<string, object> ToDictionary(Transmission transmission)
        {
            return WithCommonConventions(transmission, new Dictionary<string, object>
            {
                ["recipients"] = transmission.ListId != null
                    ? (object) new Dictionary<string, object> {["list_id"] = transmission.ListId}
                    : transmission.Recipients.Select(ToDictionary)
            });
        }

        public virtual IDictionary<string, object> ToDictionary(Recipient recipient)
        {
            return WithCommonConventions(recipient);
        }

        public virtual IDictionary<string, object> ToDictionary(Address address)
        {
            return WithCommonConventions(address);
        }

        public virtual IDictionary<string, object> ToDictionary(Options options)
        {
            if (typeof(Options)
                .GetProperties()
                .Any(x => x.GetValue(options) != null))
                return WithCommonConventions(options);
            return null;
        }

        public virtual IDictionary<string, object> ToDictionary(Content content)
        {
            return WithCommonConventions(content, new Dictionary<string, object>
            {
                ["attachments"] = content.Attachments.Any() ? content.Attachments.Select(ToDictionary) : null,
                ["inline_images"] = content.InlineImages.Any() ? content.InlineImages.Select(ToDictionary) : null,
            });
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
            if (dictionaryConverters.ContainsKey(propertyType))
            {
                var dictionary = dictionaryConverters[propertyType].Invoke(this, BindingFlags.Default, null,
                    new[] {value}, CultureInfo.CurrentCulture);
                value = dictionary;
            }
            else if (value is bool?)
            {
                value = value as bool? == true;
            }
            else if (value is DateTimeOffset?)
            {
                value = string.Format("{0:s}{0:zzz}", (DateTimeOffset?)value);
            }
            else if (value is IDictionary<string, object>)
            {
                var dictionary = (IDictionary<string, object>) value;
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
            return value;
        }

        private string ToSnakeCase(string input)
        {
            var regex = new Regex("[A-Z]");

            var matches = regex.Matches(input);

            for (var i = 0; i < matches.Count; i++)
                input = input.Replace(matches[i].Value, "_" + matches[i].Value.ToLower());

            if (input.StartsWith("_"))
                input = input.Substring(1, input.Length - 1);

            return input;
        }

        private static bool AtLeastOneOptionWasSet(Options options)
        {
            return typeof(Options)
                .GetProperties()
                .Any(x => x.GetValue(options) != null);
        }
    }
}
