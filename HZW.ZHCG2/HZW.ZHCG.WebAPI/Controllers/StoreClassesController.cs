using HZW.ZHCG.BLL;
using HZW.ZHCG.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.WebAPI.Controllers
{
    public class StoreClassesController : ApiController
    {
        StoreClassesBLL StoreClassesbll = new StoreClassesBLL();

        [HttpGet]
        public List<StoreClasses> GetAllStoreClasses()
        {
            return StoreClassesbll.GetAllComb(); ;
        }
    }
}