using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.NIPTdto;

namespace InternalLims.Main
{
    public partial class NiptList : AppCode.Base
    {
        string Institute = string.Empty;
        string Test = string.Empty;
        string Status = string.Empty;
        string Barcode = string.Empty;
        string DateRange = string.Empty;
        string final = string.Empty;
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Audit Audit = new AppCode.Audit();
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
                        LoadGrid();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed NIPT List";
                        AppCode.Audit.auditlog(UID, UserName, "NIPT List", meta, RoleId, false);
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

            DataTable getstatus = drop.GetTestStatus();
            drop.FillDropDownList(Status_Drop, getstatus, "Status", "Id");

            //NiptListGrid.DataSource = AppCode.ListData.getTestRequestList();
            //NiptListGrid.DataBind();
        }
        protected void NewTestListGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //NiptListGrid.PageIndex = e.NewPageIndex;
            //LoadNewTestList();
            LoadGrid();
        }

        protected void NewTestListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                ImageButton lnkView = (ImageButton)e.CommandSource;
                string commandArgs = e.CommandArgument.ToString();
                string Id = commandArgs;
                Response.Redirect("NIPTView?Id=" + Id + "");
            }
        }
        protected void Search_Btn_Click(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            try
            {
                //string query = "SELECT DISTINCT NiPtMaster.TestId, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, NiPtMaster.NationalId, NiPtMaster.PatientMRN, NiPtMaster.Name, NiPtMaster.Mobile, NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor,  case when RejectId is null then '' else 'Re-Ordered' end as RejectStatus,CASE WHEN RejectId IS NULL THEN '../assets/images/icon/Regular.png' ELSE '../assets/images/icon/ReOrdered.png' END AS RejectImg,InstituteMasterL.InstituteName FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON NiPtMaster.HID = InstituteMasterL.InstituteSerno ";
                string query = "SELECT DISTINCT NiPtMaster.TestId,NiPtMaster.NIPTSerno NIPTID, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, NiPtMaster.NationalId,case when  NiPtMaster.PatientMRN=0 then'--' else NiPtMaster.PatientMRN end PatientMRN , NiPtMaster.Name PatName, NiPtMaster.Mobile, NiPtMaster.CreatedDt, case when RejectId is null then '' else 'Re-Ordered' end as RejectStatus,CASE WHEN RejectId IS NULL THEN '../assets/images/icon/Regular.png' ELSE '../assets/images/icon/ReOrdered.png' END AS RejectImg,CASE WHEN NiPtMaster.cancelStatus in (6,4) THEN   CancelStatusL.TestStatus when NiPtMaster.cancelStatus =12 THEN   TestStatusMasterL.TestStatus when (NiPtMaster.cancelStatus is null and  TestStatusMasterL.TestStatusSerno in(13,14,15,17)) THEN   'In NOVO LAB' when (NiPtMaster.cancelStatus is null and  TestStatusMasterL.TestStatusSerno in(18)) THEN   'Report Ready' else TestStatusMasterL.TestStatus END AS HospitalStatus,CASE WHEN NiPtMaster.cancelStatus in (6,4) THEN   'btn btn-danger btn-sm' when (NiPtMaster.cancelStatus =12 or NiPtMaster.NIPTStatus=12) THEN   'dripicons-cross btn btn-danger btn-sm' when (NiPtMaster.cancelStatus is null and  NiPtMaster.NIPTStatus in(3)) THEN 'btn btn-secondary btn-sm' when (NiPtMaster.cancelStatus is null and  TestStatusMasterL.TestStatusSerno in(13,14,15,17)) THEN 'btn btn-success btn-sm' when (NiPtMaster.cancelStatus is null and  TestStatusMasterL.TestStatusSerno in(18)) THEN   'btn btn-dark btn-sm' else 'btn btn-primary btn-sm' END AS HospitalCSS,CASE WHEN NiPtMaster.cancelStatus in (6,14) THEN   CancelStatusL.TestStatus +'( '+TestStatusMasterL.TestStatus +'  )' when NiPtMaster.cancelStatus =12 THEN   TestStatusMasterL.TestStatus when (NiPtMaster.cancelStatus is null and  TestStatusMasterL.TestStatusSerno in(13,14,15,17)) THEN   'In NOVO LAB'when (NiPtMaster.cancelStatus is null and  TestStatusMasterL.TestStatusSerno in(18)) THEN   'Report Ready'else TestStatusMasterL.TestStatus END AS NOVOStatus,TestStatusMasterL.TestColor, InstituteMasterL.InstituteName, NiPtMaster.cancelStatus FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON NiPtMaster.HID = InstituteMasterL.InstituteSerno left JOIN TestStatusMasterL AS CancelStatusL ON NiPtMaster.cancelStatus = CancelStatusL.TestStatusSerno left join RejectMasterL on NiPtMaster.RejectReasonId=RejectMasterL.RejectSerno ";
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

                if (Institute_Drop.SelectedValue != "-1")
                {
                    Institute = string.Format(" and InstituteMasterL.InstituteSerno= {0}", Institute_Drop.SelectedValue);
                }

                if (Test_Drop.SelectedValue != "-1")
                {
                    Test = string.Format(" and  TestMasterT.TestId= {0}", Test_Drop.SelectedValue);
                }
                if (Status_Drop.SelectedValue != "-1")
                {
                    Status = string.Format(" and  NiPtMaster.NiptStatus={0}", Status_Drop.SelectedValue);
                }


                final = " where  (NiPtMaster.TestId is not null) " + Barcode + DateRange + Institute + Test + Status;
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                //cmd = new SqlCommand("Sp_MeMohLoad", con);            
                //cmd.CommandType = CommandType.StoredProcedure;
                string total = query + final + " order by NiPtMaster.CreatedDt desc";
                cmd = new SqlCommand(total, con);
                cmd.CommandType = CommandType.Text;
                //cmd.Parameters.Add(new SqlParameter("@whereClause", final));
                cmd.CommandTimeout = 300;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                cmd.Connection = con;
                using (DataTable dt = new DataTable())
                {
                    sda.Fill(dt);
                    NiptListGrid.DataSource = dt;
                    NiptListGrid.DataBind();
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

        protected void Clear_Btn_Click(object sender, EventArgs e)
        {
            LoadNewTestList();
            BarcodeTxt.Text = string.Empty;
            LoadGrid();
        }

        protected void ExcelBtn_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                NiptListGrid.AllowPaging = false;
                LoadGrid();

                NiptListGrid.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in NiptListGrid.HeaderRow.Cells)
                {
                    cell.BackColor = NiptListGrid.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in NiptListGrid.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = NiptListGrid.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = NiptListGrid.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                NiptListGrid.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}