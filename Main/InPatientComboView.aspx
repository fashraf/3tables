<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientComboView.aspx.cs" Inherits="InternalLims.Main.InPatientComboView" %>
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
                            <button type="button" class="btn btn-gradient-dark"><i class="mdi mdi-account-details me-2"></i>Walk-In Test Pricing List</button>
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
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Test
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="TestLbl" runat="server" CssClass="form-control" MaxLength="12"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Sub Test
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="SubTestLbl" runat="server" CssClass="form-control" MaxLength="12"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Combo Name	
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="ComboNameTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Combo Type	
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="ComboTypeTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Cost</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="PercentageOrAmountLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">
                                                Status
                                            </label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="StatusLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Label ID="ComboId" runat="server" Visible="false"></asp:Label>
                                        <asp:Button ID="EditCombo" runat="server" CssClass="btn btn-primary px-4" Text="Edit this Coupon" OnClick="EditCombo_Click" ValidationGroup="Val" />
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!-- end col -->
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
</asp:Content>




