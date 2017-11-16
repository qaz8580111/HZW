using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.CARQWGLBLL
{
   public class QWGL_CARLATESTPOSITIONSBLL
    {

        /// <summary>
        /// 添加定位信息
        /// </summary>
        /// <returns></returns>
       public static void AddCarlatestpositions(QWGL_CARLATESTPOSITIONS model)
        {
            Entities db = new Entities();
            QWGL_CARLATESTPOSITIONS model_carlatestpositions = db.QWGL_CARLATESTPOSITIONS.FirstOrDefault(t => t.IMEI == model.IMEI);
            if (model_carlatestpositions == null)
            {
                db.QWGL_CARLATESTPOSITIONS.Add(model);
            }
            else
            {
                model_carlatestpositions.SPEED = model.SPEED;
                model_carlatestpositions.DIRECTION = model.DIRECTION;
                model_carlatestpositions.MILEAGE = model.MILEAGE;
                model_carlatestpositions.ISOVERAREA = model.ISOVERAREA;
                model_carlatestpositions.X84 = model.X84;
                model_carlatestpositions.Y84 = model.Y84;
                model_carlatestpositions.X2000 = model.X2000;
                model_carlatestpositions.Y2000 = model.Y2000;
                model_carlatestpositions.LOCATETIME = model.LOCATETIME;
                model_carlatestpositions.RECIEVETIME = model.RECIEVETIME;
                model_carlatestpositions.CREATETIME = model.CREATETIME;
            }
            db.SaveChanges();
        }
    }
}
