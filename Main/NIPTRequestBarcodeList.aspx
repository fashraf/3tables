<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="NIPTRequestBarcodeList.aspx.cs" Inherits="InternalLims.Main.NIPTRequestBarcodeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/js-1.8.3.js"></script>
    <script src="../assets/js/JSPrintManager.js"></script>
   <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>--%>


    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "../assets/images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "../assets/images/plus.png");
            $(this).closest("tr").next().remove();
        });

        $(document).ready(function () {
            $("#ContentPlaceHolder1_gvParent").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable({ "order": [[0, "desc"]] });

            window.onload = onWindowLoad();
        });
   



        function onWindowLoad() {
            bindPrinters();
        }
        function getIp(logs) {
            $.getJSON("https://api.ipify.org?format=json", function (data) {
                logs.forEach(s => { s.IPAddress = data.ip });
                if (logs.filter(s => s.IPAddress == '').length == 0)
                    addLogs(logs);
            })
        }

        function bindPrinters() {
            //WebSocket settings
            JSPM.JSPrintManager.auto_reconnect = true;
            JSPM.JSPrintManager.start();
            JSPM.JSPrintManager.WS.onStatusChanged = function () {
                if (jspmWSStatus()) {
                    //get client installed printers
                    JSPM.JSPrintManager.getPrinters().then(function (myPrinters) {
                        var options = '';
                        for (var i = 0; i < myPrinters.length; i++) {
                            options += '<option>' + myPrinters[i] + '</option>';
                        }
                        $('#installedPrinterName').html(options);
                    });
                }
            };
        }

        //Check JSPM WebSocket status
        function jspmWSStatus() {
            if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Open)
                return true;
            else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Closed) {
                alert('JSPrintManager (JSPM) is not installed or not running! Download JSPM Client App from https://neodynamic.com/downloads/jspm');
                return false;
            }
            else if (JSPM.JSPrintManager.websocket_status == JSPM.WSStatus.Blocked) {
                alert('JSPM has blocked this website!');
                return false;
            }
        }

        function called() {
            JSPM.JSPrintManager.start()
                .then(_ => {
                    var cpj = new JSPM.ClientPrintJob();
                    cpj.clientPrinter = new JSPM.DefaultPrinter();
                    cpj.printerCommands = 'RAW PRINTER COMMANDS HERE';
                    cpj.sendToClient();
                })
                .catch((e) => {
                    alert(e);
                });
        }

        //Do printing...
        function print(prinetWithName) {
            if (jspmWSStatus()) {
                try {
                    //Create a ClientPrintJob

                    var cpj = new JSPM.ClientPrintJob();
                    //Set Printer type (Refer to the help, there many of them!)
                    if ($('#useDefaultPrinter').prop('checked')) {
                        cpj.clientPrinter = new JSPM.DefaultPrinter();
                    } else {
                        cpj.clientPrinter = new JSPM.InstalledPrinter($('#installedPrinterName').val());
                    }

                    var barCodeLength = 2;
                    var date1Position = 150;
                    var date2Position = 335;
                    var barCodePosition = 150;
                    for (let i = 0; i < barcodeArray.length; i++) {
                        var barCode = barcodeArray[i].BID;
                        var name = barcodeArray[i].CreatedBy;
                        const createdDt = new Date(barcodeArray[i].CreatedDt);
                        var date1 = "C " + createdDt.getDate() + '/' + createdDt.getMonth() + '/' + createdDt.getFullYear();
                        const receiveDt = new Date(barcodeArray[i].ReceiveDt);
                        var date2 = "R " + receiveDt.getDate() + 1 + '/' + receiveDt.getMonth() + '/' + receiveDt.getFullYear();
                        if (barCode.length >= 12) {
                            date1Position = 120;
                            date2Position = 290;
                            barCodePosition = 120;
                        }
                        var cmds = "^XA"

                        if (prinetWithName == true) {

                            cmds += "^FO" + date1Position + ",25^ADN,10,10^FD" + date1 + "^FS";
                            cmds += "^FO" + date2Position + ",25^ADN,10,10^FD" + date2 + "^FS";
                            cmds += "^FO210,160^ADN,10,10^FD" + name + "^FS";
                        }

                        cmds += "^FO" + barCodePosition + ",50^BY" + barCodeLength + "^BCN,70,N,,,^FD" + barCode + "^FS";
                        // cmds += "^FO120,50^BY" + barCodeLength + "^BCN,70,N,,,^FD" + barCode + "^FS";
                        cmds += "^FO210,130^ADN,10,10^FD" + barCode + "^FS";

                        cmds += "^XZ";
                        cpj.printerCommands = cmds;
                        cpj.sendToClient();
                    }
                }
                catch (err) {
                    console.log(err)
                    const log = barcodeArray.map(s => { return { TestSemo: s.TestSemo, BID: s.BID, ErrorMessage: err.ErrorMessage != undefine ? err.ErrorMessage : "Error", IPAddress: '' } });
                    getIp(log);
                    alert('somthing went wrong')
                }
            }

        }

        function addLogs(logs) {
            console.log(PageMethods);
            PageMethods.AddLogs(logs, OnSuccess);
        }

        function OnSuccess(response) {
            //alert(response)
        }

        var barcodeArray = [];
        function bindBarcode(barcodestring) {
            barcodeArray = JSON.parse(barcodestring);
            console.log(barcodeArray);
            var options = '';
            for (var i = 0; i < barcodeArray.length; i++) {
                options += '<option  value=' + barcodeArray[i].TestSerno + ' > ' + barcodeArray[i].BID + '</option > ';
            }
            $('#barcodeDrop').html(options);
            bindPrinters();
            window.print(true);
        }

    </script>



    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h4 class="page-title">Request and Barcode List</h4>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="Default.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Request List and Related Barcode</li>
                            </ol>
                        </div>
                        <!--end col-->
                        <div class="col-auto align-self-center">
                            <a href="#" class="btn btn-sm btn-outline-primary" id="Dash_Date">
                                <span class="day-name" id="Day_Name">Today:</span>&nbsp;
                                           
                                <span class="" id="Select_date">Jan 10</span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-calendar align-self-center icon-xs ms-1">
                                    <rect x="3" y="4" width="18" height="18" rx="2" ry="2"></rect><line x1="16" y1="2" x2="16" y2="6"></line><line x1="8" y1="2" x2="8" y2="6"></line><line x1="3" y1="10" x2="21" y2="10"></line></svg>
                            </a>
                            <a href="#" class="btn btn-sm btn-outline-primary">
                                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-download align-self-center icon-xs">
                                    <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4"></path><polyline points="7 10 12 15 17 10"></polyline><line x1="12" y1="15" x2="12" y2="3"></line></svg>
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="col-md-12 col-xl-12 mg-t-10">
                    <div id="installedPrinters">
                        <label for="installedPrinterName">Installed Printer:</label>
                        <select name="installedPrinterName" id="installedPrinterName" autopostback="false"></select>
                    </div>
                    <asp:GridView ID="gvParent" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered mb-0 table-centered" DataKeyNames="TestReqSerno" OnRowDataBound="OnRowDataBound" GridLines="None" HeaderStyle-CssClass="thead-light" ShowHeader="true" HeaderStyle-Font-Size="14px">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" style="cursor: pointer" src="../assets/images/plus.png" height="21" />
                                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                                        <asp:GridView ID="gvChild" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Size="14px" DataKeyNames="TestRequestId" CssClass="table table-bordered mb-0 table-centered" Width="100%" GridLines="None" HeaderStyle-CssClass="thead-light" AllowPaging="true" EmptyDataText="No Barcode !" EmptyDataRowStyle-Width="100%" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between" OnRowCommand="gvChild_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>.
                                                            <asp:LinkButton ID="Me_Link" runat="server" OnClick="Me_Link_Click" CommandArgument='<%#Eval("TestRequestId")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField ItemStyle-Width="30%" DataField="Barcode" HeaderText="Barcode" ItemStyle-Font-Size="16px" ItemStyle-Font-Bold="true" />
                                                <asp:BoundField ItemStyle-Width="20%" DataField="CreatedDt" HeaderText="Submitted Date" DataFormatString="{0:dd/MM/yy HH:mm tt}" />
                                                <asp:BoundField ItemStyle-Width="20%" DataField="TestStatus" HeaderText="Status" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="BarcodeLbl" runat="server" Text='<%#Eval("Barcode")%>'></asp:Label>
                                                        <asp:Label ID="CreatedDt" runat="server" Text='<%#Eval("CreatedDt")%>'></asp:Label>
                                                        <asp:Label ID="patName" runat="server" Text='<%#Eval("TestId")%>'></asp:Label>
                                                        <asp:Label ID="RecievedDate" runat="server" Text='<%#Eval("CreatedDt")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbOrder" runat="server" CssClass="btn btn-dark btn-sm" PostBackUrl='<%# String.Format("ViewNIPT.aspx?Id={0}", Eval("TestId"))%>' Text="View"></asp:LinkButton>
                                                        <asp:Button ID="PrintB" runat="server" CssClass="btn btn-primary btn-sm" Text="Print Barcode" OnClick="PrintB_Click" />
                                                        <button type="submit" class="btn btn-secondary btn-sm">Print Barcode With Details</button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="20%" DataField="TestName" HeaderText="Test Name" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:BoundField ItemStyle-Width="10%" DataField="SubTestName" HeaderText="Sub Test Name" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:BoundField ItemStyle-Width="10%" DataField="TestRequest" HeaderText="Test Request" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:BoundField ItemStyle-Width="10%" DataField="ActualRequest" HeaderText="Actual Request" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:BoundField ItemStyle-Width="10%" DataField="TotalUnUsed" HeaderText="Total Un Used" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:BoundField ItemStyle-Width="10%" DataField="TotalUsed" HeaderText="Total Used" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:BoundField ItemStyle-Width="5%" DataField="Percentage" HeaderText="Percentage Used" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px" />
                            <asp:TemplateField HeaderText="Graph" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="14px">
                                <ItemTemplate>
                                    <div class="progress mb-3">
                                        <div class="progress-bar" role="progressbar" style="width: <%#Eval("Percentage")%>%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100"></div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- card -->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress DisplayAfter="0" ID="MPG1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modalprogress">
                        <div id="theprogress">
                            <img alt="indicator" src="../../assets/img/loading.png" />
                        </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
    <style>
        #overlay {
            position: fixed;
            z-index: 99;
            top: 0px;
            left: 0px;
            background-color: #f8f8f8;
            width: 100%;
            height: 100%;
            filter: Alpha(Opacity=90);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }

        #theprogress {
            background-color: #fff;
            border: 1px none #ccc;
            padding: 10px;
            width: 300px;
            height: 30px;
            line-height: 30px;
            text-align: center;
            filter: Alpha(Opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

        #modalprogress {
            position: absolute;
            top: 40%;
            left: 50%;
            margin: -11px 0 0 -150px;
            color: #990000;
            font-weight: bold;
            font-size: 28px;
            font-family: SC_AMEEN;
        }

        .all {
            -webkit-box-shadow: 4px 4px 12px -4px rgba(184,11,40,1);
            -moz-box-shadow: 4px 4px 12px -4px rgba(184,11,40,1);
            box-shadow: 4px 4px 12px -4px rgba(184,11,40,1);
        }

        .reg {
            -webkit-box-shadow: 4px 4px 12px -4px rgba(46, 204, 113, 1);
            -moz-box-shadow: 4px 4px 12px -4px rgba(46, 204, 113, 1);
            box-shadow: 4px 4px 12px -4px rgba(46, 204, 113, 1);
        }
    </style>
</asp:Content>


