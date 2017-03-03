using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BandTracker
{
    public class Venue
    {
        private int _id;
        private string _name;

        public Venue(string Name, int Id = 0)
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

        public override bool Equals(System.Object otherVenue)
        {
            if(!(otherVenue is Venue))
            {
                return false;
            }
            else
            {
                Venue newVenue = (Venue) otherVenue;
                bool idEquality = (this.GetId() == newVenue.GetId());
                bool nameEquality = (this.GetName() == newVenue.GetName());
                return (idEquality && nameEquality);
            }
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO venues(name) OUTPUT INSERTED.id VALUES( @VenueName);", conn);

            SqlParameter venueName = new SqlParameter();
            venueName.ParameterName = "@VenueName";
            venueName.Value = this.GetName();
            cmd.Parameters.Add(venueName);

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

        public static List<Venue> GetAll()
        {
            List<Venue> AllVenues = new List<Venue> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int venueId = rdr.GetInt32(0);
                string venueName = rdr.GetString(1);

                Venue newVenue = new Venue(venueName, venueId);
                AllVenues.Add(newVenue);
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

        public static Venue Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);

            SqlParameter venueIdParameter = new SqlParameter();
            venueIdParameter.ParameterName = "@VenueId";
            venueIdParameter.Value = id.ToString();
            cmd.Parameters.Add(venueIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundVenueId = 0;
            string foundVenueName = null;

            while(rdr.Read())
            {
                foundVenueId = rdr.GetInt32(0);
                foundVenueName = rdr.GetString(1);
            }
            Venue foundVenue = new Venue(foundVenueName, foundVenueId);

            if(rdr != null)
            {
                rdr.Close();
            }

            if(conn != null)
            {
                conn.Close();
            }

            return foundVenue;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
