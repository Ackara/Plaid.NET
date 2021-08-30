using System;
using System.Text.Json.Serialization;

/// todo: add account filter
namespace Acklann.Plaid.Token
{
	/// <summary>
	/// Represents a request for plaid's '/link/token/create' endpoint. Create a link_token.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.SerializableContent" />
	public class CreateLinkTokenRequest : RequestWithAccessToken
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateLinkTokenRequest"/> class.
		/// </summary>
		public CreateLinkTokenRequest()
		{
			Language = "en";
		}

		public CreateLinkTokenRequest(string clientName, string userid, string countryCode, string language, params string[] products)
		{
			ClientName = clientName ?? throw new ArgumentNullException(nameof(clientName));
			Language = language ?? throw new ArgumentNullException(nameof(language));
			User = new Entity.UserInfo { ClientUserId = userid };
			CountryCodes = new string[] { countryCode };
			Products = products;
		}

		/// <summary>
		/// The name of your application, as it should be displayed in Link. Maximum length of 30 characters.
		/// </summary>
		/// <value>The client name.</value>
		[JsonPropertyName("client_name")]
		public string ClientName { get; set; }

		/// <summary>
		/// The language that Link should be displayed in.
		/// </summary>
		/// <value>The language.</value>
		[JsonPropertyName("language")]
		public string Language { get; set; }

		/// <summary>
		/// Specify an array of Plaid-supported country codes this institution supports, using the ISO-3166-1 alpha-2 country code standard.
		/// Possible Values:
		/// <list type="bullet">
		/// <item>US</item>
		/// <item>GB</item>
		/// <item>ES</item>
		/// <item>NL</item>
		/// <item>IE</item>
		/// <item>CA</item>
		/// </list>
		/// </summary>
		[JsonPropertyName("country_codes")]
		public string[] CountryCodes { get; set; }

		/// <summary>
		/// An object specifying information about the end user who will be linking their account.
		/// </summary>
		[JsonPropertyName("user")]
		public Entity.UserInfo User { get; set; }

		/// <summary>
		/// List of Plaid product(s) you wish to use.
		/// Possible values:
		/// <list type="bullet">
		/// <item>assets</item>
		/// <item>auth</item>
		/// <item>balance</item>
		/// <item>identity</item>
		/// <item>investments</item>
		/// <item>liabilities</item>
		/// <item>payment_initiation</item>
		/// <item>transactions</item>
		/// <item>credit_details</item>
		/// <item>income</item>
		/// <item>income_verification</item>
		/// <item>deposit_switch</item>
		/// <item>standing_orders</item>
		/// </list>
		/// </summary>
		[JsonPropertyName("products")]
		public string[] Products { get; set; }

		/// <summary>
		/// The destination URL to which any webhooks should be sent.
		/// </summary>
		/// <value>The webhook.</value>
		[JsonPropertyName("webhook")]
		public string Webhook { get; set; }

		/// <summary>
		/// The name of the Link customization from the Plaid Dashboard to be applied to Link.
		/// If not specified, the default customization will be used.
		/// </summary>
		/// <value>The link customization name.</value>
		[JsonPropertyName("link_customization_name")]
		public string LinkCustomizationName { get; set; }

		/// <summary>
		/// A URI indicating the destination where a user should be forwarded after completing the Link flow; used to support OAuth authentication flows when launching Link in the browser or via a webview.
		/// </summary>
		/// <value>The redirect uri.</value>
		[JsonPropertyName("redirect_uri")]
		public string RedirectUri { get; set; }

		/// <summary>
		/// Gets or sets the payment initiation.
		/// </summary>
		/// <value>The payment initiation.</value>
		/// <remarks>Payment initiation still needs to be typed and fully implemented.</remarks>
		[JsonPropertyName("payment_initiation")]
		public object PaymentInitiation { get; set; }

		/// <summary>
		/// The name of your app's Android package. Required if using the link_token to initialize Link on Android.
		/// </summary>
		[JsonPropertyName("android_package_name")]
		public string AndriodPackageName { get; set; }

		/// <summary>
		/// By default, Link will provide limited account filtering: it will only display Institutions that are compatible with all products supplied in the products parameter of /link/token/create, and, if auth is specified in the products array, will also filter out accounts other than checking and savings accounts on the Account Select pane.
		/// You can further limit the accounts shown in Link by using account_filters to specify the account subtypes to be shown in Link.
		/// </summary>
		//[JsonPropertyName("account_filters")]
		//public Dictionary<string, List<string>> AccountFilters { get; set; }

		public struct Filter
		{
			public Entity.EUConfiguration EuConfig { get; set; }

			/// <summary>
			/// Used for certain Europe-only configurations, as well as certain legacy use cases in other regions.
			/// </summary>
			[JsonPropertyName("institution_id")]
			public string InstiutionId { get; set; }

			/// <summary>
			/// Gets or sets the payment initiation.
			/// </summary>
			[JsonPropertyName("payment_initiation")]
			public string PaymentInitiation { get; set; }
		}
	}
}
