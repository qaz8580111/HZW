using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model.CustomModels;
using ZGM.BLL.CAMERABLLs;
using ZGM.Model;


namespace ZGM.Web.Controllers.XTSZ
{
    public class CAMERAController : Controller
    {
        //
        // GET: /CAMERA/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewData["ParentID"] = "1";
            return View();
        }

        /// <summary>
        /// 获取监控专题树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCameraTree()
        {
            //释放内存资源
            Dispose();
            List<TreeModel> treeModels = CameraBLL.GetTreeNodes();
            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取监控元素树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCameraItemTree()
        {
            //释放内存资源
            Dispose();
            List<TreeModel> treeModels = CameraBLL.GetTreeitemNodes();
            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加监控专题
        /// </summary>
        /// <param name="Themename"></param>
        /// <param name="ifline"></param>
        /// <param name="parentid"></param>
        /// <param name="parentidpid"></param>
        /// <returns></returns>
        public JsonResult AddCameraTheme(string Themename, decimal ifline, string parentid, string parentidpid)
        {
            string path = "";
            if (string.IsNullOrEmpty(parentidpid))
            {
                path = "";
            }
            else
            {
                path = "\\" + parentid + "\\";
            }
            decimal count = CameraBLL.camerathemecount();
            FI_CAMERA_THEME newtheme = new FI_CAMERA_THEME();
            newtheme.NAME = Themename;
            newtheme.NOTE = "";
            newtheme.PARENTID = int.Parse(parentid);
            newtheme.ISLINE = ifline;
            newtheme.PATH = path;
            newtheme.THEMEID = count+1;
            int result = CameraBLL.addcameratheme(newtheme);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //删除监控专题
        public JsonResult DeleteCameraTheme(string parentid)
        {
            int result = 0;
            decimal counts = CameraBLL.getcandelete(decimal.Parse(parentid));
            if (counts <= 0)
            {
                result = CameraBLL.deletecameratheme(int.Parse(parentid));//如果没有元素，就循环删除监控专题
            }
            else
            {
                result = 2;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取监控元素列表数据并且进行分页
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUnits(int? iDisplayStart, int? iDisplayLength, int? secho)
        {

            string strParentID = this.Request.QueryString["ParentID"];

            decimal? unitID = null;

            if (!string.IsNullOrWhiteSpace(strParentID))
            {
                unitID = decimal.Parse(strParentID);
            }

            IQueryable<FI_CAMERA_ITEM> units = CameraBLL.GetChildUnit(unitID).OrderByDescending(t => t.SORTNUM);
            if (units.Count() == 0)
            {
                units = CameraBLL.GetInfoByID(unitID).OrderByDescending(t=>t.SORTNUM);
            }

            var list = units.ToList()
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    ItemId=t.THCID,
                    UnitID = t.THEMEID,
                    UnitName = t.NAME,
                    UnitTypeName = t.CAMERA_TYPE,
                    SortNum=t.SORTNUM,
                });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = units.Count(),
                iTotalDisplayRecords = units.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 添加监控元素
        /// </summary>
        /// <param name="themeid"></param>
        /// <param name="CameraItemIds"></param>
        /// <returns></returns>
        public JsonResult AddCameraItems(string themeid, string CameraItemIds)
        {
            int c = 0;
            string[] ids = CameraItemIds.Split(',');
            for (int i = 0, len = ids.Length; i < len; i++)
            {
                decimal id = decimal.Parse(ids[i].ToString());
                var info = CameraBLL.GetCAMERA_INFObyId(id);
                decimal? sqnomax = CameraBLL.getCAMERA_themebyId(decimal.Parse(themeid)) == 0 ? i+1  : CameraBLL.getCAMERA_themebyId(decimal.Parse(themeid)) +1;
                if (info != null)
                {
                    FI_CAMERA_ITEM newitem = new FI_CAMERA_ITEM();
                    newitem.THCID = CameraBLL.GetFI_THCIDID();
                    newitem.THEMEID = decimal.Parse(themeid);
                    newitem.NAME = info.NAME;
                    newitem.INDEX_CODE = info.INDEX_CODE;
                    newitem.CAMERAID = info.CAMERA_ID;
                    newitem.CAMERA_TYPE = info.CAMERA_TYPE;
                    newitem.LONGITUDE = info.LONGITUDE;
                    newitem.LATITUDE = info.LATITUDE;
                    newitem.SORTNUM = sqnomax;
                    CameraBLL.addcameraitem(newitem);
                }
                c = 1;
            }
            return Json(c, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除监控元素
        /// </summary>
        /// <param name="themeid"></param>
        /// <returns></returns>
        public JsonResult DeleteCameraItems(string themeid)
        {
            int c = 0;
            string[] ids = themeid.Substring(0,themeid.Length-1).Split(',');
            for (int i = 0, len = ids.Length; i < len; i++)
            {
                var id = decimal.Parse(ids[i].ToString());
                CameraBLL.deletecameraitem(id);
                c = 1;
            }
            return Json(c, JsonRequestBehavior.AllowGet);
        }
    }
}
