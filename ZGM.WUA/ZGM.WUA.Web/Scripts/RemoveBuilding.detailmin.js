$(function () {
    detailMin.init(parent.RemoveBuilding.RemoveBuildingInfo);
});

var detailMin = {
    RemoveBuildingInfo: null,
    init: function (RemoveBuilding) {
        this.RemoveBuildingInfo = RemoveBuilding;
        $($(".partsminposition")[0]).html(this.RemoveBuildingInfo.ProjectName);
        var houserName = this.RemoveBuildingInfo.HouseHolder == null ? "" : this.RemoveBuildingInfo.HouseHolder;
        $($(".partsminposition")[1]).html(houserName);
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
        parent.RemoveBuilding.initDetail(detailMin.RemoveBuildingInfo);
    },   
    close: function () {
        parent.detailMin.close();
    }
}