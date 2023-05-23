using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class RegistrationDetail : AppCode.Base
    {
        AppCode.Repository repo = new AppCode.Repository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                LoadInstituteDetail(1);
            }
        }

        private void LoadInstituteDetail(int Id)
        {
            DataTable dt = repo.getInstituteDetail(2011);
            Institute_Txt.Text = dt.Rows[0]["InstituteName"].ToString();
            AddressTxt.Text = dt.Rows[0]["Address"].ToString();
            CityLbl.Text = dt.Rows[0]["City"].ToString();
            Mobile_Txt.Text = dt.Rows[0]["Ownership"].ToString();
            TypeLbl.Text = dt.Rows[0]["InstituteType"].ToString();
            NameTxt.Text = dt.Rows[0]["UserName"].ToString();
            Email_Txt.Text = dt.Rows[0]["Email"].ToString();
            Mobile_Txt.Text = dt.Rows[0]["Mobile"].ToString();
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {

        }

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            Confirm_Header_Lbl.Text = "Confirm !";
            Confirm_Middle_Lbl.Text = "Are you sure you want to <strong>Aprove this Request </strong> ?";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
            UpdatePanel2.Update();
        }
    }
}