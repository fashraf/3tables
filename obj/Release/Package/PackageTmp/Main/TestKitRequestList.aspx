<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="TestKitRequestList.aspx.cs" Inherits="InternalLims.Main.TestKitRequestList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../assets/js/jquery-2.1.1.js"></script>
    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Test Kits Requested By Institutes</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item active"><a href="javascript:void(0);">Test Kit Request List</a></li>
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
                                    <div class="col-sm-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label">Date Range</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="DateTxt" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label">Institute</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="Institute_Drop" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Select Test</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="Test_Drop" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group row">
                                            <div class="col-lg-12">
                                                <asp:Button ID="Search_Btn" runat="server" CssClass="btn btn-primary btn-icon-text mr-2 mb-2 mb-md-0" Text="Search Flter" OnClick="Search_Btn_Click" ValidationGroup="Val" />
                                                <asp:Button ID="Clear_Btn" runat="server" CssClass="btn btn-danger btn-icon-text mr-2 mb-2 mb-md-0" Text="Clear Flter" OnClick="Clear_Btn_Click" ValidationGroup="Val" />
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
                            <%--      <div class="card-header">
                        <h4 class="card-title">Floating labels Selects <span class="badge bg-soft-success font-12">New</span></h4>
                        <p class="text-muted mb-0">Create beautifully simple form labels that float over your input fields.</p>
                    </div>--%>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="KitListGrid" Width="98%" HorizontalAlign="center" CssClass="table table-hover mb-0" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" AllowPaging="true" PageSize="10" DataKeyNames="RequestId" AllowSorting="true" EmptyDataText="No Records Found" AutoGenerateColumns="false" OnPageIndexChanging="NewTestListGrid_PageIndexChanging" runat="server" OnRowCommand="NewTestListGrid_RowCommand" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ser.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="3%" />
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
                                            <asp:TemplateField HeaderText="Requested Test">
                                                <ItemTemplate>
                                                    <strong><%# Eval("RequestedTest") %></strong>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actual Request">
                                                <ItemTemplate>
                                                    <strong><%# Eval("ActualTest") %></strong>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <strong>
                                                        <div class='<%# Eval("TestColor") %>'><%# Eval("Status") %></div>
                                                    </strong>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CreatedDt">
                                                <ItemTemplate>
                                                    <%# Eval("CreatedDt") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="View_Userinfo" runat="server" ImageUrl="~/assets/images/view.png" CommandArgument='<%#Eval("RequestId")%>' CommandName="View" Font-Size="12px" ForeColor="White"></asp:ImageButton>
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
            $("#ContentPlaceHolder1_DateTxt").on('cancel.daterangepicker', function (ev, picker) { $(this).val(''); table.draw(); });
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
    <style>
        .fontbold {
            font-size: 16px;
            font-weight: 900;
        }

        .green {
            background: #1D976C; /* fallback for old browsers */
            background: -webkit-linear-gradient(to left, #93F9B9, #1D976C); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(to left, #93F9B9, #1D976C); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            color: white;
            text-align: center;
        }

        .red {
            background: #FF416C; /* fallback for old browsers */
            background: -webkit-linear-gradient(to bottom, #FF4B2B, #FF416C); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(to bottom, #FF4B2B, #FF416C); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
            color: white;
        }
    </style>
</asp:Content>