using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for NiptTaT
    /// </summary>
    public class NiptTaT : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            List<AppCode.dto.TestTatList> dataList = new List<AppCode.dto.TestTatList>();
            dataList = AppCode.ListData.GetTestTat();
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