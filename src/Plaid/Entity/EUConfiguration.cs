using System.Text.Json.Serialization;

namespace Acklann.Plaid.Entity
{
	/// <summary>
	/// Provide configuration parameters for EU flows.
	/// </summary>
	public class EUConfiguration
	{
		/// <summary>
		/// If true, open Link without an initial UI. Defaults to false.
		/// </summary>
		/// <value>
		///   <c>true</c> if headless; otherwise, <c>false</c>.
		/// </value>
		[JsonPropertyName("headless")]
		public bool Headless { get; set; }
	}
}
