using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.Model.CoordinationManager
{
    /// <summary>
    /// 执法事件实体
    /// </summary>
    public class EnforceLawEventModel
    {
        /// <summary>
        /// 执法事件编号(ID)
        /// </summary>
        public string zfsjID { get; set; }

        /// <summary>
        /// 事件标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 事件来源编号
        /// </summary>
        public int sourceID { get; set; }

        /// <summary>
        /// 相关联系人
        /// </summary>
        public string contact { get; set; }

        /// <summary>
        /// 相关联系人电话
        /// </summary>
        public string contactphone { get; set; }

        /// <summary>
        /// 事件地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 事件内容
        /// </summary>
        public string content { get; set; }      

        /// <summary>
        /// 问题大类标识
        /// </summary>
        public int mainClassID { get; set; }

        /// <summary>
        /// 问题小类标识
        /// </summary>
        public int sunClassID { get; set; }

        /// <summary>
        /// 发现时间
        /// </summary>
        public string discoverTime { get; set; }

        /// <summary>
        /// 事件超期事件
        /// </summary>
        public string overdueTime { get; set; }

        /// <summary>
        /// 紧急级别
        /// </summary>
        public int levelNum { get; set; }

        /// <summary>
        /// 地图位置（经度|纬度）
        /// </summary>
        public string mapLocation { get; set; }

        /// <summary>
        /// 上报时间
        /// </summary>
        public string reportTime { get; set; }

        /// <summary>
        /// 上报人员
        /// </summary>
        public int userID { get; set; }

        /// <summary>
        /// 手机IMEI号
        /// </summary>
        public string imeiCode { get; set; }

        /// <summary>
        /// 领导交办事件
        /// </summary>
        public string assignTime { get; set; }

        /// <summary>
        /// 事件照片一
        /// </summary>
        public byte[] eventPhoto1 { get; set; }

        /// <summary>
        /// 事件照片二
        /// </summary>
        public byte[] eventPhoto2 { get; set; }

        /// <summary>
        /// 事件照片三
        /// </summary>
        public byte[] eventPhoto3 { get; set; }

        /// <summary>
        /// 事件照片四
        /// </summary>
        public byte[] eventPhoto4 { get; set; }

        /// <summary>
        /// 事件照片五
        /// </summary>
        public byte[] eventPhoto5 { get; set; }

        /// <summary>
        /// 事件照片六
        /// </summary>
        public byte[] eventPhoto6 { get; set; }



    }
}