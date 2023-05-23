<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="noti.ascx.cs" Inherits="InternalLims.MasterPage.noti" %>
<script src="../assets/js/jquery-2.1.1.js"></script>
<%--<li class="dropdown notification-list">
   <%-- <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect" data-bs-toggle="dropdown" role="button" aria-haspopup="false" aria-expanded="false" id="notificationDropdown">--%>
   <%-- < <button id='bthnoti'>span class="badge bg-danger rounded-pill noti-icon-badge" id="Count_div" runat="server">2</span>
      <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect" href="#" data-bs-toggle="dropdown"  id="myHref">
          <table id="countTable">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
        </a>
 </button>  s </a>
    <div class="dropdown-menu" aria-labelledby="notificationDropdown">
        <div class="dropdown-header d-flex align-items-center justify-content-between">
            <p class="mb-0 font-weight-medium">
                <asp:Label ID="Noti_Count" runat="server"></asp:Label>
            </p>
        </div>
        <div class="dropdown-body">
              <div class="simplebar-content-wrapper" style="height: 120px; overflow: hidden scroll;"><div class="simplebar-content" style="padding: 0px;">
            <asp:Repeater ID="noti_rpt" runat="server" OnItemDataBound="Noti_Grid_ItemDataBound">
                <ItemTemplate>
                    <%--<div class="notification-menu" data-simplebar="init" style="width: 300px">
                        <div class="simplebar-wrapper" style="margin: 0px;">
                            <div class="simplebar-height-auto-observer-wrapper">
                                <div class="simplebar-height-auto-observer"></div>
                            </div>
                            <div class="simplebar-mask">
                                <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                                    <div class="simplebar-content-wrapper" style="height: auto; overflow: hidden scroll;">
                                        <div class="simplebar-content" style="padding: 0px;">
                                            <!-- item-->
                                            <a href="#" class="dropdown-item py-3">
                                                <small class="float-end text-muted ps-2">2 min ago</small>
                                                <div class="media">
                                                    <div class="avatar-md bg-soft-primary">
                                                        img
                                                    </div>
                                                    <div class="media-body align-self-center ms-2 text-truncate">
                                                        <h6 class="my-0 fw-normal text-dark">Your order is placed</h6>
                                                        <small class="text-muted mb-0">Dummy text of the printing and industry.</small>
                                                    </div>
                                                    <!--end media-body-->
                                                </div>
                                                <!--end media-->
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="simplebar-placeholder" style="width: auto; height: 340px;"></div>
                        </div>
                        <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                            <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
                        </div>
                        <div class="simplebar-track simplebar-vertical" style="visibility: visible;">
                            <div class="simplebar-scrollbar" style="height: 142px; display: block; transform: translate3d(0px, 0px, 0px);"></div>
                        </div>
                    </div>
                  
                    <a class="dropdown-item py-3" style="width: 300px">
                        <small class="float-end text-muted ps-2"><asp:Label Id="Dt_Lbl" runat="server" Text='<%# Eval("Dt") %>'></asp:Label></small>
                        <div class="media">
                            <div class="avatar-md bg-soft-primary">
                                <img src="<%# Eval("img") %>" height="40" />
                            </div>
                            <div class="media-body align-self-center ms-2 text-truncate">
                                <h6 class="my-0 fw-normal text-dark"><%# Eval("TestStatus") %></h6>
                                <small class="text-muted mb-0"><%# Eval("msg") %></small>
                            </div>
                            <!--end media-body-->
                        </div>
                        <!--end media-->
                    </a>
                       
                </ItemTemplate>
            </asp:Repeater>
        </div> </div></div>
      <%--  <div class="dropdown-footer d-flex align-items-center justify-content-center">
            <a href="javascript:;">View all</a>
        </div>
    </div>
