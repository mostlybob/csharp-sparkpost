using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SparkPost.ValueMappers
{
    public class MapASingleItemUsingToDictionary : IValueMapper
    {
        private readonly Dictionary<Type, MethodInfo> converters;
        private readonly IDataMapper dataMapper;

        public MapASingleItemUsingToDictionary(IDataMapper dataMapper)
        {
            this.dataMapper = dataMapper;
            converters = GetTheConverters();
        }

        public bool CanMap(Type propertyType, object value)
        {
            return propertyType != typeof (int) && converters.ContainsKey(propertyType);
        }

        public object Map(Type propertyType, object value)
        {
            return converters[propertyType].Invoke(dataMapper, BindingFlags.Default, null,
                new[] {value}, CultureInfo.CurrentCulture);
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