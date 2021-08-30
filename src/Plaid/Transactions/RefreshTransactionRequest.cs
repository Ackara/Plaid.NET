using System;

namespace Acklann.Plaid.Transactions
{
	/// <summary>
	/// Request that initiates an on-demand extraction to fetch the newest transactions for an Item.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.AuthorizedRequestBase" />
	public class RefreshTransactionRequest : RequestWithAccessToken
	{
		public RefreshTransactionRequest()
		{
		}

		public RefreshTransactionRequest(string accessToken)
		{
			AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
		}
	}
}
