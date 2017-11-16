using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.XTBG;
using ZGM.Model.XTBGModels;

namespace ZGM.Web.Controllers.XTBG
{
    public class HocListController : Controller
    {
        //
        // GET: /HocList/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        public JsonResult HocListTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string DZSTARTINGTIME = Request["DZSTARTINGTIME"];
            string DZENDTIME = Request["DZENDTIME"];
            string MeetingAddresses = Request["MeetingAddresses"];
            decimal MeetingAddressesid = 0;
            decimal.TryParse(MeetingAddresses, out MeetingAddressesid);
            IEnumerable<HocListModel> List = null;
            try
            {
                List = OA_MEETINGSBLL.GetHocList();
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            if (!string.IsNullOrEmpty(DZSTARTINGTIME))
                List = List.Where(t => t.STIME.Value.Date >= DateTime.Parse(DZSTARTINGTIME).Date);
            if (!string.IsNullOrEmpty(DZENDTIME))
                List = List.Where(t => t.STIME.Value.Date <= DateTime.Parse(DZENDTIME).Date);
            if (!string.IsNullOrEmpty(MeetingAddresses))
                List = List.Where(t => t.MeetingAddressesid == MeetingAddressesid);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    ETIME = a.ETIME == null ? "" : a.ETIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    STIME = a.STIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    MEETINGTITLE = a.MEETINGTITLE,
                    USERNAME = a.USERNAME,
                    ADDRESSNAME = a.ADDRESSNAME
                    #endregion
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
