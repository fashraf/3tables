using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class InPatWalkInDash : System.Web.UI.Page
    {
        AppCode.Dashboard dash = new AppCode.Dashboard();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DataCountRepeater.DataSource = dash.GetInPatDsahCount();
            DataCountRepeater.DataBind();
        }
    }
}