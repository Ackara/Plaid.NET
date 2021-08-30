using Acklann.Plaid;
using Acklann.Plaid.Management;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Plaid.Link.Controllers
{
	public class PlaidController : Controller
	{
		public PlaidController(IConfiguration configuration)
		{
			_secrets = configuration.GetValue<string>("plaid:secret");
			_clientId = configuration.GetValue<string>("plaid:client_id");
			_accessTokenPath = Path.Combine(Path.GetTempPath(), "plaid_access_token.tmp");

			if (System.IO.File.Exists(_accessTokenPath)) _accessToken = System.IO.File.ReadAllText(_accessTokenPath);
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<IActionResult> CreateLinkAsync()
		{
			//var client = new PlaidClient(_clientId, _secrets, null, Acklann.Plaid.Environment.Sandbox);
			//var response = await client.CreateLinkToken(new Acklann.Plaid.Management.CreateLinkTokenRequest
			//{
			//	ClientName = "Example",
			//	ClientId = _clientId,
			//	Secret = _secrets,
			//	CountryCodes = new string[] { "US" },
			//	Products = new string[] { "auth", "transactions" },
			//	User = new Acklann.Plaid.Management.CreateLinkTokenRequest.UserInfo
			//	{
			//		ClientUserId = Guid.NewGuid().ToString()
			//	}
			//});

			//return Json(response);
			throw new System.NotImplementedException();
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<IActionResult> GetAccessTokenAsync([FromBody] PlaidLinkResponse bank_info)
		{
			var client = new PlaidClient(_clientId, _secrets, null, Acklann.Plaid.Environment.Sandbox);

			ExchangeTokenResponse response = await client.ExchangeTokenAsync(new ExchangeTokenRequest
			{
				ClientId = _clientId,
				Secret = _secrets,
				PublicToken = bank_info.PublicToken
			});

			UpdateAccessToken(response.AccessToken);

			return Json(response);
		}

		#region Private Members

		private readonly string _clientId, _secrets, _accessTokenPath;
		private string _accessToken;

		private void UpdateAccessToken(string token)
		{
			_accessToken = token;
			System.IO.File.WriteAllText(_accessTokenPath, token);
		}

		#endregion Private Members
	}
}
