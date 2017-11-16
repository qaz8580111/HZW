using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.PublicService
{
     public  class GGFWMONTHLYREPORTSBLL
    {
         /// <summary>
         /// 导入数据
         /// </summary>
         /// <param name="model"></param>
         public void AddGGFWMONTHLYREPORT( GGFWMONTHLYREPORT model) {
             PLEEntities db = new PLEEntities();
             GGFWMONTHLYREPORT newModel = GetList().FirstOrDefault();
             if (newModel != null)
                 model.MREPORTID = newModel.MREPORTID + 1;
             db.GGFWMONTHLYREPORTS.Add(model);
             db.SaveChanges();
         }

         public IQueryable<GGFWMONTHLYREPORT> getGGFWMONTHLYREPORTList(DateTime date){
             PLEEntities db = new PLEEntities();
             IQueryable<GGFWMONTHLYREPORT> list = db.GGFWMONTHLYREPORTS.Where(a => a.CREATETIME == date);
             return list;
         }

         public IQueryable<GGFWMONTHLYREPORT> GetList()
         {
             PLEEntities db = new PLEEntities();
             IQueryable<GGFWMONTHLYREPORT> list = db.GGFWMONTHLYREPORTS.OrderByDescending(a => a.MREPORTID);
             return list;
         }
         public IQueryable<GGFWMONTHLYREPORT> getListByTime(DateTime fistDate) {
             PLEEntities db = new PLEEntities();
             IQueryable<GGFWMONTHLYREPORT> list = db.GGFWMONTHLYREPORTS.Where(a => a.CREATETIME == fistDate);
             return list;
         }

    }
}
