<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="CalendarView.aspx.cs" Inherits="InternalLims.Main.CalendarView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>

    <div class="container-fluid">
        <!-- Page-Title -->
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Calendar View</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                                <li class="breadcrumb-item active">Calendar</li>
                            </ol>
                        </div>
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
            <div class="col-12">
                <div class="card calendar-cta">
                        <div class="row">
                            <!--end col-->
                            <div class="col" style="padding:12px">
                                <h5 class="font-20">Marked are the Test Submitted on the Date.</h5>
                                <p>You can Click on them for futher info.</p>
                            </div>
                        </div>
                        <!--end row-->
                    </div>
                    <!--end card-body-->
                <!--end card-->
            </div>
            <!--end col-->
        </div>
        <!-- End row -->
        <div class="row">
            <!--end col-->
            <div class="col-lg-2"></div>
            <div class="col-lg-8">
                <div class="card">
                    <div class="card-body">
                        <div id="calendar" class="fc fc-ltr fc-unthemed" style="height: 80%; display: inline-block;">
                        </div>
                        <div style="clear: both"></div>
                    </div>
                    <!--end card-body-->
                </div>
                <!--end card-->
            </div>
            <!--end col-->
            <div class="col-lg-2"></div>
        </div>
        <!-- End row -->

    </div>
    <!-- container -->
    <div id="fullCalModal" class="modal fade bd-example-modal-sm" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="modalTitle1" class="modal-title"></h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div id="modalBody1" class="modal-body" style="font-size: 16px"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Close</button>
                    <a id="my-link" target="_blank" class="btn btn-primary">View Test</a>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var ddt =<%=GetJsonData()%>;
        var d = new Date();
        var curYear = d.getFullYear();
        var curMonth = (d.getMonth() + 1);
        var birthdayEvents = {
            backgroundColor: 'rgba(16,183,89, .25)',
            borderColor: '#10b759',
            fontsize: '14ppx',
            textColor: 'black',
            events: ddt
        };

        $(document).ready(function () {
            var d = new Date();
            var strDate = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();

            // initialize the calendar
            $('#calendar').fullCalendar({
                displayEventTime: false,
                header: {
                    left: 'prev,today,next',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay,listMonth'
                },
                allDay: true, stick: true,
                defaultView: 'month',
                eventLimit: true, // allow "more" link when too many events
                eventSources: [birthdayEvents],
                eventClick: function (event, jsEvent, view) {
                    if (event.url) {
                        $('#modalTitle1').html(event.title);
                        $('#modalBody1').html(event.description);
                        //$('#eventUrl').attr('href', event.url);
                        $("#my-link").attr("href", event.url);
                        $('#fullCalModal').modal("show");
                        return false;

                    }

                }
            });
        });
    </script>

    <style>
        .fc-event-time {
            display: none;
        }

        .fc-time-grid .fc-slats td {
            height: 3.5em;
        }
    </style>

</asp:Content>
