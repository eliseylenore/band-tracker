using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void GetAll_VenueEmptyAtFirst_true()
        {
            int result = Venue.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameVenueName_true()
        {
            Venue firstVenue = new Venue("The Showbox");
            Venue secondVenue = new Venue("The Showbox");

            Assert.Equal(firstVenue, secondVenue);
        }

        [Fact]
        public void GetAll_ReturnsAllVenues_List()
        {
            Venue newVenue = new Venue ("The Showbox");
            newVenue.Save();

            List<Venue> expectedResult = new List<Venue>{newVenue};
            List<Venue> actual = Venue.GetAll();

            Assert.Equal(expectedResult, actual);
        }

        [Fact]
        public void Find_ReturnsVenueBasedOnId_Venue()
        {
            Venue newVenue = new Venue ("The Showbox");
            newVenue.Save();

            Venue expectedResult = newVenue;
            Venue actualResult = Venue.Find(newVenue.GetId());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Update_EditsVenueName()
        {
            Venue venue = new Venue ("The Showbox");
            venue.Save();

            string newVenueName= "The Crocodile";
            venue.Update(newVenueName);

            string actualResult = venue.GetName();

            Assert.Equal(newVenueName, actualResult);
        }

        public void Dispose()
        {
            Band.DeleteAll();
            Venue.DeleteAll();
        }
    }
}
