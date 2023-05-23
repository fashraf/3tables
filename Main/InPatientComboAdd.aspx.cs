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
    public partial class InPatientComboAdd : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
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
                        string meta = Name + " viewed Add Combo Page";
                        AppCode.Audit.auditlog(UID, UserName, "InPatient Add Combo", meta, RoleId, false);
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
            DataTable getSubtestlist = drop.GetSubTestListOnly();
            drop.FillDropDownList(SubTestDrop, getSubtestlist, "SubTestName", "Id");
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                int SubTestId = Convert.ToInt32(SubTestDrop.SelectedValue);
                string ComboName = ComboNameTxt.Text.Trim();
                int ComboType = Convert.ToInt32(ComboTypeDrop.SelectedValue);
                double Percentage = Convert.ToDouble(PercentageOrAmountTxt.Text);
                double Amount = Convert.ToDouble(PercentageOrAmountTxt.Text);
                bool status = StatusChk.Checked;

                SqlCommand command = new SqlCommand("insert into ComboMasterT (SubTestId,ComboName,isPercent,Percentage,Amount,ComboStatus,CreatedBy,CreatedDt) values(@SubTestId,@ComboName,@isPercent,@Percentage,@Amount,@ComboStatus,@CreatedBy,getdate())", con);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new SqlParameter("@SubTestId", SubTestId));
                command.Parameters.Add(new SqlParameter("@ComboName", ComboName));
                command.Parameters.Add(new SqlParameter("@isPercent", ComboType));
                if (ComboType == 1)
                {
                    command.Parameters.Add(new SqlParameter("@Percentage", Percentage));
                    command.Parameters.Add(new SqlParameter("@Amount", DBNull.Value));
                }
                else if (ComboType == 2)
                {
                    command.Parameters.Add(new SqlParameter("@Percentage", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@Amount", Amount));
                }
                string Name = Session["FullName"].ToString();
                command.Parameters.Add(new SqlParameter("@ComboStatus", status));
                command.Parameters.Add(new SqlParameter("@CreatedBy", Name));

                if (command.ExecuteNonQuery() > 0)
                {
                    display = "Pricing Information Added !";
                    DisplayToastr(display, toastrTypes.Success.ToString());
                    ///log
                    int UID = Convert.ToInt32(Session["UserID"].ToString());

                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " added new Combo Price for Combo " + ComboNameTxt.Text;
                    AppCode.Audit.auditlog(UID, UserName, "Combo Pricing Added", meta, RoleId, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('InPatientComboList') }, 3500);", true);
                }
                else
                {
                    display = "Sorry Something went wrong.";
                    DisplayToastr(display, toastrTypes.Success.ToString());
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

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to Add new Pricing ?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }
        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
    }
}