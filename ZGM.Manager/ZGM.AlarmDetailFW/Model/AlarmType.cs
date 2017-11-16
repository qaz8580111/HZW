using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmService.Model
{
    public enum AlarmType
    {
        //离线报警
        OffLine=3,

        //越界报警
        CrossLine=2,

        //停留报警
        StopForWhile=1,

        //休息处停留报警
        RestStopForWhile=1,
    }
}
