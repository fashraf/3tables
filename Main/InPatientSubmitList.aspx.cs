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
    public partial class InPatientSubmitList : AppCode.Base
    {
        string Institute = string.Empty;
        string Test = string.Empty;
        string Status = string.Empty;
        string Barcode = string.Empty;
        string DateRange = string.Empty;
        string final = string.Empty;
        AppCode.Drop drop = new AppCode.Drop();
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadNewTestList();
                        LoadList();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Submitted Test List";
                        AppCode.Audit.auditlog(UID, UserName, "Submitted Test List", meta, RoleId, false);
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
        private void LoadNewTestList()
        {
            DataTable gettestlist = drop.GetTestList();
            drop.FillDropDownList(Test_Drop, gettestlist, "TestName", "Id");

            //PatientPaymentListGrid.DataSource = AppCode.ListData.getTestRequestList();
            //PatientPaymentListGrid.DataBind();
        }
        private void LoadList()
        {
            try
            {
                string query = "SELECT TestMasterT.TestSerno TestId,TestMasterT.BID Barcode,PaymentMasterT.PaymentSerno PaymentId, TestMasterT.PatientId, PatientMaster.NationalId, PatientMaster.FirstName + ' ' + PatientMaster.LastName AS Name, PatientMaster.Mobile, PaymentMethodL.Payment, PaymentMasterT.TestCost,CASE WHEN PaymentMasterT.PaymentStatus = 1 THEN 'Paid' ELSE 'NotPaid' END AS PaymentStatus, PaymentMasterT.CreatedDt, TestMasterL.TestName, SubTestMasterL.SubTestName,TestMasterT.Completed,case when CONVERT(varchar(1000), TestMasterT.Completed)=1 then 'Complete' else 'In-Complete' end as Status,case when CONVERT(varchar(1000), TestMasterT.Completed)=1 then '../main/NiptView?Id='+CONVERT(varchar(1000), TestMasterT.TestSerno)+'' else '../main/NiptRequest?Id='+CONVERT(varchar(1000), TestMasterT.TestSerno)+'' end as Redirect FROM TestMasterT INNER JOIN PatientMaster ON TestMasterT.PatientId = PatientMaster.PatientSerno INNER JOIN PaymentMasterT ON TestMasterT.TestSerno = PaymentMasterT.TestId INNER JOIN PaymentMethodL ON PaymentMasterT.PaymentType = PaymentMethodL.PaymentSerno INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno";
                if (!string.IsNullOrEmpty(BarcodeTxt.Text))
                {
                    Barcode = string.Format(" and TestMasterT.BID= '{0}'", BarcodeTxt.Text.Trim());
                }
                if (!string.IsNullOrEmpty(DateTxt.Text))
                {
                    DateTime sdt = DateTime.Parse(DateTxt.Text.Split('-')[0]);
                    DateTime edt = DateTime.Parse(DateTxt.Text.Split('-')[1] + " 23:59");

                    string start = sdt.ToString("yyyy-dd-mm");
                    string end = edt.ToString("yyyy-dd-mm") + " 23:59";
                    DateRange = string.Format(" and PaymentMasterT.CreatedDt between '" + sdt + "'and '" + edt + "'");
                }

                if (Test_Drop.SelectedValue != "-1")
                {
                    Test = string.Format(" and  TestMasterT.TestId= {0}", Test_Drop.SelectedValue);
                }

                final = " where  (TestMasterT.TestSerno is not null) " + Barcode + DateRange + Institute + Test + Status;
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                //cmd = new SqlCommand("Sp_MeMohLoad", con);            
                //cmd.CommandType = CommandType.StoredProcedure;
                string total = query + final + " ORDER BY PaymentMasterT.CreatedDt DESC";
                cmd = new SqlCommand(total, con);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@whereClause", final));
                cmd.CommandTimeout = 300;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    PatientPaymentListGrid.DataSource = dt;
                    PatientPaymentListGrid.DataBind();
                    //totalRows.Text = " -" + dt.Rows.Count + " Row/s";
                }
                con.Dispose();
                cmd.Dispose();
                con.Close();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            LoadList();
        }

        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            BarcodeTxt.Text = string.Empty; DateTxt.Text = string.Empty; Test_Drop.SelectedValue = "-1"; ;

            LoadList();
        }
        protected void PatientPaymentListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("NIPTView?Id=" + Id + "");
            }
            else if (e.CommandName == "PaymentId")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("InPatientBillPrint?Id=" + Id + "");
            }
            else if (e.CommandName == "SubmitNIPT")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("NiptRequest?Id=" + Id + "");
            }
        }

        protected void PatientPaymentListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            PatientPaymentListGrid.PageIndex = e.NewPageIndex;
            LoadList();
        }
    }
}