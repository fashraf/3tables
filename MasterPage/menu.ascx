<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="InternalLims.MasterPage.menu" %>

<div class="simplebar-mask">
    <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
        <div class="simplebar-content-wrapper" style="height: 100%; overflow: hidden scroll;">
            <div class="simplebar-content" style="padding: 0px 0px 70px;">
                <ul class="metismenu left-sidenav-menu">
                    <asp:Repeater ID="ParentRepeater" runat="server" OnItemDataBound="ParentRepeater_ItemDataBound">
                        <ItemTemplate>
                            <li class="mm-active">
                                <a href="javascript: void(0);" aria-expanded="true"><i data-feather='<%# Eval("SubMenuIcon")%>' style="height: 16px"></i><span>
                                    <asp:Label ID="MenuId_Lbl" runat="server" Text='<%# Eval("MenuId")%>' Visible="false"></asp:Label><asp:Label ID="MenuParent" runat="server" Text='<%# Eval("MenuText")%>'></asp:Label></span><span class="menu-arrow"><i class="mdi mdi-chevron-right"></i></span></a>

                                <ul class="nav-second-level mm-collapse mm-show" aria-expanded="false" style="">
                                    <asp:Repeater ID="ChildRepeater" runat="server">
                                        <ItemTemplate>
                                            <li class="nav-item"><a class="nav-link" href="<%# Eval("SubMenuURL")%>"><i class="ti-control-record"></i><%# Eval("SubMenuText")%></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</div>
  

