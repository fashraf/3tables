using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class InPatientAddPat : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Insert insrt = new AppCode.Insert();
        AppCode.Check chk = new AppCode.Check();
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
                        LoadControl();

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Add Patient Page";
                        AppCode.Audit.auditlog(UID, UserName, "View Add Patient", meta, RoleId, false);
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
        private void LoadControl()
        {
            DataTable Gender = drop.GetGenderList();
            drop.FillDropDownList(GenderDrop, Gender, "Gender", "Id");

            DataTable EthnicBack = drop.GetEthnicBackgroundList();
            drop.FillDropDownList(EthnicBackgroundDrop, EthnicBack, "EthnicBackground", "Id");

            DataTable City = drop.GetCityList();
            drop.FillDropDownList(CityDrop, City, "City", "Id");
        }

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            string NationalId = NationalIdTxt.Text;
            string Mobile = MobileTxt.Text;
            if (chk.CheckIfPatientExist(NationalId, Mobile) == true)
            {
                Confirm_Header_Lbl.Text = "Sorry !";
                Confirm_Middle_Lbl.Text = "The Patient with the same <strong>Mobile Or National Id</strong> is already registered with Novo.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                UpdatePanel2.Update();
                Submit_Btn.Visible = false;
            }
            else
            {
                Submit_Btn.Visible = true;
                Confirm_Header_Lbl.Text = "Confirm !";
                Confirm_Middle_Lbl.Text = "Are you sure you want add Patient Information ?";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                UpdatePanel2.Update();
            }
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string NationalId = NationalIdTxt.Text;
                string MRN = MRNTxt.Text;
                int GenderId = Convert.ToInt32(GenderDrop.SelectedValue);
                string FirstName = FirstNameTxt.Text;
                string MiddleName = MiddleNameTxt.Text;
                string LastName = LastNameTxt.Text;
                var DOBDt = DoBTxt.Text;
                var DOB = DateTime.ParseExact(DOBDt, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string Mobile = MobileTxt.Text;
                string Email = EmailTxt.Text;
                int CityId = Convert.ToInt32(CityDrop.SelectedValue);
                string Address = AddressTxt.Text;
                int EthnicBackgroundId = Convert.ToInt32(EthnicBackgroundDrop.SelectedValue);
                //string CreatedBy = "Fahad Ashraf";
                int HID = 100;// Convert.ToInt32(Session["HID"]);
                int CreatedBy = Convert.ToInt32(Session["UserID"].ToString());
                bool issaudi = false;
                if (IsSaudiChk.Checked == true)
                {
                    issaudi = true;
                }
                else
                {
                    issaudi = false;
                }
                int InsertType = 1;

                bool InsertPatientInfo = insrt.AddPatientInfo(NationalId, MRN, FirstName, MiddleName, LastName, DOB.ToString(), GenderId, CityId, Email, Mobile, EthnicBackgroundId, Address, HID, CreatedBy, InsertType, issaudi);
                if (InsertPatientInfo == true)
                {
                    display = "Patient Information Added !";
                    DisplayToastr(display, toastrTypes.Success.ToString());
                    ///log
                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string Name = Session["FullName"].ToString();
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " Added New Patient For NOVO :" + HID + " with National Id: " + NationalId;
                    AppCode.Audit.auditlog(UID, UserName, "Patient Add", meta, RoleId, false);

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('InPatientPatList') }, 3500);", true);
                }
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
    }
}