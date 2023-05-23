<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientComboEdit.aspx.cs" Inherits="InternalLims.Main.InPatientComboEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <script src="../assets/js/jquery-2.1.1.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">InPatient Test Case Pricing View</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item active"><a href="javascript:void(0);">Pricing View</a></li>
                        </ol>
                    </div>
                    <!--end col-->
                    <div class="col-auto align-self-center">
                        <a href="InPatientComboList">
                            <button type="button" class="btn btn-gradient-dark"><i class="mdi mdi-account-details me-2"></i>Walk-In Test Cost Pricing List</button>
                        </a>
                        <a href="Default">
                            <button type="button" class="btn btn-primary"><i class="mdi mdi-keyboard-backspace me-2"></i>Dashboard</button>
                        </a>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </div>
            <!--end page-title-box-->
        </div>
        <!--end col-->
    </div>

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">InPatient Test Case Pricing View</h4>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Sub Test<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Val" ControlToValidate="SubTestDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="SubTestDrop" runat="server" CssClass="form-control" MaxLength="12"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Combo Name	<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Val" ControlToValidate="ComboNameTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="ComboNameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Combo Type	<asp:RequiredFieldValidator ID="NationalReq" runat="server" ValidationGroup="Val" ControlToValidate="ComboTypeDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ComboTypeDrop" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Percentage" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Amount" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-6 form-label align-self-center mb-lg-0 text-end">Combo or Percentage Cost<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Val" ControlToValidate="PercentageOrAmountTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="PercentageOrAmountTxt" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Status
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:CheckBox ID="StatusChk" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Button ID="ConfirmBtn" runat="server" CssClass="btn btn-primary px-4" Text="Add New Coupon" OnClick="ConfirmBtn_Click" ValidationGroup="Val" />
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!-- end col -->
                </div>

                <%--Modal--%>
                <div class="modal fade" id="Confirm" tabindex="-1" role="dialog" aria-textbox ledby="exampleModalTextbox " aria-hidden="true">
                    <div class="modal-dialog" role="document">
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

    <style>
        .form-label {
            color: black;
            font-weight: 600;
        }

        .mandatory {
            border: 1px solid red;
        }

        ul#ulFileUploads li {
            display: inline;
        }
    </style>
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
            $("#ContentPlaceHolder1_ComboTypeDrop,#ContentPlaceHolder1_SubTestDrop").select2({ placeholder: '--' });
            //$("#ContentPlaceHolder1_PercentageOrAmountTxt").keypress(function (e) {if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {return false;}});
            $.unblockUI();
        };
      </script>
</asp:Content>




