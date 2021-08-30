using System;
using System.Text.Json.Serialization;
/*
 * todo: date
 */
namespace Acklann.Plaid.Balance
{
	/// <summary>
	/// Request to get  the real-time balance for each of an Item's accounts.
	/// </summary>
	public class GetBalanceRequest : RequestWithAccessToken
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GetBalanceRequest"/> class.
		/// </summary>
		public GetBalanceRequest()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GetBalanceRequest"/> class.
		/// </summary>
		/// <param name="accessToken">The access token.</param>
		/// <exception cref="System.ArgumentNullException">accessToken</exception>
		public GetBalanceRequest(string accessToken)
		{
			AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
		}

		/// <summary>
		/// Gets or sets the options.
		/// </summary>
		/// <value>The options.</value>
		[JsonPropertyName("options")]
		public MoreOptions Options { get; set; }

		public class MoreOptions
		{
			/// <summary>
			/// A list of account_ids to retrieve for the Item. The default value is null.
			/// </summary>
			[JsonPropertyName("account_ids")]
			public string[] AccountIds { get; set; }

			/// <summary>
			/// Timestamp in ISO-8601 format (YYYY-MM-DDTHH:mm:ssZ) indicating the oldest acceptable balance when making a request to /accounts/balance/get.
			/// </summary>
			[JsonPropertyName("min_last_updated_datetime")]
			public string MinLastUpdatedDateTime { get; set; }
		}
	}
}
