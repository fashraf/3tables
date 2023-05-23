<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="PrintTest.aspx.cs" Inherits="InternalLims.Main.PrintTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script src="../assets/js/JSPrintManager.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>

    <script type="text/javascript">


        window.onload = onWindowLoad();

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
                    const log = barcodeArray.map(s => { return { TestSemo: s.TestSemo, BID: s.BID, ErrorMessage: err.ErrorMessage != undefine ? err.ErrorMessage :"Error", IPAddress: '' } });
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


    <script src="../assets/js/jquery-2.1.1.js"></script>
    <form runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="one" runat="server" OnClick="one_Click"  OnClientClick="print(true);" />

                <button id="btn" runat="server" type="button" onclick="print(true);" class="form-control">Print WithName</button>
                  <div id="installedPrinters">
                                        <label for="installedPrinterName">Installed Printer:</label>
                                        <select name="installedPrinterName" id="installedPrinterName" autopostback="false"></select>
                                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
</asp:Content>
