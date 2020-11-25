using Newtonsoft.Json;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Represents a request for plaid's '/institutions/search' endpoints. The '/institutions/search' endpoint to retrieve a complete list of supported institutions that match the query.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class SearchRequest : SerializableContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SearchRequest"/> class.
		/// </summary>
		public SearchRequest()
		{
			NullValueHandling = NullValueHandling.Include;
			Options = new AdditionalOptions();
		}

		/// <summary>
		/// Gets or sets the client_id.
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
		/// Gets or sets the query.
		/// </summary>
		/// <value>The query.</value>
		[JsonProperty("query")]
		public string Query { get; set; }

		/// <summary>
		/// Gets or sets the supported products.
		/// </summary>
		/// <value>The products.</value>
		[JsonProperty("products")]
		public string[] Products { get; set; }

		/// <summary>
		/// Gets or sets the country code using the ISO-3166-1 alpha-2 country code standard.
		/// </summary>
		/// <remarks>Possible values: US, GB, ES, NL, FR, IE, CA</remarks>
		[JsonProperty("country_codes")]
		public string[] CountryCodes { get; set; }

		[JsonProperty("options")]
		public AdditionalOptions Options { get; set; }

		public struct AdditionalOptions
		{
			/// <summary>
			/// Gets or sets a value indicating whether to limit results to institutions with or without OAuth login flows. This is only relevant to institutions with European country codes.
			/// </summary>
			[JsonProperty("oauth")]
			public bool? Oauth { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether to add an institution's logo, brand color, and URL.
			/// </summary>
			/// <remarks>When available, the bank's logo is returned as a base64 encoded 152x152 PNG, the brand color is in hexadecimal format.</remarks>
			[JsonProperty("include_optional_metadata")]
			public bool InclueMetadata { get; set; }
		}
	}
}
