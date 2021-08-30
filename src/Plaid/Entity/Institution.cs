using Acklann.Plaid.Institution;
using System;
using System.Text.Json.Serialization;

// todo:  add maximum_payment_amount
namespace Acklann.Plaid.Entity
{
	/// <summary>
	/// Represents a banking institution.
	/// </summary>
	public class Institution
	{
		/// <summary>
		/// Unique identifier for the institution.
		/// </summary>
		[JsonPropertyName("institution_id")]
		public string Id { get; set; }

		/// <summary>
		///The official name of the institution.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// A list of the Plaid products supported by the institution.
		/// Possible Values:
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
		/// <remarks>
		/// Note that only institutions that support Instant Auth will return auth in the product array; institutions that do not list auth may still support other Auth methods such as Instant Match or Automated Micro-deposit Verification. For more details, see https://plaid.com/docs/auth/coverage/.
		/// </remarks>
		[JsonPropertyName("products")]
		public string[] Products { get; set; }

		/// <summary>
		/// A list of the country codes supported by the institution.
		/// Possible Values:
		/// <list type="bullet">
		/// <item>US</item>
		/// <item>GB</item>
		/// <item>ES</item>
		/// <item>NL</item>
		/// <item>FR</item>
		/// <item>IE</item>
		/// <item>CA</item>
		/// </list>
		/// </summary>
		[JsonPropertyName("country_codes")]
		public string[] Countries { get; set; }

