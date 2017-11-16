using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.ViewModels
{
    public class VMUserSignIn
    {
        public string ZFZBH { get; set; }
        public decimal UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? SignInDate { get; set; }
        public string SignInTime { get; set; }
        public string RealSignInTime { get; set; }
        public string Status { get; set; }
        public string TimeDeviation { get; set; }

        /// <summary>
        /// 计划签到开始时间
        /// </summary>
        public DateTime? SignSTime { get; set; }
        /// <summary>
        /// 计划签到结束时间
        /// </summary>
        public DateTime? SignETime { get; set; }

        /// <summary>
        /// 计划签到开始时间（时分秒）
        /// </summary>
        public string str_SignSTime { get; set; }
        /// <summary>
        /// 计划签到结束时间（时分秒）
        /// </summary>
        public string str_SignETime { get; set; }
        /// <summary>
        /// 实际第一次签到时间
        /// </summary>
        public DateTime? SigninSTime { get; set; }
        /// <summary>
        /// 实际第二次签到时间
        /// </summary>
        public DateTime? SigninETime { get; set; }

        /// <summary>
        /// 结果(第一次)
        /// </summary>
        public string ResultSSign { get; set; }
        /// <summary>
        /// 结果（第二次）
        /// </summary>
        public string ResultESign { get; set; }

        /// <summary>
        /// 分钟（迟到或者早退）第一次
        /// </summary>
        public int ResultSMinute { get; set; }
        /// <summary>
        /// 分钟（迟到或者早退）第二次
        /// </summary>
        public int ResultEMinute { get; set; }
    }

    public class MapPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
