$(function () {
    detailMin.init(parent.person.User);
});

var detailMin = {
    User: null,
    init: function (user) {
        this.User = user;
        $(".pname").html(this.User.UserName);
        $(".pnumber").html(this.User.ZFZBH);
        $(".pposition").html(this.User.UserPositionName);
        if (this.User.SmallAvatar != null) {
            $('.t_left').html('');
            var str = '<img class="picoin" src="' + parent.globalConfig.smallHeadImgPath + this.User.SmallAvatar + '" />';
            $(".t_left").append(str);
            $('.picoin').css('padding-top', '0px');
        }
        detailMin.showMessageContent();
    },
    getSearchArea: function () {
        parent.person.getSearchArea(this.User);
    },
    initDetail: function () {
        parent.person.initDetail(this.User);
    },
    showMessageContent: function () {
        $('.pmessage').click(function () {
            parent.person.showMessageContent(detailMin.User);
        })       
    },
    foundCircum: function () {
        parent.person.foundCircum(this.User);
    },
    traceReplay: function () {
        parent.person.traceReplay(this.User);
    },
    close: function () {
        parent.detailMin.close();
    },
    foundCamera: function () {
        parent.cameraCenter.init(this.User);
    }
}