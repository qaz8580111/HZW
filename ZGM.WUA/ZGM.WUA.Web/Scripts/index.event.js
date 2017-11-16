$(function () {
    $("#menuHome").click(function () {
        $("#menuHome").attr("src", "../Images/Menu/Currency_Exchange_24px_1190753_easyicon_left.png");
        changeMenu("Views/Bottow/FirstPage.aspx");
        parent.list.close();
        parent.map.init();
    });
    $("#menuMapChange").click(function () {
        map.mapChange();
    });
    $("#menuCatalog").click(function () {
        $("#menuHome").attr("src", "../Images/Menu/changeToFirstPage.png");
        changeMenu("Views/Bottow/Catalog.aspx");
    });
    $("#toolsImg").click(function () {
        $("#menuHome").attr("src", "../Images/Menu/changeToFirstPage.png");
        changeMenu("Views/Bottow/Tools.aspx");
    });
});
