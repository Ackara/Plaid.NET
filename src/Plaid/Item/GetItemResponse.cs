using System;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Item
{
	/// <summary>
	/// Response to <see cref="Item.GetItemRequest"/>.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.ResponseBase" />
	public class GetItemResponse : ResponseBase
	{
		/// <summary>
		/// Metadata about the Item.
		/// </summary>
		[JsonPropertyName("item")]
		public Entity.Item Item { get; set; }

		/// <summary>
		///  An object with information about the status of the Item.
		/// </summary>
		[JsonPropertyName("status")]
		public ItemStatus Status { get; set; }

		/// <summary>
		/// Information about the last transaction, webhook statuses of the Item.
		/// </summary>
		public partial class ItemStatus
		{
			/// <summary>
			/// Information about the last successful and failed investments update for the Item.
			/// </summary>
			[JsonPropertyName("investments")]
			public UpdateInfo Investments { get; set; }

			/// <summary>
			/// Information about the last successful and failed transactions update for the Item.
			/// </summary>
			[JsonPropertyName("transactions")]
			public UpdateInfo Transactions { get; set; }

			/// <summary>
			/// Information about the last webhook fired for the Item.
			/// </summary>
			[JsonPropertyName("last_webhook")]
			public LastWebhook LastWebhook { get; set; }
		}

		/// <summary>
		/// Information about the last webhook fired for the Item.
		/// </summary>
		public partial class LastWebhook
		{
			/// <summary>
			/// Timestamp of when the webhook was fired.
			/// </summary>
			[JsonPropertyName("sent_at")]
			public DateTime? SentAt { get; set; }

			/// <summary>
			/// The last webhook code sent
			/// </summary>
			[JsonPropertyName("code_sent")]
			public string CodeSent { get; set; }
		}

		/// <summary>
		/// Information about the last successful and failed transactions update for the Item.
		/// </summary>
		public partial class UpdateInfo
		{
			/// <summary>
			/// Timestamp of the last successful transactions update for the Item.
			/// </summary>
			[JsonPropertyName("last_successful_update")]
			public DateTime? LastSuccessfulUpdate { get; set; }

			/// <summary>
			/// Timestamp of the last failed transactions update for the Item.
			/// </summary>
			[JsonPropertyName("last_failed_update")]
			public DateTime? LastFailedUpdate { get; set; }
		}
	}
}
