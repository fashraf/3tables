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
    public partial class UserDetail : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Repository repo = new AppCode.Repository();
        AppCode.Ts t = new AppCode.Ts();
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
                        int UserId = Convert.ToInt32(Request.QueryString["Id"]);
                        LoadPatientInfo(UserId);
                        DataTable getUserStatus = drop.GetUserStatus();
                        drop.FillDropDownList(UserSatusDrop, getUserStatus, "Status", "Id");

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed User Details of User " + UserId;
                        AppCode.Audit.auditlog(UID, UserName, "User Detail", meta, RoleId, false);
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

        private void LoadPatientInfo(int UserId)
        {
            try
            {
                DataTable dt = repo.geUserDetail(UserId);
                //NationalIdTxt.Text = dt.Rows[0]["UserId"].ToString();
                TitleNameLbl.Text = dt.Rows[0]["UserTitle"].ToString() + " " + dt.Rows[0]["UserFullName"].ToString();
                MobileLbl.Text = dt.Rows[0]["Mobile"].ToString();
                EmailLbl.Text = dt.Rows[0]["Email"].ToString();
                InstituteLbl.Text = dt.Rows[0]["InstituteName"].ToString();
                CityLbl.Text = dt.Rows[0]["City"].ToString();
                OwnershipLbl.Text = dt.Rows[0]["Ownership"].ToString();
                UserSatusDrop.Text = dt.Rows[0]["UserAccStatus"].ToString();
                string dtleft = t.GetElapsedTime(Convert.ToDateTime(dt.Rows[0]["CreatedDt"]));
                CreatedDtLbl.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt") + " (" + dtleft + " )";
            }
            catch (Exception ex)
            {
                JsonMessage obj = new JsonMessage();
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to <strong>" + UserSatusDrop.SelectedItem.Text + "</strong> this User ?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                int UserId = Convert.ToInt32(Request.QueryString["Id"]);
                string UpdatedUserName = Session["FullName"].ToString();
                int UserStatus = Convert.ToInt32(UserSatusDrop.SelectedValue);
                bool UpdateUserInfo = repo.UpdateUserInfo(UserId, UserStatus, UpdatedUserName);
                if (UpdateUserInfo == true)
                {
                   

                    string FullName = TitleNameLbl.Text;
                    string Email = EmailLbl.Text.Trim();
                    if (UserSatusDrop.SelectedValue == "10")
                    {
                        display = "User Activated Successfully.";
                        DisplayToastr(display, toastrTypes.Success.ToString());

                        string Page = "../emiltemp/approve.html";
                        string EmailSubject = "Account Activated Successfully.[Novo Genomics]";
                        string msg = "Account Activated Sucessfully.";
                        AppCode.sEmail.SendEmail(FullName, Email, EmailSubject, Page, msg);
                    }
                    else if (UserSatusDrop.SelectedValue == "11")
                    {
                        display = "User Deactivated Successfully.";
                        DisplayToastr(display, toastrTypes.Error.ToString());

                        string Page = "../emiltemp/disapprove.html";
                        string EmailSubject = "Account Not-Approved.[Novo Genomics]";
                        string msg = "Account Not Approved.";
                        AppCode.sEmail.SendEmail(FullName, Email, EmailSubject, Page, msg);
                    }
                    else
                    {
                    }

                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
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
    }
}