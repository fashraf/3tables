using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.handler.NIPT
{
    /// <summary>
    /// Summary description for PrintBarcode
    /// </summary>
    public class PrintBarcode : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            int RequestId = Convert.ToInt32(context.Request.Params["Id"]);
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            //  command = new SqlCommand("select * from TestMasterT where TestSerno=@RequestId ", con);
            command = new SqlCommand();
            //string query = " select TestMasterT.*, (PM.FirstName + ' ' + PM.LastName) as PName from TestMasterT left join PatientMaster PM on PM.PatientSerno = TestMasterT.PatientId where TestSerno in (" + RequestId+") ";
            //string query = "SELECT TestMasterT.TestSerno TestId,TestMasterT.BID,PatientMaster.FirstName +' '+PatientMaster.LastName PName,NiPtMaster.CratedBy, NiPtMaster.CreatedDt, NiPtMaster.InLabDt FROM     NiPtMaster INNER JOIN TestMasterT ON NiPtMaster.TestId = TestMasterT.TestSerno INNER JOIN PatientMaster ON NiPtMaster.PatId = PatientMaster.PatientSerno where TestMasterT.TestSerno = (" + RequestId + ") ";
            string query = " select TestMasterT.*, '12' as  PName from TestMasterT left join PatientMaster PM on PM.PatientSerno = TestMasterT.PatientId where TestSerno in (" + RequestId + ") ";
            command = new SqlCommand(query, con);
            List<AppCode.dto.TestMasterT> barcodeWithRelatedData = new List<AppCode.dto.TestMasterT>();

            using (command = new SqlCommand(query, con))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AppCode.dto.TestMasterT testMasterT = new AppCode.dto.TestMasterT();

                        //testMasterT.BID = reader["BID"].ToString();
                        //testMasterT.PatName = Convert.ToString(reader["PName"]);
                        ////testMasterT.TestSerno = Convert.ToInt32(reader["TestSerno"]);
                        //testMasterT.CreatedBy = reader["CratedBy"].ToString();
                        //testMasterT.TestId = Convert.ToInt32(reader["TestId"]);
                        //testMasterT.CreatedDt = (DateTime)reader["CreatedDt"];
                        //var rec = reader["InLabDt"];
                        //// testMasterT.ReceiveDt = (DateTime)reader["ReceiveDt"];
                        //barcodeWithRelatedData.Add(testMasterT);
                        testMasterT.BID = reader["BID"].ToString();
                        testMasterT.PatName = Convert.ToString(reader["PName"]);
                        //testMasterT.TestSerno = Convert.ToInt32(reader["TestSerno"]);
                        testMasterT.CreatedBy = reader["CreatedBy"].ToString();
                        testMasterT.TestId = Convert.ToInt32(reader["TestId"]);
                        testMasterT.CreatedDt = (DateTime)reader["CreatedDt"];
                        var rec = reader["ReceiveDt"];
                        // testMasterT.ReceiveDt = (DateTime)reader["ReceiveDt"];
                        barcodeWithRelatedData.Add(testMasterT);
                    }
                }
            }
            con.Dispose();
            con.Close();
            con.Dispose();
            var result = JsonConvert.SerializeObject(barcodeWithRelatedData);
            context.Response.Write(JsonConvert.SerializeObject(barcodeWithRelatedData));
            //return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}