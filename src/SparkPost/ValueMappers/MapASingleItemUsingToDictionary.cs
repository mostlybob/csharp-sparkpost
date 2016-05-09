using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SparkPost.Utilities;

namespace SparkPost.ValueMappers
{
    public class MapASingleItemUsingToDictionary : IValueMapper
    {
        private readonly Dictionary<Type, MethodInfo> converters;
        private readonly IDataMapper dataMapper;

        public MapASingleItemUsingToDictionary(IDataMapper dataMapper)
        {
            this.dataMapper = dataMapper;
            converters = ToDictionaryMethodFinder.GetTheConverters(dataMapper);
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
    }
}