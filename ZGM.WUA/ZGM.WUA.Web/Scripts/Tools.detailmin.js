$(function () {
    detailMin.init(parent.mapEW.GraphicID);
});

var detailMin = {
    ID: null,
    init: function (ID) {
        this.ID = ID;
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Draw/GetDraw",
            data: { id: detailMin.ID },
            dataType: "json",
            success: function (data) {
                //$(".eventsminname").html(data.UserName);
                $(".pposition").html(data.Note);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
       
        //$(".pnumber").html(this.BMDInfo.ZFZBH);
        //if (this.User.SLAvatar != null) {
        //    $('.t_left').html('');
        //    var str = '<img class="picoin" src="' + this.User.SLAvatar + '" />';
        //    $(".t_left").append(str);
        //    $('.picoin').css('padding-top', '0px');
        //}
    },
    deleteGraphic: function () {
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + "/api/Draw/DeleteDraw",
            data: { id: detailMin.ID },
            dataType: "json",
            success: function (data) {
                parent.mapEW.deleteGraphic(detailMin.ID);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });       
    },
    foundCircum: function () {
        parent.BMD.foundCircum(this.ID);
    },
    traceReplay: function () {
        parent.person.traceReplay(this.ID);
    },
    close: function () {
        parent.detailMin.close();
    }
}