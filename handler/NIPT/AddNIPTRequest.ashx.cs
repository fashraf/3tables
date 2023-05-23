using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static InternalLims.AppCode.NIPTdto;
using static InternalLims.AppCode.NiptCmd;
using System.Web.SessionState;

namespace InternalLims.handler.NIPT
{
    /// <summary>
    /// Summary description for AddNIPTRequest
    /// </summary>
    public class AddNIPTRequest : IHttpHandler, IRequiresSessionState
    {
        AppCode.Audit Audit = new AppCode.Audit();
        // AppCode.sNotiNovo novonoti = new AppCode.sNotiNovo();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.ContentType = "text/plain";
                string barcode = context.Request.Params["barcode"];
                int TestId = Convert.ToInt32(context.Request.Params["TestId"]);
                int hid = Convert.ToInt32(context.Request.Params["hid"]);
                string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
                List<AddRequest> results = JsonConvert.DeserializeObject<List<AddRequest>>(strJson);
                if (results != null)
                {

                    int userID = Convert.ToInt32(context.Session["UserID"]);
                    long recordID = Convert.ToInt64(context.Session["RecordID"]);

                    //on edit mode 
                    if (results[0].FileUploadList != null)
                    {
                        if (results[0].FileUploadList.Count > 0)
                        {
                            //delete this files first and remove item from FileUploadList
                            foreach (FileUpload fupd in results[0].FileUploadList)
                            {
                                if (File.Exists(fupd.FilePathName))
                                {
                                    File.Delete(fupd.FilePathName);
                                }
                            }
                            results[0].FileUploadList = null;
                        }
                    }

                    string fileUploadPath = context.Server.MapPath("~/Uploads/" + recordID);
                    if (Directory.Exists(fileUploadPath))
                    {
                        string[] files = Directory.GetFiles(fileUploadPath);

                        List<FileUpload> listFileUploads = new List<FileUpload>();
                        foreach (string f in files)
                        {
                            FileInfo file = new FileInfo(f);

                            FileUpload fileUpload = new FileUpload();
                            fileUpload.RecordID = recordID;
                            fileUpload.FileName = file.Name;
                            fileUpload.FilePathName = file.FullName;
                            fileUpload.FileSize = Convert.ToDecimal(file.Length) / 1024;
                            fileUpload.FileExtension = file.Extension;

                            listFileUploads.Add(fileUpload);
                        }

                        results[0].FileUploadList = listFileUploads;
                    }

                    string xml = SerializeToXml(results);
                    xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                    int niptId = 0;
                    string errorMessage = "";
                    bool flag = SaveRequesterData(TestId, barcode, hid, xml, userID, recordID, ref niptId, ref errorMessage);

                    if (flag == true)
                    {
                        JsonMessage obj = new JsonMessage();
                        context.Session["NewNIPTID"] = niptId;
                        obj.Message = "Data Saved Successfully.";
                        obj.Success = true;
                        obj.Id = niptId;
                        var result = JsonConvert.SerializeObject(obj);
                        context.Response.Write(result);


                        ///log
                        int UID = Convert.ToInt32(context.Session["UserID"].ToString());
                        string Name = context.Session["FullName"].ToString();
                        string UserName = context.Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(context.Session["RoleId"].ToString());
                        string meta = Name + " Added New NIPT Request Id: " + niptId;
                        AppCode.Audit.auditlog(UID, UserName, "NIPT Added", meta, RoleId, false);


                        //string Institute = context.Session["HospitalName"].ToString();
                        //string CreatedName = context.Session["Name"].ToString();
                        //string novomsg = CreatedName + " from </br>" + Institute + " Created a New NIPT Request with Barcode  </br><strong>" + barcode + "</strong>";
                        //string Page = "../../emiltemp/novo1.html";
                        //novonoti.SendNotiToNovo(1, barcode, novomsg, Page);

                        //string Email = context.Session["Email"].ToString();
                        //string Name =CreatedName;
                        //string Title = "[Novo Genomics] NIPT -" + barcode;
                        //string Msg = "Thank you for submitting NIPT Request. We will contact you soon regarding Sample Collection.</br>The new NIPT Request Reference Id is </br><strong>" + barcode + "</strong>";
                        //string EmailSubject = niptId + " New NIPT Request.";
                        //AppCode.sNotiNovo.SendEmail(Email, Name, Title, Msg, Page);
                        //AppCode.notification.SaveNotification(hID, userID, 1, novomsg, true);
                    }
                    else
                    {
                        JsonMessage obj = new JsonMessage();
                        obj.Message = errorMessage != "" ? errorMessage : "Error Occurred While Saving Data";
                        Exception objErr = new Exception();
                        //AppCode.Logger.WriteLog(objErr.GetBaseException(), obj.Message);
                        obj.Success = false;
                        obj.Id = niptId;
                        var result = JsonConvert.SerializeObject(obj);
                        context.Response.Write(result);
                    }
                }
                else
                {
                    JsonMessage obj = new JsonMessage();
                    obj.Message = "Error Occurred While Saving Data";
                    Exception objErr = new Exception();
                    // AppCode.Logger.WriteLog(objErr.GetBaseException(), obj.Message);
                    obj.Success = false;
                    obj.Id = 0;
                    var result = JsonConvert.SerializeObject(obj);
                    context.Response.Write(result);
                }
            }
            catch (Exception ex)
            {

                JsonMessage obj = new JsonMessage();
                obj.Message = "Error Occurred While Saving Data";
                Exception objErr = ex.GetBaseException();
                // AppCode.Logger.WriteLog(objErr, ex.StackTrace);
                obj.Success = false;
                obj.Id = 0;
                var result = JsonConvert.SerializeObject(obj);
                context.Response.Write(result);
                //Logger.WriteLog(ex.StackTrace);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}