using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class HospitalAdd : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Insert insrt = new AppCode.Insert();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        FillData();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Add Hospital.";
                        AppCode.Audit.auditlog(UID, UserName, "New Hospital", meta, RoleId, false);
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

        private void FillData()
        {
            DataTable City = drop.GetCityList(); drop.FillDropDownList(CityDrop, City, "City", "Id");
            DataTable Ownership = drop.GetOwnershipList(); drop.FillDropDownList(OwnershipDrop, Ownership, "Ownership", "Id");
            //DataTable Type = drop.GetOwnershipList(); drop.FillDropDownList(OwnerhipDrop, Ownership, "Ownership", "Id");
        }

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to  <strong>add Hospital</strong>?";
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
                if(StatusChk.Checked==true)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
                string Name = Session["FullName"].ToString();

                bool NewHospital = insrt.AddNewHospial(InstituteName, CityId, OwnershipId, MainBranch, MainInstituteId, CRNumber, Address, status, Name);

                if (NewHospital == true)
                {
                    display = "Hospital Added Successful!";
                    DisplayToastr(display, toastrTypes.Success.ToString());

                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " added New Hospital :" + InstituteName;
                    AppCode.Audit.auditlog(UID, UserName, "New Hospital", meta, RoleId, false);
                }
                else
                {

                }
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
            if(MainBranchId==1)
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