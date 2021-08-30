using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;

namespace Acklann.Plaid.Tests
{
	[TestClass]
	public class InstitutionEndpointTest
	{
		[TestMethod]
		public async Task Can_get_details_of_all_supported_institutions()
		{
			// Arrange
			var request = new Institution.GetAllInstitutionsRequest(25, 0, "US");
			var sut = CreateClient();

			// Act
			var response = await sut.GetAllInstituionsAsync(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.ShouldNotBeNull();
			response.Data.Total.ShouldBeGreaterThan(1);
			response.Data.Institutions.ShouldAllBe(x => !string.IsNullOrEmpty(x.Id));
			response.Data.Institutions.ShouldAllBe(x => !string.IsNullOrEmpty(x.Name));
			response.Data.Institutions.ShouldAllBe(x => x.Products.Length > 0);
		}

		[TestMethod]
		public async Task Can_get_details_of_an_institution()
		{
			// Arrange
			var request = new Institution.GetInstitutionByIdRequest("ins_130958", "US");
			var sut = CreateClient();

			// Act
			var response = await sut.GetInstitutionByIdAsync(request);

			// Assert
			response.Succeeded.ShouldBeTrue(response.Message);
			response.Data.ShouldNotBeNull();
			response.Data.Institution.Name.ShouldNotBeNullOrEmpty();
		}

		#region Backing Members

		private static Client CreateClient() => TestData.CreateClient();

		#endregion Backing Members
	}
}
