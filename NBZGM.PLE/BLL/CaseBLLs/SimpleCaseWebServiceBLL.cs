using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.WebServiceModels;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class SimpleCaseWebServiceBLL
    {
        /// <summary>
        /// 新增简易案件
        /// </summary>
        /// <param name="simpleCase">简易案件对象</param>
        /// <param name="files">简易案件图片文件</param>
        /// <param name="pictureTypes">简易案件图片类型数组</param>
        public static int SubmitSimpleCase(SimpleCase simpleCase)
        {
            PLEEntities db = new PLEEntities();
            int count = db.SIMPLECASES.Where(t => t.PHONEID == simpleCase.PhoneID).Count();
            if (count > 0)
            {
                return 0;
            }
            USER user = GetUserByUserID((decimal)simpleCase.UserID);

            string sql = "SELECT SEQ_SIMPLECASEID.NEXTVAL FROM DUAL";
            decimal simpleCaseID = db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
            DateTime dt = new DateTime();

            SIMPLECAS simpleCas = new SIMPLECAS();

            simpleCas.SIMPLECASEID = simpleCaseID;
            simpleCas.JDSBH = simpleCase.JDSBH;
            simpleCas.DSRNAME = simpleCase.DSRName;
            simpleCas.DSRGENDER = simpleCase.DSRGender;
            simpleCas.DSRIDNUMBER = simpleCase.DSRIDNumber;
            simpleCas.FZRNAME = simpleCase.FZRName;
            simpleCas.FZRPOSITION = simpleCase.FZRPosition;
            simpleCas.FZRADDRESS = simpleCase.FZRAddress;
            simpleCas.CASETIME = simpleCase.CaseTime != "" ? Convert.ToDateTime(simpleCase.CaseTime) : dt;
            simpleCas.CASEADDRESS = simpleCase.CaseAddress;
            simpleCas.ILLEGALITEMID = simpleCase.IllegalItemID;
            simpleCas.FKJE = simpleCase.FKJE;
            simpleCas.ZFRNAME = user != null ? user.USERNAME : "";
            simpleCas.ZFZH = user != null ? user.ZFZBH : "";
            simpleCas.ZFSJ = simpleCase.ZFSJ != "" ? Convert.ToDateTime(simpleCase.ZFSJ) : dt;
            simpleCas.LON = simpleCase.Lon;
            simpleCas.LAT = simpleCase.Lat;
            simpleCas.USERID = user.USERID;
            simpleCas.UNTIID = user.UNITID;
            simpleCas.PHONEID = simpleCase.PhoneID;
            if (simpleCase.processedPhotoList != null)
            {
                for (int i = 0; i < simpleCase.processedPhotoList.Count; i++)
                {
                    string str = "SELECT SEQ_SIMPLECASEPICTUREID.NEXTVAL FROM DUAL";
                    decimal pictureID = db.Database.SqlQuery<decimal>(str).FirstOrDefault();
                    if (simpleCase.processedPhotoList[i] == null)
                    {
                        continue;
                    }
                    SIMPLECASEPICTURE scp = new SIMPLECASEPICTURE
                    {
                        SIMPLECASEPICTUREID = pictureID,
                        SIMPLECASEID = simpleCaseID,
                        PICTURETYPE = 2,
                        PICTURE = Encoding.UTF8.GetBytes(simpleCase.processedPhotoList[i])
                    };
                    db.SIMPLECASEPICTURES.Add(scp);
                }
            }

            db.SIMPLECASES.Add(simpleCas);

            count = db.SaveChanges();
            if (count <= 0)
            {
                return 2;
            }
            return 1;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="UserID">用户标识</param>
        /// <returns></returns>
        public static USER GetUserByUserID(decimal UserID)
        {
            return UserBLLs.UserBLL.GetUserByUserID(UserID);
        }
    }
}
