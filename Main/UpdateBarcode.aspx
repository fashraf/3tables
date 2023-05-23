<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="UpdateBarcode.aspx.cs" Inherits="InternalLims.Main.UpdateBarcode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Update Barcode</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="#">Dashboard</a></li>
                            <li class="breadcrumb-item active">Update Barcode</li>
                        </ol>
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
                                <h4 class="card-title">Update Barcode</h4>
                                <p class="text-muted mb-0">
                                   Barcode can be Updated.
                                </p>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label">Barcode</label>
                                            <div class="col-sm-10">
                                                <asp:Label ID="BarcodeIdLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Status</label>
                                            <div class="col-sm-9">
                                               <asp:Label ID="CompleteStatusLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Test Name</label>
                                            <div class="col-sm-9">
                                                <asp:HiddenField ID="TestSernoValue" runat="server" />
                                                <asp:DropDownList ID="TestDrop" runat="server" CssClass="form-control" OnSelectedIndexChanged="TestDrop_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label">Sub Test Name</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="SubTestDrop" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Institute</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="InstituteDrop" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Button ID="ConfirmBtn" runat="server" CssClass="btn btn-primary px-4" Text="Submit" OnClick="ConfirmBtn_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                     <%--Modal--%>
                    <div class="modal fade bd-example-modal-lg" id="Confirm" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static tabindex='-1'">
                        <div class="modal-dialog">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h6 class="modal-title m-0" id="exampleModalDefaultLabel">Confirm !</h6>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="alert alert-success border-0" role="alert">Are you sure you want to <strong>Update this Barcode Information ?</strong></div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="Button2" runat="server" Text="Cancel" data-bs-dismiss="modal" class="btn btn-danger" />
                                            <asp:Button ID="Submit_Btn" runat="server" Text="Yes" UseSubmitBehavior="false" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClick="Submit_Btn_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
     <script type="text/javascript">
         $(document).ready(function () {
             LoadSelect();
         });
        function LoadSelect() {
            $("#ContentPlaceHolder1_InstituteDrop,#ContentPlaceHolder1_TestDrop,#ContentPlaceHolder1_SubTestDrop").select2({ placeholder: '--' });
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    LoadSelect();
                }
            });
        };
     </script>
</asp:Content>
