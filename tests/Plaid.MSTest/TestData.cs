using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Acklann.Plaid
{
	internal static class TestData
	{
		static TestData()
		{
			Configuration = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();
		}

		internal static readonly IConfiguration Configuration;

		public static readonly string Directory = Path.Combine(AppContext.BaseDirectory, "test-data");

		public static string GetFilePath(string pattern)
		{
			return System.IO.Directory.EnumerateFiles(Directory, pattern, SearchOption.AllDirectories).First();
		}

		public static IEnumerable<string> GetFilePaths(string pattern)
		{
			return System.IO.Directory.EnumerateFiles(Directory, pattern, SearchOption.AllDirectories);
		}

		public static string CreateAccessToken()
			=> CreateAccessToken("ins_129571", "assets", "auth", "balance", "transactions", "identity");

		public static string CreateAccessToken(string institutionId, params string[] products)
		{
			var client = CreateClient();
			var response = client.CreateSandboxPublicTokenAsync(new Sandbox.CreatePublicTokenRequest(institutionId, products)).Result;
			if (response.Succeeded == false) Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Fail(response.Message);

			var exchange = client.ExchangeTokenForAccessTokenAsync(new Token.ExchangePublicTokenRequest(response.Data.PublicToken)).Result;
			return exchange.Data.AsscessToken;
		}

		public static Client CreateClient()
		{
			return new Client("https://sandbox.plaid.com", GetClientId(), GetSecret(), "2020-09-14", default);
		}

		public static string GetClientId() => GetValue("plaid:client_id");

		public static string GetSecret() => GetValue("plaid:secret");

		public static string GetValue(string key)
			=> GetValue<string>(key) ?? throw new ArgumentNullException(key);

		public static T GetValue<T>(string key)
		{
			return Configuration.GetValue<T>(key, default);
		}
	}
}
