using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.GCGLBLLs
{
    public class GCSGBLL
    {
        /// <summary>
        /// 获取一个新的施工进度标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewSGJDID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_GCSGJD_ID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的施工问题标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewSGWTID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_GCSGWT_ID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }
        /// <summary>
        /// 获取一个新的施工资金拨付标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewZJBFID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_GC_BFID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }


        /// <summary>
        /// 添加施工进度
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddSGJD(BP_GCSGJDXX model)
        {
            Entities db = new Entities();
            if (model != null)
            {
                db.BP_GCSGJDXX.Add(model);
            }

            return db.SaveChanges();
        }
        /// <summary>
        /// 添加施工问题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddSGWT(BP_GCSGWTXX model)
        {
            Entities db = new Entities();
            if (model != null)
            {
                db.BP_GCSGWTXX.Add(model);
            }

            return db.SaveChanges();
        }
        /// <summary>
        /// 添加施工拨付金额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddBFJE(BP_GCZJSYQKYB model)
        {
            Entities db = new Entities();
            if (model != null)
            {
                db.BP_GCZJSYQKYB.Add(model);
            }

            return db.SaveChanges();
        }

        /// <summary>
        /// 查询所有工程施工进度列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BP_GCSGJDXX> GetGCSGJDLists(decimal GC_ID)
        {
            Entities db = new Entities();
            IQueryable<BP_GCSGJDXX> lists = db.BP_GCSGJDXX.Where(a => a.GC_ID == GC_ID).OrderByDescending(t => t.TBSJ);
            return lists;
        }

        /// <summary>
        /// 查询所有工程施工问题列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BP_GCSGWTXX> GetGCSGWTLists(decimal GC_ID)
        {
            Entities db = new Entities();
            IQueryable<BP_GCSGWTXX> lists = db.BP_GCSGWTXX.Where(a => a.GC_ID == GC_ID).OrderByDescending(t => t.TBSJ);
            return lists;
        }
        /// <summary>
        /// 查询所有工程资金拨付列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BP_GCZJSYQKYB> GetGCZJBFLists(decimal GC_ID)
        {
            Entities db = new Entities();
            IQueryable<BP_GCZJSYQKYB> lists = db.BP_GCZJSYQKYB.Where(a => a.GC_ID == GC_ID).OrderByDescending(t => t.TJSJ);
            return lists;
        }
    }
}
