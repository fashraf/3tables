using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class InPatientBillPrint : AppCode.Base
    {
        int Id = Convert.ToInt32(System.Web.HttpContext.Current.Request.QueryString["Id"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            var getdata = Cookiesvalue();
            if (getdata != "" && getdata == "valid")
            {
                if (Session["UserID"] != null)
                {
                    if (IsPostBack == false)
                    {
                        LoadBill(Id);
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Payment Detail of Bill# " + Id;
                        AppCode.Audit.auditlog(UID, UserName, "Payment Detail", meta, RoleId, false);
                    }
                }
                else
                {
                    Response.Redirect("../Login.aspx");
                }
            }
            else
            {
                Response.Redirect("../Login.aspx");
            }

        }

        private void LoadBill(int BillId)
        {
            try
            {
                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                DataTable results = new DataTable();
                SqlCommand command = null;
                command = new SqlCommand("sp_GetBillDetail", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@billid ", BillId));
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(results);
                adapter.Dispose();
                command.Dispose();
                con.Close();
                con.Dispose();
                MainRpt.DataSource = results;
                MainRpt.DataBind();
                //return results;
                Generatebarcode(BillId.ToString(), results);
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        private void Generatebarcode(string Barcode, DataTable results)
        {
            try
            {
                string barCode = results.Rows[0]["Barcode"].ToString(); //"NG-12239-112";
                BarcodeLbl.Text = barCode;
                using (Bitmap bitMap = new Bitmap(barCode.Length * 20, 60))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        Font oFont = new Font("IDAutomationHC39M Free Version", 16);
                        PointF point = new PointF(2f, 2f);
                        SolidBrush blackBrush = new SolidBrush(Color.Black);
                        SolidBrush whiteBrush = new SolidBrush(Color.White);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                    }
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitMap.Save(ms, ImageFormat.Png);
                        imgBarcode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                        imgBarcode.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
    }
}