<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InternalLims.Main.Default" %>

<%@ Register Src="~/Main/uc/NIPTHeatMap.ascx" TagPrefix="uc1" TagName="NIPTHeatMap" %>
<%@ Register Src="~/Main/uc/TaTPieChart.ascx" TagPrefix="uc1" TagName="TaTPieChart" %>
<%@ Register Src="~/Main/uc/TATDonut.ascx" TagPrefix="uc1" TagName="TATDonut" %>
<%@ Register Src="~/Main/uc/TestBarChart.ascx" TagPrefix="uc1" TagName="TestBarChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../assets/plugins/fullcalendar/packages/bootstrap/main.min.css" rel="stylesheet" />
    <link href="../assets/plugins/fullcalendar/packages/core/main.css" rel="stylesheet" />
    <script src="../assets/steppedbar/stepped-bar.js"></script>
    <link href="../assets/steppedbar/style.css" rel="stylesheet" />
    <form runat="server">
        <div class="container-fluid">
            <!-- Page-Title -->
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h3>Welcome to NOVO Admin Dashbaord</h3>
                                <asp:Label ID="TimeOftheDayLbl" runat="server" CssClass="text-dark mb-0 fw-semibold" Font-Size="14px"></asp:Label>

                            </div>
                            <!--end col-->
                            <div class="col-auto align-self-center">
                                <a class="btn btn-light" id="Dash_Date">
                                    <%--<asp:Label ID="dt" runat="server" CssClass="text-dark mb-0 fw-bold"></asp:Label>--%><span id="dt"></span>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--end row-->
            <!-- end page title end breadcrumb -->
            <div class="row">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                    </div>
                </div>
            </div>

            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-3">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold">Submited Today</p>
                                    <h2 class="m-0">
                                        <asp:Label ID="SubmitedLbl" runat="server"></asp:Label></h2>
                                    <p class="mb-0 text-truncate text-muted"><span class="text-danger">Submitted by <strong>Hospital</strong></span></p>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <img src="../assets/images/icon/submiited.png" height="72" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
                <!--end col-->
                <div class="col-md-6 col-lg-3">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold">Recieved at NOVO</p>
                                    <h2 class="m-0">
                                        <asp:Label ID="RecievedatNOVOLbl" runat="server"></asp:Label></h2>
                                    <p class="mb-0 text-truncate text-muted"><span class="text-danger">Recieved at  <strong>NOVO Today</strong></span></p>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <img src="../assets/images/icon/recieve.png" height="72" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
                <!--end col-->
                <div class="col-md-6 col-lg-3">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold">In Lab</p>
                                    <h2 class="m-0">
                                        <asp:Label ID="InLabLbl" runat="server"></asp:Label></h2>
                                    <p class="mb-0 text-truncate text-muted"><span class="text-danger">Accepted in Lab <strong>Today</strong></span></p>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <img src="../assets/images/icon/inlab.png" height="72" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
                <!--end col-->
                <div class="col-md-6 col-lg-3">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold">Result</p>
                                    <h2 class="m-0">
                                        <asp:Label ID="ResultLbl" runat="server"></asp:Label></h2>
                                    <p class="mb-0 text-truncate text-muted"><span class="text-danger">Result Released <strong>Today</strong></span></p>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <img src="../assets/images/icon/result.png" height="72" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end card-body-->
                    </div>
                    <!--end card-->
                </div>
                <!--end col-->
            </div>

            <div class="row justify-content-center">
                <div class="col-md-6 col-lg-3">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold">Delivery Today</p>
                                    <h2 class="m-0">
                                        <asp:Label ID="DeliveryLbl" runat="server"></asp:Label></h2>
                                </div>
                                <div class="col-auto align-self-center">
                                    <div class="report-main-icon bg-light-alt">
                                        <img src="../assets/images/icon/Travel.png" height="72" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Repeater ID="UserRpt" runat="Server">
                    <ItemTemplate>
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">Total Users</p>
                                            <h3 class="m-0"><%# Eval("TotalUsers") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <img src="../assets/images/icon/group.png" height="72" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end col-->
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">New User Registration</p>
                                            <h3 class="m-0"><%# Eval("NewUserRegistration") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <img src="../assets/images/icon/unknown.png" height="72" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--end col-->
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">Active Users</p>
                                            <h3 class="m-0"><%# Eval("UserActive") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <img src="../assets/images/icon/activeuser.png" height="72" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

            </div>






            <%--            <div class="card-body">
                <div data-stepped-bar></div>
            </div>--%>

            <div class="row">
                <!--end col-->
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h4 class="card-title">Kit Request</h4>
                                </div>
                                <!--end col-->
                            </div>
                            <!--end row-->
                        </div>
                        <!--end card-header-->
                        <asp:Repeater ID="NiptRequestList" runat="Server">
                            <ItemTemplate>
                                <div class="card-body border-bottom-dashed">
                                    <div class="earning-data text-center">
                                        <h4 class="earn-money mb-1">
                                            <img src="../assets/images/icon/pendingkit.png" height="35" />
                                            <%# Eval("Pending") %></h4>
                                        <p class="text-dark mb-0 fw-bold">
                                            Waiting For Approval</a>
                                    </div>
                                </div>
                                <!--end card-body-->
                                <div class="card-body" style="text-align: center">
                                    <div class="row">
                                        <div class="col">
                                            <div class="media">
                                                <div class="media-body align-self-center">
                                                    <h4 class="m-0 font-24">
                                                        <img src="../assets/images/icon/totalKit.png" height="30" /><%# Eval("Total") %></h4>
                                                    <p class="text-dark mb-0 fw-bold">Total Kits</p>
                                                </div>
                                            </div>
                                        </div>
                                        <!--end col-->
                                        <div class="col">
                                            <div class="media">
                                                <div class="media-body align-self-center">
                                                    <h4 class="m-0 font-24">
                                                        <img src="../assets/images/icon/kitcomplete.png" height="30" /><%# Eval("Completed") %></h4>
                                                    <p class="text-dark mb-0 fw-bold">Approved Kits</p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h4 class="card-title">Test Kit Inventory</h4>
                                </div>
                            </div>
                        </div>
                        <!--end card-header-->
                        <div class="card-body">
                            <asp:GridView ID="NiptStatusGrid" runat="server" AutoGenerateColumns="False" CssClass="table mb-0 table-centered" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" FooterStyle-CssClass="thead-light" FooterStyle-Font-Bold="true" ShowFooter="true" Width="100%" GridLines="None" OnRowDataBound="NiptStatusGrid_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Test Name">
                                        <ItemTemplate>
                                            <p class="text-dark mb-0 fw-bold"><%# Eval("TestName") %></p>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTestName" runat="server" Font-Bold="true" Font-Size="14px" Text="Total" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Used">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsed" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("Used")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblUsedFooter" runat="server" Font-Bold="true" Font-Size="14px" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" BackColor="#E1FFF0" ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Center" BackColor="#E1FFF0" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Not Used">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNotUsed" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("NotUsed")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNotUsedFooter" runat="server" Font-Bold="true" Font-Size="14px" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" BackColor="#FFE6E1" ForeColor="Black" />
                                        <FooterStyle HorizontalAlign="Center" BackColor="#FFE6E1" ForeColor="Black" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("Total")%>' />
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalFooter" runat="server" Font-Bold="true" Font-Size="14px" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage">
                                        <ItemTemplate>
                                            <p class="text-dark mb-0 fw-bold"><%# Eval("Percentage")%>%</p>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Graph">
                                        <ItemTemplate>
                                            <div class="progress mb-3">
                                                <div class="progress-bar" role="progressbar" style="width: <%# Eval("Percentage")%>%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100"></div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <!--end card-body-->
                    </div>
                </div>
            </div>


            <!--end row-->
            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">
                                            <img src="../assets/images/icon/hospital.png" height="45" />Hospital who Submitted Test Today</h4>
                                    </div>
                                    <!--end col-->
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="analytic-dash-activity" data-simplebar="init">
                                    <div class="simplebar-wrapper" style="margin: 0px;">
                                        <div class="simplebar-height-auto-observer-wrapper">
                                            <div class="simplebar-height-auto-observer"></div>
                                        </div>
                                        <div class="simplebar-mask">
                                            <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                                                <div class="simplebar-content-wrapper" style="height: 100%; overflow: hidden scroll;">
                                                    <div class="simplebar-content" style="padding: 0px;">
                                                        <asp:GridView ID="HospitalTestGrid" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" EmptyDataText="No Request to show !" CssClass="table table-bordered mb-0 table-centered" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" Width="100%" GridLines="None">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <p class="font-54 fw-normal mt-0 mb-4" style="font-size: 16px">
                                                                            <img src="../assets/images/point.png" height="14" />
                                                                            <%#Container.DataItemIndex+1 %>.   <%# Eval("InstituteName") %>
                                                                        </p>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nipt Basic">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUsed" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("NiptBasic")%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nipt Plus">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNotUsed" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("NiptPlus")%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Total">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotal" Font-Bold="true" Font-Size="14px" runat="server" Text='<%# Eval("Total")%>' />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="simplebar-placeholder" style="width: auto; height: 395px;"></div>
                                    </div>
                                    <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                                        <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
                                    </div>
                                    <div class="simplebar-track simplebar-vertical" style="visibility: visible;">
                                        <div class="simplebar-scrollbar" style="height: 301px; transform: translate3d(0px, 44px, 0px); display: block;"></div>
                                    </div>
                                </div>
                                <!--end analytics-dash-activity-->
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <div class="col-6">
                        <div class="card">
                            <div class="card-header">
                                <div class="row align-items-center">
                                    <div class="col">
                                        <h4 class="card-title">
                                            <img src="../assets/images/icon/infographic.png" height="45" />Request Per Day</h4>
                                    </div>
                                    <!--end col-->
                                </div>
                                <!--end row-->
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <uc1:TestBarChart runat="server" ID="TestBarChart" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>








            <div class="row justify-content-center">
                <asp:Repeater ID="Repeater1" runat="Server">
                    <ItemTemplate>
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">Sessions</p>
                                            <h3 class="m-0"><%# Eval("TotalUsers") %></h3>
                                            <p class="mb-0 text-truncate text-muted"><span class="text-success"></span>Total Registered Users</p>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-users align-self-center text-muted icon-sm">
                                                    <path d="M17 21v-2a4 4 0 0 0-4-4H5a4 4 0 0 0-4 4v2"></path><circle cx="9" cy="7" r="4"></circle><path d="M23 21v-2a4 4 0 0 0-3-3.87"></path><path d="M16 3.13a4 4 0 0 1 0 7.75"></path></svg>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                        <!--end col-->
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">New Registration</p>
                                            <h3 class="m-0"><%# Eval("NewUserRegistration") %></h3>
                                            <p class="mb-0 text-truncate text-muted"><span class="text-success"></span>Users waiting for Approval</p>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <i class="dripicons-archive"></i>.
                                        </div>
                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                        <!--end col-->
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">Active Users</p>
                                            <h3 class="m-0"><%# Eval("UserActive") %></h3>
                                            <p class="mb-0 text-truncate text-muted"><span class="text-success"></span>User who are Active</p>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-activity align-self-center text-muted icon-sm">
                                                    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"></polyline></svg>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                        <!--end col-->
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">Inactive Users</p>
                                            <h3 class="m-0"><%# Eval("UserInactive") %></h3>
                                            <p class="mb-0 text-truncate text-muted"><span class="text-success"></span>User who are Inactive</p>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-briefcase align-self-center text-muted icon-sm">
                                                    <rect x="2" y="7" width="20" height="14" rx="2" ry="2"></rect><path d="M16 21V5a2 2 0 0 0-2-2h-4a2 2 0 0 0-2 2v16"></path></svg>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                        <!--end col-->
                    </ItemTemplate>
                </asp:Repeater>
            </div>



            <div class="row">
                <div class="col-lg-12 col-xl-6">
                    <div class="card">
                        <div class="card-header">
                            <h4 class="card-title">
                                <img src="../assets/images/icon/warning.png" height="35" />Turnaround Time</h4>
                        </div>
                        <div class="card-body">
                            <uc1:TATDonut runat="server" ID="TATDonut" />
                        </div>
                    </div>
                </div>

                <div class="col-lg-12 col-xl-6">
                    <div class="card">
                        <div class="card-header">
                            <h4 class="card-title">
                                <img src="../assets/images/icon/calc.png" height="35" />Heat Map of Submitted Test</h4>
                        </div>
                        <div class="card-body">
                            <uc1:NIPTHeatMap runat="server" ID="NIPTHeatMap" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        var j = [<%=GetTypeChart() %>];
        var defaults = {
            catagories: j,
        };



        $(document).ready(function () {
            var interval = setInterval(function () {var momentNow = moment();
                $("#dt").html(momentNow.format("hh:mm:ss A") + '</br>' + momentNow.format("dddd") + ', ' + momentNow.format("DD ") + ' ' + momentNow.format("MMMM YYYY"));
            }, 100);
        });

    </script>
</asp:Content>