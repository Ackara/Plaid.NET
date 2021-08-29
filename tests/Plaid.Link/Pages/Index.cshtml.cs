using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Acklann.Plaid.Link.Pages
{
	public class IndexPageModel : PageModel
	{
		public IndexPageModel(IConfiguration configuration)
		{
			_secrets = configuration.GetValue<string>("plaid:secret");
			_clientId = configuration.GetValue<string>("plaid:client_id");
			_access_token = AccessToken = configuration.GetValue<string>("plaid:access_token");
		}

		public string LinkToken { get; set; }

		public string AccessToken { get; set; }

		public async Task OnGetAsync()
		{
			var client = new PlaidClient(_clientId, _secrets, null, Environment.Sandbox);
			var response = await client.CreateLinkToken(new Management.CreateLinkTokenRequest
			{
				ClientName = "Example",
				ClientId = _clientId,
				Secret = _secrets,
				CountryCodes = new string[] { "US" },
				Products = new string[] { "auth", "transactions" },
				User = new Management.CreateLinkTokenRequest.UserInfo
				{
					ClientUserId = Guid.NewGuid().ToString()
				}
			});

			LinkToken = response.LinkToken;
		}

		#region Private Members

		private readonly string _clientId, _secrets, _access_token;

		#endregion Private Members
	}
}
