using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
    public  class UserPatrolModel
    {
        //用户编号
        public decimal UserID { get; set; }

        //用户姓名
        public string UserName { get; set; }

        //用户手机
        public string PhoneNum { get; set; }

        //巡查开始时间
        public DateTime? SDate { get; set; }

        //巡查结束时间
        public DateTime? EDate { get; set; }

        //巡查区域
        public string PatrolGeometry { get; set; }

        //休息区域
        public string RestGeometry { get; set; }
    }
}
