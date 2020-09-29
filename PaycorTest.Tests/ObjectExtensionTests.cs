using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PaycorTest.Common.Extensions;

namespace PaycorTest.Tests
{
    [TestClass]
    public class ObjectExtensionTests
    {
        [TestMethod]
        public void TestQueryStringForGetFilteredProductsRequestBasic()
        {
            var expectedResult =
            "SellDateTime=2008-04-30T00%3A00%3A00&PaginationData.CurrentPage=1&PaginationData.RowsPerPage=10";

            PaycorTest.API.Models.Request.GetFilteredProductsRequest request =
            new API.Models.Request.GetFilteredProductsRequest()
            {
                ProductName = null,
                SellDateTime = new DateTime(2008, 4, 30),
                Keywords = new List<string>(),
                PaginationData = new Common.Result.PaginationData()
                {
                    CurrentPage = 1,
                    RowsPerPage = 10
                }
            };

            string queryString = null;
            queryString = request.ToQueryString();

            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void TestQueryStringForGetFilteredProductsRequestWithAllData()
        {
            var expectedResult =
            "ProductName=Chain&SellDateTime=2008-04-30T00%3A00%3A00&Keywords=Superior&Keywords=performance&Keywords=high&PaginationData.CurrentPage=3&PaginationData.RowsPerPage=30";

            PaycorTest.API.Models.Request.GetFilteredProductsRequest request =
            new API.Models.Request.GetFilteredProductsRequest()
            {
                ProductName = "Chain",
                SellDateTime = new DateTime(2008, 4, 30),
                Keywords = new List<string>() { "Superior", "performance", "high" },
                PaginationData = new Common.Result.PaginationData()
                {
                    CurrentPage = 3,
                    RowsPerPage = 30
                }
            };

            string queryString = null;
            queryString = request.ToQueryString();

            Assert.AreEqual(expectedResult, queryString);
        }

        [TestMethod]
        public void TestQueryStringForGetLineTotalAndOrderQtySumPerDay()
        {
            var expectedResult =
            "StartDate=2000-01-01T00%3A00%3A00&EndDate=2020-01-01T00%3A00%3A00&PaginationData.CurrentPage=1&PaginationData.RowsPerPage=10";

            PaycorTest.API.Models.Request.GetAggregatedLineTotalAndOrderQtyInTimePeriodRequest request =
            new API.Models.Request.GetAggregatedLineTotalAndOrderQtyInTimePeriodRequest()
            {
                StartDate = new DateTime(2000,1,1),
                EndDate = new DateTime(2020,1,1),
                PaginationData = new Common.Result.PaginationData()
                {
                    CurrentPage = 1,
                    RowsPerPage = 10
                }
            };

            string queryString = null;
            queryString = request.ToQueryString();

            Assert.AreEqual(expectedResult, queryString);
        }
    }
}
