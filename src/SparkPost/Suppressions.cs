using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace SparkPost
{
    public class Suppressions
    {
        private readonly Client client;
        private readonly RequestSender requestSender;
        private readonly DataMapper dataMapper;

        public Suppressions(Client client, RequestSender requestSender, DataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<Response> List(SuppressionsQuery supppressionsQuery)
        {
            return await List(supppressionsQuery as object);
        }

        public async Task<Response> List(object query = null)
        {
            if (query == null) query = new {};
            var request = new Request()
            {
                Url = $"/api/{client.Version}/suppression-list",
                Method = "GET",
                Data = query
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;

            return new SuppressionListResponse()
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Suppressions = ConvertResultsToAListOfSuppressions(results)
            };
        }

        public async Task<SuppressionListResponse> Retrieve(string email)
        {
            var request = new Request()
            {
                Url = $"/api/{client.Version}/suppression-list/{HttpUtility.UrlEncode(email)}",
                Method = "GET"
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;

            return new SuppressionListResponse()
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Suppressions = ConvertResultsToAListOfSuppressions(results)
            };
        }

        public async Task<Response> CreateOrUpdate(IEnumerable<Suppression> suppressions)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/suppression-list",
                Method = "PUT JSON",
                Data = new
                {
                    recipients = suppressions
                        .Select(x => new
                        {
                            email = x.Email,
                            transactional = x.Transactional,
                            non_transactional = x.NonTransactional,
                            description = x.Description
                        })
                        .ToList()
                }
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;

            return new SuppressionListResponse()
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Suppressions = results
            };
        }

        public class SuppressionListResponse : Response
        {
            public IEnumerable<Suppression> Suppressions { get; set; }
        }

        public async Task<bool> Delete(string email)
        {
            var request = new Request
            {
                Url = $"api/{client.Version}/suppression-list/{HttpUtility.UrlEncode(email)}",
                Method = "DELETE"
            };

            var response = await requestSender.Send(request);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        private static IEnumerable<Suppression> ConvertResultsToAListOfSuppressions(dynamic results)
        {
            var suppressions = new List<Suppression>();

            foreach (var result in results)
            {
                suppressions.Add(new Suppression()
                {
                    Description = result.description,
                    Transactional = result.transactional,
                    NonTransactional = result.non_transactional,
                    Email = result.recipient
                });
            }
            return suppressions;
        }

    }
}