using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkPost
{
    public interface IRecipientLists
    {
        /// <summary>
        /// Creates a recipient list.
        /// </summary>
        /// <param name="recipientList">The properties of the recipientList to create.</param>
        /// <returns>The response from the API.</returns>
        Task<SendRecipientListsResponse> Create(RecipientList recipientList);

        /// <summary>
        /// Retrieves an email transmission.
        /// </summary>
        /// <param name="recipientListsId">The id of the transmission to retrieve.</param>
        /// <returns>The response from the API.</returns>
        Task<RetrieveRecipientListsResponse> Retrieve(string recipientListsId);
    }
}