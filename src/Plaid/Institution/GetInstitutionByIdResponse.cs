using System.Text.Json.Serialization;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Response to <see cref="GetInstitutionByIdRequest"/>.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.PlaidResponseBase" />
	public class GetInstitutionByIdResponse : PlaidResponseBase
	{
		/// <summary>
		/// Details relating to a specific financial institution.
		/// </summary>
		[JsonPropertyName("institution")]
		public Entity.Institution Institution { get; set; }
	}
}
