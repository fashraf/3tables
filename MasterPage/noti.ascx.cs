using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.MasterPage
{
    public partial class noti : System.Web.UI.UserControl
    {
       AppCode.Ts t = new AppCode.Ts();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (IsPostBack == false)
            //{
                LoadNoti();
            //}
        }
        public void LoadNoti()
        {
            try
            {
                //int uid = Convert.ToInt32(Session["UserId"]);
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP (10) TestStatusMasterL.TestStatus, notimaster.noti_msg AS msg, TestStatusMasterL.TestStatusImg AS Img, notimaster.noti_dt AS dt,case when  notiread.noti_id is null then 'alert alert-light alert-dismissible fade show border-0 mb-0' else '' end 'readcss' FROM notimaster INNER JOIN TestStatusMasterL ON notimaster.noti_type = TestStatusMasterL.TestStatusSerno left JOIN notiread ON notimaster.noti_serno = notiread.noti_id AND notimaster.userid = notiread.userid ORDER BY dt DESC", con);
               // SqlCommand cmd = new SqlCommand("GetNotiByUserId", con);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@userid ", uid));
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                sda.Fill(ds);
                int RowCount = ds.Rows.Count;
                //if (RowCount > 0)
                //{
                //    Count_div.Visible = true;
                //}
                //else
                //{
                //    Count_div.Visible = false;
                //}

                string Notitxt = "";
                if (RowCount == 0)
                {
                    Notitxt = "No Notifications.";
                }
                else if (RowCount == 1)
                {
                    Notitxt = " notification.";
                }
                else if (RowCount > 1)
                {
                    Notitxt = " new notifications.";
                }
                 Noti_Count.Text = RowCount.ToString();
                // Count_div.InnerText = RowCount.ToString();
                noti_rpt.DataSource = ds;
                noti_rpt.DataBind();
            }
            catch
            {
            }
        }

        protected void Noti_Grid_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label b = e.Item.FindControl("Dt_Lbl") as Label;
                DateTime dt = Convert.ToDateTime(b.Text);
                b.Text = t.TimeAgo(dt).ToString();
            }
        }
    }
}