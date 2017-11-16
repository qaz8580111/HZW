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
    public class UnitTypeController : ApiController
    {
        private UnitTypeBLL bll = new UnitTypeBLL();

        [HttpGet]
        public List<UnitType> GetUnitTypes()
        {
            return bll.GetUnitTypes();
        }

        [HttpGet]
        public Paging<List<UnitType>> GetUnitTypes(int start, int limit)
        {
            return bll.GetUnitTypes(start, limit);
        }

        [HttpPost]
        public HttpResponseMessage AddUnitType(UnitType unitType)
        {
            bll.AddUnitType(unitType);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage EditUnitType(UnitType unitType)
        {
            bll.EditUnitType(unitType);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage DeleteUnitType(int id)
        {
            bll.DeleteUnitType(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}