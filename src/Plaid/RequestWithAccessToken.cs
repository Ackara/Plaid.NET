using System.Text.Json.Serialization;

namespace Acklann.Plaid
{
	public abstract class RequestWithAccessToken : RequestBase2
	{
		/// <summary>
		/// The access_token associated with the Item to update, used when updating or modifying an existing access_token. Used when launching Link in update mode, when completing the Same-day (manual) Micro-deposit flow, or (optionally) when initializing Link as part of the Payment Initiation (UK and Europe) flow.
		/// </summary>
		/// <value>The access token.</value>
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }
	}
}
