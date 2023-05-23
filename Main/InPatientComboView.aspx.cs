using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class InPatientComboView : AppCode.Base
    {
        int Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["Id"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadCombo();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed InPatient Price List";
                        AppCode.Audit.auditlog(UID, UserName, "InPatient Price List", meta, RoleId, false);
                    }
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }
        }


        private void LoadCombo()
        {
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT TestMasterL.TestName,SubTestMasterL.SubTestName,ComboMasterT.ComboSerno ComboId,ComboMasterT.ComboName, CASE WHEN ComboMasterT.isPercent = 1 THEN 'Percentage' ELSE 'Amount' END AS ComboType, ComboMasterT.Percentage,case when ComboMasterT.Amount is null then  '-' else ComboMasterT.Amount end Amount,case when ComboMasterT.ComboStatus=1 then 'Active' else 'In-Active' end Status FROM ComboMasterT INNER JOIN SubTestMasterL ON ComboMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestMasterL ON SubTestMasterL.TestCodeId = TestMasterL.TestMasterSerno where ComboMasterT.ComboSerno=@ComboId", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            cmd.Parameters.Add(new SqlParameter("@ComboId", Id));
            DataSet ds = new DataSet();
            sda.Fill(ds);
            TestLbl.Text = ds.Tables[0].Rows[0].Field<string>("TestName");
            SubTestLbl.Text = ds.Tables[0].Rows[0].Field<string>("SubTestName");
            ComboNameTxt.Text = ds.Tables[0].Rows[0].Field<string>("ComboName");
            ComboTypeTxt.Text = ds.Tables[0].Rows[0].Field<string>("ComboType");
            if (ComboTypeTxt.Text == "Percentage")
            {
                PercentageOrAmountLbl.Text = ds.Tables[0].Rows[0]["Percentage"].ToString();
            }
            if (ComboTypeTxt.Text == "Amount")
            {
                PercentageOrAmountLbl.Text = ds.Tables[0].Rows[0]["Amount"].ToString(); 
            }
            StatusLbl.Text = ds.Tables[0].Rows[0].Field<string>("Status");
            ComboId.Text = ds.Tables[0].Rows[0].Field<int>("ComboId").ToString();
        }

        protected void EditCombo_Click(object sender, EventArgs e)
        {
            string ID = ComboId.Text;
            Response.Redirect("InPatientComboEdit?Id=" + ID + "");
        }
    }
}