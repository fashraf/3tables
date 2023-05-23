<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="ViewNIPT.aspx.cs" Inherits="InternalLims.Main.ViewNIPT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <link href="../assets/dt/bootstrap-datepicker.min.css" rel="stylesheet" />
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">NIPT</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">NIPT Request</a></li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a href="RequestTest.aspx">
                            <button type="button" class="btn btn-gradient-danger">Apply for New Samples</button>
                        </a>

                        <a href="NIPT.aspx">
                            <button type="button" class="btn btn-dark">Request a New NIPT</button>
                        </a>
                        <a href="Default.aspx">
                            <button type="button" class="btn btn-primary"><i class="mdi mdi-keyboard-backspace me-2"></i>Back to Dashboard</button>
                        </a>
                        <a>
                            <input id="btnPrint" value="Download or Print" class="btn btn-success" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <form runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-10">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h3 class="card-title">Request a NIPT Test</h3>
                                    </div>
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Barcode</label>
                                                <div class="col-sm-9">
                                                    <div class="input-group mb-0">
                                                        <asp:Label ID="txtBarCode" runat="server" CssClass="form-control"></asp:Label>
                                                        <asp:HiddenField ID="niptid" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Requested Test</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="Lbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">NIPT Test Created</label>
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
                                                <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">National Id/Iqama</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="txtNationalID" runat="server" CssClass="form-control"></asp:Label>
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
                                                <label for="example-text-input" class="col-sm-1 form-label align-self-center mb-lg-0 text-end">Name</label>
                                                <div class="col-sm-11">
                                                    <asp:Label ID="txtName" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-date-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Date OF Birth</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="DobTxt" runat="server" CssClass="form-control restrictinput"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Mobile</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtMobile" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Email</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtEmail" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">City</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="CityLbl" runat="server" Width="100%" CssClass="form-control"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Address</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtAddress" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-5 form-label align-self-center mb-lg-0 text-end">Ethnic Background</label>
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
                                            <div class="col-md-5">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-6 form-label align-self-center mb-lg-0 text-end">Last Menstrual Period Date:</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="MenstrualPeriodTxt" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-5 form-label align-self-center mb-lg-0 text-end">Age of Gestation:</label>
                                                    <div class="col-sm-7">
                                                        <asp:Label ID="txtAgeOfGestation" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-5 form-label align-self-center mb-lg-0 text-end">Maternal Weight:</label>
                                                    <div class="col-sm-7">
                                                        <div class="input-group mb-3">
                                                            <asp:Label ID="txtMaternalWeight" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                            <span class="input-group-text" id="basic-addon2">Kg</span>
                                                        </div>
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
                                                    <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Marriage Consanguineous:</label>
                                                    <div class="col-sm-8">
                                                        <asp:Label ID="MarriageConsanguineousLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Mode of Conception:</label>
                                                    <div class="col-sm-8">
                                                        <asp:Label ID="ModeConceptionLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="mb-4 row">
                                                    <label for="example-tel-input" class="col-sm-5 form-label align-self-center mb-lg-0 text-end">History of genetic testing:</label>
                                                    <div class="col-sm-7">
                                                        <asp:Label ID="HistoryGeneticTestingLbl" runat="server" Width="100%" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="mb-3 row">
                                                    <label for="example-tel-input" class="col-sm-6 form-label align-self-center mb-lg-0 text-end">Latest Ultrasound:</label>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="LatestUltrasoundTxt" runat="server" CssClass="form-control"></asp:Label>
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
                                                    <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Name</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtRequestorName" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Mobile</label>
                                                    <div class="col-sm-9">
                                                        <asp:Label ID="txtRequestorMobile" runat="server" CssClass="form-control numberonly"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="mb-3 row">
                                                    <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Email</label>
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
                                    <!--end card-body-->
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                        <!--end col-->
                    </div>


                    <div class="col-lg-2">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h3 class="card-title">Attachments</h3>
                                    </div>
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="card-body">
                                    <div class="row">
                                        <asp:GridView ID="Img_Grid" runat="server" OnRowCommand="Img_Grid_RowCommand" AutoGenerateColumns="false" Width="100%" CssClass="table-light" GridLines="None" HeaderStyle-CssClass="table mb-0">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
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
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="myModal" class="modal fade" role="dialog">
                            <div class="modal-dialog modal-xl">
                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title">Report</h4>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <embed frameborder="0" width="100%" height="500px" id="report" runat="server" title="Test"/>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-bs-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
        $(document).ready(function () {
            $(document).on('click', "[id='btnPrint']", function () {
                $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif" /><br/>Just a moment...</h1>' });
                $('#myModal').modal('show');
                $.unblockUI();
            });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $(document).on('click', "[id='btnPrint']", function () {
                        $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif" /><br/>Just a moment...</h1>' });
                        $('#myModal').modal('show');
                        $.unblockUI();
                    });
                }
            });
        };
    </script>
</asp:Content>


