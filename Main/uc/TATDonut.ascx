<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TATDonut.ascx.cs" Inherits="InternalLims.Main.uc.TATDonut" %>

<style>
.chart-inner {padding: 20px; width:50%}
#BA-chart-job-error {margin: 0 auto;}
</style>
<script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.0/Chart.min.js"></script>
<script src="https://cdn.jsdelivr.net/gh/emn178/chartjs-plugin-labels/src/chartjs-plugin-labels.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-doughnutlabel/2.0.3/chartjs-plugin-doughnutlabel.js"></script>
<script>
    var j =<%=GetJsonData()%>;
    var BAChartDataValue = j; /* 개발 연동 데이터 */
    var BAChartDataLabel = [
        'Dew Soon',
        'In Lab Today',
        'Tat is Today',
        'Tat Exceeded',
        'Tat Exceeded'
    ]; /* 개발 연동 데이터 */
    var BAChartJobErrColors = [
        'rgba(255,217,102)',
        'rgba(56,118,29)',
        'rgba(217,76,159)',
        'rgba(255, 76, 76)'
    ];

    var BAChartCountTotal = 0;
    if (BAChartDataValue.length > 0) {
        BAChartCountTotal = BAChartDataValue.reduce(function (acc, currentVal, currentIdx, arr) {
            return acc + currentVal;
        }, 0);
    }

    window.addEventListener('load', function () {
        var BAChartCtx = document.getElementById('BA-chart-job-error').getContext('2d');
        var BAChartJobErr = new Chart(BAChartCtx, {
            type: 'pie',
            data: {
                labels: BAChartDataLabel,
                datasets: [{
                    data: BAChartDataValue,
                    backgroundColor: BAChartJobErrColors,
                    borderWidth: 1,
                }]
            },
            options: {
                responsive: false,
                maintainAspectRatio: false,
                //title: {
                //    display: true,
                //    position: 'top',
                //    fontSize: 16,
                //    fontColor: '#000',
                //    fontStyle: 'bold',
                //    padding: 24
                //    //text: 'Turnaround Time',
                //},
                plugins: {
                    labels: [
                        {
                            render: 'label',
                            fontColor: '#000',
                            position: 'outside'
                        },
                        {
                            render: 'percentage',
                            fontColor: '#fff',
                        }
                    ],
                    doughnutlabel: {
                        labels: [
                            {
                                text: 'Total: ' + BAChartCountTotal,
                            }
                        ]
                    }
                },
                legend: {
                    display: false
                }
            }
        });
    });
</script>

<div class="chart-inner">
	<canvas id="BA-chart-job-error" width="350" height="310" ></canvas>
</div>