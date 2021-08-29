using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a response from plaid's '<c>/processor/stripe/bank_account_token/create</c>' endpoint. Exchange a Link <see cref="ExchangeTokenResponse.AccessToken"/> for a <see cref="ProcessorTokenResponse.ProcessorToken"/>.
	/// </summary>
	public class ProcessorTokenResponse : ResponseBase
	{
		/// <summary>
		/// The <c>processor_token</c> that can then be used by the Plaid partner to make API requests
		/// </summary>
		[JsonProperty("processor_token")]
		public string ProcessorToken { get; set; } = string.Empty;
	}
}
