using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class Insert
    {
        public bool AddPatientInfo(string NationalId, string MRN, string FullName, string MiddleName, string LastName, string DOB, int GenderId, int CityId, string Email, string Mobile, int EthnicBackgroundId, string Address, int HID, int CreatedBy, int InsertType,bool IsSaudi)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("Sp_InsertInPatientInfo", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@NationalId", NationalId));
            command.Parameters.Add(new SqlParameter("@MRN", MRN));
            command.Parameters.Add(new SqlParameter("@FirstName", FullName));
            command.Parameters.Add(new SqlParameter("@MiddleName", MiddleName));
            command.Parameters.Add(new SqlParameter("@LastName", LastName));
            command.Parameters.Add(new SqlParameter("@DOB", DOB));
            command.Parameters.Add(new SqlParameter("@GenderId", GenderId));
            command.Parameters.Add(new SqlParameter("@CityId", CityId));
            command.Parameters.Add(new SqlParameter("@Email", Email));
            command.Parameters.Add(new SqlParameter("@Mobile", Mobile));
            command.Parameters.Add(new SqlParameter("@EthnicBackgroundId", EthnicBackgroundId));
            command.Parameters.Add(new SqlParameter("@Address", Address));
            command.Parameters.Add(new SqlParameter("@HID", HID));
            command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
            command.Parameters.Add(new SqlParameter("@InsertType", InsertType));
            command.Parameters.Add(new SqlParameter("@IsSaudi", IsSaudi));
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


        public bool InsertFileUpad(int TestId, string PatID, string RecordID, string FilePathName, string FileName, string FileExtension, string FileSize, string UploadType)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("insert into Fileuploads(TestId,PatID,RecordID,FilePathName,FileName,FileExtension,FileSize,UploadType,CreatedOn) values(@TestId,@PatID,@RecordID,@FilePathName,@FileName,@FileExtension,@FileSize,@UploadType,@CreatedOn)", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@TestId", TestId));
            command.Parameters.Add(new SqlParameter("@PatID", PatID));
            command.Parameters.Add(new SqlParameter("@RecordID", RecordID));
            command.Parameters.Add(new SqlParameter("@FilePathName", FilePathName));
            command.Parameters.Add(new SqlParameter("@FileName", FileName));
            command.Parameters.Add(new SqlParameter("@FileExtension", FileExtension));
            command.Parameters.Add(new SqlParameter("@FileSize", FileSize)); 
            command.Parameters.Add(new SqlParameter("@UploadType", UploadType));
            command.Parameters.Add(new SqlParameter("@CreatedOn", DateTime.Now.ToString()));
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


        public bool AddNewHospial(string InstituteName, int CityId ,int OwnershipId, int MainBranch, int MainInstituteId, string CRNumber,string Address, bool Status, string CreatedBy)
        {
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = new SqlCommand("insert into InstituteMasterL(InstituteName,CityId,OwnershipId,MainBranch,MainInstituteId,CRNumber,Address,InstituteStatus,CreatedBy,CreatedDt)values(@InstituteName,@CityId,@OwnershipId,@MainBranch,@MainInstituteId,@CRNumber,@Address,@InstituteStatus,@CreatedBy,GetDate())", con);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@InstituteName", InstituteName));
            command.Parameters.Add(new SqlParameter("@CityId", CityId));
            command.Parameters.Add(new SqlParameter("@OwnershipId", OwnershipId));
            command.Parameters.Add(new SqlParameter("@MainBranch", MainBranch));
            command.Parameters.Add(new SqlParameter("@MainInstituteId", MainInstituteId));
            command.Parameters.Add(new SqlParameter("@CRNumber", CRNumber));
            command.Parameters.Add(new SqlParameter("@Address", Address));
            command.Parameters.Add(new SqlParameter("@InstituteStatus", Status));
            command.Parameters.Add(new SqlParameter("@CreatedBy", CreatedBy));
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
    }
}