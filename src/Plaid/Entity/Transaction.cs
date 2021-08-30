using System;
using System.Text.Json.Serialization;

/*
 * todo: transaction_code enum
 * todo: date time members
 */

namespace Acklann.Plaid.Entity
{
	/// <summary>
	/// Represents a banking transaction.
	/// </summary>
	/// <remarks>
	/// Transaction data is standardized across financial institutions, and in many cases transactions are linked to a clean name, entity type, location, and category. Similarly, account data is standardized and returned with a clean name, number, balance, and other meta information where available.
	/// </remarks>
	public class Transaction
	{
		/// <summary>
		/// The merchant name or transaction description.
		/// </summary>
		[JsonPropertyName("name")]
		public string Name { get; set; }

		/// <summary>
		/// The string returned by the financial institution to describe the transaction.
		/// </summary>
		[JsonPropertyName("original_description")]
		public string OriginalDescription { get; set; }

		/// <summary>
		/// Gets or sets the unique id of the transaction.
		/// </summary>
		/// <value>The transaction identifier.</value>
		[JsonPropertyName("transaction_id")]
		public string TransactionId { get; set; }

		/// <summary>
		/// The ID of the account in which this transaction occurred.
		/// </summary>
		[JsonPropertyName("account_id")]
		public string AccountId { get; set; }

		/// <summary>
		/// A hierarchical array of the categories to which this transaction belongs.
		/// </summary>
		[JsonPropertyName("category")]
		public string[] Categories { get; set; }

		/// <summary>
		/// The ID of the category to which this transaction belongs. See https://plaid.com/docs/api/#categories.
		/// </summary>
		[JsonPropertyName("category_id")]
		public string CategoryId { get; set; }

		/// <summary>
		/// Please use the payment_channel field, transaction_type will be deprecated in the future.
		/// </summary>
		/// <value>The type of the transaction.</value>
		[Obsolete]
		[JsonPropertyName("transaction_type")]
		public string TransactionType { get; set; }

		/// <summary>
		/// The channel used to make a payment. Possible values are: online, in store, other. This field will replace the transaction_type field
		/// </summary>
		[JsonPropertyName("payment_channel")]
		public string PaymentChannel { get; set; }

		/// <summary>
		/// The settled value of the transaction, denominated in the account's currency, as stated in iso_currency_code or unofficial_currency_code. Positive values when money moves out of the account; negative values when money moves in.
		/// </summary>
		[JsonPropertyName("amount")]
		public decimal Amount { get; set; }

		/// <summary>
		/// The ISO currency code of the transaction, either USD or CAD. Always null if unofficial_currency_code is non-null.
		/// </summary>
		/// <value>The ISO currency code.</value>
		[JsonPropertyName("iso_currency_code")]
		public string IsoCurrencyCode { get; set; }

		/// <summary>
		/// The unofficial currency code associated with the transaction. Always null if iso_currency_code is non-null.
		/// </summary>
		/// <value>The unofficial currency code.</value>
		[JsonPropertyName("unofficial_currency_code")]
		public string UnofficialCurrencyCode { get; set; }

		/// <summary>
		/// Gets the currency code from either IsoCurrencyCode or UnofficialCurrencyCode. If non-null, IsoCurrencyCode is returned, else if non-null, UnofficialCurrencyCode, else null is returned.
		/// </summary>
		/// <value>Either available currency code.</value>
		public string CurrencyCode => IsoCurrencyCode ?? UnofficialCurrencyCode ?? null;

		/// <summary>
		/// For pending transactions, the date that the transaction occurred; for posted transactions, the date that the transaction posted. Both dates are returned in an ISO 8601 format ( YYYY-MM-DD ).
		/// </summary>
		[JsonPropertyName("date")]
		public DateTime Date { get; set; }

		/// <summary>
		/// Gets or sets the date that the transaction was authorized.
		/// </summary>
		/// <value>The transaction authorized date. Optional</value>
		[JsonPropertyName("authorized_date")]
		public string AuthorizedDate { get; set; }

		/// <summary>
		/// Date and time when a transaction was authorized in ISO 8601 format ( YYYY-MM-DDTHH:mm:ssZ ).
		/// </summary>
		[JsonPropertyName("authorized_datetime")]
		public string AuthorizedDateTime { get; set; }

		/// <summary>
		/// Date and time when a transaction was posted in ISO 8601 format ( YYYY-MM-DDTHH:mm:ssZ ).
		/// </summary>
		[JsonPropertyName("datetime")]
		public string DateTime { get; set; }

		/// <summary>
		/// The check number of the transaction. This field is only populated for check transactions.
		/// </summary>
		[JsonPropertyName("check_number")]
		public string CheckNumber { get; set; }

		/// <summary>
		/// A representation of where a transaction took place.
		/// </summary>
		[JsonPropertyName("location")]
		public LocationInfo Location { get; set; }

