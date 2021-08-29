﻿using Newtonsoft.Json;

namespace Acklann.Plaid.Management
{
	/// <summary>
	/// Represents a request for plaid's '/item/access_token/update_version' endpoint. If you have an access_token from the legacy version of Plaid’s API, you can use the '/item/access_token/update_version' endpoint to generate an access_token for the Item that works with the current API.
	/// </summary>
	/// <seealso cref="Acklann.Plaid.RequestBase" />
	/// <remarks>
	/// Calling this endpoint does not revoke the legacy API access_token. You can still use the legacy access_token in the legacy API environment to retrieve data. You can also begin using the new access_token with our current API immediately.
	/// </remarks>

	public class UpdateAccessTokenVersionRequest : RequestBase
	{
		/// <summary>
		/// Gets or sets the access token v1.
		/// </summary>
		/// <value>The access token v1.</value>
		[JsonProperty("access_token_v1")]
		public string AccessTokenV1 { get; set; }
	}
}
