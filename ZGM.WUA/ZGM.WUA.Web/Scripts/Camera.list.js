$(function () {   
    list.init();
});
//$('#scrollDiv').niceScroll({
//    cursorcolor: "#ccc",//#CC0071 光标颜色
//    cursoropacitymax: 1, //改变不透明度非常光标处于活动状态（scrollabar“可见”状态），范围从1到0
//    touchbehavior: false, //使光标拖动滚动像在台式电脑触摸设备
//    cursorwidth: "5px", //像素光标的宽度
//    cursorborder: "0", // 	游标边框css定义
//    cursorborderradius: "5px",//以像素为光标边界半径
//    autohidemode: false //是否隐藏滚动条
//});
var list = {
    cameraList: null,
    cameraInfo:[],
    init: function () {
        $(".minbtn").toggle(function () {
            list.collapse();
        }, function () {
            list.expand();
        });
        this.tabClick();
        $('#allbg').css("display", "inline");
        $.ajax({
            type: "GET",
            async: true,
            url: parent.globalConfig.apiconfig + '/api/Camera/GetCameraUnitsJson',          
            dataType: "json",
            success: function (result) {
                $('#allbg').css("display", "none");
                list.cameraList = result;
                list.cameraInfo = [];
                list.getAllCamera(result);
            
                $('#cameraTree').tree({
                    data: result,
                    animate: true,
                    formatter: function (node) {
                        var s = node.text;
                        if (node.children) {
                            s += ' <span style=\'color:#56e15c\'>(' + node.children.length + ')</span>';
                        }
                        return s;
                    },
                    onClick: function (node) {
                        if (node.iconCls != "icoNoImg") {
                            list.showDetailMin(node);
                        }
                    },
                });
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
       
    },
    searchClick: function () {
        if (this.getSearchContent() != "") {
            $('#ztCameraTree').css("display", "none");
            $('#cameraTree').css("display", "none");
            $("#searchTree").css("display", "inline");
            $('#allbg').css("display", "inline");
            $.ajax({
                type: "GET",
                async: true,
                url: parent.globalConfig.apiconfig + '/api/Camera/GetCamerasJson',
                data: {
                    cameraName: list.getSearchContent()
                },
                dataType: "json",
                success: function (result) {
                    $('#allbg').css("display", "none");
                    list.cameraList = result;
                    list.cameraInfo = [];
                    list.getAllCamera(result);
                    $('#searchTree').tree({
                        data: result,
                        animate: true,
                        formatter: function (node) {
                            var s = node.text;
                            if (node.children) {
                                s += ' <span style=\'color:#56e15c\'>(' + node.children.length + ')</span>';
                            }
                            return s;
                        },
                        onClick: function (node) {
                            if (node.iconCls != "icoNoImg") {
                                list.showDetailMin(node);
                            }
                        },
                    });                    
                    if (result.length == 0) {
                        $("#searchTree").html('<p style="margin:0px;font-size:14px;color:#fff;text-align: center;line-height:200px">暂无数据</p>');
                    }
                },
                error: function (errorMsg) {
                    console.log(errorMsg);
                }
            });
            //$('#searchTree').tree({
            //    url: parent.globalConfig.apiconfig + '/api/Camera/GetCamerasJson?cameraName=' + list.getSearchContent(),
            //    method: 'get',
            //});
        }
        else {
            $('#ztCameraTree').css("display", "none");
            $('#searchTree').css("display", "none");
            $('#cameraTree').css("display", "inline");
            //$('#allbg').css("display", "inline");
            //$.ajax({
            //    type: "GET",
            //    async: true,
            //    url: parent.globalConfig.apiconfig + '/api/Camera/GetCameraUnitsJson',
            //    dataType: "json",
            //    success: function (result) {
            //        $('#allbg').css("display", "none");
            //        $('#cameraTree').tree({
            //            data: result,
            //            animate: true,
            //            formatter: function (node) {
            //                var s = node.text;
            //                if (node.children) {
            //                    s += ' <span style=\'color:#56e15c\'>(' + node.children.length + ')</span>';
            //                }
            //                return s;
            //            },
            //        });
            //    },
            //    error: function (errorMsg) {
            //        console.log(errorMsg);
            //    }
            //});
        }
    },
    showDetailMin: function (node) {
        parent.Camera.initCameraDeatilMin(node);
    },
    positions: function (children) {
        parent.Camera.positionCameras(children);
    },
    getSearchContent: function () {
        return $("#search").val() == "请输入搜索内容" ? "" : $("#search").val();
    },  
    tabClick: function () {
        $('.tab div').each(function (i) {
            $(this).click(function () {
                $(this).addClass("tabChecked").siblings().removeClass("tabChecked");
                if (i == 0) {
                    $('#ztCameraTree').css("display", "none");
                    $('#searchTree').css("display", "none");
                    $('#cameraTree').css("display", "inline");
                    $('#allbg').css("display", "inline");
                    $.ajax({
                        type: "GET",
                        async: true,
                        url: parent.globalConfig.apiconfig + '/api/Camera/GetCameraUnitsJson',
                        dataType: "json",
                        success: function (result) {
                            $('#allbg').css("display", "none");
                            list.cameraList = result;
                            list.cameraInfo = [];
                            list.getAllCamera(result);
                            $('#cameraTree').tree({
                                data: result,
                                animate: true,
                                formatter: function (node) {
                                    var s = node.text;
                                    if (node.children) {
                                        s += ' <span style=\'color:#56e15c\'>(' + node.children.length + ')</span>';
                                    }
                                    return s;
                                },
                                onClick: function (node) {
                                    if (node.iconCls != "icoNoImg") {
                                        list.showDetailMin(node);
                                    }
                                },
                            });
                        },
                        error: function (errorMsg) {
                            console.log(errorMsg);
                        }
                    });
                }
                else if (i == 1) {
                    $('#cameraTree').css("display", "none");
                    $('#searchTree').css("display", "none");
                    $('#ztCameraTree').css("display", "inline");
                    $('#allbg').css("display", "inline");
                    var dd = '[{"id":"9","text":"test","state":"closed","iconCls":"icoNoImg","children":[]},{"id":"2","text":"test2","state":"closed","iconCls":"icoNoImg","children":[]}]';
                    $.ajax({
                        type: "GET",
                        async: true,
                        url: parent.globalConfig.apiconfig + '/api/Camera/GetCameraThemesJson',
                        dataType: "json",
                        success: function (result) {
                            $('#allbg').css("display", "none");
                            list.cameraList = result;
                            list.cameraInfo = [];
                            list.getAllCamera(result);
                            $('#ztCameraTree').tree({
                                data: result,
                                animate: true,
                                formatter: function (node) {
                                    var s = node.text;
                                    if (node.children) {
                                        s += ' <span style=\'color:#56e15c\'>(' + node.children.length + ')</span>';
                                    }
                                    return s;
                                },
                                onClick: function (node) {
                                    if (node.iconCls != "icoNoImg") {
                                        list.showDetailMin(node);
                                    }
                                    else {
                                        if (node.children.length > 0) {
                                            if (node.children[0].iconCls == "icoKQCamera" || node.children[0].iconCls == "icoQJCamera") {
                                                list.positions(node.children);
                                            }
                                        }
                                    }
                                },
                            });
                        },
                        error: function (errorMsg) {
                            console.log(errorMsg);
                        }
                    });
                }
            })
        })
    },
    collapse: function () {
        parent.list.collapse();
        setTimeout(function () {
            $("body").css("background-image", "url('/images/list/listdivbg5.png')");
        }, 300);
    },
    expand: function () {
        parent.list.expand();
        $("body").css("background-image", "url('/images/list/listdivbg4.png')");
    },
    close: function () {
        parent.list.close();
    },
    getAllCamera: function (obj) {
        if (obj != null) {
            for (var i = 0; i < obj.length;i++){
                if (obj[i].iconCls != "icoNoImg" || obj[i].iconCls == "icoQJCamera" || obj[i].iconCls == "icoKQCamera") {
                    list.cameraInfo.push(obj[i]);
                }
                else {
                    list.getAllCamera(obj[i].children)
                }
            }
            //list.positionAllCamera();
        }
    },
    positionAllCamera:function(){
        parent.Camera.positionCameras2(list.cameraInfo);
    }
};


//var dd = '[{"id":"0001","text":"1","checked":false,"children":[{"id":"00010001","text":"4"}]},{"id":"000100010002","text":"8"},{"id":"00010002","text":"5"},{"id":"00010003","text":"6"},{"id":"0002","text":"2"},{"id":"0003","text":"3","checked":false}]    ';
//$('#cameraTree').tree({
//    url: parent.globalConfig.apiconfig + '/api/Camera/GetCameraUnitsJson?unitId=',
//    method:'get',
//    //checkbox: false,
//    ////url: '/category/getCategorys.java?Id=0',
//    //onBeforeExpand: function (node, param) {
//    //    $('#cameraTree').tree('options').url = "http://localhost:5618/api/Parts/GetCameraDept?id=" + Math.random();
//    //},
//    //data: eval(dd)
//    //onClick: function (node) {
//    //    var state = node.state;
//    //    if (!state) {                                   //判断当前选中的节点是否为根节点
//    //        currentId = node.id;
//    //        $("#chooseOk").attr("disabled", false);   //如果为根节点 则OK按钮可用
//    //    } else {
//    //        $("#chooseOk").attr("disabled", true);    //如果不为根节点 则OK按钮不可用
//    //    }
//    //}
//});