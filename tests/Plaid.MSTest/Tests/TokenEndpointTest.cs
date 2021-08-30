using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class TokenEndpointTest
	{
		[TestMethod]
		public async Task Can_create_link_token()
		{
			// Arrange
			var request = new Token.CreateLinkTokenRequest("foo", Guid.NewGuid().ToString(), "US", "en", "transactions");

			var sut = CreateClient();

			// Act
			var response = await sut.CreateLinkTokenAsync(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.LinkToken.ShouldNotBeNullOrEmpty();
			response.Data.ExpirationDate.ShouldNotBe(default);
		}

		[TestMethod]
		public async Task Can_exchange_public_token_for_an_access_token()
		{
			// Arrange
			var sut = CreateClient();

			// Act
			var publicToken = await sut.CreateSandboxPublicTokenAsync(new Sandbox.CreatePublicTokenRequest("ins_129571", "assets", "auth", "balance", "transactions", "identity"));
			if (publicToken.Succeeded == false) Assert.Fail(publicToken.Message);

			var response = await sut.ExchangeTokenForAccessTokenAsync(new Token.ExchangePublicTokenRequest(publicToken.Data.PublicToken));

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.AsscessToken.ShouldNotBeNullOrEmpty();
			response.Data.ItemId.ShouldNotBeNullOrEmpty();
		}

		#region Backing Members

		private static Client CreateClient() => TestData.CreateClient();

		#endregion Backing Members
	}
}
