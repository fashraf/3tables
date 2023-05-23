using InternalLims.AppCode;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static InternalLims.AppCode.dto;

namespace InternalLims.Main
{
    public partial class CreateNewBarcode : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Audit Audit = new AppCode.Audit();
        static string constr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadData();
                        alert.Visible = false;
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed NIPT List";
                        Audit.auditlog(UID, UserName, "NIPT List", meta, RoleId, false);
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

        private void LoadData()
        {
            DataTable getdata = drop.GetInstituteList();
            drop.FillDropDownList(InstituteDrop, getdata, "Name", "Id");
            TestDrop.DataSource = drop.GetTestList();
            TestDrop.DataBind();
            DataTable gettestlist = drop.GetTestList();
            drop.FillDropDownList(TestDrop, gettestlist, "TestName", "Id");
        }


        protected void TestDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TestId = Convert.ToInt32(TestDrop.SelectedValue);
            DataTable getsubtest = drop.GetSubTestList(TestId);
            drop.FillDropDownList(SubTestDrop, getsubtest, "SubTestName", "Id");
            string jsFunc = " bindPrinters()";
            // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);
        }

        [System.Web.Services.WebMethod]
        public static string Insertdata(string Test, string subtest, string TypeDrop, string InstituteDrop, string TotalTxt)
        {
            //return "Hello " + Test + Environment.NewLine + "The Current Time is: "
            //    + DateTime.Now.ToString();
            string jsFunc = "";
            try
            {
                int testid = Convert.ToInt32(Test);
                int subtestid = Convert.ToInt32(subtest);
                int TypeId = Convert.ToInt32(1);
                int instituteid = Convert.ToInt32(InstituteDrop);
                int Total = Convert.ToInt32(TotalTxt);

                Connection Con = new Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand command = new SqlCommand("Insert into TestMasterT(TestId,SubTestId,SubTestCode,TypeId,InstituteId,PatientId,CreatedDt,CreatedBy,ReceiveDt) values (@TestId,@SubTestId,@SubTestCode,@TypeId,@InstituteId,@PatientId,getdate(),@CreatedBy,getdate());Select Scope_Identity();", con);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new SqlParameter("@TestId ", testid));
                command.Parameters.Add(new SqlParameter("@SubTestId", subtestid));
                command.Parameters.Add(new SqlParameter("@SubTestCode", 1));
                command.Parameters.Add(new SqlParameter("@TypeId", TypeId));
                if (TypeId == 1)
                {
                    command.Parameters.Add(new SqlParameter("@InstituteId", instituteid));
                    command.Parameters.Add(new SqlParameter("@PatientId", DBNull.Value));
                }
                else if (TypeId == 2)
                {
                    command.Parameters.Add(new SqlParameter("@InstituteId", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@PatientId", instituteid));
                }
                else if (TypeId == 3)
                {
                    command.Parameters.Add(new SqlParameter("@InstituteId", DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@PatientId", DBNull.Value));
                }

                command.Parameters.Add(new SqlParameter("@CreatedBy", "Test"));
                var values = "";
                for (int i = 0; i < Total; i++)
                {
                    int CompletedTestId = Convert.ToInt32(command.ExecuteScalar());
                    if (CompletedTestId > 0)
                        values = values == "" ? CompletedTestId.ToString() : values + "," + CompletedTestId.ToString();
                }
                command = new SqlCommand();
                string query = "SELECT TestMasterT.TestSerno, TestMasterT.TestRequestId, TestMasterT.TestId, TestMasterT.SubTestCode, TestMasterT.SubTestId, TestMasterT.TypeId, TestMasterT.InstituteId, TestMasterT.PatientId,case when NiPtMaster.CreatedDt is null then '' else NiPtMaster.CreatedDt end CreatedDt,TestMasterT.CreatedBy, TestMasterT.BID, TestMasterT.SID, TestMasterT.ReceiveDt, TestMasterT.Completed, PM.FirstName AS PName FROM NiPtMaster right JOIN PatientMaster AS PM ON NiPtMaster.PatId = PM.PatientSerno right JOIN TestMasterT ON NiPtMaster.TestId = TestMasterT.TestSerno where TestSerno in(" + values + ")";
                command = new SqlCommand(query, con);

                List<dto.TestMasterT> barcodeWithRelatedData = new List<dto.TestMasterT>();

                using (command = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dto.TestMasterT testMasterT = new dto.TestMasterT();
                            testMasterT.BID = reader["BID"].ToString();
                            testMasterT.TestSerno = Convert.ToInt32(reader["TestSerno"]);
                            testMasterT.PatName = Convert.ToString(reader["PName"]);
                            testMasterT.CreatedBy = reader["CreatedBy"].ToString();
                            testMasterT.TestId = Convert.ToInt32(reader["TestId"]);
                            testMasterT.CreatedDt = (DateTime)reader["CreatedDt"];
                            var rec = reader["ReceiveDt"];
                            testMasterT.ReceiveDt = (DateTime)reader["ReceiveDt"];
                            barcodeWithRelatedData.Add(testMasterT);
                        }
                    }
                }

                con.Dispose();
                con.Close();
                command.Dispose();
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn4", "alert('Data Saved Succeessfully')", true);

                ///log
                int UID = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                string Name = HttpContext.Current.Session["FullName"].ToString();
                string UserName = HttpContext.Current.Session["UserName"].ToString();
                int RoleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"].ToString());
                string meta = Name + " Added " + TotalTxt + " Barcode for Hospital Id: " + instituteid;
                AppCode.Audit.auditlog(UID, UserName, "Create Barcode", meta, RoleId, false);
                //alert.Visible = true;


                var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
                // jsFunc = "bindBarcode( '" + result + "')";

                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn", jsFunc, true);
                return result;
            }
            catch (Exception ex)
            {
                jsFunc = "Alert( '" + ex.Message.ToString() + "')";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "error", jsFunc, true);
            }
            return jsFunc;
        }

  
        [WebMethod]
        //public static string AddLogs()
        public static void AddLogs(List<dto.BarCodeLogs> barCodeLogs)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand();

            barCodeLogs.ForEach(log =>
            {
                try
                {
                    command = new SqlCommand("Insert into BarCodeLogs(TestSemo,BID,ErrorMessage,IPAddress) values (@TestSemo,@BID,@ErrorMessage,@IPAddress);", con);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@TestSemo", log.TestSemo));
                    command.Parameters.Add(new SqlParameter("@BID", log.BID));
                    command.Parameters.Add(new SqlParameter("@ErrorMessage", log.ErrorMesssage));
                    command.Parameters.Add(new SqlParameter("@IPAddress", log.IPAddress));
                    command.ExecuteScalar();
                }
                catch (Exception ex)
                {


                }
            });
            command.Dispose();
            con.Close();
            con.Dispose();
        }
    }
}