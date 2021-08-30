using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Request to get details of an institution.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase2" />
	public class GetInstitutionByIdRequest : RequestBase2
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetInstitutionByIdRequest"/> class.
		/// </summary>
		public GetInstitutionByIdRequest()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GetInstitutionByIdRequest"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="countryCodes">The country codes.</param>
		/// <exception cref="System.ArgumentNullException">id</exception>
		public GetInstitutionByIdRequest(string id, params string[] countryCodes)
		{
			InstitutionId = id ?? throw new ArgumentNullException(nameof(id));
			CountryCodes = countryCodes;
		}

		/// <summary>
		/// The ID of the institution to get details about.
		/// </summary>
		/// <value>The institution identifier.</value>
		[JsonPropertyName("institution_id")]
		public string InstitutionId { get; set; }

		/// <summary>
		/// Specify an array of Plaid-supported country codes this institution supports, using the ISO-3166-1 alpha-2 country code standard.
		/// Possible Values:
		/// <list type="bullet">
		/// <item>US</item>
		/// <item>GB</item>
		/// <item>ES</item>
		/// <item>NL</item>
		/// <item>IE</item>
		/// <item>CA</item>
		/// </list>
		/// </summary>
		[JsonPropertyName("country_codes")]
		public string[] CountryCodes { get; set; }

		/// <summary>
		/// Specifies optional parameters for /institutions/get_by_id. If provided, must not be null.
		/// </summary>
		[JsonPropertyName("options")]
		public AdditionalOptions Options { get; set; }

		public struct AdditionalOptions
		{
			/// <summary>
			/// When true, return an institution's logo, brand color, and URL. When available, the bank's logo is returned as a base64 encoded 152x152 PNG, the brand color is in hexadecimal format. The default value is false.
			/// </summary>
			[JsonPropertyName("include_optional_metadata")]
			public bool IncludeMetadata { get; set; }

			/// <summary>
			/// If true, the response will include status information about the institution. Default value is false.
			/// </summary>
			[JsonPropertyName("include_status")]
			public bool IncludeStatus { get; set; }

			/// <summary>
			/// When true, returns metadata related to the Payment Initiation product indicating which payment configurations are supported.
			/// </summary>
			[JsonPropertyName("include_payment_initiation_metadata")]
			public bool IncludePaymentInitiationMetadata { get; set; }
		}
	}
}
