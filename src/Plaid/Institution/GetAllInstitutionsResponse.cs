using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Acklann.Plaid.Institution
{
	/// <summary>
	/// Response for <see cref="GetAllInstitutionsRequest"/>
	/// </summary>
	public class GetAllInstitutionsResponse : PlaidResponseBase
	{
		/// <summary>
		/// A list of Plaid Institution
		/// </summary>
		[JsonPropertyName("institutions")]
		public IEnumerable<Entity.Institution> Institutions { get; set; }

		/// <summary>
		/// The total number of institutions available via this endpoint.
		/// </summary>
		[JsonPropertyName("total")]
		public int Total { get; set; }
	}
}
