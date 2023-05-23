<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientBillPrint.aspx.cs" Inherits="InternalLims.Main.InPatientBillPrint" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
    <link href="https://fonts.googleapis.com/css2?family=Cairo&family=Teko:wght@600&display=swap" rel="stylesheet">
    <style>
        .b {
            font-family: 'Cairo', sans-serif;
            font-family: 'Teko', sans-serif;
            font-size: 20px;
        }
    </style>
    <div class="row b">
        <div class="col-lg-10 mx-auto" style="border-color: black; padding: 10px">
            <div class="card">
                <div class="card-body invoice-head">
                    <div class="row">
                        <!--end col-->
                        <div class="col-md-12">
                            <ul class="list-inline mb-0 contact-detail">
                                <li class="list-inline-item" style="background-color: white; border-left: white; text-align: left">
                                    <div class="ps-3">
                                        <p class="text-muted mb-0">&nbsp;</p>
                                        <img src="../assets/images/mainlogo.png" alt="logo-large" height="60">
                                        <p class="text-muted mb-0">&nbsp;</p>
                                    </div>
                                </li>
                                <li class="list-inline-item">
                                    <div class="ps-3">
                                        <i class="mdi mdi-web"></i>
                                        <p class="text-muted mb-0">www.novogenomics.sa</p>
                                        <p class="text-muted mb-0">info@novo-genomics.com</p>
                                    </div>
                                </li>
                                <li class="list-inline-item">
                                    <div class="ps-3">
                                        <i class="mdi mdi-phone"></i>
                                        <p class="text-muted mb-0">Abdulaziz Rd Al Malqa</p>
                                        <p class="text-muted mb-0">+966 58 306 4989</p>
                                    </div>
                                </li>
                                <li class="list-inline-item" style="text-align: center">
                                    <div class="ps-3">
                                        <asp:Image ID="imgBarcode" runat="server" Visible="false" class="logo-sm me-1" Height="44" />
                                        <p class="text-muted mb-0">
                                            <asp:Label ID="BarcodeLbl" runat="server"></asp:Label></p>
                                    </div>
                                </li>
                            </ul>
                        </div>
                        <!--end col-->
                    </div>
                    <!--end row-->
                </div>
                <!--end card-body-->
                <asp:Repeater ID="MainRpt" runat="server">
                    <ItemTemplate>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4 align-self-center" style="font-size: 10px">
                                    <p class="mt-2 mb-0 text-muted">Patient Information.</p>
                                </div>
                            </div>
                            <!--end row-->
                            <table class="table table-bordered mb-0">
                                <thead class="thead-light">
                                    <tr>
                                        <th style="font-size: 16px">Name :<%# Eval("FirstName") %> <%# Eval("LastName") %>
                                            <br />
                                            ID &nbsp;&nbsp;&nbsp;:<%# Eval("NationalId") %> </th>
                                    </tr>
                                    <!--end tr-->
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                            <hr class="hr-dashed hr-menu">
                            <div class="row">
                                <div class="col-md-4 align-self-center" style="font-size: 10px">
                                    <p class="mt-2 mb-0 text-muted">Payment Invoice Information.</p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive project-invoice">
                                        <table class="table table-bordered mb-0">
                                            <thead style="background-color: grey">
                                                <tr style="background-color: #eaf0f9">
                                                    <th style="width: 60%; border-color: white">Test Detail</th>
                                                    <th style="width: 5%; border-color: white"></th>
                                                    <th style="width: 0%; border-color: white"></th>
                                                    <th style="width: 30%; text-align: center; border-color: white">Amount</th>
                                                </tr>
                                                <!--end tr-->
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td colspan="2" class="border-0">
                                                        <h5 class="mt-0 mb-1 font-14">
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />.<%# Eval("SubTestName") %></h5>
                                                        <p class="mb-0 text-muted">Payment Mode:<%# Eval("Payment") %></p>
                                                        <p class="mb-0 text-muted">Payment Status:<%# Eval("PaymentStatus") %></p>

                                                    </td>
                                                    <td class="border-0 font-14 text-dark" style="width: 20%; text-align: right"><b>Test Price</b></td>
                                                    <td class="border-0 font-14 text-dark" style="width: 20%; text-align: center"><%# Eval("TestCost", "{0:0.00}") %></td>
                                                </tr>
                                                <!--end tr-->
                                                <tr>
                                                    <td colspan="2" class="border-0"></td>
                                                    <td class="border-0 font-14 text-dark" style="width: 20%; text-align: right"><b>Discount</b></td>
                                                    <td class="border-0 font-14 text-dark" style="width: 20%; text-align: center"><b><%# Eval("DiscountAmount", "{0:0.00}") %><br />
                                                        <a style="font-size: 9px"><%# Eval("DiscountName") %></a></td>
                                                </tr>
                                                <!--end tr-->
                                                <tr>
                                                    <td colspan="2" class="border-0"></td>
                                                    <td class="border-0 font-14 text-dark" style="width: 20%; text-align: right"><b>VAT</b></td>
                                                    <td class="border-0 font-14 text-dark" style="width: 20%; text-align: center"><b><%# Eval("Vat", "{0:0.00}") %></b></td>
                                                </tr>
                                                <!--end tr-->
                                                <tr class="bg-black text-white">
                                                    <td colspan="2" class="border-0"></td>
                                                    <td class="border-0 font-14" style="width: 20%; text-align: right"><b>Total</b></td>
                                                    <td class="border-0 font-14" style="width: 20%; text-align: center"><b><%# Eval("TotalPaid", "{0:0.00}") %></b></td>
                                                </tr>
                                                <!--end tr-->
                                            </tbody>
                                        </table>
                                        <!--end table-->
                                    </div>
                                    <!--end /div-->
                                </div>
                                <!--end col-->
                            </div>
                            <!--end row-->

                            <div class="row justify-content-center">
                                <div class="col-lg-6">
                                    <h5 class="mt-4">Terms And Condition :</h5>
                                    <ul class="ps-1">
                                        <li><small class="font-8">All accounts are to be paid within 7 days from receipt of invoice. </small></li>
                                         <li><small class="font-8">To be paid by cheque or credit card or direct payment online.</small></li>
                                       <%--    <li><small class="font-12">If account is not paid within 7 days the credits details supplied as confirmation of work undertaken will be charged the agreed quoted fee noted above.</small></li>--%>
                                    </ul>
                                </div>
                                <!--end col-->
                                <div class="col-lg-6 align-self-end">
                                    <div class="float-end" style="width: 30%; text-align: center">
                                        <img src="../assets/images/novostamp.png" class="mt-2 mb-1" height="175" />
                                        <p class="border-top" style="font-size: 18px">Account Manager</p>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <!--end row-->
                            <div class="row d-flex justify-content-center">
                                <div class="col-lg-12 col-xl-4 ms-auto align-self-center">
                                    <div class="text-center"><small class="font-12">Thank you for your Visit.</small></div>
                                </div>
                                <!--end col-->
                                <div class="col-lg-12 col-xl-4">
                                    <div class="float-end d-print-none">
                                         <button type="button" class="btn btn-primary"><i class="mdi mdi-check-all me-2"></i><a href="javascript:window.print()" style="color:white">Print</a></button>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <!--end row-->
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!--end card-->
        </div>
        <!--end col-->
    </div>
   
</asp:Content>

