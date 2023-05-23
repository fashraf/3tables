using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class ListData
    {
        public static List<dto.InstituteInformation> getInstituteList()
        {
            List<dto.InstituteInformation> list = new List<dto.InstituteInformation>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "Sp_getInstituteList";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.InstituteInformation d = new dto.InstituteInformation();
                        d.ID = Convert.ToInt32(reader["Id"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.City = Convert.ToString(reader["City"]);
                        d.Ownership = Convert.ToString(reader["Ownership"]);
                        d.MainBranch = Convert.ToString(reader["MainBranch"]);
                        d.CRNumber = Convert.ToString(reader["CRNumber"]);
                        d.Status = Convert.ToString(reader["Status"]);
                        d.StatusCss = Convert.ToString(reader["StatusCss"]);
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }


        public static List<dto.NewUserRegistration> getNewRegisterationUserList()
        {
            //////User Status=1
            List<dto.NewUserRegistration> list = new List<dto.NewUserRegistration>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "SELECT UserMaster.UserSerNo AS UserId, UserTitleMasterL.UserTitle, UserMaster.UserFullName, UserMaster.Email, UserMaster.Mobile, InstituteMasterL.InstituteName, CityMasterL.City, UserMaster.CreatedDt,TestStatusMasterL.TestStatus UserAccStatus,TestStatusMasterL.TestColor FROM UserMaster INNER JOIN InstituteMasterL ON UserMaster.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN UserTitleMasterL ON UserMaster.UserTitleId = UserTitleMasterL.UserTitleSerno INNER JOIN TestStatusMasterL ON UserMaster.UserAccStatus = TestStatusMasterL.TestStatusSerno WHERE  (UserMaster.UserAccStatus in (9,10,11)) order by UserMaster.UserSerNo desc";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.NewUserRegistration d = new dto.NewUserRegistration();
                        d.UserID = Convert.ToInt32(reader["UserId"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.InstituteCity = Convert.ToString(reader["City"]);
                        d.UserTitle = Convert.ToString(reader["UserTitle"]);
                        d.UserFullName = Convert.ToString(reader["UserFullName"]);
                        d.UserMobile = Convert.ToString(reader["Mobile"]);
                        d.UserEmail = Convert.ToString(reader["Email"]);
                        d.UserStatus = Convert.ToString(reader["UserAccStatus"]);
                        d.CreatedDate = Convert.ToDateTime(reader["CreatedDt"]).ToString();//Convert.ToString(reader["CreatedDt"]);
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }


        #region

        public static DataTable getTrasnportTestList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT TestMasterT.TestSerno, NiPtMaster.NIPTSerno AS NIPTID, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, InstituteMasterL.InstituteName, NiPtMaster.NationalId, NiPtMaster.PatientMRN,NiPtMaster.Name, NiPtMaster.Mobile, NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus, TestStatusMasterL.TestColor, TransportDetailT.TransportCompanyId, TransportCompanyL.TransportCompany,TransportDetailT.TrackingNumber, TransportDetailT.PickUpDate FROM TransportDetailT left JOIN TransportCompanyL ON TransportDetailT.TransportCompanyId = TransportCompanyL.TransportCompanySerno right JOIN TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno ON TransportDetailT.TestId = TestMasterT.TestSerno  WHERE (TestStatusMasterL.TestStatusSerno  between 1 and 2)  order by NiptMaster.CreatedDt desc", con);
            command.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }

        public static List<dto.TestRegistrationList> getTestRequestListByStatus(string status)
        {
            List<dto.TestRegistrationList> list = new List<dto.TestRegistrationList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            //string query = "SELECT TestMasterT.TestSerno, TestMasterT.TestSerno TestId, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName,InstituteMasterL.InstituteName, NiPtMaster.NationalId, NiPtMaster.PatientMRN, NiPtMaster.Name, NiPtMaster.Mobile,NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno WHERE (TestStatusMasterL.TestStatusSerno = @status) order by TestMasterT.TestSerno desc";
            string query = "SELECT TestMasterT.TestSerno,TestMasterT.TestSerno TestId, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, InstituteMasterL.InstituteName, NiPtMaster.NationalId, NiPtMaster.PatientMRN,NiPtMaster.Name, NiPtMaster.Mobile, NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus, TestStatusMasterL.TestColor,TransportDetailT.TransportCompanyId,case when TransportCompanyL.TransportCompany is null then '--' else TransportCompanyL.TransportCompany end TransportCompany,case when TransportDetailT.TrackingNumber is null then '--' else TransportDetailT.TrackingNumber end TrackingNumber,ISNULL(CONVERT(VARCHAR(30),TransportDetailT.PickUpDate,6),'--') as PickUpDate FROM TransportDetailT left JOIN TransportCompanyL ON TransportDetailT.TransportCompanyId = TransportCompanyL.TransportCompanySerno right JOIN TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno ON TransportDetailT.TestId = TestMasterT.TestSerno  WHERE (TestStatusMasterL.TestStatusSerno in (@status)) order by NiPtMaster.CreatedDt desc";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new SqlParameter("@status", status));
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.TestRegistrationList d = new dto.TestRegistrationList();
                        d.TestSerno = Convert.ToInt32(reader["TestSerno"]);
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.Barcode = Convert.ToString(reader["Barcode"]);
                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.Name = Convert.ToString(reader["Name"]);
                        d.NationalId = Convert.ToString(reader["NationalId"]);
                        d.Mobile = Convert.ToString(reader["Mobile"]);
                        d.CreatedDt = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                        d.TestStatus = Convert.ToString(reader["TestStatus"]);
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        d.TransportCompany = Convert.ToString(reader["TransportCompany"]);
                        d.TrackingNumber = Convert.ToString(reader["TrackingNumber"]);
                        d.PickUpDate = reader["PickUpDate"].ToString();
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }


        public static List<dto.TestRegistrationList> getTestResultRequestListByStatus()
        {
            List<dto.TestRegistrationList> list = new List<dto.TestRegistrationList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            //string query = "SELECT TestMasterT.TestSerno, TestMasterT.TestSerno TestId, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName,InstituteMasterL.InstituteName, NiPtMaster.NationalId, NiPtMaster.PatientMRN, NiPtMaster.Name, NiPtMaster.Mobile,NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno WHERE (TestStatusMasterL.TestStatusSerno = @status) order by TestMasterT.TestSerno desc";
            string query = "SELECT TestMasterT.TestSerno,TestMasterT.TestSerno TestId, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, InstituteMasterL.InstituteName, NiPtMaster.NationalId, NiPtMaster.PatientMRN,NiPtMaster.Name, NiPtMaster.Mobile, NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus, TestStatusMasterL.TestColor,TransportDetailT.TransportCompanyId,case when TransportCompanyL.TransportCompany is null then '--' else TransportCompanyL.TransportCompany end TransportCompany,case when TransportDetailT.TrackingNumber is null then '--' else TransportDetailT.TrackingNumber end TrackingNumber,ISNULL(CONVERT(VARCHAR(30),TransportDetailT.PickUpDate,6),'--') as PickUpDate FROM TransportDetailT left JOIN TransportCompanyL ON TransportDetailT.TransportCompanyId = TransportCompanyL.TransportCompanySerno right JOIN TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno ON TransportDetailT.TestId = TestMasterT.TestSerno  WHERE (TestStatusMasterL.TestStatusSerno in (17,18)) order by NiPtMaster.CreatedDt desc";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.TestRegistrationList d = new dto.TestRegistrationList();
                        d.TestSerno = Convert.ToInt32(reader["TestSerno"]);
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.Barcode = Convert.ToString(reader["Barcode"]);
                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.Name = Convert.ToString(reader["Name"]);
                        d.NationalId = Convert.ToString(reader["NationalId"]);
                        d.Mobile = Convert.ToString(reader["Mobile"]);
                        d.CreatedDt = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                        d.TestStatus = Convert.ToString(reader["TestStatus"]);
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        d.TransportCompany = Convert.ToString(reader["TransportCompany"]);
                        d.TrackingNumber = Convert.ToString(reader["TrackingNumber"]);
                        d.PickUpDate = reader["PickUpDate"].ToString();
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }

        public static List<dto.TestRegistrationList> getTestRequestList()
        {
            List<dto.TestRegistrationList> list = new List<dto.TestRegistrationList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "SELECT DISTINCT NiPtMaster.TestId, TestMasterT.BID AS Barcode, TestMasterL.TestName, SubTestMasterL.SubTestName, NiPtMaster.NationalId, NiPtMaster.PatientMRN, NiPtMaster.Name, NiPtMaster.Mobile, NiPtMaster.CreatedDt, TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor, case when RejectId is null then '' else 'Re-Ordered' end as RejectStatus,CASE WHEN RejectId IS NULL THEN '../assets/images/icon/Regular.png' ELSE '../assets/images/icon/ReOrdered.png' END AS RejectImg, InstituteMasterL.InstituteName FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno  order by NiPtMaster.CreatedDt desc";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.TestRegistrationList d = new dto.TestRegistrationList();
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.Barcode = Convert.ToString(reader["Barcode"]);
                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.Name = Convert.ToString(reader["Name"]);
                        d.NationalId = Convert.ToString(reader["NationalId"]);
                        d.Mobile = Convert.ToString(reader["Mobile"]);
                        d.CreatedDt = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                        d.TestStatus = Convert.ToString(reader["TestStatus"]);
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        d.RejectStatus = Convert.ToString(reader["RejectStatus"]);
                        d.RejectImg = Convert.ToString(reader["RejectImg"]);
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }

        #endregion


        public static List<dto.TestKitsRequestList> getTestKitRequestList()
        {
            List<dto.TestKitsRequestList> list = new List<dto.TestKitsRequestList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "SELECT TestRequestMasterT.TestReqSerno RequestId,TestRequestMasterT.TestId, TestMasterL.TestName, SubTestMasterL.SubTestMasterSerno SubTestId, SubTestMasterL.SubTestName,TestRequestMasterT.TestRequest,case when TestRequestMasterT.ActualRequest is null then CONVERT(varchar(100), '---') else CONVERT(varchar(100), TestRequestMasterT.ActualRequest) end ActualRequest , TestRequestMasterT.InstituteId, InstituteMasterL.InstituteName, CityMasterL.City,TestStatusMasterL.TestStatus,TestStatusMasterL.TestColor,TestRequestMasterT.CreatedDt FROM TestMasterL INNER JOIN TestRequestMasterT ON TestMasterL.TestMasterSerno = TestRequestMasterT.TestId INNER JOIN SubTestMasterL ON TestRequestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN InstituteMasterL ON TestRequestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN TestStatusMasterL ON TestRequestMasterT.RequestStatus = TestStatusMasterL.TestStatusSerno  order by TestRequestMasterT.TestReqSerno desc";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.TestKitsRequestList d = new dto.TestKitsRequestList();
                        d.RequestId = Convert.ToInt32(reader["RequestId"]);
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestId = Convert.ToInt32(reader["SubTestId"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.RequestedTest = Convert.ToInt32(reader["TestRequest"]);
                        d.ActualTest = Convert.ToString(reader["ActualRequest"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.City = Convert.ToString(reader["City"]);
                        d.Status = Convert.ToString(reader["TestStatus"]);
                        d.CreatedDt = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }


        public static List<dto.InstituteBarList> getInstituteBarcodeList()
        {
            List<dto.InstituteBarList> list = new List<dto.InstituteBarList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "SELECT distinct TestMasterT.TestSerno 'TestId',TestMasterT.BID,InstituteMasterL.InstituteName,TestMasterL.TestMasterSerno TestId,TestMasterL.TestName,SubTestMasterL.TestCodeId SubtestId,SubTestMasterL.SubTestName, TestMasterT.TestId, TestMasterT.SubTestId,case when TestMasterT.Completed=1 then 'Completed' else 'Available' end CompletedStatus,TestMasterT.Completed,TestMasterT.CreatedDt FROM  TestMasterT inner JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.InstituteBarList d = new dto.InstituteBarList();
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.BID = Convert.ToString(reader["BID"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);

                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.CompletedStatus = Convert.ToString(reader["CompletedStatus"]);
                        d.CreatedDt = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }



        public static List<dto.PatientList> GetPatientList()
        {
            List<dto.PatientList> list = new List<dto.PatientList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            DataTable results = new DataTable();
            con.Open();
            string query = "SELECT ROW_NUMBER() OVER (ORDER BY PatientMaster.PatientSerno) SerId, PatientMaster.PatientSerno as PatId, PatientMaster.NationalId,PatientMaster.MRN,REPLACE(CONCAT(PatientMaster.FirstName+' ',PatientMaster.MiddleName+' ',PatientMaster.LastName+' '),'  ',' ') FullName, PatientMaster.DOB 'DOB', GenderMasterL.Gender, CityMasterL.City,PatientMaster.Email, PatientMaster.Mobile,case when PatientMaster.IsSaudi=1 then 'Saudi' Else 'Non Saudi' end as IsSaudi FROM PatientMaster INNER JOIN GenderMasterL ON PatientMaster.GenderId = GenderMasterL.GenderSerno INNER JOIN CityMasterL ON PatientMaster.CityId = CityMasterL.CitySerno WHERE  PatientMaster.HID=@hid";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.Add(new SqlParameter("@hid", 100));
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.PatientList d = new dto.PatientList();
                        d.PatId = Convert.ToInt32(reader["PatId"]);
                        d.FullName = Convert.ToString(reader["FullName"]);
                        d.NationalId = Convert.ToString(reader["NationalId"]);
                        d.Mobile = Convert.ToString(reader["Mobile"]);
                        d.Email = Convert.ToString(reader["Email"]);
                        d.Gender = Convert.ToString(reader["Gender"]);
                        d.City = Convert.ToString(reader["City"]);
                        d.IsSaudi = Convert.ToString(reader["IsSaudi"]); 
                        d.DOB = Convert.ToDateTime(reader["DOB"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                     
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }
        public static List<dto.TestRegistrationList> GetTestListByPatId(int PatId)
        {
            List<dto.TestRegistrationList> list = new List<dto.TestRegistrationList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "SELECT DISTINCT TestMasterT.TestSerno TestId, TestMasterT.BID AS Barcode,TestMasterT.PatientId, TestMasterL.TestName,SubTestMasterL.SubTestName,TestMasterT.CreatedDt,case when TestStatusMasterL.TestStatus is null then 'Test Not Added' else TestStatusMasterL.TestStatus end TestStatus,TestStatusMasterL.TestColor, InstituteMasterL.InstituteName FROM TestMasterT left JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId left JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno left JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno left JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno left JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno where TestMasterT.PatientId=@pid order by TestMasterT.CreatedDt desc ";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.Parameters.Add(new SqlParameter("@pid", PatId));
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.TestRegistrationList d = new dto.TestRegistrationList();
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.Barcode = Convert.ToString(reader["Barcode"]);
                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.CreatedDt = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                        d.TestStatus = Convert.ToString(reader["TestStatus"]);
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }


        public static List<dto.TestTatList> GetTestTat()
        {
            List<dto.TestTatList> list = new List<dto.TestTatList>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            string query = "NIPTTestListWithTAT";
            using (SqlCommand command = new SqlCommand(query, con))
            {
                command.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dto.TestTatList d = new dto.TestTatList();
                        d.TestId = Convert.ToInt32(reader["TestId"]);
                        d.Barcode = Convert.ToString(reader["Barcode"]);
                        d.TestName = Convert.ToString(reader["TestName"]);
                        d.SubTestName = Convert.ToString(reader["SubTestName"]);
                        d.InstituteName = Convert.ToString(reader["InstituteName"]);
                        d.Name = Convert.ToString(reader["Name"]);
                        d.SampleCollectionDT = Convert.ToString(reader["SampleCollectionDT"]);
                        d.TestColor = Convert.ToString(reader["TestColor"]);
                        d.TestStatus = Convert.ToString(reader["TestStatus"]);
                        d.RecievedatNOVO = Convert.ToString(reader["RecievedatNOVO"]);
                        d.StatusStyle = Convert.ToString(reader["StatusStyle"]);
                        d.ActalStatus = Convert.ToString(reader["ActalStatus"]);
                        d.DaysLeft = Convert.ToString(reader["DaysLeft"]);
                        d.DaysInNovo = Convert.ToString(reader["DaysInNovo"]);
                        //d.instituteStatus = Convert.ToDateTime(reader["CreatedDt"]).ToString("dd.MMM.yyyy hh:mm tt");//Convert.ToString(reader["CreatedDt"]);
                        //d.CreatedDate = "Active";
                        list.Add(d);
                    }
                }
                command.Dispose();
                con.Close();
                con.Dispose();
            }
            return list;
        }



        /// <summary>
        /// InPatient SubTest Cost List 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetInPatSubTestCostList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("Sp_InPatSubTestCostList", con);
            command.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
    }
}
