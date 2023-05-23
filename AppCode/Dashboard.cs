using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Dashboard
    {
        public DataTable getNiptDetail()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT COUNT(NIPTSerno) as 'TotalNIPT',COUNT(CASE WHEN niptstatus = 1 then 1 ELSE NULL END) as 'Submitted',COUNT(CASE WHEN niptstatus in (13,14,15) then 1 ELSE NULL END) as 'InLab',(select COUNT(*) from niptmaster where cast(CreatedDt as Date) = cast(getdate() as Date)) 'SubmittedToday' from niptmaster", con);
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

        public DataTable getRegisteredUserDetail()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT COUNT(UserAccStatus) as 'TotalUsers',COUNT(CASE WHEN UserAccStatus = 9 then 1 ELSE NULL END) as 'NewUserRegistration',COUNT(CASE WHEN UserAccStatus = 10 then 1 ELSE NULL END) as 'UserActive',COUNT(CASE WHEN UserAccStatus =11 then 1 ELSE NULL END) as 'UserInactive' from usermaster", con);
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


        public DataTable getKitsRequestList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select count(*) 'Total',sum(case when RequestStatus=1 then 1 else 0 end )'Pending',sum(case when RequestStatus=7 then 1 else 0 end )'Completed' from TestRequestMasterT", con);
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

        public DataTable getNiptUsedNotUsed()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            //command = new SqlCommand("select count(*) 'Total',sum(case when Completed is null then 1 else 0 end )'NotUsed',sum(case when Completed=1 then 1 else 0 end )'Completed' from TestMasterT", con);
            command = new SqlCommand("SELECT sum(case when Completed is null then 1 else 0 end) 'NotUsed',sum(case when Completed=1 then 1 else 0 end) 'Used',count(TestMasterT.SubTestId)Total,case when NULLIF(sum(case when Completed is null then 1 else 0 end),0)*100/(count(TestMasterT.SubTestId)) is null then 0 else NULLIF(sum(case when Completed is null then 1 else 0 end),0)*100/(count(TestMasterT.SubTestId)) end Percentage,SubTestMasterL.SubTestName 'TestName' FROM TestMasterT inner JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno where TestCodeId=3  group by SubTestMasterL.SubTestName", con);
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

        public DataTable getNiptSubmittedByInstituteToday()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            //command = new SqlCommand("select count(*) 'Total',sum(case when Completed is null then 1 else 0 end )'NotUsed',sum(case when Completed=1 then 1 else 0 end )'Completed' from TestMasterT", con);
            command = new SqlCommand("SELECT TestMasterT.InstituteId, InstituteMasterL.InstituteName,sum(case when TestMasterT.SubTestId=14 then 1 else 0 end) 'NiptBasic',sum(case when  TestMasterT.SubTestId=15 then 1 else 0 end) 'NiptPlus',count(TestMasterT.SubTestId) Total FROM TestMasterT INNER JOIN NiPtMaster ON TestMasterT.TestSerno = NiPtMaster.TestId INNER JOIN InstituteMasterL ON TestMasterT.InstituteId = InstituteMasterL.InstituteSerno INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno where CONVERT(date, NiPtMaster.CreatedDt) =CONVERT(date, getdate()) group by TestMasterT.InstituteId, InstituteMasterL.InstituteName order by Total desc", con);
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

        public DataTable DashNumbers(string Query)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand(Query, con);
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



        #region InpatientDash
        public DataTable GetInPatDsahCount()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("select (select count(PaymentSerno) from PaymentMasterT where DATEADD(dd, 0, DATEDIFF(dd, 0, CreatedDt))=DATEADD(dd, 0, DATEDIFF(dd, 0, GetDate()))) today,(select count(PaymentSerno) from PaymentMasterT where DATEADD(dd, 0, DATEDIFF(dd, 0, CreatedDt))=DATEADD(dd, 0, DATEDIFF(dd, 0, GetDate()))-1) Yesterday,(select count(PaymentSerno) from PaymentMasterT where DATEADD(M, 0, DATEDIFF(M, 0, CreatedDt))=DATEADD(M, 0, DATEDIFF(M, 0, GetDate()))) ThisMonth,(select count(PatientSerno) from PatientMaster) TotalPatients", con);
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
        #endregion

    }
}