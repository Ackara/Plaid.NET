using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
    /// <summary>
    /// Represents an account balance.
    /// </summary>
    public struct Balance
    {
        /// <summary>
        /// Gets or sets the current balance.
        /// </summary>
        /// <value>The current.</value>
        [JsonProperty("current")]
        public decimal Current { get; set; }

        /// <summary>
        /// Gets or sets the available balance.
        /// </summary>
        /// <value>The available.</value>
        [JsonProperty("available")]
        public decimal? Available { get; set; }

        /// <summary>
        /// Gets or sets the account limit.
        /// </summary>
        /// <value>The limit.</value>
        [JsonProperty("limit")]
        public decimal? Limit { get; set; }

        /// <summary>
        /// Gets or sets the iso currency code.
        /// </summary>
        /// <value>currency code.</value>
        [JsonProperty("iso_currency_code")]
        public string ISOCurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the unofficial currency code.
        /// </summary>
        /// <value>currency code.</value>
        [JsonProperty("unofficial_currency_code")]
        public string UnofficialCurrencyCode { get; set; }

    }
}