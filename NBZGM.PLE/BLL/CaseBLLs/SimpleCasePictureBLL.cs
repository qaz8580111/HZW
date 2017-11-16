using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class SimpleCasePictureBLL
    {
        /// <summary>
        /// 根据简易案件标识获取该案件所属的图片列表
        /// </summary>
        /// <param name="simpleCaseID">简易案件标识</param>
        /// <returns>该案件所属的图片列表</returns>
        public static IQueryable<SIMPLECASEPICTURE> GetPicturesBySimpleCaseID(decimal simpleCaseID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<SIMPLECASEPICTURE> results= 
                                from scp in db.SIMPLECASEPICTURES
                                where scp.SIMPLECASEID == simpleCaseID
                                select scp;

            return results;
        }

        /// <summary>
        /// 根据图片标识获取简易案件图片
        /// </summary>
        /// <param name="pictureID">图片标识</param>
        /// <returns></returns>
        public static SIMPLECASEPICTURE GetPictureByPictureID(decimal pictureID)
        {
            PLEEntities db = new PLEEntities();
            return db.SIMPLECASEPICTURES
                .SingleOrDefault(t=>t.SIMPLECASEPICTUREID==pictureID);
        
        }
    }
}
