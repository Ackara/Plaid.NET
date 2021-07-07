using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the client user id.
        /// </summary>
        /// <value>The client user id.</value>
        [JsonProperty("client_user_id")]
        public string ClientUserId { get; set; }

	/// <summary>
	/// Gets or sets the client legal name.
	/// </summary>
	/// <value>The client legal name.</value>
	[JsonProperty("legal_name")]
	public string LegalName { get; set; }

	/// <summary>
	/// Gets or sets the client phone number.
	/// </summary>
	/// <value>The client phone number.</value>
	[JsonProperty("phone_number")]
	public string PhoneNumber { get; set; }

	/// <summary>
	/// Gets or sets the verification time of the client phone number.
	/// </summary>
	/// <value>The verification time of the client phone number.</value>
	[JsonProperty("phone_number_verified_time")]
	public string PhoneNumberVerifiedTime { get; set; }

	/// <summary>
	/// Gets or sets the client email address.
	/// </summary>
	/// <value>The client email address.</value>
	[JsonProperty("email_address")]
	public string EmailAddress { get; set; }

	/// <summary>
	/// Gets or sets the verification time of the client email address.
	/// </summary>
	/// <value>The verification time of the client email address.</value>
	[JsonProperty("email_address_verified_time")]
	public string EmailAddressVerifiedTime { get; set; }
    }
}
