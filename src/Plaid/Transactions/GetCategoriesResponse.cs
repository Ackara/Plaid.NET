using System.Text.Json.Serialization;

namespace Acklann.Plaid.Transactions
{
	/// <summary>
	/// Response to <see cref="GetCategoriesRequest"/>.
	/// </summary>
	public class GetCategoriesResponse : PlaidResponseBase
	{
		/// <summary>
		/// An array of all of the transaction categories used by Plaid.
		/// </summary>
		[JsonPropertyName("categories")]
		public Entity.Category[] Categories { get; set; }
	}
}
