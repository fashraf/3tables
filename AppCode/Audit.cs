using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace InternalLims.AppCode
{
    public class Audit
    {
        public static string auditlog(int uid, string username, string cat, string meta, int usertype, bool showhide)
        {
            Connection Con = new Connection();
            String Connection = Con.NovoAdmin();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string IP = GetLocalIPAddress();
            SqlCommand command = new SqlCommand("insert into InternalAuditMaster(uid,username,cat,meta,roleid,showhide,ip,dt) values(@uid,@username,@cat,@meta,@roleid,@showhide,@ip,@dt)", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@uid", uid));
            command.Parameters.Add(new SqlParameter("@username", username));
            command.Parameters.Add(new SqlParameter("@cat", cat));
            command.Parameters.Add(new SqlParameter("@meta", meta));
            command.Parameters.Add(new SqlParameter("@roleid", usertype));
            command.Parameters.Add(new SqlParameter("@showhide", showhide));
            command.Parameters.Add(new SqlParameter("@ip", IP));
            command.Parameters.Add(new SqlParameter("@dt", DateTime.Now.ToString()));
            command.ExecuteNonQuery();
            //if (command.ExecuteNonQuery() > 0)
            //{
            return "";
            //}
            //else
            //{
            //    return false;
            //}
            command.Dispose();
            con.Close();
            con.Dispose();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("IP not found!");
        }

        public static string CatchError(string err, string Page, string ErrorMessage, string Date, string Machine, string Source, string StackTrace, string One, string type, string UserId)
        {
            Connection Con = new Connection();
            String Connection = Con.NovoAdmin();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand insert = new SqlCommand("insert into Error(Error,Page,ErrorMessage,Date_Time,Machine,Source,StackTrace,Data,Type,RU,UserId) values(@Error,@Page,@ErrorMessage,@Date_Time,@Machine,@Source,@StackTrace,@Data,@Type,@RU,@UserId)", con);
            insert.CommandType = CommandType.Text;

            insert.Parameters.Add("@Error", err);
            insert.Parameters.Add("@Page", Page);
            insert.Parameters.Add("@ErrorMessage", ErrorMessage);
            insert.Parameters.Add("@Date_Time", Date);
            insert.Parameters.Add("@Machine", Machine);
            insert.Parameters.Add("@Source", Source);
            insert.Parameters.Add("@StackTrace", StackTrace);
            insert.Parameters.Add("@Data", One);
            insert.Parameters.Add("@Type", type);
            insert.Parameters.Add("@RU", false);
            insert.Parameters.Add("@UserId", UserId);
            insert.ExecuteNonQuery();
            HttpContext.Current.Response.Redirect("~/ErrPage.aspx", true);
            return "";
            insert.Dispose();
            con.Close();
        }
    }
}