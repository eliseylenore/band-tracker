using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
    public class Band
    {
        private int _id;
        private string _name;

        public Band(string Name, int Id = 0)
        {
            _name = Name;
            _id = Id;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public override int GetHashCode()
        {
            return this.GetName().GetHashCode();
        }

        public override bool Equals(System.Object otherBand)
        {
            if(!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newBand = (Band) otherBand;
                bool idEquality = (this.GetId() == newBand.GetId());
                bool nameEquality = (this.GetName() == newBand.GetName());
                return (idEquality && nameEquality);
            }
        }

        public static List<Band> GetAll()
        {
            List<Band> AllBands = new List<Band> {};

           SqlConnection conn = DB.Connection();
           conn.Open();

           SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
           SqlDataReader rdr = cmd.ExecuteReader();

           while(rdr.Read())
           {
               int bandId = rdr.GetInt32(0);
               string bandName = rdr.GetString(1);

               Band newBand = new Band(bandName, bandId);
               AllBands.Add(newBand);
           }

           if(rdr != null)
           {
               rdr.Close();
           }
           if(conn != null)
           {
               conn.Close();
           }
           return AllBands;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands(name) OUTPUT INSERTED.id VALUES(@BandName);", conn);

            SqlParameter bandName = new SqlParameter();
            bandName.ParameterName = "@BandName";
            bandName.Value = this.GetName();
            cmd.Parameters.Add(bandName);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Band Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);

            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = id.ToString();
            cmd.Parameters.Add(bandIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundBandId = 0;
            string foundBandName = null;

            while(rdr.Read())
            {
                foundBandId = rdr.GetInt32(0);
                foundBandName = rdr.GetString(1);
            }
            Band foundBand = new Band(foundBandName, foundBandId);

            if(rdr != null)
            {
                rdr.Close();
            }

            if(conn != null)
            {
                conn.Close();
            }

            return foundBand;
        }

        public void AddVenue(Venue newVenue)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO bands_venues(band_id, venue_id) VALUES(@BandId, @VenueId);", conn);

            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = this._id;
            cmd.Parameters.Add(bandIdParameter);

            SqlParameter venueIdParameter = new SqlParameter();
            venueIdParameter.ParameterName = "@VenueId";
            venueIdParameter.Value = newVenue.GetId();
            cmd.Parameters.Add(venueIdParameter);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Venue> GetVenues()
        {
            List<Venue> AllVenues = new List<Venue>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN bands_venues ON (bands.id = bands_venues.band_id) JOIN venues ON (venues.id = bands_venues.venue_id) WHERE bands.id = @BandId;", conn);

            SqlParameter bandIdParameter = new SqlParameter();
            bandIdParameter.ParameterName = "@BandId";
            bandIdParameter.Value = this._id;
            cmd.Parameters.Add(bandIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundVenueId = 0;
            string foundVenueName = null;

            while(rdr.Read())
            {
                foundVenueId = rdr.GetInt32(0);
                foundVenueName = rdr.GetString(1);

                Venue foundVenue = new Venue(foundVenueName, foundVenueId);
                AllVenues.Add(foundVenue);
            }

            if(rdr != null)
            {
                rdr.Close();
            }

            if(conn != null)
            {
                conn.Close();
            }

            return AllVenues;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
            SqlParameter bandIdParameter = new SqlParameter();
            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
