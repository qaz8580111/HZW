using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.ParkingCaseModels
{
    public class ParkingCase
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public decimal XLH { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string carNo { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public string carType { get; set; }

        /// <summary>
        /// 违法时间
        /// </summary>
        public DateTime? caseTime { get; set; }

        /// <summary>
        /// 违法地点
        /// </summary>
        public string caseAddress { get; set; }

        /// <summary>
        /// 采集单位
        /// </summary>
        public string CJDW { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public string CLZT { get; set; }

        /// <summary>
        /// 违法行为名称
        /// </summary>
        public string WFXWName { get; set; }

        /// <summary>
        /// 采集人
        /// </summary>
        public string CJR { get; set; }

        /// <summary>
        /// 校对人
        /// </summary>
        public string JDR { get; set; }

        /// <summary>
        /// 校对日期
        /// </summary>
        public DateTime? JDRQ { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        public string CLR { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? CLSJ { get; set; }

        /// <summary>
        /// 处理单位
        /// </summary>
        public string CLDW { get; set; }

        /// <summary>
        /// 解锁人
        /// </summary>
        public string JSR { get; set; }

        /// <summary>
        /// 解锁时间
        /// </summary>
        public DateTime? JSSJ { get; set; }

        /// <summary>
        /// 当事人
        /// </summary
        public string DSR { get; set; }

        /// <summary>
        /// 当事人电话
        /// </summary>
        public string DSRDH { get; set; }

        /// <summary>
        /// 当事人地址
        /// </summary>
        public string DSRDZ { get; set; }

        /// <summary>
        /// 罚款金额
        /// </summary>
        public decimal? FKJE { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string FZSHR { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? FZSHSJ { get; set; }

        /// <summary>
        /// 审核作废原因
        /// </summary>
        public string FZSHYJ { get; set; }

        /// <summary>
        /// 校对结果
        /// </summary>
        public string JDJG { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public string CSBJ { get; set; }

        /// <summary>
        /// 交警编号
        /// </summary>
        public string WFXH { get; set; }

        /// <summary>
        /// 发票号码 
        /// </summary>
        public string FPHM { get; set; }
    }
}
