using System;

namespace SparkPost.ValueMappers
{
    public class DateTimeValueMapper : IValueMapper
    {
        public bool CanMap(Type propertyType, object value)
        {
            return value is DateTime;
        }

        public object Map(Type propertyType, object value)
        {
            return string.Format("{0:s}{0:zzz}", (DateTime) value);
        }
    }
}