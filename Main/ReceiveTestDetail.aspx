<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="ReceiveTestDetail.aspx.cs" Inherits="InternalLims.Main.ReceiveTestDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="page-title-box">
                            <div class="row">
                                <div class="col">
                                    <h4 class="page-title">Test Detail</h4>
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                                        <li class="breadcrumb-item active">Test Detail</li>
                                    </ol>
                                </div>
                                <div class="col-auto align-self-center">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-9">
                        <div class="card" style="padding: 5px">
                            <!--end card-body-->
                            <div class="card-body pt-0">
                                <div style="text-align: center; width: 100%">
                                </div>
                            </div>
                            <div class="card-header" style="text-align:center">
                                <div class="row align-items-center">
                                    <asp:Label ID="StatusRibbon" runat="server" Width="100%" Font-Bold="true" Font-Size="18px"></asp:Label></h4>
                                </div>
                            </div>

                            <%-- <hr class="hr-dashed">--%>
                            <!-- Row -->
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-3 form-label">Barcode*</label>
                                        <div class="col-sm-9">
                                            <div class="input-group mb-0">
                                                <asp:Label ID="txtBarCode" runat="server" CssClass="form-control"></asp:Label>
                                                <asp:HiddenField ID="niptid" runat="server" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <asp:HiddenField ID="InstituteLbl" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="mb-3 row">
                                        <label for="example-tel-input" class="col-sm-4 form-label">Test</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="TestLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
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
                                        <label for="example-tel-input" class="col-sm-4 form-label">Test Created</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="CreatedLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                        </div>
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
                                        <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Patient MRN*</label>
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
                                            <label for="example-tel-input" class="col-sm-5 form-label">Ethnic BG*</label>
                                            <div class="col-sm-7">
                                                <asp:Label ID="EthnicLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                    <div class="media">
                                        <i class="dripicons-document alert-icon text-primary align-self-center font-18 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Pregnancy Information</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-3 form-label">Last Menstrual Period Date:*</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="MenstrualPeriodTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-4 form-label">Age of Gestation:*</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="txtAgeOfGestation" runat="server" CssClass="form-control numberonly"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-4 form-label">Pregnancy Type:*</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="PregnancyTypeLbl" runat="server" CssClass="form-control numberonly"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-5 form-label">Maternal Weight:*</label>
                                            <div class="col-sm-7">
                                                <div class="input-group mb-3">
                                                    <asp:Label ID="txtMaternalWeight" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                    <span class="input-group-text">Kg</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-5 form-label">Maternal Height:*</label>
                                            <div class="col-sm-7">
                                                <div class="input-group mb-3">
                                                    <asp:Label ID="txtMaternalHeight" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                    <span class="input-group-text" id="basic-addon2">CM</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-5 form-label">BMI:*</label>
                                            <div class="col-sm-7">
                                                <asp:Label ID="PatientBMILbl" runat="server" CssClass="form-control numberonly"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                    <div class="media">
                                        <i class="dripicons-user-group alert-icon text-primary align-self-center font-18 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Marriage & Conception Information</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-6 form-label">Marriage Consanguineous:*</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="MarriageConsanguineousLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-6 form-label">Mode of Conception:*</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="ModeConceptionLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="mb-4 row">
                                            <label for="example-tel-input" class="col-sm-6 form-label">History of genetic testing:*</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="HistoryGeneticTestingLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="mb-4 row">
                                            <label for="example-tel-input" class="col-sm-4 form-label">History of Abortion:*</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="HistoryAbortionLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                    <div class="media">
                                        <i class="dripicons-feed alert-icon text-primary align-self-center font-18 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Ultrasound findings and Further Clinical Details</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-3 row">
                                            <label for="example-tel-input" class="col-sm-6 form-label">Latest Ultrasound:*</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="LatestUltrasoundTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="mb-3">
                                            <label class="form-label" for="message">Ultrasound findings</label>
                                            <asp:Label class="form-control" ID="txtUltrasoundFindings" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="mb-3">
                                            <label class="form-label" for="message">Further Clinical Details</label>
                                            <asp:Label class="form-control" ID="txtFurtherClinicalDetails" runat="server"></asp:Label>
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
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Name*</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="txtRequestorName" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Mobile*</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="txtRequestorMobile" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Email*</label>
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

                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <input id="btnPrint" value="Download or Print" class="btn btn-success px-4" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Change Status</h4>

                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <asp:HiddenField ID="Status_hdn" runat="server" />
                                <asp:HiddenField ID="Status_Name" runat="server" />
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView runat="server" ID="Status_Grid" AutoGenerateColumns="false" OnRowCommand="Status_Grid_RowCommand" Width="100%" GridLines="None" DataKeyNames="Id" ShowHeader="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <div class="row align-items-center">
                                                            <asp:Button ID="Status_Lnk" runat="server" CausesValidation="False" CommandArgument='<%# Eval("Id") %>' CommandName="Status" Text='<%# Eval("Status") %>' CssClass="btn btn-primary btn-lg" Height="100%" />
                                                            <asp:Label ID="status_lbl" runat="server" Text='<%# Eval("Id") %>' Visible="false"></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="grditem" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-12">
                                        <div class="row align-items-center">
                                            <h1 class="text-uppercase text-muted mt-2 m-0">
                                                <asp:Button ID="Cancle_Lnk" runat="server" Text="Cancle Test" CssClass="btn btn-danger btn-lg" Width="100%" OnClick="Cancle_Lnk_Click" /></h1>
                                        </div>
                                        <!--end card-body-->
                                    </div>
                                </div>
                                <hr class="hr-dashed">
                                <div class="card-header">
                                    <h4 class="card-title">Attachment/s</h4>
                                </div>
                                <asp:GridView ID="Img_Grid" runat="server" OnRowCommand="Img_Grid_RowCommand" AutoGenerateColumns="false" Width="100%" CssClass="table-light" GridLines="None" HeaderStyle-CssClass="table mb-0" EmptyDataText="No Attachment !" EmptyDataRowStyle-CssClass="alert-danger-shadow">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img src="../assets/images/attached.png" height="85" />
                                                <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FilePathName") %>' CommandName="Download" Text='<%# Eval("FileName") %>' CssClass="text-primary" />
                                                <asp:Label ID="path_lbl" runat="server" Text='<%# Eval("FilePathName") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
                                        <button type="button" class="close" data-dismiss="modal" aria-textbox="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="Confirm_Middle_Lbl" runat="server" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="Button3" runat="server" Text="Cancel" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
                                        <asp:Button ID="Submit_Btn" runat="server" Text="Submit" UseSubmitBehavior="false" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClick="Submit_Btn_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-xl">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">NIPT</h4>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                <embed frameborder="0" width="100%" height="650px" id="report" runat="server" title="Test" />
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <style>
        .col-form-label {
            color: black;
            font-weight: 600;
            text-align: left;
        }
    </style>
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
            $("#ContentPlaceHolder1_Received_Consent_Letter_Drop,#ContentPlaceHolder1_Recieved_By_Drop,#ContentPlaceHolder1_StatusDrop,#ContentPlaceHolder1_RejectionReasonDrop").select2({ placeholder: '--' });

            $(document).on('click', "[id='btnPrint']", function () {
                $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif" /><br/>Just a moment...</h1>' });
                $('#myModal').modal('show');
                $.unblockUI();
            });
        };
    </script>

</asp:Content>



