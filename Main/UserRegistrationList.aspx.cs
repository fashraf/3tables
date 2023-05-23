using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.NIPTdto;

namespace InternalLims.Main
{
    public partial class UserRegistrationList : AppCode.Base
    {
        string Institute = string.Empty;
        string Status = string.Empty;
        string Search = string.Empty;
        string final = string.Empty;
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
                        LoadData();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed User Registration List";
                        AppCode.Audit.auditlog(UID, UserName, "User Registration List", meta, RoleId, false);
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

        private void LoadData()
        {
            DataTable Institute = drop.GetInstituteList();
            drop.FillDropDownList(Institute_Drop, Institute, "Name", "Id");

            DataTable Status = drop.GetTestStatusForUser();
            drop.FillDropDownList(Status_Drop, Status, "Status", "Id");

            UserRegisteredGrid.DataSource = AppCode.ListData.getNewRegisterationUserList();
            UserRegisteredGrid.DataBind();
            totalRows.Text = UserRegisteredGrid.Rows.Count + " Row/s";
        }

        protected void UserRegisteredGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UserRegisteredGrid.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void UserRegisteredGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("UserDetail?Id=" + Id + "");
            }
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "SELECT UserMaster.UserSerNo AS UserId, UserTitleMasterL.UserTitle, UserMaster.UserFullName, UserMaster.Email UserEmail, UserMaster.Mobile UserMobile, InstituteMasterL.InstituteName, CityMasterL.City, UserMaster.CreatedDt 'CreatedDate',TestStatusMasterL.TestStatus 'UserStatus',TestStatusMasterL.TestColor FROM UserMaster INNER JOIN InstituteMasterL ON UserMaster.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN UserTitleMasterL ON UserMaster.UserTitleId = UserTitleMasterL.UserTitleSerno INNER JOIN TestStatusMasterL ON UserMaster.UserAccStatus = TestStatusMasterL.TestStatusSerno ";
                if (!string.IsNullOrEmpty(SearchTxt.Text))
                {
                    Search = string.Format(" and UserMaster.UserFullName+' '+UserMaster.Email+' '+ UserMaster.Mobile like '%" + SearchTxt.Text + "%'");
                }

                if (Institute_Drop.SelectedValue != "-1")
                {
                    Institute = string.Format(" and InstituteMasterL.InstituteSerno= {0}", Institute_Drop.SelectedValue);
                }

                if (Status_Drop.SelectedValue != "-1")
                {
                    Status = string.Format(" and TestStatusMasterL.TestStatusSerno= {0}", Status_Drop.SelectedValue);
                }

                final = " where  (UserMaster.UserSerNo is not null) " + Search + Institute + Status + " order by UserMaster.UserSerNo desc";
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                string total = query + final;
                cmd = new SqlCommand(total, con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    UserRegisteredGrid.DataSource = dt;
                    UserRegisteredGrid.DataBind();
                    totalRows.Text = dt.Rows.Count + " Row/s";
                }

                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                JsonMessage obj = new JsonMessage();
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }

        }

        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            LoadData();
            SearchTxt.Text = string.Empty;
        }
    }
}