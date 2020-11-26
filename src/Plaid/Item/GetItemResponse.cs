using Newtonsoft.Json;
using System;

namespace Acklann.Plaid.Item
{
	/// <summary>
	/// Represents a response from plaid's '/item/get' endpoint. The POST /item/get endpoint returns information about the status of an <see cref="Entity.Item"/>.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.ResponseBase" />
	public class GetItemResponse : ResponseBase
	{
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>The item.</value>
		public Entity.Item Item { get; set; }

		/// <summary>
		///  Gets or sets Last transaction, webhook statuses of the Item
		/// </summary>
		/// <value>The Item status info.</value>
		[JsonProperty("status")]
		public ItemStatus Status { get; set; }

		/// <summary>
		/// Information about the last transaction, webhook statuses of the Item.
		/// </summary>
		public partial class ItemStatus
		{
			/// <summary>
			/// Gets or sets the timestamps of the last successful and failed transactions update for the Item.
			/// </summary>
			[JsonProperty("transactions")]
			public Transactions Transactions { get; set; }

			/// <summary>
			/// Gets or sets the information about the last webhook fired for the Item.
			/// </summary>
			[JsonProperty("last_webhook")]
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
			[JsonProperty("sent_at")]
			public DateTime? SentAt { get; set; }

			/// <summary>
			/// The last webhook code sent
			/// </summary>
			[JsonProperty("code_sent")]
			public string CodeSent { get; set; }
		}

		/// <summary>
		/// Information about the last successful and failed transactions update for the Item.
		/// </summary>
		public partial class Transactions
		{
			/// <summary>
			/// Timestamp of the last successful transactions update for the Item.
			/// </summary>
			[JsonProperty("last_successful_update")]
			public DateTime? LastSuccessfulUpdate { get; set; }

			/// <summary>
			/// Timestamp of the last failed transactions update for the Item.
			/// </summary>
			[JsonProperty("last_failed_update")]
			public DateTime? LastFailedUpdate { get; set; }
		}
	}
}
