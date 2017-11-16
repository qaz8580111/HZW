using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class DrawDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 获取全部兴趣点
        /// </summary>
        /// <returns></returns>
        public IQueryable<DrawModel> GetAllDraws()
        {
            IQueryable<DrawModel> result = from t in db.FI_DRAWS
                                           select new DrawModel
                                           {
                                               ID = t.ID,
                                               Type = t.TYPE,
                                               Points = t.POINTS,
                                               UserID = t.USERID,
                                               UserName = t.SYS_USERS.USERNAME,
                                               Note = t.NOTE,
                                               CreateTime = t.CREATETIME
                                           };

            return result;
        }

        /// <summary>
        /// 根据兴趣点标识获取兴趣点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DrawModel GetDraw(decimal id)
        {
            IQueryable<DrawModel> result = db.FI_DRAWS
                .Where(t => t.ID == id)
                .Select(t => new DrawModel
                {
                    ID = t.ID,
                    Type = t.TYPE,
                    Points = t.POINTS,
                    UserID = t.USERID,
                    UserName = t.SYS_USERS.USERNAME,
                    Note = t.NOTE,
                    CreateTime = t.CREATETIME
                });
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 添加兴趣点
        /// 返回添加的兴趣点ID
        /// 0:失败
        /// </summary>
        /// <param name="draw"></param>
        /// <returns></returns>
        public decimal AddDraw(DrawModel draw)
        {
            try
            {
                db.FI_DRAWS
                    .Add(new FI_DRAWS
                    {
                        TYPE = draw.Type,
                        POINTS = draw.Points,
                        USERID = draw.UserID,
                        NOTE = draw.Note,
                        CREATETIME = DateTime.Now
                    });
                db.SaveChanges();

                FI_DRAWS result = db.FI_DRAWS
                    .OrderByDescending(t => t.ID)
                    .FirstOrDefault();
                return result.ID;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除兴趣点
        /// 0：失败；1：成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteDraw(decimal id)
        {
            try
            {

                FI_DRAWS draw = db.FI_DRAWS
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
                if (draw == null)
                    return 0;

                FI_DRAWS result = db.FI_DRAWS.Remove(draw);
                int count = db.SaveChanges();
                return count;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
