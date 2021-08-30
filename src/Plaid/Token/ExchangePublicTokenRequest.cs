using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Token
{
	/// <summary>
	/// Request to exchange public token for an access token.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase2" />
	public class ExchangePublicTokenRequest : RequestBase2
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ExchangePublicTokenRequest"/> class.
		/// </summary>
		public ExchangePublicTokenRequest()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExchangePublicTokenRequest"/> class.
		/// </summary>
		/// <param name="publicToken">The public token.</param>
		/// <exception cref="System.ArgumentNullException">publicToken</exception>
		public ExchangePublicTokenRequest(string publicToken)
		{
			PublicToken = publicToken ?? throw new ArgumentNullException(nameof(publicToken));
		}

		/// <summary>
		/// Your public_token, obtained from the Link onSuccess callback or <see cref="Client.CreateSandboxPublicTokenAsync(Sandbox.CreatePublicTokenRequest)"/>.
		/// </summary>
		[JsonPropertyName("public_token")]
		public string PublicToken { get; set; }
	}
}
