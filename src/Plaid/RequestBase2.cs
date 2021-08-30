using System.Text.Json.Serialization;

namespace Acklann.Plaid
{
	public abstract class RequestBase2
	{
		[JsonPropertyName("client_id")]
		public string ClientId { get; set; }

		[JsonPropertyName("secret")]
		public string Secret { get; set; }

		[JsonIgnore]
		internal string RawJson { get; set; }
	}
}
