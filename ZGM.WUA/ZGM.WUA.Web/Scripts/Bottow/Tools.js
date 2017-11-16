$(function () {
    tools.changeSize();
});
var param = {
    bottomDiv_Width: 0,
    showTableID: ""
};
document.body.onresize = function () {
    tools.changeSize();
}

//工具页事件
var tools = {
    //窗体大小改变
    changeSize: function () {
        param.bottomDiv_Width = document.body.clientWidth;

        $('#opera')[0].clientWidth = param.bottomDiv_Width;

        $('.tb').css("width", param.bottomDiv_Width);
        $('.fullTb').css("width", param.bottomDiv_Width - 20);
    },
    //标点点击
    bdOnClick: function () {
        parent.map.changeMapToEW();
        parent.map.tools("Point");
    },
    //画线点击
    hxOnClick: function () {
        parent.map.changeMapToEW();
        parent.map.tools("Polyline");
    },
    //测距点击
    cjOnClick: function () {
        parent.map.tools("PolylineWithLen");
    },
    //测面点击
    cmOnClick: function () {
        parent.map.tools("PolygonWithArea");
    },
    //画面点击
    hmOnClick: function () {
        parent.map.changeMapToEW();
        parent.map.tools("Polygon");
    },
    //框选点击
    kxOnClick: function () {
        parent.map.changeMapToEW();
        parent.map.recSearch();
    },
    //三维点击
    swOnClick: function () {
        parent.map.selectModel();
    },
    clears: function () {
        parent.clearInterval(parent.globalConfig.refreshPosition);
        parent.map.clears();
    },
}