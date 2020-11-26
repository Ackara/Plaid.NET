using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using System.Linq;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class PlaidClientTest
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

		// ==================== Institution ==================== //

		[TestMethod]
		public void Can_get_all_institutions()
		{
			// Arrange
			const int limit = 5;
			var request = new Institution.SearchAllRequest() { Take = limit };
			request.CountryCodes = new string[] { "US" };
			request.UseDefaults();

			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var response1 = sut.FetchAllInstitutionsAsync(request).Result;

			request.Take = 1;
			request.Skip = limit;
			var response2 = sut.FetchAllInstitutionsAsync(request).Result;

			var page = response1.Institutions.Select(x => x.Id).ToArray();
			var nextItem = response2.Institutions.First().Id;

			// Assert
			response1.IsSuccessStatusCode.ShouldBeTrue();
			response2.IsSuccessStatusCode.ShouldBeTrue();
			response1.Institutions.Length.ShouldBe(limit);
			response2.Institutions.Length.ShouldBe(1);

			page.ShouldNotContain(nextItem);
		}

		[TestMethod]
		public void Can_get_institutions_by_name()
		{
			// Arrange
			var request = new Institution.SearchRequest().UseDefaults();
			request.Query = "citi";
			request.CountryCodes = new string[] { "US" };

			var sut = new PlaidClient(Environment.Sandbox);

			// Act
			var result = sut.FetchInstitutionsAsync(request).Result;

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Institutions.Length.ShouldBeGreaterThanOrEqualTo(1);
			result.Institutions.ShouldAllBe(i => i.Name.ToLower().Contains(request.Query.ToLower()));
		}

		[TestMethod]
		public void Can_get_institutions_by_id()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Institution.SearchByIdRequest().UseDefaults();
			request.InstitutionId = "ins_109511";
			request.CountryCodes = new string[] { "US" };
			request.Options = new Institution.SearchByIdRequest.AdditionalOptions
			{
				InclueMetadata = true
			};

			// Act
			var response = sut.FetchInstitutionByIdAsync(request).Result;

			// Assert
			response.IsSuccessStatusCode.ShouldBeTrue();
			response.RequestId.ShouldNotBeNullOrEmpty();
			response.Institution.Id.ShouldBe(request.InstitutionId);
			response.Institution.Name.ShouldNotBeNullOrEmpty();
		}

		// ==================== Management ==================== //

		// ==================== Token ==================== //

		[TestMethod]
		public void Can_create_link_token()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Management.CreateLinkTokenRequest()
			{
				ClientName = "Example Client Name",
				Language = "en",
				CountryCodes = new string[] { "US" },
				User = new Management.CreateLinkTokenRequest.UserInfo
				{
					ClientUserId = Guid.NewGuid().ToString()
				},
				Products = new string[] { "auth" }
			}.UseDefaultsWithNoAccessToken();

			// Act
			var result = sut.CreateLinkToken(request).Result;

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.LinkToken.ShouldNotBeNullOrEmpty();
			result.Expiration.ShouldNotBeNullOrEmpty();
		}

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

		[TestMethod]
		public void Can_get_transactions()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Transactions.GetTransactionsRequest().UseDefaults();

			// Act
			var result = sut.FetchTransactionsAsync(request).Result;

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.TransactionsReturned.ShouldBeGreaterThan(0);
			result.Transactions.Length.ShouldBeGreaterThan(0);
			result.Transactions[0].Amount.ShouldBeGreaterThan(0);
		}

		[TestMethod]
		public void Can_refresh_transactions()
		{
			// Arrange
			var request = new Transactions.RefreshTransactionRequest();
			request.UseDefaults();

			// Act
			var sut = new PlaidClient(Environment.Sandbox);
			var result = sut.RefreshTransactionsAsync(request).Result;

			// Assert
			result.ShouldNotBeNull();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.IsSuccessStatusCode.ShouldBeTrue();
		}

		// ==================== Item ==================== //

		[TestMethod]
		public void Can_retrieve_an_item()
		{
			// Arrange
			var sut = new PlaidClient(Environment.Sandbox);
			var request = new Item.GetItemRequest().UseDefaults();

			// Act
			var result = sut.FetchItemAsync(request).Result;

			// Assert
			result.IsSuccessStatusCode.ShouldBeTrue();
			result.RequestId.ShouldNotBeNullOrEmpty();
			result.Item.Id.ShouldNotBeNullOrEmpty();
			result.Item.InstitutionId.ShouldNotBeNullOrEmpty();
			result.Item.BilledProducts.Length.ShouldBeGreaterThan(0);
			result.Item.AvailableProducts.Length.ShouldBeGreaterThan(0);
		}

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
