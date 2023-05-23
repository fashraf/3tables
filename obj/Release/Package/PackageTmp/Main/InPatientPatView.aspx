<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientPatView.aspx.cs" Inherits="InternalLims.Main.InPatientPatView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script src="../assets/js/jquery-2.1.1.js"></script>
    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Patient Detail</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active">Patient Detail</li>
                            </ol>
                        </div>
                        <div class="col-auto align-self-center">
                            <asp:Button ID="AddTestBtn" runat="server" OnClick="AddTestBtn_Click" CssClass="btn btn-gradient-danger" Text="Add Test" Width="120px" />
                            <a href="InPatientPatList.aspx">
                                <button type="button" class="btn btn-gradient-info"><i class="mdi mdi-format-list-numbered me-2"></i>Patient List</button>
                            </a>
                            <a href="Default">
                                <button type="button" class="btn btn-primary"><i class="mdi mdi mdi-monitor-dashboard me-2"></i>Dashboard</button>
                            </a>
                            <%--  <a href="Default.aspx">
                                <asp:Button Id="Edit_Btn" runat="server" CssClass="btn btn-gradient-dark" OnClick="Edit_Btn_Click" Text="Edit This Patient"></asp:Button>
                            </a>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-body" style="position: relative;">
                        <asp:Repeater ID="PatRpt" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col-lg-6 align-self-center mb-3 mb-lg-0">
                                        <div class="dastone-profile-main">
                                            <div class="dastone-profile-main-pic">
                                                <h3 class="font-54 fw-normal mt-0 mb-0"><%# Eval("FullName") %></h3>
                                            </div>
                                            <div class="dastone-profile_user-detail">
                                                <h4 class="dastone-user-name">MRN#:-<%# Eval("MRN") %></h4>
                                                <p class="mb-0 dastone-user-name-post">DOB:<%# Eval("DOB", "{0:dd.MMM.yyyy}") %>.<strong><%# Eval("ActualAge") %>years</strong>,  <%# Eval("Gender") %></p>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-3 ms-auto align-self-center">
                                        <ul class="list-unstyled personal-detail mb-0">
                                            <li class=""><i class="ti ti-mobile me-2 text-secondary font-16 align-middle"></i><b>phone </b>: <%# Eval("Mobile") %></li>
                                            <li class="mt-2"><i class="ti ti-email text-secondary font-16 align-middle me-2"></i><b>Email </b>: <%# Eval("Email") %></li>
                                            <li class="mt-2"><i class="ti-location-pin text-secondary font-16 align-middle me-2"></i><b>City</b>: <%# Eval("City") %>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="col-lg-3 ms-auto align-self-center">
                                        <ul class="list-unstyled personal-detail mb-0">
                                            <li class=""><i class="ti ti-credit-card me-2 text-secondary font-16 align-middle"></i><b>National/ Iqama Id</b>: <%# Eval("NationalId") %></li>
                                           <%-- <li class="mt-2"><i class="ti ti-calendar text-secondary font-16 align-middle me-2"></i><b>Total Tests </b>: 2</li>--%>
                                            <li class="mt-2"><i class="ti-pencil text-secondary font-16 align-middle me-2"></i><b>Ethnic Background</b>: <%# Eval("EthnicBackground") %>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-12">
                <div class="card">
                    <div class="card-header">
                        <div class="row align-items-center">
                            <div class="col">
                                <h3 class="card-title">Tests for this Patient</h3>
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="PatTestList" Width="98%" HorizontalAlign="center" CssClass="table table-hover mb-0" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" AllowPaging="true" PageSize="10" DataKeyNames="TestId" AllowSorting="true" EmptyDataText="No Records Found" AutoGenerateColumns="false" OnPageIndexChanging="PatTestList_PageIndexChanging" runat="server" OnRowCommand="PatTestList_RowCommand" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Ser.No">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Barcode">
                                            <ItemTemplate>
                                                <strong><%# Eval("Barcode") %></strong>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Font-Size="14px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Test Name">
                                            <ItemTemplate>
                                                <strong><%# Eval("TestName") %></strong>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Font-Size="14px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubTest Name">
                                            <ItemTemplate>
                                                <%# Eval("SubTestName") %>
                                            </ItemTemplate>
                                            <ItemStyle Width="25%" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CreatedDt">
                                            <ItemTemplate>
                                                <%# Eval("CreatedDt") %>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NIPTStatus">
                                            <ItemTemplate>
                                                <%# Eval("TestStatus") %>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" Font-Size="16px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="View_Userinfo" runat="server" ImageUrl="~/assets/images/view.png" CommandArgument='<%#Eval("TestId")%>' CommandName="View" Font-Size="12px" ForeColor="White"></asp:ImageButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>



