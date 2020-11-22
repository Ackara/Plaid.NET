using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/item/public_token/exchange' endpoint. Exchange a Link public_token for an API access_token.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class ExchangeTokenRequest : SerializableContent
	{
		[JsonProperty("secret")]
        public string Secret { get; set; }
        [JsonProperty("public_token")]
        public string PublicToken { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
	}
}
