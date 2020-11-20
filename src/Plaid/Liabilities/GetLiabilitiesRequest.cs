using Newtonsoft.Json;
using System;

namespace Acklann.Plaid.Liabilities
{
	public class GetLiabilitiesRequest : RequestBase
	{
		[JsonProperty("options")]
		public AdditionalOptions Options { get; set; }

		public struct AdditionalOptions
		{
			/// <summary>
			/// Gets or sets the list of account ids to retrieve for the <see cref="Entity.Item" />. Note: An error will be returned if a provided account_id is not associated with the <see cref="Entity.Item" />.
			/// </summary>
			/// <value>The ac count ids.</value>
			[JsonProperty("account_ids")]
			public string[] AccountIds { get; set; }
		}
	}
}
