using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static InternalLims.AppCode.NiptCmd;
using System.Web.SessionState;

namespace InternalLims.handler.NIPT
{
    /// <summary>
    /// Summary description for GetPatientMaster
    /// </summary>
    public class GetPatientMaster : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                int nationalId = 0;
                string paramNationalId = context.Request.Params["NationalID"];
                string hospitalid = context.Request.Params["hid"];
                if (paramNationalId == null)
                {
                    nationalId = 0;
                }
                else
                {
                    if (paramNationalId != "")
                    {
                        nationalId = Convert.ToInt32(paramNationalId);
                    }
                }

                int hid = Convert.ToInt32(hospitalid);
                int userID = Convert.ToInt32(1);

                List<AppCode.NIPTdto.AddRequest> list = new List<AppCode.NIPTdto.AddRequest>();
                list = GetPatientMasterData(nationalId, hid);//GetPatientMasterData(nationalId, hid);
                string json = JsonConvert.SerializeObject(list);
                context.Response.ContentType = "text/json";
                context.Response.Write(json);
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                // AppCode.Logger.WriteLog(objErr, ex.StackTrace);
                //Logger.WriteLog(ex.StackTrace);
            }
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