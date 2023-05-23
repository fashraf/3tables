using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for TestRequestList
    /// </summary>
    public class TestRequestList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            List<AppCode.dto.TestRegistrationList> dataList = new List<AppCode.dto.TestRegistrationList>();
            dataList = AppCode.ListData.getTestRequestList();
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