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
    public partial class InPatPaymentTypeDonut : System.Web.UI.UserControl
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
            string query = "SELECT count(PaymentMasterT.PaymentSerno) Total,CAST(count(*) * 100.0 / sum(count(*)) over()AS NUMERIC(18,1)) Percentage, PaymentMethodL.Payment FROM PaymentMasterT INNER JOIN PaymentMethodL ON PaymentMasterT.PaymentType = PaymentMethodL.PaymentSerno group by  PaymentMethodL.Payment";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            StringBuilder JSON = new StringBuilder();
            string prefix = "";
            JSON.Append("[");
            while (dr.Read())
            {
                JSON.Append(prefix + "{");
                JSON.Append("label: " + "\"" + dr[2] + "(" + dr[1] + "%)" + "\",");
                JSON.Append("\"value\": " + dr[0] );

                JSON.Append("}");
                prefix = ",";
            }
            JSON.Append("]");

            return JSON.ToString();
        }
    }
}
