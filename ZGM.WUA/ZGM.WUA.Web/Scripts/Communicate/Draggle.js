var rDrag = {
    o: null,
    init: function (o) {
        o.onmousedown = this.start;

    },
    start: function (e) {
        var o;
        e = rDrag.fixEvent(e);
        e.preventDefault && e.preventDefault();
        rDrag.o = o = this;
        o.x = e.clientX - rDrag.o.parentElement.offsetLeft;
        o.y = e.clientY - rDrag.o.parentElement.offsetTop;
        document.onmousemove = rDrag.move;
        document.onmouseup = rDrag.end;
    },
    move: function (e) {   
        $("#scrollContent").getNiceScroll().resize();
        e = rDrag.fixEvent(e);
        var oLeft, oTop;
        oLeft = e.clientX - rDrag.o.x;
        oTop = e.clientY - rDrag.o.y;
        if (oTop < -5) {
            oTop = -5;
        }
        if (oTop > document.body.clientHeight - 36) {
            oTop = document.body.clientHeight - 36;
        }
        rDrag.o.parentElement.style.left = oLeft + 'px';
        rDrag.o.parentElement.style.top = oTop + 'px';

    },
    end: function (e) {
        e = rDrag.fixEvent(e);
        rDrag.o.parentElement = document.onmousemove = document.onmouseup = null;
    },
    fixEvent: function (e) {
        if (!e) {
            e = window.event;
            e.target = e.srcElement;
            e.layerX = e.offsetX;
            e.layerY = e.offsetY;
        }
        return e;
    }
}
//window.onload = function () {
   
