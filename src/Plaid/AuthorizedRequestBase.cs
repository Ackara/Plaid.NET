using Newtonsoft.Json;

namespace Acklann.Plaid
{
	/// <summary>
	/// Provides methods and properties for making a standard request.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase" />
	public abstract class AuthorizedRequestBase : RequestBase
	{
		/// <summary>
		/// Gets or sets the access_token.
		/// </summary>
		/// <value>The access token.</value>
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }
	}
}
