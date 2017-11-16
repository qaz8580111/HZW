$(function () {
	detailMin.init(parent.Camera.CameraInfo);
	parent.mapSW.deepView();
});

var detailMin = {
	CameraInfo: null,
	init: function (CameraInfo) {
		this.CameraInfo = CameraInfo;
		$(".pname").html(this.CameraInfo.CameraName);
		$(".pposition").html(this.CameraInfo.CameraTypeName);
	},
	initHistoryDetail: function () {
	    console.log(detailMin.CameraInfo);
	    parent.document.cookie = "param=" + JSON.stringify(detailMin.CameraInfo);
	    var CameraId = detailMin.CameraInfo.CameraId;
	    var CameraName = encodeURI(encodeURI(detailMin.CameraInfo.CameraName));
	    var IndexCode = detailMin.CameraInfo.IndexCode;
	    var StartTime = "";
	    var EndTime = "";

	    var param = "?CameraId=" + CameraId + "&CameraName=" + CameraName + "&IndexCode=" + IndexCode + "&StartTime=" + StartTime + "&EndTime=" + EndTime;
	    window.open("CameraPlayBack.aspx" + param, "_blank", "top=60,left=300,width=1000, height=515");
	},
	initDetail: function () {
	    parent.Camera.initDetail(detailMin.CameraInfo);

	},
	foundCircum: function () {
	    parent.Camera.foundCircumCamera(detailMin.CameraInfo);
	},
	close: function () {
		parent.detailMin.close();
	},
	//foundCircum: function () {
	//    parent.Camera.foundCircumCamera(this.CameraInfo);
	//}
}