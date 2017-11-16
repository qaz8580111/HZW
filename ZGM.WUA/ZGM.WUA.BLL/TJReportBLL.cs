using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.DAL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.BLL
{
    public class TJReportBLL
    {
        TJReportDAL dal = new TJReportDAL();

        /// <summary>
        /// 获取部门事件上报
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public List<UnitReportEventModel> GetUnitReportEvents(decimal? DateType)
        {
            List<UnitReportEventModel> list = dal.GetUnitReportEventsList(DateType);

            return list;
        }

        /// <summary>
        /// 获取片区事件上报
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public List<ZoneReportEventModel> GetZoneReportEvents(decimal? DateType)
        {
            List<ZoneReportEventModel> list = dal.GetZoneReportEventsList(DateType);

            return list;
        }

        /// <summary>
        /// 获取队员签到统计
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public List<UnitReportSignModel> GetUnitSign(decimal? DateType)
        {
            List<UnitReportSignModel> list = dal.GetUnitSignInList(DateType);

            return list;
        }

        /// <summary>
        /// 获取人员报警情况
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public UnitReportAlarmModel GetUnitAlarm(decimal? DateType)
        {
            DateTime dt = DateTime.Now;
            UnitReportAlarmModel model = new UnitReportAlarmModel();
            model = dal.GetPieReportAlarm(DateType);
            decimal? count = model.OverBorderCount + model.OverTimeCount + model.OverLineCount;
            //UnitReportAlarmModel model = new UnitReportAlarmModel();
            //model.OverBorderCount = dal.GetUnitAlarmCount(1, DateType, dt.Year, dt.Month, dt.Day);
            //model.OverTimeCount = dal.GetUnitAlarmCount(2, DateType, dt.Year, dt.Month, dt.Day);
            //model.OverLineCount = dal.GetUnitAlarmCount(3, DateType, dt.Year, dt.Month, dt.Day);
            //decimal? count = model.OverBorderCount+model.OverTimeCount+model.OverLineCount;
            model.OverBorderPercent = count == 0 ? "0%" : ((double)model.OverBorderCount / (double)count).ToString("0%");
            model.OverTimePercent = count == 0 ? "0%" : ((double)model.OverTimeCount / (double)count).ToString("0%");
            model.OverLinePercent = count == 0 ? "0%" : ((double)model.OverLineCount / (double)count).ToString("0%");

            return model;
        }

        /// <summary>
        /// 获取报警类型统计
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public List<UnitReportProwledModel> GetUnitProwled(decimal? DateType)
        {
            List<UnitReportProwledModel> list = dal.GetUnitProwledList(DateType);

            return list;
        }

        /// <summary>
        /// 获取队员路程前10名 
        /// </summary>
        /// <param name="DateType"></param>
        /// <returns></returns>
        public List<ReportWalkModel> GetPersonWalk(decimal? DateType)
        {
            List<ReportWalkModel> list = dal.GetPersonWalkList(DateType);

            return list;
        }
    }
}
