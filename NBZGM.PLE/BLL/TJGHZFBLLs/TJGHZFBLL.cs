using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.TJGHZFBLLs
{
    public class TJGHZFBLL
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        public void AddTJGHZF(TJGHZF model)
        {
            PLEEntities db = new PLEEntities();
            TJGHZF result = List().OrderByDescending(a=>a.ID).FirstOrDefault();
            if (result != null)
                model.ID = result.ID + 1;

            db.TJGHZFS.Add(model);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateTJGHZF(TJGHZF model)
        {
            PLEEntities db = new PLEEntities();
            TJGHZF result = db.TJGHZFS.SingleOrDefault(a => a.ID == model.ID);
            result.UNITID = model.UNITID;
            result.TJTIME = model.TJTIME;
            result.AYXCFX = model.AYXCFX;
            result.AYOTHER = model.AYOTHER;

            result.LA = model.LA;
            result.BJ = model.BJ;
            result.FK = model.FK;
            result.WFMJ = model.WFMJ;
            result.MSSW = model.MSSW;
            result.MSWFSR = model.MSWFSR;
            result.OZLTZJS = model.OZLTZJS;
            result.ODDFY = model.ODDFY;
            result.OYS = model.OYS;
            result.OZZTZ = model.OZZTZ;
            result.OSQFYZXSQ = model.OSQFYZXSQ;
            result.OSQFYZXZJ = model.OSQFYZXZJ;
            result.OBQSZFZCALL = model.OBQSZFZCALL;
            result.OBQSZFZCZJ = model.OBQSZFZCZJ;
            result.OXZFYSS = model.OXZFYSS;
            result.CREATETIME = model.CREATETIME;
            result.CREATEUSER = model.CREATEUSER;
            result.CHECKUSSER = model.CHECKUSSER;

            return db.SaveChanges() == 1 ? true : false;
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        public TJGHZF Single(int id)
        {
            PLEEntities db = new PLEEntities();
            TJGHZF result = db.TJGHZFS.SingleOrDefault(a => a.ID == id);
            return result;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<TJGHZF> List()
        { 
            PLEEntities db = new PLEEntities();
            IQueryable<TJGHZF> result = db.TJGHZFS;
            return result;
        }


    }
}
