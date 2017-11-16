$(function () {
    var chart;
    var totalMoney = 50000;

    $(document).ready(function () {
        var caseListCount = Number($("#caseListCount").val());
        var simpCaseCount = Number($("#simpCaseCount").val());
        var zfsjListCount = Number($("#zfsjListCount").val());

        $("#div_pie").highcharts({
            chart: {
                renderTo: 'container',
                type: 'pie',
                marginTop: 10,
                height: 300,
            },
            title: {
                align: "left",
                text: '网上案件统计',
                style: {
                    "fontSize": "14px",
                    "backgroundColor": "red"
                }
            },

            plotOptions: {
                pie: {
                    startAngle: -90,
                    endAngle: 90,
                    center: ['50%', '70%']
                }
            },
            series: [{
                data: [
                    ['一般案件', caseListCount],
                    ['简易案件', simpCaseCount],
                    ['执法事件', zfsjListCount],
                ]
            }]
        });
    });
});
