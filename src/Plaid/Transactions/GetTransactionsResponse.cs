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
		/// An array containing the accounts associated with the Item for which transactions are being returned.
		/// Each transaction can be mapped to its corresponding account via the account_id field.
		/// </summary>
		[JsonPropertyName("accounts")]
		public Account[] Accounts { get; set; }

		/// <summary>
		/// An array containing transactions from the account. Transactions are returned in reverse chronological order, with the most recent at the beginning of the array.
		/// The maximum number of transactions returned is determined by the count parameter.
		/// </summary>
		[JsonPropertyName("transactions")]
		public Transaction[] Transactions { get; set; }

		/// <summary>
		/// The total number of transactions available within the date range specified.
		/// If total_transactions is larger than the size of the transactions array, more transactions are available and can be fetched via manipulating the offset parameter.
		/// </summary>
		[JsonPropertyName("total_transactions")]
		public int TotalTransactions { get; set; }

		/// <summary>
		/// Metadata about the Item.
		/// </summary>
		[JsonPropertyName("item")]
		public Entity.Item Item { get; set; }
	}
}
