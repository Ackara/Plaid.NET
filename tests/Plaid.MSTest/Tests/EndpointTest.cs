using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class EndpointTest
	{
		[ClassInitialize]
		public static void Setup(TestContext _)
		{
			var client = CreateClient();
			var publicToken = client.CreateLinkToken(
				new Link.CreateLinkTokenRequest(
					"setup",
					Guid.NewGuid().ToString(),
					"US",
					"en",
					"transactions"),
				true).Result;

			var accessToken = client.ExchangeTokenForAccessTokenAsync(new Item.ExchangePublicTokenRequest
			{
				PublicToken = publicToken.Data.LinkToken
			}).Result;
			AccessToken = accessToken.Data.AsscessToken;
		}

		[TestMethod]
		public async Task Can_create_link_token()
		{
			// Arrange
			var request = new Link.CreateLinkTokenRequest("foo", Guid.NewGuid().ToString(), "US", "en", "transactions");

			var sut = CreateClient();

			// Act
			var response = await sut.CreateLinkToken(request);

			// Assert
			ShouldHaveNoErrors(response);
			response.Data.LinkToken.ShouldNotBeNullOrEmpty();
			response.Data.ExpirationDate.ShouldNotBe(default);
		}

		[TestMethod]
		public async Task Can_Exchange_public_token_for_an_access_token()
		{
			// Arrange
			var sut = CreateClient();

			var request = new Item.ExchangePublicTokenRequest("public-sandbox-5c224a01-8314-4491-a06f-39e193d5cddc");

			// Act
			var response = await sut.ExchangeTokenForAccessTokenAsync(request);

			// Assert
			ShouldHaveNoErrors(response);
		}

		#region Backing Members

		private static string AccessToken;

		private static void ShouldHaveNoErrors<T>(Response<T> response)
		{
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.ShouldNotBe(default(T));
		}

		private static Client CreateClient()
		{
			return new Client("https://sandbox.plaid.com", TestData.GetClientId(), TestData.GetSecret(), default);
		}

		#endregion Backing Members
	}
}
