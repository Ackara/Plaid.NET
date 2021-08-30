using System;

namespace Acklann.Plaid.Item
{
	/// <summary>
	/// Request to retrieve an <see cref="Entity.Item"/>.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.AuthorizedRequestBase" />
	public class GetItemRequest : RequestWithAccessToken
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetItemRequest"/> class.
		/// </summary>
		public GetItemRequest()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GetItemRequest"/> class.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <exception cref="System.ArgumentNullException">accessToken</exception>
		public GetItemRequest(string accessToken)
		{
			AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
		}
	}
}
