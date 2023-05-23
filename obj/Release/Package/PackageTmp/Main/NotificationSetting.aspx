<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="NotificationSetting.aspx.cs" Inherits="InternalLims.Main.NotificationSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="Form1" runat="server">
        <script src="../assets/js/jquery-2.1.1.js"></script>

        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Notification Setting</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active">Notification Setting</li>
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
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-xl-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Email Notiication</h4>
                                <p class="text-muted mb-0">
                                    Email Notiication when any Action is taken.
                                </p>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-12 col-lg-4 col-xl">
                                        <asp:GridView ID="NotiRpt" runat="server" CssClass="table table-hover mb-0 table-bordered" OnRowDataBound="NotiRpt_RowDataBound" HeaderStyle-CssClass="thead-light" GridLines="None" AutoGenerateColumns="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%--  <div class="card" id="div_id" runat="server">
                                                            <div class="card-body">
                                                                <div class="row align-items-center">
                                                                    <div class="col text-center">--%>
                                                        <span class="h4"><%# Eval("NotificationDesc") %><asp:Label ID="NotiControlId" runat="server" Text='<%# Eval("Serial") %>' Visible="false"></asp:Label></span>
                                                        <%-- <h6 class="text-uppercase text-muted mt-2 m-0">
                                                                            <asp:CheckBox ID="IdCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("hasaccess")) %>' Font-Size="16px" /></h6>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="IdCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("hasaccess")) %>' Font-Size="16px" /></h6>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <hr class='dotted' />
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Button ID="Confirm_Btn" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="Confirm_Btn_Click" ValidationGroup="Val" Enabled="True" />
                                        <a class="btn btn-secondary m-btn m-btn--custom" href="Default.aspx">Back To Dashboard</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="Confirm" tabindex="-1" role="dialog" aria-textbox ledby="exampleModalTextbox " aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalTextbox ">
                                            <asp:Label ID="Confirm_Header_Lbl" runat="server"></asp:Label></h5>
                                        <button type="button" class="close" data-bs-dismiss="modal" aria-textbox="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:Label ID="Confirm_Middle_Lbl" runat="server" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="Button2" runat="server" Text="Cancel" data-bs-dismiss="modal" CssClass="btn btn-secondary" />
                                        <asp:Button ID="Submit_Btn" runat="server" Text="Yes" UseSubmitBehavior="false" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClick="Submit_Btn_Click" />
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
            Calculate();
        };
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    Calculate();
                }
            });
        };

        function Calculate() {
            $("#ContentPlaceHolder1_StaffCat_imgtool_1,#ContentPlaceHolder1_StaffCat_imgtool_2,#ContentPlaceHolder1_StaffCat_imgtool_3,#ContentPlaceHolder1_StaffCat_imgtool_4,#ContentPlaceHolder1_StaffCat_imgtool_5,#ContentPlaceHolder1_StaffCat_imgtool_6,#ContentPlaceHolder1_StaffCat_imgtool_10,#ContentPlaceHolder1_StaffCat_imgtool_11,#ContentPlaceHolder1_StaffCat_imgtool_12,#ContentPlaceHolder1_StaffCat_imgtool_8").tooltip();
        };
    </script>

    <style>
        .blink {
            animation: blinker 1s linear infinite;
            animation: colorchange 1s ease 0s 1 normal forwards;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>

</asp:Content>