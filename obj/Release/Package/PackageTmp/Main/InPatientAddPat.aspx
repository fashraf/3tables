<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientAddPat.aspx.cs" Inherits="InternalLims.Main.InPatientAddPat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script src="../assets/js/jquery-2.1.1.js"></script>
    <script src="../assets/toastr/toastr.min.js"></script>
    <script src="../assets/toastr/Common.js"></script>
    <script src="../assets/toastr/toastr.js"></script>
    <script src="../assets/toastr/new/tjs.js"></script>
    <link href="../assets/toastr/new/tcss.css" rel="stylesheet" />
    <link href="../assets/dt/bootstrap-datepicker.min.css" rel="stylesheet" />
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Add New Patient Information</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item"><a href="PatientList">Patient List</a></li>
                            <li class="breadcrumb-item active"><a href="javascript:void(0);">New Patient</a></li>
                        </ol>
                    </div>
                    <!--end col-->
                    <div class="col-auto align-self-center">
                        <a href="InPatientPatList">
                            <button type="button" class="btn btn-gradient-dark"><i class="mdi mdi-account-details me-2"></i>Patient List</button>
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
                                <h4 class="card-title">Add New Patient.</h4>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                National Id
                                                <asp:RequiredFieldValidator ID="NationalReq" runat="server" ValidationGroup="Val" ControlToValidate="NationalIdTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="NationalIdTxt" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                MRN</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="MRNTxt" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Gender
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Val" ControlToValidate="GenderDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="GenderDrop" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                First Name
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Val" ControlToValidate="FirstNameTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="FirstNameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Middle Name</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="MiddleNameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Last Name
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Val" ControlToValidate="LastNameTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="LastNameTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                DOB
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="Val" ControlToValidate="DoBTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="DoBTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Mobile
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Val" ControlToValidate="MobileTxt" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="MobileTxt" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                <asp:RegularExpressionValidator ControlToValidate="MobileTxt" ID="RegularExpressionValidator4" ValidationExpression="^[\s\S]{8,}$" ForeColor="Red" Font-Bold="true" Font-Size="10px" runat="server" ErrorMessage="Mobile should be 10 digits." ValidationGroup="Val"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Email
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="EmailTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please Enter Valid Email." ForeColor="Red" Font-Bold="true" Font-Size="10px" ValidationGroup="Val" ControlToValidate="EmailTxt" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                City
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="Val" ControlToValidate="CityDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></label></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="CityDrop" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Address</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="AddressTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Ethnic Background</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="EthnicBackgroundDrop" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Saudi ?</label>
                                            <div class="col-sm-8">
                                                <asp:CheckBox ID="IsSaudiChk" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Label ID="msg" runat="server"></asp:Label>
                                        <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Send Patient" OnClick="Confirm_Btn_Click" ValidationGroup="Val" />
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
    <script>
        $(function () { Control() });//onlyinteger();});

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
            $("#ContentPlaceHolder1_GenderDrop,#ContentPlaceHolder1_CityDrop,#ContentPlaceHolder1_EthnicBackgroundDrop").select2({ placeholder: '--' });
            $("#ContentPlaceHolder1_NationalIdTxt,#ContentPlaceHolder1_MRNTxt,#ContentPlaceHolder1_MobileTxt").keypress(function (e) { if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) { return false; } });
            $('#ContentPlaceHolder1_DoBTxt').datepicker({ dateFormat: 'yyyy', format: "dd/mm/yyyy", todayHighlight: true, autoclose: true, orientation: 'bottom', endDate: "today", startView:2 });
        };
    </script>
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
</asp:Content>



