using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Acklann.Plaid.Tests
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void Can_fetch_transactions()
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
    }
}
