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
    public partial class Default : AppCode.Base
    {
        AppCode.Dashboard dash = new AppCode.Dashboard();
        protected void Page_Load(object sender, EventArgs e)
        {

            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadData();

                        if (DateTime.Now.Hour < 12)
                        {
                            TimeOftheDayLbl.Text = "Good Morning.";
                        }
                        else if (DateTime.Now.Hour < 17)
                        {
                            TimeOftheDayLbl.Text = "Good Afternoon";
                        }
                        else
                        {
                            TimeOftheDayLbl.Text = "Good Evening";
                        }


                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Dashboard";
                        AppCode.Audit.auditlog(UID, UserName, "Dashboard", meta, RoleId, false);
                    }
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
            //  dt.Text = DateTime.Now.ToString("dddd ,dd.MMMM.yyy");
        }

        private void LoadData()
        {
            UserRpt.DataSource = dash.getRegisteredUserDetail();
            UserRpt.DataBind();


            NiptRequestList.DataSource = dash.getKitsRequestList();
            NiptRequestList.DataBind();

            NiptStatusGrid.DataSource = dash.getNiptUsedNotUsed();
            NiptStatusGrid.DataBind();

            HospitalTestGrid.DataSource = dash.getNiptSubmittedByInstituteToday();
            HospitalTestGrid.DataBind();


            DataTable SubmitedDt = dash.DashNumbers("select count(*) CreatedDt from Niptmaster where CONVERT(date, CreatedDt) = CONVERT(date, getdate())");
            SubmitedLbl.Text = SubmitedDt.Rows[0]["CreatedDt"].ToString();

            DataTable RecieveDt = dash.DashNumbers("select count(*) RecievedatNOVO from Niptmaster where CONVERT(date, RecievedatNOVO) =CONVERT(date, getdate())  ");
            RecievedatNOVOLbl.Text = RecieveDt.Rows[0]["RecievedatNOVO"].ToString();

            DataTable InLabDt = dash.DashNumbers("select count(*) InLabDt from Niptmaster where CONVERT(date, InLabDt) =CONVERT(date, getdate())");
            InLabLbl.Text = InLabDt.Rows[0]["InLabDt"].ToString();

            DataTable ResultDt = dash.DashNumbers("select count(*) ResultDt from Niptmaster where CONVERT(date, ResultDt) =CONVERT(date, getdate())");
            ResultLbl.Text = ResultDt.Rows[0]["ResultDt"].ToString();


            DataTable DeliveryDt = dash.DashNumbers("select count(*) PickUpDate from TransportDetailT where CONVERT(date, PickUpDate) =CONVERT(date, getdate())");
            DeliveryLbl.Text = DeliveryDt.Rows[0]["PickUpDate"].ToString();
        }

        decimal TotalMain = 0M;
        decimal TotalusedMain = 0M;
        decimal TotalUnusedMain = 0M;
        protected void NiptStatusGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTotal = (Label)e.Row.FindControl("lblTotal");
                Label lblNotUsed = (Label)e.Row.FindControl("lblNotUsed");
                Label lblUsed = (Label)e.Row.FindControl("lblUsed");


                decimal Total = Decimal.Parse(lblTotal.Text);
                decimal NotUsed = Decimal.Parse(lblNotUsed.Text);
                decimal Used = Decimal.Parse(lblUsed.Text);

                TotalMain += Total;
                TotalusedMain += NotUsed;
                TotalUnusedMain += Used;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalFooter = (Label)e.Row.FindControl("lblTotalFooter");
                Label lblNotUsedFooter = (Label)e.Row.FindControl("lblNotUsedFooter");
                Label lblUsedFooter = (Label)e.Row.FindControl("lblUsedFooter");

                lblTotalFooter.Text = TotalMain.ToString();
                lblUsedFooter.Text = TotalUnusedMain.ToString();
                lblNotUsedFooter.Text = TotalusedMain.ToString();
            }
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
            string query = "SELECT count(DISTINCT TestMasterT.TestSerno) value,TestStatusMasterL.TestStatus name,TestStatusMasterL.Color color FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.BID = NiPtMaster.BarcodeId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno group by TestStatusMasterL.TestStatus,TestStatusMasterL.Color";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            StringBuilder JSON = new StringBuilder();
            string prefix = "";
            while (dr.Read())
            {
                JSON.Append(prefix + "{");
                JSON.Append("name: " + "\"" + dr[1] + "\",");
                JSON.Append("value: " + dr[0] + ",");
                JSON.Append("color: " + "\"" + dr[2] + "\"");
                JSON.Append("}");
                prefix = ",";
            }
            return JSON.ToString();

            cmd.Dispose();
            con.Close();
            con.Dispose();
        }
    }
}