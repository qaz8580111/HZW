using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class CFKYJDS
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 当事人
        /// </summary>
        public string DSR { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string DZ { get; set; }

        /// <summary>
        /// 违法行为
        /// </summary>
        public string WFXW { get; set; }

        /// <summary>
        /// 接受调查处理时间
        /// </summary>
        public DateTime? JSDCCLSJ { get; set; }

        /// <summary>
        /// 接受调查处理地点
        /// </summary>
        public string JSDCCLDD { get; set; }

        /// <summary>
        /// 物品清单列表
        /// </summary>
        public List<WPQDLB> WPQDLBS { get; set; }

        /// <summary>
        /// 扣押期限天数
        /// </summary>
        public string KYQXTS { get; set; }

        /// <summary>
        /// 查封扣押开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 查分扣押结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 查封(扣押)物品存放地点
        /// </summary>
        public string CFKYWPCFDD { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 执法人员1
        /// </summary>
        public string ZFRY1 { get; set; }

        /// <summary>
        /// 执法证号1
        /// </summary>
        public string ZFZH1 { get; set; }

        /// <summary>
        /// 执法人员2
        /// </summary>
        public string ZFRY2 { get; set; }

        /// <summary>
        /// 执法证号1
        /// </summary>
        public string ZFZH2 { get; set; }


        /// <summary>
        /// 查封扣押决定时间
        /// </summary>
        public DateTime? CFKYJDSJ { get; set; }
    }

    public class WPQDLB
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string XH { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string WPMC { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string GG { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public string SCRQ { get; set; }

        /// <summary>
        /// 生产单位
        /// </summary>
        public string SCDW { get; set; }

        /// <summary>
        /// 特征
        /// </summary>
        public string TZ { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
