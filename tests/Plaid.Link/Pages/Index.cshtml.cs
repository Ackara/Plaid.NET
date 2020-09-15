using Acklann.Plaid;
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
		}

		public string LinkToken { get; set; }

		public async Task OnGetAsync()
		{
			var client = new PlaidClient(_clientId, _secrets, null, Acklann.Plaid.Environment.Sandbox);
			var response = await client.CreateLinkToken(new Acklann.Plaid.Management.CreateLinkTokenRequest
			{
				ClientName = "Example",
				ClientId = _clientId,
				Secret = _secrets,
				CountryCodes = new string[] { "US" },
				Products = new string[] { "auth", "transactions" },
				User = new Acklann.Plaid.Management.CreateLinkTokenRequest.UserInfo
				{
					ClientUserId = Guid.NewGuid().ToString()
				}
			});

			LinkToken = response.LinkToken;
		}

		#region Private Members

		private readonly string _clientId, _secrets;

		#endregion Private Members
	}
}
