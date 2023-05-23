using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InternalLims.Main
{
    public partial class InPatientBillTest : AppCode.Base
    {
        AppCode.Drop drop = new AppCode.Drop();
        AppCode.Repository repo = new AppCode.Repository();
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
                        LoadData();
                        ///log
                        int UID = Convert.ToInt32(Session["UserID"].ToString());
                        string Name = Session["FullName"].ToString();
                        string UserName = Session["UserName"].ToString();
                        int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                        string meta = Name + " viewed Payment Bill Page";
                        AppCode.Audit.auditlog(UID, UserName, "View Payment Bill Page", meta, RoleId, false);

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

        private void LoadData()
        {
            try
            {
                DataTable dt = repo.getPatientDetail(Id);
                NationalIdLbl.Text = dt.Rows[0]["NationalId"].ToString();
                NameLbl.Text = dt.Rows[0]["FullName"].ToString();
                MobileLbl.Text = dt.Rows[0]["Mobile"].ToString();
                CityLbl.Text = dt.Rows[0]["City"].ToString();
                IsSaudiLbl.Text = dt.Rows[0]["IsSaudi"].ToString();
                if (IsSaudiLbl.Text == "Saudi")
                {
                    IsSaudiLbl.CssClass = "btn btn-success btn-xs";
                }
                else
                {
                    IsSaudiLbl.CssClass = "btn btn-primary btn-xs";
                }

                TestDrop.DataSource = drop.GetTestList();
                TestDrop.DataBind();
                DataTable gettestlist = drop.GetTestList();
                drop.FillDropDownList(TestDrop, gettestlist, "TestName", "Id");
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void Confirm_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                decimal Change = Convert.ToDecimal(ChangeTxt.Text);
                if (Change <= 0)
                {
                    if (PaymentTypeDrop.SelectedValue == "1")
                    {
                        Confirm_Header_Lbl.Text = "Confirm !";
                        Confirm_Middle_Lbl.Text = "Are you sure ";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                        UpdatePanel2.Update();

                        PatNameModalLbl.Text = NameLbl.Text;
                        IdModalLbl.Text = NationalIdLbl.Text;

                        TestModalLbl.Text = TestDrop.SelectedItem.Text + " >" + SubTestDrop.SelectedItem.Text;
                        GrossAmountModalLbl.Text = GrossAmountLbl.Text;
                        VatModalLbl.Text = VatLbl.Text;
                        DiscountModalLbl.Text = DiscountDrop.SelectedItem.Text + " -" + DiscountLbl.Text;
                        TotalModalLbl.Text = FinalAmountLbl.Text;
                        Submit_Btn.Visible = true;

                    }
                    else if (PaymentTypeDrop.SelectedValue == "2")
                    {
                        Confirm_Header_Lbl.Text = "Confirm !";
                        Confirm_Middle_Lbl.Text = "The Patient with the same <strong>Mobile Or National Id</strong> is already registered with Novo.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                        UpdatePanel2.Update();

                        double Total = Convert.ToDouble(CashTxt.Text) - Convert.ToDouble(ChangeTxt.Text);
                        PatNameModalLbl.Text = NameLbl.Text;
                        IdModalLbl.Text = NationalIdLbl.Text;

                        TestModalLbl.Text = TestDrop.SelectedItem.Text + " >" + SubTestDrop.SelectedItem.Text;
                        GrossAmountModalLbl.Text = GrossAmountLbl.Text;
                        VatModalLbl.Text = VatLbl.Text;
                        DiscountModalLbl.Text = DiscountDrop.SelectedItem.Text + " -" + DiscountLbl.Text;
                        TotalModalLbl.Text = FinalAmountLbl.Text;
                        Submit_Btn.Visible = true;
                    }
                    else if (PaymentTypeDrop.SelectedValue == "3")
                    {
                        Confirm_Header_Lbl.Text = "Confirm !";
                        Confirm_Middle_Lbl.Text = "The Patient with the same <strong>Mobile Or National Id</strong> is already registered with Novo.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                        UpdatePanel2.Update();

                        double Total = Convert.ToDouble(CashTxt.Text) - Convert.ToDouble(ChangeTxt.Text);
                        PatNameModalLbl.Text = NameLbl.Text;
                        IdModalLbl.Text = NationalIdLbl.Text;

                        TestModalLbl.Text = TestDrop.SelectedItem.Text + " >" + SubTestDrop.SelectedItem.Text;
                        GrossAmountModalLbl.Text = GrossAmountLbl.Text;
                        VatModalLbl.Text = VatLbl.Text;
                        DiscountModalLbl.Text = DiscountDrop.SelectedItem.Text + " -" + DiscountLbl.Text;
                        TotalModalLbl.Text = FinalAmountLbl.Text;
                        Submit_Btn.Visible = true;
                    }
                }
                else
                {
                    Confirm_Header_Lbl.Text = "Error !";
                    Confirm_Middle_Lbl.Text = "The Payment Information is not Correct.";
                    PatNameModalLbl.Text = "xxx";
                    IdModalLbl.Text = "xxx";
                    TestModalLbl.Text = "xxx";
                    GrossAmountModalLbl.Text = "xxx";
                    VatModalLbl.Text = "xxx";
                    DiscountModalLbl.Text = "xxx";
                    TotalModalLbl.Text = "xxx";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                    UpdatePanel2.Update();
                    Submit_Btn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        protected void TestDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int TestId = Convert.ToInt32(TestDrop.SelectedValue);
            DataTable getsubtest = drop.GetSubTestList(TestId);
            drop.FillDropDownList(SubTestDrop, getsubtest, "SubTestName", "Id");
        }

        protected void SubTestDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int SubTestId = Convert.ToInt32(SubTestDrop.SelectedValue);
                DataTable dt = repo.getSubTestCost(SubTestId);
                SubTestCostLbl.Text = Convert.ToDouble(dt.Rows[0]["Cost"]).ToString("#.##");

                string hasdiscount = dt.Rows[0]["hasdiscount"].ToString();
                if (hasdiscount == "True")
                {
                    DiscountDrop.Enabled = true;
                    DataTable getsubtest = repo.getComboBySubTestId(SubTestId);
                    drop.FillDropDownList(DiscountDrop, getsubtest, "ComboName", "ComboId");
                    CalculateAmount(SubTestId, -1);
                }
                else
                {
                    DiscountDrop.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        protected void DiscountDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int SubTestId = Convert.ToInt32(SubTestDrop.SelectedValue);
            int ComboId = Convert.ToInt32(DiscountDrop.SelectedValue);
            CalculateAmount(SubTestId, ComboId);
        }

        private void CalculateAmount(int subTestId, int comboId)
        {
            try
            {
                double SubTestAmount = Convert.ToDouble(SubTestCostLbl.Text);
                double Amount = 0;
                int Vat = 15;

                double VATamount = 0.00;
                if (IsSaudiLbl.Text == "Saudi")
                {
                    VATamount = 0.00;
                    VatLbl.Text = "0";
                }
                else
                {
                    VATamount = (SubTestAmount * Vat) / (100 + Vat);
                    VatLbl.Text = VATamount.ToString("#.##");
                }

                //double GrossAmount = (SubTestAmount - VATamount);
                double GrossAmount = (SubTestAmount);
                GrossAmountLbl.Text = GrossAmount.ToString("#.##");

                double TotalAmount = (SubTestAmount + VATamount);
                AmountLbl.Text = TotalAmount.ToString("#.##");

                if (comboId == -1)
                {
                    DiscountLbl.Text = "0";
                    DiscountTypeLbl.Text = "0";
                    FinalAmountLbl.Text = TotalAmount.ToString("#.##");
                }
                else
                {
                    DataTable dt = repo.getComboDetailByComboId(comboId);
                    int isPercent = Convert.ToInt32(dt.Rows[0]["isPercent"].ToString());

                    if (isPercent == 1)
                    {
                        double Percentage = Convert.ToDouble(dt.Rows[0]["Percentage"].ToString());
                        double DiscountAmount = ((TotalAmount * Percentage) / 100);
                        Amount = TotalAmount - DiscountAmount;
                        DiscountLbl.Text = Percentage + "%" + "( " + "SR." + DiscountAmount.ToString("#.##") + " )";
                        DiscountTypeLbl.Text = "Percentage";
                        DiscountHidden.Value = DiscountAmount.ToString();
                        FinalAmountLbl.Text = Amount.ToString("#.##");
                    }
                    else if (isPercent == 2)
                    {
                        Amount = TotalAmount - Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                        DiscountLbl.Text = "( " + (TotalAmount - Amount) + " )";
                        DiscountTypeLbl.Text = "Amount";
                        DiscountHidden.Value = (TotalAmount - Amount).ToString();
                        FinalAmountLbl.Text = Amount.ToString("#.##");
                    }
                    else
                    {
                        DiscountHidden.Value = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        protected void PaymentTypeDrop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PaymentType = Convert.ToInt32(PaymentTypeDrop.SelectedValue);
            if (PaymentType == 1)
            {
                CardTxt.Text = FinalAmountLbl.Text;
                CardTxt.ForeColor = System.Drawing.Color.Red;
                CashTxt.Text = "0";
                CardTxt.Enabled = false;
                CashTxt.Enabled = false;
                ChangeTxt.Enabled = false;
            }
            else if (PaymentType == 2)
            {
                CardTxt.Text = "0";
                CashTxt.ForeColor = System.Drawing.Color.Red;
                CardTxt.Enabled = false;
                CashTxt.Enabled = true;
                ChangeTxt.Enabled = true;
            }
            else if (PaymentType == 3)
            {
                CardTxt.Enabled = true;
                CashTxt.Enabled = true;
                ChangeTxt.Enabled = true;
            }
        }

        protected void Submit_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                decimal Change = Convert.ToDecimal(ChangeTxt.Text);
                if (Change <= 0)
                {
                    int TestId = Convert.ToInt32(TestDrop.SelectedValue);
                    int SubTestId = Convert.ToInt32(SubTestDrop.SelectedValue);
                    int TypeId = 2;
                    AppCode.Connection Con = new AppCode.Connection();
                    String Connection = Con.Con();
                    SqlConnection con = new SqlConnection(Connection);
                    con.Open();
                    SqlCommand command = new SqlCommand("Insert into TestMasterT(TestId,SubTestId,TypeId,InstituteId,PatientId,CreatedDt,CreatedBy,ReceiveDt) values (@TestId,@SubTestId,@TypeId,@InstituteId,@PatientId,getdate(),@CreatedBy,getdate());SELECT @@IDENTITY", con);
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add(new SqlParameter("@TestId ", TestId));
                    command.Parameters.Add(new SqlParameter("@SubTestId", SubTestId));
                    command.Parameters.Add(new SqlParameter("@TypeId", 2));
                    command.Parameters.Add(new SqlParameter("@InstituteId", 100));
                    command.Parameters.Add(new SqlParameter("@PatientId", Id));
                    command.Parameters.Add(new SqlParameter("@CreatedBy", Session["UserID"].ToString()));
                    //command.Parameters.Add(new SqlParameter("@CreatedDt", DateTime.Now.ToString()));

                    int CompletedTestId = Convert.ToInt32(command.ExecuteScalar());

                    ///log
                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string Name = Session["FullName"].ToString();
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " Ceated Test for Patient#" + Id + " with TestId#" + CompletedTestId + " Step.1";
                    AppCode.Audit.auditlog(UID, UserName, "Create Test Id", meta, RoleId, false);


                    AddToPayment(CompletedTestId);

                    con.Dispose();
                    con.Close();
                    con.Dispose();
                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "myJsFn4", "alert('Data Saved Succeessfully')", true);
                }
                else
                {
                    Confirm_Header_Lbl.Text = "Error !";
                    Confirm_Middle_Lbl.Text = "The Payment Information is not Correct.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>$('#Confirm').modal('show');</script>", false);
                    UpdatePanel2.Update();
                    Submit_Btn.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }
        private void AddToPayment(int completedTestId)
        {
            try
            {
                int PaymentType = Convert.ToInt32(PaymentTypeDrop.SelectedValue);
                double SubTestCost = Convert.ToDouble(SubTestCostLbl.Text);
                double Card;
                if (CardTxt.Enabled == false)
                {
                    Card = 0;
                }
                else
                {
                    Card = Convert.ToDouble(CardTxt.Text);
                }
                double Cash;
                if (CashTxt.Enabled == false)
                {
                    Cash = 0;
                }
                else
                {
                    Cash = Convert.ToDouble(CashTxt.Text);
                }

                double Change;
                if (ChangeTxt.Enabled == false)
                {
                    Change = 0;
                }
                else
                {
                    Change = Convert.ToDouble(ChangeTxt.Text);
                }
                double Vat = Convert.ToDouble(VatLbl.Text);

                AppCode.Connection Con = new AppCode.Connection();
                String Connection = Con.Con();
                SqlConnection con = new SqlConnection(Connection);
                con.Open();
                SqlCommand command = new SqlCommand("Insert into PaymentMasterT(TestId,PaymentType,CardAmount,CashAmount,ChangeAmount,VatAmount,TestCost,DiscountType,DiscountName,DiscountAmount,TotalPaid,PaymentStatus,TransactionNumber,CreatedDt,CreatedBy) values (@TestId,@PaymentType,@CardAmount,@CashAmount,@ChangeAmount,@VatAmount,@TestCost,@DiscountType,@DiscountName,@DiscountAmount,@TotalPaid,@PaymentStatus,@TransactionNumber,getdate(),@CreatedBy)", con);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.Add(new SqlParameter("@TestId ", completedTestId));
                command.Parameters.Add(new SqlParameter("@PaymentType", PaymentType));
                command.Parameters.Add(new SqlParameter("@TestCost", SubTestCost));

                if (PaymentType == 1)
                {
                    CardTxt.Text = FinalAmountLbl.Text;
                    CashTxt.Enabled = false;
                    ChangeTxt.Enabled = false;

                    command.Parameters.Add(new SqlParameter("@CardAmount", Convert.ToDouble(Card)));
                    command.Parameters.Add(new SqlParameter("@CashAmount", Convert.ToDouble(0)));
                    command.Parameters.Add(new SqlParameter("@ChangeAmount", Convert.ToDouble(0)));
                    command.Parameters.Add(new SqlParameter("@VatAmount", Convert.ToDouble(Vat)));
                }
                else if (PaymentType == 2)
                {
                    CardTxt.Enabled = false;
                    CashTxt.Enabled = true;
                    ChangeTxt.Enabled = true;

                    int CardAmount = 0;
                    command.Parameters.Add(new SqlParameter("@CardAmount", CardAmount));
                    command.Parameters.Add(new SqlParameter("@CashAmount", Convert.ToDouble(CashTxt.Text)));
                    command.Parameters.Add(new SqlParameter("@ChangeAmount", Change));
                    command.Parameters.Add(new SqlParameter("@VatAmount", Convert.ToDouble(VatLbl.Text)));
                }
                else if (PaymentType == 3)
                {
                    CardTxt.Enabled = true;
                    CashTxt.Enabled = true;
                    ChangeTxt.Enabled = true;

                    command.Parameters.Add(new SqlParameter("@CardAmount", Card));
                    command.Parameters.Add(new SqlParameter("@CashAmount", Cash));
                    command.Parameters.Add(new SqlParameter("@ChangeAmount", Change));
                    command.Parameters.Add(new SqlParameter("@VatAmount", Vat));
                }

                int DiscountName = 0;
                if (DiscountDrop.SelectedValue == "-1")
                {
                    command.Parameters.Add(new SqlParameter("@DiscountName", DiscountName));
                    command.Parameters.Add(new SqlParameter("@DiscountAmount", DiscountName));
                }
                else
                {
                    command.Parameters.Add(new SqlParameter("@DiscountName", DiscountDrop.SelectedItem.Text));
                    command.Parameters.Add(new SqlParameter("@DiscountAmount", Convert.ToDouble(DiscountHidden.Value)));
                }
                command.Parameters.Add(new SqlParameter("@TotalPaid", Convert.ToDouble(FinalAmountLbl.Text)));


                command.Parameters.Add(new SqlParameter("@DiscountType", DiscountDrop.SelectedValue));
                

                command.Parameters.Add(new SqlParameter("@PaymentStatus", 1));
                command.Parameters.Add(new SqlParameter("@TransactionNumber", TransactionNumberTxt.Text));


                command.Parameters.Add(new SqlParameter("@CreatedBy", Session["FullName"].ToString()));
              
                if (command.ExecuteNonQuery() > 0)
                {
                    display = "Test Updated Successful!";
                    DisplayToastr(display, toastrTypes.Success.ToString());

                    ///log
                    int UID = Convert.ToInt32(Session["UserID"].ToString());
                    string Name = Session["FullName"].ToString();
                    string UserName = Session["UserName"].ToString();
                    int RoleId = Convert.ToInt32(Session["RoleId"].ToString());
                    string meta = Name + " Ceated Payment Details for Patient#" + Id + " with TestId#" + completedTestId + " Step.2";
                    AppCode.Audit.auditlog(UID, UserName, "Create Payment Details", meta, RoleId, false);

                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS", "setTimeout(function() { window.location.replace('InPatientSubmitList') }, 3500);", true);
                }
                else
                {
                }
                //AddToPayment(CompletedTestId);

                con.Dispose();
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                Exception objErr = ex.GetBaseException();
                AppCode.Logger.WriteLog(objErr, ex.StackTrace);
            }
        }

        enum toastrTypes { Success, Error, Info, Warning };
        public string display;

        protected void DisplayToastr(string message, string type)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "toastr", "alertMe(" + "\"" + message + "\"" + "," + "\"" + type + "\"" + " );", true);
        }
    }
}