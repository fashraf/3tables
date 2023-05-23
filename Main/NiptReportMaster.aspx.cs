using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.NIPTdto;

namespace InternalLims.Main
{
    public partial class NiptReportMaster : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Repository repo = new AppCode.Repository();
        AppCode.Ts ts = new AppCode.Ts();
        AppCode.Audit Audit = new AppCode.Audit();
        AppCode.Insert insert = new AppCode.Insert();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                            int TestId = Convert.ToInt32(Request.QueryString["Id"]);
                            FillUserData(TestId);
                            //Logger.WriteLog("Add Request page load");
                            int UID = Convert.ToInt32(Session["UserID"].ToString());
                            string Name = Session["FullName"].ToString();
                            string UserName = Session["UserName"].ToString();
                            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                            string meta = Name + " viewed NIPT Result Page of TestId :" + TestId;
                            AppCode.Audit.auditlog(UID, UserName, "Result Page Detail", meta, RoleId, false);
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
                DataTable Imgdt = repo.geTestResultAttachment(TestId);///Get the Attachment.
                if(Imgdt.Rows.Count>0)
                {
                    UploadBtn.Enabled = false;
                    FileUpload1.Enabled = false;
                }
                else
                {

                }
                Img_Grid.DataSource = Imgdt;
                Img_Grid.DataBind();


                DataTable dt = repo.getNiptDetailById(TestId);
                txtBarCode.Text = dt.Rows[0]["BarcodeId"].ToString();
                TestLbl.Text = dt.Rows[0]["TestName"].ToString() + ">" + dt.Rows[0]["SubTestName"].ToString();
                InstituteNameLbl.Text = dt.Rows[0]["InstituteName"].ToString();
                InstituteId.Value = dt.Rows[0]["InstituteId"].ToString();
                StatsLbl.Text = dt.Rows[0]["TestStatus"].ToString();
                StatsLbl.CssClass = dt.Rows[0]["TestColor"].ToString();

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

