$(function () {
    detailMin.init(parent.BMD.BMDInfo);
    parent.mapSW.deepView();
});

var detailMin = {
    BMDInfo: null,
    init: function (BMDInfo) {
        this.BMDInfo = BMDInfo;
        $(".pname").html(this.BMDInfo.Name);
        $(".pposition").html(this.BMDInfo.TypeName);
        //$(".pnumber").html(this.BMDInfo.ZFZBH);
        //if (this.User.SLAvatar != null) {
        //    $('.t_left').html('');
        //    var str = '<img class="picoin" src="' + this.User.SLAvatar + '" />';
        //    $(".t_left").append(str);
        //    $('.picoin').css('padding-top', '0px');
        //}
    },
    initDetail: function () {
        parent.BMD.initDetail(detailMin.BMDInfo);
    },
    foundCircum: function () {
        parent.BMD.foundCircum(this.User);
    },
    traceReplay: function () {
        parent.person.traceReplay(this.User);
    },
    close: function () {
        parent.detailMin.close();
    }
}