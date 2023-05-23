using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;

namespace InternalLims.AppCode
{
    public class SNotiNovo
    {
        public static void SendNotiToNovo(int notitype, string subject, string msg)
        {
            try
            {
                Connection Con = new Connection();
                String Connection = Con.NovoAdmin();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand command;
                SqlDataReader reader;
                command = new SqlCommand("SELECT EmpMaster.EmpName, EmpMaster.EmpEmail, EmpMaster.EmpMobile, NotiTypeMaster.Notification FROM EmpEmailMaster INNER JOIN EmpMaster ON EmpEmailMaster.EmpId = EmpMaster.EmpSerno INNER JOIN NotiTypeMaster ON EmpEmailMaster.NotificationType = NotiTypeMaster.Serial WHERE (EmpEmailMaster.NotificationType = @notitype)", con);
                command.Parameters.Add(new SqlParameter("@notitype ", notitype));
                reader = command.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        string Name = reader["EmpName"].ToString();
                        string email = (reader["EmpEmail"].ToString());
                        string Sub = subject + (reader["Notification"].ToString());
                        SendEmail(email, Name, Sub, msg);
                    }
                }
                else
                {

                }
                reader.Close();
                command.Clone();
                command.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        internal static void SendEmail(string Email, string Name, string EmailSubject, string msg)
        {
            string to = Email;
            string from = "noreply@novo-genomics.com";
            string subject = EmailSubject;
            string body = SendData(Name, msg);// PopulateBody(Name);
            Thread email = new Thread(delegate () { SendEmail(to, from, subject, body, EmailSubject); });
            email.IsBackground = true;
            email.Start();
        }

        private static string SendData(string name, string msg)
        {
            string body = string.Empty;
            StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("../emiltemp/novo.html"));
            body = reader.ReadToEnd();
            body = body.Replace("{Name}", name);
            body = body.Replace("{msg}", msg);
            return body;
        }

        internal static void SendEmail(string to, string from, string subject, string body, string EmailSubject)
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