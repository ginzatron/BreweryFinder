using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApi.DAL;
using SampleApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApi.Tests.DAL
{
    [TestClass]
    public class BrewerySqlDAOTests : BreweryFinderDAOTests
    {
        [DataTestMethod]
        [DataRow("Fat Head's Brewery", 1)]
        [DataRow("notBrewery", 0)]
        public void GetByName_Should_Return_1Brewery(string name, int expectedCount)
        {
            BrewerySqlDAO dao = new BrewerySqlDAO(ConnectionString);

            IList<Brewery> breweries = dao.GetByName(name);

            int actualCount = breweries.Count;

            Assert.AreEqual(expectedCount, actualCount);

        }
    }
}
