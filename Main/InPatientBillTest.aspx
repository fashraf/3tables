<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientBillTest.aspx.cs" Inherits="InternalLims.Main.InPatientBillTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <form runat="server">
           <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Create Test For Patient</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                                <li class="breadcrumb-item active"><a href="InPatientPatList">Patient List</a></li>
                                <li class="breadcrumb-item active">Create Test For Patient</li>

                            </ol>
                        </div>
                        <div class="col-auto align-self-center">
                            <a href="InPatientPatList">
                                <button type="button" class="btn btn-gradient-info"><i class="mdi mdi-format-list-numbered me-2"></i>Patient List</button>
                            </a>
                            <a href="Default">
                                <button type="button" class="btn btn-primary"><i class="mdi mdi mdi-monitor-dashboard me-2"></i>Dashboard</button>
                            </a>
                            <%--  <a href="Default.aspx">
                                <asp:Button Id="Edit_Btn" runat="server" CssClass="btn btn-gradient-dark" OnClick="Edit_Btn_Click" Text="Edit This Patient"></asp:Button>
                            </a>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Create Test Request</h4>
                                <p class="text-muted mb-0">Create a test request along with Barcode.</p>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">National Id</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="NationalIdLbl" runat="server" CssClass="form-control"></asp:Label>
                                                <asp:Label ID="IsSaudiLbl" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Name</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="NameLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Mobile</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="MobileLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-6 form-label align-self-center mb-lg-0 text-end">City</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="CityLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Test<asp:RequiredFieldValidator ID="NationalReq" runat="server" ValidationGroup="Val" ControlToValidate="TestDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="TestDrop" runat="server" CssClass="form-control" OnSelectedIndexChanged="TestDrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Sub Test<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Val" InitialValue="-1" ControlToValidate="SubTestDrop" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="SubTestDrop" runat="server" CssClass="form-control" OnSelectedIndexChanged="SubTestDrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Cost</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="SubTestCostLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Discount</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="DiscountDrop" runat="server" CssClass="form-control" OnSelectedIndexChanged="DiscountDrop_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="false">
                                                    <asp:ListItem Text="No Discount" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end card-->
                    </div>
                </div>
                <hr class="hr-dashed hr-menu">

                <div class="row">
                    <!--end col-->
                    <div class="col-12 col-lg-6 col-xl text-primary bg-soft-success p-2 m-0 font-11 rounded">
                        <div class="card text-primary bg-soft-primary p-2 m-0 font-11 rounded">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <h3 class="mt-0 text-dark">
                                            <asp:Label ID="GrossAmountLbl" runat="server"></asp:Label></h3>
                                        <h4 class="mt-0 header-title text-truncate font-15 mb-0">Test Amount</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-12 col-lg-6 col-xl text-primary bg-soft-success p-2 m-0 font-11 rounded">
                        <div class="card text-primary bg-soft-primary p-2 m-0 font-11 rounded">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <h3 class="mt-0 text-dark">
                                            <asp:Label ID="VatLbl" runat="server"></asp:Label></h3>
                                        <h4 class="mt-0 header-title text-truncate font-15 mb-0">Vat(15%)</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-12 col-lg-6 col-xl text-primary bg-soft-success p-2 m-0 font-11 rounded">
                        <div class="card text-primary bg-soft-primary p-2 m-0 font-11 rounded">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <h3 class="mt-0 text-dark">
                                            <asp:Label ID="AmountLbl" runat="server"></asp:Label></h3>
                                        <h4 class="mt-0 header-title text-truncate font-15 mb-0">Gross Amount</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-12 col-lg-6 col-xl text-primary bg-light-gradient p-2 m-0 font-11 rounded">
                        <div class="card text-primary bg-light-gradient p-2 m-0 font-11 rounded">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <h3 class="mt-0 text-dark">
                                            <asp:Label ID="DiscountLbl" runat="server"></asp:Label>
                                            <asp:HiddenField ID="DiscountHidden" runat="server"></asp:HiddenField>
                                        </h3>
                                        <h4 class="mt-0 header-title text-truncate font-15 mb-0">
                                            <asp:Label ID="DiscountTypeLbl" runat="server"></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-12 col-lg-6 col-xl text-primary bg-soft-success p-2 m-0 font-11 rounded">
                        <div class="card text-primary bg-soft-success p-2 m-0 font-11 rounded">
                            <div class="card-body">
                                <div class="row align-items-center">
                                    <div class="col text-center">
                                        <h3 class="mt-0 text-dark">
                                            <asp:Label ID="FinalAmountLbl" runat="server"></asp:Label></h3>
                                        <h4 class="mt-0 header-title text-truncate font-15 mb-0">To Be Paid</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr class="hr-dashed hr-menu">
                <asp:Panel ID="PaymentModePanel" runat="server">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <div class="row align-items-center">
                                        <div class="col">
                                            <h4 class="card-title">Payment Mode</h4>
                                        </div>
                                    </div>
                                    <!--end row-->
                                </div>
                                <!--end card-header-->
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-6 form-label">Payment Type<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Val" ControlToValidate="PaymentTypeDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="PaymentTypeDrop" runat="server" CssClass="form-control" OnSelectedIndexChanged="PaymentTypeDrop_SelectedIndexChanged" AutoPostBack="true" Width1="100%">
                                                        <asp:ListItem Text="Select Payment Type" Value="-1"></asp:ListItem>
                                                        <asp:ListItem Text="Card" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Cash" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Both" Value="3"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Card</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="CardTxt" runat="server" CssClass="form-control" Text="0" onkeyup="sum()"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Cash</label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="CashTxt" runat="server" CssClass="form-control" onkeyup="sum()"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Change</label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="ChangeTxt" runat="server" CssClass="form-control" Text="0" onkeyup="sum()"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-2 form-label align-self-center mb-lg-0">Transaction Number</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="TransactionNumberTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="row">
                    <div class="col-sm-12 text-end">
                        <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Create Record" OnClick="Confirm_Btn_Click" ValidationGroup="Val" />
                    </div>
                </div>

                <%--Modal--%>
                <div class="modal fade bd-example-modal-xl" id="Confirm" tabindex="-1" role="dialog" aria-textbox ledby="exampleModalTextbox " aria-hidden="true">
                    <div class="modal-dialog modal-xl" role="document">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalTextbox ">
                                            <asp:Label ID="Confirm_Header_Lbl" runat="server"></asp:Label></h5>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="Confirm_Middle_Lbl" runat="server" Font-Size="16px"></asp:Label>
                                        <div class="table-responsive">
                                            <table class="table mb-0">
                                                <thead class="table-light">
                                                    <tr>
                                                        <th class="border-top-0">Patient Info</th>
                                                        <th class="border-top-0">Test</th>
                                                        <th class="border-top-0">Amount</th>
                                                        <th class="border-top-0">Vat</th>
                                                        <th class="border-top-0">Discount</th>
                                                        <th class="border-top-0">Total</th>
                                                    </tr>
                                                    <!--end tr-->
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <div class="media">
                                                                <div class="media-body align-self-center">
                                                                    <h6 class="m-0">
                                                                        <asp:Label ID="PatNameModalLbl" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label></h6>
                                                                    <a href="#" class="font-14 text-primary">ID:
                                                                        <asp:Label ID="IdModalLbl" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label></a>
                                                                </div>
                                                                <!--end media body-->
                                                            </div>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="TestModalLbl" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="GrossAmountModalLbl" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="VatModalLbl" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="DiscountModalLbl" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="TotalModalLbl" runat="server" Font-Bold="true" Font-Size="20px"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!--end table-->
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="Button2" runat="server" Text="Cancel" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
                                        <asp:Button ID="Submit_Btn" runat="server" Text="Submit" UseSubmitBehavior="false" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClick="Submit_Btn_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script>
        $(function () { Control() });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    Control();
                  
                }
            });
        };

        function Control() {
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h6>' });
            $("#ContentPlaceHolder1_TestDrop,#ContentPlaceHolder1_SubTestDrop,#ContentPlaceHolder1_DiscountDrop,#ContentPlaceHolder1_PaymentTypeDrop").select2({ placeholder: '--' });
            $("#ContentPlaceHolder1_CardTxt,#ContentPlaceHolder1_ChangeTxt").keypress(function (e) { if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) { return false; } });
            sum();
            $.unblockUI();
        };

        function sum() {
            var Final = document.getElementById('ContentPlaceHolder1_FinalAmountLbl').innerText;
            var card = document.getElementById('ContentPlaceHolder1_CardTxt').value;
            var cash = document.getElementById('ContentPlaceHolder1_CashTxt').value;

            if (Final == "")
                Final = 0;
            if (card == "")
                card = 0;
            if (cash == "")
                cash = 0;

            var TotalPayment = parseFloat(card) + parseFloat(cash);
            var result = parseFloat(Final) - parseFloat(TotalPayment);
            if (!isNaN(result)) {
                document.getElementById('ContentPlaceHolder1_ChangeTxt').value = result.toFixed(2);
            }
        }
    </script>
</asp:Content>

