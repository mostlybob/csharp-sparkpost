using System.Threading.Tasks;

namespace SparkPost
{
    public interface ITransmissions
    {
        Task<SendTransmissionResponse> Send(Transmission transmission);
    }
}