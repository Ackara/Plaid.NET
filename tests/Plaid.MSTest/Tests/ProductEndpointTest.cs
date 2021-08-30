using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class ProductEndpointTest
	{
		[ClassInitialize]
		public static void Setup(TestContext _)
		{
			AccessToken = TestData.CreateAccessToken();
			if (string.IsNullOrEmpty(AccessToken)) Assert.Fail("Could not create plaid access_token.");
		}

		[TestMethod]
		public async Task Can_get_transaction_data()
		{
			// Arrange
			var sut = CreateClient();
			var start = System.DateTime.Now.AddDays(-60);
			var end = System.DateTime.Now.AddDays(-30);

			// Act
			var request = new Transactions.GetTransactionsRequest(AccessToken, start, end);
			var response = await sut.GetTransactionsAsync(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.Transactions.ShouldNotBeEmpty();
			response.Data.Transactions.ShouldAllBe(x => !string.IsNullOrEmpty(x.Name));
		}

		[TestMethod]
		public async Task Can_refresh_transaction_data()
		{
			throw new System.NotImplementedException();
		}

		[TestMethod]
		public async Task Get_get_categories()
		{
			throw new System.NotImplementedException();
		}

		#region Backing Members

		private static string AccessToken;

		private static Client CreateClient() => TestData.CreateClient();

		#endregion Backing Members
	}
}
