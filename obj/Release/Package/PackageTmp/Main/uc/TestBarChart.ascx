<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestBarChart.ascx.cs" Inherits="InternalLims.Main.uc.TestBarChart" %>
   <style>
#chartdiv {
  width: 100%;
  height: 350px;
  font-size: 11px;
}
	.desc
    {
        color:white;
	}
</style>

<!-- Resources -->
<%--<script src="../../assets/plugins/amchart/amcharts.js"></script>
<script src="../../assets/plugins/amchart/serial.js"></script>
<script src="../../assets/plugins/amchart/light.js"></script>--%>

   <script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
    <script src="https://www.amcharts.com/lib/3/serial.js"></script>
    <script src="https://www.amcharts.com/lib/3/themes/light.js"></script>

    <script>
        var j =<%=GetJsonData()%>;
        //var chart = AmCharts.makeChart("chartdiv", {
        //    "type": "serial",
        //    "theme": "light",
        //    "dataProvider": j,
        //    "valueAxes": [{
        //        "gridColor": "#FFFFFF",
        //        "gridAlpha": 0,
        //        "dashLength": 0
        //    }],
        //    "gridAboveGraphs": true,
        //    "startDuration": 1,
        //    "graphs": [{
        //        "balloonText": "[[dt]]: <b>[[Total]]</b>",
        //        "fillAlphas": 1,
        //        "lineAlpha": 1,
        //        "type": "column",
        //        "valueField": "Total"
        //    }],
        //    "chartCursor": {
        //        "categoryBalloonEnabled": false,
        //        "cursorAlpha": 0,
        //        "zoomable": false
        //    },
        //    "categoryField": "dt",
        //    "categoryAxis": {
        //        "gridPosition": "start",
        //        "gridAlpha": 0,
        //        "tickPosition": "start",
        //        "tickLength": 10
        //    },
        //    "export": {
        //        "enabled": false
        //    }

        //});






        var chart = AmCharts.makeChart("chartdiv", {
            "type": "serial",
            "theme": "light",
            "marginTop": 0,
            "marginRight": 10,
            "dataProvider": j,
            "valueAxes": [{
                "axisAlpha": 0,
                "position": "left"
            }],
            "graphs": [{
                "id": "g1",
                "balloonText": "[[dt]]<br><b><span style='font-size:14px;'>Total Submitted :[[Total]]</span></b>",
                "bullet": "round",
                "bulletSize": 8,
                "lineColor": "#d1655d",
                "lineThickness": 2,
                "negativeLineColor": "#637bb6",
                "type": "smoothedLine",
                "valueField": "Total"
            }],
            "dataDateFormat": "YYYY-MM-DD",
            "categoryField": "dt",
            "categoryAxis": {
                "minPeriod": "DD",
                "parseDates": false,
                "minorGridAlpha": 0.1,
                "minorGridEnabled": false
            },
            "export": {
                "enabled": false
            }
        });

        chart.addListener("rendered", zoomChart);
        if (chart.zoomChart) {
            chart.zoomChart();
        }

        function zoomChart() {
            //chart.zoomToIndexes(Math.round(chart.dataProvider.length * 1), Math.round(chart.dataProvider.length * 1));
            // chart.zoomToIndexes(chart.dataProvider.length - 40, chart.dataProvider.length - 1);
        }
    </script>
  <div id="chartdiv"></div>  