<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="CancelTest.aspx.cs" Inherits="InternalLims.Main.CancelTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <link href="../assets/css/component.css" rel="stylesheet" />
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
                        <a href="NiptList.aspx">
                            <button type="button" class="btn btn-primary"><i class="mdi mdi-keyboard-backspace me-2"></i>Back to NIPT List</button>
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
                    <div class="col-lg-9">
                        <div class="card">
                            <div class="card-header" style="text-align: center">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="row align-items-center">
                                                    <div class="col">
                                                        <h4 class="card-title">Hospital/Institute Status</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end card-header-->
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col support-tickets">
                                                        <h2 class="fw-semibold">
                                                            <asp:Label ID="HospitalStatusLbl" runat="server" Width="100%"></asp:Label></h2>
                                                        <h6>
                                                            <asp:Label ID="SubmitDateLbl" runat="server" Width="100%" Font-Size="10px"></asp:Label></h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                    <div class="col-lg-4">
                                        <div class="card">
                                            <div class="card-header">
                                                <div class="row align-items-center">
                                                    <div class="col">
                                                        <h4 class="card-title">NOVO Status</h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <!--end card-header-->
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col support-tickets">
                                                        <h2 class="fw-semibold">
                                                            <asp:Label ID="StatusRibbon" runat="server" Width="100%"></asp:Label></h2>
                                                        <h6>
                                                            <asp:Label ID="InLabDtLbl" runat="server" Width="100%" Font-Size="10px"></asp:Label></h6>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6">
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
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-5 form-label align-self-center mb-lg-0 text-end">Requested Test</label>
                                                <div class="col-sm-7">
                                                    <asp:Label ID="Lbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">NIPT Created</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="CreatedLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-tel-input" class="col-sm-4 form-label">InstituteName</label>
                                                <div class="col-sm-8">
                                                    <asp:Label ID="InstituteNameLbl" runat="server" Text="&nbsp;" CssClass="form-control" Font-Bold="true"></asp:Label>
                                                    <asp:HiddenField ID="InstituteId" runat="server"></asp:HiddenField>
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



                                    <div class="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                        <div class="media">
                                            <i class="dripicons-cross alert-icon text-danger align-self-center font-18 me-3"></i>
                                            <div class="media-body align-self-center">
                                                <h5 class="mb-1 fw-bold mt-0">Cancel Test</h5>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="alert alert-light mb-0 row">
                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-4 form-label" style="font-size: 14px">
                                                    Change Status
                                                    <asp:RequiredFieldValidator ID="Statusreq" runat="server" InitialValue="-1" ControlToValidate="StatusDrop" CssClass="fa fa-exclamation-circle pull-right re" ForeColor="Red" Font-Bold="true" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="StatusDrop" runat="server" CssClass="form-control" OnSelectedIndexChanged="StatusDrop_SelectedIndexChanged" AutoPostBack="true" ValidationGroup="Val"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="mb-3 row">
                                                <label for="example-text-input" class="col-sm-4 form-label" style="font-size: 14px">
                                                    Rejection Reason
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="-1" ControlToValidate="RejectionDrop" CssClass="fa fa-exclamation-circle pull-right re" ForeColor="Red" Font-Bold="true" ValidationGroup="Val" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                                <div class="col-sm-8">
                                                    <asp:DropDownList ID="RejectionDrop" runat="server" CssClass="form-control" ValidationGroup="Val"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <label class="col-sm-3 form-label" style="font-size: 14px">Reason<asp:RequiredFieldValidator ID="CancelReq" runat="server" ControlToValidate="CancelTxt" ValidationGroup="Val" ForeColor="Red" Font-Bold="true" CssClass="fa fa-exclamation-circle pull-right re" SetFocusOnError="true"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-12">
                                                <asp:TextBox ID="CancelTxt" runat="server" CssClass="form-control" ValidationGroup="Val"></asp:TextBox>
                                            </div>
                                        </div>
                                        <hr class="hr-dashed hr-menu">
                                        <div class="col-sm-12 text-end">
                                            <asp:Button ID="Cancle_Lnk" runat="server" ValidationGroup="Val" CommandName="No" Text="Cancle Test" CssClass="btn btn-danger px-4" Height="100%" OnClick="Cancle_Lnk_Click" />
                                            <asp:Button ID="CancleRemoveBtn" runat="server" CommandName="No" Text="Remove Cancel Stats" CssClass="btn btn-success px-4" Height="100%" OnClick="CancleRemoveBtn_Click" />
                                        </div>
                                        <hr class="hr-dashed hr-menu">
                                        <div class="col-md-12">
                                            <div class="input-group mb-3">
                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                <asp:Button ID="UploadBtn" Text="Upload" runat="server" OnClick="UploadFile" CssClass="btn btn-primary" />
                                            </div>
                                            <a class="font-16 text-primary">You can Upload The Image For Cancellation.</a><br />
                                            <br />
                                            <asp:Label ID="lblMessageLbl" Font-Bold="true" Font-Size="16px" runat="server" Width="100%" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>




                    <div class="col-lg-3">
                        <div class="card">
                            <!--end card-header-->
                            <div class="card-body">
                                <asp:HiddenField ID="Status_hdn" runat="server" />
                                <asp:HiddenField ID="Status_Name" runat="server" />
                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between mb-0" role="alert">
                                    <div class="media">
                                        <i class="dripicons-alarm alert-icon text-primary align-self-center font-18 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Timeline</h5>
                                        </div>
                                    </div>
                                </div>
                                <asp:Repeater ID="TimelineRpt" runat="server" OnItemDataBound="TimelineRpt_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="activity">
                                            <div class="activity-info">
                                                <div class="icon-info-activity">
                                                    <asp:Label ID="serLbl" runat="server" Text='<%# Container.ItemIndex + 1 %>'></asp:Label>
                                                </div>
                                                <div class="activity-info-text">
                                                    <div class="d-flex justify-content-between align-items-center">
                                                        <p class="text-muted mb-0 font-13 w-75">
                                                            <span><%# Eval("TestStatus") %></span><br />
                                                            <a style="font-weight:700"><%# Eval("Name") %></a>
                                                        </p>
                                                        <small class="text-muted"><span><%# Eval("Dt","{0:dd.MMM.yy}") %></span> <span><%# Eval("Dt","{0:hh:mm tt}") %><br />
                                                            <asp:Label ID="dt_txt" runat="server" Text='<%# Eval("Dt") %>'></asp:Label>
                                                        </small>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>


                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between mb-0" role="alert">
                                    <div class="media">
                                        <i class="dripicons-cloud-upload alert-icon text-primary align-self-center font-20 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Attachment</h5>
                                        </div>
                                    </div>
                                </div>

                                <asp:GridView ID="Img_Grid" runat="server" OnRowCommand="Img_Grid_RowCommand" AutoGenerateColumns="false" Width="100%" CssClass="table-light" GridLines="None" HeaderStyle-CssClass="table mb-0">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <li class="list-group-item"><i class="la la-angle-double-right text-info me-2"><i class="dripicons-document"></i>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CausesValidation="False" CommandArgument='<%# Eval("FilePathName") %>' CommandName="Download" Text='<%# Eval("FileName") %>' CssClass="text-primary" />
                                                    <asp:Label ID="path_lbl" runat="server" Text='<%# Eval("FilePathName") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="name_lbl" runat="server" Text='<%# Eval("FileName") %>' Visible="false"></asp:Label><br />
                                                    <span class="badge badge-soft-danger px-2"><%# Eval("UploadType") %></span>
                                                </i>
                                                </li>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
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
                                            <asp:Button ID="Submit_Btn" runat="server" Text="Submit" data-bs-dismiss="modal" UseSubmitBehavior="false" CssClass="btn btn-primary" OnClick="Submit_Btn_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <%--Reject--%>
                    <div class="modal fade" id="Rejectmodal" tabindex="-1" role="dialog" aria-textbox ledby="exampleModalTextbox " aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">
                                                <asp:Label ID="Reject_Header_Lbl" runat="server"></asp:Label></h5>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:Label ID="Reject_Lbl" runat="server" Font-Size="16px"></asp:Label><br />
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="Button1" runat="server" Text="Cancel" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
                                            <asp:Button ID="RejectBtn" runat="server" Text="Submit" data-bs-dismiss="modal" UseSubmitBehavior="false" CssClass="btn btn-primary" OnClick="RemoveBtn_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="UploadBtn" />
            </Triggers>
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
            $("#ContentPlaceHolder1_StatusDrop,#ContentPlaceHolder1_RejectionDrop").select2({ placeholder: '--' });
        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                        $("#ContentPlaceHolder1_StatusDrop,#ContentPlaceHolder1_RejectionDrop").select2({ placeholder: '--' });
                }
            });
        };
    </script>
</asp:Content>