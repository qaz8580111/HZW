using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class ZHCGService
    {
        /// <summary>
        /// 智慧城管回馈接口
        /// </summary>
        /// <param name="disposal">json</param>
        /// <param name="medias">附件</param>
        /// <param name="user">用户</param>
        /// <returns>
        /// "1":授权申请成功
        /// "-1"：案件处置主体无效
        /// "-2"：申请类型错误
        /// "-3"：授权申请成功，案件状态修改失败，发生异常
        /// "-4,e.Message"：授权申请成功，案件状态修改失败，发生异常
        /// "-5"：授权申请失败，发生异常
        /// "-6,{ZHCGResult}"：授权申请失败
        /// </returns>
        public string TaskFeedBack(string disposal, string medias, string user)
        {
            ServiceSoap.ZHCGWebServiceSoapClient client = new ServiceSoap.ZHCGWebServiceSoapClient();
            return client.TaskFeedBack(disposal, medias, user);
        }
    }
}
