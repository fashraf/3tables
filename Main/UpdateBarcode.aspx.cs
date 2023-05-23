using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class UpdateBarcode : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
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
                        TestSernoValue.Value = Request.QueryString["Id"];//Session["NIPTID"].ToString();
                        int TestSerno = Convert.ToInt32(TestSernoValue.Value);
                        LoadData(TestSerno);
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
        private void LoadData(int TestSerno)
        {
            TestDrop.DataSource = drop.GetTestList();
            TestDrop.DataBind();
            DataTable gettestlist = drop.GetTestList();
            drop.FillDropDownList(TestDrop, gettestlist, "TestName", "Id");

            DataTable getSubtestlist = drop.GetSubTestListOnly();
            drop.FillDropDownList(SubTestDrop, getSubtestlist, "SubTestName", "Id");

            DataTable getInstituteList = drop.GetInstituteList();
            drop.FillDropDownList(InstituteDrop, getInstituteList, "Name", "Id");

            DataTable dt = repo.geInstituteBarcode(TestSerno);
            BarcodeIdLbl.Text= dt.Rows[0]["BID"].ToString();
            CompleteStatusLbl.Text = dt.Rows[0]["CompletedStatus"].ToString();
            TestDrop.SelectedValue = dt.Rows[0]["TestId"].ToString();
            SubTestDrop.SelectedValue = dt.Rows[0]["SubtestId"].ToString();

            InstituteDrop.SelectedValue = dt.Rows[0]["InstituteId"].ToString();
            bool status = Convert.ToBoolean(dt.Rows[0]["Completed"].ToString());
            if (status == true)
            {
                ConfirmBtn.Enabled = false;
                ConfirmBtn.Text = "Barcode Used !";
            }
            else
            {
             
            }
            int TestId = Convert.ToInt32(TestDrop.SelectedValue);
            LoadSubTest(TestId);
        }

        protected void TestDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TestId = Convert.ToInt32(TestDrop.SelectedValue);
            LoadSubTest(TestId);
        }

        private void LoadSubTest(int TestId)
        {
            DataTable getsubtest = drop.GetSubTestList(TestId);
            drop.FillDropDownList(SubTestDrop, getsubtest, "SubTestName", "Id");
        }

        protected void ConfirmBtn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
          
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            int TestSerno = Convert.ToInt32(TestSernoValue.Value);  
            int TestId = Convert.ToInt32(TestDrop.SelectedValue);
            int SubTestId = Convert.ToInt32(SubTestDrop.SelectedValue);
            int InstituteId = Convert.ToInt32(InstituteDrop.SelectedValue);

            bool UpdteBarcode = repo.UpdateTestBarcode(TestSerno, TestId, SubTestId, InstituteId);
            if (UpdteBarcode == true)
            {

                string display = "Barcode Information Updated.";
                DisplayToastr(display, toastrTypes.Success.ToString());

                ///log
                int UID = Convert.ToInt32(Session["UserID"].ToString());
                
                string UserName = Session["UserName"].ToString();
                string Name = Session["FullName"].ToString();
                int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                string meta = Name + " Updated Barcode of Id" +TestSerno;
                AppCode.Audit.auditlog(UID, UserName, "Barcode Update", meta, RoleId, false);
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