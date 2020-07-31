using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Linq;

namespace Acklann.Plaid.Tests
{
    [TestClass]
    public class InstitutionTest
    {
        [TestMethod]
        public void Can_paginate_institutions()
        {
            // Arrange
            const int limit = 5;
            var sut = new PlaidClient(Environment.Sandbox);

            // Act
            var request = new Institution.SearchAllRequest() { Take = limit }.UseDefaults();
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
        public void Can_fetch_institutions_by_name()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);

            // Act
            var request = new Institution.SearchRequest() { Query = "citi" }.UseDefaults();

            var result = sut.FetchInstitutionsAsync(request).Result;

            // Assert
            result.IsSuccessStatusCode.ShouldBeTrue();
            result.RequestId.ShouldNotBeNullOrEmpty();
            result.Institutions.Length.ShouldBeGreaterThanOrEqualTo(1);
            result.Institutions.ShouldAllBe(i => i.Name.ToLower().Contains(request.Query.ToLower()));
        }

        [TestMethod]
        public void Can_fetch_institutions_by_id()
        {
            // Arrange
            var sut = new PlaidClient(Environment.Sandbox);
            var request = new Institution.SearchByIdRequest().UseDefaults();
            request.InstitutionId = "ins_109511";
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
    }
}
