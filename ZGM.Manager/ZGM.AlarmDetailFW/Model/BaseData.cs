using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Model.PhoneModel;

namespace AlarmService.Model
{
    public class BaseData
    {
        //服务运行间隔(分钟)
        public static int SInterval = 15;

        //离线报警界限（分钟）
        public static int OLTimeLimit = 5;

        //越界报警界限（分钟）
        public static int CLTimeLimit = 10;

        //停留报警界限（分钟）
        public static int STTimeLimit = 15;

        //休息区停留报警界限（分钟）
        public static int RSTTimeLinit = 30;

        //报警对象
        public static List<UserPatrolModel> UserList { get; set; }

        //正在持续的报警
        public static List<QWGL_ALARMMEMORYLOCATIONDATA> UserAlarm { get; set; }
    }
}
