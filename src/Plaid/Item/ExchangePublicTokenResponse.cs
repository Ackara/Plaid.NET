namespace Acklann.Plaid.Item
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
		public string AsscessToken { get; set; }

		/// <summary>
		/// The item_id value of the Item associated with the returned access_token.
		/// </summary>
		public string ItemId { get; set; }
	}
}
