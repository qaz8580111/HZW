using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Model
{
    public class Disdance
    {
        /// <summary>
        /// 最短距离
        /// </summary>
        public decimal disdance { get; set; }

        /// <summary>
        /// 道路编号
        /// </summary>
        public string ddbh { get; set; }

        /// <summary>
        /// 违法地点
        /// </summary>
        public string wfdd { get; set; }
    }
}