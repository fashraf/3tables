<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="CreateNewBarcode.aspx.cs" Inherits="InternalLims.Main.CreateNewBarcode" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <script src="../assets/js/JSPrintManager.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bluebird/3.3.5/bluebird.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>

    <script type="text/javascript">

        var selectedPrinterName;
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
            /*selectedPrinterName = $('#installedPrinterName').val();*/
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
                        if (selectedPrinterName) {
                            $('#installedPrinterName').val(selectedPrinterName);
                        }
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
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h6>' });
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
                        var date2 = "R " + receiveDt.getDate() + '/' + receiveDt.getMonth() + '/' + receiveDt.getFullYear();
                        if (barCode.length >= 12) {
                            date1Position = 120;
                            date2Position = 290;
                            barCodePosition = 120;
                        }
                        var cmds = "^XA"

                        if (barcodeArray[i].PatName !== "") {
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
            $.unblockUI();
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
            //var options = '';
            //for (var i = 0; i < barcodeArray.length; i++) {
            //    options += '<option  value=' + barcodeArray[i].TestSerno + ' > ' + barcodeArray[i].BID + '</option > ';
            //}
            //$('#barcodeDrop').html(options);
            //  bindPrinters();
        }

        function CreateData() {
            try {
                if (ValidateImageExtension()) {
                    $('#ConfirmModal').modal('show');
                    //Submitdata();
                }
                else {
                    //ShowErrorMessage("Some Info. is missing.");
                }
            }
            catch (e) { alert(e); }
        }


        function ValidateImageExtension() {
            var Test = $("#ContentPlaceHolder1_TestDrop").val();
            var subtest = $("#ContentPlaceHolder1_SubTestDrop").val();
            var InstituteDrop = $("#ContentPlaceHolder1_InstituteDrop").val();
            var TotalTxt = $("#ContentPlaceHolder1_TotalTxt").val();
          
            if (Test != "-1") {
                if (subtest != "-1") {
                    if (InstituteDrop != "-1") {
                        if (TotalTxt != "") {
                            return true;
                        }
                        else {
                            ShowErrorMessage("Enter Total Barcodes");
                            $("#ContentPlaceHolder1_TotalTxt").focus();
                        }
                    }
                    else {
                        ShowErrorMessage("Please Select a Hospital");
                        $("#ContentPlaceHolder1_InstituteDrop").focus();
                    }
                }
                else {
                    ShowErrorMessage("Please Select a Sub-Test");
                    $("#ContentPlaceHolder1_SubTestDrop").focus();
                }
            }
            else {
                ShowErrorMessage("Please Select a Test");
                $("#ContentPlaceHolder1_TestDrop").focus();
            }
        }


        function Submitdata() {
            var data = {
                Test: $("#ContentPlaceHolder1_TestDrop option:selected").val(),
                subtest: $("#ContentPlaceHolder1_SubTestDrop option:selected").val(),
                TypeDrop: 1,
                InstituteDrop: $("#ContentPlaceHolder1_InstituteDrop option:selected").val(),
                TotalTxt: $("#ContentPlaceHolder1_TotalTxt").val()
            }
            $.ajax({
                type: "POST",
                url: "CreateNewBarcode.aspx/Insertdata",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            $('#Confirm').modal('hide');
            //alert(response.d);
            bindBarcode(response.d);
            print(false);
        }

    </script>


    <script src="../assets/js/jquery-2.1.1.js"></script>
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">Create and Print Barcode</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                            <li class="breadcrumb-item"><a href="javascript:void(0);">Create Test Request</a></li>
                        </ol>
                    </div>
                    <!--end col-->
                    <div class="col-auto align-self-center">
                        <select name="installedPrinterName" id="installedPrinterName" autopostback="false" style="width: 220px"></select>
                    </div>
                    <!--end col-->
                </div>
            </div>
            <!--end page-title-box-->
        </div>
    </div>
    <form runat="server" id="friendform">
        <asp:HiddenField ID="HiddenField1" runat="server" /> 
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Create Test Request</h4>
                                <p class="text-muted mb-0">Create a test request along with Barcode.</p>
                            </div>
                            <!--end card-header-->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Test</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="TestDrop" runat="server" CssClass="form-control"   AutoPostBack="true" OnSelectedIndexChanged="TestDrop_SelectedIndexChanged" ></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Sub Test</label>
                                            <div class="col-sm-9">
                                               <asp:DropDownList ID="SubTestDrop" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-3 form-label align-self-center mb-lg-0 text-end">Institute</label>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="InstituteDrop" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="mb-3 row">
                                            <label for="example-text-input" class="col-sm-4 form-label align-self-center mb-lg-0 text-end">Total</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="TotalTxt" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Create Record" OnClientClick="CreateData();"/>
                                        <button type="button" onclick="print(false);" class="btn btn-soft-dark px-4">Print Barcode/s</button>
                                    </div>

                                    <br /><br />
                                    <div class="alert alert-success border-0" role="alert" id="alert" runat="server">
                                        <strong>Barcode Created Succeessfully!</strong>
                                    </div>
                                </div>
                            </div>
                            <!--end card-body-->
                        </div>
                        <!--end card-->
                    </div>
                    <!-- end col -->
                </div>


                <div class="modal fade" id="ConfirmModal" tabindex="-1" aria-labelledby="exampleModalDefaultLabel" style="display: none;" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title m-0" id="exampleModalDefaultLabel">Confirm!</h6>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <!--end modal-header-->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h5>Are you sure you want to <strong>Create this request ?</h5>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" Text="Yes" UseSubmitBehavior="false" Width="120px" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClientClick="Submitdata();"/>
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script>
        $(function () { Control() });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    Control();
                    bindPrinters();
                }
            });
        };

        function Control() {
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h6>' });
            $("#ContentPlaceHolder1_TestDrop,#ContentPlaceHolder1_SubTestDrop,#ContentPlaceHolder1_InstituteDrop,#installedPrinterName").select2({ placeholder: '--' });
            $("#ContentPlaceHolder1_TotalTxt").keypress(function (e) {if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {return false;}});
            $('#installedPrinterName').val(selectedPrinterName);
            $.unblockUI();
        };
    </script>
    <style>
        .form-label {
            color: black;
            font-weight: 600;
        }
    </style>
    </asp:Content>


