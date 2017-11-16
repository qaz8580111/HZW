using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Model;

namespace ZGM.Web.Controllers.QWGL
{
    public class TeamTaskController : Controller
    {
        //
        // GET: /TeamTask/


        /// <summary>
        /// 根据中队获取分队
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserByDeptName()
        {
            string BCLASSID = this.Request.QueryString["BCLASSID"];
            decimal questionDLID = 0.0M;

            if (!string.IsNullOrWhiteSpace(BCLASSID)
                && decimal.TryParse(BCLASSID, out questionDLID))
            {
                IQueryable<SYS_UNITS> results = UnitBLL.IQuerableGetUnitByDeptID(questionDLID);
                var list = from result in results
                           select new
                           {
                               Value = result.UNITID,
                               Text = result.UNITNAME
                           };
                return Json(list, JsonRequestBehavior.AllowGet);
            }

            return null;
        }
       
    }
}
