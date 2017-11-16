using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HZW.ZHCG.SyncEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            SyncEvent se = new SyncEvent();
            Console.WriteLine("开始同步数据!");
            se.SyncEventInfo();
            Console.WriteLine("数据同步完成!");
        }
    }
}
