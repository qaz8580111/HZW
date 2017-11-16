using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.CoordinationManager;

namespace ZGM.BLL.ZHCGBLL
{
    public class XTGL_ZHCGSBLL
    {

        /// <summary>
        /// 添加案件
        /// </summary>
        /// <param name="model"></param>
        public static string AddZHCGS(XTGL_ZHCGS model)
        {
            Entities db = new Entities();
            // model.ATTRACHID = GetNewId();
            XTGL_ZHCGS zhcgmodel = db.XTGL_ZHCGS.FirstOrDefault(t=>t.TASKID==model.TASKID);
            if (zhcgmodel!=null)
            {
                //zhcgmodel.TASKID = model.TASKID;
                zhcgmodel.TASKNUM = model.TASKNUM;
                zhcgmodel.FINDTIME = model.FINDTIME;
                zhcgmodel.EVENTSOURCE = model.EVENTSOURCE;
                zhcgmodel.EVENTTYPE = model.EVENTTYPE;
                zhcgmodel.MAINTYPE = model.MAINTYPE;
                zhcgmodel.SUBTYPE = model.SUBTYPE;
                zhcgmodel.DISTRICTCODE = model.DISTRICTCODE;
                zhcgmodel.DISTRICTNAME = model.DISTRICTNAME;
                zhcgmodel.STREETCODE = model.STREETCODE;
                zhcgmodel.STREETNAME = model.STREETNAME;
                zhcgmodel.COMMUNITYCODE = model.COMMUNITYCODE;
                zhcgmodel.COMMUNITYNAME = model.COMMUNITYNAME;
                zhcgmodel.COORDINATEX = model.COORDINATEX;
                zhcgmodel.COORDINATEY = model.COORDINATEY;
                zhcgmodel.EVENTADDRESS = model.EVENTADDRESS;
                zhcgmodel.EVENTDESCRIPTION = model.EVENTDESCRIPTION;
                zhcgmodel.EVENTPOSITIONMAP = model.EVENTPOSITIONMAP;
                zhcgmodel.SENDTIME = model.SENDTIME;
                zhcgmodel.DEALENDTIME = model.DEALENDTIME;
                zhcgmodel.SENDMEMO = model.SENDMEMO;
                zhcgmodel.DEALTIMELIMIT = model.DEALTIMELIMIT;
                zhcgmodel.DEALUNIT = model.DEALUNIT;
                zhcgmodel.CRATETIME = model.CRATETIME;
                zhcgmodel.STATE = model.STATE;
                zhcgmodel.DISPOSEID = model.DISPOSEID;
                zhcgmodel.DISPOSENAME = model.DISPOSENAME;
                zhcgmodel.DISPOSEDATE = model.DISPOSEDATE;
                zhcgmodel.DISPOSEMEMO = model.DISPOSEMEMO;
                zhcgmodel.LATESTDEALTIMELIMIT = model.LATESTDEALTIMELIMIT;
                zhcgmodel.LATESTDEALENDTIME = model.LATESTDEALENDTIME;
                zhcgmodel.WORKLOAD = model.WORKLOAD;
                zhcgmodel.COST = model.COST;
                zhcgmodel.ISVALUATION = model.ISVALUATION;
                zhcgmodel.NOTE = model.NOTE;
            }
            else
            {
                db.XTGL_ZHCGS.Add(model);
            }
           
           return db.SaveChanges().ToString();
        }

        public static IEnumerable<EnforcementUpcoming> GetAllZHCGSList(string state)
        {
            Entities db = new Entities();
            IEnumerable<EnforcementUpcoming> list = (from zhcg in db.XTGL_ZHCGS
                                                     select new EnforcementUpcoming
                                                     {
                                                         EVENTCODE=zhcg.TASKNUM,
                                                         ZFSJID = zhcg.TASKNUM,
                                                         EVENTTITLE = zhcg.EVENTDESCRIPTION,
                                                         SOURCENAME = "智慧城管",
                                                         createtime = zhcg.FINDTIME,
                                                         SOURCEID = 1,
                                                         judgment = 1,
                                                         state = zhcg.STATE,
                                                         DEALENDTIME=zhcg.DEALENDTIME,
                                                         // DISPATCHOR =decimal.Parse( zhcg.DISPATCHOR),
                                                     }).Where(a => a.state != state);
            return list;
        }
        /// <summary>
        /// 修改智慧城管同步表
        /// </summary>
        /// <returns></returns>
        public static void ModifyZHCG(string TASKNUM,string state)
        {
            Entities db = new Entities();
            XTGL_ZHCGS results = db.XTGL_ZHCGS.FirstOrDefault(a => a.TASKNUM == TASKNUM);
            results.STATE = state;
            db.Configuration.ValidateOnSaveEnabled = false;
            db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            
        }


        /// <summary>
        /// 查询智慧城管同步表
        /// </summary>
        /// <returns></returns>
        public static XTGL_ZHCGS GetZHCGList(string ID)
        {
            Entities db = new Entities();
            XTGL_ZHCGS results = db.XTGL_ZHCGS.SingleOrDefault(t => t.TASKNUM == ID);
            return results;
        }


        /// <summary>
        /// 获取的编号
        /// </summary>
        private static string GetNewId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10000, 99999);
        }

        /// <summary>
        /// 取最新的一条数据
        /// </summary>
        /// <returns></returns>
        public static XTGL_ZHCGS GetZHCGMAXID()
        {
            Entities db = new Entities();
            XTGL_ZHCGS model = db.XTGL_ZHCGS.OrderByDescending(a => a.TASKID).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 获取最新的一条附件
        /// </summary>
        /// <returns></returns>
        public static XTGL_ZHCGMEDIAS GetMediasMaxID()
        {
            Entities db = new Entities();
            XTGL_ZHCGMEDIAS model = db.XTGL_ZHCGMEDIAS.OrderByDescending(a => a.MEDIAID).FirstOrDefault();
            return model;
        }

        /// <summary>
        /// 添加一条附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddMEDIA(XTGL_ZHCGMEDIAS model)
        {
            Entities db = new Entities();
            db.XTGL_ZHCGMEDIAS.Add(model);
            return db.SaveChanges();
        }
    }
}
