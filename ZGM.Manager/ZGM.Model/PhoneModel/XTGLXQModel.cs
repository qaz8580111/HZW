using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    public class XTGLXQModel : XTGL_ZFSJS
    {
        /// <summary>
        /// 登陆人姓名ID
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// 登陆人部门
        /// </summary>
        public int unitId { get; set; }
        /// <summary>
        /// 是否为领导督办
        /// </summary>
        public int ISSupervision { get; set; }     

        /// <summary>
        /// 手机标识
        /// </summary>
        public string PhoneIMEI { get; set; }
        /// <summary>
        /// 图片1
        /// </summary>
        public string Photo1 { get; set; }
        /// <summary>
        /// 图片2
        /// </summary>
        public string Photo2 { get; set; }
        /// <summary>
        /// 图片3
        /// </summary>
        public string Photo3 { get; set; }
    }
}
