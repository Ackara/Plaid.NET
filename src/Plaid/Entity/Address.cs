using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
	public class Address
	{
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Address"/> is primary.
		/// </summary>
		[JsonProperty("primary")]
		public bool Primary { get; set; }

		/// <summary>
		/// Gets or sets the data about the components comprising an address; see Address data object for fields.
		/// </summary>
		[JsonProperty("data")]
		public Data Info { get; set; }

		public struct Data
		{
			/// <summary>
			/// Gets or sets the full city name.
			/// </summary>
			[JsonProperty("city")]
			public string City { get; set; }

			/// <summary>
			/// Gets or sets the region or state.
			/// </summary>
			[JsonProperty("region")]
			public string Region { get; set; }

			/// <summary>
			/// Gets or sets the full street address.
			/// </summary>
			[JsonProperty("street")]
			public string Street { get; set; }

			/// <summary>
			/// Gets or sets the postal code.
			/// </summary>
			[JsonProperty("postal_code")]
			public string PostalCode { get; set; }

			/// <summary>
			/// Gets or sets the ISO 3166-1 alpha-2 country code.
			/// </summary>
			[JsonProperty("country")]
			public string Country { get; set; }
		}
	}
}