//    //$('.chat-thread li.other').each(function () {
//    //    $(this).before("background-image", "url('uploadFile.png')");
//    //});
//}
$(function () {
    var obj = document.getElementById('aa');
    rDrag.init(obj);
    rDrag.init($('.nav-list')[0]);
    Message.init();
})
var Message = {
    selfId: "",
    selfName: "",
    otherId: 140,
    isClosed: true,
    getMessageResult: null,
    messageList: null,
    countMessage: 0,
    circleGetMessage: null,
    init: function () {
        //setInterval(Message.loadUnreadMessCount, 5000);
        Message.selfId = globalConfig.UserID;
        Message.selfName = globalConfig.UserName;
        Message.minContent();
        Message.maxContent();
        Message.closeContent();
        Message.positionClick();
        Message.loadUnreadMessCount();
      
    },
    loadUnreadMessCount: function () {
        $.ajax({
            type: "POST",
            async: true,
            url: globalConfig.apiconfig + "/ServerPush.asyn",
            data: { Action: "GetMessageNoReadStat", ReceiverId: Message.selfId },
            dataType: "json",
            success: function (result) {
                try{
                    if (result.IsFinish == 1) {
                        return;
                    }
                }catch(ex){
                    return;
                }
                $(".messageList").html("");
                Message.countMessage = 0;
                Message.messageList = result;
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        if (i == result.length - 1) {
                            $(".messageList").append('<div class="messageSonList lastmessage"><span class="name">' + result[i].SendName + '</span><span class="num">' + result[i].Count + '</span></div>');
                        }
                        else {
                            $(".messageList").append('<div class="messageSonList"><span class="name">' + result[i].SendName + '</span><span class="num">' + result[i].Count + '</span></div>');
                        }
                        Message.countMessage += result[i].Count;

                    }
                    if (Message.countMessage > 0) {
                        Message.navListHover();
                        Message.messageSonListClick();
                    }
                    $("span:contains('消息') span").html(Message.countMessage);
                } else {
                    $("span:contains('消息') span").html(0);
                }
                Message.loadUnreadMessCount();
            },
            error: function (errorMsg) {
                console.log("获取未读消息汇总失败");
                Message.loadUnreadMessCount();
            }
        });
    },
    messageSonListClick: function () {
        $('.messageSonList').each(function (i) {
            $(this).click(function () {
                $('.messageList')[0].removeChild(this);
                $("span:contains('消息') span").html(Message.countMessage - Message.messageList[i].Count);
                Message.otherId = Message.messageList[i].SendId;
                $("#Draggable").css("display", "block");
                $("#name").html(Message.messageList[i].SendName);
                $.ajax({
                    type: "POST",
                    async: true,
                    url: globalConfig.apiconfig + "/api/Message/GetMessages",
                    data: { SendId: Message.otherId, ReceiverId: Message.selfId },
                    dataType: "json",
                    success: function (result) {
                        Message.getMessageResult == result;
                        $('.chat-thread').html("");
                        for (var i = result.length - 1; i >= 0; i--) {
                            var Content = result[i].Content;
                            if (result[i].SendId == Message.selfId) {
                                if (Content == null || Content.length == 0) {
                                    $('.chat-thread').append('<li class="self">' + "&nbsp" + '</li>');
                                }
                                else {
                                    $('.chat-thread').append(Message.ConvertToHtml(Content, 1, result[i].SendName, result[i].sSendTime));
                                }
                            }
                            else {
                                if (Content == null || Content.length == 0) {
                                    $('.chat-thread').append('<li class="other">' + "&nbsp" + '</li>');
                                }
                                else {
                                    $('.chat-thread').append(Message.ConvertToHtml(Content, 0, result[i].SendName, result[i].sSendTime));
                                }
                            }
                        }
                        Message.isClosed = false;
                        Message.circleGetNoreadMessage(Message.otherId, Message.selfId);
                        $('.chat-thread').scrollTop($('.chat-thread')[0].scrollTopMax);
                    },
                    error: function (errorMsg) {
                        console.log(errorMsg);
                    }
                });
            })
        })
    },
    ConvertToHtml: function (Content, IsSelf, SendName, SendTime) {
        var html = "";
        SendTime = this.formatTime(SendTime);
        if (IsSelf == 1) {
            html = '<li class="self"><p>' + SendName + '<span class="mar">' + SendTime + '</span></p><div>';
        }
        else {
            html = '<li class="other"><p>' + SendName + '<span class="mar">' + SendTime + '</span></p><div>';
        }
        for (var i = 0; i < Content.length; i++) {
            switch (Content[i].Type) {
                case 0: html += '<p>' + Content[i].Content + '</p>'; break;
                case 3: var imgSmallPath = globalConfig.messImgSmallPath + Content[i].Content;
                    var imgOriginalPath = globalConfig.messImgOriginalPath + Content[i].Content;
                    html += '<img id="img' + (Math.random() * 10000) + '" onclick="zoom(this, \'' + imgOriginalPath.toString().replace(/\\/g, '/') + '\')" src="' + imgSmallPath.toString().replace(/\\/g, '/') + '" onmouseover="showMenu({\'ctrlid\':this.id,\'pos\':\'12\'})" border="0" style="width:50px;height:50px">'; break;
                    //html += "<img  id='img" + i + "' onmouseover='showMenu({'ctrlid':this.id,'pos':'12'})' border='0' onclick='zoom(this, 'images/2.jpg')' src='http://218.108.93.246:15212/GetZFSJPicByPath.ashx?PicPath=" + Content[i].Content + "' />";
                case 5:
                    if (IsSelf == 1) {
                        html = '<li class="self"  style="cursor: pointer" onclick=Position("' + (Content[i].Content.split('$').length > 1 ? Content[i].Content.split('$')[1] : Content[i].Content.split('$')[0]) + '")><p>' + SendName + '<span  class="mar">' + SendTime + '</span></p>';
                    }
                    else {
                        html = '<li class="other"  style="cursor: pointer" onclick=Position("' + (Content[i].Content.split('$').length > 1 ? Content[i].Content.split('$')[1] : Content[i].Content.split('$')[0]) + '")><p>' + SendName + '<span  class="mar">' + SendTime + '</span></p>';
                    }
                    //html = "<li class='self' style='color:#b6f50c'>";
                    //html += "坐标：[" + Content[i].Content+"]" ; break;
                    html += '<div><img id="img" style="width:50px;height:50px" src="/images/Communicate/地图.png"';
            }
        }
        html += '</div></li>';
        return html;
    },
    navListHover: function () {
        var flag = false;
        $('.nav-list').hover(function () {
            flag = false;
            if (Message.countMessage > 0) {
                $('.messageList').css("display", "block");
            }
            $('.messageList').hover(function () {
                flag = true;
                if (Message.countMessage > 0) {
                    $('.messageList').css("display", "block");
                }
            }, function () {
                flag = false;
                $('.messageList').css("display", "none");
            });
        }, function () {
            if (!flag) {
                $('.messageList').css("display", "none");
            }
        });
    },
    formatTime: function (time) {
        return time.replace('T', " ")
    },
    circleGetNoreadMessage: function (SendId, ReceiverId) {
        if (SendId == null || SendId == 0 || ReceiverId == null || ReceiverId == 0) {
            return;
        }
        $.ajax({
            type: "POST",
            async: true,
            //url: globalConfig.apiconfig + "/api/Message/GetMessageNoRead",
            url: globalConfig.apiconfig + "/ServerPush.asyn",
            data: { Action: "GetMessageNoRead", SendId: SendId, ReceiverId: ReceiverId },
            dataType: "json",
            success: function (result) {
                if (result.IsFinish == 1) {
                    return;
                }
                for (var i = result.length - 1; i >= 0; i--) {
                    var Content = result[i].Content;
                    if (Content == null || Content.length == 0) {
                        $('.chat-thread').append('<li class="other">' + "" + '</li>');
                    }
                    else {
                        $('.chat-thread').append(Message.ConvertToHtml(Content, 0, result[i].SendName, result[i].sSendTime));
                    }
                }
                $('.chat-thread').scrollTop($('.chat-thread')[0].scrollTopMax);
                Message.circleGetNoreadMessage(SendId, ReceiverId);
                //if (!Message.isClosed) {
                //    setTimeout(Message.circleGetNoreadMessage(SendId, ReceiverId), 5000);
                //}
            },
            error: function (errorMsg) {
                Message.circleGetNoreadMessage(SendId, ReceiverId);
                console.log(errorMsg);
                //if (!Message.isClosed) {
                //    setTimeout(Message.circleGetNoreadMessage(SendId, ReceiverId), 5000);
                //}
            }
        })
    },
    minContent: function () {
        $('.minImg').click(function () {
            //$(this).removeClass('minImg').addClass('maxImg');
            //$(".displayDiv").each(function (i) {
            //    if (i != 0) {
            //        $(this).removeClass('hideC').addClass('showC');
            //    }
            //})
            $(this).css('display', 'none');
            $('.maxImg').css('display', 'block')
            $('.displayDiv').css('display', 'none')
            $("#Draggable").animate({ height: '35px' });

        })
    },
    maxContent: function () {
        $('.maxImg').click(function () {
            //$(this).removeClass('maxImg').addClass('minImg');
            //$(".displayDiv").each(function (i) {
            //    if (i != 0) {
            //        $(this).removeClass('showC').addClass('hideC');
            //    }
            //})
            $(this).css('display', 'none');
            $('.minImg').css('display', 'block')
            $("#Draggable").animate({ height: '525px' });
            $('.displayDiv').css('display', 'block');
        })
    },
    closeContent: function () {
        $('.closeImg').click(function () {
            closeClick();
        })
    },
    showContent: function (User) {
        $('#scrollContent').niceScroll({
            cursorcolor: "#12dffc",//#CC0071 光标颜色
            cursoropacitymax: 1, //改变不透明度非常光标处于活动状态（scrollabar“可见”状态），范围从1到0
            touchbehavior: false, //使光标拖动滚动像在台式电脑触摸设备
            cursorwidth: "5px", //像素光标的宽度
            cursorborder: "0", // 	游标边框css定义
            cursorborderradius: "5px",//以像素为光标边界半径
            autohidemode: false //是否隐藏滚动条
        });
        Message.isClosed = false;

        $('.chat-thread').html("");
        $("#preview").html("");
        $("#Draggable").css("display", "block");
        $("#name").html(User.UserName);
        $("#level").html(User.UserPositionName);
        Message.otherId = User.UserId;
        if (Message.otherId == null || Message.otherId == 0 || Message.selfId == null || Message.selfId == 0) {
            return;
        }
        $.ajax({
            type: "POST",
            async: true,
            url: globalConfig.apiconfig + "/api/Message/GetMessages",
            data: { SendId: Message.otherId, ReceiverId: Message.selfId },
            dataType: "json",
            success: function (result) {
                Message.getMessageResult == result;
                $('.chat-thread').html("");
                for (var i = result.length - 1; i >= 0; i--) {
                    var Content = result[i].Content;
                    if (result[i].SendId == Message.selfId) {
                        if (Content == null || Content.length == 0) {
                            $('.chat-thread').append('<li class="self">' + "&nbsp" + '</li>');
                        }
                        else {
                            $('.chat-thread').append(Message.ConvertToHtml(Content, 1, result[i].SendName, result[i].sSendTime));
                        }
                    }
                    else {
                        if (Content == null || Content.length == 0) {
                            $('.chat-thread').append('<li class="other">' + "&nbsp" + '</li>');
                        }
                        else {
                            $('.chat-thread').append(Message.ConvertToHtml(Content, 0, result[i].SendName, result[i].sSendTime));
                        }
                    }
                }
                Message.isClosed = false;
                Message.circleGetNoreadMessage(Message.otherId, Message.selfId);
                $('.chat-thread').scrollTop($('.chat-thread')[0].scrollTopMax);
            },
            error: function (errorMsg) {
                console.log(errorMsg);
            }
        });
    },
    positionClick: function () {
        $('.position').click(function () {
            if (globalConfig.mapFlag == 3) {
                map.mapChange();
            }
            $('#Draggable').css("display", "none");
            main.slContent.Content.MainPage.GetPosition();
        });
    },
}

