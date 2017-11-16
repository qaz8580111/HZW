using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Enums
{
    public class PhoneErrorEnum
    {
        public enum ErrorTypeEnum : int
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 用户名不存在或密码错误
            /// </summary>
            UserOrPassWord = 1,

            /// <summary>
            /// 手机时间格式不正确
            /// </summary>
            PhoneTimeType = 2,

            /// <summary>
            /// 版本号不正确
            /// </summary>
            Vision = 3
        }
    }
}
