$(document).ready(function () {
    setHeight();
});

function setHeight() {
    var browserHeight = $(window).height();
    var mapCenterHeight = browserHeight - 210;
    var mapCenter = document.getElementById("container");
    mapCenter.style.height = mapCenterHeight + 'px';
}