<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="NiptBarcodeList.aspx.cs" Inherits="InternalLims.Main.NiptBarcodeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%-- <script src="https://code.jquery.com/jquery-1.10.1.min.js"></script>--%>
    <script src="../assets/js/jquery-2.1.1.js"></script>
    <script src="../assets/js/JSPrintManager.js"></script>
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
                url: "NiptBarcodeList.aspx/getTestKitRequest2",
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
                            <select name="installedPrinterName" id="installedPrinterName" autopostback="false" style="width:220px" ></select>
                        </div>
                        <!--end col-->
                    </div>
                </div>
                <!--end page-title-box-->
            </div>
        </div>
       
        

         <div class="row">
            <div class="col-md-12 grid-margin stretch-card">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-4">
                                <div class="mb-3 row">
                                    <label for="example-text-input" class="col-sm-2 form-label">Barcode</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="BarcodeTxt" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="mb-3 row">
                                    <label for="example-text-input" class="col-sm-2 form-label">Institute</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="Institute_Drop" runat="server" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="mb-3 row">
                                    <label for="example-text-input" class="col-sm-2 form-label">Test</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="Test_Drop" runat="server" Width="100%"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <div class="col-sm-4">
                                <div class="mb-3 row">
                                    <label for="example-text-input" class="col-sm-4 form-label">Barcode Status</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="UsedTypeDrop" runat="server" Width="100%">
                                             <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Used" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Not Used" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                         
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <div class="col-lg-12">
                                        <asp:Button ID="Search_Btn" runat="server" CssClass="btn btn-primary btn-icon-text mr-2 mb-2 mb-md-0" Text="Search Flter" OnClick="Search_Btn_Click" ValidationGroup="Val" />
                                        <asp:Button ID="Clear_Btn" runat="server" CssClass="btn btn-danger btn-icon-text mr-2 mb-2 mb-md-0" Text="Clear Flter" OnClick="Clear_Btn_Click" ValidationGroup="Val" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPrint" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Size="14px" DataKeyNames="TestRequestId" OnPageIndexChanging="gvPrint_PageIndexChanging" CssClass="table table-bordered mb-0 table-centered table-hover" PageSize="15" AllowSorting="true" Width="100%" GridLines="None" HeaderStyle-CssClass="thead-light" AllowPaging="true" EmptyDataText="No Barcode found !" OnRowDataBound="gvPrint_RowDataBound" EmptyDataRowStyle-Width="100%" EmptyDataRowStyle-CssClass="alert custom-alert custom-alert-danger icon-custom-alert shadow-sm fade show d-flex justify-content-between">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>.
                                    <asp:LinkButton ID="Me_Link" runat="server" OnClick="Me_Link_Click" CommandArgument='<%#Eval("TestRequestId")%>'></asp:LinkButton>
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
                                        <asp:Label ID="RecievedDate" runat="server" Text='<%#Eval("RecievedatNOVO")%>' Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lbOrder" runat="server" CssClass="btn btn-dark btn-sm" PostBackUrl='<%# String.Format("NIPTView?Id={0}", Eval("TestId"))%>' Text="View"></asp:LinkButton>
                                        <asp:HiddenField ID="RequestId" runat="server" Value='<%#Eval("TestSerno")%>' ClientIDMode="Static"></asp:HiddenField>
                                        <asp:Button ID="PrintB" runat="server" CommandName="Barcode" CssClass="btn btn-gradient-info btn-sm" Text="Print Barcode" OnClientClick='<%# "GetData(" + Eval("TestSerno") + ", false);" %>' />
                                        <asp:Button ID="PrintName" runat="server" CommandName="Name" CssClass="btn btn-secondary btn-sm" Text="Print Barcode & Name" class="btn btn-secondary btn-sm" OnClientClick='<%# "GetData(" + Eval("TestSerno") + ", true);" %>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys" />
                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
               
        <asp:UpdateProgress DisplayAfter="0" ID="MPG1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
            <ProgressTemplate>
                <div id="overlay">
                    <div id="modalprogress">
                        <img alt="indicator" src="../assets/images/loading.gif" />
                    </div>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>

    <script>
        $(document).ready(function () {
            LoadSelect();
            Control();
            // bindPrinters();
        });
        function LoadSelect() {
            //   bindPrinters();
            $.blockUI({ message: '<h6><img src="../../assets/images/loading.gif"/><br/>Just a moment...</h6>' });
            Control();
            $("#ContentPlaceHolder1_UsedTypeDrop,#ContentPlaceHolder1_Institute_Drop,#ContentPlaceHolder1_Test_Drop,#ContentPlaceHolder1_Status_Drop").select2({ placeholder: '--' });
            $.unblockUI();
        };
        var prm = Sys.WebForms.PageRequestManager.getInstance();if (prm != null) {prm.add_endRequest(function (sender, e) {if (sender._postBackSettings.panelsToUpdate != null) {LoadSelect();}});
        };
        function Control() {$("#installedPrinterName").select2({ placeholder: '--' });//  bindPrinters();
        };
    </script>
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

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }
    </style>
</asp:Content>





