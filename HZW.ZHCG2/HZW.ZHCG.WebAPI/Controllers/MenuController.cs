using HZW.ZHCG.BLL;
using HZW.ZHCG.Model;
using HZW.ZHCG.WebAPI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class MenuController : ApiController
    {
        private MenuBLL bll = new MenuBLL();

        [HttpGet]
        public List<TreeMenu> GetTreeMenus(int userID)
        {
            return bll.GetTreeMenus(userID);
        }
    }

}