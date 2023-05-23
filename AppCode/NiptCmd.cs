using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InternalLims.AppCode
{
    public class NiptCmd
    {
        public static List<AppCode.NIPTdto.AddRequest> GetPatientMasterData(int nationalId, int HID)
        {
            List<AppCode.NIPTdto.AddRequest> objList = new List<AppCode.NIPTdto.AddRequest>();
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("USP_GetPatientMaster", con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@nationalId", nationalId));
                    command.Parameters.Add(new SqlParameter("@hid", HID));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                AppCode.NIPTdto.AddRequest obj = new AppCode.NIPTdto.AddRequest();
                                obj.PatientSerNo = Convert.ToInt32(reader["PatientSerno"]);
                                obj.NationalId = Convert.ToInt32(reader["NationalId"]);
                                obj.PatientMRN = Convert.ToInt32(reader["MRN"]);
                                obj.FirstName = Convert.ToString(reader["FirstName"]).Trim();
                                obj.MiddleName = Convert.ToString(reader["MiddleName"]).Trim();
                                obj.LastName = Convert.ToString(reader["LastName"]).Trim();
                                obj.DOB = Convert.ToDateTime(reader["DOB"]).ToString("dd/MM/yyyy").Replace("-", "/").Trim();
                                obj.Email = Convert.ToString(reader["Email"]).Trim();
                                obj.Mobile = Convert.ToInt64(reader["Mobile"]);
                                obj.Ethnic = Convert.ToInt32(reader["EthnicBackgroundId"]);
                                obj.Address = Convert.ToString(reader["Address"]).Trim();
                                obj.HID = Convert.ToInt32(reader["HID"]);
                                obj.City = Convert.ToInt32(reader["CityId"]);

                                objList.Add(obj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                //AppCode.Logger.WriteLog(objErr, ex.StackTrace);
                ///Logger.WriteLog(ex.StackTrace);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return objList;
        }



        #region NIPT
        public static bool SaveRequesterData(int TestId, string barcode, int hid, string xml, int userid, long recordID, ref int NIPTSerNo, ref string errorMessage)
        {
            bool flag = false;
            Connection Con = new Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            try
            {
                SqlCommand command = new SqlCommand("usp_SaveRequesterData", con);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 0;
                command.Parameters.Add(new SqlParameter("@TestId", TestId));
                command.Parameters.Add(new SqlParameter("@barcode", barcode));
                command.Parameters.Add(new SqlParameter("@hID", hid));
                command.Parameters.Add(new SqlParameter("@xml", xml));
                command.Parameters.Add(new SqlParameter("@userID", userid));
                command.Parameters.Add(new SqlParameter("@recordID", recordID));
                SqlParameter paramOut = new SqlParameter("@niptID", NIPTSerNo);
                paramOut.Direction = ParameterDirection.InputOutput;
                command.Parameters.Add(paramOut);
                int i = command.ExecuteNonQuery();
                NIPTSerNo = Convert.ToInt32(command.Parameters["@niptID"].Value);
                flag = true;
            }
            catch (Exception ex)
            {
                if (((SqlException)ex.GetBaseException()).Number == 51000)
                {
                    errorMessage = ex.Message;
                }
                else if (((SqlException)ex.GetBaseException()).Number == 2601)
                {
                    errorMessage = "This barcode already used.";
                }
                flag = false;

                Exception objErr = ex.GetBaseException();
                //AppCode.Logger.WriteLog(objErr, ex.StackTrace);

                //Logger.WriteLog(ex.StackTrace);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return flag;
        }
        #endregion
    }
}