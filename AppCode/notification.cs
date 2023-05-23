using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class notification
    {
        public static void SaveNotification(int hid, int userid, int notitype,int notistatus, string msg, bool showtocreator)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("insert into notimaster(hid,userid,noti_type,noti_status,noti_msg,showtocreator,NotiFrom,noti_dt) values(@hid,@userid,@noti_type,@noti_status,@noti_msg,@showtocreator,2,GetDate())", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@hid", hid));
            command.Parameters.Add(new SqlParameter("@userid", userid));
            command.Parameters.Add(new SqlParameter("@noti_type", notitype));
            command.Parameters.Add(new SqlParameter("@noti_status", notistatus));
            command.Parameters.Add(new SqlParameter("@noti_msg", msg));
            command.Parameters.Add(new SqlParameter("@showtocreator", showtocreator));
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            con.Dispose();
        }
    }
}