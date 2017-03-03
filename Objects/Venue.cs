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
            // List<Venue> AllVenues = new List<Venue> {};
            //
            // SqlConnection conn = DB.Connection();
            // conn.Open();
            //
            // SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
            // SqlDataReader rdr = cmd.ExecuteReader();
            //
            // while(rdr.Read())
            // {
            //     int venueId = rdr.GetInt32(0);
            //     string venueName = rdr.GetString(1);
            //
            //     Venue newVenue = new Venue(venueName, venueId);
            //     AllVenues.Add(newVenue);
            // }
            //
            // if(rdr != null)
            // {
            //     rdr.Close();
            // }
            // if(conn != null)
            // {
            //     conn.Close();
            // }

            Venue newVenue = new Venue("Hey");
            List<Venue> AllVenues = new List<Venue>{newVenue};
            return AllVenues;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM venues;", conn);
            SqlParameter bandIdParameter = new SqlParameter();
            cmd.ExecuteNonQuery();

            conn.Close();

        }
    }
}
