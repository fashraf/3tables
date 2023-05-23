using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Repository
    {
        public DataTable getInstituteDetail(int InstituteId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT InstituteMasterL.InstituteName,'--' Address,CityMasterL.City,UserMaster.UserSerNo Id,OwnershipMasterL.Ownership,  InstituteTypeL.InstituteType, UserTitleMasterL.UserTitle+' '+ UserMaster.UserFullName UserName, UserMaster.Email, UserMaster.Mobile, TestStatusMasterL.TestStatusSerno StatusId,TestStatusMasterL.TestStatus,UserMaster.CreatedDt RegistrationDt FROM UserTitleMasterL INNER JOIN UserMaster ON UserTitleMasterL.UserTitleSerno = UserMaster.UserTitleId INNER JOIN OwnershipMasterL INNER JOIN InstituteMasterL ON OwnershipMasterL.OwnershipSerno = InstituteMasterL.OwnershipId INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN InstituteTypeL ON InstituteMasterL.TypeId = InstituteTypeL.InstituteTypeSerno ON UserMaster.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN TestStatusMasterL ON UserMaster.UserAccStatus = TestStatusMasterL.TestStatusSerno where UserMaster.UserSerNo=@Id", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", InstituteId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable geUserDetail(int UserId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT UserMaster.UserSerNo AS UserId, UserTitleMasterL.UserTitle, UserMaster.UserFullName, UserMaster.Email, UserMaster.Mobile, InstituteMasterL.InstituteName, CityMasterL.City,OwnershipMasterL.Ownership, UserMaster.UserAccStatus, UserMaster.CreatedDt FROM UserMaster INNER JOIN InstituteMasterL ON UserMaster.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN UserTitleMasterL ON UserMaster.UserTitleId = UserTitleMasterL.UserTitleSerno INNER JOIN OwnershipMasterL ON InstituteMasterL.OwnershipId = OwnershipMasterL.OwnershipSerno where UserMaster.UserSerNo=@Id", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", UserId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable geTestDetail(int TestId)
        {
            ///NIPT
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT DISTINCT TestMasterT.TestSerno TestSerno,NIPTSerno,TestMasterT.BID,InstituteMasterL.InstituteName,InstituteMasterL.InstituteSerno,CityMasterL.City,NiPtMaster.NationalId, NiPtMaster.PatId,NiPtMaster.Name,TestMasterT.TestId,TestMasterL.TestName, TestMasterT.SubTestId, SubTestMasterL.SubTestName,NiPtMaster.CreatedDt Dt,TestStatusMasterL.TestStatusSerno StatusId,TestStatusMasterL.TestStatus,case when NiPtMaster.RejectReasonId is null then 0 else NiPtMaster.RejectReasonId end RejectReasonId FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.BID = NiPtMaster.BarcodeId INNER JOIN InstituteMasterL ON NiPtMaster.HID = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN TestStatusMasterL ON NiPtMaster.NIPTStatus = TestStatusMasterL.TestStatusSerno where TestMasterT.TestSerno=@TestId", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable geTestAttachment(int TestId)
        {
            ///NIPT
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select UploadID,FileName,FilePathName,UploadType from FileUploads where TestId=@TestId order by UploadID desc", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable geTestResultAttachment(int TestId)
        {
            ///NIPT
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select UploadID,FileName,FilePathName,UploadType,RecordID,FileExtension from FileUploads where TestId=@TestId and UploadType='Result' order by UploadID desc", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable getHospitalKitInventory(int HID)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("Sp_TestTakenCountbyHid", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Institute", HID));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable getSubTestCost(int SubTestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT SubTestCost Cost,hasdiscount FROM SubTestMasterL where SubTestMasterSerno=@Id", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", SubTestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable getComboBySubTestId(int SubTestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT ComboSerno ComboId,ComboName ComboName FROM ComboMasterT where SubTestId=@Id", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", SubTestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable getComboDetailByComboId(int ComboId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select isPercent,Percentage,Amount from CombomasterT where ComboSerno=@Id", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", ComboId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public bool UpdateUserInfo(int UserId, int StatusId, string ApprovedUser)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("update UserMaster set UserAccStatus=@UserAccStatus,ApprovedUser=@ApprovedUser where UserSerNo=@UserSerNo", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@UserSerNo", UserId));
            command.Parameters.Add(new SqlParameter("@UserAccStatus", StatusId));
            command.Parameters.Add(new SqlParameter("@ApprovedUser", ApprovedUser));
            if (command.ExecuteNonQuery() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            con.Dispose();
            con.Close();
            con.Dispose();
        }
        public bool UpdateTestRequestStatus(int TestId, int StatusId, int RejectReasonId, string ApprovedUser)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("sp_UpdateNIPTStatus", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            command.Parameters.Add(new SqlParameter("@NIPTStatus", StatusId));
            command.Parameters.Add(new SqlParameter("@EditBy", ApprovedUser));
            if (command.ExecuteNonQuery() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        public bool CancelRequestStatus(int TestId, int? cancelStatus,string cancelReason, int? RejectReasonId, string ApprovedUser)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("sp_CancelNIPTStatus", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            command.Parameters.Add(new SqlParameter("@cancelStatus", cancelStatus));
            command.Parameters.Add(new SqlParameter("@cancelReason", cancelReason));
            command.Parameters.Add(new SqlParameter("@RejectReasonId", RejectReasonId));
            command.Parameters.Add(new SqlParameter("@EditBy", ApprovedUser));
            //command.Parameters.Add(new SqlParameter("@EditDt", DateTime.Now.ToString()));
            if (command.ExecuteNonQuery() != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        #region Get Nipt Detail by NIPTID

        public DataTable getTrasnportInfoById(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select TransportCompanyId,TrackingNumber,PickUpDate from TransportDetailT where testId=@TestId", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }

        public DataTable getNiptReportCompleteById(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select FetalRiskTypeId,FetalGenderId,Fetalfraction from NiptReportT where TestId=@TestId", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        public DataTable getNiptDetailById(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("USP_GetNiptDetail", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }

        #region
        public DataTable getBarcodeDetailByTestId(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT InstituteMasterL.InstituteSerno HID,InstituteMasterL.InstituteName, TestMasterT.BID, TestMasterT.TestId, TestMasterL.TestName+'> '+SubTestMasterL.SubTestName TestName FROM InstituteMasterL INNER JOIN TestMasterT ON InstituteMasterL.InstituteSerno = TestMasterT.InstituteId INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestCode = SubTestMasterL.SubTestMasterSerno where TestMasterT.TestSerno=@TestId", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion


        public DataTable getTestKitRequest(int RequestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT TestRequestMasterT.TestReqSerno, TestRequestMasterT.TestId, TestMasterL.TestName, SubTestMasterL.SubTestMasterSerno, SubTestMasterL.SubTestName, TestRequestMasterT.TestRequest,TestRequestMasterT.ActualRequest ,TestRequestMasterT.InstituteId,InstituteMasterL.InstituteName, CityMasterL.City, TestStatusMasterL.TestStatus,TestStatusMasterL.TestStatusSerno StatusId,TestStatusMasterL.TestColor,TestRequestMasterT.CreatedDt, TestRequestMasterT.CreatedBy, UserMaster.UserFullName, UserMaster.Email, UserMaster.Mobile FROM TestMasterL INNER JOIN TestRequestMasterT ON TestMasterL.TestMasterSerno = TestRequestMasterT.TestId INNER JOIN SubTestMasterL ON TestRequestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno INNER JOIN InstituteMasterL ON TestRequestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN CityMasterL ON InstituteMasterL.CityId = CityMasterL.CitySerno INNER JOIN TestStatusMasterL ON TestRequestMasterT.RequestStatus = TestStatusMasterL.TestStatusSerno INNER JOIN UserMaster ON TestRequestMasterT.CreatedBy = UserMaster.UserSerNo Where  (TestRequestMasterT.TestReqSerno=@Id)", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", RequestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion


        #region Enter Timeline 
        public void InsertTimelineForTest(int TestId, int? StatusId, string UserName, string MetaData, string Reason)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("insert into TestTimeLineT (TestId,StatusId,UserName,MetaData,Reason,TimelineDt) values  (@TestId,@StatusId,@UserName,@MetaData,@Reason,GetDate()) ", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId ", TestId));
            command.Parameters.Add(new SqlParameter("@StatusId ", StatusId));
            command.Parameters.Add(new SqlParameter("@UserName ", UserName));
            command.Parameters.Add(new SqlParameter("@MetaData ", MetaData));
            command.Parameters.Add(new SqlParameter("@Reason ", Reason));
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            con.Dispose();
        }

        public DataTable getTimelineByTestId(int TestId, int AppId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT TestStatusMasterL.TestStatus,case when TestTimeLineT.UserName is null then '--' else TestTimeLineT.UserName end Name,TestTimeLineT.TimelineDt dt FROM TestStatusMasterL inner JOIN TestStepMasterL ON TestStatusMasterL.TestStatusSerno = TestStepMasterL.statusid left JOIN TestTimeLineT ON TestStatusMasterL.TestStatusSerno = TestTimeLineT.StatusId and TestTimeLineT.testid=@Id and TestStepMasterL.TestName=@AppId order by TestStepMasterL.Stepnumber asc", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", TestId));
            command.Parameters.Add(new SqlParameter("@AppId ", AppId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion

        #region Workflow
        public DataTable GetTestAutoWorkFlowStatus(int TestId)
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            //Read It later///command = new SqlCommand("SELECT top 1 TestStatusMasterL.TestStatusSerno Id,TestStatusMasterL.TestStatus Status FROM TestStepMasterL INNER JOIN TestStatusMasterL ON TestStepMasterL.statusid = TestStatusMasterL.TestStatusSerno where TestStepMasterL.StepNumber > (SELECT TestStepMasterL.StepNumber FROM NiPtMaster INNER JOIN TestStepMasterL ON NiPtMaster.NIPTStatus = TestStepMasterL.statusid WHERE  (NiPtMaster.TestId = @Id)) order by TestStepMasterL.stepnumber asc", con);
            command = new SqlCommand("SELECT top 1 TestStatusMasterL.TestStatusSerno Id,TestStatusMasterL.TestStatus Status FROM TestStepMasterL INNER JOIN TestStatusMasterL ON TestStepMasterL.statusid = TestStatusMasterL.TestStatusSerno left JOIN NovoLabAdmin.dbo.RoleStatusMasterT urs on TestStepMasterL.statusid=urs.StatusId where TestStepMasterL.StepNumber > (SELECT TestStepMasterL.StepNumber FROM NiPtMaster INNER JOIN TestStepMasterL ON NiPtMaster.NIPTStatus = TestStepMasterL.statusid WHERE  (NiPtMaster.TestId = @TestId)) and urs.RoleId=@UserId order by TestStepMasterL.stepnumber asc ", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            command.Parameters.Add(new SqlParameter("@TestId ", TestId));
            command.Parameters.Add(new SqlParameter("@UserId ", 1));
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion


        #region ReadUnreadNoti
        public void NotiRead(int NotiId, int UserId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("insert into TestTimeLineT (noti_id,userid,noti_read_dt) values  (@noti_id,@userid,@Reason,GetDate()) ", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@noti_id ", NotiId));
            command.Parameters.Add(new SqlParameter("@userid ", UserId));
            command.ExecuteNonQuery();
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        #endregion


        #region Comment
        public bool InsertComment(int TestId, string Comment,int CreatedId, string CreatedName)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("insert into CommentMasterT (TestId,Comment,Dt,CreatedId,CreatedName,CommentStatus) values  (@TestId,@Comment,GetDate(),@CreatedId,@CreatedName,1) ", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId ", TestId));
            command.Parameters.Add(new SqlParameter("@Comment ", Comment));
            command.Parameters.Add(new SqlParameter("@CreatedId ", CreatedId));
            command.Parameters.Add(new SqlParameter("@CreatedName ", CreatedName));
            if (command.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            command.Dispose();
            con.Close();
            con.Dispose();
        }


        public DataTable getCommentByTestId(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT ComentSerno,Comment,Dt,CreatedName  FROM  CommentMasterT where CommentStatus=1 and TestId=@TestId order by ComentSerno desc", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion

        #region GetBarcodeInstituute
        public DataTable geInstituteBarcode(int TestSerno)
        {
            ///NIPT
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT distinct TestMasterT.TestSerno,TestMasterT.BID,InstituteMasterL.InstituteSerno InstituteId,TestMasterL.TestMasterSerno TestId,SubTestMasterL.SubTestMasterSerno SubtestId,case when TestMasterT.Completed=1 then 'Completed' else 'Available' end CompletedStatus,case when TestMasterT.Completed=1 then 'True' else 'False' end Completed FROM  TestMasterT inner JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno where TestMasterT.TestSerno=@TestSerno", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestSerno", TestSerno));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }


        public bool UpdateTestBarcode(int TestSerno,int TestId, int SubTest, int InstituteId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("update TestMasterT set TestId=@TestId,SubTestId=@SubTestId,InstituteId=@InstituteId where TestSerno=@TestSerno", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestSerno", TestSerno));
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            command.Parameters.Add(new SqlParameter("@SubTestId", SubTest));
            command.Parameters.Add(new SqlParameter("@InstituteId", InstituteId));
            if (command.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        #endregion

        #region PatientInfo
        public DataTable getPatientDetail(int PatientId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT PatientMaster.PatientSerno, PatientMaster.NationalId, PatientMaster.MRN,REPLACE(CONCAT( PatientMaster.FirstName+' ',PatientMaster.MiddleName+' ',PatientMaster.LastName+' '),'  ',' ') FullName, PatientMaster.DOB,cast((DATEDIFF(M, DOB, GETDATE())/12) as varchar) ActualAge, PatientMaster.GenderId, GenderMasterL.Gender,PatientMaster.Email, PatientMaster.Mobile, PatientMaster.Address, PatientMaster.HID, PatientMaster.CityId, CityMasterL.City, EthnicBackgroundL.EthnicBackgroundSerno,EthnicBackgroundL.EthnicBackground,case when PatientMaster.IsSaudi=1 then 'Saudi' Else 'Non Saudi' end as IsSaudi  FROM PatientMaster INNER JOIN GenderMasterL ON PatientMaster.GenderId = GenderMasterL.GenderSerno left JOIN EthnicBackgroundL ON PatientMaster.EthnicBackgroundId = EthnicBackgroundL.EthnicBackgroundSerno LEFT OUTER JOIN CityMasterL ON PatientMaster.CityId = CityMasterL.CitySerno WHERE (PatientMaster.PatientSerno = @Id)", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", PatientId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion

        #region Get RecordId from TestId
        public DataTable GetRecordIdFromFileUpload(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select top 1 RecordID from  Fileuploads where TestId= @Id", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion

        #region If RecordId is Read
        public bool CheckIfHospitalReadReport(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open(); SqlCommand command = null;
            command = new SqlCommand(" select distinct TestId from ReportViewLogT where TestId= @Id", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@Id ", TestId));
            SqlDataReader reader = (command.ExecuteReader());
            if(reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
            command.Dispose();
            con.Close();
            con.Dispose();
        }
        #endregion

        #region Get  If Reordered
        public DataTable GetReorderedRecord(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open(); SqlCommand command = null; DataTable results = new DataTable();
            command = new SqlCommand("SELECT TestMasterT.BID,TestMasterT.TestSerno FROM NiPtMaster INNER JOIN TestMasterT ON NiPtMaster.RejectId = TestMasterT.TestSerno WHERE  (NiPtMaster.TestId = @TestId)", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }
        #endregion
    }
}