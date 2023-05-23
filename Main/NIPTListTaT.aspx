<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="NIPTListTaT.aspx.cs" Inherits="InternalLims.Main.NIPTListTaT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h1>' });
            $.ajax({
                type: "POST",
                url: "../../Handler/NiptTaT.ashx",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $.each(response, function (index, itemData) {
                        var tr = "<tr>" +
                            "<td>" + itemData.Barcode + "</td>" +
                            "<td>" + itemData.TestName + ' > ' + itemData.SubTestName + "</td>" +
                            "<td>" + itemData.InstituteName + "</td>" +
                            "<td>" + itemData.Name + "</td>" +
                            "<td>" + itemData.SampleCollectionDT + "</td>" +
                            "<td>" + itemData.TestStatus + "</td>" +
                            "<td>" + itemData.RecievedatNOVO + '( ' + itemData.DaysInNovo + ' )' + "</td>" +
                            "<td>" + itemData.ActalStatus +  "</td>" +
                            "<td>" + itemData.DaysLeft + '<i class="' + itemData.StatusStyle + '"></i>' +"</td>" +
                            "<td><a href='NIPTView.aspx?Id=" + itemData.TestId + "'><img src='../assets/images/view.png' width='15' /></a></td>" +
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
                            <h4 class="page-title">Turnaround time</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Turnaround time</a></li>
                            </ol>
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
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
                                    <tfoot>
                                        <tr class="thead-light">
                                            <th>Barcode</th>
                                            <th>Test Name</th>
                                            <th>Hospital Name</th>
                                            <th>Patient Name</th>
                                            <th>Sample Collection DT</th>
                                            <th>Status</th>
                                            <th>Recieved at NOVO</th>
                                            <th>TAT Status</th>
                                            <th>Days Left</th>
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

                if (title == "Barcode") {
                    'Barcode' + $(this).html('<input type="text" class="form-control-sm datatable-filter" placeholder="' + title + 'All" size="10" />');
                }
                //else if (title == "Test Name") {
                //    $(this).html('<input type="text" class="form-control-sm datatable-filter" placeholder="' + title + 'All" size="10" />');
                //}
                //else if (title == "Barcode") {
                //    $(this).html('<input type="text" class="form-control-sm" placeholder="' + title + 'All" size="10" />');
                //}
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
                    add_dd(api.column(1));
                    add_dd(api.column(2));
                    add_dd(api.column(5));
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