                CreatedLbl.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDt"]).ToString("dd.MMMM.yyyy hh:mm tt");
                txtRequestorName.Text = dt.Rows[0]["RequesterName"].ToString();
                txtRequstorEmail.Text = dt.Rows[0]["RequesterEmail"].ToString();
                txtRequestorMobile.Text = dt.Rows[0]["RequesterMobile"].ToString();
                string createdt = ts.GetElapsedTime(Convert.ToDateTime(CreatedLbl.Text));
                SubmitDateLbl.Text = CreatedLbl.Text + " ( " + createdt + " )";
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        protected void Img_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            if (e.CommandName == "Download")
            {
                int rowindex = row.RowIndex; /* Zero based */
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                string Name = txtBarCode.Text + "_Reeport.pdf";
                Response.AppendHeader("Content-Disposition", "filename=" + Name);
                Response.TransmitFile(e.CommandArgument.ToString());
                Response.End();
            }
            else if (e.CommandName == "Del")
            {
                //FileInfo file = new FileInfo(e.CommandArgument.ToString());If you want to del..
                //file.Delete();
                Label uid = (Label)row.FindControl("uid");
                DeleteResult(Convert.ToInt32(uid.Text));
            }
        }

        private void DeleteResult(int uid)
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("delete from Fileuploads where UploadID=@UploadID", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@UploadID", uid));
            if (command.ExecuteNonQuery() != 0)
            {
                int TestId = Convert.ToInt32(Request.QueryString["Id"]);
                UpdateNiptTest(TestId);
            }
            else
            {
               
            }
            con.Dispose();
            con.Close();
            con.Dispose();
        }


        private void UpdateNiptTest(int TestId)
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("update NiPtMaster set NIPTStatus=@NIPTStatus,ResultDt=@ResultDt,ResultBy=@ResultBy where TestId=@TestId", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            command.Parameters.Add(new SqlParameter("@NIPTStatus", 17));
            command.Parameters.Add(new SqlParameter("@ResultDt", DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ResultBy", DBNull.Value));
            if (command.ExecuteNonQuery() != 0)
            {
                alert.Text = "Record Deleted";
                alert.Attributes["class"] = "alert alert-danger border-0";
                FillUserData(TestId);
                UploadBtn.Enabled = true;
                FileUpload1.Enabled = true;

                //Logger.WriteLog("Add Request page load");
                int UID = Convert.ToInt32(Session["UserID"].ToString());
                string Name = Session["FullName"].ToString();
                string UserName = Session["UserName"].ToString();
                int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                string meta = Name + " Deleted NIPT Result for TestId :" + TestId;
                AppCode.Audit.auditlog(UID, UserName, "Result Page Detail", meta, RoleId, false);
            }
            else
            {

            }
            con.Dispose();
            con.Close();
            con.Dispose();
        }

        //private void LoadReport()
        //{
        //    try
        //    {
        //        int TestId = Convert.ToInt32(Request.QueryString["Id"]);
        //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report/Report12.rdl");
        //        ReportViewer1.LocalReport.EnableExternalImages = true;
        //        //string imagePath = new Uri(Server.MapPath("~/images/Mudassar.jpg")).AbsoluteUri;
        //        Microsoft.Reporting.WebForms.ReportParameter parameter = new Microsoft.Reporting.WebForms.ReportParameter("TestId", "1");
        //        ReportViewer1.LocalReport.SetParameters(parameter);
        //        int serialNumber = Convert.ToInt32(TestId);
        //        Microsoft.Reporting.WebForms.ReportDataSource datasourcemain = new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", AppCode.NiptReport.GetReportData(serialNumber));
        //        ReportViewer1.LocalReport.DataSources.Clear();
        //        ReportViewer1.LocalReport.DataSources.Add(datasourcemain);
        //        ReportViewer1.LocalReport.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        JsonMessage obj = new JsonMessage();
        //        Exception objErr = ex.GetBaseException();
        //        AppCode.Logger.WriteLog(objErr, ex.StackTrace);
        //    }
        //}

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to  <strong>Add Info to Report</strong>?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }

        //protected void Submit_Btn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int TestId = Convert.ToInt32(Request.QueryString["Id"]);
        //        string Name = Session["FullName"].ToString();
        //        int StatusId = 18;
        //        display = "Report Information Added.!";
        //        DisplayToastr(display, toastrTypes.Success.ToString());

        //        int IId = Convert.ToInt32(InstituteId.Value);
        //        string novomsg = "NIPT Request with Barcode  </br><strong>" + txtBarCode.Text + "</strong>  Report Information Added.";

        //        AppCode.Connection Con = new AppCode.Connection();
        //        String Connection = Con.Con();
        //        SqlConnection con = new SqlConnection(Connection);
        //        con.Open();
        //        SqlCommand command = new SqlCommand("Sp_NiptReportUpdate", con);
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        command.Parameters.Add(new SqlParameter("@TestId", TestId));
        //        command.Parameters.Add(new SqlParameter("@FetalRiskTypeId", FetalRiskTypeDrop.SelectedValue));
        //        command.Parameters.Add(new SqlParameter("@FetalGenderId", FetalGenderDrop.SelectedValue));
        //        command.Parameters.Add(new SqlParameter("@Fetalfraction", FetalfractionTxt.Text));
        //        command.Parameters.Add(new SqlParameter("@CreatedBy", Name));
        //        if (command.ExecuteNonQuery() != 0)
        //        {
        //            bool UpdateUserInfo = repo.UpdateTestRequestStatus(TestId, StatusId, 0, Name);
        //            repo.InsertTimelineForTest(TestId, StatusId, Name, "", "");
        //            int UID = Convert.ToInt32(Session["UserID"].ToString());
        //            AppCode.notification.SaveNotification(IId, UID, 1, StatusId, novomsg, false);
        //            display = "Report Information Updated !";
        //            DisplayToastr(display, toastrTypes.Success.ToString());

        //            FillUserData(TestId);
        //        }
        //        else
        //        {

        //        }
        //        con.Dispose();
        //        con.Close();
        //        con.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        JsonMessage obj = new JsonMessage();
        //        Exception objErr = ex.GetBaseException();
        //        AppCode.Logger.WriteLog(objErr, ex.StackTrace);
        //    }



        //    //bool UpdateUserInfo = repo.UpdateTestRequestStatus(TestSerno, StatusId, 0, UpdatedUserName);

        //    //string barcode = txtBarCode.Text;
        //    //string novomsg = "NIPT Request with Barcode  </br><strong>" + barcode + "</strong>  has been updated to";


        //    //if (StatusId == 13)
        //    //{
        //    //    string emailsub = "[Novo-Genomics]-" + barcode + " Updated";
        //    //    string EmailPage = "../emiltemp/novo.html";
        //    //    AppCode.sEmail.SendEmail(txtRequestorName.Text, txtRequstorEmail.Text, emailsub, EmailPage, novomsg);
        //    //}


        //    //AppCode.SNotiNovo.SendNotiToNovo(2, barcode, novomsg);

        //    //string toastmsg = "Test Status Updated.";
        //    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "launch_toast('" + toastmsg + "')", true);
        //    //FillUserData(TestSerno);

        //    //int InstituteId = Convert.ToInt32(niptid.Value);
        //    //AppCode.notification.SaveNotification(InstituteId, 0, StatusId, novomsg, false);

        //    //Control c = this.Master.FindControl("noti");
        //    //c.DataBind();

        //}
        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }

        protected void ViewReviewBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#ReportConfirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }


        protected void UploadFile(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    alert.Text = string.Empty;
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
                                RecordId.Text = dt.Rows[0]["RecordID"].ToString();
                                FileInfo fi = new FileInfo(imgName);
                                string ext = fi.Extension;
                                string unid = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000).ToString();
                                string imgPath = ConfigurationManager.AppSettings["ImagePath"];
                                string onlyimg = unid;
                                string FullPath = imgPath + RecordId.Text + "\\" + onlyimg + ext;

                                //then save it to the Folder
                                FileUpload1.SaveAs((FullPath));
                                string imgSize = FileUpload1.PostedFile.ContentLength.ToString();

                                alert.Text = "File Uploaded!";
                                alert.Attributes["class"] = "alert alert-success border-0";

                                string UpdatedUserName = Session["FullName"].ToString();
                                int StatusId = 18;

                                insert.InsertFileUpad(TestSerno, "", RecordId.Text, FullPath, imgName, ext, imgSize, "Result");
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
                                alert.Text = "Only PDF File!";
                                alert.Attributes["class"] = "alert alert-danger border-0";
                            }
                        }
                        else
                        {
                            alert.Text = "Max. Size 2MB!";
                            alert.Attributes["class"] = "alert alert-danger border-0";
                        }
                    }
                    else
                    {
                        alert.Text = "Please Upload Document!";
                        alert.Attributes["class"] = "alert alert-danger border-0";
                    }
                }
            }
        }
    }
}