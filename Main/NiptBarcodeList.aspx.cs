using InternalLims.AppCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class NiptBarcodeList : Base
    {
        string Institute = string.Empty;
        string Test = string.Empty;
        string Barcode = string.Empty;
        string UsedSatus = string.Empty;
        string final = string.Empty;

        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Audit Audit = new AppCode.Audit();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadNewTestList();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Barcode List";
                        Audit.auditlog(UID, UserName, "NIPT List", meta, RoleId, false);
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
        }

        private void LoadNewTestList()
        {
            DataTable getInstitute = drop.GetInstituteList();
            drop.FillDropDownList(Institute_Drop, getInstitute, "Name", "Id");

            DataTable gettestlist = drop.GetTestList();
            drop.FillDropDownList(Test_Drop, gettestlist, "TestName", "Id");
        }
        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void Me_Link_Click(object sender, EventArgs e)
        {
            string MEID = (sender as LinkButton).CommandArgument;
            Session["MEID"] = MEID.ToString();
            Response.Redirect("Me_Detail.aspx");
        }

        [System.Web.Services.WebMethod]
        public static string getTestKitRequest2(int RequestId)
        {
                Connection Con = new Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                //  command = new SqlCommand("select * from TestMasterT where TestSerno=@RequestId ", con);
                command = new SqlCommand();
                string query = "SELECT distinct TestMasterT.TestSerno, TestMasterT.TestRequestId, TestMasterT.TestId, TestMasterT.SubTestCode, TestMasterT.SubTestId, TestMasterT.TypeId, TestMasterT.InstituteId, TestMasterT.PatientId,case when NiPtMaster.CreatedDt is null then '' else NiPtMaster.CreatedDt end CreatedDt,TestMasterT.CreatedBy, TestMasterT.BID, TestMasterT.SID,NiPtMaster.RecievedatNOVO as ReceiveDt, TestMasterT.Completed,PM.FirstName +' ' +Pm.LastName AS PName FROM NiPtMaster right JOIN PatientMaster AS PM ON NiPtMaster.PatId = PM.PatientSerno right JOIN TestMasterT ON NiPtMaster.TestId = TestMasterT.TestSerno where TestSerno = (" + RequestId + ") ";
                command = new SqlCommand(query, con);
                List<dto.TestMasterT> barcodeWithRelatedData = new List<dto.TestMasterT>();
                dto.TestMasterT testMasterT = new dto.TestMasterT();
                using (command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testMasterT.BID = reader["BID"].ToString();
                            testMasterT.PatName = Convert.ToString(reader["PName"]);
                            //testMasterT.TestSerno = Convert.ToInt32(reader["TestSerno"]);
                            testMasterT.CreatedBy = reader["CreatedBy"].ToString();
                            testMasterT.TestId = Convert.ToInt32(reader["TestSerno"]);
                            testMasterT.CreatedDt = (DateTime)reader["CreatedDt"];
                            var rec = reader["ReceiveDt"];
                            testMasterT.ReceiveDt = (DateTime)reader["ReceiveDt"];
                            barcodeWithRelatedData.Add(testMasterT);
                        }
                    }
                }
                con.Dispose();
                con.Close();
                command.Dispose();
                int UID = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                string Name = HttpContext.Current.Session["FullName"].ToString();
                string UserName = HttpContext.Current.Session["UserName"].ToString();
                int RoleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"].ToString());
                string meta = Name + " Printed Barcode " + testMasterT.BID + " Barcode for TestId : " + RequestId;
                AppCode.Audit.auditlog(UID, UserName, "Print Barcode", meta, RoleId, false);

                var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
                return result;
        }

        protected void gvPrint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TestRequestId = gvPrint.DataKeys[e.Row.RowIndex].Value.ToString();
                Label CreatedDt = e.Row.FindControl("CreatedDt") as Label;
                Label RecievedDate = e.Row.FindControl("RecievedDate") as Label;
                Button PrintName = e.Row.FindControl("PrintName") as Button;
                LinkButton View = e.Row.FindControl("lbOrder") as LinkButton;
                if (CreatedDt.Text == "" || RecievedDate.Text=="")
                {
                    View.Visible = false;
                    PrintName.Visible = false;
                }
            }
        }

        protected void gvPrint_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPrint.PageIndex = e.NewPageIndex;
            SearchData();
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void SearchData()
        {
            string query = "SELECT TestMasterT.TestSerno,TestMasterT.TestRequestId,CASE WHEN NiPtMaster.BarcodeId IS NULL THEN 'Not Used' ELSE 'Used' END AS 'BarcodeStatus',CASE WHEN NiPtMaster.BarcodeId IS NULL THEN 'btn btn-warning btn-sm' ELSE 'btn btn-success btn-sm' END AS 'BarcodeColour',NiPtMaster.TestId, TestMasterL.TestName + ' -' + SubTestMasterL.SubTestName AS TestName, TestMasterT.BID BarcodeId,REPLACE(CONCAT( PatientMaster.FirstName+' ',PatientMaster.MiddleName+' ',PatientMaster.LastName+' '),'  ',' ') PatName, NiPtMaster.CreatedDt AS CreatedDt,NiPtMaster.RecievedatNOVO as RecievedatNOVO, TestMasterT.InstituteId, InstituteMasterL.InstituteName,CityMasterL.City, TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor FROM TestStatusMasterL INNER JOIN PatientMaster INNER JOIN NiPtMaster ON PatientMaster.PatientSerno = NiPtMaster.PatId ON TestStatusMasterL.TestStatusSerno = NiPtMaster.NIPTStatus RIGHT OUTER JOIN TestMasterL INNER JOIN TestMasterT ON TestMasterL.TestMasterSerno = TestMasterT.TestId INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno ON NiPtMaster.TestId = TestMasterT.TestSerno ";

            if (!string.IsNullOrEmpty(BarcodeTxt.Text))
            {
                Barcode = string.Format(" and TestMasterT.BID= '{0}'", BarcodeTxt.Text.Trim());
            }
            if (Institute_Drop.SelectedValue != "-1")
            {
                Institute = string.Format(" and InstituteMasterL.InstituteSerno= {0}", Institute_Drop.SelectedValue);
            }

            if (Test_Drop.SelectedValue != "-1")
            {
                Test = string.Format(" and  TestMasterT.TestId= {0}", Test_Drop.SelectedValue);
            }
            if (UsedTypeDrop.SelectedValue != "-1")
            {
                if (UsedTypeDrop.SelectedValue == "0")
                {
                    UsedSatus = string.Format(" and  NiPtMaster.TestId is null");
                }
                else if (UsedTypeDrop.SelectedValue == "1")
                {
                    UsedSatus = string.Format(" and  NiPtMaster.TestId is not null");
                }
            }


            final = " where  (TestMasterT.TestSerno is not null) " + Barcode + Institute + Test + UsedSatus + " order by TestMasterT.TestSerno desc";
            //final = " where  (TestMasterT.TestSerno is not null) order by TestMasterT.TestSerno desc";
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            string total = query + final;
            cmd = new SqlCommand(total, con);
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 300;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            cmd.Connection = con;
            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);
                gvPrint.DataSource = dt;
                gvPrint.DataBind();
                //totalRows.Text = " -" + dt.Rows.Count + " Row/s";
            }
            cmd.Dispose();
            con.Close();
            con.Dispose();
        }

        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            BarcodeTxt.Text = string.Empty;
            Institute_Drop.SelectedValue="-1";
            Test_Drop.SelectedValue = "-1";
            UsedTypeDrop.SelectedValue = "-1";
            SearchData();
        }
    }
}