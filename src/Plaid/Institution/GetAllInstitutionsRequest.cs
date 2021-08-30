using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Request to get details of all supported institutions.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase2" />
	public class GetAllInstitutionsRequest : RequestBase2
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetAllInstitutionsRequest"/> class.
		/// </summary>
		public GetAllInstitutionsRequest()
		{
			Count = 100;
			CountryCodes = new string[] { "US" };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GetAllInstitutionsRequest"/> class.
		/// </summary>
		/// <param name="count">The page size.</param>
		/// <param name="offset">The number of items loaded thus far.</param>
		/// <param name="countryCodes">The country codes.</param>
		/// <exception cref="System.ArgumentOutOfRangeException">count - Cannot be greater than 500</exception>
		/// <exception cref="System.ArgumentNullException">countryCodes</exception>
		public GetAllInstitutionsRequest(int count, int offset, params string[] countryCodes)
		{
			int max = 500;
			if (count > max) throw new ArgumentOutOfRangeException(nameof(count), $"Cannot be greater than {max}");

			Count = count;
			Offset = offset;
			CountryCodes = countryCodes ?? throw new ArgumentNullException(nameof(countryCodes));
		}

		/// <summary>
		/// The total number of Institutions to return. Maximum: 500
		/// </summary>
		[JsonPropertyName("count")]
		public int Count { get; set; }

		/// <summary>
		/// The number of Institutions to skip.
		/// </summary>
		[JsonPropertyName("offset")]
		public int Offset { get; set; }

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
		/// An optional object to filter /institutions/get results.
		/// </summary>
		[JsonPropertyName("options")]
		public MoreOptions Options { get; set; }

		public struct MoreOptions
		{
			/// <summary>
			/// Filter the Institutions based on which products they support.
			/// </summary>
			[JsonPropertyName("products")]
			public string[] Products { get; set; }

			/// <summary>
			/// Specify an array of routing numbers to filter institutions. The response will only return institutions that match all of the routing numbers in the array.
			/// </summary>
			[JsonPropertyName("routing_numbers")]
			public string[] RoutingNumber { get; set; }

			/// <summary>
			/// Limit results to institutions with or without OAuth login flows. This is primarily relevant to institutions with European country codes.
			/// </summary>
			/// <value>
			///   <c>true</c> if [o authentication]; otherwise, <c>false</c>.
			/// </value>
			[JsonPropertyName("oauth")]
			public bool OAuth { get; set; }

			/// <summary>
			/// When true, return the institution's homepage URL, logo and primary brand color.
			/// </summary>
			[JsonPropertyName("include_optional_metadata")]
			public bool IncludeOptionalMetadata { get; set; }

			/// <summary>
			/// When true, returns metadata related to the Payment Initiation product indicating which payment configurations are supported.
			/// </summary>
			[JsonPropertyName("include_payment_initiation_metadata")]
			public bool IncludePaymentInitiationMetadata { get; set; }
		}
	}
}
