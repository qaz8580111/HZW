using HZW.ZHCG.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
{
    public class CoordinateController : ApiController
    {
        private CoordinateBLL bll = new CoordinateBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetUserCoordinate(int userid)
        {
            return bll.GetUserCoordinate(userid);
        }
    }
}