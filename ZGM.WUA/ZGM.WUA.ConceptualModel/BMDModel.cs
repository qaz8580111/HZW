using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZGM.WUA.ConceptualModel
{
    public class BMDModel
    {
        [Key]
        public decimal BMDId { get; set; }
        public decimal? TypeId { get; set; }
        public string TypeName { get; set; }
        /// <summary>
        /// 矫正单位
        /// </summary>
        public string CorrectUnit { get; set; }
        public string Number { get; set; }
        public DateTime? CorrectDate { get; set; }
        public string Name { get; set; }
        public string OtherName { get; set; }
        public string Sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }
        public DateTime? Birthday { get; set; }
        /// <summary>
        /// 教育程度
        /// </summary>
        public string EDU { get; set; }
        public string Job { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string Political { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IDCard { get; set; }
        public string HeadIMGName { get; set; }
        public string HeadIMGPath { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string BirthPlace { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        public string DomicilePlace { get; set; }
        /// <summary>
        /// 婚姻情况
        /// </summary>
        public string Marriage { get; set; }
        /// <summary>
        /// 固定居住地
        /// </summary>
        public string FixedPlace { get; set; }
        /// <summary>
        /// 常用居住地
        /// </summary>
        public string CommonPlace { get; set; }
        /// <summary>
        /// 判决书号
        /// </summary>
        public string SentenceNumber { get; set; }
        /// <summary>
        /// 判决机关
        /// </summary>
        public string SentenceUnit { get; set; }
        /// <summary>
        /// 判决时间
        /// </summary>
        public DateTime? SentenceDate { get; set; }
        /// <summary>
        /// 罪名
        /// </summary>
        public string Charge { get; set; }
        /// <summary>
        /// 判决刑期
        /// </summary>
        public decimal? SentenceTerm { get; set; }
        /// <summary>
        /// 附加刑
        /// </summary>
        public string SentenceAdd { get; set; }
        /// <summary>
        /// 刑期开始时间
        /// </summary>
        public DateTime? SentenceStartTime { get; set; }
        /// <summary>
        /// 刑期结束时间
        /// </summary>
        public DateTime? SentenceEndTime { get; set; }
        /// <summary>
        /// 刑期变动情况
        /// </summary>
        public string SentenceChange { get; set; }
        /// <summary>
        /// 奖惩情况
        /// </summary>
        public string Reward { get; set; }
        /// <summary>
        /// 社区矫正开始时间
        /// </summary>
        public DateTime? CorrectStartTime { get; set; }
        /// <summary>
        /// 社区矫正结束时间
        /// </summary>
        public DateTime? CorrectEndTime { get; set; }
        /// <summary>
        /// 刑法执行类别
        /// </summary>
        public string SentenceType { get; set; }
        /// <summary>
        /// 主要犯罪事实
        /// </summary>
        public string Content { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public DateTime? CreateTime { get; set; }
        public decimal? CreateUser { get; set; }
        /// <summary>
        /// 状态 1：正常；2：删除
        /// </summary>
        public decimal? Status { get; set; }
    }
}
