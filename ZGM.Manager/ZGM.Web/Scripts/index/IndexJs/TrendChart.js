var chartok;
var chartData = [];

AmCharts.ready(function () {
    // generate some random data first
    generateChartData();

    // SERIAL CHART
    chartok = new AmCharts.AmSerialChart();
    chartok.path = "../amcharts/";
    chartok.dataProvider = chartData;
    chartok.categoryField = "date";
    // listen for "dataUpdated" event (fired when chart is inited) and call zoomChart method when it happens
    chartok.addListener("dataUpdated", zoomChart);

    //AXES
    //category
    var categoryAxis = chartok.categoryAxis;
    categoryAxis.parseDates = true; // as our data is date-based, we set parseDates to true
    categoryAxis.minPeriod = "DD" // our data is daily, so we set minPeriod to DD
    categoryAxis.minorGridEnabled = true;
    categoryAxis.axisColor = "#DADADA";
    categoryAxis.twoLineMode = true;
    categoryAxis.dateFormats = [{
        period: 'DD',
        format: '　'
    }, {
        period: 'WW',
        format: ''
    }, {
        period: 'MM',
        format: ''
    }, {
        period: 'YYYY',
        format: ''
    }];

    // first value axis (on the left)
    var valueAxis1 = new AmCharts.ValueAxis();
    valueAxis1.axisColor = "#FF6600";
    valueAxis1.axisThickness = 2;
    valueAxis1.gridAlpha = 0;
    chartok.addValueAxis(valueAxis1);

    // second value axis (on the right)
    var valueAxis2 = new AmCharts.ValueAxis();
    valueAxis2.position = "right"; // this line makes the axis to appear on the right
    valueAxis2.axisColor = "#FCD202";
    valueAxis2.gridAlpha = 0;
    valueAxis2.axisThickness = 2;
    chartok.addValueAxis(valueAxis2);

    // third value axis (on the left, detached)
    valueAxis3 = new AmCharts.ValueAxis();
    valueAxis3.offset = 50; // this line makes the axis to appear detached from plot area
    valueAxis3.gridAlpha = 0;
    valueAxis3.axisColor = "#B0DE09";
    valueAxis3.axisThickness = 2;
    chartok.addValueAxis(valueAxis3);

    valueAxis4 = new AmCharts.ValueAxis();
    valueAxis4.offset = 50; // this line makes the axis to appear detached from plot area
    valueAxis4.gridAlpha = 0;
    valueAxis4.axisColor = "#DB4342";
    valueAxis4.axisThickness = 2;
    chartok.addValueAxis(valueAxis4);

    // GRAPHS
    // first graph
    var graph1 = new AmCharts.AmGraph();
    graph1.valueAxis = valueAxis1; // we have to indicate which value axis should be used
    graph1.title = "一般案件";
    graph1.valueField = "visits";
    graph1.bullet = "round";
    graph1.hideBulletsCount = 30;
    graph1.bulletBorderThickness = 1;
    graph1.balloonText = null;
    chartok.addGraph(graph1);

    // second graph
    var graph2 = new AmCharts.AmGraph();
    graph2.valueAxis = valueAxis2; // we have to indicate which value axis should be used
    graph2.title = "执法事件";
    graph2.valueField = "hits";
    graph2.bullet = "round";
    graph2.balloonText = null;
    graph2.hideBulletsCount = 30;
    graph2.bulletBorderThickness = 1;
    chartok.addGraph(graph2);

    // third graph
    var graph3 = new AmCharts.AmGraph();
    graph3.valueAxis = valueAxis3; // we have to indicate which value axis should be used
    graph3.valueField = "views";
    graph3.title = "行政审批";
    graph3.bullet = "round";
    graph3.balloonText = null;
    graph3.hideBulletsCount = 30;
    graph3.bulletBorderThickness = 1;
    chartok.addGraph(graph3);

    var graph4 = new AmCharts.AmGraph();
    graph4.valueAxis = valueAxis3; // we have to indicate which value axis should be used
    graph4.valueField = "restone";
    graph4.title = "简易事件";
    graph4.balloonText = null;
    graph4.bullet = "round";
    graph4.hideBulletsCount = 30;
    graph4.bulletBorderThickness = 1;
    chartok.addGraph(graph4);

    // CURSOR
    var chartCursor = new AmCharts.ChartCursor();
    chartCursor.valueLineEnabled = true;
    chartCursor.categoryBalloonDateFormat = "YYYY年MM月DD日";
    chartok.addChartCursor(chartCursor);

    // SCROLLBAR
    //var chartScrollbar = new AmCharts.ChartScrollbar();
    //chartok.addChartScrollbar(chartScrollbar);

    // LEGEND
    var legend = new AmCharts.AmLegend();
    legend.marginLeft = 110;
    chartok.addLegend(legend);

    // WRITE
    chartok.write("divtrend");
});

//获取数据
function generateChartData() {
    var sourceone;
    var sourcetwo;
    var sourcethree;
    var sourcefour;

    //一般案件
    var visits;
    //执法事件
    var hits;
    //行政审批
    var views;
    //简易事件
    var restone;
    a();
    function a() {
        $.ajax({
            type: "POST",
            async: false,
            url: "/PersonalWorkbench/returnStr",
            success: function (data) {
                sourceone = data.split("'")[0];
                sourcetwo = data.split("'")[1];
                sourcethree = data.split("'")[2];
                sourcefour = data.split("'")[3];
                ok();
            }, error: function () {
                return null;
            }
        });
    }
    function ok() {
        //一般案件
        var arrayone = sourceone.split(",");
        //执法事件
        var arraytwo = sourcetwo.split(",");
        //行政审批
        var arraythree = sourcethree.split(",");
        //简易事件
        var arrayfour = sourcefour.split(",");

        //获取服务器时间
        var year;
        var month;
        var YearandMonth = document.getElementById("divServiceTime").innerText;
        year = YearandMonth.split(",")[0];
        month = YearandMonth.split(",")[1];
        var truemonth;
        switch (month) {
            case "1":
                truemonth = 0;
                break;
            case "2":
                truemonth = 1;
                break;
            case "3":
                truemonth = 2;
                break;
            case "4":
                truemonth = 3;
                break;
            case "5":
                truemonth = 4;
                break;
            case "6":
                truemonth = 5;
                break;
            case "7":
                truemonth = 6;
                break;
            case "8":
                truemonth = 7;
                break;
            case "9":
                truemonth = 8;
                break;
            case "10":
                truemonth = 9;
                break;
            case "11":
                truemonth = 10;
                break;
            case "12":
                truemonth = 11;
                break;
        }
        //从当月1号开始
        var j = 1;
        for (var i = 1; i <= (arrayone.length - 1) ; i++) {
            if (i % 2 != 0) {
                var newDate = new Date();

                newDate.setFullYear(year, truemonth, j);
                visits = arrayone[i];
                hits = arraytwo[i];
                views = arraythree[i];
                restone = arrayfour[i];

                chartData.push({
                    date: newDate,
                    visits: visits,
                    hits: hits,
                    views: views,
                    restone: restone
                });
                j++;
            }
        }
    }
}

// this method is called when chart is first inited as we listen for "dataUpdated" event
function zoomChart() {
    // different zoom methods can be used - zoomToIndexes, zoomToDates, zoomToCategoryValues
    chartok.zoomToIndexes(0, 40);
}