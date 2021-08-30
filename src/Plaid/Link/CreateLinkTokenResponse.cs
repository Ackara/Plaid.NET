using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Link
{
	public class CreateLinkTokenResponse : PlaidResponseBase
	{
		/// <summary>
		/// The expiration date for the link_token, in ISO 8601 format.
		/// A link_token created to generate a public_token that will be exchanged for a new access_token expires after 4 hours.
		/// A link_token created for an existing Item (such as when updating an existing access_token by launching Link in update mode) expires after 30 minutes.
		/// </summary>
		[JsonPropertyName("expiration")]
		public DateTime ExpirationDate { get; set; }

		/// <summary>
		/// A link_token, which can be supplied to Link in order to initialize it and receive a public_token, which can be exchanged for an access_token.
		/// </summary>
		[JsonPropertyName("link_token")]
		public string LinkToken { get; set; }
	}
}
