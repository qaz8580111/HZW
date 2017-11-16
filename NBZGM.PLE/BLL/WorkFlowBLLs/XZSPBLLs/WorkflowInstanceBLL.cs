using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZSPBLLs
{
    public class WorkflowInstanceBLL
    {
        /// <summary>
        /// 获取所有的行政审批流程实例
        /// </summary>
        /// <returns></returns>
        public static IQueryable<XZSPWFIST> Query()
        {
            PLEEntities db = new PLEEntities();

            return db.XZSPWFISTS;
        }

        public static IQueryable<XZSPWFIST> Query(decimal statusID)
        {
            IQueryable<XZSPWFIST> instances = Query();
            instances = instances.Where(t => t.STATUSID == statusID);
            return instances;
        }

        /// <summary>
        /// 获取行政审批流程实例
        /// </summary>
        /// <param name="id">流程实例标识</param>
        /// <returns></returns>
        public static XZSPWFIST Single(string id)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == id);

            return instance;
        }

        /// <summary>
        /// 添加行政审批流程实例
        /// </summary>
        /// <param name="instance">流程实例</param>
        public static string Add(XZSPWFIST instance)
        {
            PLEEntities db = new PLEEntities();
            instance.WIID = Guid.NewGuid().ToString("N");
            db.XZSPWFISTS.Add(instance);
            db.SaveChanges();

            return instance.WIID;
        }

        /// <summary>
        /// 更新行政审批流程实例
        /// </summary>
        /// <param name="id">流程实例标识</param>
        /// <param name="data">属性值</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static XZSPWFIST Update(string id, decimal? status, string data)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == id);

            if (status != null)
            {
                instance.STATUSID = status;
            }

            if (!string.IsNullOrWhiteSpace(data))
            {
                instance.WDATA = data;
            }

            db.SaveChanges();

            return instance;
        }

        /// <summary>
        /// 更新行政审批流程实例
        /// </summary>
        /// <param name="id">流程实例标识</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static void UpdateStatus(string id, decimal status)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == id);
            instance.STATUSID = status;
            db.SaveChanges();
        }

        public static void UpdateData(string id, string data)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == id);
            instance.WDATA = data;
            db.SaveChanges();
        }

        public static void UpdateAIID(string id, string aiid)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == id);
            instance.CURRENTAIID = aiid;
            db.SaveChanges();
        }
        public static void UpdateDTWZ(string wiid, string dtwz)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .SingleOrDefault(t => t.WIID == wiid);
            instance.DTWZ = dtwz;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据流程实例标识删除实例
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        public static void RemoveWorkflowByWIID(string wiid)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .Single<XZSPWFIST>(t => t.WIID == wiid);
            db.XZSPWFISTS.Remove(instance);
            db.SaveChanges();
        }

        /// <summary>
        /// 更新中队名称
        /// </summary>
        /// <param name="wiid">中队名称</param>
        /// <param name="ZFZDName"></param>
        public static void UpdateZFZDName(string wiid, string ZFZDName)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS
                .Single<XZSPWFIST>(t => t.WIID == wiid);
            instance.ZFZDNAME = ZFZDName;
            db.SaveChanges();
        }

        /// <summary>
        /// 更新中队文书编号
        /// </summary>
        /// <param name="wiid">文书编号</param>
        /// <param name="ZFZDName"></param>
        public static void UpdateZFZDWSBH(string wiid, string xzspwsbh)
        {
            PLEEntities db = new PLEEntities();
            XZSPWFIST instance = db.XZSPWFISTS.FirstOrDefault(t => t.WIID == wiid);
            if (instance != null)
            {
                instance.XZSPWSBH = xzspwsbh;
            }
            db.SaveChanges();
        }
        /// <summary>
        /// 返回文书编号
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        public static string GetXZSPWSBH(string year)
        {
            PLEEntities db = new PLEEntities();
            string sqltext = "select * from xzspwfists where xzspwsbh like '%" + year + "%'";
            int count = db.XZSPWFISTS.SqlQuery(sqltext).ToList().Count();
            return string.Format("{0:D4}", ++count);
        }

        /// <summary>
        /// 判断文书编号是否存在
        /// </summary>
        /// <param name="xzspwsbh">文书编号</param>
        /// <returns></returns>
        public static bool ISGetXZSPWSBH(string xzspwsbh)
        {
            bool flag = true;
            PLEEntities db = new PLEEntities();
            int count = db.XZSPWFISTS.Where(t => t.XZSPWSBH == xzspwsbh).ToList().Count();
            if (count > 0)
            {
                flag = false;
            }
            return flag;
        }

        /// <summary>
        /// 查询当月行政审批的集合
        /// </summary>
        /// <returns></returns>
        public static List<XZSPWFIST> GetXZSPInstances()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            PLEEntities db = new PLEEntities();
            return db.XZSPWFISTS.Where(t => t.CREATEDTIME > dt && t.CREATEDTIME <= DateTime.Now).ToList();
        }
        /// <summary>
        /// 获取行政审批的数量
        /// </summary>
        /// <returns></returns>
        public static int XZSPListCount()
        {
            PLEEntities db = new PLEEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return db.XZSPWFISTS.Where(t => t.CREATEDTIME > dt).Count();
        }
        /// <summary>
        /// 获得行政审批每月每天的数量
        /// </summary>
        /// <returns></returns>
        public static List<XZSPWFIST> GetXZSPWFISTByMum() 
        {
            PLEEntities db = new PLEEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            List<XZSPWFIST> listx = db.XZSPWFISTS.Where(t => t.CREATEDTIME > dt).ToList();
            return listx;
        }

    }
}
