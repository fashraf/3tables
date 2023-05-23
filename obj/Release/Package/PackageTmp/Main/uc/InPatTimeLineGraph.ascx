<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InPatTimeLineGraph.ascx.cs" Inherits="InternalLims.Main.uc.InPatTimeLineGraph" %>
<style>
#area-chart
{
  min-height: 250px;
}
</style>
<div id="areaChart" ></div>
<script>
    var j =<%=GetJsonData()%>;
    const monthNames = ["", "Jan", "Feb", "Mar", "Apr", "May", "Jun","Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    Morris.Area({
        element: 'areaChart',
        data: j,
        xkey: 'y',
        parseTime: false,
        ykeys: ['a'],
        xLabelFormat: function (x) {
            var index = parseInt(x.src.y);
            return monthNames[index];
        },
        xLabels: "month",
        labels: ['Total Sales'],
        fillOpacity: 0.6,
        hideHover: 'auto',
        behaveLikeLine: true,
        resize: true,
        pointFillColors: ['#ffffff'],
        pointStrokeColors: ['black'],
        lineColors: ['red'],
        padding:10
    });
</script>