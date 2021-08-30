using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
	/// <summary>
	/// Represents an object specifying information about the end user who will be linking their account.
	/// </summary>
	public class User
	{
		/// <summary>
		/// A unique ID representing the end user.
		/// Typically this will be a user ID number from your application.
		/// Personally identifiable information, such as an email address or phone number, should not be used in the client_user_id. It is currently used as a means of searching logs for the given user in the Plaid Dashboard.
		/// </summary>
		/// <value>The client user id.</value>
		[JsonProperty("client_user_id")]
		public string ClientUserId { get; set; }

		
		public string LegalName { get; set; }
	}
}
