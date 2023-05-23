using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace InternalLims.handler.NIPT
{
    /// <summary>
    /// Summary description for DeleteFileHandler
    /// </summary>
    public class DeleteFileHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            long recordID = Convert.ToInt64(context.Session["RecordID"]);
            string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
            FileUpload results = JsonConvert.DeserializeObject<FileUpload>(strJson);
            //var imagePath = ConfigurationManager.AppSettings["ImagePath"];
            string dirFullPath = HttpContext.Current.Server.MapPath("~/Uploads/" + recordID);
            //string dirFullPath = HttpContext.Current.Server.MapPath(imagePath + recordID);
            dirFullPath += "/" + results.FileName;
            try
            {
                //File.Delete(dirFullPath);
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                //AppCode.Logger.WriteLog(objErr, ex.StackTrace);
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