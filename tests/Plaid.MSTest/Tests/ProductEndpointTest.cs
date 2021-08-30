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
			if (response.Data.TotalTransactions > 0)
			{
				response.Data.Transactions.ShouldNotBeEmpty();
				response.Data.Transactions.ShouldAllBe(x => !string.IsNullOrEmpty(x.Name));
				response.Data.Transactions.ShouldAllBe(x => !string.IsNullOrEmpty(x.CategoryId));
			}

			response.Data.Accounts.ShouldNotBeEmpty();
			response.Data.Accounts.ShouldAllBe(x => !string.IsNullOrEmpty(x.Name));
		}

		[TestMethod]
		public async Task Can_refresh_transaction_data()
		{
			// Arrange
			var sut = CreateClient();
			var request = new Transactions.RefreshTransactionRequest(AccessToken);

			// Act
			var response = await sut.RefreshTransactionData(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.RequestId.ShouldNotBeNullOrEmpty();
		}

		[TestMethod]
		public async Task Get_get_categories()
		{
			// Arrange
			var sut = CreateClient();

			// Act
			var response = await sut.GetCategoriesAsync();

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.Categories.ShouldNotBeEmpty();
			response.Data.Categories.ShouldAllBe(x => !string.IsNullOrEmpty(x.Id));
			response.Data.Categories.ShouldAllBe(x => !string.IsNullOrEmpty(x.Group));
			response.Data.Categories.ShouldAllBe(x => x.Hierarchy != null);
		}

		[TestMethod]
		public async Task Can_retrieve_real_time_balance_data()
		{
			// Arrange
			var sut = CreateClient();
			var request = new Balance.GetBalanceRequest(AccessToken);

			// Act
			var response = await sut.GetAccountsBalanceAsync(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.Accounts.ShouldNotBeEmpty();
			response.Data.Item.ShouldNotBeNull();
		}

		#region Backing Members

		private static string AccessToken;

		private static Client CreateClient() => TestData.CreateClient();

		#endregion Backing Members
	}
}
