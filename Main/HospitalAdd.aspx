<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="HospitalAdd.aspx.cs" Inherits="InternalLims.Main.HospitalAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <!-- Page-Title -->
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Add New Hospital</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dastone</a></li>
                            <li class="breadcrumb-item active">New Hospital</li>
                        </ol>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </div>
            <!--end page-title-box-->
        </div>
        <!--end col-->
    </div>
    <!--end row-->
    <!-- end page title end breadcrumb -->
    <form runat="server">
        <div class="row">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Add New Hospital</h4>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-4 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Institute Name
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ControlToValidate="InstituteNameTxt" ValidationGroup="Val"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="InstituteNameTxt" runat="server" CssClass="form-control" ValidationGroup="Val" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">City<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ControlToValidate="CityDrop" ValidationGroup="Val" InitialValue="-1"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="CityDrop" runat="server" CssClass="form-control" ValidationGroup="Val" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-4 row">
                                            <label for="example-email-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Ownership<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ControlToValidate="OwnershipDrop" ValidationGroup="Val" InitialValue="-1"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="OwnershipDrop" runat="server" CssClass="form-control" ValidationGroup="Val" ></asp:DropDownList>
                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="mb-4 row">
                                            <label for="example-text-input" class="col-sm-5 form-label">Main/Branch<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ControlToValidate="MainBranchDrop" ValidationGroup="Val" InitialValue="-1"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="MainBranchDrop" runat="server" CssClass="form-control" ValidationGroup="Val"  AutoPostBack="true" OnSelectedIndexChanged="MainBranchDrop_SelectedIndexChanged">
                                                    <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                                    <asp:ListItem Text="Main Hospital" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Branch" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-5 form-label">Institute Name<asp:RequiredFieldValidator ID="InstituteNameValidate" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ControlToValidate="InstituteNameDrop" ValidationGroup="Val" InitialValue="-1"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-7">
                                                <asp:DropDownList ID="InstituteNameDrop" runat="server" CssClass="form-control" ValidationGroup="Val" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-4 form-label">CR Number<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ForeColor="Red" CssClass="fa fa-exclamation-circle pull-right red" SetFocusOnError="true" ControlToValidate="CRNumberTxt" ValidationGroup="Val"></asp:RequiredFieldValidator></label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="CRNumberTxt" runat="server" CssClass="form-control" ValidationGroup="Val" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="mb-8 row">
                                            <label for="example-url-input" class="col-sm-1 form-label">Address</label>
                                            <div class="col-sm-11">
                                                <asp:TextBox ID="AddressTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-3 form-label">Status</label>
                                            <div class="col-sm-9">
                                                <asp:CheckBox ID="StatusChk" runat="server" CssClass="form-control" Checked="true"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                

                                <div class="col-sm-12 text-end">
                                    <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Add Hospital" OnClick="Confirm_Btn_Click" ValidationGroup="Val"/>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!--end col-->
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
                                            <asp:Button ID="CancelBtn" runat="server" Text="Cancel" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
                                            <asp:Button ID="Submit_Btn" runat="server" Text="Submit" UseSubmitBehavior="false" data-bs-dismiss="modal" CssClass="btn btn-primary" OnClick="Submit_Btn_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
    <!--end row-->

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
              $("#ContentPlaceHolder1_CityDrop,#ContentPlaceHolder1_OwnershipDrop,#ContentPlaceHolder1_MainBranchDrop,#ContentPlaceHolder1_InstituteNameDrop").select2({ placeholder: '--' });
              $.unblockUI();
        };
      </script>

</asp:Content>
