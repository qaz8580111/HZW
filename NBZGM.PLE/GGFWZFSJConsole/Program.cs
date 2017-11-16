using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGFWZFSJConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //更新公共服务中的事件同步到执法事件以后，每一步处理的人员及超期时间
            new UpdateZFSJTime().DealTime();

            //更新来源为公共服务中的事件在执法时间系统中已经删除的事件，需要公共服务模块重新指派
            new ZFSJToGGFWBack().DealZFSJDeleteToBack();

            // Console.Read();
        }
    }
}
