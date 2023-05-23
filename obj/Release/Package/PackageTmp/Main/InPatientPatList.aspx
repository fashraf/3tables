<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="InPatientPatList.aspx.cs" Inherits="InternalLims.Main.InPatientPatList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <script type="text/javascript">
        $(document).ready(function () {
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h1>' });
            $.ajax({
                type: "POST",
                url: "../../Handler/InPatientList.ashx",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $.each(response, function (index, itemData) {
                        var tr = "<tr>" +
                            "<td>" + itemData.PatId + "</td>" +
                            "<td>" + itemData.FullName + "</td>" +
                            "<td>" + itemData.NationalId  + "</td>" +
                            "<td>" + itemData.Mobile  + "</td>" +
                            "<td>" + itemData.Email + "</td>" +
                            "<td>" + itemData.Gender + "</td>" +
                            "<td>" + itemData.City + "</td>" +
                            "<td>" + itemData.IsSaudi +  "</td>" +
                            "<td><a href='InPatientPatView?Id=" + itemData.PatId + "'><img src='../assets/images/view.png' width='15' /></a></td>" +
                            "</tr>";
                        $("#tableinfo").find("tbody").append(tr);
                    });
                    BindTable();
                }
            });
        });
       </script>
    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Walk-In-Patient List</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Patient List</a></li>
                            </ol>
                        </div>

                        <div class="col-auto align-self-center">
                            <a href="InPatientAddPat">
                                <button type="button" class="btn btn-gradient-dark"><i class="mdi mdi-account-details me-2"></i>Add New Patient</button>
                            </a>
                            <a href="Default">
                                <button type="button" class="btn btn-primary"><i class="mdi mdi-keyboard-backspace me-2"></i>Dashboard</button>
                            </a>
                        </div>
                    </div>
                    <!--end row-->
                </div>
                <!--end page-title-box-->
            </div>
            <!--end col-->
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-body">
                                <table id="tableinfo" class="table table-bordered mb-0 table-centered" style="width: 100%">
                                    <thead>
                                        <tr class="thead-light">
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                    <tfoot>
                                        <tr class="thead-light">
                                            <th>Id</th>
                                            <th>Name</th>
                                            <th>National Id</th>
                                            <th>Mobile</th>
                                            <th>Email</th>
                                            <th>Gender</th>
                                            <th>City</th>
                                            <th>Saudi/Non Saudi</th>
                                            <th>View</th>
                                        </tr>
                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>


    <script>
        let dr_table;
        function BindTable() {

            function add_dd(column) {
                let select = $('<select class="form-control-sm"><option value="">All</option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $(this).val();
                        column.search(val ? val : '', true, false).draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            }

            $('#tableinfo tfoot th').each(function () {
                let title = $('#tableinfo thead th').eq($(this).index()).text();

                if (title == "Mobile") {
                    'Mobile' + $(this).html('<input type="text" class="form-control-sm datatable-filter" placeholder="' + title + 'All" size="10" />');
                }
            });

            dr_table = $('#tableinfo').DataTable({
                order: [[0, 'desc']],
                "initComplete": function () {
                    // Enable search box
                    let r = $('#tableinfo tfoot tr');
                    r.find('th').each(function () {
                        $(this).css('padding', 0);
                    });
                    $('#tableinfo thead').append(r);
                    let api = this.api();
                    // Add dd search box
                    add_dd(api.column(6));
                    add_dd(api.column(7));
                    $.unblockUI();
                }
            });

            // Apply the search
            dr_table.columns().eq(0).each(function (colIdx) {
                $('input', dr_table.column(colIdx).footer()).on('keyup change', function () {
                    let search_text = this.value;
                    dr_table.column(colIdx).search(search_text).draw();
                });
            });

        };
    </script>



</asp:Content>