using System;

namespace SparkPost
{
    public class Dkim
    {
        private const string PUBLIC_PROPERTY_NAME = "public";
        private const string PRIVATE_PROPERTY_NAME = "private";

        public string SigningDomain { get; set; }
        
        public string PrivateKey { get; set; }
        
        public string PublicKey { get; set; }
        
        public string Selector { get; set; }
        
        public string Headers { get; set; }

        /// <summary>
        /// Convert json result form Sparkpost API to Dkim.
        /// </summary>
        /// <param name="result">Json result form Sparkpost API.</param>
        /// <returns></returns>
        public static Dkim ConvertToDkim(dynamic result)
        {
            return result != null ? new Dkim
                {
                    SigningDomain = result[PUBLIC_PROPERTY_NAME],
                    PublicKey = result[PRIVATE_PROPERTY_NAME],
                    Selector = result.selector,
                    Headers = result.headers
                }
                : null;
        }
    }
}