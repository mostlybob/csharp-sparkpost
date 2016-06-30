using System.Net;
using System.Threading.Tasks;
using System.Web;
using SparkPost.RequestSenders;
using SparkPost.Utilities;

namespace SparkPost
{
    public class SendingDomains : ISendingDomains
    {
        private readonly IClient client;
        private readonly IRequestSender requestSender;
        private readonly IDataMapper dataMapper;

        public SendingDomains(IClient client, IRequestSender requestSender, IDataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<ListSendingDomainResponse> List()
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/sending-domains", 
                Method = "GET"
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            return ListSendingDomainResponse.CreateFromResponse(response);
        }

        public async Task<CreateSendingDomainResponse> Create(SendingDomain sendingDomain)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/sending-domains", 
                Method = "POST",
                Data = sendingDomain
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = Jsonification.DeserializeObject<dynamic>(response.Content).results;

            return new CreateSendingDomainResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Domain = results.domain,
                Dkim = results.dkim != null ? Jsonification.DeserializeObject<Dkim>(results.dkim.ToString()) : null
            };
        }

        public async Task<UpdateSendingDomainResponse> Update(SendingDomain sendingDomain)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/sending-domains/{sendingDomain.Domain}", 
                Method = "DELETE",
                Data = sendingDomain
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = Jsonification.DeserializeObject<dynamic>(response.Content).results;

            return new UpdateSendingDomainResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Domain = results.domain,
                Dkim = results.dkim != null ? Jsonification.DeserializeObject<Dkim>(results.dkim.ToString()) : null
            };
        }

        public async Task<GetSendingDomainResponse> GetByDomain(string domain)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/sending-domains/{domain}", 
                Method = "GET",
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = Jsonification.DeserializeObject<dynamic>(response.Content).results;

            return new GetSendingDomainResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                SendingDomain = results != null ? Jsonification.DeserializeObject<SendingDomain>(results.ToString()) : null
            };
        }

        public async Task<Response> DeleteByDomain(string domain)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/sending-domains/{domain}",
                Method = "DELETE",
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.ReasonPhrase = HttpWorkerRequest.GetStatusDescription((int)HttpStatusCode.OK);
            }

            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);
            return response;
        }

        public async Task<VerifySendingDomainResponse> VerifyByDomain(VerifySendingDomain verifySendingDomain)
        {
            var request = new Request
            {
                Url = $"/api/{client.Version}/sending-domains/{verifySendingDomain.Domain}/verify",
                Method = "POST",
                Data = verifySendingDomain
            };

            var response = await requestSender.Send(request);
            if (response.StatusCode != HttpStatusCode.OK) throw new ResponseException(response);

            var results = Jsonification.DeserializeObject<dynamic>(response.Content).results;

            return new VerifySendingDomainResponse
            {
                ReasonPhrase = response.ReasonPhrase,
                StatusCode = response.StatusCode,
                Content = response.Content,
                Status = results != null ? Jsonification.DeserializeObject<VerifySendingDomainStatus>(results.ToString()) : null
            };
        }
    }
}