using System.Threading.Tasks;

namespace SparkPost
{
    public interface IMessageEvents
    {
        Task<ListMessageEventsResponse> List(MessageEventsQuery messageEventsQuery);
        Task<ListMessageEventsResponse> List(object query = null);
    }
}