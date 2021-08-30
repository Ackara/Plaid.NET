using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Sandbox
{
	/// <summary>
	/// Request to create a test Item.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase2" />
	public class CreatePublicTokenRequest : RequestBase2
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CreatePublicTokenRequest"/> class.
		/// </summary>
		public CreatePublicTokenRequest()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CreatePublicTokenRequest" /> class.
		/// </summary>
		/// <param name="id">The ID of the institution the Item will be associated with.</param>
		/// <param name="products">The products to initially pull for the Item. May be any products that the specified institution_id  supports. This array may not be empty.</param>
		/// <exception cref="System.ArgumentNullException">
		/// id
		/// or
		/// products
		/// </exception>
		public CreatePublicTokenRequest(string id, params string[] products)
		{
			InstitutionId = id ?? throw new ArgumentNullException(nameof(id));
			InitialProducts = products ?? throw new ArgumentNullException(nameof(products)); ;
		}

		/// <summary>
		/// The ID of the institution the Item will be associated with.
		/// </summary>
		[JsonPropertyName("institution_id")]
		public string InstitutionId { get; set; }

		/// <summary>
		/// The products to initially pull for the Item. May be any products that the specified institution_id  supports. This array may not be empty.
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
		[JsonPropertyName("initial_products")]
		public string[] InitialProducts { get; set; }

		/// <summary>
		/// An optional set of options to be used when configuring the Item. If specified, must not be null.
		/// </summary>
		[JsonPropertyName("options")]
		public MoreOptions Options { get; set; }

		#region Nested

		public class MoreOptions
		{
			/// <summary>
			/// Specify a webhook to associate with the new Item.
			/// </summary>
			[JsonPropertyName("webhook")]
			public string Webhook { get; set; }

			/// <summary>
			/// Test username to use for the creation of the Sandbox Item. Default value is user_good.
			/// </summary>
			[JsonPropertyName("override_username")]
			public string OverrideUsername { get; set; } = "user_good";

			/// <summary>
			/// Test password to use for the creation of the Sandbox Item. Default value is pass_good.
			/// </summary>
			[JsonPropertyName("override_password")]
			public string OverridePassword { get; set; } = "pass_good";

			/// <summary>
			/// SandboxPublicTokenCreateRequestOptionsTransactions is an optional set of parameters corresponding to transactions options.
			/// </summary>
			[JsonPropertyName("transactions")]
			public DateRange Transactions { get; set; }
		}

		public struct DateRange
		{
			/// <summary>
			/// The earliest date for which to fetch transaction history. Dates should be formatted as YYYY-MM-DD.
			/// </summary>
			[JsonPropertyName("start_date")]
			public string Start { get; set; }

			/// <summary>
			/// The most recent date for which to fetch transaction history. Dates should be formatted as YYYY-MM-DD.
			/// </summary>
			[JsonPropertyName("end_date")]
			public string End { get; set; }
		}

		#endregion Nested
	}
}
