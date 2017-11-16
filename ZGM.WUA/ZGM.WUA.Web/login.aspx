<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ZGM.WUA.Web.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>登入</title>
    <body>
        <form id="form1" runat="server">
            <div>
                用户名：<input name="account" id="account" /><br />
                密&nbsp;&nbsp;码：<input type="password" name="pwd" id="pwd" />
                <button type="button" onclick="login();">登录</button>
            </div>
        </form>
    </body>
    <script src="Scripts/base/jquery-1.8.2.min.js"></script>
    <script>
        function login() {
            var acc = $("#account").val();
            var pass = $("#pwd").val();
            var apiconfig = '<%=System.Web.Configuration.WebConfigurationManager.AppSettings["apiconfig"] %>';
            //sessionStorage.setItem("account", acc);
            //sessionStorage.setItem("password", pass);
            $.ajax({
                type: "GET",
                async: true,
                url: apiconfig + "/api/User/UserLogin2",
                data: { account: acc, password: pass },
                dataType: "json",
                success: function (result) {
                    var user = result;
                    document.cookie = "account=" + acc;
                    document.cookie = "password=" + pass;
                    document.cookie = "UserID=" + user.UserId;
                    document.cookie = "RoleName=" + user.RoleName;
                    window.location = "Index.aspx";
                }
            });
        };
    </script>
</head>
</html>
