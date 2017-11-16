$(function () {
    detail.init(parent.BMD.BMDInfo);
});
var detail = {
    BMDInfo: null,
    init: function (BMDInfo) {
        $(".minbtn").toggle(function () {
            detail.collapse();
        }, function () {
            detail.expand();
        });
        this.BMDInfo = BMDInfo;
        if ($("div:contains('姓名')+div").length > 0) {
            $("div:contains('姓名')+div")[0].textContent = BMDInfo.Name == null ? "" : BMDInfo.Name;
        }
        if ($("div:contains('别名')+div").length > 0) {
            $("div:contains('别名')+div")[0].textContent = BMDInfo.OtherName == null ? "" : BMDInfo.OtherName;
        }
        if ($("div:contains('性别')+div").length > 0) {
            $("div:contains('性别')+div")[0].textContent = BMDInfo.Sex == null ? "" : BMDInfo.Sex;
        }
        if ($("div:contains('民族')+div").length > 0) {
            $("div:contains('民族')+div")[0].textContent = BMDInfo.Nation == null ? "" : BMDInfo.Nation;
        }
        if ($("div:contains('出生日期')+div").length > 0) {
            $("div:contains('出生日期')+div")[0].textContent = BMDInfo.Birthday == null ? "" : BMDInfo.Birthday.substr(0, 10);
        }
        if ($("div:contains('文化程度')+div").length > 0) {
            $("div:contains('文化程度')+div")[0].textContent = BMDInfo.EDU == null ? "" : BMDInfo.EDU;
        }
        if ($("div:contains('职业')+div").length > 0) {
            $("div:contains('职业')+div")[0].textContent = BMDInfo.Job == null ? "" : BMDInfo.Job;
        }
        if ($("div:contains('原政治面貌')+div").length > 0) {
            $("div:contains('原政治面貌')+div")[0].textContent = BMDInfo.Political == null ? "" : BMDInfo.Political;
        }
        if ($("div:contains('类型')+div").length > 0) {
            $("div:contains('类型')+div")[0].textContent = BMDInfo.TypeName == null ? "" : BMDInfo.TypeName;
        }
        if ($("div:contains('身份证')+div").length > 0) {
            $("div:contains('身份证')+div")[0].textContent = BMDInfo.IDCard == null ? "" : BMDInfo.IDCard;
        }
        if ($("div:contains('籍贯')+div").length > 0) {
            $("div:contains('籍贯')+div")[0].textContent = BMDInfo.BirthPlace == null ? "" : BMDInfo.BirthPlace;
        }
        if ($("div:contains('户籍所在地')+div").length > 0) {
            $("div:contains('户籍所在地')+div")[0].textContent = BMDInfo.DomicilePlace == null ? "" : BMDInfo.DomicilePlace;
        }
        if ($("div:contains('固定居住地')+div").length > 0) {
            $("div:contains('固定居住地')+div")[0].textContent = BMDInfo.FixedPlace == null ? "" : BMDInfo.FixedPlace;
        }
        //if ($("div:contains('常用居住地')+div").length > 0) {
        //    $("div:contains('常用居住地')+div")[0].textContent = BMDInfo.CommonPlace == null ? "" : BMDInfo.CommonPlace;
        //}
    },
    collapse: function () {
        parent.detail.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/ppolice/ppolicebg1.png')");
        }, 300);
    },
    expand: function () {
        parent.detail.expand();
        $("body").css("background-image", "url('/images/ppolice/ppolicebg.png')");
    },
    close: function () {
        parent.detail.close();
    }
}