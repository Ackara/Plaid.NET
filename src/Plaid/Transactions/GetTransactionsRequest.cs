using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Transactions
{
	/// <summary>
	/// Request to receive user-authorized transaction data for credit, depository, and some loan-type accounts (only those with account subtype student; coverage may be limited).
	/// </summary>
	public class GetTransactionsRequest : RequestWithAccessToken
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetTransactionsRequest"/> class.
		/// </summary>
		public GetTransactionsRequest()
		{
		}

		public GetTransactionsRequest(string accessToken, DateTime startDate, DateTime endDate)
		{
			AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
			StartDate = startDate.ToPlaidDate();
			EndDate = endDate.ToPlaidDate();
		}

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>The start date.</value>
		[JsonPropertyName("start_date")]
		public string StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>The end date.</value>
		[JsonPropertyName("end_date")]
		public string EndDate { get; set; }

		/// <summary>
		/// Gets or sets the pagination options.
		/// </summary>
		/// <value>The pagination options.</value>
		[JsonPropertyName("options")]
		public PaginationOptions Options { get; set; }

		/// <summary>
		/// Represents pagination options.
		/// </summary>
		public class PaginationOptions
		{
			/// <summary>
			/// Gets or sets the number of transactions to fetch, where 0 &lt; count &lt;= 500.
			/// </summary>
			/// <value>The number of transactions to return.</value>
			[JsonPropertyName("count")]
			public uint Total
			{
				get { return _count; }
				set
				{
					if (value < 0) _count = 0;
					else if (value > 500) _count = 500;
					else _count = value;
				}
			}

			/// <summary>
			/// Gets or sets the number of transactions to skip, where offset &gt;= 0.
			/// </summary>
			/// <value>The offset.</value>
			[JsonPropertyName("offset")]
			public uint Offset { get; set; }

			/// <summary>
			/// Gets or sets the list of account ids to retrieve for the <see cref="Entity.Item" />. Note: An error will be returned if a provided account_id is not associated with the <see cref="Entity.Item" />.
			/// </summary>
			/// <value>The account ids.</value>
			[JsonPropertyName("account_ids")]
			public string[] AccountIds { get; set; }

			#region Private Members

			private uint _count = 100;

			#endregion Private Members
		}
	}
}
