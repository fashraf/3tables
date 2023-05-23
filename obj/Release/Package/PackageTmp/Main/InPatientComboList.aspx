<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientComboList.aspx.cs" Inherits="InternalLims.Main.InPatientComboList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Walk-In Coupon List</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item active"><a href="javascript:void(0);">Walk-In Coupon List </a></li>
                        </ol>
                    </div>
                    <!--end col-->
                    <div class="col-auto align-self-center">
                        <a href="InPatientPatList">
                            <button type="button" class="btn btn-gradient-dark"><i class="mdi mdi-account-details me-2"></i>Patient List</button>
                        </a>
                            <a href="InPatientComboAdd">
                            <button type="button" class="btn btn-gradient-pink">Add New Combo</button>
                        </a>
                        <a href="Default">
                            <button type="button" class="btn btn-primary"><i class="mdi mdi-keyboard-backspace me-2"></i>Dashboard</button>
                        </a>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </div>
            <!--end page-title-box-->
        </div>
        <!--end col-->
    </div>

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Walk-In Coupon List</h4>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <asp:GridView ID="Combo_Grid" runat="server" AutoGenerateColumns="false" EmptyDataText="No Data !" EmptyDataRowStyle-CssClass="alert alert-danger" HeaderStyle-CssClass="thead-light" CssClass="table dataTable table-hover" Width="100%" GridLines="None" OnRowCommand="Combo_Grid_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="Id_Lbl" runat="server" Text='<%#Eval("ComboId")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test">
                                                <ItemTemplate>
                                                <strong><%#Eval("TestName")%></strong>    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sub Test">
                                                <ItemTemplate>
                                                    <strong><%#Eval("SubTestName")%></strong>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Combo Name">
                                                <ItemTemplate>
                                                    <%#Eval("ComboName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Combo Type">
                                                <ItemTemplate>
                                                    <%#Eval("ComboType")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentage %">
                                                <ItemTemplate>
                                                    <%#Eval("Percentage")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <%#Eval("Amount", "{0:0.00}")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <%#Eval("Status")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" CommandArgument='<%# Eval("ComboId")%>' ImageUrl="../assets/images/view.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
    <script>
        $(function () {
            onlyinteger();
        });
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    onlyinteger();
                }
            });
        };
        function onlyinteger() {
            $("#ContentPlaceHolder1_AppDrop").select2({ placeholder: '------' });
            //$("#ContentPlaceHolder1_AppDrop").inputmask({ alias: "currency", prefix: '' });
            /// $("#ContentPlaceHolder1_TotalBids_Txt").keypress(function (e) { if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) { return false; } });
        };
    </script>
</asp:Content>
