using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for TestKitsRequestList
    /// </summary>
    public class TestKitsRequestList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            List<AppCode.dto.TestKitsRequestList> dataList = new List<AppCode.dto.TestKitsRequestList>();
            dataList = AppCode.ListData.getTestKitRequestList();
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