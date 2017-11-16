using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    /// <summary>
    /// 监控Json封装
    /// </summary>
    public class CameraJsonModel
    {
        [Key]
        public string id { get; set; }
        public string text { get; set; }
        //是否展开(closed,open),不赋值则默认open
        public string state { get; set; }
        //图标
        public string iconCls { get; set; }
        public object attributes { get; set; }
        public List<CameraJsonModel> children { get; set; }
    }
}
