using HZW.ZHCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class PerceptionDeviceBLL
    {
        private PerceptionDeviceDAL dal = new PerceptionDeviceDAL();

        public project01 GetMaxProject01()
        {
            return dal.GetMaxProject01();
        }

        public int AddProject01(List<project01> list)
        {
            return dal.AddProject01(list);
        }

        public project02 GetMaxProject02()
        {
            return dal.GetMaxProject02();
        }

        public int AddProject02(List<project02> list)
        {
            return dal.AddProject02(list);
        }

        public project03 GetMaxProject03()
        {
            return dal.GetMaxProject03();
        }

        public int AddProject03(List<project03> list)
        {
            return dal.AddProject03(list);
        }

    }
}
