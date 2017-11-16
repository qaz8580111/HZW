using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.SZCGInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            SZCGMethod sm = new SZCGMethod();
            Console.WriteLine("开始同步数据!");
            sm.GetAllListEvent();
            Console.WriteLine("数据同步完成!");
            //sm.GetAllListFlie();
        }
    }
}
