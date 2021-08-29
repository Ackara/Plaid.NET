using Newtonsoft.Json;

namespace Acklann.Plaid.Entity
{
	public class Owner
	{
		/// <summary>
		/// Gets or sets the list of names associated with the Item's account(s).
		/// </summary>
		[JsonProperty("names")]
		public string[] Names { get; set; }

		/// <summary>
		/// Gets or sets the list of phone numbers associated with the Item's account(s).
		/// </summary>
		[JsonProperty("phone_numbers")]
		public Phonenumber[] PhoneNumbers { get; set; }

		/// <summary>
		/// Gets or sets the list of emails associated with the Item's account(s).
		/// </summary>
		[JsonProperty("emails")]
		public Email[] Emails { get; set; }

		/// <summary>
		/// Gets or sets the list of addresses associated with the Item's account(s).
		/// </summary>
		[JsonProperty("addresses")]
		public Address[] Addresses { get; set; }
	}
}