function SendPosition(position) {
    if (globalConfig.mapFlag == 3) {
        map.mapChange();
    }
    var positionInfo = eval("(" + position + ")");
    messageObj.SendID = Message.selfId;
    messageObj.ReceiverId = Message.otherId;
    var Content = [];
    Content.push({ Sort: 0, Type: 5, Content: positionInfo.x + "," + positionInfo.y });
    messageObj.Content = Content;
    $.ajax({
        type: "POST",
        async: true,
        url: globalConfig.apiconfig + "/api/Message/AddMessage",
        data: { SendID: messageObj.SendID, ReceiverId: messageObj.ReceiverId, Content: messageObj.Content },
        dataType: "json",
        success: function (result) {
            $('#Draggable').css("display", "block");
            var now = new Date();
            var year = now.getYear();
            var month = now.getMonth();
            var day = now.getDate();
            var hours = now.getHours() < 10 ? "0" + now.getHours() : now.getHours();
            var minutes = now.getMinutes() < 10 ? "0" + now.getMinutes() : now.getMinutes();
            var seconds = now.getSeconds() < 10 ? "0" + now.getSeconds() : now.getSeconds();
            if (month + 1 < 10) {
                month = "0" + (month + 1);
            } else {
                month = month + 1;
            }
            if (day < 10) {
                day = "0" + day;
            }
            var Time = (1900 + year) + "-" + month + "-" + day + "  " + hours + ":" + minutes + ":" + seconds;
            $('.chat-thread').append("<li class='self' style='color:#fff;cursor: pointer' onclick=Position('" + positionInfo.x + "," + positionInfo.y + "')>" + '<p>' + Message.selfName + '<span class="mar">' + Time + '</span></p><div>' + '<img id="img" style="width:50px;height:50px" src="/images/Communicate/地图.png"/>' + "</div></li>");
            $('.chat-thread').scrollTop($('.chat-thread')[0].scrollTopMax);
        },
        error: function (errorMsg) {
            console.log(errorMsg);
        }
    });
}

