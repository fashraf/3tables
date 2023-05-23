using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Check
    {
        public bool CheckIfPatientExist(string NationalId, string Mobile)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            bool spExists = false;
            SqlCommand command = null;
            command = new SqlCommand("select PatientSerno from PatientMaster where (Mobile=@Mobile or NationalId=@NationalId) and Hid=100", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@NationalId", NationalId));
            command.Parameters.Add(new SqlParameter("@Mobile", Mobile));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            reader.Close();
            command.Dispose();
            con.Close();
            con.Dispose();
        }
    }
}