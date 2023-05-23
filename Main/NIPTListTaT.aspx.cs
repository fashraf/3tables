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
    public partial class NIPTListTaT : AppCode.Base
    {
        string Institute = string.Empty;
        string Test = string.Empty;
        string SubTest = string.Empty;
        string Barcode = string.Empty;
        string DateRange = string.Empty;
        string final = string.Empty;
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
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        ///log
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        string meta = Name + " viewed NIPT LIST.";
                        AppCode.Audit.auditlog(UID, UserName, "NIPT List", meta, RoleId, false);
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
    }
}