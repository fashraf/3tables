<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TaTPieChart.ascx.cs" Inherits="InternalLims.Main.uc.TaTPieChart" %>
  <style>
        #chartdivPie {
            width: 100%;
            height: 650px;
            font-size: 11px;
        }

        .amcharts-pie-slice {
            transform: scale(1);
            transform-origin: 50% 50%;
            transition-duration: 0.3s;
            transition: all .3s ease-out;
            -webkit-transition: all .3s ease-out;
            -moz-transition: all .3s ease-out;
            -o-transition: all .3s ease-out;
            cursor: pointer;
            box-shadow: 0 0 30px 0 #000;
        }

            .amcharts-pie-slice:hover {
                transform: scale(1.1);
                filter: url(#shadow);
            }
    </style>

<!-- Resources -->
<script src="../../assets/plugins/amchart/amcharts.js"></script>
<script src="https://www.amcharts.com/lib/3/pie.js"></script>
<link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
<script src="../../assets/plugins/amchart/light.js"></script>
  
    <script>
        var j=<%=GetJsonData()%>;
        var chart = AmCharts.makeChart("chartdivPie", {
            "type": "pie",
            "startDuration": 0,
            "theme": "light",
            "legend":{
                "position":"right",
                "marginRight":100,
                "autoMargins":false
            },
            "addClassNames": true,
            "dataProvider":j,
            "valueField": "Total",
            "titleField": "Tat Exceeded",
            "export": {
                "enabled": true
            }
        });

        chart.addListener("init", handleInit);

        chart.addListener("rollOverSlice", function (e) {
            handleRollOver(e);
        });

        function handleInit() {
            chart.legend.addListener("rollOverItem", handleRollOver);
        }

        function handleRollOver(e) {
            var wedge = e.dataItem.wedge.node;
            wedge.parentNode.appendChild(wedge);
        }
    </script>
    <div class="alert alert-success">
        <strong>Department</strong>
    </div>
  <div id="chartdivPie"></div>  