using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.CarGPSAddress
{
    class Program
    {
        static void Main(string[] args)
        {
            GPSMethod gm = new GPSMethod();
            Console.WriteLine("开始同步数据!");
            gm.GetAllListEvent();
            Console.WriteLine("数据同步完成!");
        }
    }
}
