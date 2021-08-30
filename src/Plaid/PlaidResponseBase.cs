using System.Text.Json.Serialization;

namespace Acklann.Plaid
{
	public abstract class PlaidResponseBase
	{
		/// <summary>
		/// A unique identifier for the request, which can be used for troubleshooting.
		/// This identifier, like all Plaid identifiers, is case sensitive.
		/// </summary>
		[JsonPropertyName("request_id")]
		public string RequestId { get; set; }
	}
}
