using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SparkPost.Utilities
{
    public static class ToDictionaryMethodFinder
    {
        public static Dictionary<Type, MethodInfo> GetTheConverters(IDataMapper dataMapper)
        {
            return dataMapper.GetType().GetMethods()
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