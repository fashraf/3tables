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
    public partial class NIPTView : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Repository repo = new AppCode.Repository();
        AppCode.Ts ts = new AppCode.Ts();
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

                        niptid.Value = Request.QueryString["Id"];//Session["NIPTID"].ToString();
                        int TestId = Convert.ToInt32(niptid.Value);
                        report.Src = "../handler/NIPTReport.ashx?Id=" + TestId;
                        FillUserData(TestId);

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed NIPT of TestId :" + niptid.Value;
                        AppCode.Audit.auditlog(UID, UserName, "NIPT Detail", meta, RoleId, false);
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
                Status_Grid.DataSource = repo.GetTestAutoWorkFlowStatus(TestId);
                Status_Grid.DataBind();

                CommentGrid.DataSource = repo.getCommentByTestId(TestId);
                CommentGrid.DataBind();
                CommentContLbl.Text = CommentGrid.Rows.Count.ToString();

                DataTable ReOrdered = repo.GetReorderedRecord(TestId);
                if(ReOrdered.Rows.Count>0)
                {
                    ReOrderedLink.HRef = "NIPTView?Id=" + ReOrdered.Rows[0]["TestSerno"].ToString();
                    ReOrderedImg.ImageUrl =  "../assets/images/icon/ReOrdered.png";
                    ReOrderedLink.InnerText = ReOrdered.Rows[0]["BID"].ToString();
                    ReOrderedLbl.Text = "Re-Ordered";
                }
                else
                {
                    ReOrderedImg.ImageUrl = "../assets/images/icon/Regular.png";
                    ReOrderedLbl.Text = "Regular";
                    ReOrderedLink.Visible = false;
                }
                //ReOrderedLink 

                DataTable dt = repo.getNiptDetailById(TestId);
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
                SampleCollectionTxt.Text = Convert.ToDateTime(dt.Rows[0]["SampleCollectionDT"]).ToString("dd.MMMM.yyyy hh:mm tt");
                txtUltrasoundFindings.Text = dt.Rows[0]["Ultrasoundfindings"].ToString();
                txtFurtherClinicalDetails.Text = dt.Rows[0]["FurtherClinicalDetails"].ToString();


                txtRequestorName.Text = dt.Rows[0]["RequesterName"].ToString();
                txtRequstorEmail.Text = dt.Rows[0]["RequesterEmail"].ToString();
                txtRequestorMobile.Text = dt.Rows[0]["RequesterMobile"].ToString();
                int statusid = Convert.ToInt32(dt.Rows[0]["StatusID"].ToString());
                if (statusid == 2)
                {
                    Cancle_Lnk.Visible = false;
                }
                else
                {
                    Cancle_Lnk.Visible = true;
                }
              
                HospitalStatusLbl.Text = dt.Rows[0]["HospitalStatus"].ToString();
                HospitalStatusLbl.CssClass = dt.Rows[0]["HospitalCSS"].ToString();

                string inLabDt = dt.Rows[0]["RecievedatNOVO"].ToString();
                if (inLabDt == "")
                {
                    InLabDtLbl.Text = string.Empty;
                }
                else
                {
                    string ldt = ts.GetElapsedTime(Convert.ToDateTime(inLabDt));
                    InLabDtLbl.Text = "In Lab from " + inLabDt + "</br> ( " + ldt + " )";
                }
                StatusRibbon.Text = dt.Rows[0]["TestStatus"].ToString();
                StatusRibbon.CssClass = dt.Rows[0]["TestColor"].ToString();
                string createdt = ts.GetElapsedTime(Convert.ToDateTime(CreatedLbl.Text));
                SubmitDateLbl.Text = "Submited on " + CreatedLbl.Text + " </br>( " + createdt + " )";

                int NIPTSerno = Convert.ToInt32(dt.Rows[0]["NIPTSerno"].ToString());
                DataTable Imgdt = repo.geTestAttachment(TestId);
                Img_Grid.DataSource = Imgdt;
                Img_Grid.DataBind();
                AttachmentCountLbl.Text = " -" + Imgdt.Rows.Count.ToString();

                DataTable TimeLine = repo.getTimelineByTestId(TestId, 1);
                TimelineRpt.DataSource = TimeLine;
                TimelineRpt.DataBind();

                bool reportRead = repo.CheckIfHospitalReadReport(TestId);
                if(reportRead==true)
                {
                    ReadReportLbl.Text = "Report viewed by hospital";
                    ReadReportLbl.Visible = true;
                }
                else
                {
                    ReadReportLbl.Visible = false;
                }
            }
            catch (Exception ex)
            {
                JsonMessage obj = new JsonMessage();
                obj.Message = "Error Occurred While Saving Data";
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Img_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            Label lblItemType = (Label)row.FindControl("name_lbl");
            int rowindex = row.RowIndex; /* Zero based */

            string itemType = lblItemType.Text;

            Response.Clear();
            Response.ContentType = "application/octet-stream";
            string Name = "NG_" + lblItemType.Text;
            Response.AppendHeader("Content-Disposition", "filename=" + Name);
            //Response.TransmitFile(Server.MapPath(e.CommandArgument.ToString()));
            Response.TransmitFile(e.CommandArgument.ToString());
            Response.End();
        }
        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
                string UpdatedUserName = Session["FullName"].ToString();
                int StatusId = Convert.ToInt32(Status_hdn.Value);

                bool UpdateUserInfo = repo.UpdateTestRequestStatus(TestSerno, StatusId, 0, UpdatedUserName);
                repo.InsertTimelineForTest(TestSerno, StatusId, UpdatedUserName, "", "");
                string barcode = txtBarCode.Text;
                string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  has been updated to " + Status_Name.Value;


                if (StatusId == 13)
                {
                    string emailsub = "[Novo-Genomics]-" + barcode + " Updated";
                    string EmailPage = "../emiltemp/novo.html";
                    AppCode.sEmail.SendEmail(txtRequestorName.Text, txtRequstorEmail.Text, emailsub, EmailPage, novomsg);
                }


                AppCode.SNotiNovo.SendNotiToNovo(2, barcode, novomsg);

                display = "Test Status Updated.!";
                DisplayToastr(display, toastrTypes.Success.ToString());

                ///log
                int UID = Convert.ToInt32(Session["UserID"].ToString());
                string UserName = Session["UserName"].ToString();
                int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                string meta = UpdatedUserName + " Updated NIPT Status For TestId :" + TestSerno + " to status " + StatusId;
                AppCode.Audit.auditlog(UID, UserName, "NIPT Detail", meta, RoleId, false);

                FillUserData(TestSerno);

                int InstituteId = Convert.ToInt32(niptid.Value);
                AppCode.notification.SaveNotification(InstituteId, UID, 1, StatusId, novomsg, false);

                Control c = this.Master.FindControl("noti");
                c.DataBind();
            }
            catch (Exception ex)
            {
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
            else if (e.CommandName == "No")
            {
                Status_Name.Value = "Cancel";
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                string Status = (row.FindControl("Status_Lnk") as Button).Text;
                Status_hdn.Value = "6";
                Confirm_Header_Lbl.Text = "Confirm !";
                Confirm_Middle_Lbl.Text = "Are you sure you want to <strong>Cancel</strong> this Test?";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                UpdatePanel2.Update();
            }
        }

        protected void TimelineRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //Reference the Repeater Item.
                RepeaterItem item = e.Item;
                //Reference the Controls.
                Label dt_txt = (Label)e.Item.FindControl("dt_txt");
                Label serLbl = (Label)e.Item.FindControl("serLbl");
                if (dt_txt.Text == "")
                {
                    serLbl.CssClass = "btn btn-soft-primary";
                }
                else
                {
                    serLbl.CssClass = "btn btn-soft-success";
                    DateTime dt = Convert.ToDateTime(dt_txt.Text);
                    dt_txt.Text = ts.TwoTime(dt);
                }
            }
        }

        protected void Cancle_Lnk_Click(object sender, EventArgs e)
        {
            string Id = Request.QueryString["Id"];
            Response.Redirect("CancelTest.aspx?Id=" + Id + "");
        }

        protected void SubmitComment_Click(object sender, EventArgs e)
        {

            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    try
                    {
                        int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
                        string Comment = CommentTxt.Text.Trim();
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        bool AddNeComment = repo.InsertComment(TestSerno, Comment, UID, Name);
                        if (AddNeComment == true)
                        {
                            string display = "Comment Added Successfully";
                            DisplayToastr(display, toastrTypes.Success.ToString());
                            FillUserData(TestSerno);
                            ///log

                            string UserName = Session["UserName"].ToString();
                            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                            string meta = Name + " Added a Comment " + CommentTxt.Text + " on NIPT TestId " + TestSerno;
                            AppCode.Audit.auditlog(UID, UserName, "NIPT Detail", meta, RoleId, false);
                            CommentTxt.Text = string.Empty;
                        }
                        string barcode = txtBarCode.Text;
                        string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  has been updated to " + Status_Name.Value;
                    }
                    catch (Exception ex)
                    {
                        Exception objErr = ex.GetBaseException();
                        AppCode.Logger.WriteLog(objErr, ex.StackTrace);
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
    }
}