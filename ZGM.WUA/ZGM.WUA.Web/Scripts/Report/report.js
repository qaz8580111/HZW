$(function () {
    report.init();
  

});

var report = {
    apiconfig: null,
    ReportSonViews:[],
    init: function () {
        this.apiconfig = parent.globalConfig.apiconfig;
        this.initHTML();
        this.ReportSonViews.push({path:"/Views/Report/SonView_Event/SJNRDWT.aspx",name:"事件难热点问题"});
        this.ReportSonViews.push({ path: "/Views/Report/SonView_Event/GLSJQS.aspx", name: "各类事件趋势 " });
        //this.ReportSonViews.push({ path: "/Views/Report/SonView_Event/SJLYFX.aspx", name: "事件来源分析 " });
        this.ReportSonViews.push({ path: "/Views/Report/SonView_Event/SJSBFX.aspx", name: "事件上报分析 " });
        //this.ReportSonViews.push({path:"/Views/Report/SonView_Event/SJXX.aspx",name:"事件难热点问题"});
        this.ReportSonViews.push({path:"/Views/Report/SonView_Event/ZJSJQS.aspx",name:"最近事件趋势"});

        this.ReportSonViews.push({ path: "/Views/Report/SonView_Person/DYQDTJ.aspx", name: "队员签到统计 " });
        this.ReportSonViews.push({path:"/Views/Report/SonView_Person/RYBJ.aspx",name:"人员报警"});
        //this.ReportSonViews.push({path:"/Views/Report/SonView_Person/RYBJQK.aspx",name:"事件难热点问题"});
        //this.ReportSonViews.push({ path: "/Views/Report/SonView_Person/RYBSXL.aspx", name: "人员办事效率 " });
        this.ReportSonViews.push({ path: "/Views/Report/SonView_Person/RYLCTJ.aspx", name: "人员路程前10名 " });
        //this.ReportSonViews.push({path:"/Views/Report/SonView_Person/RYLCTJ2.aspx",name:"事件难热点问题"});
      
        $('.slide-nav').html("");
        for (var i = 0; i < this.ReportSonViews.length; i++) {
            if (i == 0) {
                $('.slide-nav').append('<li class="selected" title="' + this.ReportSonViews[i].name + '">' + (i + 1) + '</li> ');
            } else {
                $('.slide-nav').append('<li class=""  title="' + this.ReportSonViews[i].name + '">' + (i + 1) + '</li> ');
            }
        }

        $("#ifmContent").attr("src", report.ReportSonViews[0].path);
        this.liClick();
    },
    resize: function () {
        this.initHTML();
    },
    initHTML: function () {
        var vh = document.body.clientHeight;
        var bh = $("#bottomDiv").height();

        $("#contentDiv").css("height", vh - bh);
    },
    changeReport: function (uri) {
        $("#ifmContent").attr("src", uri);
    },
    liClick: function () {
        $("ul.slide-nav li").each(function (i) {
            $(this).click(function () {
                $(this).addClass("selected").siblings().removeClass("selected");
                $("#ifmContent").attr("src",  report.ReportSonViews[i].path);
            });
        })
    }
}

//当页面改变大小的时候，重置菜单大小
document.body.onresize = function () {
    report.resize();
}