using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for NewRegisterUserList
    /// </summary>
    public class NewRegisterUserList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            List<AppCode.dto.NewUserRegistration> dataList = new List<AppCode.dto.NewUserRegistration>();
            dataList = AppCode.ListData.getNewRegisterationUserList();
            context.Response.Write(JsonConvert.SerializeObject(dataList));
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