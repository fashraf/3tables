using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;


namespace InternalLims.AppCode
{
    public class sEmail
    {
        /// <summary>
        /// Sending Async Email [Welcome Email]
        /// </summary>
        /// <param name="Pharmacy_Manager_Name"></param>
        /// <param name="Pharmacy_Manager_Email"></param>
        internal static void SendEmail(string Name, string Email, string EmailSubject, string Page,string msg)
        {
            string to = Email;
            string from = "noreply@novo-genomics.com";
            string subject = EmailSubject;
            string body = SendData(Name, Page, msg);// PopulateBody(Name);
            Thread email = new Thread(delegate () { SendEmail(to, from, subject, body, EmailSubject,msg); });
            email.IsBackground = true;
            email.Start();
        }

        private static string SendData(string name, string Page,string msg)
        {
            string body = string.Empty;
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(Page));
            body = reader.ReadToEnd();
            body = body.Replace("{Name}", name);
            body = body.Replace("{msg}", msg);
            return body;
        }

        internal static void SendEmail(string to, string from, string subject, string body, string EmailSubject, string msg)
        {
            MailMessage mm = new MailMessage(from, to);
            mm.Subject = subject;
            mm.Body = body;
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.office365.com";
            smtp.UseDefaultCredentials = true;
            NetworkCredential NetworkCred = new NetworkCredential("noreply@novo-genomics.com", "Guv91620");
            smtp.EnableSsl = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            try
            {
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
    }
}