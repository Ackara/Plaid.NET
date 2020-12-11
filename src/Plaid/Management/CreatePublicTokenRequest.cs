namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/item/public_token/create' endpoint. Create a public_token from an access_token for use with Plaid LInk's update mode.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.AuthorizedRequestBase" />
	public class CreatePublicTokenRequest : AuthorizedRequestBase
	{
	}
}
