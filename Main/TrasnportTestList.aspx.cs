using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.NIPTdto;

namespace InternalLims.Main
{
    public partial class TrasnportTestList : AppCode.Base
    {
        string Institute = string.Empty;
        string Test = string.Empty;
        string SubTest = string.Empty;
        string Barcode = string.Empty;
        string DateRange = string.Empty;
        string PickUpDateRange = string.Empty;
        string final = string.Empty;

        AppCode.Audit Audit = new AppCode.Audit();
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

                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed trasnport List";
                        AppCode.Audit.auditlog(UID, UserName, "Trasnport List", meta, RoleId, false);
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
            DataTable getInstitute = drop.GetInstituteList();
            drop.FillDropDownList(Institute_Drop, getInstitute, "Name", "Id");

            DataTable gettestlist = drop.GetTestList();
            drop.FillDropDownList(Test_Drop, gettestlist, "TestName", "Id");

            NewTestListGrid.DataSource = AppCode.ListData.getTrasnportTestList();
            NewTestListGrid.DataBind();
        }

        protected void NewTestListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            NewTestListGrid.PageIndex = e.NewPageIndex;
            LoadNewTestList();
        }

        protected void NewTestListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                //Session["PatId"] = PatId.ToString();
                Response.Redirect("TrasnportMaster?Id=" + Id + "");
            }
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT TestMasterT.TestSerno, NiPtMaster.NIPTSerno AS NIPTID, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, InstituteMasterL.InstituteName, NiPtMaster.NationalId, NiPtMaster.PatientMRN,NiPtMaster.Name, NiPtMaster.Mobile, NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus, TestStatusMasterL.TestColor, TransportDetailT.TransportCompanyId, TransportCompanyL.TransportCompany,TransportDetailT.TrackingNumber, TransportDetailT.PickUpDate FROM TransportDetailT left JOIN TransportCompanyL ON TransportDetailT.TransportCompanyId = TransportCompanyL.TransportCompanySerno right JOIN TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno ON TransportDetailT.TestId = TestMasterT.TestSerno";

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
                    DateRange = string.Format(" and NiPtMaster.CreatedDt  between '" + sdt + "'and '" + edt + "'");
                }

                if (!string.IsNullOrEmpty(PickUpDate.Text))
                {
                    DateTime sdt = DateTime.Parse(PickUpDate.Text.Split('-')[0]);
                    DateTime edt = DateTime.Parse(PickUpDate.Text.Split('-')[1] + " 23:59");

                    string start = sdt.ToString("yyyy-dd-mm");
                    string end = edt.ToString("yyyy-dd-mm") + " 23:59";
                    PickUpDateRange = string.Format(" and TransportDetailT.PickUpDate  between '" + sdt + "'and '" + edt + "'");
                }

                if (Institute_Drop.SelectedValue != "-1")
                {
                    Institute = string.Format(" and InstituteMasterL.InstituteSerno= {0}", Institute_Drop.SelectedValue);
                }

                if (Test_Drop.SelectedValue != "-1")
                {
                    Test = string.Format(" and  TestMasterT.TestId= {0}", Test_Drop.SelectedValue);
                }

                final = " where (TestStatusMasterL.TestStatusSerno between 1 and 2) " + Barcode + DateRange + PickUpDateRange + Institute + Test;
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                string total = query + final + " order by TestMasterT.TestSerno desc";
                cmd = new SqlCommand(total, con);
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    NewTestListGrid.DataSource = dt;
                    NewTestListGrid.DataBind();
                    ViewState["dirState"] = dt;
                    ViewState["sortdr"] = "Asc";
                    //totalRows.Text = " -" + dt.Rows.Count + " Row/s";
                }
                con.Close();
                cmd.Dispose();
                con.Dispose();
            }
            catch (Exception ex)
            {
                JsonMessage obj = new JsonMessage();
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            LoadNewTestList();
            BarcodeTxt.Text = string.Empty;
        }
    }
}