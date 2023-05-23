using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class NotificationSetting : AppCode.Base
    {
        AppCode.Connection Con = new AppCode.Connection();
        DataTable results = new DataTable();
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
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        LoadSetting(UID, RoleId);
                        ///log
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        string meta = Name + " viewed notification setting.";
                        AppCode.Audit.auditlog(UID, UserName, "NIPT List", meta, RoleId, false);
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


        private void LoadSetting(int EmpId,int Roleid)
        {
            try
            {
                String Connection = Con.NovoAdmin();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                //////command = new SqlCommand("SELECT Teststatus.TestStatusSerno Serial,case when EmpEmailMaster.EmpId is null then 0 else 1 end hasaccess,Teststatus.TestStatus NotificationDesc FROM EmpEmailMaster left JOIN EmpMaster ON EmpEmailMaster.EmpId = EmpMaster.EmpSerno right JOIN NovoLab.dbo.TestStatusMasterL Teststatus ON EmpEmailMaster.NotificationType = Teststatus.TestStatusSerno and (EmpEmailMaster.EmpId =@userid)  where unsedinNoti=1", con);
                command = new SqlCommand("SELECT DISTINCT Teststatus.TestStatusSerno AS Serial,CASE WHEN EmpEmailMaster.EmpId IS NULL THEN 0 ELSE 1 END AS hasaccess,Teststatus.TestStatus AS NotificationDesc FROM     RoleNotificationMasterT RoleNoti right join NovoLab.dbo.TestStatusMasterL Teststatus on Teststatus.TestStatusSerno=RoleNoti.StatusId left join EmpEmailMaster on EmpEmailMaster.NotificationType=RoleNoti.StatusId AND  (EmpEmailMaster.EmpId =@userid) AND (RoleNoti.RoleId =@roleid) where RoleNoti.RoleId is not null", con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@userid", EmpId));
                command.Parameters.Add(new SqlParameter("@roleid", Roleid));
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                NotiRpt.DataSource = dt;
                NotiRpt.DataBind();
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to update your  Setting ?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            Submit_Btn.Visible = true;
            UpdatePanel2.Update();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        UpdateSettng(UID, RoleId);
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
        private void UpdateSettng(int EmpId, int RoleId)
        {
            try
            {
                String Connection = Con.NovoAdmin();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand command = null;
                command = new SqlCommand("insert into EmpEmailMaster(EmpId,NotificationType,Email) values (@EmpId,@NotificationType,@Email)", con);
                foreach (GridViewRow row in NotiRpt.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        Label NotiControlId = (row.FindControl("NotiControlId") as Label);
                        if (((CheckBox)row.FindControl("IdCheckBox")).Checked)
                        {
                            bool Check = CheckIfAlreadyExist(EmpId, Convert.ToInt32(NotiControlId.Text));
                            if (Check == true)
                            {
                                command.Parameters.Clear();
                                command.CommandType = CommandType.Text;
                                command.Parameters.Add("@EmpId", EmpId);
                                command.Parameters.Add("@NotificationType", NotiControlId.Text);
                                command.Parameters.Add("@Email", 1);
                                command.ExecuteNonQuery();

                                LoadSetting(EmpId, RoleId);
                                ///log
                                string Name = Session["FullName"].ToString();
                                string UserName = Session["UserName"].ToString();
                                string meta = Name + " Updated notification setting.";
                                AppCode.Audit.auditlog(EmpId, UserName, "NIPT List", meta, RoleId, false);
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            Delete(EmpId, Convert.ToInt32(NotiControlId.Text));
                        }
                    }
                    string display = "Notification Updated Successfully!";
                    DisplayToastr(display, toastrTypes.Success.ToString());
                }

                con.Dispose();
                con.Close();
                con.Dispose();
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

        private bool CheckIfAlreadyExist(int EmpId, int NotificationType)
        {
            String Connection = Con.NovoAdmin();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("Select EmpEmailSerno from EmpEmailMaster where EmpId=@EmpId and NotificationType=@NotificationType", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@EmpId ", EmpId));
            command.Parameters.Add(new SqlParameter("@NotificationType", NotificationType));
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows == true)
            {
                return false;
            }
            else
            {
                return true;
            }
            con.Dispose();
            con.Close();
            con.Dispose();
        }
        private void Delete(int EmpId, int NotificationType)
        {
            try
            {
                String Connection = Con.NovoAdmin();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                string Insert = "delete from EmpEmailMaster where EmpId=@EmpId and NotificationType=@NotificationType";
                SqlCommand insert = null;
                insert = new SqlCommand(Insert, con);
                insert.CommandType = CommandType.Text;
                insert.Parameters.Add("@EmpId", EmpId);
                insert.Parameters.Add("@NotificationType", NotificationType);
                insert.ExecuteNonQuery();
                insert.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        protected void NotiRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
               
            }
        }
    }
}