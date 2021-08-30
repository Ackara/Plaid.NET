using Newtonsoft.Json;
using System;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Represent the status of an institution is determined by the health of its Item logins, Transactions updates, Auth requests, Balance requests, and Identity requests.
	/// </summary>
	public class InstitutionStatus
	{
		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the last status change for the institution.
		/// </summary>
		[JsonProperty("last_status_change")]
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Gets or sets a detailed breakdown of the institution's performance.
		/// </summary>
		[JsonProperty("breakdown")]
		public Performance Breakdown { get; set; }

		public struct Performance
		{
			/// <summary>
			/// The percentage of login attempts that are successful, expressed as a decimal.
			/// </summary>
			[JsonProperty("success")]
			public float SuccessRate { get; set; }

			/// <summary>
			/// The percentage of logins that are failing due to an internal Plaid issue, expressed as a decimal.
			/// </summary>
			[JsonProperty("error_plaid")]
			public float PlaidErrorRate { get; set; }

			/// <summary>
			/// The percentage of logins that are failing due to an issue in the institution's system, expressed as a decimal.
			/// </summary>
			/// <value>
			/// The institution error rate.
			/// </value>
			[JsonProperty("error_institution")]
			public float InstitutionErrorRate { get; set; }

			/// <summary>
			/// The refresh_interval may be DELAYED or STOPPED even when the success rate is high. This value is only returned for Transactions status breakdowns.
			/// </summary>
			[JsonProperty("refresh_interval")]
			public string RefreshInterval { get; set; }
		}
	}
}
