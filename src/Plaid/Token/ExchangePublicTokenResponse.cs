using System.Text.Json.Serialization;

namespace Acklann.Plaid.Token
{
	/// <summary>
	/// Response for <see cref="Item.ExchangePublicTokenRequest"/>
	/// </summary>
	/// <seealso cref="Acklann.Plaid.PlaidResponseBase" />
	public class ExchangePublicTokenResponse : PlaidResponseBase
	{
		/// <summary>
		/// The access token associated with the Item data is being requested for.
		/// </summary>
		[JsonPropertyName("access_token")]
		public string AsscessToken { get; set; }

		/// <summary>
		/// The item_id value of the Item associated with the returned access_token.
		/// </summary>
		[JsonPropertyName("item_id")]
		public string ItemId { get; set; }
	}
}
