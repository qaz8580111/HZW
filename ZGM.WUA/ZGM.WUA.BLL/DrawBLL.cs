using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class DrawBLL
    {
        DrawDAL dal = new DrawDAL();

        /// <summary>
        /// 获取全部兴趣点
        /// </summary>
        /// <returns></returns>
        public List<DrawModel> GetAllDraws()
        {
            IQueryable<DrawModel> result = dal.GetAllDraws();
            return result.ToList();
        }

        /// <summary>
        /// 根据兴趣点标识获取兴趣点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DrawModel GetDraw(decimal id)
        {
            DrawModel result = dal.GetDraw(id);
            return result;
        }

        /// <summary>
        /// 添加兴趣点
        /// 返回添加的兴趣点ID
        /// 0:失败
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="points"></param>
        /// <param name="userId"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public decimal AddDraw(string type, string points, decimal userId, string note)
        {
            DrawModel draw = new DrawModel()
            {
                Type = type,
                Points = points,
                UserID = userId,
                Note = note
            };
            decimal result = dal.AddDraw(draw);
            return result;
        }

        /// <summary>
        /// 删除兴趣点
        /// 0：失败；1：成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteDraw(decimal id)
        {
            int result = dal.DeleteDraw(id);
            return result;
        }
    }
}
