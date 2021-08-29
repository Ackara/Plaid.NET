using Newtonsoft.Json;
using System.Collections.Generic;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/link/token/create' endpoint. Create a link_token.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class CreateLinkTokenRequest : RequestBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateLinkTokenRequest"/> class.
		/// </summary>
		public CreateLinkTokenRequest()
		{
			Language = "en";
		}

		/// <summary>
		/// The name of your application, as it should be displayed in Link. Maximum length of 30 characters.
		/// </summary>
		/// <value>The client name.</value>
		[JsonProperty("client_name")]
		public string ClientName { get; set; }

		/// <summary>
		/// The language that Link should be displayed in.
		/// </summary>
		/// <value>The language.</value>
		[JsonProperty("language")]
		public string Language { get; set; }

		/// <summary>
		/// Gets or sets the country codes.
		/// </summary>
		/// <value>The country codes.</value>
		[JsonProperty("country_codes")]
		public string[] CountryCodes { get; set; }

		/// <summary>
		/// An object specifying information about the end user who will be linking their account.
		/// </summary>
		/// <value>The user.</value>
		[JsonProperty("user")]
		public UserInfo User { get; set; }

		/// <summary>
		/// List of Plaid product(s) you wish to use.
		/// </summary>
		/// <value>The products.</value>
		[JsonProperty("products")]
		public string[] Products { get; set; }

		/// <summary>
		/// The destination URL to which any webhooks should be sent.
		/// </summary>
		/// <value>The webhook.</value>
		[JsonProperty("webhook")]
		public string Webhook { get; set; }

		/// <summary>
		/// The name of the Link customization from the Plaid Dashboard to be applied to Link.
		/// If not specified, the default customization will be used.
		/// </summary>
		/// <value>The link customization name.</value>
		[JsonProperty("link_customization_name")]
		public string LinkCustomizationName { get; set; }

		/// <summary>
		/// By default, Link will provide limited account filtering: it will only display Institutions that are compatible with all products supplied in the products parameter of /link/token/create, and, if auth is specified in the products array, will also filter out accounts other than checking and savings accounts on the Account Select pane.
		/// You can further limit the accounts shown in Link by using account_filters to specify the account subtypes to be shown in Link.
		/// </summary>
		[JsonProperty("account_filters")]
		public Dictionary<string, List<string>> AccountFilters { get; set; }

		/// <summary>
		/// The access_token associated with the Item to update, used when updating or modifying an existing access_token.
		/// </summary>
		/// <value>The access token.</value>
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		/// <summary>
		/// A URI indicating the destination where a user should be forwarded after completing the Link flow; used to support OAuth authentication flows when launching Link in the browser or via a webview.
		/// </summary>
		/// <value>The redirect uri.</value>
		[JsonProperty("redirect_uri")]
		public string RedirectUri { get; set; }

		/// <summary>
		/// Gets or sets the payment initiation.
		/// </summary>
		/// <value>The payment initiation.</value>
		/// <remarks>Payment initiation still needs to be typed and fully implemented.</remarks>
		[JsonProperty("payment_initiation")]
		public object PaymentInitiation { get; set; }

		/// <summary>
		/// The name of your app's Android package. Required if using the link_token to initialize Link on Android.
		/// </summary>
		[JsonProperty("android_package_name")]
		public string AndriodPackageName { get; set; }

		/// <summary>
		/// Represents an <see cref="Entity.User"/> metadata.
		/// </summary>
		public struct UserInfo
		{
			/// <summary>
			/// Gets or sets the <see cref="Entity.User"/> client user id.
			/// </summary>
			/// <value>The client user id.</value>
			[JsonProperty("client_user_id")]
			public string ClientUserId { get; set; }
		}
	}
}
