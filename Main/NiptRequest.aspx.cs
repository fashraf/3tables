using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.NIPTdto;

namespace InternalLims.Main
{
    public partial class NiptRequest : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Audit Audit = new AppCode.Audit();
        AppCode.Repository repo = new AppCode.Repository();
        int Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["Id"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        txtRequestorName.Text = Name;

                        BindDropdownList("CITY", CityDrop);
                        BindDropdownList("ETHNIC", EthnicDrop);
                        BindDropdownList("MarriageCon", MarriageConsanguineousDrop);
                        BindDropdownList("ConceptionMode", ModeConceptionDrop);
                        BindDropdownList("GeneticHist", HistoryGeneticTestingDrop);
                        BindDropdownList("HistoryofAbortion", HistoryofAbortionDrop);
                        BindDropdownList("PregnancyType", PregnancyTypeDrop);
                        FillUserData(Id);
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
        private void FillUserData(int TestId)
        {
            try
            {
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                command = new SqlCommand("SELECT TestMasterL.TestName,TestMasterT.BID Barcode,SubTestMasterL.SubTestMasterSerno, SubTestMasterL.SubTestName, TestMasterT.PatientId, PatientMaster.NationalId, " +
                    "PatientMaster.MRN, PatientMaster.FirstName,PatientMaster.MiddleName, PatientMaster.LastName, PatientMaster.DOB, PatientMaster.CityId, PatientMaster.Email, PatientMaster.Mobile," +
                    " PatientMaster.EthnicBackgroundId,case when PatientMaster.Address='' then '--' else PatientMaster.Address end Address, TestMasterT.TestSerno TestId FROM TestMasterT INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN " +
                    "SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN PatientMaster ON TestMasterT.PatientId = PatientMaster.PatientSerno where TestMasterT.TestSerno=@TestId", con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@TestId", TestId));
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(results);
                adapter.Dispose();
                command.Dispose();
                con.Close();
                con.Dispose();

                BarcodeLbl.Text = results.Rows[0]["Barcode"].ToString();
                TestIdLbl.Text = results.Rows[0]["TestId"].ToString();
                PatientLbl.Text = results.Rows[0]["PatientId"].ToString();
                TestNameLbl.Text = results.Rows[0]["TestName"].ToString() + ">" + results.Rows[0]["SubTestName"].ToString();

                txtNationalID.Text = results.Rows[0]["NationalId"].ToString();
                txtPatientMRN.Text = "--";
                txtFirstName.Text = results.Rows[0]["FirstName"].ToString();
                txtMiddleName.Text = results.Rows[0]["MiddleName"].ToString();
                txtLastName.Text = results.Rows[0]["LastName"].ToString();
                DateTime dob = Convert.ToDateTime(results.Rows[0]["DOB"].ToString());
                DobTxt.Text = dob.ToString("dd/MM/yyyy");

                txtMobile.Text = results.Rows[0]["Mobile"].ToString();
                txtEmail.Text = results.Rows[0]["Email"].ToString();
                CityDrop.Text = results.Rows[0]["CityId"].ToString();
                txtAddress.Text = results.Rows[0]["Address"].ToString();
                EthnicDrop.Text = results.Rows[0]["EthnicBackgroundId"].ToString();
                //txtNationalID.Text = results.Rows[0]["Barcode"].ToString();

                //return results;
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
        }

        private void BindDropdownList(string type, DropDownList ddl)
        {
            DataTable dt = new DataTable();
            dt = AppCode.Drop.GetData(type);

            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "Value";
                ddl.DataValueField = "Key";
                ddl.DataBind();
            }
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    var PatDobDt = DobTxt.Text;
                    var PatDob = DateTime.ParseExact(PatDobDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var LatestUltrasoundDt = LatestUltrasoundTxt.Text;
                    var LatestUltrasound = DateTime.ParseExact(LatestUltrasoundDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var MenstrualPeriodDt = MenstrualPeriodTxt.Text;
                    var MenstrualPeriod = DateTime.ParseExact(MenstrualPeriodDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var SampleCollectionDt = SampleCollectionTxt.Text;
                    var SampleCollection = DateTime.ParseExact(SampleCollectionDt, "dd/MM/yyyy h:mm tt", CultureInfo.InvariantCulture);

                    AppCode.Connection Con = new AppCode.Connection();
                    String Connection = Con.Con();
                    SqlConnection con = new SqlConnection(Connection);
                    con.Open();
                    string Name = txtFirstName.Text + " " + txtMiddleName.Text + " " + txtLastName.Text;
                    //SqlCommand command = new SqlCommand("insert into NiptMaster(TestId,BarcodeId,HID,PatId,NationalId,PatientMRN,Name,DOB,Mobile,Email,CityId,Address,EthnicBackground,LastMenstrualPeriodDate,AgeofGestation,PregnancyTypeId,MaternalWeight,MaternalHeight,MarriageConsanguineousId,ModeofConceptionId,HistoryofgenetictestingId,HistoryofgenetictestingOther,HistoryofAbortionId,LatestUltrasound,SampleCollectionDT,Ultrasoundfindings,FurtherClinicalDetails,RequesterName,RequesterEmail,RequesterMobile,RequesterId,CreatedDt,CratedBy,CreatedByName,NIPTStatus) values(@TestId,@BarcodeId,@HID,@PatId,@NationalId,@PatientMRN,@Name,@DOB,@Mobile,@Email,@CityId,@Address,@EthnicBackground,@LastMenstrualPeriodDate,@AgeofGestation,@PregnancyTypeId,@MaternalWeight,@MaternalHeight,@MarriageConsanguineousId,@ModeofConceptionId,@HistoryofgenetictestingId,@HistoryofgenetictestingOther,@HistoryofAbortionId,@LatestUltrasound,@SampleCollectionDT,@Ultrasoundfindings,@FurtherClinicalDetails,@RequesterName,@RequesterEmail,@RequesterMobile,@RequesterId,@CreatedDt,@CratedBy,@CreatedByName,@NIPTStatus)", con);
                    SqlCommand command = new SqlCommand("Sp_WalkInNIPTInsert", con);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@NationalId", txtNationalID.Text));
                    command.Parameters.Add(new SqlParameter("@TestId", TestIdLbl.Text));
                    command.Parameters.Add(new SqlParameter("@BarcodeId", BarcodeLbl.Text));
                    command.Parameters.Add(new SqlParameter("@HID", 100));
                    command.Parameters.Add(new SqlParameter("@PatId", PatientLbl.Text));
                    //command.Parameters.Add(new SqlParameter("@PatientMRN", txtPatientMRN.Text));
                    command.Parameters.Add(new SqlParameter("@Name", Name));
                    command.Parameters.Add(new SqlParameter("@DOB", PatDob));
                    command.Parameters.Add(new SqlParameter("@Mobile", txtMobile.Text));
                    command.Parameters.Add(new SqlParameter("@Email", txtEmail.Text));
                    command.Parameters.Add(new SqlParameter("@CityId", CityDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@Address", txtAddress.Text));
                    command.Parameters.Add(new SqlParameter("@EthnicBackground", EthnicDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@LastMenstrualPeriodDate", MenstrualPeriod));
                    command.Parameters.Add(new SqlParameter("@AgeofGestation", txtAgeOfGestation.Text));
                    command.Parameters.Add(new SqlParameter("@PregnancyTypeId", PregnancyTypeDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@MaternalWeight", txtMaternalWeight.Text));
                    command.Parameters.Add(new SqlParameter("@MaternalHeight", txtMaternalheight.Text));
                    command.Parameters.Add(new SqlParameter("@MarriageConsanguineousId", MarriageConsanguineousDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@ModeofConceptionId", ModeConceptionDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@HistoryofgenetictestingId", HistoryGeneticTestingDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@HistoryofgenetictestingOther", txtOthers.Text));
                    command.Parameters.Add(new SqlParameter("@HistoryofAbortionId", HistoryofAbortionDrop.SelectedValue));
                    command.Parameters.Add(new SqlParameter("@LatestUltrasound", LatestUltrasound));
                    command.Parameters.Add(new SqlParameter("@SampleCollectionDT", SampleCollection));
                    command.Parameters.Add(new SqlParameter("@Ultrasoundfindings", txtUltrasoundFindings.InnerText));
                    command.Parameters.Add(new SqlParameter("@FurtherClinicalDetails", txtFurtherClinicalDetails.InnerText));
                    command.Parameters.Add(new SqlParameter("@RequesterName", txtRequestorName.Text));
                    command.Parameters.Add(new SqlParameter("@RequesterEmail", ""));
                    command.Parameters.Add(new SqlParameter("@RequesterMobile", ""));
                    command.Parameters.Add(new SqlParameter("@RequesterId", Session["UserID"]));
                    //command.Parameters.Add(new SqlParameter("@CreatedDt", DateTime.Now.ToString()));
                    command.Parameters.Add(new SqlParameter("@CratedBy", Session["UserID"]));
                    command.Parameters.Add(new SqlParameter("@CreatedByName", Session["FullName"]));
                    command.Parameters.Add(new SqlParameter("@NIPTStatus", 3));
                    int rowsUpdated = command.ExecuteNonQuery();
                    if (rowsUpdated ==-1)
                    {
                       display = "NIPT Added Successful!";
                       DisplayToastr(display, toastrTypes.Success.ToString());

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string UserFullName = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = UserFullName + " Added a New NIPT Request with TestId#: " + TestIdLbl.Text;
                        AppCode.Audit.auditlog(UID, UserName, "New NIPT Request", meta, RoleId, false);

                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('NiptList') }, 3500);", true);
                    }
                    else
                    {
                       display = "Sorry something went wrong. Please Try Again!";
                       DisplayToastr(display, toastrTypes.Error.ToString());
                    }
                    command.Dispose();
                    con.Close();
                    con.Dispose();
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

        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
    }
}