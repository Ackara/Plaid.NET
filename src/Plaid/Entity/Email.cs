using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
	public class Email
	{
		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		[JsonProperty("data")]
		public string Data { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Email"/> is primary.
		/// </summary>
		[JsonProperty("primary")]
		public bool Primary { get; set; }

		/// <summary>
		/// Gets or sets the type of email account as described by the financial institution.
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}
