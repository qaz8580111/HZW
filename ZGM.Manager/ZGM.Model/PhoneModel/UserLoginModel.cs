/*类名：WF_WORKFLOWDETAILBLL
 *功能：详细流程的基本操作(查询)
 *创建时间:2016-04-05 15:16:07 
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-04-05 15:19:35
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    /// <summary>
    /// 手机端用户登录返回信息
    /// </summary>
    /// <returns></returns>
    public class UserLoginModel
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public decimal USERID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// 所属单位标识
        /// </summary>
        public decimal UNITID { get; set; }

        /// <summary>
        /// 所属单位名称
        /// </summary>
        public string UNITNAME { get; set; }

        /// <summary>
        /// 用户角色标识
        /// </summary>
        public string USERROLEID { get; set; }

        /// <summary>
        /// 用户角色名称
        /// </summary>
        public string USERROLENAME { get; set; }

        /// <summary>
        /// 用户职位标识
        /// </summary>
        public decimal? USERPOSITIONID { get; set; }

        /// <summary>
        /// 用户职位名称
        /// </summary>
        public string USERPOSITIONNAME { get; set; }

        /// <summary>
        /// 区域标识
        /// </summary>
        public decimal RegionID { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// 行政单位类型标识
        /// </summary>
        public decimal UnitTypeId { get; set; }

        /// <summary>
        /// 行政单位编号，二级组织编号
        /// </summary>
        public decimal UnitPID { get; set; }

        /// <summary>
        /// 行政单位名称，二级组织名称
        /// </summary>
        public string UnitPName { get; set; }

        /// <summary>
        /// 行政单位路径
        /// </summary>
        public string UnitPath { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string ACCOUNT { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string PASSWORD { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string PHONE { get; set; }

        /// <summary>
        /// 手机IMEI号
        /// </summary>
        public string PHONEIMEI { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string AVATAR { get; set; }

        /// <summary>
        /// 当前服务器版本号
        /// </summary>
        public string VISION { get; set; }

        /// <summary>
        /// 服务器版本下载URL
        /// </summary>
        public string VISIONURL { get; set; }

        /// <summary>
        /// 需要更新版本大小
        /// </summary>
        public decimal? VISIONSIZE { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LASTLOGINTIME { get; set; }

        /// <summary>
        /// 返回错误类型
        /// </summary>
        public decimal ERRORTYPE { get; set; }
    }

    /// <summary>
    /// 登入数据传入模型
    /// </summary>
    /// <returns></returns>
    public class UserLoginPostModel
    {
        /// <summary>
        /// 用户名字
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 手机时间
        /// </summary>
        public string PhoneTime { get; set; }

        /// <summary>
        /// IMEI
        /// </summary>
        public string IMEICode { get; set; }

        /// <summary>
        /// 用户版本
        /// </summary>
        public string Vision { get; set; }
    }

    /// <summary>
    /// 修改密码时用到的类
    /// </summary>
    public class UserPassWordEdit
    {
        /// <summary>
        /// 初始密码
        /// </summary>
        public string OLDPassword { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string NEWPassword { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public decimal userId { get; set; }
    }
}