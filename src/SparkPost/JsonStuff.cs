using Newtonsoft.Json;

namespace SparkPost
{
    internal static class JsonStuff
    {
        internal static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        internal static string SerializeObject(object @object)
        {
            return JsonConvert.SerializeObject(@object);
        }
    }
}