function Position(positionInfo) {
    main.slContent.Content.MainPage.Position(positionInfo);
}

var imgFiles = [];
function preview(file) {
    var prevDiv = document.getElementById('preview');

    imgFiles.push(file.files);
    if (file.files && file.files[0]) {
        var reader = new FileReader();
        reader.onload = function (evt) {
            $(prevDiv).append('<img id="img' + (Math.round(Math.random() * 100) + 100) + '" onclick="zoom(this, \'' + evt.target.result + '\')" src="' + evt.target.result + '" onmouseover="showMenu({\'ctrlid\':this.id,\'pos\':\'12\'})" border="0"  style="width:50px;height:50px"/>');
            //$(prevDiv).append('<img src="' + evt.target.result + '"  style="width:50px;height:50px"/>');
        }
        reader.readAsDataURL(file.files[0]);
    }
    if ($('#preview').html().replace(' ', '') == '') {
        return;
    }
}
var messageObj = {
    Content: null,
    SendID: 0,
    ReceiverId: 0
}
function closeClick() {
    Message.isClosed = true;
    //messageObj = null;
    $('.chat-thread').html("");
    $("#preview").html("");

    //

    Message.circleGetMessage = clearInterval(Message.circleGetMessage);
    //清除定位图表
    main.slContent.Content.MainPage.ClearPosition();
    $("#Draggable").css("display", "none");
}
function sentClick() {
    var prevDiv = document.getElementById('preview');
    var j = 0;
    messageObj.SendID = Message.selfId;
    messageObj.ReceiverId = Message.otherId;
    var Content = [];
    for (var i = 0; i < prevDiv.childNodes.length; i++) {
        if (prevDiv.childNodes[i].nodeName == "IMG") {
            //imgFiles[j]
            Content.push({ Sort: i, Type: 3, Content: prevDiv.childNodes[i].src });
            j++;
        } else if (prevDiv.childNodes[i].nodeName == "#text") {
            Content.push({ Sort: i, Type: 0, Content: prevDiv.childNodes[i].textContent });
        }
    }
    messageObj.Content = Content;

    $.ajax({
        type: "POST",
        async: true,
        url: globalConfig.apiconfig + "/api/Message/AddMessage",
        data: { SendID: messageObj.SendID, ReceiverId: messageObj.ReceiverId, Content: messageObj.Content },
        dataType: "json",
        success: function (result) {
            var now = new Date();
            var year = now.getYear();
            var month = now.getMonth();
            var day = now.getDate();
            var hours = now.getHours() < 10 ? "0" + now.getHours() : now.getHours();
            var minutes = now.getMinutes() < 10 ? "0" + now.getMinutes() : now.getMinutes();
            var seconds = now.getSeconds() < 10 ? "0" + now.getSeconds() : now.getSeconds();
            if (month + 1 < 10) {
                month = "0" + (month + 1);
            } else {
                month = month + 1;
            }
            if (day < 10) {
                day = "0" + day;
            }
            var Time = (1900 + year) + "-" + month + "-" + day + "  " + hours + ":" + minutes + ":" + seconds;
            $('.chat-thread').append("<li class='self'>" + '<p>' + Message.selfName + '<span class="mar">' + Time + '</span></p><div>' + prevDiv.innerHTML + "</div></li>");
            $(prevDiv).html("");
            $('.chat-thread').scrollTop($('.chat-thread')[0].scrollTopMax);
        },
        error: function (errorMsg) {
            console.log(errorMsg);
        }
    });
}
