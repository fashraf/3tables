<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="NiptReportMaster.aspx.cs" Inherits="InternalLims.Main.NiptReportMaster" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Report Details</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Report Details</a></li>
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
                                <div class="col-lg-12">
                                    <div class="row">
                                        <asp:Label ID="alert" runat="server" Width="100%"></asp:Label>
                                        <div class="col-6">
                                            <div class="card report-card">
                                                <div class="card-body">
                                                    <div class="row d-flex justify-content-center">
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                            <asp:Button ID="UploadBtn" Text="Upload" runat="server" OnClick="UploadFile" CssClass="btn btn-primary" />
                                                        </div>
                                                        <code>You can Upload The Result and It will Reflect in the Portal.</code>
                                                    </div>
                                                    <br />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Label ID="RecordId" runat="server" Visible="false"></asp:Label>
                                        <!--end col-->
                                        <div class="col-6">
                                            <div class="card report-card">
                                                <div class="card-body">
                                                    <div class="row d-flex justify-content-center">
                                                        <asp:GridView ID="Img_Grid" runat="server" OnRowCommand="Img_Grid_RowCommand" AutoGenerateColumns="false" Width="100%" GridLines="None" ShowHeader="false" HeaderStyle-CssClass="table mb-0" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="14px" EmptyDataText="No Report Uploaded!">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <li class="list-group-item"><i class="la la-angle-double-right text-info me-2"><i class="dripicons-document"></i>
                                                                            <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FilePathName") %>' CommandName="Download" Text='<%# Eval("FileName") %>' CssClass="text-primary" />
                                                                            <asp:Label ID="path_lbl" runat="server" Text='<%# Eval("FilePathName") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="name_lbl" runat="server" Text='<%# Eval("FileName") %>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="uid" runat="server" Text='<%# Eval("UploadID") %>' Visible="false"></asp:Label>
                                                                       
                                                                       
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkDel" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FilePathName") %>' CommandName="Del" Text='Delete' CssClass="btn btn-danger" />
                                                                         </i> </li>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

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
                                        <label for="example-tel-input" class="col-sm-4 form-label">InstituteName</label>
                                        <div class="col-sm-8">
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



                                  <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                        <div class="media">
                                            <i class="dripicons-user alert-icon text-primary align-self-center font-18 me-3"></i>
                                            <div class="media-body align-self-center">
                                                <h5 class="mb-1 fw-bold mt-0">Patient Information</h5>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-1 form-label">ID</label>
                                                <div class="col-sm-11">
                                                    <asp:Label ID="txtNationalID" runat="server" CssClass="form-control"></asp:Label>
                                                    National Id/Iqama/Passport
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Patient MRN</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="txtPatientMRN" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-1 form-label">Name</label>
                                                <div class="col-sm-11">
                                                    <asp:Label ID="txtName" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-date-input" class="col-sm-2 form-label">DOB</label>
                                                    <div class="col-sm-10">
                                                        <asp:Label ID="DobTxt" runat="server" CssClass="form-control restrictinput"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label">Mobile</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtMobile" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label">Email</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtEmail" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        
                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label">City</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="CityLbl" runat="server" Width="100%" CssClass="form-control"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label">Address</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtAddress" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-5 form-label">Ethnic BG</label>
                                                    <div class="col-sm-7">
                                                        <asp:Label ID="EthnicLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                                    </div>
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
                                        <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Name:</label>
                                        <div class="col-sm-9">
                                            <asp:Label ID="txtRequestorName" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Mobile:</label>
                                        <div class="col-sm-9">
                                            <asp:Label ID="txtRequestorMobile" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="mb-3 row">
                                        <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Email:</label>
                                        <div class="col-sm-9">
                                            <div class="form-control-wrap">
                                                <div class="form-icon form-icon-right">
                                                    <em class="icon ni ni-mail"></em>
                                                </div>
                                                <asp:TextBox ID="txtRequstorEmail" runat="server" CssClass="form-control" MaxLength="25"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--                            <div class="row">
                                <div class="col-12">
                                    <div style="overflow-x: scroll; overflow-y: scroll">
                                         <rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true">
                                </rsweb:ReportViewer>
                                    </div>
                                </div>
                            </div>--%>
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
                                        <asp:Button ID="Submit_Btn" runat="server" Text="Submit" UseSubmitBehavior="false" data-bs-dismiss="modal" CssClass="btn btn-primary" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


                <%--Modal--%>
                <div class="modal fade bd-example-modal-xl" id="bd-example-modal-xl" tabindex="-1" aria-labelledby="myExtraLargeModalLabel" style="display: none;" aria-hidden="true">
                    <div class="modal-dialog modal-xl" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title m-0" id="myExtraLargeModalLabel">Report</h6>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="UploadBtn" />
            </Triggers>
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
            $("#ContentPlaceHolder1_FetalRiskTypeDrop,#ContentPlaceHolder1_FetalGenderDrop").select2({ placeholder: '--' });
        };


    </script>
</asp:Content>
