using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the client user id.
        /// </summary>
        /// <value>The client user id.</value>
        [JsonProperty("client_user_id")]
        public string ClientUserId { get; set; }
    }
}
