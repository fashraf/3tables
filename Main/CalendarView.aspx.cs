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

namespace InternalLims.Main
{
    public partial class CalendarView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]//webmethod for a static getjsondata function so that the method can be accessed using jquery in aspx page.
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetJsonData()
        {
            //sql connection
           AppCode. Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("USP_GetTestCalendar", con);
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
                JSON.Append("Id: " + "\"" + dr[0] + "\",");
                JSON.Append("title: " + "\"" + dr[1] + "\",");
                JSON.Append("url: " + "\"" + dr[2] + "\",");
                JSON.Append("className: " + "\"" + dr[3] + "\",");
                JSON.Append("description: " + "\"" + dr[7] + "\",");
                JSON.Append("start: " + "\"" + dr[4] + "\",");
                JSON.Append("state: " + "\"" + dr[8] + "\",");
                //JSON.Append("end: " + "\"" + dr[5] + "\",");

                JSON.Append("}");
                prefix = ",";
            }
            JSON.Append("]");
            return JSON.ToString();
        }
    }
}