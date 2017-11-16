$(function () {
    detailMin.init(parent.Constr.ConstrInfo);
});

var detailMin = {
    ConstrInfo: null,
    init: function (ConstrInfo) {
        this.ConstrInfo = ConstrInfo;
        $(".pname").html(this.ConstrInfo.ConstrName);
        $(".pposition").html(this.ConstrInfo.Address);
        //$(".pnumber").html(this.BMDInfo.ZFZBH);
        //if (this.User.SLAvatar != null) {
        //    $('.t_left').html('');
        //    var str = '<img class="picoin" src="' + this.User.SLAvatar + '" />';
        //    $(".t_left").append(str);
        //    $('.picoin').css('padding-top', '0px');
        //}
    },
    initDetail: function () {
        parent.Constr.initDetail(detailMin.ConstrInfo);
    },
    foundCircum: function () {
        parent.Constr.foundCircum(this.User);
    },
    traceReplay: function () {
        parent.Constr.traceReplay(this.User);
    },
    close: function () {
        parent.detailMin.close();
    }
}