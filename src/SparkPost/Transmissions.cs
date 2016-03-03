using System.Threading.Tasks;

namespace SparkPost
{
    public class Transmissions
    {
        private readonly Client client;
        private readonly RequestSender requestSender;
        private readonly DataMapper dataMapper;

        public Transmissions(Client client, RequestSender requestSender, DataMapper dataMapper)
        {
            this.client = client;
            this.requestSender = requestSender;
            this.dataMapper = dataMapper;
        }

        public async Task<Response> Send(Transmission transmission)
        {
            var request = new Request
            {
                Method = string.Format("api/{0}/transmissions", client.Version),
                Data = dataMapper.ToDictionary(transmission)
            };

            return await requestSender.Send(request);
        }
    }
}