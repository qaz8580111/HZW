$(function () {
    detailMin.init(parent.NonConforBuild.NonConforBuildInfo);
});

var detailMin = {
    NonConforBuildInfo: null,
    init: function (NonConforBuildInfo) {
        this.NonConforBuildInfo = NonConforBuildInfo;
        $(".partsminposition").html(this.NonConforBuildInfo.IBUnitName);
        //$(".pposition").html(this.BMDInfo.TypeName);
        //$(".pnumber").html(this.BMDInfo.ZFZBH);
        //if (this.User.SLAvatar != null) {
        //    $('.t_left').html('');
        //    var str = '<img class="picoin" src="' + this.User.SLAvatar + '" />';
        //    $(".t_left").append(str);
        //    $('.picoin').css('padding-top', '0px');
        //}
    },
    initDetail: function () {
        parent.NonConforBuild.initDetail(detailMin.NonConforBuildInfo);
    },   
    close: function () {
        parent.detailMin.close();
    }
}