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
