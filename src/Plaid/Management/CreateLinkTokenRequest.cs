using Newtonsoft.Json;
using System.Collections.Generic;

namespace Acklann.Plaid.Management
{
    /// <summary>
    /// Represents a request for plaid's '/link/token/create' endpoint. Create a link_token.
    /// </summary>
    /// <seealso cref="Acklann.Plaid.SerializableContent" />
    public class CreateLinkTokenRequest : SerializableContent
    {
        public CreateLinkTokenRequest()
        {
            Language = "en";
        }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>The client identifier.</value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        [JsonProperty("secret")]
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>The client name.</value>
        [JsonProperty("client_name")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the country codes.
        /// </summary>
        /// <value>The country codes.</value>
        [JsonProperty("country_codes")]
        public string[] CountryCodes { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        [JsonProperty("user")]
        public UserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        [JsonProperty("products")]
        public string[] Products { get; set; }

        /// <summary>
        /// Gets or sets the webhook.
        /// </summary>
        /// <value>The webhook.</value>
        [JsonProperty("webhook")]
        public string Webhook { get; set; }

        /// <summary>
        /// Gets or sets the link customization name.
        /// </summary>
        /// <value>The link customization name.</value>
        [JsonProperty("link_customization_name")]
        public string LinkCustomizationName { get; set; }

        /// <summary>
        /// Gets or sets the account filters.
        /// </summary>
        /// <value>The account filters.</value>
        [JsonProperty("account_filters")]
        public Dictionary<string, List<string>> AccountFilters { get; set; }

        /// <summary>
        /// Gets or sets the access_token.
        /// </summary>
        /// <value>The access token.</value>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the redirect uri.
        /// </summary>
        /// <value>The redirect uri.</value>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the payment initiation.
        /// </summary>
        /// <value>The payment initiation.</value>
        /// <remarks>Payment initiation still needs to be typed and fully implemented.</remarks>
        [JsonProperty("payment_initiation")]
        public object PaymentInitiation { get; set; }

        /// <summary>
        /// Represents an <see cref="Entity.User"/> metadata.
        /// </summary>
        public struct UserInfo
        {
            /// <summary>
            /// Gets or sets the <see cref="Entity.User"/> client user id.
            /// </summary>
            /// <value>The client user id.</value>
            [JsonProperty("client_user_id")]
            public string ClientUserId { get; set; }
        }
    }
}