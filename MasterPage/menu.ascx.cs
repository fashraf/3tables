using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.MasterPage
{
    public partial class menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                // Get the key from config file
                int appid = 1;// Convert.ToInt32(Session["appid"]);//(int)settingsReader.GetValue("appid", typeof(int));
                int roleid = Convert.ToInt32(Session["ROLEID"]);
                LoadParent(appid, roleid);
            }
        }

        private void LoadParent(int appid, int roleid)
        {
            if (!this.IsPostBack)
            {
                ParentRepeater.DataSource = GetData("SELECT distinct MenuOrderBy,Menu.MenuID, Menu.MenuText,SubMenu.SubMenuIcon FROM Menu INNER JOIN SubMenu ON Menu.MenuID = SubMenu.MenuID INNER JOIN RoleAccessMaster ON SubMenu.SubMenu = RoleAccessMaster.PageId where RoleAccessMaster.RoleId=" + roleid + " and SubMenu.Show=1 order by MenuOrderBy asc");
                ParentRepeater.DataBind();
            }
        }

        private static DataTable GetData(string query)
        {
            string constr = ConfigurationManager.ConnectionStrings["permission"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

        protected void ParentRepeater_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string MenuId = (e.Item.FindControl("MenuId_Lbl") as Label).Text;
                int roleid = Convert.ToInt32(Session["ROLEID"]);
                Repeater rptOrders = e.Item.FindControl("ChildRepeater") as Repeater;
                //rptOrders.DataSource = GetData(string.Format("SELECT SubMenu.SubMenuText, SubMenu.SubMenuURL,SubMenu.SubMenuIcon FROM SubMenu INNER JOIN RoleAccessMaster ON SubMenu.SubMenu = RoleAccessMaster.PageId WHERE SubMenu.MenuID='{0}' and RoleAccessMaster.RoleId=" + roleid + " and SubMenu.Show=1", MenuId));
                rptOrders.DataSource = GetData(string.Format("SELECT distinct SubMenu.SubMenuText, SubMenu.SubMenuURL,SubMenu.SubMenuIcon,SubMenuOrderBy FROM SubMenu INNER JOIN RoleAccessMaster ON SubMenu.SubMenu = RoleAccessMaster.PageId WHERE SubMenu.MenuID='{0}'  and RoleAccessMaster.RoleId=1 and SubMenu.Show=1 order by SubMenuOrderBy asc", MenuId));
                rptOrders.DataBind();
            }
        }
    }
}
