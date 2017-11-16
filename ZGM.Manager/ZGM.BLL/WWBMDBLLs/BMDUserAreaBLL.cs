using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.WWBMDModels;

namespace ZGM.BLL.WWBMDBLLs
{
    public class BMDUserAreaBLL
    {
        /// <summary>
        /// 根据ID获取集合
        /// </summary>
        /// <returns></returns>
        public static IQueryable<BMDUserAreaModel> GetBMDUserAreaListByID(decimal ID)
        {
            Entities db = new Entities();
            IQueryable<BMDUserAreaModel> BMDUserAreas = from s in db.BMD_USERAREA
                                                        where s.BMDID == ID
                                                        select new BMDUserAreaModel
                                                  {
                                                      UAID = s.UAID,
                                                      BMDID = s.BMDID,
                                                      ADDRESSNAME = s.ADDRESSNAME,
                                                      REMARK = s.REMARK,
                                                      GEOMETRY = s.GEOMETRY
                                                  };
            return BMDUserAreas;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="BMDUserArea"></param>
        /// <returns></returns>
        public static int AddBMDUserArea(BMD_USERAREA BMDUserArea)
        {
            Entities db = new Entities();
            db.BMD_USERAREA.Add(BMDUserArea);
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public static int EditBMDUserArea(BMD_USERAREA BMDUserArea)
        {
            Entities db = new Entities();
            BMD_USERAREA _model = db.BMD_USERAREA.FirstOrDefault(t => t.BMDID == BMDUserArea.BMDID);
            if (_model != null)
            {
                _model.ADDRESSNAME = BMDUserArea.ADDRESSNAME;
                _model.REMARK = BMDUserArea.REMARK;
                _model.GEOMETRY = BMDUserArea.GEOMETRY;
            }
            return db.SaveChanges();
        }
    }
}
