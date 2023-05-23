<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InPatPaymentTypeDonut.ascx.cs" Inherits="InternalLims.Main.uc.InPatPaymentTypeDonut" %>

<style>
    .morris-donut-inverse{
        text [fill="#000000"] fill: $body-text;
    }
    path[stroke="#ffffff"] {
        stroke: $body-bg;
    }
</style>

 <div id="donut-example" class="morris-donut-inverse"></div>
  <script type="text/javascript">
    var j =<%=GetJsonData()%>;
      var colorDanger = "#FF1744";
      Morris.Donut({
          element: 'donut-example',
          resize: true,
          //colors: [
          //    '#E0F7FA'
          //],
          //labelColor:"#000", // text color
          //backgroundColor: '#333333', // border color
          data: j
      });
  </script>



<%-- <style>
        #chartdiv {
            width: 100%;
            height: 340px;
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
<script src="../../assets/amchart/amcharts.js"></script>
<script src="../../assets/amchart/light.js"></script>
<script src="https://www.amcharts.com/lib/3/pie.js"></script>
    <script>
        var j =<%=GetJsonData()%>;
        var chart = AmCharts.makeChart("chartdiv", {
            "type": "pie",
            "startDuration": 0,
            "theme": "light",
            //"legend":{
            //    "position":"bottom",
            //    "marginRight":100,
            //    "autoMargins":false
            //},
            "addClassNames": true,
            "dataProvider": j,
            "valueField": "Total",
            "titleField": "Payment"
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
  <div id="chartdiv"></div>  --%>


<%--<style>
#Agechartdiv {
  width: 100%;
  height: 340px;
}
</style>

<!-- Resources -->
<script src="https://cdn.amcharts.com/lib/5/index.js"></script>
<script src="https://cdn.amcharts.com/lib/5/xy.js"></script>
<script src="https://cdn.amcharts.com/lib/5/themes/Animated.js"></script>

<!-- Chart code -->
<script>
    am5.ready(function () {
        var rootCjart = am5.Root.new("Agechartdiv");
        rootCjart.setThemes([
            am5themes_Animated.new(rootCjart)
        ]);

        var chartdiv = rootCjart.container.children.push(am5xy.XYChart.new(rootCjart, {
            panX: false,
            panY: false,
            wheelX: "panX",
            wheelY: "zoomX",
            layout: rootCjart.verticalLayout
        }));


        var cursor = chartdiv.set("cursor", am5xy.XYCursor.new(rootCjart, {}));
        cursor.lineY.set("visible", false);

        var xRenderer = am5xy.AxisRendererX.new(rootCjart, { minGridDistance: 30 });
        xRenderer.labels.template.setAll({
            rotation: -90,
            centerY: am5.p50,
            centerX: am5.p100,
            paddingRight: 15
        });

        xRenderer.grid.template.setAll({
            location: 1
        })

        var xAxis = chartdiv.xAxes.push(am5xy.CategoryAxis.new(rootCjart, {
            maxDeviation: 0.3,
            categoryField: "Payment",
            renderer: xRenderer,
            tooltip: am5.Tooltip.new(rootCjart, {})
        }));

        var yAxis = chartdiv.yAxes.push(am5xy.ValueAxis.new(rootCjart, {
            maxDeviation: 0.3,
            renderer: am5xy.AxisRendererY.new(rootCjart, {
                strokeOpacity: 0.1
            })
        }));

        var series = chartdiv.series.push(am5xy.ColumnSeries.new(rootCjart, {
            name: "Series 1",
            xAxis: xAxis,
            yAxis: yAxis,
            valueYField: "value",
            sequencedInterpolation: true,
            verticalLayout: false,
            categoryXField: "Payment",
            tooltip: am5.Tooltip.new(rootCjart, {
                labelText: "{value}"
            })
        }));

        series.columns.template.setAll({ cornerRadiusTL: 5, cornerRadiusTR: 5, strokeOpacity: 0 });
        series.columns.template.adapters.add("fill", function (fill, target) {
            return chartdiv.get("colors").getIndex(series.columns.indexOf(target));
        });

        series.columns.template.adapters.add("stroke", function (stroke, target) {
            return chartdiv.get("colors").getIndex(series.columns.indexOf(target));
        });

        var jd =<%=GetJsonData()%>;
        // Set data
        var data = jd;

        xAxis.data.setAll(data);
        series.data.setAll(data);

        series.appear(1000);
        chartdiv.appear(1000, 100);

    }); // end am5.ready()
</script>

<!-- HTML -->
<div id="Agechartdiv"></div>--%>