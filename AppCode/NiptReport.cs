using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class NiptReport
    {
        public static DataTable GetReportData(int TestId)
        {
            DataTable dt = new DataTable();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetNiptResultCertificate", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.Add(new SqlParameter("@TestId", TestId));

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex.StackTrace);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dt;
        }
    }
}