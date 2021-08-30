using Acklann.Plaid.Entity;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Balance
{
	/// <summary>
	/// Response to <see cref="GetBalanceRequest" />.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.PlaidResponseBase" />
	public class GetBalanceResponse : PlaidResponseBase
	{
		/// <summary>
		/// An array of financial institution accounts associated with the Item.
		/// </summary>
		[JsonPropertyName("accounts")]
		public Account[] Accounts { get; set; }

		/// <summary>
		/// Metadata about the Item.
		/// </summary>
		[JsonPropertyName("item")]
		public Entity.Item Item { get; set; }
	}
}
