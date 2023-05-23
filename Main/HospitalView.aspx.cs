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
    public partial class HospitalView : AppCode.Base
    {
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
                        LoadData(Id);
                        LoadUsers(Id);
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Hospital Details of " + InstituteNameTxt.Text;
                        AppCode.Audit.auditlog(UID, UserName, "View Hospital", meta, RoleId, false);
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

        private void LoadUsers(int Id)
        {
            try
            {
                string query = "SELECT UserMaster.UserSerNo AS UserId, UserTitleMasterL.UserTitle, UserMaster.UserFullName, UserMaster.Email UserEmail, UserMaster.Mobile UserMobile, InstituteMasterL.InstituteName, CityMasterL.City, UserMaster.CreatedDt 'CreatedDate',TestStatusMasterL.TestStatus 'UserStatus',TestStatusMasterL.TestColor FROM UserMaster INNER JOIN InstituteMasterL ON UserMaster.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN UserTitleMasterL ON UserMaster.UserTitleId = UserTitleMasterL.UserTitleSerno INNER JOIN TestStatusMasterL ON UserMaster.UserAccStatus = TestStatusMasterL.TestStatusSerno where InstituteMasterL.InstituteSerno=" + Id + "  order by UserMaster.UserSerNo desc";
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    UserGrid.DataSource = dt;
                    UserGrid.DataBind();
                    //totalRows.Text = dt.Rows.Count + " Row/s";
                }

                cmd.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        private void LoadData(int Id)
        {
            try
            {
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                command = new SqlCommand("SELECT  InstituteMasterL.InstituteName,case when InstituteMasterL.MainBranch =0 then 'Main Hospital' else 'Branch Hospital' end MainBranch,InstituteMasterL.CRNumber, InstituteMasterL.Address,case when InstituteMasterL.InstituteStatus=1 then 'Active' else 'in-Active' end Status,CityMasterL.City, OwnershipMasterL.Ownership,case when InstituteMasterL_1.InstituteName is null then '--' else InstituteMasterL_1.InstituteName end MainBranchInstituteName FROM InstituteMasterL INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN OwnershipMasterL ON InstituteMasterL.OwnershipId = OwnershipMasterL.OwnershipSerno full JOIN InstituteMasterL AS InstituteMasterL_1 ON InstituteMasterL.MainInstituteId = InstituteMasterL_1.InstituteSerno WHERE (InstituteMasterL.InstituteSerno = @Id)", con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Id", Id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        InstituteNameTxt.Text = reader["InstituteName"].ToString();
                        CityLbl.Text = reader["City"].ToString();
                        OwnershipLbl.Text = reader["Ownership"].ToString();
                        MainBranchLbl.Text = reader["MainBranch"].ToString();
                        MainBranchInstituteNameLbl.Text = reader["MainBranchInstituteName"].ToString();
                        CRNumberLbl.Text = reader["CRNumber"].ToString();
                        AddressLbl.Text = reader["Address"].ToString();
                        StatusLbl.Text = reader["Status"].ToString();
                    }
                }
                reader.Close();
                command.Dispose();
                con.Close();
                reader.Dispose();
                con.Dispose();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void UserGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("UserDetail?Id=" + Id + "");
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("HospitalEdit?Id=" + Id + "");
        }
    }
}