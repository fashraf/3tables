using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Response.Cookies.Clear();
            Session.Abandon();
            Session.Clear();

            Session["UserID"] = null;
            Session["FullName"] = null;
            Session["UserName"] = null;
            Session["RoleId"] = null;

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("adminportal", ""));

            HttpCookie reqCookies = Request.Cookies["adminportal"];
            if (reqCookies != null)
            {
                reqCookies.Value = null;
                reqCookies.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(reqCookies);

            }
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}