using System.Text.Json.Serialization;

namespace Acklann.Plaid.Sandbox
{
	/// <summary>
	/// Response to <see cref="CreatePublicTokenRequest"/>
	/// </summary>
	/// <seealso cref="Acklann.Plaid.PlaidResponseBase" />
	public class CreatePublicTokenResponse : PlaidResponseBase
	{
		/// <summary>
		/// A public token that can be exchanged for an access token using <see cref="Client.ExchangeTokenForAccessTokenAsync(Item.ExchangePublicTokenRequest)"/>.
		/// </summary>
		[JsonPropertyName("public_token")]
		public string PublicToken { get; set; }
	}
}
