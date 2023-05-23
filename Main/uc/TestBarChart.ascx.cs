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
    public partial class TestBarChart : System.Web.UI.UserControl
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
            string query = "SELECT COUNT(*) AS Total, CONVERT(VARCHAR(12), CreatedDt, 109) AS dt FROM NiPtMaster GROUP BY CONVERT(VARCHAR(12), CreatedDt, 109) ORDER BY CAST(CONVERT(VARCHAR(12), CreatedDt, 109) AS Date) asc";
            ///string query = "select  COUNT(*) AS Total, CONVERT(varchar(10),right(convert(varchar(10),CreatedDt,105),7)) dt  from     NiPtMaster  GROUP BY CONVERT(varchar(10),right(convert(varchar(10),CreatedDt,105),7))   order by CONVERT(varchar(10),right(convert(varchar(10),CreatedDt,105),7)) asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();


            StringBuilder JSON = new StringBuilder();
            string prefix = "";
            JSON.Append("[");
            while (dr.Read())
            {
                JSON.Append(prefix + "{");
                JSON.Append("\"dt\": " + "\"" + dr[1] + "\",");
                JSON.Append("\"Total\": " + dr[0]);

                JSON.Append("}");
                prefix = ",";
            }
            JSON.Append("]");

            return JSON.ToString();
        }
    }
}
