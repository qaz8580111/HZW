using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CaseDocModels
{
    /// <summary>
    /// 案件处理审批表(只有备注可填写和修改)
    /// </summary>
    public class AJCLSPB
    {
        /// <summary>
        /// 案由
        /// </summary>
        public string AY { get; set; }

        /// <summary>
        /// 案件来源
        /// </summary>
        public string AJLY { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

        /// <summary>
        /// 组织机构代码证编号
        /// </summary>
        public string ZZJGDMZBH { get; set; }

        /// <summary>
        /// 法定代表人姓名
        /// </summary>
        public string FDDBRXM { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string ZW { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public string CSNY { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string MZ { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZH { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }

        /// <summary>
        /// 住所地
        /// </summary>
        public string ZSD { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string LXDH { get; set; }

        /// <summary>
        /// 认定的违法事实
        /// </summary>
        public string RDDWFSS { get; set; }

        /// <summary>
        /// 证据
        /// </summary>
        public string ZJ { get; set; }

        /// <summary>
        /// 违法的法律、法规和规章
        /// </summary>
        public string WFDFLFGHGZ { get; set; }

        /// <summary>
        /// 处罚依据
        /// </summary>
        public string CFYJ { get; set; }

        /// <summary>
        /// 调查终结后承办人意见(主办人)
        /// </summary>
        public string DCJBHCBRYJ { get; set; }

        /// <summary>
        ///调查终结后承办人签章（主办人）
        /// </summary>
        public string DCJBHCBRZ { get; set; }

        /// <summary>
        /// 调查终结后承办人日期(主办人)
        /// </summary>
        public DateTime? DCJBHCBRQ { get; set; }

        /// <summary>
        /// 协办队员意见
        /// </summary>
        public string XBDYYJ { get; set; }

        /// <summary>
        /// 协办队员处理结果
        /// </summary>
        public bool XBDYCLJG { get; set; }

        /// <summary>
        /// 协办队员处理时间
        /// </summary>
        public DateTime? XBDYCLSJ { get; set; }

        /// <summary>
        ///协办队员签章
        /// </summary>
        public string XBDYQZ { get; set; }



        /// <summary>
        /// 办案单位意见
        /// </summary>
        public string BADWYJ { get; set; }

        /// <summary>
        /// 办案单位签章
        /// </summary>
        public string BADWQZ { get; set; }

        /// <summary>
        /// 办案单位日期
        /// </summary>
        public DateTime? BADWRQ { get; set; }

        /// <summary>
        /// 办案单位结果
        /// </summary>
        public bool? BADWJG { get; set; }

        /// <summary>
        /// 法制处处理结果
        /// </summary>
        public bool FZCCLJG { get; set; }

        /// <summary>
        /// 法制处意见
        /// </summary>
        public string FZCYJ { get; set; }

        /// <summary>
        /// 法制处意见签章
        /// </summary>
        public string FZCQZ { get; set; }

        /// <summary>
        /// 法制处意见签章日期
        /// </summary>
        public DateTime? FZCQZRQ { get; set; }


        /// <summary>
        /// 分管领导意见
        /// </summary>
        public string FGLDYJ { get; set; }

        /// <summary>
        /// 分管局领导签章
        /// </summary>
        public string FGLDQZ { get; set; }

        /// <summary>
        /// 分管局领导签章日期
        /// </summary>
        public DateTime? FGLDQZRQ { get; set; }



        /// <summary>
        /// 集体讨论日期
        /// </summary>
        public DateTime? JTTLRQ { get; set; }

        /// <summary>
        /// 讨论结果
        /// </summary>
        public string TLJG { get; set; }

        /// <summary>
        /// 集体讨论处理用户
        /// </summary>
        public string JTTLCLYH { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
