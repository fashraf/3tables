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
    public partial class ViewNIPT : AppCode.Base
    {
        AppCode.Repository repo = new AppCode.Repository();
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
                        niptid.Value = Request.QueryString["Id"];
                        int nid = Convert.ToInt32(niptid.Value);
                        report.Src = "../handler/NIPTReport.ashx?Id=" + nid;
                        FillUserData(nid);

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed NIPT ID: " + niptid.Value;
                        AppCode.Audit.auditlog(UID, UserName, "NIPT View", meta, RoleId, false);
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

        private void FillUserData(int nid)
        {
            try
            {
                DataTable dt = repo.getNiptDetailById(nid);
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

                MenstrualPeriodTxt.Text = Convert.ToDateTime(dt.Rows[0]["LastMenstrualPeriodDate"]).ToString("dd.MMMM.yyyy") + " ( " + dt.Rows[0]["LastMenstrualPeriodWeeks"].ToString() + " ) week/s";
                txtAgeOfGestation.Text = dt.Rows[0]["AgeofGestation"].ToString();

                txtMaternalWeight.Text = dt.Rows[0]["MaternalWeight"].ToString();
                MarriageConsanguineousLbl.Text = dt.Rows[0]["MarriageCon"].ToString();
                ModeConceptionLbl.Text = dt.Rows[0]["ModeConception"].ToString();

                HistoryGeneticTestingLbl.Text = dt.Rows[0]["HistoryGenetic"].ToString();
                LatestUltrasoundTxt.Text = Convert.ToDateTime(dt.Rows[0]["LatestUltrasound"]).ToString("dd.MMMM.yyyy");
                txtUltrasoundFindings.Text = dt.Rows[0]["Ultrasoundfindings"].ToString();
                txtFurtherClinicalDetails.Text = dt.Rows[0]["FurtherClinicalDetails"].ToString();


                txtRequestorName.Text = dt.Rows[0]["RequesterName"].ToString();
                txtRequstorEmail.Text = dt.Rows[0]["RequesterEmail"].ToString();
                txtRequestorMobile.Text = dt.Rows[0]["RequesterMobile"].ToString();

                //txtRequstorEmail.Text = dt.Rows[0]["NationalId"].ToString();
                //txtRequestorMobile.Text = dt.Rows[0]["NationalId"].ToString();

                //txtRequestorName.Text = dt.Rows[0]["NationalId"].ToString();
                //txtRequestorMobile.Text = dt.Rows[0]["NationalId"].ToString();

                DataTable Imgdt = repo.geTestAttachment(nid);
                Img_Grid.DataSource = Imgdt;
                Img_Grid.DataBind();


            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Img_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
            Response.TransmitFile(Server.MapPath(e.CommandArgument.ToString()));
            Response.End();
        }
    }
}