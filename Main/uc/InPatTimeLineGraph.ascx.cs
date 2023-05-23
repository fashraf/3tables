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
    public partial class InPatTimeLineGraph : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetJsonData()
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "select MONTH(CreatedDt) MonthPortal,count(PaymentSerno) Total from paymentmastert where year(CreatedDt)=year(getdate()) group by MONTH(CreatedDt) order by MONTH(CreatedDt) asc";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            StringBuilder JSON = new StringBuilder();
            string prefix = "";
            JSON.Append("[");
            while (dr.Read())
            {
                ///{ y: 1, a: 50, b: 90},
                JSON.Append(prefix + "{");
                JSON.Append("y: " + dr[0] + ",");
                JSON.Append("a: " + dr[1]);

                JSON.Append("}");
                prefix = ",";
            }
            JSON.Append("]");

            return JSON.ToString();
        }
    }
}
