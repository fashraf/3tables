using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class TrasnportMaster : AppCode.Base
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
                        int TestId = Convert.ToInt32(Request.QueryString["Id"]);
                        FillUserData(TestId);

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Trasport For TestId :"+ TestId+ " in trasnport Detail.";
                        AppCode.Audit.auditlog(UID, UserName, "Trasnport Detail", meta, RoleId, false);
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
            DataTable TransportCompany = drop.GetTransportCompanyList();
            drop.FillDropDownList(TransportCompanyDrop, TransportCompany, "TransportCompany", "Id");

            DataTable TransportInfo = repo.getTrasnportInfoById(TestId);
            if (TransportInfo.Rows.Count > 0)
            {
                alert.InnerText = "Transport Information already Updated.";
                alert.Attributes["class"] = "alert alert-success border-0";

                TransportCompanyDrop.SelectedValue = TransportInfo.Rows[0]["TransportCompanyId"].ToString();
                TrackingTxt.Text = TransportInfo.Rows[0]["TrackingNumber"].ToString();
                DateTxt.Text = Convert.ToDateTime(TransportInfo.Rows[0]["PickUpDate"].ToString()).ToString("dd/MM/yyyy");
            }
            else
            {
                alert.InnerText = "Please Update transport Information.";
                alert.Attributes["class"] = "alert alert-danger border-0";
            }

            DataTable dt = repo.getNiptDetailById(TestId);
            txtBarCode.Text = dt.Rows[0]["BarcodeId"].ToString();
            TestLbl.Text = dt.Rows[0]["TestName"].ToString() + ">" + dt.Rows[0]["SubTestName"].ToString();
            InstituteNameLbl.Text = dt.Rows[0]["InstituteName"].ToString();
            InstituteId.Value = dt.Rows[0]["InstituteId"].ToString();
            StatsLbl.Text = dt.Rows[0]["TestStatus"].ToString();


            CreatedLbl.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDt"]).ToString("dd.MMMM.yyyy hh:mm tt");

            txtRequestorName.Text = dt.Rows[0]["RequesterName"].ToString();
            txtRequstorEmail.Text = dt.Rows[0]["RequesterEmail"].ToString();
            txtRequestorMobile.Text = dt.Rows[0]["RequesterMobile"].ToString();
           
          
            string createdt = ts.GetElapsedTime(Convert.ToDateTime(CreatedLbl.Text));
            SubmitDateLbl.Text =CreatedLbl.Text + " ( " + createdt + " )";
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want Change the status to  <strong>Sent To Pick Up</strong>?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            int TestId = Convert.ToInt32(Request.QueryString["Id"]);
            string Name = Session["FullName"].ToString();
            int StatusId = 2;
            string toastmsg = "Trasnport Information Updated.";
            int IId = Convert.ToInt32(InstituteId.Value);
            string novomsg = "NIPT Request with Barcode  </br><strong>" + txtBarCode.Text + "</strong>  has been updated to Ready for Pickup";

            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("Sp_TransportDetail", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            command.Parameters.Add(new SqlParameter("@TransportCompanyId", Convert.ToInt32(TransportCompanyDrop.SelectedValue)));
            command.Parameters.Add(new SqlParameter("@TrackingNumber", TrackingTxt.Text.Trim()));
            command.Parameters.Add(new SqlParameter("@PickUpDate", DateTime.ParseExact(DateTxt.Text, "dd/MM/yyyy", null)));
            command.Parameters.Add(new SqlParameter("@CreatedBy", Name));
            if (command.ExecuteNonQuery() != 0)
            {
                repo.InsertTimelineForTest(TestId, StatusId, Name, "", "");
                int UID = Convert.ToInt32(Session["UserID"].ToString());
                AppCode.notification.SaveNotification(IId, UID, 1, StatusId, novomsg, false);
                display = "Transport Information Updated !";
                DisplayToastr(display, toastrTypes.Success.ToString());

                ///log
                string UserName = Session["UserName"].ToString();
                int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                string meta = Name + " Updated Trasport For TestId :" + TestId + " with Trasnport Compnay " + TransportCompanyDrop.SelectedItem.Text + " and Tracking Number " + TrackingTxt.Text + " pickup date on " + DateTxt.Text + " in trasnport Detail.";
                AppCode.Audit.auditlog(UID, UserName, "Trasnport Detail", meta, RoleId, false);

                FillUserData(TestId);
            }
            else
            {
               
            }
            con.Dispose();
            con.Close();
            con.Dispose();
        }

        enum toastrTypes { Success, Error, Info, Warning };
        public string display;
        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
    }
}