		/// <summary>
		/// Transaction information specific to inter-bank transfers. If the transaction was not an inter-bank transfer, all fields will be null.
		/// </summary>
		[JsonPropertyName("payment_meta")]
		public PaymentInfo Payment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Transaction"/> is pending or unsettled. Pending transaction details (name, <see cref="TransactionType"/>, <see cref="Amount"/>) may change before they are settled.
		/// </summary>
		/// <value><c>true</c> if pending; otherwise, <c>false</c>.</value>
		[JsonPropertyName("pending")]
		public bool Pending { get; set; }

		/// <summary>
		/// The ID of a posted transaction's associated pending transaction, where applicable.
		/// </summary>
		[JsonPropertyName("pending_transaction_id")]
		public string PendingTransactionId { get; set; }

		/// <summary>
		/// Gets or sets the name of the account owner. This property is not typically populated and only relevant when dealing with sub-accounts.
		/// </summary>
		/// <value>The account owner.</value>
		[JsonPropertyName("account_owner")]
		public string AccountOwner { get; set; }

		/// <summary>
		/// Gets the transaction type.
		/// </summary>
		/// <value>The transaction type.</value>
		public TransactionType Type
		{
			get
			{
				bool valid = Enum.TryParse(TransactionType, out TransactionType type);
				return valid ? type : Entity.TransactionType.Unresolved;
			}
		}

		/// <summary>
		/// The merchant name, as extracted by Plaid from the name field.
		/// </summary>
		[JsonPropertyName("merchant_name")]
		public string MerchantName { get; set; }

		/// <summary>
		/// An identifier classifying the transaction type.
		/// </summary>
		[JsonPropertyName("transaction_code")]
		public string TransactionCode { get; set; }

		#region Nested Members

		/// <summary>
		/// Represents a geographical location.
		/// </summary>
		public struct LocationInfo
		{
			/// <summary>
			/// The street address where the transaction occurred.
			/// </summary>
			[JsonPropertyName("address")]
			public string Address { get; set; }

			/// <summary>
			/// The city where the transaction occurred.
			/// </summary>
			[JsonPropertyName("city")]
			public string City { get; set; }

			/// <summary>
			/// The region or state where the transaction occurred.
			/// </summary>
			[JsonPropertyName("region")]
			public string State { get; set; }

			/// <summary>
			/// The postal code where the transaction occurred.
			/// </summary>
			[JsonPropertyName("postal_code")]
			public string PostalCode { get; set; }

			/// <summary>
			/// The latitude where the transaction occurred (x-coordinate).
			/// </summary>
			[JsonPropertyName("lat")]
			public double? Latitude { get; set; }

			/// <summary>
			/// The longitude where the transaction occurred (y-coordinate).
			/// </summary>
			[JsonPropertyName("lon")]
			public double? Longitude { get; set; }

			/// <summary>
			/// Gets or sets the alpha-2 country code where the transaction occurred.
			/// </summary>
			/// <value>Country code</value>
			[JsonPropertyName("country")]
			public string Country { get; set; }

			/// <summary>
			/// The merchant defined store number where the transaction occurred.
			/// </summary>
			[JsonPropertyName("store_number")]
			public string StoreNumber { get; set; }
		}

		/// <summary>
		/// Metadata about the customer and merchant.
		/// </summary>
		public struct PaymentInfo
		{
			/// <summary>
			/// Gets or sets the reference number.
			/// </summary>
			/// <value>The reference number.</value>
			[JsonPropertyName("reference_number")]
			public string ReferenceNumber { get; set; }

			/// <summary>
			/// Gets or sets the PPD identifier.
			/// </summary>
			/// <value>The PPD identifier.</value>
			[JsonPropertyName("ppd_id")]
			public string PPDId { get; set; }

			/// <summary>
			/// Gets or sets the name of the payee.
			/// </summary>
			/// <value>The name of the payee.</value>
			[JsonPropertyName("payee")]
			public string PayeeName { get; set; }

			/// <summary>
			/// Gets or sets the name of the payer.
			/// </summary>
			/// <value>The name of the payer.</value>
			[JsonPropertyName("payer")]
			public string Payer { get; set; }

			/// <summary>
			/// Gets or sets the payment processor.
			/// </summary>
			/// <value>The payment processor.</value>
			[JsonPropertyName("payment_processor")]
			public string PaymentProcessor { get; set; }

			/// <summary>
			/// Gets or sets the reason.
			/// </summary>
			/// <value>The reason.</value>
			[JsonPropertyName("reason")]
			public string Reason { get; set; }

			/// <summary>
			/// Gets or sets the by order of.
			/// </summary>
			/// <value>The by order of.</value>
			[JsonPropertyName("by_order_of")]
			public string ByOrderOf { get; set; }
		}

		#endregion Nested Members
	}
}
