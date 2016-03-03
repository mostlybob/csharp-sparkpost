using System.Threading.Tasks;

namespace SparkPost
{
    public class Transmissions
    {
        private readonly RequestSender requestSender;

        public Transmissions(Client client)
        {
            requestSender = new RequestSender(client);
        }

        public async Task<Response> Send(Transmission transmission)
        {
            var request = new Request
            {
                Method = "api/v1/transmissions",
                Data = transmission.ToDictionary()
            };

            return await requestSender.Send(request);
        }
    }
}