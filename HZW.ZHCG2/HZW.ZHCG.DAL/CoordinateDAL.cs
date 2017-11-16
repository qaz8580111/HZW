using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class CoordinateDAL
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string GetUserCoordinate(int userid) {
            DateTime time = DateTime.Now;
           DateTime dt1 = time.Date;
           DateTime dt2 = dt1.AddDays(1);
           string Coordinate = "";
          hzwEntities db=new hzwEntities();
          IQueryable<fi_topatrolpos> queryable = db.fi_topatrolpos.Where(a => a.patrolid == userid && a.datainsertdate > dt1 && a.datainsertdate < dt2).OrderBy(a=>a.datainsertdate);

          foreach (var item in queryable)
          {
              Coordinate += item.coordinatex + "," + item.coordinatey + ";";
          }
          return Coordinate;
       }
    }
}
