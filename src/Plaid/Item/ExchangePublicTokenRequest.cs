using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Item
{
	/// <summary>
	/// Exchange a Link public_token for an API access_token. Link hands off the public_token client-side via the onSuccess callback once a user has successfully created an Item. The public_token is ephemeral and expires after 30 minutes.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase2" />
	public class ExchangePublicTokenRequest : RequestBase2
	{
		public ExchangePublicTokenRequest()
		{
		}

		public ExchangePublicTokenRequest(string publicToken)
		{
			PublicToken = publicToken ?? throw new ArgumentNullException(nameof(publicToken));
		}

		/// <summary>
		/// Your public_token, obtained from the Link onSuccess callback or /sandbox/item/public_token/create.
		/// </summary>
		[JsonPropertyName("public_token")]
		public string PublicToken { get; set; }
	}
}
