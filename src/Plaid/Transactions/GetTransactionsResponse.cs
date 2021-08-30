using Acklann.Plaid.Entity;

using System.Text.Json.Serialization;

namespace Acklann.Plaid.Transactions
{
	/// <summary>
	/// Response to <see cref="GetTransactionsRequest" />
	/// </summary>
	/// <seealso cref="Acklann.Plaid.PlaidResponseBase" />
	public class GetTransactionsResponse : PlaidResponseBase
	{
		/// <summary>
		/// Gets or sets the number of transactions returned.
		/// </summary>
		[JsonPropertyName("total_transactions")]
		public int TransactionsReturned { get; set; }

		/// <summary>
		/// Gets or sets the transactions.
		/// </summary>
		[JsonPropertyName("transactions")]
		public Transaction[] Transactions { get; set; }

		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		[JsonPropertyName("item")]
		public Entity.Item Item { get; set; }

		/// <summary>
		/// Gets or sets the accounts.
		/// </summary>
		[JsonPropertyName("accounts")]
		public Account[] Accounts { get; set; }
	}
}
