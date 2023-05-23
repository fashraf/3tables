using InternalLims.AppCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.dto;

namespace InternalLims.Main
{
    public partial class TestKitRequestView : AppCode.Base
    {
        AppCode.Repository repo = new AppCode.Repository();
        AppCode.KitRequestBarcode barcode = new AppCode.KitRequestBarcode();
        public static string KitId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        int Id = Convert.ToInt32(Request.QueryString["Id"]);
                        KitId = Request.QueryString["Id"];
                        KitRequestId.Value = Id.ToString();
                        LoadInstituteDetail(Id);

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Kit Request Id :" + Id;
                        AppCode.Audit.auditlog(UID, UserName, "Kit Detail", meta, RoleId, false);
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

        private void LoadInstituteDetail(int Id)
        {

            DataTable dt = repo.getTestKitRequest(Id);
            InstituteHidden.Value = dt.Rows[0]["InstituteId"].ToString();
            InstituteDrp.Text = dt.Rows[0]["InstituteName"].ToString();
            CityTxt.Text = dt.Rows[0]["City"].ToString();

            UsernameLbl.Text = dt.Rows[0]["UserFullName"].ToString();
            MobileLbl.Text = dt.Rows[0]["Mobile"].ToString();
            EmailLbl.Text = dt.Rows[0]["Email"].ToString();
            StatusTxt.CssClass = dt.Rows[0]["TestColor"].ToString() + " form-control";

            StatusTxt.Text = dt.Rows[0]["TestStatus"].ToString();
            TestHidden.Value = dt.Rows[0]["TestId"].ToString();
            Testtxt.Text = dt.Rows[0]["TestName"].ToString();

            SubTestHidden.Value = dt.Rows[0]["SubTestMasterSerno"].ToString();
            SubTesttxt.Text = dt.Rows[0]["SubTestName"].ToString();

            TestRequestTxt.Text = dt.Rows[0]["TestRequest"].ToString();
            string ActualRequest = dt.Rows[0]["ActualRequest"].ToString();
            if (ActualRequest == "")
            {
                ActualTestTxt.Text = dt.Rows[0]["TestRequest"].ToString();
            }
            else
            {
                ActualTestTxt.Text = dt.Rows[0]["ActualRequest"].ToString();
            }


            DtTxt.Text = dt.Rows[0]["CreatedDt"].ToString();

            string StatusId = dt.Rows[0]["StatusId"].ToString();
            if (StatusId == "7")
            {
                PrintAllBtn.Visible = true;
                Confirm_Btn.Visible = false;
            }
            else
            {
                PrintAllBtn.Visible = false;
            }

            TestTakenTestAvailableGrid.DataSource = repo.getHospitalKitInventory(Convert.ToInt32(InstituteHidden.Value));
            TestTakenTestAvailableGrid.DataBind();
            LoadTestGrid();
        }

        public void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = Convert.ToInt32(Request.QueryString["Id"]);

                int testid = Convert.ToInt32(TestHidden.Value);
                int subtestid = Convert.ToInt32(SubTestHidden.Value);
                int TypeId = Convert.ToInt32(1);
                int instituteid = Convert.ToInt32(InstituteHidden.Value);
                string InstituteName = InstituteDrp.Text;
                int Total = Convert.ToInt32(ActualTestTxt.Text);
                string CreatedBy = Session["FullName"].ToString();

