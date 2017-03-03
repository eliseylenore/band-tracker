using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void GetAll_BandEmptyAtFirst_true()
        {
            int result = Band.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameBandName_true()
        {
            Band firstBand = new Band("Journey");
            Band secondBand = new Band("Journey");

            Assert.Equal(firstBand, secondBand);
        }

        [Fact]
        public void GetAll_ReturnsAllBands_List()
        {
            Band newBand = new Band ("Journey");
            newBand.Save();

            List<Band> expectedResult = new List<Band>{newBand};
            List<Band> actual = Band.GetAll();

            Assert.Equal(expectedResult, actual);
        }

        public void Dispose()
        {
            Band.DeleteAll();
        }

    }
}
