<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="NiptReportList.aspx.cs" Inherits="InternalLims.Main.NiptReportList" %>
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
        .blink {
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }
        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }
    </style>

    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Report List</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                                <li class="breadcrumb-item active"><a href="#">Institute List</a></li>
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
                    <div class="col-md-12 grid-margin stretch-card">
                        <div class="card">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label">Barcode</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="BarcodeTxt" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label">Date Range</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="DateTxt" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label">Institute</label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="Institute_Drop" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-2 form-label">Test</label>
                                            <div class="col-sm-10">
                                                <asp:DropDownList ID="Test_Drop" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <div class="col-lg-12">
                                                <asp:Button ID="Search_Btn" runat="server" CssClass="btn btn-primary btn-icon-text mr-2 mb-2 mb-md-0" Text="Search Flter" OnClick="Search_Btn_Click" ValidationGroup="Val" />
                                                <asp:Button ID="Clear_Btn" runat="server" CssClass="btn btn-danger btn-icon-text mr-2 mb-2 mb-md-0" Text="Clear Flter" OnClick="Clear_Btn_Click" ValidationGroup="Val" />
                                               <%-- <asp:Button ID="ExcelBtn" runat="server" CssClass="btn btn-dark btn-icon-text mr-2 mb-2 mb-md-0" Text="Export To Excel" OnClick="ExcelBtn_Click" ValidationGroup="Val" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="NiptListGrid" Width="100%" HorizontalAlign="center" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-Font-Size="14px" EmptyDataRowStyle-Font-Italic="true" CssClass="table table-hover mb-0" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" AllowPaging="true" PageSize="10" DataKeyNames="TestId" AllowSorting="true" EmptyDataText="No Records Found" AutoGenerateColumns="false" OnPageIndexChanging="NewTestListGrid_PageIndexChanging" runat="server" OnRowCommand="NewTestListGrid_RowCommand" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ser.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Barcode">
                                                <ItemTemplate>
                                                    <strong><%# Eval("Barcode") %></strong>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Test Name">
                                                <ItemTemplate>
                                                    <%# Eval("TestName") %>  - <%# Eval("SubTestName") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Institute">
                                                <ItemTemplate>
                                                    <%# Eval("InstituteName") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Patient Name">
                                                <ItemTemplate>
                                                    <strong><%# Eval("Name") %></strong>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <%--    <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <strong><%# Eval("Mobile") %></strong>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <div class='<%# Eval("TestColor") %>'><%# Eval("TestStatus") %></div>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CreatedDt">
                                                <ItemTemplate>
                                                    <%# Eval("CreatedDt") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="View_Userinfo" runat="server" ImageUrl="~/assets/images/view.png" CommandArgument='<%#Eval("TestId")%>' CommandName="View" Font-Size="12px" ForeColor="White"></asp:ImageButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
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
        <asp:UpdateProgress DisplayAfter="0" ID="MPG1" runat="server">
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modalprogress">
                        <div id="theprogress">
                            <img alt="indicator" src="../assets/images/loading.gif" />
                        </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>

    <script type="text/javascript">
        window.onload = function () {
            LoadSelect();
        };
        function LoadSelect() {
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h6>' });
            $("#ContentPlaceHolder1_Institute_Drop,#ContentPlaceHolder1_Test_Drop,#ContentPlaceHolder1_Status_Drop").select2({ placeholder: '--' });
            $("#ContentPlaceHolder1_DateTxt").daterangepicker({autoUpdateInput: false,locale: {"cancelLabel": "Clear",}});
            $("#ContentPlaceHolder1_DateTxt").on('apply.daterangepicker', function (ev, picker) {$(this).val(picker.startDate.format('MM/DD/YYYY') + '-' + picker.endDate.format('MM/DD/YYYY'));table.draw();});
            $("#ContentPlaceHolder1_DateTxt").on('cancel.daterangepicker', function (ev, picker) {$(this).val('');table.draw();});
            $.unblockUI();
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    LoadSelect();
                }
            });
        };
    </script>
</asp:Content>





