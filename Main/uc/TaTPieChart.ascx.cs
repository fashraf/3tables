using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class TaTPieChart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]//webmethod for a static getjsondata function so that the method can be accessed using jquery in aspx page.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetJsonData()
        {
            //sql connection
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("NIPTTestListWithTATCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            StringBuilder JSON = new StringBuilder();
            string prefix = "";
            JSON.Append("[");
            while (dr.Read())
            {
                JSON.Append(prefix + "{");
                JSON.Append("\"Tat Exceeded\": " + "\"" + dr[4] + "\",");
                JSON.Append("\"Total\": " + dr[0]);

                JSON.Append("}");
                prefix = ",";
            }
            JSON.Append("]");

            return JSON.ToString();
        }
    }
}