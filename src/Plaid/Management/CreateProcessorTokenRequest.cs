using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/processor/token/create' endpoint. Create a processor_token from an access_token.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class CreateProcessorTokenRequest : SerializableContent
	{
		/// <summary>
		/// Gets or sets the client identifier.
		/// </summary>
		/// <value>The client identifier.</value>
		[JsonProperty("client_id")]
		public string ClientId { get; set; }

		/// <summary>
		/// Gets or sets the secret.
		/// </summary>
		/// <value>The secret.</value>
		[JsonProperty("secret")]
		public string Secret { get; set; }

		/// <summary>
		/// Gets or sets the access_token.
		/// </summary>
		/// <value>The access token.</value>
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		/// Gets or sets the client_id.
		/// </summary>
		/// <value>The account identifier.</value>
		[JsonProperty("account_id")]
		public string AccountId { get; set; }

		/// <summary>
		/// Gets or sets the client_id.
		/// </summary>
		/// <value>The processor you are integrating with.</value>
		[JsonProperty("processor")]
		public string Processor { get; set; }
	}
}
