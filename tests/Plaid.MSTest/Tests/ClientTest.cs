using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class ClientTest
	{
		// ==================== Account ==================== //

		[TestMethod]
		public void Can_get_accounts()
		{
			// Arrange
			var request = new Accounts.GetAccountRequest();
			request.UseDefaults();

			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var result = sut.FetchAccountAsync(request).Result;

			// Assert
			result.ShouldNotBeNull();
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.Item.ShouldNotBeNull();
			result.Accounts.ShouldNotBeEmpty();
		}

		// ==================== Balance ==================== //

		[TestMethod]
		public void Can_get_account_balances()
		{
			// Arrange
			var request = new Balance.GetBalanceRequest().UseDefaults();
			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var result = sut.FetchAccountBalanceAsync(request).Result;

			// Assert
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Accounts.Length.ShouldBeGreaterThanOrEqualTo(1);
			result.Accounts[0].Balance.Current.ShouldBeGreaterThanOrEqualTo(1);
		}

		// ==================== Auth ==================== //

		[TestMethod]
		public void Can_get_bank_account_information()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Auth.GetAccountInfoRequest().UseDefaults();

			// Act
			var result = sut.FetchAccountInfoAsync(request).Result;

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Accounts.Length.ShouldBeGreaterThan(0);
			(result.Numbers.ACH.Length + result.Numbers.EFT.Length + result.Numbers.International.Length + result.Numbers.BACS.Length).ShouldBeGreaterThan(0);
			result.Item.ShouldNotBeNull();
		}

		// ==================== Category ==================== //

		[TestMethod]
		public void Can_retrieve_plaid_category_list()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Category.GetCategoriesRequest().UseDefaults();

			// Act
			var result = sut.FetchCategoriesAsync(request).Result;

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Categories.Length.ShouldBeGreaterThan(0);
		}

		// ==================== Identity ==================== //

		[TestMethod]
		public void Can_get_account_holder_information()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Identity.GetUserIdentityRequest().UseDefaults();

			// Act
			var result = sut.FetchUserIdentityAsync(request).Result;
			bool publicKeyDontHaveAccess = result.Exception?.ErrorCode == Exceptions.ErrorCode.InvalidProduct;
			if (publicKeyDontHaveAccess) Assert.Inconclusive(Helper.your_public_key_do_not_have_access_contact_plaid);

			// Assert
			result.ShouldNotBeNull();
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Item.ShouldNotBeNull();
			result.Accounts.Length.ShouldBeGreaterThan(0);
			result.Accounts[0].Owners.ShouldAllBe(x => x.Names.Length > 0);
			result.Accounts[0].Owners.ShouldAllBe(x => x.Addresses.Length > 0);
			result.Accounts[0].Owners.ShouldAllBe(x => x.PhoneNumbers.Length > 0);
		}

		// ==================== Management ==================== //

		// ==================== Token ==================== //

		[TestMethod]
		public void Can_exchange_link_token_for_access_token()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var request = new Management.ExchangeTokenRequest()
			{
				PublicToken = "public-sandbox-5c224a01-8314-4491-a06f-39e193d5cddc"
			}.UseDefaults();
			var result = sut.ExchangeTokenAsync(request).Result;

			// Assert
			result.Exception.ShouldNotBeNull();
			result.IsSuccessStatusCode.ShouldBeFalse();
		}

		// ==================== Transaction ==================== //

		// ==================== Item ==================== //

		//[TestMethod]
		public void Can_remove_an_item()
		{
			// Arrange
			var request = new Item.RemoveItemRequest();
			request.UseDefaults();

			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var result = sut.DeleteItemAsync(request).Result;

			// Assert
			result.ShouldBeNull();
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
		}

		public void Can_update_webhook()
		{
			throw new System.NotImplementedException();
		}

		// ==================== Income ==================== //

		[TestMethod]
		public void Can_retrieve_the_monthly_earnings_of_an_user()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Income.GetIncomeRequest()
			{
			}.UseDefaults();

			// Act
			var result = sut.FetchUserIncomeAsync(request).Result;
			bool publicKeyDontHaveAccess = result.Exception?.ErrorCode == Exceptions.ErrorCode.InvalidProduct;
			if (publicKeyDontHaveAccess) Assert.Inconclusive(Helper.your_public_key_do_not_have_access_contact_plaid);

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Income.Streams.Length.ShouldBeGreaterThan(0);
			result.Income.LastYearIncome.ShouldBeGreaterThan(0);
		}

		// ==================== Liabilities ==================== //

		[TestMethod]
		public void Can_retrieve_liabilities()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var request = new Liabilities.GetLiabilitiesRequest().UseDefaults();
			var result = sut.FetchLiabilitiesAsync(request).Result;

			// Assert
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Accounts.Length.ShouldBeGreaterThanOrEqualTo(1);
			result.Accounts[0].Balance.Current.ShouldBeGreaterThanOrEqualTo(1);
			result.Liabilities.Credit.ShouldNotBeNull();
			result.Liabilities.Mortgage.ShouldNotBeNull();
			result.Liabilities.Student.ShouldNotBeNull();
		}

		// ==================== Investments ==================== //

		//[TestMethod]
		public void Can_get_investment_holdings()
		{
			// Arrange
			var request = new Investments.GetInvestmentHoldingsRequest();
			request.UseDefaults();

			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var result = sut.FetchInvestmentHoldingsAsync(request).Result;

			// Assert
			result.ShouldNotBeNull();
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.Holdings.ShouldNotBeNull();
		}
	}
}
