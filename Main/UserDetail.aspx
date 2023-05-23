<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="InternalLims.Main.UserDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <asp:Panel ID="Main_Panel" runat="server" Width="100%">
                        <div class="col-md-12" style="margin-top: 50px">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Registered User</h3>
                                </div>
                                <div class="card-body">
                                    <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                        <div class="media">
                                            <i class="dripicons-home alert-icon text-primary align-self-center font-22 me-3"></i>
                                            <div class="media-body align-self-center">
                                                <h5 class="mb-1 fw-bold mt-0">Institute Information</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <!-- Col -->
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Institute Name</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="InstituteLbl" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label">City</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="CityLbl" runat="server" class="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Col -->
                                        <div class="col-sm-3">
                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Ownership</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="OwnershipLbl" runat="server" class="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Row -->
                                    <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                        <div class="media">
                                            <i class="dripicons-user alert-icon text-primary align-self-center font-22 me-3"></i>
                                            <div class="media-body align-self-center">
                                                <h5 class="mb-1 fw-bold mt-0">Contact Informtion</h5>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Row -->
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">Full Name </label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="TitleNameLbl" runat="server" CssClass="form-control"></asp:Label>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label">Email</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="EmailLbl" runat="server" CssClass="form-control"></asp:Label>
                                                   
                                                </div>
                                            </div>
                                        </div>
                                        <!-- Col -->
                                        <div class="col-sm-4">
                                            <div class="form-group row">
                                                <label class="col-sm-2 col-form-label">Mobile</label>
                                                <div class="col-sm-10">
                                                    <asp:Label ID="MobileLbl" runat="server" CssClass="form-control"></asp:Label>
                                                </div>

                                            </div>
                                        </div>
                                        <!-- Col -->
                                    </div>
                                    <!-- Row -->
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-4 col-form-label">Registration Date </label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="CreatedDtLbl" CssClass="form-control" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                      
                                        <div class="col-sm-6">
                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Status </label>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="UserSatusDrop" CssClass="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Col -->
                            </div>
                            <div class="row">
                                <div class="col-sm-12 text-end">
                                    <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Approve User" OnClick="Confirm_Btn_Click" ValidationGroup="Val" />
                                    <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="Panel_Alert" runat="server" Width="100%" Visible="false">
                        <div class="card" style="margin-top: 50px">
                            <div class="card-body">
                                <div class="d-flex">
                                    <h2 class="m-0 align-self-center">
                                        <img src="assets/images/registration.png" /></h2>
                                    <div class="d-block ms-2 align-self-center">
                                        <span class="text-success" style="font-size: 22px">Registration Completed Successfully.</span>
                                        <h4 class="my-1">We are processing your request.</h4>
                                        <p class="mb-0 text-muted font-16">
                                            Thank you for registering with us. Someone from our team will contact you shortly to confirm your account.
                                           
                                            <a href="" class="text-primary">Read More <i class="las la-arrow-right"></i></a>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
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
                                        <button type="button" class="close" data-dismiss="modal" aria-textbox="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
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
            $("#ContentPlaceHolder1_UserSatusDrop").select2({ placeholder: '--' });
        };
    </script>

    
</asp:Content>
