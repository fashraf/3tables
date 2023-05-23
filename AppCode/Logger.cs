using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Logger
    {
        public static void WriteLog(Exception objErr, string messageText)
        {
            string err = "Error Message:" + objErr.Message.ToString();
            string ErrorMessage = objErr.Message.ToString();
            string Page = "Error in: " + HttpContext.Current.Request.Url.ToString();
            string Date = DateTime.Now.ToString();
            string Machine = HttpContext.Current.Request.UserHostAddress;
            string Source = objErr.Source;
            string StackTrace = objErr.StackTrace;
            string One = objErr.TargetSite.ToString();
            string oneee = objErr.GetType().ToString();

            string UserId;
            if (HttpContext.Current.Session["FullName"] != null)
            {
                UserId = HttpContext.Current.Session["FullName"].ToString();
            }
            else
            {
                UserId = "";
            }
            //= HttpContext.Current.Session["FullName"].ToString();
            //if (username == "")
            //{
            //    UserId = "";
            //}
            //else
            //{
            //    UserId = username;
            //}

            AppCode.Audit.CatchError(err, Page, ErrorMessage, Date, Machine, Source, StackTrace, One, "Admintal", UserId);
        }
    }
}