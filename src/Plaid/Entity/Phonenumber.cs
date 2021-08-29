using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
	public class Phonenumber
	{
		/// <summary>
		/// Gets or sets the phone number.
		/// </summary>
		[JsonProperty("data")]
		public string Data { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Phonenumber"/> is primary.
		/// </summary>
		[JsonProperty("primary")]
		public bool Primary { get; set; }

		/// <summary>
		/// Gets or sets the type of phone number as described by the financial institution.
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
