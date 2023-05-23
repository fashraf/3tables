<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientTestPricingEdit.aspx.cs" Inherits="InternalLims.Main.InPatientTestPricingEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Test Cost List</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item active">Test Cost List</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a href="InPatientTestPricingList" class="btn btn-sm btn-primary">Test Cost List</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Test Cost List</h4>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label align-self-center mb-lg-0 text-end">Test</label>
                                            <div class="col-sm-10">
                                                <asp:Label ID="TestLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label align-self-center mb-lg-0 text-end">Sub Test</label>
                                            <div class="col-sm-10">
                                                <asp:Label ID="SubTestLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label align-self-center mb-lg-0 text-end">Cost</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="CostTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Button ID="Confirm_Btn" runat="server" Text="Submit Test" OnClick="Confirm_Btn_Click" type="submit" class="btn btn-primary px-4" />
                                        <asp:HiddenField ID="SubTestId" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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

    <script type="text/javascript">
        window.onload = function () {
            LoadSelect();
        };
        function LoadSelect() {
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h1>' });
            $('#ContentPlaceHolder1_CostTxt').keypress(function (event) {
                if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
                    if (event.keyCode !== 8 && event.keyCode !== 46) { //exception
                        event.preventDefault();
                    }
                }
                if (($(this).val().indexOf('.') != -1) && ($(this).val().substring($(this).val().indexOf('.'), $(this).val().indexOf('.').length).length > 2)) {
                    if (event.keyCode !== 8 && event.keyCode !== 46) { //exception
                        event.preventDefault();
                    }
                }
            });


            $.unblockUI();
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) { LoadSelect(); }
            });
        };
    </script>
</asp:Content>
