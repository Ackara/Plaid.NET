using Newtonsoft.Json;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Represents a request for plaid's '/institutions/get' endpoint. The '/institutions/get' endpoint to retrieve a <see cref="Entity.Institution"/> with the specified id.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class SearchAllRequest : SerializableContent
	{
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
		/// Gets or sets the total number of Institutions to return.
		/// </summary>
		[JsonProperty("count")]
		public int Take { get; set; }

		/// <summary>
		/// Gets or sets the number of Institutions to skip before returning results.
		/// </summary>
		[JsonProperty("offset")]
		public int Skip { get; set; }

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
			/// Gets or sets the supported products.
			/// </summary>
			/// <value>The products.</value>
			[JsonProperty("products")]
			public string[] Products { get; set; }

			/// <summary>
			/// Gets or sets the routing numbers to filter institutions.
			/// </summary>
			[JsonProperty("routing_numbers")]
			public string[] RoutingNumbers { get; set; }

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
