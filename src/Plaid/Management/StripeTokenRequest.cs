using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/processor/stripe/bank_account_token/create' endpoint. Exchange a Link access_token for an Stripe API stripe_bank_account_token and request_id.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class StripeTokenRequest : AuthorizedRequestBase
	{
		/// <summary>
		/// Gets or sets the account id.
		/// </summary>
		/// <value>The account id.</value>
		[JsonProperty("account_id")]
		public string AccountId { get; set; }
	}
}
