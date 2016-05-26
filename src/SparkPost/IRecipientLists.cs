using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRecipientLists
    {
        /// <summary>
        /// Sends an email transmission.
        /// </summary>
        /// <param name="recipientList">The properties of the recipientList to send.</param>
        /// <returns>The response from the API.</returns>
        Task<SendRecipientListsResponse> Send(RecipientList recipientList);

        /// <summary>
        /// Retrieves an email transmission.
        /// </summary>
        /// <param name="recipientListsId">The id of the transmission to retrieve.</param>
        /// <returns>The response from the API.</returns>
        Task<RetrieveRecipientListsResponse> Retrieve(string recipientListsId);
    }
}