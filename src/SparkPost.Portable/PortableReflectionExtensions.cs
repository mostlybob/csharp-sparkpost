using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SparkPost
{
    public static class ReflectionExt
    {
        public static PropertyInfo[] GetProperties(this Type type)
        {
            return type.GetRuntimeProperties().ToArray();
        }

        public static Type[] GetGenericArguments(this Type type)
        {
            return type.GenericTypeArguments;
        }

        public static object Invoke(this MethodBase me, object obj, BindingFlags bindingFlags, object binder,
            object[] parameters, CultureInfo cultureInfo)
        {
            return me.Invoke(obj, parameters);
        }

        public static MethodInfo[] GetMethods(this Type type)
        {
            return type.GetRuntimeMethods().ToArray();
        }
    }

    public enum BindingFlags
    {
        Default
    }
}