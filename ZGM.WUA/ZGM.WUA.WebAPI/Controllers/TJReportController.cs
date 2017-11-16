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
    public class TJReportController : ApiController
    {
        TJReportBLL bll = new TJReportBLL();

        /// <summary>
        /// 获取部门事件上报
        /// </summary>
        /// <param name="DateType">日期类型:0当日,1当月,2当年</param>
        /// <returns></returns>
        public List<UnitReportEventModel> GetUnitReportEvents(decimal? DateType)
        {
            List<UnitReportEventModel> list = bll.GetUnitReportEvents(DateType);
            return list; 
        }

        /// <summary>
        /// 获取片区事件上报
        /// </summary>
        /// <param name="DateType">日期类型:0当日,1当月,2当年</param>
        /// <returns></returns>
        public List<ZoneReportEventModel> GetZoneReportEvents(decimal? DateType)
        {
            List<ZoneReportEventModel> list = bll.GetZoneReportEvents(DateType);
            return list;
        }

        /// <summary>
        /// 获取队员签到统计
        /// </summary>
        /// <param name="DateType">日期类型:0当日,1当月,2当年</param>
        /// <returns></returns>
        public List<UnitReportSignModel> GetUnitSign(decimal? DateType)
        {
            List<UnitReportSignModel> list = bll.GetUnitSign(DateType);
            return list;
        }

        /// <summary>
        /// 获取人员报警情况
        /// </summary>
        /// <param name="DateType">日期类型:0当日,1当月,2当年</param>
        /// <returns></returns>
        public UnitReportAlarmModel GetUnitAlarm(decimal? DateType)
        {
            UnitReportAlarmModel model = bll.GetUnitAlarm(DateType);
            return model;
        }

        /// <summary>
        /// 获取报警类型统计
        /// </summary>
        /// <param name="DateType">日期类型:0当日,1当月,2当年</param>
        /// <returns></returns>
        public List<UnitReportProwledModel> GetUnitProwled(decimal? DateType)
        {
            List<UnitReportProwledModel> list = bll.GetUnitProwled(DateType);
            return list;
        }

        /// <summary>
        /// 获取队员路程前10名 
        /// </summary>
        /// <param name="DateType">日期类型:0当日,1当月,2当年</param>
        /// <returns></returns>
        public List<ReportWalkModel> GetPersonWalk(decimal? DateType)
        {
            List<ReportWalkModel> list = bll.GetPersonWalk(DateType);
            return list;
        }
    }
}
