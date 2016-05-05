using Newtonsoft.Json.Converters;

namespace SparkPost
{
    public class IsoDateTimeMinuteConverter : IsoDateTimeConverter
    {
        public IsoDateTimeMinuteConverter()
        {
            this.DateTimeFormat = "yyyy-MM-ddTHH:mm";
        }
    }
}
