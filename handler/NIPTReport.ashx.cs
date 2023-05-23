using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace InternalLims.handler
{
    /// <summary>
    /// Summary description for NIPTReport
    /// </summary>
    public class NIPTReport : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int NID =Convert.ToInt32(context.Request.QueryString["Id"]);///Convert.ToInt32(context.Session["NID"]);
           

            try
            {
                if (NID > 0)
                {
                    DataTable NiptDetail = LoadDataInTable(NID);

                 
                    ReportDataSource NiptInfo = new ReportDataSource("NIPT", NiptDetail);

                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = ".pdf";

                    ReportViewer viewer = new ReportViewer();
                    viewer.ProcessingMode = ProcessingMode.Local;
                    viewer.LocalReport.ReportPath = "Main\\Report\\NiptView.rdlc";

                    viewer.LocalReport.EnableExternalImages = true;
                    viewer.LocalReport.DataSources.Add(NiptInfo);

                    byte[] bytes = viewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    viewer.LocalReport.DisplayName = "TestNameReport_" + DateTime.Now.ToString("dd.mm.yyyy");
                    if (bytes != null)
                    {
                        context.Response.ContentType = "application/pdf";
                        //context.Response.AddHeader("content-length","attachment;; filename= filename_" + DateTime.Now.ToString("dd.mm.yyyy")+ "." + extension, bytes.Length.ToString());
                        context.Response.AddHeader("content-length", "attachment; filename= filename_" + DateTime.Now.ToString("dd.mm.yyyy") + "." + extension);
                        context.Response.BinaryWrite(bytes);
                    }
            }
                else
            {
                context.Response.ContentType = "text/json";
                context.Response.Write("Hospital not found");
            }
        }
            catch (Exception ex)
            {
                context.Response.ContentType = "text/json";
                context.Response.Write(ex);
            }
        }

        //private string GenerateQRCode(int hospitalId)
        //{
        //    //string url = string.Empty;
        //    //if (ConfigurationSettings.AppSettings["QRCodeUrl"] != null)
        //    //{
        //    //    url = ConfigurationSettings.AppSettings["QRCodeUrl"].ToString();
        //    //}
        //    //int hid = hospitalId + 18 + 20 + 30;
        //    //url += "?Id=" + hid;
        //    ////18+30+20 is a thing that masks the HID
        //    //string filename = hospitalId + "_qrcode.bmp";
        //    //QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
        //    //QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
        //    //QRCoder.QRCode qRCode = new QRCoder.QRCode(qRCodeData);
        //    //Bitmap bmp = qRCode.GetGraphic(20);
        //    //using (MemoryStream ms = new MemoryStream())
        //    //{
        //    //    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

        //    //    string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/QRCode/") + filename;

        //    //    File.WriteAllBytes(filePath, ms.ToArray());
        //    //}

        //  //  return filename;
        //}

        private DataTable LoadDataInTable(int nid)
        {
            DataTable dt = null;
            AppCode.Connection Con = new AppCode.Connection();
            String Connection = Con.Con();
            SqlConnection con = new SqlConnection(Connection);
            con.Open();
            SqlCommand command = null;
            command = new SqlCommand("USP_GetNiptDetail", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@TestId", nid));
            SqlDataAdapter da = new SqlDataAdapter(command);
            dt = new DataTable();
            da.Fill(dt);
            command.Dispose();

            return dt;
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