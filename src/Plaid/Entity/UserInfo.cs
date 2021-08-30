using System.Text.Json.Serialization;

// TODO: add date properties

namespace Acklann.Plaid.Entity
{
	/// <summary>
	/// Represents an object specifying information about the end user who will be linking their account.
	/// </summary>
	public class UserInfo
	{
		/// <summary>
		/// A unique ID representing the end user.
		/// Typically this will be a user ID number from your application.
		/// Personally identifiable information, such as an email address or phone number, should not be used in the client_user_id. It is currently used as a means of searching logs for the given user in the Plaid Dashboard.
		/// </summary>
		/// <value>The client user id.</value>
		[JsonPropertyName("client_user_id")]
		public string ClientUserId { get; set; }

		/// <summary>
		/// The user's full legal name.
		/// This is an optional field used in the returning user experience to associate Items to the user.
		/// </summary>
		[JsonPropertyName("legal_name")]
		public string LegalName { get; set; }

		/// <summary>
		/// The user's phone number in E.164 format.
		/// This field is optional, but required to enable the returning user experience.
		/// </summary>
		[JsonPropertyName("phone_number")]
		public string Phone { get; set; }

		/// <summary>
		/// The date and time the phone number was verified in ISO 8601 format (YYYY-MM-DDThh:mm:ssZ).
		/// This field is optional, but required to enable any returning user experience.
		/// </summary>
		/// <remarks>
		/// Only pass a verification time for a phone number that you have verified. If you have performed verification but don’t have the time, you may supply a signal value of the start of the UNIX epoch.
		/// </remarks>
		[JsonPropertyName("phone_number_verified_time")]
		public string PhoneNumberVerifiedTime { get; set; }

		/// <summary>
		/// The user's email address.
		/// This field is optional, but required to enable the pre-authenticated returning user flow.
		/// </summary>
		[JsonPropertyName("email_address")]
		public string Email { get; set; }

		/// <summary>
		/// The date and time the email address was verified in ISO 8601 format (YYYY-MM-DDThh:mm:ssZ). This is an optional field used in the returning user experience.
		/// </summary>
		/// <remarks>
		///  Only pass a verification time for an email address that you have verified. If you have performed verification but don’t have the time, you may supply a signal value of the start of the UNIX epoch.
		/// </remarks>
		[JsonPropertyName("email_address_verified_time")]
		public string EmailAddressVerifiedTime { get; set; }

		/// <summary>
		/// To be provided in the format "ddd-dd-dddd". This field is optional and will support not-yet-implemented functionality for new products.
		/// </summary>
		[JsonPropertyName("ssn")]
		public string SSN { get; set; }

		/// <summary>
		/// To be provided in the format "yyyy-mm-dd". This field is optional and will support not-yet-implemented functionality for new products.
		/// </summary>
		[JsonPropertyName("date_of_birth")]
		public string DateOfBirth { get; set; }
	}
}
