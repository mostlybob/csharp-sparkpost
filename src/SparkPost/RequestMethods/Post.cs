using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SparkPost.RequestMethods
{
    public class Post : PutAndPostAreTheSame
    {
        public Post(HttpClient client) : base(client)
        {
        }

        public override bool CanExecute(Request request)
        {
            return (request.Method ?? "").ToLower().StartsWith("post");
        }

        public override Task<HttpResponseMessage> Execute(string url, StringContent stringContent)
        {
            return Client.PostAsync(url, stringContent);
        }
    }
}