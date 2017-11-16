using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class DrawController : ApiController
    {
        DrawBLL bll = new DrawBLL();

        /// <summary>
        /// /api/Draw/GetAllDraws
        /// 获取全部兴趣点
        /// </summary>
        /// <returns></returns>
        public List<DrawModel> GetAllDraws()
        {
            List<DrawModel> result = bll.GetAllDraws();
            return result;
        }

        /// <summary>
        /// /api/Draw/GetDraw?id=
        /// 根据兴趣点标识获取兴趣点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DrawModel GetDraw(string id)
        {

            DrawModel result = bll.GetDraw(Convert.ToDecimal( id));
            return result;
        }

        /// <summary>
        /// /api/Draw/AddDraw?type=&points=&userId=&note=
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
        [System.Web.Http.HttpGet]
        public decimal AddDraw(string type, string points, decimal userId, string note)
        {
            decimal result = bll.AddDraw(type, points, userId, note);
            return result;
        }

        /// <summary>
        /// /api/Draw/DeleteDraw?id=
        /// 删除兴趣点
        /// 0：失败；1：成功
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public int DeleteDraw(decimal id)
        {
            int result = bll.DeleteDraw(id);
            return result;
        }
    }
}
