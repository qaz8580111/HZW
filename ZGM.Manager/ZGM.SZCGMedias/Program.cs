using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGM.SZCGMedias
{
    class Program
    {
        static void Main(string[] args)
        {
            SZCGMethod sm = new SZCGMethod();
            Console.WriteLine("开始同步附件!");
            sm.GetAllListFlie();
            Console.WriteLine("同步附件成功!");
        }
    }
}
