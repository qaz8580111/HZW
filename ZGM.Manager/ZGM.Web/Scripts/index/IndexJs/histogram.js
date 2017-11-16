
var chart;
var todaypeople;
var onlinepeople;
var monthpeople;
var fourpeople;
var chartDataMobile;

//当前定位人数
function onek() {
    $.ajax({
        type: "GET",
        url: "/PersonalWorkbench/GettodayPeople",
        async: false,
        success: function (data) {
            todaypeople = data;
        },
        error: function () {
            todaypeople = 0;
        }
    });
}
//返回当前在线人数
function two() {
    $.ajax({
        type: "GET",
        url: "/PersonalWorkbench/GettodayOnlinePeople",
        async: false,
        success: function (data) {
            onlinepeople = data;
        },
        error: function () {
            onlinepeople = 0;
        }
    });
}
//返回当月在线人数
function three() {
    $.ajax({
        type: "GET",
        url: "/PersonalWorkbench/GetMothOnlinePeople",
        async: false,
        success: function (data) {
            monthpeople = data;
        },
        error: function () {
            monthpeople = 0;
        }
    });
}
//返回当月定位在线人数
function four() {
    $.ajax({
        type: "GET",
        url: "/PersonalWorkbench/FourPeople",
        async: false,
        success: function (data) {
            fourpeople = data;
        },
        error: function () {
            fourpeople = 0;
        }
    });
}
function BindData() {
    chartDataMobile = [
      {
          "year": "",
          "当前定位人数": todaypeople,
          "当前在线人数": onlinepeople,
          "当月在线人数": monthpeople,
          "当月定位人数": fourpeople
      }
    ];
}
AmCharts.ready(function () {
    onek();
    two();
    three();
    four();
    BindData();
    // SERIAL CHART
    chart = new AmCharts.AmSerialChart();
    chart.dataProvider = chartDataMobile;
    chart.categoryField = "year";
    chart.startDuration = 1;
    chart.plotAreaBorderColor = "#DADADA";
    chart.plotAreaBorderAlpha = 1;
    // 设置横向纵向
    chart.rotate = true;

    // AXES
    // Category
    var categoryAxis = chart.categoryAxis;
    categoryAxis.gridPosition = "start";
    categoryAxis.gridAlpha = 0.1;
    categoryAxis.axisAlpha = 0;

    // Value
    var valueAxis = new AmCharts.ValueAxis();
    valueAxis.axisAlpha = 0;
    valueAxis.gridAlpha = 0.1;
    valueAxis.position = "top";
    chart.addValueAxis(valueAxis);

    // GRAPHS
    // first graph
    var graph5 = new AmCharts.AmGraph();
    graph5.type = "column";
    graph5.title = "当前定位人数";
    graph5.valueField = "当前定位人数";
    graph5.balloonText = "当前定位人数:[[value]]";
    graph5.lineAlpha = 0;
    graph5.fillColors = "#ADD981";
    graph5.fillAlphas = 1;
    chart.addGraph(graph5);

    // second graph
    var graph6 = new AmCharts.AmGraph();
    graph6.type = "column";
    graph6.title = "当前在线人数";
    graph6.valueField = "当前在线人数";
    graph6.balloonText = "当前在线人数:[[value]]";
    graph6.lineAlpha = 0;
    graph6.fillColors = "#81acd9";
    graph6.fillAlphas = 1;
    chart.addGraph(graph6);

    var graph8 = new AmCharts.AmGraph();
    graph8.type = "column";
    graph8.title = "当月定位人数";
    graph8.valueField = "当月定位人数";
    graph8.balloonText = "当月定位人数:[[value]]";
    graph8.lineAlpha = 0;
    graph8.fillColors = "#FC405A";
    graph8.fillAlphas = 1;
    chart.addGraph(graph8);

    var graph7 = new AmCharts.AmGraph();
    graph7.type = "column";
    graph7.title = "当月在线人数";
    graph7.valueField = "当月在线人数";
    graph7.balloonText = "当月在线人数:[[value]]";
    graph7.lineAlpha = 0;
    graph7.fillColors = "#007AFF";
    graph7.fillAlphas = 1;
    chart.addGraph(graph7);

    // LEGEND
    var legend5 = new AmCharts.AmLegend();
    legend5.valueWidth = 10;
    chart.addLegend(legend5);

    chart.creditsPosition = "top-right";

    // WRITE
    chart.write("chartdiv");
});