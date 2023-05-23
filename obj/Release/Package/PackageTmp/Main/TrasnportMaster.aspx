<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="TrasnportMaster.aspx.cs" Inherits="InternalLims.Main.TrasnportMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Transport Details</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Transport Details</a></li>
                        </ol>
                    </div>
                    <!--end col-->
                </div>
            </div>
        </div>
    </div>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="card-body">
                    <div class="card-body">
                        <div class="row">
                            <div id="alert" runat="server">
                            </div>
                            <div class="alert alert-light mb-0 row">
                                <div class="col-lg-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-5 form-label">
                                            Transport Company
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" Font-Bold="true" InitialValue="-1" Font-Size="10px" ControlToValidate="TransportCompanyDrop" ValidationGroup="Val"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="TransportCompanyDrop" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-3 form-label">
                                            Tracking #
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" Font-Bold="true" Font-Size="10px" ControlToValidate="TrackingTxt" ValidationGroup="Val"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-9">
                                            <asp:TextBox CssClass="form-control" ID="TrackingTxt" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-4 form-label">
                                            Pick-Up Date
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" Font-Bold="true" Font-Size="10px" ControlToValidate="DateTxt" ValidationGroup="Val"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="col-sm-8">
                                            <asp:TextBox CssClass="form-control" ID="DateTxt" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Button ID="ConfirmBtn" CssClass="btn btn-primary px-4" Text="Submit" runat="server" OnClick="ConfirmBtn_Click" ValidationGroup="Val" />
                                    </div>
                                </div>
                            </div>


                            <hr class="hr-dashed hr-menu">
                            <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                <div class="media">
                                    <i class="dripicons-user alert-icon text-primary align-self-center font-18 me-3"></i>
                                    <div class="media-body align-self-center">
                                        <h5 class="mb-1 fw-bold mt-0">Test Detail</h5>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-4 form-label">Barcode</label>
                                        <div class="col-sm-8">
                                            <div class="input-group mb-0">
                                                <asp:Label ID="txtBarCode" runat="server" CssClass="form-control"></asp:Label>
                                                <asp:HiddenField ID="InstituteId" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-4 form-label">Test</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="TestLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-4 form-label">Status</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="StatsLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-3 form-label">InstituteName</label>
                                        <div class="col-sm-9">
                                            <asp:Label ID="InstituteNameLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-4 form-label">Submitted On</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="CreatedLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true" Visible="false"></asp:Label>
                                            <asp:Label ID="SubmitDateLbl" runat="server" Width="100%" CssClass="form-control" Font-Bold="true"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                <div class="media">
                                    <i class="dripicons-pencil alert-icon text-primary align-self-center font-18 me-3"></i>
                                    <div class="media-body align-self-center">
                                        <h5 class="mb-1 fw-bold mt-0">Request Submitted By</h5>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-3 form-label">Name:</label>
                                        <div class="col-sm-9">
                                            <asp:Label ID="txtRequestorName" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-3 form-label">Mobile:</label>
                                        <div class="col-sm-9">
                                            <asp:Label ID="txtRequestorMobile" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-3 form-label">Email:</label>
                                        <div class="col-sm-9">
                                            <div class="form-control-wrap">
                                                <div class="form-icon form-icon-right">
                                                    <em class="icon ni ni-mail"></em>
                                                </div>
                                                <asp:Label ID="txtRequstorEmail" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
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
                                        <asp:Label ID="Confirm_Middle_Lbl" runat="server" Font-Size="16px"></asp:Label><br />
                                    </div>
                                    <div class="modal-footer"> 
                                        <asp:Button ID="Button3" runat="server" Text="Cancel" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
                                        <asp:Button ID="Submit_Btn" runat="server" Text="Submit" UseSubmitBehavior="false" data-bs-dismiss="modal" CssClass="btn btn-primary" OnClick="Submit_Btn_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div> 
                </div>
            </ContentTemplate>
        </asp:UpdatePanel> 
        <asp:UpdateProgress DisplayAfter="0" ID="MPG1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modalprogress">
                        <div id="theprogress">
                            <img alt="indicator" src="../assets/images/loading.gif" />
                        </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
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
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h1>' });
            $("#ContentPlaceHolder1_TransportCompanyDrop").select2({ placeholder: '--' });
            $('#ContentPlaceHolder1_DateTxt').datepicker({ dateFormat: 'yyyy', format: "dd/mm/yyyy", todayHighlight: true, autoclose: true, orientation: 'bottom' });
            $.unblockUI();
        };

        
    </script>
</asp:Content>
