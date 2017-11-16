using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;

namespace ZGM.BLL.QWGLBLLs
{
    public class UserLatestPositionBLL
    {
        /// <summary>
        /// 登录成功后更改最后一次队员数据
        /// </summary>
        /// <returns></returns>
        public static void UpdateLastPosition(decimal UserId,string IMEICode)
        {
            Entities db = new Entities();
            QWGL_USERLATESTPOSITIONS model = db.QWGL_USERLATESTPOSITIONS.FirstOrDefault(t => t.USERID == UserId);
            if (model == null)
            {
                QWGL_USERLATESTPOSITIONS mmodel = new QWGL_USERLATESTPOSITIONS();
                mmodel.USERID = UserId;
                mmodel.IMEICODE = IMEICode;
                mmodel.POSITIONTIME = DateTime.Now;
                mmodel.LASTLOGINTIME = DateTime.Now;
                db.QWGL_USERLATESTPOSITIONS.Add(mmodel);
            }
            if (model != null && UserId !=0)
            {
                model.LASTLOGINTIME = DateTime.Now;
            }
            db.SaveChanges();
        }

        /// <summary>
        /// 获取APP最新版本
        /// </summary>
        /// <returns></returns>
        public static APP_VERSIONS  GetLastVision()
        {
            Entities db = new Entities();
            return db.APP_VERSIONS.FirstOrDefault();
        }
    }
}
