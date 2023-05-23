using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        AppCode.Base cc = new AppCode.Base();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = cc.Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        //string currentPageName = Request.Url.Segments[Request.Url.Segments.Length - 1];
                        ////Response.Write("Current Page: " + currentPageName);
                        Name_Lbl.Text ="Logged in as: "+ Session["FullName"].ToString();
                    }
                    else
                    {

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