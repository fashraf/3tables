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
  
    public partial class InPatientTestPricingEdit : AppCode.Base
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
                        LoadData();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Sub Test Cost of " + SubTestLbl.Text;
                        AppCode.Audit.auditlog(UID, UserName, "Viewed Sub Test Cost", meta, RoleId, false);
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
            try
            {
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                command = new SqlCommand("SELECT SubTestMasterL.SubTestMasterSerno Id,TestMasterL.TestName,SubTestMasterL.SubTestName,SubTestMasterL.SubTestCost Cost FROM SubTestMasterL INNER JOIN TestMasterL ON SubTestMasterL.TestCodeId = TestMasterL.TestMasterSerno where SubTestMasterL.SubTestMasterSerno = @Id", con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@Id", Id));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        TestLbl.Text = reader["TestName"].ToString();
                        SubTestLbl.Text = reader["SubTestName"].ToString();
                        CostTxt.Text = reader["Cost"].ToString();
                        SubTestId.Value = reader["Id"].ToString();
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
            string value = CostTxt.Text;
            decimal number;
            if (Decimal.TryParse(value, out number))
            {
                Confirm_Header_Lbl.Text = "Confirm !";
                Confirm_Middle_Lbl.Text = "Are you sure you want to Update the Cost?";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                UpdatePanel2.Update();
                Submit_Btn.Visible = true;
            }
            else
            {
                //invalid
                Confirm_Header_Lbl.Text = "Sorry !";
                Confirm_Middle_Lbl.Text = "Please enter correct Amount.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                UpdatePanel2.Update();
                Submit_Btn.Visible = false;

                return;
            }
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    string value = CostTxt.Text;
                    decimal number;
                    if (Decimal.TryParse(value, out number))
                    {
                        AppCode.Connection Con = new AppCode.Connection();
                        String Connection = Con.Con();
                        SqlConnection con = new SqlConnection(Connection);
                        con.Open();
                        decimal Cost = Convert.ToDecimal(CostTxt.Text);
                        SqlCommand command = new SqlCommand("update SubTestMasterL set SubTestCost=@SubTestCost where SubTestMasterSerno=@Id", con);
                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@SubTestCost", Cost));
                        command.Parameters.Add(new SqlParameter("@Id", Id));
                        if (command.ExecuteNonQuery()>0)
                        {
                            display = "Test Cost Updated!";
                            DisplayToastr(display, toastrTypes.Success.ToString());

                            ///log
                            int UID = Convert.ToInt32(Session["UserID"].ToString());
                            string UserFullName = Session["FullName"].ToString();
                            string UserName = Session["UserName"].ToString();
                            int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                            string meta = UserFullName + " Updated Sub Test cost of : " + SubTestLbl.Text;
                            AppCode.Audit.auditlog(UID, UserName, "Sub Test Cost Updated", meta, RoleId, false);
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('InPatientTestPricingList') }, 3500);", true);
                        }
                        else
                        {
                            display = "Sorry something went wrong. Please Try Again!";
                            DisplayToastr(display, toastrTypes.Error.ToString());
                        }

                        command.Dispose();
                        con.Close();
                        con.Dispose();
                    }
                    else
                    {
                        //invalid
                        Confirm_Header_Lbl.Text = "Sorry !";
                        Confirm_Middle_Lbl.Text = "Please enter correct Amount.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                        UpdatePanel2.Update();
                        Submit_Btn.Visible = false;
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

        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
    }
}