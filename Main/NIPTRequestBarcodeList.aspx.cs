using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.dto;

namespace InternalLims.Main
{
    public partial class NIPTRequestBarcodeList : AppCode.Base
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPatentGrid();
            }
        }

        private void LoadPatentGrid()
        {
            gvParent.DataSource = GetData("Sp_NIPTTestRequestBarcodeList");
            gvParent.DataBind();
        }

        private static DataTable GetData(string query)
        {
            string strConnString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(strConnString))
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
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string TestRequestId = gvParent.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvOrders = e.Row.FindControl("gvChild") as GridView;
                gvOrders.DataSource = GetData(string.Format("SELECT NiPtMaster.NIPTSerno AS TestId, TestMasterT.TestRequestId, CASE WHEN NiPtMaster.BarcodeId IS NULL THEN TestMasterT.BID + '--Not Used' ELSE NiPtMaster.BarcodeId END AS 'Barcode', NiPtMaster.CreatedDt,TestStatusMasterL.TestStatus FROM TestStatusMasterL INNER JOIN NiPtMaster ON TestStatusMasterL.TestStatusSerno = NiPtMaster.NIPTStatus RIGHT OUTER JOIN  TestMasterT ON NiPtMaster.BarcodeId = TestMasterT.BID WHERE  (TestMasterT.TestRequestId = '{0}')", TestRequestId));
                gvOrders.DataBind();
            }
        }

        protected void Me_Link_Click(object sender, EventArgs e)
        {
            string MEID = (sender as LinkButton).CommandArgument;
            Session["MEID"] = MEID.ToString();
            Response.Redirect("Me_Detail.aspx");
        }

        protected void gvChild_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          
        }

        protected void PrintB_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Label BarcodeLbl = (Label)row.FindControl("BarcodeLbl");
            Label CreatedDt = (Label)row.FindControl("CreatedDt");
            Label patName = (Label)row.FindControl("patName");
            Label RecievedDate = (Label)row.FindControl("CreatedDt");

            List<TestMasterT> barcodeWithRelatedData = new List<TestMasterT>();

            TestMasterT testMasterT = new TestMasterT();
            testMasterT.BID = BarcodeLbl.Text;
            testMasterT.CreatedDt = Convert.ToDateTime(CreatedDt.Text);
            testMasterT.PatName = patName.Text;
            testMasterT.ReceiveDt = Convert.ToDateTime(RecievedDate.Text);
            barcodeWithRelatedData.Add(testMasterT);

            var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
            string jsFunc = "bindBarcode( '" + result + "')";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "print();", true);
        }

        //protected void gvChild_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    GridView gvChild = (GridView)sender;
        //    gvChild.PageIndex = e.NewPageIndex;
        //    gvChild.DataSource = GetData(string.Format("SELECT TestMasterT.TestRequestId,case when NiPtMaster.BarcodeId is null then 'Not Used' else NiPtMaster.BarcodeId end 'Barcode',NiPtMaster.CreatedDt  FROM TestMasterT LEFT JOIN NiPtMaster ON TestMasterT.BID = NiPtMaster.BarcodeId where TestMasterT.TestRequestId='{0}'", TestRequestId));
        //    gvChild.DataBind();
        //}
    }
}