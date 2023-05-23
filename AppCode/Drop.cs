using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace InternalLims.AppCode
{
    public class Drop
    {
        public DropDownList FillDropDownList(DropDownList dropdownlist, DataTable dt, string datatext, string datavalue)
        {
            if (dt.Rows.Count > 0)
            {
                dropdownlist.DataSource = dt;
                dropdownlist.DataTextField = datatext;
                dropdownlist.DataValueField = datavalue;
                dropdownlist.DataBind();
                dropdownlist.Items.Insert(0, new ListItem("All", "-1"));
            }
            else if (dt.Rows.Count == 0)
            {
                dropdownlist.Items.Clear();
            }
            return dropdownlist;
        }

        public RadioButtonList FillRadioList(RadioButtonList Radiolist, DataTable dt, string datatext, string datavalue)
        {
            if (dt.Rows.Count > 0)
            {
                Radiolist.DataSource = dt;
                Radiolist.DataTextField = datatext;
                Radiolist.DataValueField = datavalue;
                Radiolist.DataBind();
            }
            else if (dt.Rows.Count == 0)
            {
                Radiolist.Items.Clear();
            }
            return Radiolist;
        }

        public DataTable GetCityList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("SELECT CitySerno Id,City from CityMasterL", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }
        public DataTable GetOwnershipList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select OwnershipSerno Id,Ownership from OwnershipMasterL", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }
        public DataTable GetTypeList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select OwnershipSerno Id,Ownership from OwnershipMasterL", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }
        public DataTable GetUserStatus()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select TestStatusSerno Id,TestStatus Status from TestStatusMasterL where TestStatusType =2 order by TestStatusType asc", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetRecievedSpecificStatus()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select TestStatusSerno Id,TestStatus Status from TestStatusMasterL where TestStatusSerno in(3,8,1009) order by TestStatusType asc", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetRejectReason()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select RejectSerno Id,RejectReason RejectReason from RejectMasterL", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetRejectReasonCatId(int CatId)
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select RejectSerno Id,RejectReason RejectReason from RejectMasterL where CatId=@CatId", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            command.Parameters.Add(new SqlParameter("@CatId ", CatId));
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetTestStatus()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select TestStatusSerno Id,TestStatus Status from TestStatusMasterL where TestStatusType =1 order by TestStatusType asc", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetTestStatusForUser()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select TestStatusSerno Id,TestStatus Status from TestStatusMasterL where TestStatusType =2 order by TestStatusType asc", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }


        public DataTable GetTestAutoWorkFlowStatus(int TestId)
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("SELECT top 1 TestStatusMasterL.TestStatusSerno Id,TestStatusMasterL.TestStatus Status FROM TestStepMasterL INNER JOIN TestStatusMasterL ON TestStepMasterL.statusid = TestStatusMasterL.TestStatusSerno where TestStepMasterL.statusid > (SELECT NIPTStatus  FROM NiPtMaster where NiPtMaster.NIPTSerno = @Id) order by TestStepMasterL.stepnumber asc", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            command.Parameters.Add(new SqlParameter("@Id ", TestId));
            // Populate a Table with the rows returned. 
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetCancelDropList()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("select TestStatusSerno Id,TestStatus Status from TestStatusMasterL where TestStatusSerno in(6,4,12) order by TestStatus asc", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        public DataTable GetTransportCompanyList()
        {
            ////in TestStatusMasterL where TestStatusType=2
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = new SqlCommand();
            command = new SqlCommand("SELECT TransportCompanySerno Id,TransportCompany FROM TransportCompanyL where TransportCompanyStatus=1", con);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            // Dispose resources used. 
            adapter.Dispose();
            command.Dispose();
            con.Dispose();
            con.Close();
            return results;
        }

        #region CraeateBarcode
        public DataTable GetTestList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT TestMasterSerno Id,TestName FROM TestMasterL", con);
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

        public DataTable GetBarcodeListByHid(int hid)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT TestMasterT.TestSerno AS Id, TestMasterT.BID + ' - '+TestMasterL.TestName+'>'+ SubTestMasterL.SubTestName BID FROM TestMasterT INNER JOIN TestMasterL ON TestMasterT.TestId = TestMasterL.TestMasterSerno INNER JOIN SubTestMasterL ON TestMasterT.SubTestId = SubTestMasterL.SubTestMasterSerno WHERE (TestMasterT.InstituteId = @hid) and TestMasterT.Completed is null", con);
            command.CommandType = CommandType.Text;
            command.Parameters.Add(new SqlParameter("@hid", hid));
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(results);
            adapter.Dispose();
            command.Dispose();
            con.Close();
            con.Dispose();
            return results;
        }

        public DataTable GetSubTestListOnly()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT SubTestMasterSerno Id,SubTestName FROM SubTestMasterL", con);
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

        public DataTable GetSubTestList(int TestId)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT SubTestMasterSerno Id,SubTestName FROM SubTestMasterL where TestCodeId=@TestId", con);
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

        public DataTable GetInstituteList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT InstituteSerno Id,InstituteName Name FROM InstituteMasterL", con);
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
        public DataTable GetPatientList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT PatientSerno Id,FirstName Name FROM PatientMaster", con);
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

        #region PatientIno
        public DataTable GetGenderList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT GenderSerno Id,Gender FROM GenderMasterL order by GenderSerno desc", con);
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

        public DataTable GetEthnicBackgroundList()
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            DataTable results = new DataTable();
            SqlCommand command = null;
            command = new SqlCommand("SELECT EthnicBackgroundSerno Id,EthnicBackground FROM EthnicBackgroundL order by EthnicBackground asc", con);
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



        /// <summary>
        /// NIPT Dropdown
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DataTable GetData(string type)
        {
            DataTable dt = new DataTable();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("usp_GetDropDownListData", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@type", type));
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(ex.StackTrace);
            }
            finally
            {
                adapter.Dispose();
                command.Dispose();
                con.Dispose();
                con.Close();
            }

            return dt;

        }
    }
}