using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class StoreClassesBLL
    {
        StoreClassesDAL StoreClassDal = new StoreClassesDAL();

        /// <summary>
        /// 获取所有店家类型
        /// </summary>
        /// <returns></returns>
        public List<StoreClasses> GetAllComb()
        {
            return StoreClassDal.GetAllComb();
        }
    }
}
