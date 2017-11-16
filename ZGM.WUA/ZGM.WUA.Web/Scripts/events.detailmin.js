$(function () {

    detailMin.init(parent.event.Event);
});

var detailMin = {
    Event: null,
    init: function (event) {
        if (event.SourceId != 3 || event.Remark1 == "" || event.Remark1 == null) {
            $("#review").css("display", "none");
        } else {
            $("#review").css("display", "inline-block");
        }
        this.Event = event;
        var date = this.Event.FoundTime;
        $(".eventsminname").html(this.Event.EventTitle);
        try{
            $(".pposition").html(date.replace('T', ' '));
        } catch (e)
        {
            console.log(e);
        }
        if (event.LevelNum == 1) {
            $(".policecase").css("visibility", "hidden");
        }
        
        //$(".pposition").html(User.UserPositionName);
    },
    initHistoryDetail: function () {
        if (this.Event.Remark1 == null || this.Event.Remark1 == "") {
            return;
        }
        try{
            var cameraInfo = this.Event.Remark1.split(',');
            $.ajax({
                type: "GET",
                async: true,
                url: parent.globalConfig.apiconfig + "/api/Camera/GetCameraInfo",
                data: { cameraId: cameraInfo[0] },
                dataType: "json",
                success: function (data) {
                    if (data == null) {
                        return;
                    }
                    parent.document.cookie = "param=" + JSON.stringify(data);
                    var CameraId = data.CameraId;
                    var CameraName = encodeURI(encodeURI(data.CameraName));
                    var IndexCode = data.IndexCode;
                    var StartTime = cameraInfo[1];
                    var EndTime = cameraInfo[2];
                    var param = "?CameraId=" + CameraId + "&CameraName=" + CameraName + "&IndexCode=" + IndexCode + "&StartTime=" + StartTime + "&EndTime=" + EndTime;
                    window.open("/Views/Camera/CameraPlayBack.aspx" + param, "_blank", "top=60,left=300,width=815, height=515");
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });
        }
        catch (e)
        {
            console.log(e);
        }
       
    },
    initDetail: function () {
        parent.event.initDetail(this.Event);
    },
    foundCircum: function () {
        parent.event.foundCircum(this.Event);
    },
    close: function () {
        parent.detailMin.close();
    }
}