using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System;
using static Acklann.Plaid.Management.CreateLinkTokenRequest;

namespace Acklann.Plaid.Tests
{
    [TestClass]
    public class PlaidClientTest
    {
        [TestMethod]
        public void GetItemAsync_should_retrieve_the_item_associated_with_the_specified_access_token()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Management.GetItemRequest()
            {
            }.UseDefaults();

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

        [TestMethod]
        public void ExchangePublicTokenAsync_should_retrieve_a_response_from_the_api_server()
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

        [TestMethod]
        public void FetchCategoriesAsync_should_retrieve_the_api_category_list()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Category.GetCategoriesRequest()
            {
            }.UseDefaults();

            // Act
            var result = sut.FetchCategoriesAsync(request).Result;

            // Assert
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Categories.Length.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void FetchAccountBalanceAsync_should_retrieve_the_account_balances_of_an_user()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);

            // Act
            var request = new Balance.GetBalanceRequest().UseDefaults();
            var result = sut.FetchAccountBalanceAsync(request).Result;

            // Assert
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThanOrEqualTo(1);
            result.Accounts[0].Balance.Current.ShouldBeGreaterThanOrEqualTo(1);
        }

        [TestMethod]
        public void FetchAccountInfoAsync_should_retrieve_the_routing_numbers_of_an_user_accounts()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Auth.GetAccountInfoRequest()
            {
            }.UseDefaults();

            // Act
            var result = sut.FetchAccountInfoAsync(request).Result;

            // Assert
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThan(0);
            (result.Numbers.ACH.Length + result.Numbers.EFT.Length + result.Numbers.International.Length + result.Numbers.BACS.Length).ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }

        [TestMethod]
        public void FetchAccountInfoAsync_International_Bacs_should_retrieve_the_routing_numbers_of_an_user_accounts()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Auth.GetAccountInfoRequest()
            {
            }.UseIntlDefaults();

            // Act
            var result = sut.FetchAccountInfoAsync(request).Result;

            // Assert
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThan(0);
            result.Numbers.International.Length.ShouldBeGreaterThan(0);
            result.Numbers.BACS.Length.ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }

        [TestMethod]
        public void FetchUserIdentityAsync_should_retrieve_the_personal_info_of_an_user()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Identity.GetUserIdentityRequest()
            {
            }.UseDefaults();

            // Act
            var result = sut.FetchUserIdentityAsync(request).Result;
            bool publicKeyDontHaveAccess = result.Exception?.ErrorCode == "INVALID_PRODUCT";
            if (publicKeyDontHaveAccess) Assert.Inconclusive(Helper.your_public_key_do_not_have_access_contact_plaid);

            // Assert
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Accounts.Length.ShouldBeGreaterThan(0);
            result.Item.ShouldNotBeNull();
        }

        [TestMethod]
        public void FetchIncomeAsync_should_retrieve_the_monthly_earnings_of_an_user()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Income.GetIncomeRequest()
            {
            }.UseDefaults();

            // Act
            var result = sut.FetchUserIncomeAsync(request).Result;
            bool publicKeyDontHaveAccess = result.Exception?.ErrorCode == "INVALID_PRODUCT";
            if (publicKeyDontHaveAccess) Assert.Inconclusive(Helper.your_public_key_do_not_have_access_contact_plaid);

            // Assert
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Income.Streams.Length.ShouldBeGreaterThan(0);
            result.Income.LastYearIncome.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void CreateLinkToken_should_retrieve_link_token_and_expiration()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Management.CreateLinkTokenRequest()
            {
                ClientName = "Example Client Name",
                Language = "en",
                CountryCodes = new string[] { "US" },
                User = new UserInfo
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
    }
}