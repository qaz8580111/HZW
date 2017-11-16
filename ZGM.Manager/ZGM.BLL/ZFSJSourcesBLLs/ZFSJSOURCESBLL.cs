/*类名：ZFSJSOURCESBLL
 *功能：执法事件来源的基本操作(查询)
 *创建时间:2016-04-06 10:39:46
 *版本：VS 1.1.0
 *作者:方勇
 *完成时间:2016-04-06 10:39:51
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Common.Enums;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL
{
    /// <summary>
    /// 执法事件来源
    /// </summary>
   public  class ZFSJSOURCESBLL
    {
      /// <summary>
        /// 获取所有的事件来源
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XTGL_ZFSJSOURCES> GetZFSJSourceList() 
        {
            Entities db =new Entities();
            IQueryable<XTGL_ZFSJSOURCES> results =db.XTGL_ZFSJSOURCES ;
            return results;
        }
       /// <summary>
       /// 根据主键获取事件来源
       /// </summary>
       /// <param name="SOURCEID"></param>
       /// <returns></returns>
        public static string GetZFSJSource(string SOURCEID)
        {
            Entities db = new Entities();
            int id = int.Parse(SOURCEID);
            XTGL_ZFSJSOURCES model = db.XTGL_ZFSJSOURCES.FirstOrDefault(t => t.SOURCEID == id);
            return model.SOURCENAME;
        }
       /// <summary>
       /// 根据标识ID获取事件来源
       /// </summary>
       /// <param name="ID"></param>
       /// <returns></returns>
        public static XTGL_ZFSJSOURCES GetSourceByID(decimal ID)
        {
            Entities db = new Entities();
            XTGL_ZFSJSOURCES result = db.XTGL_ZFSJSOURCES.Single<XTGL_ZFSJSOURCES>(t => t.SOURCEID == ID);
            return result;
        }

        /// <summary>
        /// 获取一段时间内事件列表
        /// </summary>
        /// <returns></returns>
        public static VMZFSJTJ GetReportEventList(decimal UserId, string STime, string ETime)
        {
            Entities db = new Entities();
            VMZFSJTJ model = new VMZFSJTJ();
            IQueryable<VMZFSJTJ> list = (from tjph in db.TJ_PERSONEVENT_HISTORY
                                        where tjph.PERSONID == UserId
                                         select new VMZFSJTJ
                                        {
                                            PERSONNAME = tjph.PERSONNAME,
                                            STATTIME = tjph.STATTIME,
                                            PATROLRCOUNT = tjph.PATROLRCOUNT,
                                            PATROLCLOSEDCOUNT = tjph.PATROLCLOSEDCOUNT
                                        });
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.STATTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.STATTIME <= endTime);
            }

            IQueryable<VMZFSJTJ> tlist = from tjpt in db.TJ_PERSONEVENT_TODAY
                                         where tjpt.PERSONID == UserId && tjpt.STATTIME.Year == DateTime.Now.Year && tjpt.STATTIME.Month == DateTime.Now.Month && tjpt.STATTIME.Day == DateTime.Now.Day
                                         select new VMZFSJTJ
                                         {
                                             PERSONNAME = tjpt.PERSONNAME,
                                             STATTIME = tjpt.STATTIME,
                                             PATROLRCOUNT = tjpt.PATROLRCOUNT,
                                             PATROLCLOSEDCOUNT = tjpt.PATROLCLOSEDCOUNT
                                         };
            IQueryable<VMZFSJTJ> ttlist = list.Union(tlist);
            model.PCount = list.Sum(t => t.PATROLRCOUNT);
            model.PCCount = list.Sum(t => t.PATROLCLOSEDCOUNT);

            return model;
        }

        /// <summary>
        /// 获取一段时间内队员所走路程
        /// </summary>
        /// <returns></returns>
        public static decimal? GetPersonWalk(decimal UserId, string STime, string ETime)
        {
            Entities db = new Entities();
            decimal? count = 0;
            IQueryable<VMWalkSum> list = (from tjph in db.TJ_PERSONWALK_HISTORY
                                          where tjph.PERSONID == UserId
                                          select new VMWalkSum
                                          {
                                                PERSONNAME = tjph.PERSONNAME,
                                                STATTIME = tjph.STATTIME,
                                                WALKSUM = tjph.WALKSUM
                                          });
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.STATTIME >= startTime);
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.STATTIME <= endTime);
            }
            IQueryable<VMWalkSum> tlist = (from tjpt in db.TJ_PERSONWALK_TODAY
                                           where tjpt.PERSONID == UserId && tjpt.STATTIME.Year == DateTime.Now.Year && tjpt.STATTIME.Month == DateTime.Now.Month && tjpt.STATTIME.Day == DateTime.Now.Day
                                          select new VMWalkSum
                                          {
                                              PERSONNAME = tjpt.PERSONNAME,
                                              STATTIME = tjpt.STATTIME,
                                              WALKSUM = tjpt.WALKSUM
                                          });
            IQueryable<VMWalkSum> ttlist = list.Union(tlist);
            count = list.Sum(t => t.WALKSUM);

            return count;
        } 
   }
}
