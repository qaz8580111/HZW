using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    /// <summary>
    /// 登录错误
    /// </summary>
    [Serializable]
    public class SignInError
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 返回的错误数据
        /// </summary>
        public object ErrorData { get; set; }
    }
}