                string submitbarcode = barcode.CreateBarcodeReqest(Id, testid, subtestid, TypeId, instituteid, Total, CreatedBy);
                if (submitbarcode == "1")
                {
                   

                    display = "Barcode Created Successful!";
                    DisplayToastr(display, toastrTypes.Success.ToString());

                    GetPrintBarcode(Id);

                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = CreatedBy + " added " + Total + " for Hospital :" + InstituteName;
                    AppCode.Audit.auditlog(UID, UserName, "Add Kit", meta, RoleId, false);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //string jsFunc = "Alert( '" + ex.Message.ToString() + "')";
                string msg = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "launch_toast('" + msg + "')", true);
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "error", jsFunc, true);
            }
        }
        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }

        protected void gvPrint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TestRequestId = gvPrint.DataKeys[e.Row.RowIndex].Value.ToString();
                Label CreatedDt = e.Row.FindControl("CreatedDt") as Label;
                Button PrintName = e.Row.FindControl("PrintName") as Button;
                LinkButton View = e.Row.FindControl("lbOrder") as LinkButton;
                if (CreatedDt.Text == "")
                {
                    View.Visible = false;
                    PrintName.Visible = false;
                }
            }
        }

        private void LoadTestGrid()
        {
            int Id = Convert.ToInt32(Request.QueryString["Id"]);
            string query = "SELECT TestMasterT.TestSerno,TestMasterT.TestRequestId,CASE WHEN NiPtMaster.BarcodeId IS NULL THEN 'Not Used' ELSE 'Used' END AS 'BarcodeStatus',CASE WHEN NiPtMaster.BarcodeId IS NULL THEN 'btn btn-warning btn-sm' ELSE 'btn btn-success btn-sm' END AS 'BarcodeColour',NiPtMaster.TestId, TestMasterL.TestName + ' -' + SubTestMasterL.SubTestName AS TestName, TestMasterT.BID BarcodeId,REPLACE(CONCAT( PatientMaster.FirstName+' ',PatientMaster.MiddleName+' ',PatientMaster.LastName+' '),'  ',' ') PatName, NiPtMaster.CreatedDt AS CreatedDt, TestMasterT.InstituteId, InstituteMasterL.InstituteName,CityMasterL.City, TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor FROM TestStatusMasterL INNER JOIN PatientMaster INNER JOIN NiPtMaster ON PatientMaster.PatientSerno = NiPtMaster.PatId ON TestStatusMasterL.TestStatusSerno = NiPtMaster.NIPTStatus RIGHT OUTER JOIN TestMasterL INNER JOIN TestMasterT ON TestMasterL.TestMasterSerno = TestMasterT.TestId INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno ON NiPtMaster.TestId = TestMasterT.TestSerno where  TestMasterT.TestRequestId=@KitId order by TestMasterT.TestSerno desc";
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@KitId", Id));
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

        private void GetPrintBarcode(int Id)
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            SqlCommand command = new SqlCommand();
            command = new SqlCommand();
            string query = "select * from TestMasterT where TestSerno=" + Id + "";
            command = new SqlCommand(query, con);
            con.Open();

            List<BarCodeLogs> barcodeWithRelatedData = new List<BarCodeLogs>();

            using (command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        BarCodeLogs testMasterT = new BarCodeLogs();
                        testMasterT.BID = reader["BID"].ToString();
                        barcodeWithRelatedData.Add(testMasterT);

                    }
                }
            }

            con.Dispose();
            con.Close();
            con.Dispose();
            string msg = "Kits Created Succeessfully";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "launch_toast('" + msg + "')", true);
            TestTakenTestAvailableGrid.DataSource = repo.getHospitalKitInventory(Convert.ToInt32(InstituteHidden.Value));
            TestTakenTestAvailableGrid.DataBind();
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn4", "alert('Data Saved Succeessfully')", true);
            var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
            string jsFunc = "bindBarcode( '" + result + "')";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);
            LoadInstituteDetail(Id);

            string novomsg =TestRequestTxt.Text + " of " + SubTesttxt.Text + " of NIPT Kits Have been assigned to your Institute.";
            string EmailPage = "../emiltemp/novo.html";
            //novonoti.SendNotiToNovo(16, "", novomsg, Page);
            AppCode.sEmail.SendEmail(UsernameLbl.Text, EmailLbl.Text, "[Novo-Genomics]-NIPT KIT Request.", EmailPage, novomsg);
            int UID = Convert.ToInt32(Session["UserID"].ToString());
            AppCode.notification.SaveNotification(Convert.ToInt32(InstituteHidden.Value), UID, 2, 16, novomsg, false);
            //LoadDropDown();
        }

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
            Submit_Btn.Visible = true;
            ModalTestLbl.Text = Testtxt.Text;
            ModalSubTestLbl.Text = SubTesttxt.Text;
            ModalInstituteLbl.Text = InstituteDrp.Text;
            Total_Lbl.Text = "Actual Test :" + ActualTestTxt.Text;
            Button2.Text = "Close";
        }


        [System.Web.Services.WebMethod]
        public static string SingleTestKitRequest(int RequestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            //  command = new SqlCommand("select * from TestMasterT where TestSerno=@RequestId ", con);
            command = new SqlCommand();
            //string query = " select TestMasterT.*, (PM.FirstName + ' ' + PM.LastName) as PName from TestMasterT left join PatientMaster PM on PM.PatientSerno = TestMasterT.PatientId where TestSerno in (" + RequestId+") ";
            //string query = "SELECT TestMasterT.TestSerno TestId,TestMasterT.BID,PatientMaster.FirstName +' '+PatientMaster.LastName PName,NiPtMaster.CratedBy, NiPtMaster.CreatedDt, NiPtMaster.InLabDt FROM NiPtMaster right JOIN TestMasterT ON NiPtMaster.TestId = TestMasterT.TestSerno left JOIN PatientMaster ON NiPtMaster.PatId = PatientMaster.PatientSerno where TestMasterT.TestSerno=(" + RequestId + ") ";
            string query = "SELECT TestMasterT.TestSerno, TestMasterT.TestRequestId, TestMasterT.TestId, TestMasterT.SubTestCode, TestMasterT.SubTestId, TestMasterT.TypeId, TestMasterT.InstituteId, TestMasterT.PatientId,case when NiPtMaster.CreatedDt is null then '' else NiPtMaster.CreatedDt end CreatedDt,TestMasterT.CreatedBy, TestMasterT.BID, TestMasterT.SID, TestMasterT.ReceiveDt, TestMasterT.Completed, PM.FirstName AS PName FROM NiPtMaster right JOIN PatientMaster AS PM ON NiPtMaster.PatId = PM.PatientSerno right JOIN TestMasterT ON NiPtMaster.TestId = TestMasterT.TestSerno where TestSerno in (" + RequestId + ") ";
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
                        // testMasterT.ReceiveDt = (DateTime)reader["ReceiveDt"];
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

        [System.Web.Services.WebMethod]
        public static string AllTestKitRequest()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand();
            string query = "SELECT TestMasterT.TestSerno, TestMasterT.TestRequestId, TestMasterT.TestId, TestMasterT.SubTestCode, TestMasterT.SubTestId, TestMasterT.TypeId, TestMasterT.InstituteId, TestMasterT.PatientId,case when NiPtMaster.CreatedDt is null then '' else NiPtMaster.CreatedDt end CreatedDt,TestMasterT.CreatedBy, TestMasterT.BID, TestMasterT.SID, TestMasterT.ReceiveDt, TestMasterT.Completed, PM.FirstName AS PName FROM NiPtMaster right JOIN PatientMaster AS PM ON NiPtMaster.PatId = PM.PatientSerno right JOIN TestMasterT ON NiPtMaster.TestId = TestMasterT.TestSerno where TestRequestId =" + KitId + "";
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
                        // testMasterT.ReceiveDt = (DateTime)reader["ReceiveDt"];
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
            string meta = Name + " Printed Barcode Kit Request It : " + KitId;
            AppCode.Audit.auditlog(UID, UserName, "Print Barcode", meta, RoleId, false);

            var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
            return result;
        }
    }
}