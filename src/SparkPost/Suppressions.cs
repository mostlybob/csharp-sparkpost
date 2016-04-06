using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace SparkPost
{
    public class Suppressions : ISuppressions
    {
        private readonly Client client;
        private readonly RequestSender requestSender;

        public Suppressions(Client client, RequestSender requestSender)
        {
            this.client = client;
            this.requestSender = requestSender;
        }

        public async Task<Response> List(SuppressionsQuery supppressionsQuery)
        {
            return await List(supppressionsQuery as object);
        }

        public async Task<Response> List(object query = null)
        {
            if (query == null) query = new {};
            var request = new Request
            {
                Url = $"/api/{client.Version}/suppression-list",
                Method = "GET",
                Data = query
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = JsonConvert.DeserializeObject<dynamic>(response.Content).results;

            return new ListSuppressionResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Suppressions = ConvertResultsToAListOfSuppressions(results)
            };
        }

        public async Task<ListSuppressionResponse> Retrieve(string email)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/suppression-list/{HttpUtility.UrlEncode(email)}",
                Method = "GET"
            };

            var response = await requestSender.Send(request);

            if (new[] {HttpStatusCode.OK, HttpStatusCode.NotFound}.Contains(response.StatusCode) == false)
                throw new ResponseException(response);

            dynamic results = response.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<dynamic>(response.Content).results
                : null;

            return new ListSuppressionResponse
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

            return new Response
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content
            };
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

            if (results == null) return suppressions;

            foreach (var result in results)
            {
                suppressions.Add(new Suppression
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