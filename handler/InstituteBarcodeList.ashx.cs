using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for InstituteBarcodeList
    /// </summary>
    public class InstituteBarcodeList : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            List<AppCode.dto.InstituteBarList> dataList = new List<AppCode.dto.InstituteBarList>();
            dataList = AppCode.ListData.getInstituteBarcodeList();
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