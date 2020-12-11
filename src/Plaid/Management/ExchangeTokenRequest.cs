using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/item/public_token/exchange' endpoint. Exchange a Link public_token for an API access_token.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class ExchangeTokenRequest : RequestBase
	{
		/// <summary>
		/// Gets or sets the public token.
		/// </summary>
		[JsonProperty("public_token")]
		public string PublicToken { get; set; }
	}
}
