using Acklann.Plaid.Institution;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Acklann.Plaid.Entity
{
    /// <summary>
    /// Represents a banking institution.
    /// </summary>
    public class Institution
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [JsonProperty("institution_id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has Multi-Factor Authentication.
        /// </summary>
        /// <value><c>true</c> if this instance has Multi-Factor Authentication; otherwise, <c>false</c>.</value>
        [JsonProperty("has_mfa")]
        public bool HasMfa { get; set; }

        /// <summary>
        /// Gets or sets the Multi-Factor Authentication selections.
        /// </summary>
        /// <value>The mfa selections.</value>
        [JsonProperty("mfa")]
        public string[] MfaSelections { get; set; }

        [JsonProperty("mfa_code_type")]
        public string MfaType { get; set; }

        /// <summary>
        /// Gets or sets the hexadecimal representation of the primary color used by the institution.
        /// </summary>
        [JsonProperty("primary_color")]
        public string PrimaryColor { get; set; }

        /// <summary>
        /// Gets or sets the Base64 encoded representation of the institution's logo.
        /// </summary>
        [JsonProperty("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// Gets or sets the URL for the institution's website.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the credentials.
        /// </summary>
        /// <value>The credentials.</value>
        [JsonProperty("credentials")]
        public Credential[] Credentials { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>The products.</value>
        [JsonProperty("products")]
        public string[] Products { get; set; }

        /// <summary>
        /// Gets or sets the country codes using the ISO-3166-1 alpha-2 country code standard.
        /// </summary>
        /// <value>
        /// The countries.
        /// </value>
        [JsonProperty("country_codes")]
        public string[] Countries { get; set; }

        /// <summary>
        /// Gets or sets the information about the institution's current status.
        /// </summary>
        [JsonProperty("status")]
        public StatusSchema Status { get; set; }

        /// <summary>
        /// Represents an <see cref="Institution"/> login credentials.
        /// </summary>
        public struct Credential
        {
            /// <summary>
            /// Gets or sets the label.
            /// </summary>
            /// <value>The label.</value>
            [JsonProperty("label")]
            public string Label { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the type of the data.
            /// </summary>
            /// <value>The type of the data.</value>
            [JsonProperty("type")]
            public string DataType { get; set; }
        }

        public class StatusSchema
        {
            /// <summary>
            /// Gets or sets status information regarding Item adds via Link.
            /// </summary>
            [JsonProperty("item_logins")]
            public InstitutionStatus ItemLogin { get; set; }

            /// <summary>
            /// Gets or sets the status information regarding transactions updates.
            /// </summary>
            [JsonProperty("transactions_updates")]
            public InstitutionStatus Transactions { get; set; }

            /// <summary>
            /// Gets or sets the status information regarding Auth requests..
            /// </summary>
            /// <value>
            /// The authentication.
            /// </value>
            [JsonProperty("auth")]
            public InstitutionStatus Auth { get; set; }

            /// <summary>
            /// Gets or sets the status information regarding Balance requests.
            /// </summary>
            /// <value>
            /// The balance.
            /// </value>
            [JsonProperty("balacne")]
            public InstitutionStatus Balance { get; set; }

            /// <summary>
            /// Gets or sets the status information regarding Identity requests.
            /// </summary>
            [JsonProperty("identity")]
            public InstitutionStatus Identity { get; set; }
        }
    }
}