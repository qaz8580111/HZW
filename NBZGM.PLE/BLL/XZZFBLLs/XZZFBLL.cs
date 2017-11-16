using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XZZFBLLs
{
    public class XZZFBLL
    {
        /// <summary>
        /// 行政规划添加数据
        /// </summary>
        public static void InsertXZZF(XZZFTABLIST model)
        {
            PLEEntities db = new PLEEntities();
            XZZFTABLIST result = GetMaxID().FirstOrDefault();
            if (result != null)
            {
                model.ID = result.ID + 1;
            }
            else
            {
                model.ID = 1;
            }
            db.XZZFTABLISTS.Add(model);
            db.SaveChanges();

        }

        /// <summary>
        /// 获得XZZFTabList 中的最大ID那条数据
        /// </summary>
        public static List<XZZFTABLIST> GetMaxID()
        {
            PLEEntities db = new PLEEntities();
            List<XZZFTABLIST> list = db.XZZFTABLISTS.OrderByDescending(t => t.ID).ToList();
            return list;
        }

        /// <summary>
        /// 获得全部的数据
        /// </summary>
        /// <returns></returns>
        public static List<XZZFTABLIST> GetAllList()
        {
            PLEEntities db = new PLEEntities();
            List<XZZFTABLIST> list = db.XZZFTABLISTS.ToList();
            return list;
        }

        /// <summary>
        /// 修改行政执法数据
        /// </summary>
        public static void UpdateXZZF(XZZFTABLIST model)
        {
            PLEEntities db = new PLEEntities();
            XZZFTABLIST result = db.XZZFTABLISTS.SingleOrDefault(a => a.ID == model.ID);
            result.ANYOTHER = model.ANYOTHER;
            result.AYXCFX = model.AYXCFX;
            result.CASEBJ = model.CASEBJ;
            result.CASEFAKY = model.CASEFAKY;
            result.CASELA = model.CASELA;
            result.CASEMSWFCWY = model.CASEMSWFCWY;
            result.CASEMSWFSDY = model.CASEMSWFSDY;
            result.CASEQZCSJ = model.CASEQZCSJ;
            result.CASESQFYZX = model.CASESQFYZX;
            result.CASEZLTYJ = model.CASEZLTYJ;
            result.CASEZZTZ = model.CASEZZTZ;
            result.DTTIME = model.DTTIME;
            result.SHUSER = model.SHUSER;
            result.SIMPLEFKJ = model.SIMPLEFKJ;
            result.SIMPLEFKY = model.SIMPLEFKY;
            result.TBUSER = model.TBUSER;
            db.SaveChanges();

        }

        /// <summary>
        /// 修改行政执法页面列表
        /// </summary>
        public static void UpdateXZZFList(XZZFTABLIST model, DateTime StartDT, DateTime EndDT)
        {
            PLEEntities db = new PLEEntities();
            XZZFTABLIST result = db.XZZFTABLISTS.SingleOrDefault(a => a.DTTIME >= StartDT && a.DTTIME < EndDT && a.CLASSID == model.CLASSID && a.UNITNAMEID == model.UNITNAMEID);
            if (result != null)
            {
                result.TBUSER = model.TBUSER;
                result.TBUSER = model.TBUSER;
                result.AYXCFX = model.AYXCFX;
                result.ANYOTHER = model.ANYOTHER;
                result.SIMPLEFKJ = model.SIMPLEFKJ;
                result.SIMPLEFKY = model.SIMPLEFKY;
                result.CASELA = model.CASELA;
                result.CASEBJ = model.CASEBJ;
                result.CASEFAKY = model.CASEFAKY;
                result.CASEMSWFSDY = model.CASEMSWFSDY;
                result.CASEMSWFCWY = model.CASEMSWFCWY;
                result.CASEQZCSJ = model.CASEQZCSJ;
                result.CASEZLTYJ = model.CASEZLTYJ;
                result.CASEOTHER = model.CASEOTHER;
                result.CASEZZTZ = model.CASEZZTZ;
                result.CASESQFYZX = model.CASESQFYZX;
                db.SaveChanges();
            }

        }
    }
}
