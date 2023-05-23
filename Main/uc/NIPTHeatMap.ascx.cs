using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main.uc
{
    public partial class NIPTHeatMap : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]//webmethod for a static getjsondata function so that the method can be accessed using jquery in aspx page.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetTypeChart()
        {
            //sql connection
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "SELECT count(*)'count',convert(varchar,CreatedDt, 23) 'date' from NIPTMaster group by convert(varchar,CreatedDt, 23) order by date desc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            StringBuilder JSON = new StringBuilder();
            string prefix = "";
            while (dr.Read())
            {
                JSON.Append(prefix + "{");
                JSON.Append("count: " + "\"" + dr[0] + "\",");
                //JSON.Append("date: " + dr[1]);
                JSON.Append("date: " + "\"" + dr[1] + "\"");
                JSON.Append("}");
                prefix = ",";
            }   

            return JSON.ToString();
        }
    }
}