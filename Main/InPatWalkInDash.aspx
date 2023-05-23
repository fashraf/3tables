<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatWalkInDash.aspx.cs" Inherits="InternalLims.Main.InPatWalkInDash" %>

<%@ Register Src="~/Main/uc/InPatTimeLineGraph.ascx" TagPrefix="uc1" TagName="InPatTimeLineGraph" %>
<%@ Register Src="~/Main/uc/InPatPaymentTypeDonut.ascx" TagPrefix="uc1" TagName="InPatPaymentTypeDonut" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../assets/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <script src="../assets/morris/raphael-2.1.0.min.js"></script>
    <script src="../assets/morris/Gruntfile.js"></script>
    <script src="../assets/morris/morris.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Walk-In Dashboard</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                            <li class="breadcrumb-item active">Walk-In Dashboard</li>
                        </ol>
                    </div>
                    <!--end col-->
                    <div class="col-auto align-self-center">
                        <a href="InPatientPatList" class="btn btn btn-primary">
                            <span class="ay-name" id="Day_Name">Patient List</span>&nbsp;
                        </a>
                        <a href="InPatientSubmitList" class="btn btn btn-dark">
                           Test List
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



    <div class="row">
        <div class="col-lg-12" style="text-align: center">
            <div class="row">
                <div class="col-md-6 col-lg-4">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold"></p>
                                    <img src="../assets/images/addpat.png" height="80" />
                                    <h3 class="m-0">1.Add Patient</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end col-->
                <div class="col-md-6 col-lg-4">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold"></p>
                                    <img src="../assets/images/pament.png" height="80" />
                                    <h3 class="m-0">2.Make Payment</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--end col-->
                <div class="col-md-6 col-lg-4">
                    <div class="card report-card">
                        <div class="card-body">
                            <div class="row d-flex justify-content-center">
                                <div class="col">
                                    <p class="text-dark mb-0 fw-semibold"></p>
                                    <img src="../assets/images/takeblood.png" height="80" />
                                    <h3 class="m-0">3.Extract Blood</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <asp:Repeater ID="DataCountRepeater" runat="server">
        <ItemTemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-md-6 col-lg-3">
                            <div class="card report-card">
                                <div class="card-body">
                                    <div class="row d-flex justify-content-center">
                                        <div class="col">
                                            <p class="text-dark mb-0 fw-semibold">Today</p>
                                            <h3 class="m-0"><%# Eval("today") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <i class="dripicons-user" style="height=45"></i>
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
                                            <p class="text-dark mb-0 fw-semibold">Yesterday</p>
                                            <h3 class="m-0"><%# Eval("Yesterday") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <i class="dripicons-clock" style="height=45"></i>
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
                                            <p class="text-dark mb-0 fw-semibold">This Month</p>
                                            <h3 class="m-0"><%# Eval("ThisMonth") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <i class="dripicons-calendar" style="height=45"></i>
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
                                            <p class="text-dark mb-0 fw-semibold">All Patients Registered</p>
                                            <h3 class="m-0"><%# Eval("TotalPatients") %></h3>
                                        </div>
                                        <div class="col-auto align-self-center">
                                            <div class="report-main-icon bg-light-alt">
                                                <i class="dripicons-user-group" style="height=45"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>


    <div class="row">
        <div class="col-lg-8">
            <div class="card">
                <div class="card-header">
                    <h4 class="card-title">Sales This Year</h4>
                </div>
                <!--end card-header-->
                <div class="card-body">
                    <uc1:InPatTimeLineGraph runat="server" id="InPatTimeLineGraph" />
                </div>
                <!--end card-body-->
            </div>
            <!--end card-->
        </div>
        <!--end col-->
        <div class="col-lg-4">
            <div class="card">
                <div class="card-header">
                    <div class="row align-items-center">
                        <div class="col">
                            <h4 class="card-title">Payment Type</h4>
                        </div>
                        <!--end col-->
                    </div>
                    <!--end row-->
                </div>
                <!--end card-header-->
                <div class="card-body">
                    <uc1:InPatPaymentTypeDonut runat="server" id="InPatPaymentTypeDonut" />
                </div>
                <!--end card-body-->
            </div>
            <!--end card-->
        </div>
        <!--end col-->
    </div>

</asp:Content>
