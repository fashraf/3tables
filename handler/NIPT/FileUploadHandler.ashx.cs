using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace InternalLims.handler.NIPT
{
    /// <summary>
    /// Summary description for FileUploadHandler
    /// </summary>
    public class FileUploadHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string fileName = "";
            try
            {
                if (context.Session != null)
                {
                    // context.Response.Write(context.Session["RecordID"]);
                    long recordID = Convert.ToInt64(context.Session["RecordID"]);
                    //var imagePath = ConfigurationManager.AppSettings["ImagePath"];
                    string fileUploadPath = context.Server.MapPath("~/Uploads/" + recordID);
                    //string fileUploadPath = context.Server.MapPath("~/+" + imagePath + recordID);

                    if (System.IO.Directory.Exists(fileUploadPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(fileUploadPath);
                    }

                    if (context.Request.Files.Count > 0)
                    {
                        HttpFileCollection files = context.Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFile file = files[i];
                            fileName = file.FileName;
                            string fname = context.Server.MapPath("~/uploads/" + recordID + "/" + file.FileName);
                            //string fname = context.Server.MapPath("~/+" + imagePath + recordID + "/" + file.FileName);
                            file.SaveAs(fname);
                        }
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(fileName);
                    }
                }
                else
                {
                    //context.Response.Write(context.Session["RecordID"]);   
                }
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