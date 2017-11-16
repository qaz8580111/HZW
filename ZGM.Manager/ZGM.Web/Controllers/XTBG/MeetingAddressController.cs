using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;

namespace ZGM.Web.Controllers.XTBG
{
    public class MeetingAddressController : Controller
    {
        //
        // GET: /MeetingAddress/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 添加会议地址
        /// </summary>
        /// <param name="model"></param>
        public void Add(OA_MEETINGADDRESSES model) {
            OA_MEETINGADDRESSESBLL.AddMEETINGADDRESS(model);
            Response.Write("<script>parent.AddCallBack('Address')</script>");
        }

        /// <summary>
        /// 会议地址列表
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        public JsonResult AllEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            IEnumerable<OA_MEETINGADDRESSES> List = OA_MEETINGADDRESSESBLL.GetAllMeetingAddressList();

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength).ToList()
                .Select(a => new
                {
                    #region 获取
                    ADDRESSID=a.ADDRESSID,
                    ADDRESSNAME = a.ADDRESSNAME,
                    SEQ = a.SEQ,
                    REMARK = a.REMARK
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

        /// <summary>
        /// 删除列
        /// </summary>
        /// <returns></returns>
        public int DeleteMeetingAddressList()
        {
            string ADDRESSID = Request["ADDRESSID"];
            OA_MEETINGADDRESSESBLL.DeleteMeetingAddress(ADDRESSID);
            return 1;
        }

        /// <summary>
        /// 查看会议地址详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyAddress()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            decimal ADDRESSID = 0;
            decimal.TryParse(Request["ADDRESSID"], out ADDRESSID);
            OA_MEETINGADDRESSES model = new OA_MEETINGADDRESSES();
            if (ADDRESSID > 0)
            {
                model = OA_MEETINGADDRESSESBLL.GetMeetingAddressDetail(ADDRESSID);
                ViewBag.ADDRESSID = model.ADDRESSID;
                ViewBag.ADDRESSNAME = model.ADDRESSNAME;
                ViewBag.SEQ = model.SEQ;
                ViewBag.REMARK = model.REMARK;
            }

            return View();
        }


        public void ModifyMeetingAddress(OA_MEETINGADDRESSES model)
        {
            OA_MEETINGADDRESSESBLL.ModifyMeetingAddress(model);
            Response.Write("<script>parent.AddCallBack('Address')</script>");
        }
    }
}
