using HZW.ZHCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
   public class CoordinateBLL
    {
       private CoordinateDAL dal = new CoordinateDAL();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
       public string GetUserCoordinate(int userid)
       {
           return dal.GetUserCoordinate(userid);
       }
    }
}
