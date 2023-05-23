using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for InstituteList
    /// </summary>
    public class InstituteList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            List<AppCode.dto.InstituteInformation> dataList = new List<AppCode.dto.InstituteInformation>();
            dataList = AppCode.ListData.getInstituteList();
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