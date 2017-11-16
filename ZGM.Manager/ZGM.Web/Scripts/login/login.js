
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
          
            if (userData.account == "") {
                flag = false;
                $("#account").addClass("blur");
            }
           
            if (userData.password == "") {
                flag = false;
                $("#password").addClass("blur");
            }
            
            if (userData.password != "" && userData.account != "") {
                $("#account").removeClass("blur");
                $("#password").removeClass("blur");
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