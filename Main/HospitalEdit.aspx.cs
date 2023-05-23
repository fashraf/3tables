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
    public partial class HospitalEdit : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
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
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Edit Hospital Page.";
                        AppCode.Audit.auditlog(UID, UserName, "Edit Hospital", meta, RoleId, false);
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

        private void LoadData(int Id)
        {
            try
            {
                DataTable City = drop.GetCityList(); drop.FillDropDownList(CityDrop, City, "City", "Id");
                DataTable Ownership = drop.GetOwnershipList(); drop.FillDropDownList(OwnershipDrop, Ownership, "Ownership", "Id");
                DataTable InstituteList = drop.GetInstituteList(); drop.FillDropDownList(InstituteNameDrop, InstituteList, "Name", "Id");
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                command = new SqlCommand("SELECT InstituteName,CityId,OwnershipId,MainBranch,MainInstituteId,CRNumber,Address,InstituteStatus from InstituteMasterL WHERE InstituteSerno=@Id", con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Id", Id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        InstituteNameTxt.Text = reader["InstituteName"].ToString();
                        CityDrop.SelectedValue = reader["CityId"].ToString();
                        OwnershipDrop.SelectedValue = reader["OwnershipId"].ToString();
                        int MainBranch = Convert.ToInt32(reader["MainBranch"].ToString());


                        CRNumberTxt.Text = reader["CRNumber"].ToString();
                        AddressTxt.Text = reader["Address"].ToString();
                        int status = Convert.ToInt32(reader["InstituteStatus"].ToString());
                        int MainInstituteId = Convert.ToInt32(reader["MainInstituteId"].ToString());

                        if (status == 1)
                        {
                            StatusChk.Checked = true;
                        }
                        else if (status == 0)
                        {
                            StatusChk.Checked = false;
                        }
                        if (MainBranch == 1)
                        {
                            MainBranchDrop.SelectedValue = MainBranch.ToString();
                            InstituteNameDrop.SelectedValue = MainInstituteId.ToString();
                        }
                        else
                        {
                            MainBranchDrop.SelectedValue = MainBranch.ToString();
                            InstituteNameDrop.Enabled = false;
                            InstituteNameValidate.Enabled = false;
                        }
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

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to  <strong>Update this Hospital info.</strong>?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string InstituteName = InstituteNameTxt.Text.Trim();
                int CityId = Convert.ToInt32(CityDrop.SelectedValue);
                int OwnershipId = Convert.ToInt32(OwnershipDrop.SelectedValue);
                int MainBranchId = Convert.ToInt32(MainBranchDrop.SelectedValue);
                
                int MainBranch = 0;
                int MainInstituteId = 0;
                if (MainBranchId == 0)////Main 
                {
                    MainBranch = 0;
                    MainInstituteId = 0;
                }
                else if (MainBranchId == 1)////Branch 
                {
                    MainBranch = 1;
                    MainInstituteId = Convert.ToInt32(InstituteNameDrop.SelectedValue);
                }
                string Address = AddressTxt.Text.Trim();
                string CRNumber = CRNumberTxt.Text.Trim();

                bool status;
                if (StatusChk.Checked == true)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                string Name = Session["FullName"].ToString();

                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand command = new SqlCommand("update InstituteMasterL set InstituteName=@InstituteName,CityId=@CityId,OwnershipId=@OwnershipId,MainBranch=@MainBranch,MainInstituteId=@MainInstituteId,CRNumber=@CRNumber,Address=@Address,InstituteStatus=@InstituteStatus,EditBy=@EditBy,EditDt=GetDate() where InstituteSerno=@Id", con);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Id", Id));
                command.Parameters.Add(new SqlParameter("@InstituteName", InstituteName));
                command.Parameters.Add(new SqlParameter("@CityId", CityId));
                command.Parameters.Add(new SqlParameter("@OwnershipId", OwnershipId));
                command.Parameters.Add(new SqlParameter("@MainBranch", MainBranch));
                command.Parameters.Add(new SqlParameter("@MainInstituteId", MainInstituteId));
                command.Parameters.Add(new SqlParameter("@CRNumber", CRNumber));
                command.Parameters.Add(new SqlParameter("@Address", Address));
                command.Parameters.Add(new SqlParameter("@InstituteStatus", status));
                command.Parameters.Add(new SqlParameter("@EditBy", Name));

                if (command.ExecuteNonQuery() > 0)
                {
                    display = "Hospital Updated Successful!";
                    DisplayToastr(display, toastrTypes.Success.ToString());
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('Hospitalist') }, 3500);", true);

                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " updated Hospital :" + InstituteName;
                    AppCode.Audit.auditlog(UID, UserName, "Updated Hospital", meta, RoleId, false);
                }
                else
                {

                }
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

        protected void MainBranchDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MainBranchId = Convert.ToInt32(MainBranchDrop.SelectedValue);
            if (MainBranchId == 1)
            {
                InstituteNameDrop.Enabled = true;
                InstituteNameValidate.Enabled = true;
                DataTable InstituteList = drop.GetInstituteList(); drop.FillDropDownList(InstituteNameDrop, InstituteList, "Name", "Id");
            }
            else
            {
                InstituteNameDrop.Enabled = false;
                InstituteNameValidate.Enabled = false;
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