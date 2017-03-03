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

        public override bool Equals(Band otherBand)
        {
            if(!(otherBand is Band))
            {
                return false;
            }
            else
            {
                Band newStudent = (Band) otherBand;
                bool idEquality = (this.GetId() == newStudent.GetId());
                bool nameEquality = (this.GetName() == newStudent.GetName());
                return (idEquality && nameEquality);
            }
        }

        public static List<Student> GetAll()
        {
            return List<Student>{};
        }
    }
}
