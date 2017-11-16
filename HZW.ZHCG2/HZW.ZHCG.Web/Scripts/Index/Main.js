$(document).ready(function () {
    setHeight();


    var angle = 0;

    setInterval(function () {

        angle += 3;

        $("#earth").rotate(angle);

    }, 50);

    setInterval(function () {

        angle += 3;

        $("#setUp").rotate(angle);

    }, 50);
});

function setHeight() {
    var browserHeight = $(window).height();
    var mapCenterHeight = browserHeight - 70;
    var mapCenter = document.getElementById("__g");
    mapCenter.style.height = mapCenterHeight + 'px';
}