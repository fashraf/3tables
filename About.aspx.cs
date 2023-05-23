using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethod]
        public static string GetEvents()
        {
            string events = GetEventList();
            return events;
        }

        private static string GetEventList()
        {
            string strEvents = "";
            string query = "USP_GetTestCalendar";

            try
            {
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    object obj = command.ExecuteScalar();
                    if (obj != null)
                    {
                        strEvents = Convert.ToString(obj);
                    }
                }
                con.Close();
                if (strEvents != "")
                {
                    strEvents = "[" + strEvents.Substring(0, strEvents.Length - 1) + "]";
                }
                else
                {
                    strEvents = "[]";
                }
            }
            catch (Exception ex)
            {

            }
            return strEvents;
        }
    }
}