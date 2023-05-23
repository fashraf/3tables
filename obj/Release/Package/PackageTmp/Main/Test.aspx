﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="InternalLims.Main.Test" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /*
Colors
rgb(249, 99, 50); orange flame
rgb(255, 77, 77); red
rgb(198, 89, 99); red'ish
*/

        @page {
            bleed: 1cm;
            size: A4 portrait;
            size: auto; /* auto is the initial value */
            margin-left: 0mm; /* this affects the margin in the printer settings */
            margin-bottom: 0mm;
            margin-top: 0mm;
        }

        html {
            background-color: #ffffff;
            margin: 0px; /* this affects the margin on the html before sending to printer */
        }

        body {
            border: solid 1px blue;
            margin: 10mm 15mm 10mm 15mm; /* margin you want for the content */
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
            }


            body {
                background: #eee;
                font-familly: roboto;
                -webkit-print-color-adjust: exact !important;
            }

            div.container {
                border-radius: 15px;
                background: white;
                box-shadow: 0 10px 10px rgba(0, 0, 0, 0.2);
            }

            div.invoice-letter {
                width: auto;
                position: relative;
                background-color: #04617b;
                margin-right: -48px;
                margin-left: -48px;
                box-shadow: 0 4px 3px rgba(0, 0, 0, 0.4);
            }

            div.letter-title {
                border-right: 2px solid #eee;
            }

            div.letter-content {
                margin-top: 10px;
            }

            table.invoice thead th {
                background-color: rgba(4, 97, 123, 0.2);
                border-top: none;
            }

            table.invoice thead tr:first-child th:first-child {
                border-top-left-radius: 25px;
            }

            table.invoice thead tr:first-child th:last-child {
                border-top-right-radius: 25px;
            }

            tr.last-row {
                background-color: rgba(4, 97, 123, 0.2);
            }

                tr.last-row th {
                    border-bottom-left-radius: 25px;
                    width: 30px;
                }

                tr.last-row td {
                    border-bottom-right-radius: 25px;
                }

            div.row div.to {
                height: 260px;
                padding-right: 25px;
                border-right: 2px solid rgba(4, 97, 123, 0.2);
            }
        }
    </style>
    <div class="container my-5 px-5 py-5" id="Page">
        <div class="row">
            <div class="col-3 contact-details">
                <h5>Name of Issuer</h5>
                <h6><em>Address of issuer</h6>
                <p>Contact details Spanning on multiple rows cuz bootstrap is awesome.</em></p>
            </div>
            <div class="col-1 offset-2 logo">
                <img width="125px" height="125px" src="https://www.designfreelogoonline.com/wp-content/uploads/2016/03/000692-best-letters-logo-design-free-logo-maker-02.png" />
            </div>
            <div class="invoice-details col-3 offset-3 text-right">

                <h6>Invoice No. #1</h6>
                <h6>Issued at: 01/02/20 20</h6>

            </div>
        </div>

        <div class="container-fluid invoice-letter mt-3">
            <div class="row">
                <div class="col-3 text-white pl-5 py-2 letter-title">
                    <h5>Summary & Notes</h5>
                </div>
                <div class="col-9 text-white pr-5 py-2 letter-content">
                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent consequat erat mauris, a scelerisque est pellentesque vel. Proin tempus felis purus, vel molestie nisl condimentum nec. Sed at lorem maximus, venenatis nulla ut, laoreet eros. Etiam et lectus sem. Aenean tempus ante nec lacus aliquet consectetur. Vestibulum sem felis, tempus sit amet libero non, vehicula pellentesque turpis. Suspendisse potenti. </p>
                </div>
            </div>
        </div>

        <div class="row table mt-5">
            <table class="invoice table table-hover">
                <thead class="thead">
                    <tr>
                        <th scope="col">NO.</th>
                        <th scope="col">Item</th>
                        <th scope="col">Qty.</th>
                        <th scope="col">Price</th>
                        <th scope="col">Discount</th>
                        <th scope="col">Amount</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <th scope="row">1</th>
                        <td class="item">What about if we have a long-item title</td>
                        <td>1</td>
                        <td>25 <span class="currency">&euro;</span></td>
                        <td>5 %</td>
                        <td>28,75 <span class="currency">&euro;</span></td>
                    </tr>
                    <tr>
                        <th scope="row">2</th>
                        <td class="item">What about if we have a really, really, really long item title</td>
                        <td>1</td>
                        <td>25 <span class="currency">&euro;</span> </td>
                        <td></td>
                        <td>28,75 <span class="currency">&euro;</span> </td>
                    </tr>
                    <tr>
                        <th scope="row">3</th>
                        <td class="item">Bootstrap will just take care of this long title. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut sit amet tellus lectus. Quisque tincidunt lacus sed turpis blandit rutrum.</td>
                        <td>1</td>
                        <td>25 <span class="currency">&euro;</span> </td>
                        <td>13 %</td>
                        <td>28,75 <span class="currency">&euro;</span> </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="row">
            <!-- invoiced to details -->
            <div class=" offset-2 col-4">
                <div class="to text-right">
                    <h5 class="effect-color pt-4">Invoiced to:</h5>
                    <h4 class="theme-color"><strong>Company Name</strong></h4>
                    <h6>Address of issuer</h6>
                    <p>Contact details Spanning on multiple rows cuz bootstrap is awesome.</p>
                    <h5>Due date: 02/02/20 20</h5>
                </div>
            </div>
            <!-- Invoice assets and total -->
            <div class="col-6 pr-5">
                <table class="table table-borderless text-right">
                    <tbody>
                        <tr>
                            <th scope="row">Subtotal</th>
                            <td>75 <span class="currency">&euro;</span></td>
                        </tr>
                        <tr>
                            <th scope="row">Taxes*</th>
                            <td>11,25 <span class="currency">&euro;</span></td>
                        </tr>
                        <tr>
                            <th scope="row">Discounts</th>
                            <td>7,5 <span class="currency">&euro;</span></td>
                        </tr>
                        <tr>
                            <th scope="row">Shipping</th>
                            <td><span class="currency">&euro;</span> 10,25</td>
                        </tr>
                        <tr class="last-row">
                            <th scope="row">
                                <h4>Total</h4>
                            </th>
                            <td>
                                <h4><span class="currency">&euro;</span> 90,25</h4>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <p class="text-center mt-3"><em>* Taxes will be calculated in &euro; using the default % value for your region</em></p>

         
    </div>
    <button id="print" onclick="PrintElem('Page');" >Print</button>

    <a href="javascript:window.print()" class="btn btn-soft-info btn-sm">Print</a>
    <script>
        function PrintElem(elem) {
            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(elem).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            //mywindow.close();

            return true;
        }
    </script>
</asp:Content>