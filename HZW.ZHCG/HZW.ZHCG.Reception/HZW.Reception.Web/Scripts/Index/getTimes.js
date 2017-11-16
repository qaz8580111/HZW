$(document).ready(function () {
    setInterval(function () {
        getData();
    }, 1000);
});
function getData() {
    var time = document.getElementById("time");
    var yearMonthday = document.getElementById("yearMonthday");
    var date = new Date();
    var year = date.getFullYear();//当前年份
    var month = date.getMonth();//当前月份
    var days = date.getDay();//天
    var data = date.getDate();//天
    var hours = date.getHours();//小时
    var minute = date.getMinutes();//分
    var second = date.getSeconds();//秒
    time.innerHTML = hours + ":" + minute + ":" + second;
    yearMonthday.innerHTML = year + "." + month + "." + days + judgeDays(days);
}

function judgeDays(days) {
    var week = "";
    switch (days) {
        case 0:
            week = "星期天";
            break;
        case 1:
            week = "星期一";
            break;
        case 2:
            week = "星期二";
            break;
        case 3:
            week = "星期三";
            break;
        case 4:
            week = "星期四";
            break;
        case 5:
            week = "星期五";
            break;
        case 6:
            week = "星期六";
            break;
        default:
            return week;
    }
    return week;
}