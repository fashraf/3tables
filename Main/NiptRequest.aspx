<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="NiptRequest.aspx.cs" Inherits="InternalLims.Main.NiptRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../assets/js/jquery-2.1.1.js"></script>
    <script src="../assets/js/nipt.js"></script>
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
                </div>
            </div>
        </div>
    </div>

    <form runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h3 class="card-title">Non-invasive prenatal testing (NIPT)</h3>
                                    </div>
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Hospital</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group mb-0">
                                                        <asp:Label ID="BarcodeLbl" runat="server" Font-Bold="true" Font-Size="24px"></asp:Label>
                                                        <asp:Label ID="TestIdLbl" runat="server" ForeColor="White"></asp:Label>
                                                        <asp:Label ID="PatientLbl" runat="server" ForeColor="White"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Test</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group mb-0">
                                                        <asp:Label ID="TestNameLbl" runat="server" Font-Bold="true" Font-Size="24px"></asp:Label>
                                                    </div>
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
                                                <label for="example-text-input" class="col-sm-4 form-label">National Id/Iqama/Passport</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group mb-0">
                                                        <asp:Label ID="txtNationalID" runat="server" CssClass="form-control"></asp:Label>
                                                    </div>
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


                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">First Name</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="txtFirstName" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-email-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Middle Name</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="txtMiddleName" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-email-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Last Name</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="txtLastName" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-date-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Date OF Birth*</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="DobTxt" runat="server" CssClass="form-control"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Mobile</label>
                                                <div class="col-sm-9">
                                                    <asp:Label ID="txtMobile" runat="server" CssClass="form-control"></asp:Label>
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
                                                    <asp:DropDownList ID="CityDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                                    <asp:DropDownList ID="EthnicDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="-1" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                                <label for="example-tel-input" class="col-sm-5 form-label">Last Menstrual Period Date:*<asp:RequiredFieldValidator Id="RequiredFieldValidator3" runat="server" ControlToValidate="MenstrualPeriodTxt" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <asp:TextBox ID="MenstrualPeriodTxt" runat="server" CssClass="form-control restrictinput"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-5 form-label">Age of Gestation:*(weeks)<asp:RequiredFieldValidator Id="AgeofGestationReq" runat="server" ControlToValidate="txtAgeOfGestation" InitialValue="0" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="txtAgeOfGestation" runat="server" Width="100%" AppendDataBoundItems="true" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                                        <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                                        <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                                        <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                                        <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                                        <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-5 form-label">Pregnancy Type:*<asp:RequiredFieldValidator Id="RequiredFieldValidator2" runat="server" ControlToValidate="PregnancyTypeDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="PregnancyTypeDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Value="-1" Text="Select"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-5">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label">Maternal Weight:*<asp:RequiredFieldValidator Id="RequiredFieldValidator5" runat="server" ControlToValidate="txtMaternalWeight" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <div class="input-group mb-3">
                                                        <asp:TextBox ID="txtMaternalWeight" runat="server" CssClass="form-control numberonly" MaxLength="4"></asp:TextBox>
                                                        <span class="input-group-text" id="basic-addon2">Kg</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-5 form-label">Maternal Height:*<asp:RequiredFieldValidator Id="RequiredFieldValidator4" runat="server" ControlToValidate="txtMaternalheight" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <div class="input-group mb-3">
                                                        <asp:TextBox ID="txtMaternalheight" runat="server" CssClass="form-control numberonly" MaxLength="4"></asp:TextBox>
                                                        <span class="input-group-text">cm</span>
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
                                                <label for="example-tel-input" class="col-sm-4 form-label">Consanguineous Marriage :<br />
                                                    <code>Relative Marriage</code></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="MarriageConsanguineousDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Mode of Conception:*<asp:RequiredFieldValidator Id="RequiredFieldValidator6" runat="server" ControlToValidate="ModeConceptionDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="ModeConceptionDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="mb-4 row">
                                                <label for="example-tel-input" class="col-sm-5 form-label">History of genetic testing:</label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="HistoryGeneticTestingDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Others:<span id="spanOthersMandatory" style="display: none">*</span></label>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-5">
                                            <div class="mb-4 row">
                                                <label for="example-tel-input" class="col-sm-5 form-label align-self-center mb-lg-0 text-end">History of Abortion:*<asp:RequiredFieldValidator Id="RequiredFieldValidator7" runat="server" ControlToValidate="HistoryofAbortionDrop" InitialValue="-1" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <asp:DropDownList ID="HistoryofAbortionDrop" runat="server" Width="100%" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
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
                                                <label for="example-tel-input" class="col-sm-4 form-label">Latest Ultrasound:*<asp:RequiredFieldValidator Id="RequiredFieldValidator8" runat="server" ControlToValidate="LatestUltrasoundTxt" CssClass="fa fa-exclamation-circle pull-right red" ForeColor="Red" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-8">
                                                    <asp:TextBox ID="LatestUltrasoundTxt" runat="server" CssClass="form-control restrictinput"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group row">
                                                <label for="exampleInputUsername2" class="col-sm-5 form-label">Sample Collection Date & Time:*<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Bold="true" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" ControlToValidate="SampleCollectionTxt" SetFocusOnError="true" ValidationGroup="Val"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-7">
                                                    <div class="input-group datepicker">
                                                        <asp:HiddenField ID="hdnEvent_Dt_Txt" runat="server" />
                                                        <asp:TextBox ID="SampleCollectionTxt" runat="server" CssClass="form-control datetimepicker restrictinput" ValidationGroup="Val"></asp:TextBox><span class="input-group-addon" aria-disabled="true"><svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-calendar"><rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect><line x1="16" y1="2" x2="16" y2="6"></line><line x1="8" y1="2" x2="8" y2="6"></line><line x1="3" y1="10" x2="21" y2="10"></line></svg></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="mb-3">
                                                <label class="form-label" for="message">Ultrasound findings</label>
                                                <textarea class="form-control" rows="3" id="txtUltrasoundFindings" runat="server" maxlength="500"></textarea>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="mb-3">
                                                <label class="form-label" for="message">Further Clinical Details</label>
                                                <textarea class="form-control" rows="3" id="txtFurtherClinicalDetails" runat="server" maxlength="500"></textarea>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                        <div class="media">
                                            <i class="dripicons-pencil alert-icon text-primary align-self-center font-18 me-3"></i>
                                            <div class="media-body align-self-center">
                                                <h5 class="mb-1 fw-bold mt-0">Request Submitted By</h5>
                                                <span>User who have Requested.</span>
                                            </div>
                                        </div>
                                        <%--   <div class="button-items">
                                            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#bd-example-modal-xl1">
                                                <i class="mdi mdi-check-all me-2"></i>Search Requester
                                            </button>
                                            <button id="btnClearRequest" type="button" class="btn btn-danger"><i class="mdi mdi-alert-outline me-2"></i>Clear</button>
                                        </div>--%>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-1 form-label align-self-center mb-lg-0 text-end">Name</label>
                                                <div class="col-sm-11">
                                                    <asp:TextBox ID="txtRequestorName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="form-check">
                                            <br />
                                            <br />
                                            <h5 class="mb-0 fw-bold">Please make sure the Patient Has signed the Consent Form. This Can be uploaded later.</h5>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 text-end">
                                              <asp:Button ID="ConfirmBtn" runat="server" CssClass="btn btn-primary px-4" Text="Submit" OnClick="ConfirmBtn_Click" ValidationGroup="Val" />
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
                                                    <div class="alert alert-success border-0" role="alert">Are you sure you want to <strong>Add this NIPT Information ?</strong></div>
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

            //OnLoad();

            if ($('#datetimepicker').length) {var date = new Date(); var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                $('#datetimepicker').datepicker({ format: "dd/MM/yyyy", todayHighlight: true, autoclose: true, orientation: "bottom left", endDate: new Date(), buttonImageOnly: true }); $('#datetimepicker').datepicker('setDate', today);
            }

            $("#ContentPlaceHolder1_HospitalDrop,#ContentPlaceHolder1_BarcodeDrop,#ContentPlaceHolder1_CityDrop,#ContentPlaceHolder1_ActiveBarcodeDrop,#ContentPlaceHolder1_MarriageConsanguineousDrop,#ContentPlaceHolder1_PregnancyTypeDrop,#ContentPlaceHolder1_HistoryofAbortionDrop,#ContentPlaceHolder1_ModeConceptionDrop,#ContentPlaceHolder1_HistoryGeneticTestingDrop,#ContentPlaceHolder1_EthnicDrop,#ContentPlaceHolder1_SingletonTwinsDrop,#ContentPlaceHolder1_FamilyMemberDrop,#ContentPlaceHolder1_RelationDrop,#ContentPlaceHolder1_GenderDrop,#ContentPlaceHolder1_txtAgeOfGestation").select2({ placeholder: '--' });
            $('#ContentPlaceHolder1_DobTxt,#ContentPlaceHolder1_LatestUltrasoundTxt,#ContentPlaceHolder1_MenstrualPeriodTxt').datepicker({ dateFormat: 'yyyy', format: "dd/mm/yyyy", todayHighlight: true, autoclose: true, orientation: 'bottom', endDate: "today" });
            $('.datetimepicker').datetimepicker({ format: 'dd/mm/yyyy HH:ii p', weekStart: 7, todayBtn: 1, autoclose: 1, todayHighlight: 1, startView: 2, forceParse: 0, showMeridian: 1, minuteStep: 1, startDate: "2021-01-1 06:00", endDate: new Date() });
            //ClearPatientData();
        });//onlyinteger();});

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                   
                    //OnLoad();
                    if ($('#datetimepicker').length) {var date = new Date(); var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
                        $('#datetimepicker').datepicker({ format: "dd/MM/yyyy", todayHighlight: true, autoclose: true, orientation: "bottom left", endDate: new Date(), buttonImageOnly: true }); $('#datetimepicker').datepicker('setDate', today);}

                    $("#ContentPlaceHolder1_HospitalDrop,#ContentPlaceHolder1_BarcodeDrop,#ContentPlaceHolder1_CityDrop,#ContentPlaceHolder1_ActiveBarcodeDrop,#ContentPlaceHolder1_MarriageConsanguineousDrop,#ContentPlaceHolder1_PregnancyTypeDrop,#ContentPlaceHolder1_HistoryofAbortionDrop,#ContentPlaceHolder1_ModeConceptionDrop,#ContentPlaceHolder1_HistoryGeneticTestingDrop,#ContentPlaceHolder1_EthnicDrop,#ContentPlaceHolder1_SingletonTwinsDrop,#ContentPlaceHolder1_FamilyMemberDrop,#ContentPlaceHolder1_RelationDrop,#ContentPlaceHolder1_GenderDrop,#ContentPlaceHolder1_txtAgeOfGestation").select2({ placeholder: '--' });
                    $('#ContentPlaceHolder1_DobTxt,#ContentPlaceHolder1_LatestUltrasoundTxt,#ContentPlaceHolder1_MenstrualPeriodTxt').datepicker({ dateFormat: 'yyyy', format: "dd/mm/yyyy", todayHighlight: true, autoclose: true, orientation: 'bottom', endDate: "today" });
                    $('.datetimepicker').datetimepicker({ format: 'dd/mm/yyyy HH:ii p', weekStart: 7, todayBtn: 1, autoclose: 1, todayHighlight: 1, startView: 2, forceParse: 0, showMeridian: 1, minuteStep: 1, startDate: "2021-01-1 06:00", endDate: new Date() });
                    //ClearPatientData();
                }
            });
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
