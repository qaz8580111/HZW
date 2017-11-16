using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.PublicService
{
    public class GGFWSourceBLL
    {
        public static IQueryable<GGFWSOURCE> GetGGFWEvents()
        {
            PLEEntities db = new PLEEntities();
            return db.GGFWSOURCES;
        }
        public static string  GetNameByID(decimal ID)
        {
            PLEEntities db = new PLEEntities();
            GGFWSOURCE result = db.GGFWSOURCES.FirstOrDefault(a => a.SOURCEID==ID);
            return result.SOURCENAME;
        }

       
    }
}
