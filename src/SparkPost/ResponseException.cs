using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SparkPost
{
    public class ResponseException : Exception
    {
        private readonly Response response;

        public ResponseException(Response response)
        {
            this.response = response;
        }

        public override string Message =>
            JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content)["errors"];
    }
}