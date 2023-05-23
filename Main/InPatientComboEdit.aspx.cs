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
    public partial class InPatientComboEdit : AppCode.Base
    {
        int Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["Id"]);
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
                        LoadCombo();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed InPatient Price List";
                        AppCode.Audit.auditlog(UID, UserName, "InPatient Price List", meta, RoleId, false);
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

        private void LoadCombo()
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TestMasterL.TestMasterSerno TestId,SubTestMasterL.SubTestMasterSerno SubTestId,ComboMasterT.ComboSerno ComboId,ComboMasterT.ComboName,ComboMasterT.isPercent ComboType, ComboMasterT.Percentage,ComboMasterT.Amount Amount,case when ComboMasterT.ComboStatus=1 then 'True' else 'False' end Status FROM ComboMasterT INNER JOIN SubTestMasterL ON ComboMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestMasterL ON SubTestMasterL.TestCodeId = TestMasterL.TestMasterSerno where ComboMasterT.ComboSerno=@ComboId", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            cmd.Parameters.Add(new SqlParameter("@ComboId", Id));
            DataSet ds = new DataSet();
            sda.Fill(ds);
            SubTestDrop.SelectedValue = ds.Tables[0].Rows[0].Field<int>("SubTestId").ToString();
            ComboNameTxt.Text = ds.Tables[0].Rows[0].Field<string>("ComboName");
            int ComboType = Convert.ToInt32(ds.Tables[0].Rows[0].Field<int>("ComboType"));
            if (ComboType == 1)
            {
                ComboTypeDrop.SelectedValue = "1";
                PercentageOrAmountTxt.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
            }
            if (ComboType == 2)
            {
                ComboTypeDrop.SelectedValue = "2";
                PercentageOrAmountTxt.Text = ds.Tables[0].Rows[0]["Amount"].ToString();
            }
            string status = ds.Tables[0].Rows[0].Field<string>("Status").ToString();
            StatusChk.Checked = Convert.ToBoolean(status);
        }
        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want Update the Pricing?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
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

                SqlCommand command = new SqlCommand("Update ComboMasterT set SubTestId=@SubTestId,ComboName=@ComboName,isPercent=@isPercent,Percentage=@Percentage,Amount=@Amount,ComboStatus=@ComboStatus,EditBy=@EditBy,EditDt=getdate() where ComboSerno=@ComboId", con);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new SqlParameter("@ComboId", Id));
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
                command.Parameters.Add(new SqlParameter("@EditBy", Name));

                if (command.ExecuteNonQuery() != 0)
                {
                    display = "Pricing Information Updated !";
                    DisplayToastr(display, toastrTypes.Success.ToString());
                    ///log
                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " Update Combo Price for Combo Id :" + Id;
                    AppCode.Audit.auditlog(UID, UserName, "Combo Pricing Update", meta, RoleId, false);
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
        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
    }
}