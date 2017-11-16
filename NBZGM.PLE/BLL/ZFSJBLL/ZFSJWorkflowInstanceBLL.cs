using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJWorkflowModel.ZFSJWorkflow;

namespace Taizhou.PLE.BLL.ZFSJBLL
{
    public class ZFSJWorkflowInstanceBLL
    {
        /// <summary>
        /// 添加流程实例
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string AddWorkflowInstance(ZFSJWORKFLOWINSTANCE instance)
        {
            if (instance != null)
            {
                PLEEntities db = new PLEEntities();
                instance.WIID = Guid.NewGuid().ToString("N");
                db.ZFSJWORKFLOWINSTANCES.Add(instance);
                db.SaveChanges();
                return instance.WIID;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据流程实例标识获取流程
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        /// <returns></returns>
        public static ZFSJWORKFLOWINSTANCE GetWorkflowInstanceByWIID(string wiid)
        {
            ZFSJWORKFLOWINSTANCE instance = null;
            if (wiid != null)
            {
                PLEEntities db = new PLEEntities();
                // instance = db.ZFSJWORKFLOWINSTANCES
                //  .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
                instance = db.ZFSJWORKFLOWINSTANCES
                    .SingleOrDefault<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
                return instance;
            }
            else
            {
                return instance;
            }
        }

        public static void UpdateAIID(string wiid, string aiid)
        {
            if (wiid != null && aiid != null)
            {
                PLEEntities db = new PLEEntities();
                ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                    .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
                instance.CURRENTAIID = aiid;
                db.SaveChanges();
            }
        }

        public static void UpdateStatus(string wiid, decimal status)
        {
            PLEEntities db = new PLEEntities();
            ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
            instance.STATUSID = status;
            instance.UPDATETIME = DateTime.Now;
            db.SaveChanges();
        }

        public static void UpdateData(string wiid, string data)
        {
            if (wiid != null && data != null)
            {
                PLEEntities db = new PLEEntities();
                ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                    .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == wiid);
                instance.WDATA = data;
                db.SaveChanges();
            }
        }

        public static void Update(ZFSJWORKFLOWINSTANCE model)
        {
            if (model != null)
            {
                PLEEntities db = new PLEEntities();
                ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                    .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == model.WIID);
                instance.STATUSID = model.STATUSID;
                instance.CURRENTAIID = model.CURRENTAIID;
                instance.WDATA = model.WDATA;
                instance.CREATETIME = model.CREATETIME;
                instance.UPDATETIME = model.UPDATETIME;
                instance.USERID = model.USERID;
                instance.UNTIID = model.UNTIID;
                instance.PHONEID = model.PHONEID;
                instance.EVENTSOURCEID = model.EVENTSOURCEID;
                instance.EVENTSOURCEPKID = model.EVENTSOURCEPKID;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加执法事件概要信息
        /// </summary>
        /// <param name="entity">执法事件概要信息对象</param>
        public static void AddSummaryInformation(ZFSJSUMMARYINFORMATION entity)
        {
            PLEEntities db = new PLEEntities();

            db.ZFSJSUMMARYINFORMATIONS.Add(entity);
            db.SaveChanges();
        }

        /// <summary>
        /// 获取执法事件的条数
        /// </summary>
        public static int zfsjListCount()
        {
            PLEEntities db = new PLEEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return db.ZFSJWORKFLOWINSTANCES.Where(t => t.CREATETIME > dt).Count();

        }
        /// <summary>
        /// 获得执法事件当月每天的条数
        /// </summary>
        /// <returns></returns>
        public static List<ZFSJWORKFLOWINSTANCE> GetZFSJWORKFLOWINSTANCEByMum()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PLEEntities db = new PLEEntities();
            List<ZFSJWORKFLOWINSTANCE> list = db.ZFSJWORKFLOWINSTANCES.Where(t => t.CREATETIME >= dt).ToList();
            return list;

        }

        public static IQueryable<ZFSJWORKFLOWINSTANCE> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<ZFSJWORKFLOWINSTANCE> list = db.ZFSJWORKFLOWINSTANCES;
            return list;
        }

        /// <summary>
        /// 执法事件流程表中给UNITID补上值
        /// </summary>
        public static void DaoRu()
        {

            PLEEntities db = new PLEEntities();
            //获得ZFSJWORKFLOWINSTANCE的所有数据
            IList<ZFSJWORKFLOWINSTANCE> list = db.ZFSJWORKFLOWINSTANCES.OrderByDescending(t=>t.CREATETIME).ToList();
            foreach (var item in list)
            {
                //反序列化json
                ZFSJForm Ilist = JsonHelper
             .JsonDeserialize<ZFSJForm>(item.WDATA);
                //获得的这条数据的Wiid
                string Wiid = Ilist.WIID;
                //获得的这条数据的中队ID
                decimal? SSZDID = Ilist.FinalForm.Form101.SSZDID;
                if (Wiid != null && SSZDID != 0)
                {
                    BuZhi(Wiid, SSZDID);
                }
                else 
                {
                    //如果有一个为Null 的值，则忽略这条数据，跳出此次循环
                    continue;
                }
            }


        }
        /// <summary>
        /// 执法事件流程表修改UNITID的值
        /// </summary>
        /// <param name="Wiid"></param>
        /// <param name="SSZDID"></param>
        public static void BuZhi(string Wiid,decimal? SSZDID)
        {
                PLEEntities db = new PLEEntities();
                ZFSJWORKFLOWINSTANCE instance = db.ZFSJWORKFLOWINSTANCES
                    .Single<ZFSJWORKFLOWINSTANCE>(t => t.WIID == Wiid);
                instance.UNTIID = SSZDID;
                db.SaveChanges(); 
        }
    }
}
