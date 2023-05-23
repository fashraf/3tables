<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NIPTHeatMap.ascx.cs" Inherits="InternalLims.Main.uc.NIPTHeatMap" %>
<script src="../../assets/js/jquery-2.1.1.js"></script>
<script src="../../assets/js/moment.js"></script>
<%--<link href="../../assets/heatmap/css/jquery.calmosaic.min.css" rel="stylesheet" />
<script src="../../assets/heatmap/js/jquery.calmosaic.min.js"></script>--%>
<link href="../../assets/heatmap/css/jquery.CalendarHeatmap.css" rel="stylesheet" />
<script src="../../assets/heatmap/js/jquery.CalendarHeatmap.min.js"></script>
   
                <div id="heatmap-1"></div>
              

    <script>
        var data = [<%=GetTypeChart()%>];
        $("#heatmap-1").CalendarHeatmap(data, {
            months: 5,
            lastMonth: moment().month() + 4,
            coloring: "green",
            legend: {
                align: "right",
                minLabel: "Fewer"
            },
            weekStartDay: 0,
            labels: {
                days: true,
                custom: {
                    monthLabels: "MMM 'YY"
                }
            },
            tooltips: {
                show: true
            }
        });

</script>