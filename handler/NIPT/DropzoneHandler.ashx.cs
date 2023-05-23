using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace InternalLims.handler.NIPT
{
    /// <summary>
    /// Summary description for DropzoneHandler
    /// </summary>
    public class DropzoneHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                long recordID = Convert.ToInt64(context.Session["RecordID"]);
                //var imagePath = ConfigurationManager.AppSettings["ImagePath"];
                string dirFullPath = HttpContext.Current.Server.MapPath("~/Uploads/" + recordID);
                //string dirFullPath = HttpContext.Current.Server.MapPath(imagePath + recordID);
                if (System.IO.Directory.Exists(dirFullPath) == false)
                {
                    System.IO.Directory.CreateDirectory(dirFullPath);
                }
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;

                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = "File_" + numFiles.ToString() + fileExtension;
                        string pathToSave = HttpContext.Current.Server.MapPath("~/Uploads/" + recordID + "/") + str_image;
                        //string pathToSave = HttpContext.Current.Server.MapPath(imagePath + recordID + "/") + str_image;
                        file.SaveAs(pathToSave);
                    }
                }
                context.Response.Write(str_image);
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