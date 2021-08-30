using System.Text.Json.Serialization;

//todo: create enum for update_type
//todo: add unique error object

namespace Acklann.Plaid.Entity
{
	/// <summary>
	/// Represents a Plaid item. A set of credentials at a financial institution.
	/// </summary>
	public class Item
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		[JsonPropertyName("item_id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Entity.Institution"/> identifier.
		/// </summary>
		[JsonPropertyName("institution_id")]
		public string InstitutionId { get; set; }

		/// <summary>
		/// Gets or sets the webhook.
		/// </summary>
		[JsonPropertyName("webhook")]
		public string Webhook { get; set; }

		/// <summary>
		/// A list of products available for the Item that have not yet been accessed.
		/// </summary>
		[JsonPropertyName("available_products")]
		public string[] AvailableProducts { get; set; }

		/// <summary>
		/// A list of products that have been billed for the Item. Note - billed_products is populated in all environments but only requests in Production are billed.
		/// </summary>
		/// <value>The billed products.</value>
		[JsonPropertyName("billed_products")]
		public string[] BilledProducts { get; set; }

		/// <summary>
		/// The RFC 3339 timestamp after which the consent provided by the end user will expire. Upon consent expiration, the item will enter the ITEM_LOGIN_REQUIRED error state.
		/// To circumvent the ITEM_LOGIN_REQUIRED error and maintain continuous consent, the end user can reauthenticate via Link’s update mode in advance of the consent expiration time.
		/// </summary>
		[JsonPropertyName("consent_expiration_time")]
		public string ConsentExpirationTime { get; set; }

		/// <summary>
		/// Indicates whether an Item requires user interaction to be updated, which can be the case for Items with some forms of two-factor authentication.
		/// </summary>
		[JsonPropertyName("update_type")]
		public string UpdateType { get; set; }

		[JsonPropertyName("error")]
		public Exceptions.PliadError Error { get; set; }
	}
}
