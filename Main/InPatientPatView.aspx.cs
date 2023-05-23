using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class InPatientPatView : AppCode.Base
    {
        AppCode.Repository repo = new AppCode.Repository();
        AppCode.Audit Audit = new AppCode.Audit();
        int Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["Id"]);
        //string nid = HttpContext.Current.Session["N"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadPatientInfo(Id);
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Patient Detail of Patient Id:" + Id;
                        AppCode.Audit.auditlog(UID, UserName, "Patient Detail", meta, RoleId, false);
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
        private void LoadPatientInfo(int pid)
        {
            DataTable dt = repo.getPatientDetail(pid);
            PatRpt.DataSource = dt;
            PatRpt.DataBind();

            PatTestList.DataSource = AppCode.ListData.GetTestListByPatId(pid);
            PatTestList.DataBind();
        }
        protected void Edit_Btn_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditPatientInfo?Id=" + Id + "");
        }

        protected void PatTestList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PatTestList.PageIndex = e.NewPageIndex;
            LoadPatientInfo(Id);
        }

        protected void PatTestList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string TestId = commandArgs;
                Response.Redirect("NIPTView?Id=" + TestId + "");
            }
        }

        protected void AddTestBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InPatientBillTest?Id=" + Id + "");
        }
    }
}