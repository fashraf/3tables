<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="UserRegistrationList.aspx.cs" Inherits="InternalLims.Main.UserRegistrationList" %>
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
                            <h4 class="page-title">List of Registered Users</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active"><a href="javascript:void(0);">Registered List</a></li>
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
                            <div class="card-body" style="padding-bottom: 1px">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label">Search</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="SearchTxt" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                                <code>Mobile,Email or Name</code>
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
                                    <div class="col-sm-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Status</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="Status_Drop" runat="server" Width="100%"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group row">
                                            <div class="col-lg-12">
                                                <asp:Button ID="Search_Btn" runat="server" CssClass="btn btn-primary btn-icon-text mr-2 mb-2 mb-md-0" Text="Search Flter" OnClick="Search_Btn_Click" ValidationGroup="Val" />
                                                <asp:Button ID="Clear_Btn" runat="server" CssClass="btn btn-danger btn-icon-text mr-2 mb-2 mb-md-0" Text="Clear Flter" OnClick="Clear_Btn_Click" ValidationGroup="Val" />
                                                <asp:Label ID="totalRows" runat="server" Font-Bold="false" Font-Size="Large"></asp:Label>
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
                                    <div class="table-responsive">
                                        <asp:GridView ID="UserRegisteredGrid" Width="98%" HorizontalAlign="center" EmptyDataRowStyle-CssClass="alert alert-danger border-0  mb-2" CssClass="table table-hover mb-0" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" AllowPaging="true" PageSize="10" DataKeyNames="UserId" AllowSorting="true" EmptyDataText="No Records Found" AutoGenerateColumns="false" OnPageIndexChanging="UserRegisteredGrid_PageIndexChanging" runat="server" OnRowCommand="UserRegisteredGrid_RowCommand" GridLines="None" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ser.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="3%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <%# Eval("UserTitle") %> <%# Eval("UserFullName") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile">
                                                    <ItemTemplate>
                                                        <%# Eval("UserMobile") %></strong>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email">
                                                    <ItemTemplate>
                                                        <%# Eval("UserEmail") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Institute">
                                                    <ItemTemplate>
                                                        <%# Eval("InstituteName") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <strong>
                                                            <div class='<%# Eval("TestColor") %>'><%# Eval("UserStatus") %></div>
                                                        </strong>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Registered Date">
                                                    <ItemTemplate>
                                                        <%# Eval("CreatedDate") %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="View_Userinfo" runat="server" ImageUrl="~/assets/images/view.png" CommandArgument='<%#Eval("UserId")%>' CommandName="View" Font-Size="12px" ForeColor="White"></asp:ImageButton>
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
        function LoadSelect() { $("#ContentPlaceHolder1_Institute_Drop,#ContentPlaceHolder1_Status_Drop").select2({ placeholder: '--' }); };
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h1>' });
                    LoadSelect();
                    $.unblockUI();
                }
            });
        };
    </script>
    <style>
        .fontbold {
            font-size: 16px;
            font-weight: 900;
        }
    </style>
</asp:Content>




