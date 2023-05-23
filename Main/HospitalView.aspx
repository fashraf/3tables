<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="HospitalView.aspx.cs" Inherits="InternalLims.Main.HospitalView" %>
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
                            <h4 class="page-title">View Hospital</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                                <li class="breadcrumb-item active">View Hospital</li>
                            </ol>
                        </div>
                        <!--end col-->
                        <div class="col-auto align-self-center">
                            <asp:Button ID="EditBtn" runat="server" OnClick="EditBtn_Click" Text="Edit this Hospital" CssClass="btn btn-primary" />
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

        <div class="row">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title">View Hospital</h3>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="mb-4 row">
                                            <label for="example-text-input" class="col-sm-3 form-label">Institute Name</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="InstituteNameTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">City</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="CityLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-4 row">
                                            <label for="example-email-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Ownership</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="OwnershipLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="mb-4 row">
                                            <label for="example-text-input" class="col-sm-5 form-label">Main/Branch</label>
                                            <div class="col-sm-7">
                                                <asp:Label ID="MainBranchLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-5 form-label">Institute Name</label>
                                            <div class="col-sm-7">
                                                <asp:Label ID="MainBranchInstituteNameLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-4 form-label">CR Number</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="CRNumberLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="mb-8 row">
                                            <label for="example-url-input" class="col-sm-1 form-label">Address</label>
                                            <div class="col-sm-11">
                                                <asp:Label ID="AddressLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="mb-4 row">
                                            <label for="example-url-input" class="col-sm-3 form-label">Status</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="StatusLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="card-header">
                            <div class="row align-items-center">
                                <div class="col">
                                    <h4 class="card-title">User/s In this Hospital</h4>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <div class="table-responsive">
                                    <asp:GridView ID="UserGrid" Width="98%" HorizontalAlign="center" EmptyDataRowStyle-CssClass="alert alert-danger border-0  mb-2" CssClass="table table-hover mb-0" RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="thead-light" DataKeyNames="UserId" AllowSorting="true" EmptyDataText="No Records Found" AutoGenerateColumns="false" runat="server" OnRowCommand="UserGrid_RowCommand" GridLines="None" HeaderStyle-HorizontalAlign="Center">
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</asp:Content>