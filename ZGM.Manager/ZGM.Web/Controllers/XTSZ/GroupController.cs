using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.GroupBLL;
using ZGM.Model;

namespace ZGM.Web.Controllers.XTSZ
{
    public class GroupController : Controller
    {
        private GroupBLL bll = new GroupBLL();
        //
        // GET: /Group/

        public ActionResult Index()
        {
            return View();
        }

        public int Add()
        {
            string name = Request["name"];
            string sepon = Request["sepon"];
            SYS_GROUP model = new SYS_GROUP();
            model.NAME = name;
            model.SEPON = decimal.Parse(sepon);
            return bll.AddGroup(model);
        }


        public int Edit()
        {
            string id = Request["id"];
            string name = Request["name"];
            string sepon = Request["sepon"];
            SYS_GROUP model = new SYS_GROUP();
            model.ID = decimal.Parse(id);
            model.NAME = name;
            model.SEPON = decimal.Parse(sepon);
            return bll.EditGroup(model);
        }


        /// <summary>
        /// 获取用户列表并且进行分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsers(int? iDisplayStart, int? iDisplayLength, int? secho)
        {


            IEnumerable<SYS_GROUP> sglist = bll.GetMajorProjectsLists();
            var list = sglist
                      .Skip((int)iDisplayStart.Value)
                      .Take((int)iDisplayLength.Value)
                      .Select(t => new
                      {
                          ID = t.ID,
                          NAME = t.NAME,
                          SEPON = t.SEPON,
                      })
                      .OrderBy(a => a.SEPON);


            return Json(new
            {
                sEcho = secho,
                iTotalRecords = sglist.Count(),
                iTotalDisplayRecords = sglist.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 根据详情
        /// </summary>
        /// <returns></returns>
        public JsonResult GetGroupByID()
        {
            string id = Request.QueryString["id"];
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            SYS_GROUP schedule = bll.GetGroup(Convert.ToDecimal(id));
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        public int Delete()
        {
            decimal id = decimal.Parse(Request["id"]);
            return bll.DeleteGroup(id);


        }

    }
}
