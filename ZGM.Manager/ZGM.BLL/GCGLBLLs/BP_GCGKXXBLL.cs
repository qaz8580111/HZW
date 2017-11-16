using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.GCGLBLLs
{
    public class BP_GCGKXXBLL
    {
        /// <summary>
        /// 添加工程基础信息
        /// </summary>
        /// <param name="model"></param>
        public static void AddGCGKXX(BP_GCGKXX model)
        {
            Entities db = new Entities();
            BP_GCGKXX gcgk_model = db.BP_GCGKXX.FirstOrDefault(t => t.GC_ID == model.GC_ID);
            if (gcgk_model == null)
            {
                db.BP_GCGKXX.Add(model);
            }
            else
            {
                gcgk_model.GC_ID = model.GC_ID;
                gcgk_model.GC_NAME = model.GC_NAME;
                gcgk_model.GCGCZT_ID = model.GCGCZT_ID;
                gcgk_model.GCDZ = model.GCDZ;
                gcgk_model.JSGM_TYPE = model.JSGM_TYPE;
                gcgk_model.GCLX_TYPE = model.GCLX_TYPE;
                gcgk_model.GCXZ_TYPE = model.GCXZ_TYPE;
                gcgk_model.YSZJ = model.YSZJ;
                gcgk_model.JSNR = model.JSNR;
                gcgk_model.JHKGRQ = model.JHKGRQ;
                gcgk_model.JHJGRQ = model.JHJGRQ;
                gcgk_model.SGGQ = model.SGGQ;
                gcgk_model.GCNRLX_ID = model.GCNRLX_ID;
                gcgk_model.TBR_ID = model.TBR_ID;
                gcgk_model.TBSJ = model.TBSJ;
                gcgk_model.SFZJ = model.SFZJ;
                gcgk_model.GLBM_ID = model.GLBM_ID;
                gcgk_model.GCBH = model.GCBH;
                gcgk_model.LXYJ = model.LXYJ;
                gcgk_model.LXPZJG = model.LXPZJG;
                gcgk_model.SFLJSC = model.SFLJSC;
                gcgk_model.XEBZ = model.XEBZ;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 查询所有列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<VMZDGCModels> GetMajorProjectsLists()
        {
            Entities db = new Entities();
            string sql = "select bg.SFLJSC, bg.GC_ID,bg.GC_NAME,bg.GCGCZT_ID,bg.JHKGRQ,bg.JHJGRQ,su.UserName from BP_GCGKXX bg left join sys_users su on bg.TBR_ID=su.userid order by bg.JHKGRQ desc";
            IEnumerable<VMZDGCModels> lists = db.Database.SqlQuery<VMZDGCModels>(sql);
            return lists;
        }

        /// <summary>
        /// 删除工程
        /// </summary>
        public static void DeleteGCGKXX(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCGKXX models = db.BP_GCGKXX.FirstOrDefault(t => t.GC_ID == gc_id);
            if (models != null)
            {
                models.SFLJSC = 1;
            }
            db.SaveChanges();
        }


        /// <summary>
        /// 修改工程过程
        /// </summary>
        public static void ModifyGCGKXX(decimal gc_id, string GCGCZT_ID)
        {
            Entities db = new Entities();
            BP_GCGKXX models = db.BP_GCGKXX.FirstOrDefault(t => t.GC_ID == gc_id);
            if (models != null)
            {
                models.GCGCZT_ID = GCGCZT_ID;
            }

            db.SaveChanges();
        }

        /// <summary>
        /// 查询工程概况
        /// </summary>
        /// <returns></returns>
        public static BP_GCGKXX GetGCGKlist(decimal gc_id)
        {
            Entities db = new Entities();
            BP_GCGKXX model = new BP_GCGKXX();
            BP_GCGKXX gc = db.BP_GCGKXX.Where(t => t.GC_ID == gc_id).FirstOrDefault();
            if (gc != null)
            {
                model = gc;
            }
            //foreach (var item in lists)
            //{
            //gc = item;
            //}


            return model;
        }

    }
}