</li>--%>


  <li class="dropdown notification-list">
    <a class="nav-link dropdown-toggle arrow-none waves-light waves-effect" data-bs-toggle="dropdown" href="#" role="button" aria-haspopup="false" aria-expanded="false">
        <img src="../assets/images/bell.png" class="align-self-center topbar-icon"style="width:32px;height:28px"/>
        <span class="badge bg-danger rounded-pill noti-icon-badge"> <asp:Label ID="Noti_Count" runat="server" Font-Size="12px"></asp:Label></span>
    </a>

    <div class="dropdown-menu dropdown-menu-end dropdown-lg pt-0" style="position: absolute; inset: 0px auto auto 0px; margin: 0px; transform: translate(-276px, 54px);" data-popper-placement="bottom-end">
        <h6 class="dropdown-item-text font-15 m-0 py-3 border-bottom d-flex justify-content-between align-items-center">Notifications <span class="badge bg-primary rounded-pill"></span>
        </h6>
        <div class="notification-menu" data-simplebar="init">
            <div class="simplebar-wrapper" style="margin: 0px;">
               <i class="fas fa-circle text-success"></i>
                <div class="simplebar-mask" style="width:320px">
                    <div class="simplebar-offset" style="right: 0px; bottom: 0px;">
                        <div class="simplebar-content-wrapper" style="height: auto; overflow: hidden scroll;">
                            <div class="simplebar-content" style="padding: 0px;">
                                <asp:Repeater ID="noti_rpt" runat="server" OnItemDataBound="Noti_Grid_ItemDataBound">
                                    <ItemTemplate>
                                        <div class='<%# Eval("readcss") %>' style="padding: 0px; margin: 0px">
                                            <a href="#" class="dropdown-item py-3">
                                                <small class="float-end text-muted ps-2">
                                                    <%# Eval("Dt", "{0: dd.MMM hh:mm tt}") %>
                                                    <asp:Label ID="Dt_Lbl" runat="server" Text='<%# Eval("Dt") %>' Font-Bold="true" Font-Size="9px"></asp:Label></small>
                                                <div class="media">
                                                    <div class="avatar-md bg-soft-primary">
                                                        <img src="<%# Eval("img") %>" height="40" />
                                                        <i data-feather="shopping-cart" class="align-self-center icon-xs"></i>
                                                    </div>
                                                    <div class="media-body align-self-center ms-2 text-truncate">
                                                        <h6 class="my-0 fw-normal text-dark"><%# Eval("TestStatus") %></h6>
                                                        <small class="text-muted mb-0"><%# Eval("msg") %></small>
                                                    </div>
                                                </div>

                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>
                    </div>
                </div>
                <div class="simplebar-placeholder" style="width: auto; height: 340px;"></div>
            </div>
            <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
                <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
            </div>
            <div class="simplebar-track simplebar-vertical" style="visibility: visible;">
                <div class="simplebar-scrollbar" style="height: 142px; display: block; transform: translate3d(0px, 0px, 0px);"></div>
            </div>
        </div>
        <!-- All-->
        <a href="javascript:void(0);" class="dropdown-item text-center text-primary">View all <i class="fi-arrow-right"></i>
        </a>
    </div>
    <%--<div class="dropdown-menu dropdown-menu-end dropdown-lg pt-0" style="width:340px">
        <h6 class="dropdown-item-text font-15 m-0 py-3 border-bottom d-flex justify-content-between align-items-center">Notifications <span class="badge bg-primary rounded-pill"></span>
        </h6>
        <div class="notification-menu">
            <!-- item-->
            <asp:Repeater ID="noti_rpt" runat="server" OnItemDataBound="Noti_Grid_ItemDataBound">
                <ItemTemplate>
                      <div class='<%# Eval("readcss") %>' style="padding:0px;margin:0px">
                    <a href="#" class="dropdown-item py-3">
                            <small class="float-end text-muted ps-2">
                               <%# Eval("Dt", "{0: dd.MMM HH:mm tt}") %> <asp:Label ID="Dt_Lbl" runat="server" Text='<%# Eval("Dt") %>'></asp:Label></small>
                            <div class="media">
                                <div class="avatar-md bg-soft-primary">
                                    <img src="<%# Eval("img") %>" height="40" />
                                    <i data-feather="shopping-cart" class="align-self-center icon-xs"></i>
                                </div>
                                <div class="media-body align-self-center ms-2 text-truncate">
                                    <h6 class="my-0 fw-normal text-dark"><%# Eval("TestStatus") %></h6>
                                    <small class="text-muted mb-0"><%# Eval("msg") %></small>
                                </div>
                            </div>
                      
                    </a>  </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!-- All-->
        <a href="javascript:void(0);" class="dropdown-item text-center text-primary">View all <i class="fi-arrow-right"></i>
        </a>
    </div>--%>
</li>



<style>
    button {
  all: unset;
  cursor: pointer;
}

button:focus {
  outline: orange 5px auto;
}
</style>