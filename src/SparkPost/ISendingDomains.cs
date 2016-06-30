using System.Threading.Tasks;

namespace SparkPost
{
    public interface ISendingDomains
    {
        Task<ListSendingDomainResponse> List();
        Task<CreateSendingDomainResponse> Create(SendingDomain sendingDomain);
        Task<UpdateSendingDomainResponse> Update(SendingDomain sendingDomain);
        Task<GetSendingDomainResponse> GetByDomain(string domain);
        Task<Response> DeleteByDomain(string domain);
        Task<VerifySendingDomainResponse> VerifyByDomain(VerifySendingDomain verifySendingDomain);
    }
}