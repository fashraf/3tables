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
    public partial class TestKitRequestList : AppCode.Base
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
            if (IsPostBack == false)
            {
                LoadNewKitList();
            }
        }

        private void LoadNewKitList()
        {
            DataTable getInstitute = drop.GetInstituteList();
            drop.FillDropDownList(Institute_Drop, getInstitute, "Name", "Id");

            DataTable gettestlist = drop.GetTestList();
            drop.FillDropDownList(Test_Drop, gettestlist, "TestName", "Id");

            //DataTable getstatus = drop.GetTestStatus();
            //drop.FillDropDownList(Status_Drop, getstatus, "Status", "Id");

            KitListGrid.DataSource = AppCode.ListData.getTestKitRequestList();
            KitListGrid.DataBind();
        }
        protected void NewTestListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            KitListGrid.PageIndex = e.NewPageIndex;
            LoadNewKitList();
        }

        protected void NewTestListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("TestKitRequestView?Id=" + Id + "");
            }
        }

        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            string query = "SELECT TestRequestMasterT.TestReqSerno RequestId,TestRequestMasterT.TestId, TestMasterL.TestName, SubTestMasterL.SubTestMasterSerno SubTestId, SubTestMasterL.SubTestName,TestRequestMasterT.TestRequest 'RequestedTest',case when TestRequestMasterT.ActualRequest is null then CONVERT(varchar(100), '---') else CONVERT(varchar(100), TestRequestMasterT.ActualRequest) end ActualTest , TestRequestMasterT.InstituteId, InstituteMasterL.InstituteName, CityMasterL.City,TestStatusMasterL.TestStatus 'Status',TestStatusMasterL.TestColor,TestRequestMasterT.CreatedDt FROM TestMasterL INNER JOIN TestRequestMasterT ON TestMasterL.TestMasterSerno = TestRequestMasterT.TestId INNER JOIN SubTestMasterL ON TestRequestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN InstituteMasterL ON TestRequestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN TestStatusMasterL ON TestRequestMasterT.RequestStatus = TestStatusMasterL.TestStatusSerno"; 
            if (!string.IsNullOrEmpty(DateTxt.Text))
            {
                DateTime sdt = DateTime.Parse(DateTxt.Text.Split('-')[0]);
                DateTime edt = DateTime.Parse(DateTxt.Text.Split('-')[1] + " 23:59");

                string start = sdt.ToString("yyyy-dd-mm");
                string end = edt.ToString("yyyy-dd-mm") + " 23:59";
                DateRange = string.Format(" and TestRequestMasterT.CreatedDt  between '" + sdt + "'and '" + edt + "'");
            }

            if (Institute_Drop.SelectedValue != "-1")
            {
                Institute = string.Format(" and InstituteMasterL.InstituteSerno= {0}", Institute_Drop.SelectedValue);
            }

            if (Test_Drop.SelectedValue != "-1")
            {
                Test = string.Format(" and TestRequestMasterT.TestId= {0}", Test_Drop.SelectedValue);
            }

            final = " where  (TestRequestMasterT.TestReqSerno is not null) " + Barcode + DateRange + Institute + Test + Status + " order by TestRequestMasterT.TestReqSerno desc";
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            //cmd = new SqlCommand("Sp_MeMohLoad", con);            
            //cmd.CommandType = CommandType.StoredProcedure;
            string total = query + final;
            cmd = new SqlCommand(total, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@whereClause", final));
            cmd.CommandTimeout = 300;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            cmd.Connection = con;
            using (DataTable dt = new DataTable())
            {
                sda.Fill(dt);
                KitListGrid.DataSource = dt;
                KitListGrid.DataBind();
                //totalRows.Text = " -" + dt.Rows.Count + " Row/s";
            }
        }
        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            LoadNewKitList();
        }
    }
}