using Newtonsoft.Json;

namespace SparkPost
{
    public class Request
    {
        public string Method { get; set; }
        public object Data { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}