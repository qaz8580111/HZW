using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.Model.PhoneModel
{
    /// <summary>
    /// 签到列表模型
    /// </summary>
    /// <returns></returns>
    public class UserSignInModel
    {
        /// <summary>
        /// 签到用户标识
        /// </summary>
        public decimal USERID { get; set; }

        /// <summary>
        /// 签到任务标识a
        /// </summary>
        public decimal SGID { get; set; }
        /// <summary>
        /// 签到用户名字
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// 用户所属单位标识
        /// </summary>
        public decimal UNITID { get; set; }

        /// <summary>
        /// 用户所属单位名字
        /// </summary>
        public string UNITNAME { get; set; }

        /// <summary>
        /// 签到用户头像
        /// </summary>
        public string USERAVATAR { get; set; }

        /// <summary>
        /// 签到全格式
        /// </summary>
        public DateTime? SIGNINALL { get; set; }

        /// <summary>
        /// 签到日期
        /// </summary>
        public string SIGNINDATE { get; set; }

        /// <summary>
        /// 计划签到时间
        /// </summary>
        public DateTime? PLANSIGNINTIME { get; set; }

        /// <summary>
        /// 计划签到时间
        /// </summary>
        public string PLANSIGNINTIMESTR { get; set; }

        /// <summary>
        /// 计划签退时间
        /// </summary>
        public DateTime? PLANSIGNOUTTIME { get; set; }

        /// <summary>
        /// 计划签退时间
        /// </summary>
        public string PLANSIGNOUTTIMESTR { get; set; }

        /// <summary>
        /// 实际签到时间
        /// </summary>
        public DateTime? ACSIGNINTIME { get; set; }

        /// <summary>
        /// 实际签到时间
        /// </summary>
        public string ACSIGNINTIMESTR { get; set; }

        /// <summary>
        /// 实际签退时间
        /// </summary>
        public DateTime? ACSIGNOUTTIME { get; set; }

        /// <summary>
        /// 实际签退时间
        /// </summary>
        public string ACSIGNOUTTIMESTR { get; set; }

        /// <summary>
        /// 签到时间
        /// </summary>
        public string SIGNINTIME { get; set; }

        /// <summary>
        /// 签到星期
        /// </summary>
        public string SIGNINWEEK { get; set; }

        /// <summary>
        /// 签到坐标
        /// </summary>
        public string SIGNINLOGLAT { get; set; }

        /// <summary>
        /// 签到结果
        /// </summary>
        public string SIGNINRESULT { get; set; }

        /// <summary>
        /// 签到区域标识
        /// </summary>
        public decimal AREAID { get; set; }

        /// <summary>
        /// 签到区域名字
        /// </summary>
        public string AREANAME { get; set; }

        /// <summary>
        /// 签到区域描述
        /// </summary>
        public string AREADESCRIPTION { get; set; }

        /// <summary>
        /// 签到区域经纬度
        /// </summary>
        public string GEOMETRY { get; set; }

        /// <summary>
        /// 是否在签到区域
        /// </summary>
        public bool ISINAREA { get; set; }

        /// <summary>
        /// 今日签到次数
        /// </summary>
        public int SIGNINCOUNT { get; set; }

        /// <summary>
        /// 被查询名字
        /// </summary>
        public string QueryName { get; set; }

    }

    /// <summary>
    /// 手机用户签到传值
    /// </summary>
    /// <returns></returns>
    public class UserSignInPostModel
    {
        /// <summary>
        /// 用户标识
        /// </summary>
        public decimal UserId { get; set; }

        /// <summary>
        /// 签到任务标识
        /// </summary>
        public decimal SGID { get; set; }

        /// <summary>
        /// 签到区域标识
        /// </summary>
        public decimal AreaId { get; set; }

        /// <summary>
        /// 部门标识
        /// </summary>
        public decimal UnitId { get; set; }

        /// <summary>
        /// 职务标识
        /// </summary>
        public decimal PositionId { get; set; }

        /// <summary>
        /// 地理经度
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 地理纬度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 用户名搜索
        /// </summary>
        public string QueryUserName { get; set; }
    }

    /// <summary>
    /// 签到区域部分值模型
    /// </summary>
    /// <returns></returns>
    public class SignInAreaPartModel
    {
        /// <summary>
        /// 签到任务标识
        /// </summary>
        public decimal SGID { get; set; }

        /// <summary>
        /// 签到区域标识
        /// </summary>
        public decimal AREAID { get; set; }

        /// <summary>
        /// 经纬度
        /// </summary>
        public string GEOMETRY { get; set; }

        /// <summary>
        /// 签到区域名称
        /// </summary>
        public string AREANAME { get; set; }

        /// <summary>
        /// 签到区域描述
        /// </summary>
        public string AREADESCRIPTION { get; set; }

        /// <summary>
        /// 是否签到成功
        /// </summary>
        public bool ISSIGNIN { get; set; }

    }

    /// <summary>
    /// 去重复
    /// </summary>
    /// <returns></returns>
    public class UserSignInNoComparer : IEqualityComparer<UserSignInModel>
    {
        public bool Equals(UserSignInModel us1, UserSignInModel us2)
        {
            if (us1 == null)
                return us2 == null;
            return us1.USERNAME == us2.USERNAME;
        }

        public int GetHashCode(UserSignInModel us)
        {
            if (us == null)
                return 0;
            return us.USERNAME.GetHashCode();
        }
    }

}
