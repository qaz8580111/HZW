gvChartInit();
$(document).ready(function () {
    $('#myTable5').gvChart({
        chartType: 'PieChart',
        gvSettings: {
            vAxis: { title: 'No of players' },
            hAxis: { title: 'Month' },
            width: 440,
            height: 250,
        }
    });
});
