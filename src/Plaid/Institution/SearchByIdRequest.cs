using Newtonsoft.Json;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Represents a request for plaid's '/institutions/get_by_id' endpoint. The '/institutions/get_by_id' endpoint to retrieve a <see cref="Entity.Institution"/> with the specified id.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class SearchByIdRequest : SerializableContent
	{
		/// <summary>
		/// Gets or sets the <see cref="Entity.Institution"/> identifier.
		/// </summary>
		/// <value>The institution identifier.</value>
		[JsonProperty("institution_id")]
		public string InstitutionId { get; set; }

		/// <summary>
		/// Gets or sets the secret.
		/// </summary>
		/// <value>The secret.</value>
		[JsonProperty("secret")]
		public string Secret { get; set; }

		/// <summary>
		/// Gets or sets the client_id.
		/// </summary>
		/// <value>The client identifier.</value>
		[JsonProperty("client_id")]
		public string ClientId { get; set; }

		/// <summary>
		/// Gets or sets the country codes.
		/// </summary>
		/// <remarks>Possible values: US, GB, ES, NL, FR, IE, CA</remarks>
		[JsonProperty("country_codes")]
		public string[] CountryCodes { get; set; }

		[JsonProperty("options")]
		public AdditionalOptions Options { get; set; }

		public struct AdditionalOptions
		{
			/// <summary>
			/// Gets or sets a value indicating whether to add an institution's logo, brand color, and URL.
			/// </summary>
			/// <remarks>When available, the bank's logo is returned as a base64 encoded 152x152 PNG, the brand color is in hexadecimal format.</remarks>
			[JsonProperty("include_optional_metadata")]
			public bool InclueMetadata { get; set; }

			/// <summary>
			/// Gets or sets a value indicating whether to  include status information about the institution.
			/// </summary>
			[JsonProperty("include_status")]
			public bool IncludeStatus { get; set; }
		}
	}
}
