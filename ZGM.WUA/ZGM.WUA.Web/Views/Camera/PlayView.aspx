<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayView.aspx.cs" Inherits="ZGM.WUA.Web.Views.Camera.PlayView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function init() {
            var OCXobj = document.getElementById("PlayViewOCX");
            OCXobj.SetOcxMode(0);
            OCXobj.SetWndNum(1);

            var cameras = document.getElementById("cameras");
            cameras.onchange = function () {
                var camera = cameras.options[cameras.selectedIndex];
                var previewXml = camera.value;
                //alert(previewXml);
                if (previewXml == "")
                    return;
                OCXobj.StartTask_Preview_FreeWnd(previewXml);
                //OCXobj.StartTask_Preview_InWnd(previewXml, 1);
            };
        }
        //function StartPlayView_Free() {
        //    var OCXobj = document.getElementById("PlayViewOCX");
        //    //var previewXml = document.getElementById("config").value;
        //    var previewXml = "<?xml version='1.0' encoding='UTF-8'?> <Message> <Camera> <Id>1</Id> <IndexCode>33000000001310015850</IndexCode> <Name>西区泵站</Name> <ChanNo>0</ChanNo> <Matrix Code='' Id='0' /> </Camera> <Dev regtype='0' devtype='10070'> <Id>1</Id> <IndexCode>33000000001310927674</IndexCode> <Addr IP='172.16.7.101' Port='8000' /> <Auth User='admin' Pwd='12345' /> </Dev> <Vag IP='172.16.2.36' Port='7302' /> <Voice> <Encode>2</Encode> </Voice> <Media Protocol='0' Stream='1'> <Vtdu IP='172.16.2.37' Port='554' /> </Media> <Privilege Priority='50' Code='7' /> <Option> <Talk>1</Talk> <PreviewType>0</PreviewType> </Option> </Message>";
        //    OCXobj.StartTask_Preview_FreeWnd(previewXml);
        //}
    </script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div>
            <select id="cameras">
                <option value="">请选择</option>
                <option value=""></option>
                <option value="<?xml version='1.0' encoding='UTF-8'?><Message>  <Camera>    <Id>368</Id>    <IndexCode>33021203001310010996</IndexCode>    <Name>钟公庙路施家塘路3</Name>    <ChanNo>0</ChanNo>    <Matrix Code='0' Id='0' />  </Camera>  <Dev regtype='0' devtype='10016'>    <Id>368</Id>    <IndexCode>33021203001310902993</IndexCode>    <Addr IP='172.172.50.116' Port='8000' />    <Auth User='admin' Pwd='a123456789' />  </Dev>  <Vag IP='172.172.100.11' Port='7302' />  <Voice>    <Encode>2</Encode>  </Voice>  <Media Protocol='0' Stream='0'>    <Vtdu IP='172.172.100.10' Port='554' />    <NetZoneId id='100001' />  </Media>  <Privilege Priority='50' Code='31' />  <Option>    <Talk>1</Talk>    <PreviewType>1</PreviewType>  </Option></Message>">Slot 01 Camera 01</option>
                <option value="<?xml version='1.0' encoding='UTF-8'?><Message>  <Window index='0' />  <Camera>    <Id>422</Id>    <IndexCode>33021203001310016724</IndexCode>    <Name>钟公庙文化公园门口</Name>    <DevType>0</DevType>    <DetailDevType>10016</DetailDevType>    <RecLocation>2</RecLocation>  </Camera>  <Query>    <ZoneId>100001</ZoneId>    <Vrm ip='172.172.100.11' port='6300' />  </Query>  <Intelligence>    <NCG ip='172.172.100.11' port='7099' />    <IvsSvr ip='127.0.0.1' port='6060' />    <Kms ip='127.0.0.1' port='8080' />    <Imp ip='172.172.100.10' port='80' />  </Intelligence>  <DSNVR_Info>    <Addr />    <Port />    <UserName />    <PassWord />    <indexcode />  </DSNVR_Info>  <VAG_Info>    <Addr />    <Port />    <UserName />    <PassWord />  </VAG_Info>  <Privilege>5</Privilege>  <StorageLocation>1</StorageLocation></Message>">回放</option>
            </select>
        </div>
        <div style="width: 750px; height: 450px;">
            <object classid="clsid:04DE0049-8359-486e-9BE7-1144BA270F6A" id="PlayViewOCX" width="750" height="450" name="ocx" />
            <%--<object name="RealTimePlayOcx" width="750" height="450" id="PlayViewOCX" classid="clsid:D5E14042-7BF6-4E24-8B01-2F453E8154D7" />--%>
        </div>
    </form>
</body>
</html>