		/// <summary>
		/// Gets or sets the credentials.
		/// </summary>
		/// <value>The credentials.</value>
		[Obsolete]
		[JsonPropertyName("credentials")]
		public Credential[] Credentials { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has Multi-Factor Authentication.
		/// </summary>
		[Obsolete]
		[JsonPropertyName("has_mfa")]
		public bool HasMfa { get; set; }

		/// <summary>
		/// Gets or sets the Multi-Factor Authentication selections.
		/// </summary>
		[Obsolete]
		[JsonPropertyName("mfa")]
		public string[] MfaSelections { get; set; }

		/// <summary>
		/// Gets or sets the type of the mfa.
		/// </summary>
		[Obsolete]
		[JsonPropertyName("mfa_code_type")]
		public string MfaType { get; set; }

		/// <summary>
		/// The URL for the institution's website.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// Hexadecimal representation of the primary color used by the institution.
		/// </summary>
		/// <value>
		[JsonPropertyName("primary_color")]
		public string PrimaryColor { get; set; }

		/// <summary>
		/// Base64 encoded representation of the institution's logo.
		/// </summary>
		[JsonPropertyName("logo")]
		public string Logo { get; set; }

		/// <summary>
		/// A partial list of routing numbers associated with the institution. This list is provided for the purpose of looking up institutions by routing number. It is not comprehensive and should never be used as a complete list of routing numbers for an institution.
		/// </summary>
		[JsonPropertyName("routing_numbers")]
		public string[] RoutingNumbers { get; set; }

		/// <summary>
		/// Indicates that the institution has an OAuth login flow. This is primarily relevant to institutions with European country codes.
		/// </summary>
		public bool OAuth { get; set; }

		/// <summary>
		/// The status of an institution is determined by the health of its Item logins, Transactions updates, Investments updates, Liabilities updates, Auth requests, Balance requests, Identity requests, Investments requests, and Liabilities requests. A login attempt is conducted during the initial Item add in Link. If there is not enough traffic to accurately calculate an institution's status, Plaid will return null rather than potentially inaccurate data.
		/// </summary>
		[JsonPropertyName("status")]
		public StatusSchema Status { get; set; }

		/// <summary>
		/// Metadata that captures what specific payment configurations an institution supports when making Payment Initiation requests.
		/// </summary>
		[JsonPropertyName("payment_initiation_metadata")]
		public PaymentMetadata PaymentInititionMetadata { get; set; }

		/// <summary>
		/// The total number of institutions available via this endpoint.
		/// </summary>
		[JsonPropertyName("total")]
		public int Total { get; set; }

		#region Nested Objects

		public class StatusSchema
		{
			/// <summary>
			/// Gets or sets status information regarding Item adds via Link.
			/// </summary>
			[JsonPropertyName("item_logins")]
			public InstitutionStatus ItemLogin { get; set; }

			/// <summary>
			/// Gets or sets the status information regarding transactions updates.
			/// </summary>
			[JsonPropertyName("transactions_updates")]
			public InstitutionStatus Transactions { get; set; }

			/// <summary>
			/// Gets or sets the status information regarding Auth requests..
			/// </summary>
			/// <value>
			/// The authentication.
			/// </value>
			[JsonPropertyName("auth")]
			public InstitutionStatus Auth { get; set; }

			/// <summary>
			/// Gets or sets the status information regarding Balance requests.
			/// </summary>
			/// <value>
			/// The balance.
			/// </value>
			[JsonPropertyName("balacne")]
			public InstitutionStatus Balance { get; set; }

			/// <summary>
			/// Gets or sets the status information regarding Identity requests.
			/// </summary>
			[JsonPropertyName("identity")]
			public InstitutionStatus Identity { get; set; }

			/// <summary>
			/// A representation of the status health of a request type. Auth requests, Balance requests, Identity requests, Investments requests, Liabilities requests, Transactions updates, Investments updates, Liabilities updates, and Item logins each have their own status object.
			/// </summary>
			[JsonPropertyName("investments_updates")]
			public InstitutionStatus InvestmentsUpdates { get; set; }

			/// <summary>
			/// A representation of the status health of a request type. Auth requests, Balance requests, Identity requests, Investments requests, Liabilities requests, Transactions updates, Investments updates, Liabilities updates, and Item logins each have their own status object.
			/// </summary>
			[JsonPropertyName("liabilities_updates")]
			public InstitutionStatus LiabilitiesUpdates { get; set; }

			/// <summary>
			/// A representation of the status health of a request type. Auth requests, Balance requests, Identity requests, Investments requests, Liabilities requests, Transactions updates, Investments updates, Liabilities updates, and Item logins each have their own status object.
			/// </summary>
			[JsonPropertyName("liabilities")]
			public InstitutionStatus Liabilities { get; set; }

			/// <summary>
			/// A representation of the status health of a request type. Auth requests, Balance requests, Identity requests, Investments requests, Liabilities requests, Transactions updates, Investments updates, Liabilities updates, and Item logins each have their own status object.
			/// </summary>
			[JsonPropertyName("investments")]
			public InstitutionStatus Investments { get; set; }

			/// <summary>
			/// Details of recent health incidents associated with the institution.
			/// </summary>
			[JsonPropertyName("health_incidents")]
			public object HealthIncidents { get; set; }
		}

		public class PaymentMetadata
		{
			/// <summary>
			/// Indicates whether the institution supports payments from a different country.
			/// </summary>
			[JsonPropertyName("supports_international_payments")]
			public bool SupportsInternationalPayments { get; set; }

			/// <summary>
			/// Indicates whether the institution supports returning refund details when initiating a payment.
			/// </summary>
			[JsonPropertyName("supports_refund_details")]
			public bool SupportsRefundDetails { get; set; }

			/// <summary>
			/// Metadata specifically related to valid Payment Initiation standing order configurations for the institution.
			/// </summary>
			[JsonPropertyName("standing_order_metadata")]
			public object StandingOrderMetadata { get; set; }
		}

		public struct OrderMetadata
		{
			/// <summary>
			/// Indicates whether the institution supports closed-ended standing orders by providing an end date.
			/// </summary>
			[JsonPropertyName("supports_standing_order_end_date")]
			public bool SupportsStandingOrderEndDate { get; set; }

			/// <summary>
			/// This is only applicable to MONTHLY standing orders. Indicates whether the institution supports negative integers (-1 to -5) for setting up a MONTHLY standing order relative to the end of the month.
			/// Possible Values:
			/// <list type="bullet">
			/// <item>WEEKLY</item>
			/// <item>MONTHLY</item>
			/// </list>
			/// </summary>
			[JsonPropertyName("supports_standing_order_negative_execution_days")]
			public bool SupportsStandingOrderNegativeExecutionDays { get; set; }

			/// <summary>
			/// A list of the valid standing order intervals supported by the institution.
			/// </summary>
			[JsonPropertyName("valid_standing_order_intervals")]
			public string[] ValidStandingOrderIntervals { get; set; }
		}

		public struct Credential
		{
			/// <summary>
			/// Gets or sets the label.
			/// </summary>
			/// <value>The label.</value>
			[JsonPropertyName("label")]
			public string Label { get; set; }

			/// <summary>
			/// Gets or sets the name.
			/// </summary>
			/// <value>The name.</value>
			[JsonPropertyName("name")]
			public string Name { get; set; }

			/// <summary>
			/// Gets or sets the type of the data.
			/// </summary>
			/// <value>The type of the data.</value>
			[JsonPropertyName("type")]
			public string DataType { get; set; }
		}

		#endregion Nested Objects
	}
}
