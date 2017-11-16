using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Workflows;
using Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using System.Configuration;
using System.IO;
namespace Web.Controllers.IntegratedService.DocConfig
{
    public class DocConfigController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/WorkflowConfig/DocConfig/";

        /// <summary>
        /// 返回文书所有文书
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.acList = DocBLL.GetAllActivityinstance()
                .Select(t => new SelectListItem()
                {
                    Text = t.ADNAME,
                    Value = t.ADID.ToString()
                });
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 获取活动树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDocPhas()
        {
            List<TreeModel> docTrees = DocBLL.GetDocPhas();
            return Json(docTrees, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 跟活动标识返回文书
        /// </summary>
        /// <param name="ADID"></param>
        /// <returns></returns>
        public JsonResult GetDoc(int? iDisplayStart
           , int? iDisplayLength, int? secho, decimal ADID)
        {
            var doc = DocBLL.GetDoc(ADID);
            var doclist = doc
                .Skip((int)iDisplayStart.Value)
                .Take((int)iDisplayLength.Value)
                .Select(t => new
                {
                    DDNAME = t.DDNAME,
                    DDID = t.DDID,
                    ADNAME = t.ADNAME,
                    ADID = t.ADID,
                    ISREQUIRED = t.ISREQUIRED
                });
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = doc.Count(),
                iTotalDisplayRecords = doc.Count(),
                aaData = doclist
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据活动标识返回不属于该活动下的文书
        /// </summary>
        /// <param name="ADID">活动标识</param>
        /// <returns></returns>
        public JsonResult GetDocList(decimal ADID)
        {
            var doclist = DocBLL.GetDocList(ADID).Select(t => new
            {
                DDNAME = t.DDNAME,
                DDID = t.DDID
            });
            return Json(doclist, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据活动标识和文书标识删除文书活动配置
        /// </summary>
        /// <param name="DDID">文书标识</param>
        /// <param name="ADID">活动标识</param>
        public void DeleteDOC(decimal? DDID, decimal? ADID)
        {
            DocBLL.DeleteDOC(DDID, ADID);
        }

        /// <summary>
        /// 添加活动文书配置
        /// </summary>
        /// <param name="ADID">活动标识</param>
        /// <param name="DDID">文书标识</param>
        /// <param name="isRequired">是否必填</param>
        [HttpPost]
        public void AddDoc(decimal ADID, decimal DDID, decimal isRequired)
        {
            DocBLL.AddDoc(ADID, DDID, isRequired);
        }

        /// <summary>
        /// 修改活动文书配置
        /// </summary>
        /// <param name="ADID">活动标识</param>
        /// <param name="DDID">文书标识</param>
        /// <param name="isRequired">是否必填</param>
        public void EditDoc(decimal ADID, decimal DDID, decimal isRequired)
        {
            DocBLL.EditDoc(ADID, DDID, isRequired);
        }
        /// <summary>
        /// 添加活动文书配置
        /// </summary>
        ///<param name="ADID">活动标识</param>
        /// <param name="DDID">文书标识</param>
        /// <returns></returns>
        public JsonResult GetDOCByADIDAndDDID(decimal ADID, decimal DDID)
        {
            DOCActivity da = DocBLL.GetDOCByADIDAndDDID(ADID, DDID);
            if (da != null)
            {
                return Json(da, JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}
