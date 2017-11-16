using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    public class QTSXNBSPB
    {
        //审批文书标识
        public decimal? DDID { get; set; }

        /// <summary>
        /// 申请事项
        /// </summary>
        public string SQSX { get; set; }

        /// <summary>
        /// 文书编号
        /// </summary>
        public string WSBH { get; set; }

        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 立案日期
        /// </summary>
        public DateTime LARQ { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string ZY { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string SFZHM { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 法定代表人
        /// </summary>
        public string FDDBR { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string DH { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string ZZ { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string YZBM { get; set; }

        /// <summary>
        /// 简要案情及申请理由依据和内容
        /// </summary>
        public string JYAQ { get; set; }

        /// <summary>
        /// 承办人意见
        /// </summary>
        public string CBRYJ { get; set; }

        /// <summary>
        /// 承办人签章
        /// </summary>
        public string CBRQZ { get; set; }

        /// <summary>
        /// 承办人签章日期
        /// </summary>
        public string CBRQZRQ { get; set; }

        /// <summary>
        /// 承办机构审核意见
        /// </summary>
        public string CBJGSHYJ { get; set; }

        /// <summary>
        /// 承办机构审核签章
        /// </summary>
        public string CBJGSHQZ { get; set; }

        /// <summary>
        /// 承办机构审核签章日期
        /// </summary>
        public string CBJGSHQZRQ { get; set; }

        /// <summary>
        /// 行政机关负责人审批意见
        /// </summary>
        public string XZJGFZRSPYJ { get; set; }

        /// <summary>
        /// 行政机关负责人审批签章
        /// </summary>
        public string XZJGFZRSPQZ { get; set; }

        /// <summary>
        /// 行政机关负责人审批签章日期
        /// </summary>
        public string XZJGFZRSPQZRQ { get; set; }

        ///// <summary>
        ///// 查封扣押通知书
        ///// </summary>
        //public CFKYTZS CFKYTZS { get; set; }


        ///// <summary>
        ///// 解除查封扣押通知书
        ///// </summary>
        //public JCCFKYTZS JCCFKYTZS { get; set; }

        ///// <summary>
        ///// 先行登记保存证据通知书
        ///// </summary>
        //public XXDJBCZJTZS XXDJBCZJTZS { get; set; }

        ///// <summary>
        ///// 先行登记保存证据物品处理通知书
        ///// </summary>
        //public XXDJBCZJWPCLTZS XXDJBCZJWPCLTZS { get; set; }
    }
}
