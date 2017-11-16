using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.GCGLBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.BLL.XTBGBLL;
using ZGM.BLL.XTGLBLL;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.ViewModels;

namespace ZGM.Web.Controllers.GCGL
{
    public class MajorProjectsListController : Controller
    {
        //
        // GET: /MajorProjectsList/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 获取Table列表
        /// </summary>
        public JsonResult AllEventsTableList(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            string GC_NAME = Request["GC_NAME"];
            string JHKGRQSTIME = Request["JHKGRQSTIME"];
            string JHKGRQETIME = Request["JHKGRQETIME"];
            string JHJGRQSTIME = Request["JHJGRQSTIME"];
            string JHJGRQETIME = Request["JHJGRQETIME"];
            string GCGCZT_ID = Request["GCGCZT_ID"];


            decimal Id = SessionManager.User.UserID;
            IEnumerable<VMZDGCModels> List = BP_GCGKXXBLL.GetMajorProjectsLists().Where(a => a.SFLJSC == 0);
            if (!string.IsNullOrEmpty(GC_NAME))
                List = List.Where(a => a.GC_NAME.Contains(GC_NAME));
            if (!string.IsNullOrEmpty(JHKGRQSTIME))
                List = List.Where(t => t.JHKGRQ.Value.Date >= DateTime.Parse(JHKGRQSTIME).Date);
            if (!string.IsNullOrEmpty(JHKGRQETIME))
                List = List.Where(t => t.JHKGRQ.Value.Date <= DateTime.Parse(JHKGRQETIME));
            if (!string.IsNullOrEmpty(JHJGRQSTIME))
                List = List.Where(t => t.JHJGRQ.Value.Date >= DateTime.Parse(JHJGRQSTIME).Date);
            if (!string.IsNullOrEmpty(JHJGRQETIME))
                List = List.Where(t => t.JHJGRQ.Value.Date <= DateTime.Parse(JHJGRQETIME).Date);
            if (GCGCZT_ID != "请选择")
                List = List.Where(t => t.GCGCZT_ID == GCGCZT_ID);

            int count = List != null ? List.Count() : 0;//获取总行数
            var data = List.Skip((int)iDisplayStart).Take((int)iDisplayLength)
                .Select(a => new
                {
                    #region 获取
                    GC_ID = a.GC_ID,
                    GC_NAME = a.GC_NAME,
                    GCGCZT_ID = a.GCGCZT_ID,
                    JHKGRQ = a.JHKGRQ.Value.ToString("yyyy-MM-dd"),
                    JHJGRQ = a.JHJGRQ.Value.ToString("yyyy-MM-dd"),
                    TBR_ID = a.UserName,

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
        public int DeleteGCCKXXList()
        {
            string GC_ID = Request["GC_ID"];
            decimal gcid = 0;
            decimal.TryParse(GC_ID, out gcid);
            BP_GCGKXXBLL.DeleteGCGKXX(gcid);
            return 1;
        }

        /// <summary>
        /// 下拉框
        /// </summary>
        public void GetList()
        {
            //获取所有的下拉列表
            List<BP_GCZD> list = BP_GCZDBLL.GetGCZDSourceList().ToList();

            //工程类型
            ViewBag.GCLX_TYPE = GetSelectItem(list, "GCLX_TYPE");

            //工程性质
            ViewBag.GCXZ_TYPE = GetSelectItem(list, "GCXZ_TYPE");

            //限额标准
            ViewBag.XEBZ = GetSelectItem(list, "XEBZ_TYPE");

            //工程内容类型
            ViewBag.GCNRLX_ID = GetSelectItem(list, "GCNR_TYPE");

            //维护类型
            ViewBag.WHLX_TYPE = GetSelectItem(list, "WHLE_TYPE");

        }


        /// <summary>
        /// 获取下拉列表赋值
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        public List<SelectListItem> GetSelectItem(List<BP_GCZD> list, string TypeName)
        {
            List<SelectListItem> listViewBag = list.Where(a => a.TYPEID == TypeName).
                Select(c => new SelectListItem()
                {
                    Text = c.ZDNAME,
                    Value = c.ZDID.ToString()
                }).ToList();
            return listViewBag;
        }

    }
}
