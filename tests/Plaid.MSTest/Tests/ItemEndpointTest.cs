using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class ItemEndpointTest
	{
		[TestMethod]
		public async Task Can_retrieve_an_item()
		{
			// Arrange
			var sut = TestData.CreateClient();

			var token = TestData.CreateAccessToken();
			var request = new Item.GetItemRequest(token);

			// Act
			var response = await sut.GetItemAsync(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.Item.ShouldNotBeNull();
			response.Data.Item.AvailableProducts.ShouldNotBeEmpty();
		}
	}
}
