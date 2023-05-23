using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class KitRequestBarcode
    {
        public string CreateBarcodeReqest(int TestKitReqestId, int TestId, int SubtestId, int TyeId, int InstitteId, int ActualTestRequest, string CreatedBy)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("Insert into TestMasterT(TestRequestId,TestId,SubTestId,TypeId,InstituteId,PatientId,CreatedDt,CreatedBy,ReceiveDt) values (@TestRequestId,@TestId,@SubTestId,@TypeId,@InstituteId,@PatientId,getdate(),@CreatedBy,getdate()+5);Select Scope_Identity();", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestRequestId ", TestKitReqestId));
            command.Parameters.Add(new SqlParameter("@TestId ", TestId));
            command.Parameters.Add(new SqlParameter("@SubTestId", SubtestId));
            command.Parameters.Add(new SqlParameter("@TypeId", TyeId));
            if (TyeId == 1)
            {
                command.Parameters.Add(new SqlParameter("@InstituteId", InstitteId));
                command.Parameters.Add(new SqlParameter("@PatientId", DBNull.Value));
            }
            else if (TyeId == 2)
            {
                command.Parameters.Add(new SqlParameter("@InstituteId", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PatientId", InstitteId));
            }
            else if (TyeId == 3)
            {
                command.Parameters.Add(new SqlParameter("@InstituteId", DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PatientId", DBNull.Value));
            }

            command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
            //command.Parameters.Add(new SqlParameter("@CreatedDt", DateTime.Now.ToString()));
            var values = "";
            int CompletedTestId = 0;
            string returnvluel = "";
            for (int i = 0; i < ActualTestRequest; i++)
            {
                CompletedTestId = Convert.ToInt32(command.ExecuteScalar());
                if (CompletedTestId > 0)
                    values = values == "" ? CompletedTestId.ToString() : values + "," + CompletedTestId.ToString();
            }
            if (CompletedTestId > 0)
            {
                bool isupdated = UpdateTestKitRequest(InstitteId, ActualTestRequest, TestKitReqestId);
                if (isupdated == true)
                {
                    returnvluel = "1";
                }
                else
                {
                    returnvluel = "0";
                }

            }
            return returnvluel;
        }
        private bool UpdateTestKitRequest(int instituteid,int ActualTestRequest, int testKitReqestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("update TestRequestMasterT set ActualRequest=@ActualRequest,RequestStatus=@RequestStatus where TestReqSerno=@TestReqSerno", con);
            command.CommandType = System.Data.CommandType.Text;

            command.Parameters.Add(new SqlParameter("@RequestStatus", 7));
            command.Parameters.Add(new SqlParameter("@ActualRequest", ActualTestRequest));
            command.Parameters.Add(new SqlParameter("@TestReqSerno", testKitReqestId));
            
            if(command.ExecuteNonQuery() >0)
            {
                return true;

                ///log
                int UID = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
                string Name = HttpContext.Current.Session["FullName"].ToString();
                string UserName = HttpContext.Current.Session["UserName"].ToString();
                int RoleId = Convert.ToInt32(HttpContext.Current.Session["RoleId"].ToString());
                string meta = Name + " Updated Status of KitRequestId : " + testKitReqestId + " to complete.";
                AppCode.Audit.auditlog(UID, UserName, "Kit Status Update", meta, RoleId, false);
            }
            else
            {
                return false;
            }
            //command.Parameters.Add(new SqlParameter("@CreatedDt", DateTime.Now.ToString()));
        }
    }
}