<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientTestPricingList.aspx.cs" Inherits="InternalLims.Main.InPatientTestPricingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../assets/js/jquery-2.1.1.js"></script>
    <style type="text/css">
        div.dt-buttons {
            position: relative;
            float: right;
            margin-left: 5px;
        }

        .buttons-copy {
            display: none;
        }
    </style>

    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Test Cost List</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashbaord</a></li>
                                <li class="breadcrumb-item active"><a href="javascript:void(0);">Walk-In Test Cost List</a></li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="SubTestGris" Width="98%" HorizontalAlign="center" CssClass="table table-hover mb-0" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" AllowPaging="true" PageSize="10" DataKeyNames="Id" AllowSorting="true" EmptyDataText="No Records Found" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" AutoGenerateColumns="false" runat="server" OnRowCommand="SubTestGris_RowCommand" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ser.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test">
                                                <ItemTemplate>
                                                <strong>  <%# Eval("TestName") %>  - <%# Eval("SubTestName") %></strong>  
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost">
                                                <ItemTemplate>
                                                    <%# Eval("Cost") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="View_Userinfo" runat="server" ImageUrl="~/assets/images/view.png" CommandArgument='<%#Eval("Id")%>' CommandName="View" Font-Size="12px" ForeColor="White"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="pagination-ys" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
