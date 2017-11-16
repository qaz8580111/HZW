
; (function ($) {
    $(document).ready(function () {

        $("form").submit(function () {
            var flag = true;
            //验证账号密码

            //获取用户信息框
            var $account = $("input[name=account]");
            var $password = $("input[name=password]");

            var userData = {
                account: $account[0].value,
                password: $password[0].value
            };
            //清空验证信息
            $("span").html("");
            if (userData.account == "") {
                flag = false;
                $("#account span").html("");
                var msg = $("<span style='font-family:宋体!important;color:#b94a48'  generated='true'>*账号不能为空</span>");
                $("#account").append(msg);
            }
            if (userData.password == "") {
                flag = false;
                $("#password span").html("");
                var msg = $("<span style='font-family:宋体!important;color:#b94a48'  generated='true'>*密码不能为空</span>");
                $("#password").append(msg);
            }
            if (userData.password != "" && userData.account != "") {
                $("span").html("");
                $.ajax({
                    type: "POST",
                    url: ('/Login/CheckExist'),
                    async: false,
                    data: userData,
                    error: function (result) {
                        alert("登陆异常，请联系管理员！");
                        flag = false;
                    },
                    success: function (result) {
                        if (result == "False") {
                            flag = false;
                            alert("用户不存在或账号或密码错误！");
                        }
                    }
                });

            }
            return flag;
        });

    });
})(jQuery);