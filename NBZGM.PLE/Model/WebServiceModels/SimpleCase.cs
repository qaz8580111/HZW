using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.WebServiceModels
{
    public class SimpleCase
    {

        /// <summary>
        /// 用户名标识
        /// </summary>
        public decimal? UserID { get; set; }

        /// <summary>
        /// 决定书编号
        /// </summary>
        public string JDSBH { get; set; }

        /// <summary>
        /// 当事人姓名
        /// </summary>
        public string DSRName { get; set; }

        /// <summary>
        /// 当事人性别
        /// </summary>
        public string DSRGender { get; set; }

        /// <summary>
        /// 当事人身份证号
        /// </summary>
        public string DSRIDNumber { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        public string FZRName { get; set; }

        /// <summary>
        /// 负责人职务
        /// </summary>
        public string FZRPosition { get; set; }

        /// <summary>
        /// 负责人所在地址
        /// </summary>
        public string FZRAddress { get; set; }

        /// <summary>
        /// 案件违法时间
        /// </summary>
        public string CaseTime { get; set; }

        /// <summary>
        /// 案件违法地点
        /// </summary>
        public string CaseAddress { get; set; }

        /// <summary>
        /// 违法行为标识
        /// </summary>
        public decimal? IllegalItemID { get; set; }

        /// <summary>
        /// 罚款金额
        /// </summary>
        public decimal? FKJE { get; set; }

        /// <summary>
        /// 执法时间
        /// </summary>
        public string ZFSJ { get; set; }

        /// <summary>
        /// GPS定位经度
        /// </summary>
        public decimal? Lon { get; set; }

        /// <summary>
        /// GPS定位纬度
        /// </summary>
        public decimal? Lat { get; set; }

        /// <summary>
        /// 违法行为名称
        /// </summary>
        public string WFXWName { get; set; }

        /// <summary>
        /// 图片集合
        /// </summary>
        public List<string> processedPhotoList { get; set; }

        /// <summary>
        /// 手机上传唯一标识
        /// </summary>
        public string PhoneID { get; set; }
    }
}
