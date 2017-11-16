var g_iWndIndex = 0; //���Բ�����������������д��ڲ����Ľӿ��У����ô�ֵ����������Ĭ��ʹ�õ�ǰѡ�񴰿�
$(function () {
    // ������Ƿ��Ѿ���װ��
    var iRet = WebVideoCtrl.I_CheckPluginInstall();
    if (-2 == iRet) {
        alert("����Chrome������汾���ߣ���֧��NPAPI�����");
        return;
    } else if (-1 == iRet) {
        document.getElementById("VideoVersion").style.display = "block";
        document.getElementById("divPlugin").style.display = "none";
        return;
    }

    // ��ʼ�����������������
    WebVideoCtrl.I_InitPlugin(500, 300, {
        bWndFull: true,//�Ƿ�֧�ֵ�����˫��ȫ����Ĭ��֧�� true:֧�� false:��֧��
        iWndowType: 2,
        cbSelWnd: function (xmlDoc) {
            g_iWndIndex = $(xmlDoc).find("SelectWnd").eq(0).text();
            var szInfo = "��ǰѡ��Ĵ��ڱ�ţ�" + g_iWndIndex;
            //showCBInfo(szInfo);
        }
    });
    WebVideoCtrl.I_InsertOBJECTPlugin("divPlugin");

    // ������Ƿ�����
    if (-1 == WebVideoCtrl.I_CheckPluginVersion()) {
        alert("��⵽�µĲ���汾��˫��������Ŀ¼���WebComponentsKit.exe������");
        return;
    }

    // �����¼���
    $(window).bind({
        resize: function () {
            var $Restart = $("#restartDiv");
            if ($Restart.length > 0) {
                var oSize = getWindowSize();
                $Restart.css({
                    width: oSize.width + "px",
                    height: oSize.height + "px"
                });
            }
        }
    });
    WebVideoCtrl.I_ChangeWndNum(1);
    clickLogin();
    //clickStartRealPlay("60.190.179.50");
});

// ��¼
function clickLogin() {
    var szIP = "'192.168.20.10",
		szPort = "554",
		szUsername = "admin",
		szPassword = "admin123";

    if ("" == szIP || "" == szPort) {
        return;
    }

    var iRet = WebVideoCtrl.I_Login(szIP, 1, szPort, szUsername, szPassword, {
        success: function (xmlDoc) {
            //alert("mession seccess");
            ////showOPInfo(szIP + " ��¼�ɹ���");

            ////$("#ip").prepend("<option value='" + szIP + "'>" + szIP + "</option>");
            //setTimeout(function () {
            //    //$("#ip").val(szIP);
            //    //getChannelInfo(szIP);
            //}, 10);
            clickStartRealPlay(szIP);
        },
        error: function () {
            alert(szIP + " mession failed");
        }
    });

    if (-1 == iRet) {
        alert(szIP + " �ѵ�¼����");
    }
}

// ��ȡͨ��
function getChannelInfo(szIP) {
    //var szIP = $("#ip").val(),
	//	oSel = $("#channels").empty();

    //if ("" == szIP) {
    //    return;
    //}

    // ģ��ͨ��
    WebVideoCtrl.I_GetAnalogChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("VideoInputChannel");

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text();
                if ("" == name) {
                    name = "Camera " + (i < 9 ? "0" + (i + 1) : (i + 1));
                }
                oSel.append("<option value='" + id + "' bZero='false'>" + name + "</option>");
            });
            alert(szIP + " ��ȡģ��ͨ���ɹ���");
        },
        error: function () {
            alert(szIP + " ��ȡģ��ͨ��ʧ�ܣ�");
        }
    });
    // ����ͨ��
    WebVideoCtrl.I_GetDigitalChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("InputProxyChannelStatus");

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text(),
					online = $(this).find("online").eq(0).text();
                if ("false" == online) {// ���˽��õ�����ͨ��
                    return true;
                }
                if ("" == name) {
                    name = "IPCamera " + (i < 9 ? "0" + (i + 1) : (i + 1));
                }
                oSel.append("<option value='" + id + "' bZero='false'>" + name + "</option>");
            });
            alert(szIP + " ��ȡ����ͨ���ɹ���");
        },
        error: function () {
            alert(szIP + " ��ȡ����ͨ��ʧ�ܣ�");
        }
    });
    // ��ͨ��
    WebVideoCtrl.I_GetZeroChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("ZeroVideoChannel");

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text();
                if ("" == name) {
                    name = "Zero Channel " + (i < 9 ? "0" + (i + 1) : (i + 1));
                }
                if ("true" == $(this).find("enabled").eq(0).text()) {// ���˽��õ���ͨ��
                    oSel.append("<option value='" + id + "' bZero='true'>" + name + "</option>");
                }
            });
            alert(szIP + " ��ȡ��ͨ���ɹ���");
        },
        error: function () {
            alert(szIP + " ��ȡ��ͨ��ʧ�ܣ�");
        }
    });
}

function clickStartRealPlay(szIP) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex),
		szIP = szIP,
		iStreamType = 1,//parseInt($("#streamtype").val(), 10),
		iChannelID =1 ,//parseInt($("#channels").val(), 10),
		bZeroChannel =  false,
		szInfo = "";

    if ("" == szIP) {
        return;
    }

    //if (oWndInfo != null) {// �Ѿ��ڲ����ˣ���ֹͣ
    //    WebVideoCtrl.I_Stop();
    //}

    var iRet = WebVideoCtrl.I_StartRealPlay(szIP, {
        iStreamType: iStreamType,
        iChannelID: iChannelID,
        bZeroChannel: bZeroChannel
    });

    if (0 == iRet) {
        szInfo = "review seccess";
    } else {
        szInfo = "review failed";
    }

    //alert(szIP + " " + szInfo);
}