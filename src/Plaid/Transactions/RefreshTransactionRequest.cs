namespace Acklann.Plaid.Transactions
{
	/// <summary>
	/// Represents a request for plaid's '/transactions/refresh' endpoint.
	/// </summary>
	/// <remarks>/transactions/refresh is an optional endpoint for users of the Transactions product. It initiates an on-demand extraction to fetch the newest transactions for an Item. This on-demand extraction takes place in addition to the periodic extractions that automatically occur multiple times a day for any Transactions-enabled Item. If changes to transactions are discovered after calling /transactions/refresh, Plaid will fire a webhook: TRANSACTIONS_REMOVED will be fired if any removed transactions are detected, and DEFAULT_UPDATE will be fired if any new transactions are detected. New transactions can be fetched by calling /transactions/get.</remarks>
	/// <seealso cref="Acklann.Plaid.RequestBase" />
	public class RefreshTransactionRequest : RequestBase
	{
	}
}
