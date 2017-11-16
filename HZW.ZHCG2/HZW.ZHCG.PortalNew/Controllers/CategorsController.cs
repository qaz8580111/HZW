using HZW.ZHCG.BLL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.PortalNew.Controllers
{
    public class CategorsController : ApiController
    {
        private CategorsBLL bll = new CategorsBLL();

        [HttpGet]
        public List<Categors> GetmhcateGorsByParentId(int parentId, int takeNumber)
        {
            return bll.GetmhcateGorsByParentId(parentId, takeNumber);
        }
    }
}