using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.NIPTdto;

namespace InternalLims.Main
{
    public partial class ReceiveTestDetail : AppCode.Base
    {
        AppCode.Repository repo = new AppCode.Repository();
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
                        int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
                        LoadTestInfo(TestSerno);

                        report.Src = "../handler/NIPTReport.ashx?Id=" + TestSerno;

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed trasnport List";
                        AppCode.Audit.auditlog(UID, UserName, "Trasnport List", meta, RoleId, false);
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

        private void LoadTestInfo(int TestSerno)
        {
            try
            {
                Status_Grid.DataSource = repo.GetTestAutoWorkFlowStatus(TestSerno);
                Status_Grid.DataBind();

                DataTable dt = repo.getNiptDetailById(TestSerno);
                txtBarCode.Text = dt.Rows[0]["BarcodeId"].ToString();
                TestLbl.Text = dt.Rows[0]["TestName"].ToString() + ">" + dt.Rows[0]["SubTestName"].ToString();
                InstituteNameLbl.Text = dt.Rows[0]["InstituteName"].ToString();
                txtNationalID.Text = dt.Rows[0]["NationalId"].ToString();
                txtPatientMRN.Text = dt.Rows[0]["PatientMRN"].ToString();
                txtName.Text = dt.Rows[0]["Name"].ToString();
                CreatedLbl.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDt"]).ToString("dd.MMMM.yyyy hh:mm tt");

                DobTxt.Text = Convert.ToDateTime(dt.Rows[0]["Dob"]).ToString("dd.MMMM.yyyy") + " ( " + dt.Rows[0]["DobYears"].ToString() + " ) Year/s";
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                CityLbl.Text = dt.Rows[0]["City"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                EthnicLbl.Text = dt.Rows[0]["EthnicBackground"].ToString();

                MenstrualPeriodTxt.Text = Convert.ToDateTime(dt.Rows[0]["LastMenstrualPeriodDate"]).ToString("dd.MMMM.yyyy") + " ( " + dt.Rows[0]["LastMenstrualPeriodWeeks"].ToString() + " ) week/s";
                txtAgeOfGestation.Text = dt.Rows[0]["AgeofGestation"].ToString();
                PregnancyTypeLbl.Text = dt.Rows[0]["PregnancyType"].ToString();

                txtMaternalWeight.Text = dt.Rows[0]["MaternalWeight"].ToString();
                txtMaternalHeight.Text = dt.Rows[0]["MaternalHeight"].ToString();
                PatientBMILbl.Text = Convert.ToDecimal(dt.Rows[0]["BMI"]).ToString("0.00") + "( " + dt.Rows[0]["BMIRESULT"].ToString() + " )";
                MarriageConsanguineousLbl.Text = dt.Rows[0]["MarriageCon"].ToString();
                ModeConceptionLbl.Text = dt.Rows[0]["ModeConception"].ToString();

                HistoryGeneticTestingLbl.Text = dt.Rows[0]["HistoryGenetic"].ToString();
                HistoryAbortionLbl.Text = dt.Rows[0]["Abortion"].ToString();

                LatestUltrasoundTxt.Text = Convert.ToDateTime(dt.Rows[0]["LatestUltrasound"]).ToString("dd.MMMM.yyyy");
                txtUltrasoundFindings.Text = dt.Rows[0]["Ultrasoundfindings"].ToString();
                txtFurtherClinicalDetails.Text = dt.Rows[0]["FurtherClinicalDetails"].ToString();


                txtRequestorName.Text = dt.Rows[0]["RequesterName"].ToString();
                txtRequstorEmail.Text = dt.Rows[0]["RequesterEmail"].ToString();
                txtRequestorMobile.Text = dt.Rows[0]["RequesterMobile"].ToString();
                int statusid = Convert.ToInt32(dt.Rows[0]["StatusID"].ToString());
                if (statusid == 3)
                {
                    Cancle_Lnk.Visible = true;
                }
                else
                {
                    Cancle_Lnk.Visible = false;
                }

                StatusRibbon.Text = dt.Rows[0]["TestStatus"].ToString();

                if (StatusRibbon.Text == "Cancled")
                {
                    StatusRibbon.CssClass = "alert alert-danger border";
                }
                else
                {
                    StatusRibbon.CssClass = "alert alert-primary border";
                }

                DataTable Imgdt = repo.geTestAttachment(TestSerno);
                Img_Grid.DataSource = Imgdt;
                Img_Grid.DataBind();
            }
            catch (Exception ex)
            {
                JsonMessage obj = new JsonMessage();
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Confrim_Btn_Click(object sender, EventArgs e)
        {
            //Confirm_Header_Lbl.Text = "Confirm !";
            //Confirm_Middle_Lbl.Text = "Are you sure you want to <strong>" + StatusDrop.SelectedItem.Text + "</strong> this Test Request ?";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            //UpdatePanel2.Update();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string barcode = txtBarCode.Text;
                int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
                string UpdatedUserName = Session["Name"].ToString();
                int UserStatus = Convert.ToInt32(Status_hdn.Value);
                int StatusId = Convert.ToInt32(Status_hdn.Value);

                bool UpdateUserInfo = repo.UpdateTestRequestStatus(TestSerno, UserStatus, 0, UpdatedUserName);

                repo.InsertTimelineForTest(TestSerno, StatusId, UpdatedUserName, "", "");
                string toastmsg = "";
                if (UpdateUserInfo == true)
                {
                    display = "Test Status Changed !";
                    DisplayToastr(display, toastrTypes.Success.ToString());
                    //string Email =EmailLbl.Text.Trim();
                    //toastmsg = "Test Request Approved.";
                    //string Page = "../emiltemp/ReceiveTestEmail.html";
                    //string EmailSubject = "NIPT -" + barcode;
                    //string msg = "NIPT Request with barcode -" + txtBarCode.Text + " test Request is Approved and Send to the Lab.";
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "launch_toast('" + toastmsg + "')", true);
                    //AppCode.sEmail.SendEmail("User", Email, EmailSubject, Page, msg);

                    string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  is Recieved at Novo Lab.";
                    string Institute = InstituteLbl.Value;
                    AppCode.SNotiNovo.SendNotiToNovo(2, barcode, novomsg);

                    //int InstituteId = Convert.ToInt32(InstituteLbl.Value);
                    //AppCode.notification.SaveNotification(InstituteId, 0, 3, msg, true);
                }
                else
                {

                }
                LoadTestInfo(TestSerno);
            }
            catch (Exception ex)
            {
                JsonMessage obj = new JsonMessage();
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
        protected void Img_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
            Response.TransmitFile(Server.MapPath(e.CommandArgument.ToString()));
            Response.End();
        }

        protected void Status_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Status_hdn.Value = "";
            Status_Name.Value = "";
            if (e.CommandName == "Status")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string Status = (row.FindControl("Status_Lnk") as Button).Text;
                string statusid = (row.FindControl("status_lbl") as Label).Text;
                Status_hdn.Value = statusid;
                Status_Name.Value = Status;
                Confirm_Header_Lbl.Text = "Confirm !";
                Confirm_Middle_Lbl.Text = "Are you sure you want to Change the Status to </br><strong>" + Status + "</strong></br> for this NIPT Test ?";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                UpdatePanel2.Update();
            }
        }

        protected void Cancle_Lnk_Click(object sender, EventArgs e)
        {
            string Id = Request.QueryString["Id"];
            Response.Redirect("CancelTest.aspx?Id=" + Id + "");
        }
    }
}