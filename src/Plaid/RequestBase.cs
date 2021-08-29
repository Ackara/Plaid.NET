using Newtonsoft.Json;

namespace Acklann.Plaid
{
	/// <summary>
	/// Provides methods and properties for making a standard request.
	/// </summary>
	public class RequestBase : SerializableContent
	{
		/// <summary>
		/// Gets or sets the secret.
		/// </summary>
		/// <value>The secret.</value>
		[JsonProperty("secret")]
		public string Secret { get; set; }

		/// <summary>
		/// Gets or sets the client_id.
		/// </summary>
		/// <value>The client identifier.</value>
		[JsonProperty("client_id")]
		public string ClientId { get; set; }
	}
}
