using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class rytable
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int useId { get; set; }

        /// <summary>
        /// 人员名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 人员状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string departName { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string usercode { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        /// 人员照片路径
        /// </summary>
        public string  photourl { get; set; }

        /// <summary>
        /// X坐标
        /// </summary>
        public double? coordinatex { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public double? coordinatey { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updatetime { get; set; }

        /// <summary>
        /// 插入日期
        /// </summary>
        public DateTime? datainsertdate { get; set; }
    }
}
