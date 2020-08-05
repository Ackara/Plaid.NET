using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
    /// <summary>
    /// Represents a response from plaid's '/link/token/create' endpoint. Create a link_token.
    /// </summary>
    /// <seealso cref="Acklann.Plaid.ResponseBase" />
    public class CreateLinkTokenResponse : ResponseBase
    {
        /// <summary>
        /// Gets or sets the link token.
        /// </summary>
        /// <value>The link token.</value>
        [JsonProperty("link_token")]
        public string LinkToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        /// <value>The expiration.</value>
        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }
}