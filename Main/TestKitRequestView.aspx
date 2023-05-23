<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="TestKitRequestView.aspx.cs" Inherits="InternalLims.Main.TestKitRequestView" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <script src="../assets/js/JSPrintManager.js"></script>
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
            ShowSuccessMessage("Barcode Created Successfully!");
        }

        function addLogs(logs) {
            //console.log(PageMethods);
            //PageMethods.AddLogs(logs, OnSuccess);
           
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
            var TotalTxt = $("#ContentPlaceHolder1_ActualTestTxt").val();

            if (TotalTxt != "") {
                return true;
            }
            else {
                ShowErrorMessage("Enter Total Barcodes");
                $("#ContentPlaceHolder1_ActualTestTxt").focus();
            }
        }

        function Submitdata() {
            var data = {
                KitReqId: $("#ContentPlaceHolder1_KitRequestId").val(),
                Test: $("#ContentPlaceHolder1_TestHidden").val(),
                subtest: $("#ContentPlaceHolder1_SubTestHidden").val(),
                TypeDrop: 1,
                InstituteDrop: $("#ContentPlaceHolder1_InstituteHidden").val(),
                TotalTxt: $("#ContentPlaceHolder1_ActualTestTxt").val()
            }
            $.ajax({
                type: "POST",
                url: "TestKitRequestView.aspx/Insertdata",
                data: JSON.stringify(data),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                  
                    alert(response.d);
                  
                    //$(':input[type="ContentPlaceHolder1_Confirm_Btn"]').prop('disabled', false);
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


    <form runat="server">
        <div class="row">
            <div class="col-sm-12">
                <div class="page-title-box">
                    <div class="row">
                        <div class="col">
                            <h3 class="card-title">Kits Reqested By Institute</h3>
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Dashboard</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Kits Reqested</a></li>
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

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="card-body">
                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                    <div class="media">
                                        <i class="dripicons-arrow-right alert-icon text-primary align-self-center font-22 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Institute Information</h5>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <!-- Col -->
                                    <div class="col-sm-7">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-4 col-form-label">Name of the Institute</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="InstituteDrp" runat="server" class="form-control"></asp:Label>
                                                <asp:HiddenField ID="InstituteHidden" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-6 col-form-label">City</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="CityTxt" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Col -->
                                    <div class="col-sm-3">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-6 col-form-label">Request Status</label>
                                            <div class="col-sm-6">
                                                <asp:Label ID="StatusTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-4 col-form-label">Ordered By</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="UsernameLbl" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-2 col-form-label">Mobile</label>
                                            <div class="col-sm-10">
                                                <asp:Label ID="MobileLbl" runat="server" class="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Col -->
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-4 col-form-label">Email</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="EmailLbl" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Row -->

                                <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                    <div class="media">
                                        <i class="dripicons-user alert-icon text-primary align-self-center font-22 me-3"></i>
                                        <div class="media-body align-self-center">
                                            <h5 class="mb-1 fw-bold mt-0">Test Informtion</h5>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-4 col-form-label">Test</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="Testtxt" runat="server" CssClass="form-control"></asp:Label>
                                                <asp:HiddenField ID="TestHidden" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Sub Test</label>
                                            <div class="col-sm-9">
                                                <asp:Label ID="SubTesttxt" runat="server" CssClass="form-control"></asp:Label>
                                                <asp:HiddenField ID="SubTestHidden" runat="server"></asp:HiddenField>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group row">
                                            <label for="exampleInputUsername2" class="col-sm-4 col-form-label">Request Date</label>
                                            <div class="col-sm-8">
                                                <asp:Label ID="DtTxt" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-6">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <div class="input-group mb-3">
                                                            <asp:Label ID="TestRequestTxt" runat="server" CssClass="form-control" Font-Bold="true" Font-Size="25px"></asp:Label>
                                                            <span class="input-group-text" id="basic-addon2">
                                                                <img src="../assets/images/icon/in.png" height="50" /></span>
                                                        </div>
                                                        <h4 class="text-uppercase text-muted mt-2 m-0">Test Request</h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!--end col-->
                                    <div class="col-6">
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="row align-items-center">
                                                    <div class="col text-center">
                                                        <div class="input-group mb-3">
                                                            <span class="input-group-text" id="basic-addon3">
                                                                <img src="../assets/images/icon/out.png" height="50" /></span>
                                                            <asp:TextBox ID="ActualTestTxt" runat="server" CssClass="form-control" Font-Bold="true" Font-Size="25px" Style="text-align: center" oncopy="return false" onpaste="return false"></asp:TextBox>
                                                            <asp:HiddenField ID="KitRequestId" runat="server"></asp:HiddenField>
                                                        </div>
                                                        <h4 class="text-uppercase text-muted mt-2 m-0">Actual Test Approved</h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <!-- Row -->
                                <div class="row">
                                    <div class="col-sm-12 text-end">
                                        <%-- <input type="button" id="Confirm_Btn" runat="server" value="Save" onclick="CreateData();" class="btn btn-success m-btn m-btn--custom" style="width:120px"/>--%>
                                        <%-- <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Create Record" OnClientClick="CreateData();" />--%>
                                        <asp:Button ID="Confirm_Btn" runat="server" CssClass="btn btn-primary px-4" Text="Create Record" OnClick="Confirm_Btn_Click" />
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-6">
                                    <div class="card-body">
                                        <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                            <div class="media">
                                                <i class="dripicons-cart alert-icon text-primary align-self-center font-22 me-3"></i>
                                                <div class="media-body align-self-center">
                                                    <h5 class="mb-1 fw-bold mt-0">Current Inventory for this Hospital</h5>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="TestTakenTestAvailableGrid" runat="server" GridLines="None" HeaderStyle-CssClass="thead-light" CssClass="table table-hover mb-0" AutoGenerateColumns="false" EmptyDataText="No Request !" EmptyDataRowStyle-CssClass="alert alert-light border-0">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Ser.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Test & Sub Test">
                                                        <ItemTemplate>
                                                            <strong><%# Eval("TestName") %>   >   <%# Eval("SubTestName") %></strong>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currently Available">
                                                        <ItemTemplate>
                                                            <%# Eval("TotalAvalible") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Completed">
                                                        <ItemTemplate>
                                                            <%# Eval("TotalTaken") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="%(Graph)">
                                                        <ItemTemplate>
                                                            <div class="progress mb-3">
                                                                <div class="progress-bar" role="progressbar" style="width: <%# Eval("Percentage") %>%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100"></div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Percentage">
                                                        <ItemTemplate>
                                                            <%# Eval("Percentage", "{0:0.00}") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <!--end /table-->
                                        </div>
                                        <!--end /tableresponsive-->
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="card-body">
                                        <div class="alert custom-alert custom-alert-primary icon-custom-alert shadow-sm fade show d-flex justify-content-between" role="alert">
                                            <div class="media">
                                                <i class="dripicons-archive alert-icon text-primary align-self-center font-22 me-3"></i>
                                                <div class="media-body align-self-center">
                                                    <h5 class="mb-1 fw-bold mt-0">All the Barcodes for this Request</h5>
                                                </div>
                                            </div>
                                            <div class="row align-items-center">
                                                <div class="col-auto">
                                                    <input type="button" id="PrintAllBtn" runat="server" value="Print All Barcode" onclick="PrintAll('false');" class="btn btn-success m-btn m-btn--custom" style="width: 120px" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvPrint" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Size="14px" DataKeyNames="TestRequestId" CssClass="table table-bordered mb-0 table-centered table-hover" Width="100%" GridLines="None" HeaderStyle-CssClass="thead-light" EmptyDataText="No Barcode found !" OnRowDataBound="gvPrint_RowDataBound" EmptyDataRowStyle-Width="100%" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>.
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Barcode">
                                                        <ItemTemplate>
                                                            <strong><%# Eval("BarcodeID") %></strong>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="B.Status">
                                                        <ItemTemplate>
                                                            <div class='<%# Eval("BarcodeColour") %>'><%# Eval("BarcodeStatus") %></div>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="15%" DataField="TestName" HeaderText="Test Name" />
                                                    <asp:BoundField ItemStyle-Width="15%" DataField="PatName" HeaderText="Patient Name" />
                                                    <asp:TemplateField HeaderText="Lab Status">
                                                        <ItemTemplate>
                                                            <div class='<%# Eval("TestColor") %>'><%# Eval("TestStatus") %></div>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="15%" DataField="InstituteName" HeaderText="Institute Name" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Label ID="BarcodeLbl" runat="server" Text='<%#Eval("BarcodeID")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="CreatedDt" runat="server" Text='<%#Eval("CreatedDt")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="patName" runat="server" Text='<%#Eval("PatName")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="RecievedDate" runat="server" Text='<%#Eval("CreatedDt")%>' Visible="false"></asp:Label>
                                                            <asp:LinkButton ID="lbOrder" runat="server" CssClass="btn btn-dark btn-sm" PostBackUrl='<%# String.Format("NIPTView?Id={0}", Eval("TestId"))%>' Text="View"></asp:LinkButton>
                                                            <asp:HiddenField ID="RequestId" runat="server" Value='<%#Eval("TestSerno")%>' ClientIDMode="Static"></asp:HiddenField>
                                                            <asp:Button ID="PrintB" runat="server" CommandName="Barcode" CssClass="btn btn-gradient-info btn-sm" Text="Print Barcode" OnClientClick='<%# "GetData(" + Eval("TestSerno") + ", false);" %>' />
                                                            <asp:Button ID="PrintName" runat="server" CommandName="Name" CssClass="btn btn-secondary btn-sm" Text="Print Barcode & Name" class="btn btn-secondary btn-sm" OnClientClick='<%# "GetData(" + Eval("TestSerno") + ", true);" %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="15%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--Modal--%>
                    <%--Modal--%>
                    <div class="modal fade bd-example-modal-lg" id="Confirm" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static tabindex='-1'">
                        <div class="modal-dialog">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h6 class="modal-title m-0" id="exampleModalDefaultLabel">Confirm !</h6>
                                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <div class="alert alert-success border-0" role="alert">Are you sure you want to <strong>Create this request ?</strong></div>
                                                    <ul class="mt-3 mb-0">
                                                        <li>
                                                            <img src="../assets/images/btube.png" height="22">
                                                            <asp:Label ID="ModalTestLbl" runat="server" Font-Bold="true" Font-Size="18px"></asp:Label>
                                                            <ul>
                                                                <li>
                                                                    <asp:Label ID="ModalSubTestLbl" runat="server" Font-Bold="true" Font-Size="19px"></asp:Label></li>
                                                            </ul>
                                                        </li>
                                                        <br />
                                                        <li>
                                                            <asp:Image ID="location" runat="server" src="../assets/images/location.png" Height="22" />
                                                            <asp:Label ID="ModalInstituteLbl" runat="server" Font-Bold="true" Font-Size="20px"></asp:Label></li>
                                                        <br />
                                                        <li>
                                                            <asp:Label ID="Total_Lbl" runat="server" Font-Bold="true" Font-Size="20px"></asp:Label></li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="Button2" runat="server" Text="Cancel" data-bs-dismiss="modal" class="btn btn-danger" />
                                            <asp:Button ID="Submit_Btn" runat="server" Text="Create Barcodes" Width="120" UseSubmitBehavior="false" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClick="Submit_Btn_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
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
                                    <asp:Button ID="Button1" runat="server" Text="Yes" UseSubmitBehavior="false" Width="120px" CssClass="btn btn-primary" data-bs-dismiss="modal" OnClientClick="Submitdata();" />
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <style>
        .col-form-label {
            color: black;
            font-weight: 600;
            text-align: left;
        }
    </style>
    <script>
        $(function () { Control() });//onlyinteger();});

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
            $("#ContentPlaceHolder1_TitleDrop,#ContentPlaceHolder1_TypeDrop,#ContentPlaceHolder1_CityDrop,#ContentPlaceHolder1_OwnershipDrop,#installedPrinterName").select2({ placeholder: '--' });
            $("#ContentPlaceHolder1_ActualTestTxt").keypress(function (e) { if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) { return false; } });
            //bindPrinters();
            $('#installedPrinterName').val(selectedPrinterName);
            $.unblockUI();
        };


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
            debugger;
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
                    console.log(cpj.clientPrinter);
                    var barCodeLength = 2;
                    var date1Position = 150;
                    var date2Position = 335;
                    var barCodePosition = 150;
                    for (let i = 0; i < barcodeArray.length; i++) {
                        var barCode = barcodeArray[i].BID;
                        var name = barcodeArray[i].PatName;
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
                    const log = barcodeArray.map(s => { return { TestSemo: s.TestSemo, BID: s.BID, ErrorMessage: err.ErrorMessage != undefined ? err.ErrorMessage : "Error", IPAddress: '' } });
                    getIp(log);
                    alert('somthing went wrong')
                }
            }

        }

        function addLogs(logs) {
            console.log(PageMethods);
            //  PageMethods.AddLogs(logs, OnSuccess);
        }

        function OnSuccess(response) {
            //alert(response)
        }

        var barcodeArray = [];
        function bindBarcode(barcodestring, printname) {
            barcodeArray = JSON.parse(barcodestring);
            console.log(barcodeArray);
            //var options = '';
            //for (var i = 0; i < barcodeArray.length; i++) {
            //    options += '<option  value=' + barcodeArray[i].TestSerno + ' > ' + barcodeArray[i].BID + '</option > ';
            //}
            //$('#barcodeDrop').html(options);
            // bindPrinters();
            print(printname);
        }
        var isprintName = false;


        function PrintAll(printname) {
            console.log("adds")
            debugger;
            isprintName = printname;
            var chk = {};
            var data = {
                RequestId: 1,
            }
            $.ajax({
                type: "POST",
                url: "TestKitRequestView.aspx/AllTestKitRequest",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: OnSuccess2,
                failure: function (response) {
                    //alert(response.d);

                }
            });
        };

        

        function GetData(Id, printname) {
            console.log("adds")
            debugger;
            isprintName = printname;
            var chk = {};
            var data = {
                RequestId: Id,
            }
            $.ajax({
                type: "POST",
                url: "TestKitRequestView.aspx/SingleTestKitRequest",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(data),
                success: OnSuccess2,
                failure: function (response) {
                    //alert(response.d);

                }
            });

        }

        function OnSuccess2(response) {
            // $('#Confirm').modal('hide');
            //alert(response.d);
            bindBarcode(response.d, isprintName);
        }

    </script>

</asp:Content>