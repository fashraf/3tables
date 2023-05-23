using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class CancelTest : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Repository repo = new AppCode.Repository();
        AppCode.Ts ts = new AppCode.Ts();
        AppCode.Insert insert = new AppCode.Insert();
        AppCode.Audit Audit = new AppCode.Audit();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (IsPostBack == false)
                {
                    //Session["HID"] = "1";
                    //Session["UserID"] = "1";

                    niptid.Value = Request.QueryString["Id"];//Session["NIPTID"].ToString();
                    int nid = Convert.ToInt32(niptid.Value);
                    report.Src = "../handler/NIPTReport.ashx?Id=" + nid;
                    FillUserData(nid);
                    //Logger.WriteLog("Add Request page load");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }
        private void FillUserData(int nid)
        {
            DataTable getCancelStatus = drop.GetCancelDropList();
            drop.FillDropDownList(StatusDrop, getCancelStatus, "Status", "Id");

            DataTable dt = repo.getNiptDetailById(nid);
            InstituteNameLbl.Text = dt.Rows[0]["InstituteName"].ToString();
            InstituteId.Value = dt.Rows[0]["InstituteId"].ToString();

            txtBarCode.Text = dt.Rows[0]["BarcodeId"].ToString();
            Lbl.Text = dt.Rows[0]["TestName"].ToString() + ">" + dt.Rows[0]["SubTestName"].ToString();
            txtNationalID.Text = dt.Rows[0]["NationalId"].ToString();
            txtPatientMRN.Text = dt.Rows[0]["PatientMRN"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            CreatedLbl.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDt"]).ToString("dd.MMMM.yyyy");

            DobTxt.Text = Convert.ToDateTime(dt.Rows[0]["Dob"]).ToString("dd.MMMM.yyyy") + " ( " + dt.Rows[0]["DobYears"].ToString() + " ) Year/s";
            txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            CityLbl.Text = dt.Rows[0]["City"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            EthnicLbl.Text = dt.Rows[0]["EthnicBackground"].ToString();
            StatusRibbon.Text = dt.Rows[0]["TestStatus"].ToString();

            if (StatusRibbon.Text == "Cancled")
            {
                StatusRibbon.CssClass = "alert alert-danger border";
            }
            else
            {
                StatusRibbon.CssClass = "alert alert-primary border";
            }
            txtRequestorName.Text = dt.Rows[0]["RequesterName"].ToString();
            txtRequstorEmail.Text = dt.Rows[0]["RequesterEmail"].ToString();
            txtRequestorMobile.Text = dt.Rows[0]["RequesterMobile"].ToString();

            string cancelStatus= dt.Rows[0]["cancelStatus"].ToString();
            if (cancelStatus == "")
            {
                StatusDrop.SelectedValue = "-1";
                RejectionDrop.SelectedValue = "-1";
                RejectionDrop.SelectedValue = "-1";
            }
            else
            {
                StatusDrop.SelectedValue = dt.Rows[0]["cancelStatus"].ToString();
                int CatId = Convert.ToInt32(StatusDrop.SelectedValue);
                DataTable getRejectStatus = drop.GetRejectReasonCatId(CatId);
                drop.FillDropDownList(RejectionDrop, getRejectStatus, "RejectReason", "Id");
                RejectionDrop.SelectedValue = dt.Rows[0]["RejectReasonId"].ToString();
            }
           
           
            CancelTxt.Text = dt.Rows[0]["cancelReason"].ToString();
            HospitalStatusLbl.Text = dt.Rows[0]["HospitalStatus"].ToString();
            HospitalStatusLbl.CssClass = dt.Rows[0]["HospitalCSS"].ToString();

            string inLabDt = dt.Rows[0]["InLabDt"].ToString();
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
            DataTable Imgdt = repo.geTestAttachment(nid);
            Img_Grid.DataSource = Imgdt;
            Img_Grid.DataBind();
            DataTable TimeLine = repo.getTimelineByTestId(nid, 1);
            TimelineRpt.DataSource = TimeLine;
            TimelineRpt.DataBind();
        }

        protected void Img_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
            //Response.TransmitFile(Server.MapPath(e.CommandArgument.ToString()));
            Response.TransmitFile(e.CommandArgument.ToString());
            Response.End();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
            string UpdatedUserName = Session["UserName"].ToString();
            int CancelId = Convert.ToInt32(StatusDrop.SelectedValue);
            string CancelReason = CancelTxt.Text.Trim();
            int ReasonId = Convert.ToInt32(RejectionDrop.SelectedValue);

            string Reason = CancelTxt.Text;
            bool CancelTestInfo = repo.CancelRequestStatus(TestSerno, CancelId, CancelReason, ReasonId, UpdatedUserName);

            //sp_CancelNIPTStatus
            repo.InsertTimelineForTest(TestSerno, CancelId, UpdatedUserName, "", Reason);

            string barcode = txtBarCode.Text;
            string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  has been Canceled";
            AppCode.SNotiNovo.SendNotiToNovo(2, barcode, novomsg);

            //string toastmsg = "Test Status Canceled.";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "launch_toast('" + toastmsg + "')", true);

            display = "Test Status Updated. !";
            DisplayToastr(display, toastrTypes.Error.ToString());
            FillUserData(TestSerno);
        }

        protected void TimelineRpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RepeaterItem item = e.Item;
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
            string status = StatusDrop.SelectedItem.Text;
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to <strong>" + status + "</strong> this Test?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }

        protected void StatusDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int CatId = Convert.ToInt32(StatusDrop.SelectedValue);
            DataTable getRejectStatus = drop.GetRejectReasonCatId(CatId);
            drop.FillDropDownList(RejectionDrop, getRejectStatus, "RejectReason", "Id");
        }

        protected void RemoveBtn_Click(object sender, EventArgs e)
        {
            int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
            string UpdatedUserName = Session["FullName"].ToString();
            bool CancelTestInfo = repo.CancelRequestStatus(TestSerno, 0, "", 0, UpdatedUserName);

            string barcode = txtBarCode.Text;
            string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  has been Canceled";
            AppCode.SNotiNovo.SendNotiToNovo(2, barcode, novomsg);

            //string toastmsg = "Test Status Canceled.";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "launch_toast('" + toastmsg + "')", true);

            display = "Test Status Updated. !";
            DisplayToastr(display, toastrTypes.Success.ToString());
            FillUserData(TestSerno);

        }

        protected void CancleRemoveBtn_Click(object sender, EventArgs e)
        {
            string status = StatusDrop.SelectedItem.Text;
            Reject_Header_Lbl.Text = "Confirm !";
            Reject_Lbl.Text = "Are you sure you want to <strong>" + status + "</strong> this Test?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Rejectmodal').modal('show');</script>", false);
            UpdatePanel2.Update();
        }

        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }

        protected void UploadFile(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    lblMessageLbl.Text = string.Empty;
                    if (FileUpload1.HasFile)
                    {
                        int fileSize = FileUpload1.PostedFile.ContentLength;
                        // Allow only files less than 2,100,000 bytes (approximately 2 MB) to be uploaded.
                        if (fileSize < 2100000)
                        {
                            string FileExtention = System.IO.Path.GetExtension(FileUpload1.FileName);
                            if (FileExtention == ".pdf")

                            {
                                int TestSerno = Convert.ToInt32(Request.QueryString["Id"]);
                                string imgName = FileUpload1.FileName.ToString();
                                //sets the image path
                                DataTable dt = repo.GetRecordIdFromFileUpload(TestSerno);
                                string recordid = dt.Rows[0]["RecordID"].ToString();
                                FileInfo fi = new FileInfo(imgName);
                                string ext = fi.Extension;
                                string unid = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000).ToString();
                                string imgPath = ConfigurationManager.AppSettings["ImagePath"];
                                string onlyimg = unid;
                                string FullPath = imgPath + recordid + "\\" + onlyimg + ext;

                                //then save it to the Folder
                                FileUpload1.SaveAs((FullPath));
                                string imgSize = FileUpload1.PostedFile.ContentLength.ToString();

                                lblMessageLbl.Text = "File Uploaded!";
                                lblMessageLbl.Attributes["class"] = "alert alert-success border-0";

                                string UpdatedUserName = Session["FullName"].ToString();
                                int StatusId = 18;

                                insert.InsertFileUpad(TestSerno, "", recordid, FullPath, imgName, ext, imgSize, "Cancellation/Reject");
                                bool UpdateUserInfo = repo.UpdateTestRequestStatus(TestSerno, StatusId, 0, UpdatedUserName);
                                repo.InsertTimelineForTest(TestSerno, StatusId, UpdatedUserName, "", "");
                                string barcode = txtBarCode.Text;
                                string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  has been updated to Result uploaded";

                                ///log
                                int UID = Convert.ToInt32(Session["UserID"].ToString());
                                string UserName = Session["UserName"].ToString();
                                int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                                string meta = UpdatedUserName + " Updated NIPT Status For TestId :" + TestSerno + " to status " + StatusId;
                                AppCode.Audit.auditlog(UID, UserName, "NIPT Detail", meta, RoleId, false);

                                FillUserData(TestSerno);

                                int HospitalId = Convert.ToInt32(InstituteId.Value);
                                AppCode.notification.SaveNotification(HospitalId, UID, 1, StatusId, novomsg, false);
                            }
                            else
                            {
                                lblMessageLbl.Text = "Only PDF File!";
                                lblMessageLbl.Attributes["class"] = "alert alert-danger border-0";
                            }
                        }
                        else
                        {
                            lblMessageLbl.Text = "Max. Size 2MB!";
                            lblMessageLbl.Attributes["class"] = "alert alert-danger border-0";
                        }
                    }
                    else
                    {
                        lblMessageLbl.Text = "Please Upload Document!";
                        lblMessageLbl.Attributes["class"] = "alert alert-danger border-0";
                    }
                }
            }
        }
    }
}