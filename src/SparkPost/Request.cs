namespace SparkPost
{
    public class Request
    {
        public string Url { get; set; }
        public object Data { get; set; }
        public string Method { get; set; }

        public string ToJson()
        {
            return JsonStuff.SerializeObject(this);
        }
    }
}