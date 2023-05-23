using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Base : System.Web.UI.Page
    {
        public string Cookiesvalue()
        {
            var data = "";
            var UID = "";
            var Name = "";
            var UserName = "";
            var RoleId = "";
          
            HttpCookie reqCookies = HttpContext.Current.Request.Cookies["adminportal"];
            if (reqCookies != null)
            {
                UID = reqCookies["UserID"].ToString();
                Name = reqCookies["FullName"].ToString();
                UserName = reqCookies["UserName"].ToString();
                RoleId = reqCookies["RoleId"].ToString();
               
                HttpContext.Current.Session["UserID"] = UID;
                HttpContext.Current.Session["FullName"] = Name;
                HttpContext.Current.Session["UserName"] = UserName;
                HttpContext.Current.Session["RoleId"] = RoleId;

                if (HttpContext.Current.Session["UserID"] != null)
                {
                    data = "valid";

                    //string meta = Name + " logged into " + HospitalName + " and Session was <strong>created</strong>.";
                    //AppCode.audit.auditlog(Convert.ToInt32(HID), Convert.ToInt32(UID), Name, 1, meta, 1, true);
                }

            }
            else
            {
                data = "invalid";
                //string meta = "Session was <strong>Ended</strong>.";
                //AppCode.audit.auditlog(Convert.ToInt32(HID), Convert.ToInt32(UID), Name, 1, meta, 1, true);
            }
            return data;
        }
    }
}