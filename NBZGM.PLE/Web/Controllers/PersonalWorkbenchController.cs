using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.WorkFlowBLLs;
//using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.Model.ZFSJModels;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Model.SimpleCaseModels;
using Taizhou.PLE.BLL.RecipeBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL.LeaderWeekWorkPlanBLLs;
using Taizhou.PLE.BLL.DBHelper;
using Taizhou.PLE.Model.GSGGModels;
using Taizhou.PLE.BLL.ZFRYBLL;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.UserBLLs;

namespace Web.Controllers.PersonalCentre
{
    public class PersonalWorkbenchController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/PersonalCentre/PersonalWorkbench/";

        public ActionResult Index()
        {   
            //执法人员数量
            ViewBag.ZFRYCount = ZFDYCount();
            //执法事件数量
            ViewBag.zfsjListCount = zfsjListCount();
            //行政审批数量
            //ViewBag.xzspListCount = XZSPListCount();
            //一般案件数量
            ViewBag.caseListCount = CaseListCount();
            //简易案件数量
            ViewBag.simpCaseCount = simpCaseCount();
            //违停数量
            //ViewBag.CaseParkCount = CaseParkCount();
            ViewBag.UserID = SessionManager.User.UserID;
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            ViewBag.WorkCount = WorkflowBLL.GetWorkList(dt);
            USER user = UserBLL.GetAllUsers().First(t => t.USERID == SessionManager.User.UserID);
            ViewBag.ACCOUNT = user.ACCOUNT;
            ViewBag.PASSWORD = user.PASSWORD;
            return View(THIS_VIEW_PATH + "Index2.cshtml");
        }

        public ActionResult IndexTop()
        {
            return View(THIS_VIEW_PATH + "IndexTop.cshtml");
        }

        ///// <summary>
        ///// 获取行政审批待处理列表
        ///// </summary>
        ///// <returns></returns>
        public ActionResult GetXZSPPendList()
        {
            IQueryable<XZSPPendingTask> xzspPendingTask = ActivityInstanceBLL
                .GetPendActivityList(SessionManager.User);
            //if (xzspPendingTask.Count() >= 8)
            {
                xzspPendingTask = xzspPendingTask.OrderByDescending(t => t.CreateTime).Skip(0).Take(8);
            }
            return View(THIS_VIEW_PATH + "GetXZSPPendList.cshtml", xzspPendingTask);
        }

        public ActionResult GetCasePendList()
        {
            IQueryable<PendingTask> inactiveCases = WorkflowBLL
                .GetPendingTasks(SessionManager.User)
                .OrderByDescending(t => t.DeliveryTime);

            if (inactiveCases.Count() >= 8)
            {
                inactiveCases = inactiveCases
                    .OrderByDescending(t => t.DeliveryTime).Skip(0).Take(8);
            }

            return View(THIS_VIEW_PATH + "GetCasePendList.cshtml", inactiveCases);
        }
        /// <summary>
        /// 获取执法事件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetZFSJPendList()
        {
            List<ZFSJPendingTask> list = ZFSJActivityInstanceBLL
               .GetZFSJACTIVITYINSTANCE(SessionManager.User)
               .OrderByDescending(t => t.CreateTime).ToList();
            foreach (var item in list)
            {
                item.EventCode = ZFSJActivityInstanceBLL.GetEventCodeByWIID(item.WIID);
                item.EventTitle = ZFSJActivityInstanceBLL.GetEventTitleByWIID(item.WIID);
            }
            int count = list.Count;
            if (list.Count() >= 8)
            {
                list = list
                    .OrderByDescending(t => t.CreateTime).Skip(0).Take(8).ToList();
            }

            return View(THIS_VIEW_PATH + "GetZFSJPendList.cshtml", list);
        }

        /// <summary>
        ///获取公共服务列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGGFWPendList()
        {
            List<GGFWEVENT> list = GGFWEventBLL
            .GetGGFWEventsByUserID(SessionManager.User.UserID)
            .OrderByDescending(t => t.CREATETIME).ToList();
            if (list.Count() >= 8)
            {
                list = list
                    .OrderByDescending(t => t.CREATETIME).Skip(0).Take(8).ToList();
            }
            return View(THIS_VIEW_PATH + "GetGGFWPendList.cshtml", list);
        }

        /// <summary>
        /// 获取简易事件列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetJYSJPendList()
        {
            List<SimpleCase> jysjList = SimpleCaseBLL
                .GetSimpleCases()
                .OrderByDescending(t => t.CaseTime).ToList();
            int count = jysjList.Count;
            if (jysjList.Count() >= 8)
            {
                jysjList = jysjList
                    .OrderByDescending(t => t.CaseTime).Skip(0).Take(8).ToList();
            }
            return View(THIS_VIEW_PATH + "GetJYSJPendList.cshtml", jysjList);
        }
        /// <summary>
        /// 获得菜谱列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetRECIPEPendList()
        {
            List<RECIPE> recipeList = RecipeBLLs
                .GetAllRecipe()
                .OrderByDescending(t => t.CREATEDTIME).ToList();
            int count = recipeList.Count;
            if (recipeList.Count() >= 8)
            {
                recipeList = recipeList
                    .OrderByDescending(t => t.CREATEDTIME).Skip(0).Take(8).ToList();
            }
            return View(THIS_VIEW_PATH + "GetRECIPEPendList.cshtml", recipeList);
        }
        /// <summary>
        /// 获得领导值班列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLeadPendList()
        {
            List<LeaderWeekWorkPlanModel> leadList = LeaderWeekWorkPlanBLL
                .GetLeaderWeekWokrPlanList()
                .OrderByDescending(t => t.STARTDATE).ToList();
            int count = leadList.Count;
            if (leadList.Count() >= 8)
            {
                leadList = leadList
                    .OrderByDescending(t => t.STARTDATE).Skip(0).Take(8).ToList();
            }
            return View(THIS_VIEW_PATH + "GetLeadPendList.cshtml", leadList);
        }
        /// <summary>
        /// 获取执法事件  一般案件 行政审批
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetChart()
        {
            string data = "";
            string data1 = "";
            string data2 = "";
            string data3 = "";
            int length = DateTime.Now.Day;
            List<WORKFLOWINSTANCE> list = WorkflowBLL.GetWORKFLOWINSTANCEByMum();
            List<Daycount> listy = (from w in list
                                    group w by w.CREATEDTIME.Value.Day into g
                                    select new Daycount { index = g.Key, count = g.Count() }).ToList();
            int[] Arrey = new int[length];
            for (int i = 0; i < listy.Count(); i++)
            {
                Arrey[listy[i].index - 1] = listy[i].count;
            }

            for (int i = 1; i <= Arrey.Length; i++)
            {
                if (i == Arrey.Length)
                {
                    data += ("[" + i + "," + Arrey[i - 1] + "]");
                }
                else
                {
                    data += ("[" + i + "," + Arrey[i - 1] + "]" + ",");
                }
            }

            List<ZFSJWORKFLOWINSTANCE> zfsjList = ZFSJWorkflowInstanceBLL.GetZFSJWORKFLOWINSTANCEByMum();
            List<Daycount> listz = (from w in zfsjList
                                    group w by w.CREATETIME.Value.Day into g
                                    select new Daycount { index = g.Key, count = g.Count() }).ToList();
            int[] Arrzy = new int[length]; //获取当月的天数(从一号到当天的天数)
            for (int i = 0; i < listz.Count(); i++)
            {
                Arrzy[listz[i].index - 1] = listz[i].count; //因为Arrzy数组的索引是0，listz[i].index 索引是1  所以Arrzy索引第一个 要listz[i].index-1
            }

            for (int i = 1; i <= Arrzy.Length; i++)
            {
                if (i == Arrzy.Length)
                {
                    data1 += ("[" + i + "," + Arrzy[i - 1] + "]");
                }
                else
                {
                    data1 += ("[" + i + "," + Arrzy[i - 1] + "]" + ",");
                }
            }

            //List<XZSPWFIST> xzspList = WorkflowInstanceBLL.GetXZSPWFISTByMum();
            //List<Daycount> listx = (from w in xzspList
            //                        group w by w.CREATEDTIME.Value.Day into g
            //                        select new Daycount { index = g.Key, count = g.Count() }).ToList();
            //int[] Arrxy = new int[length];
            //for (int i = 1; i < listx.Count(); i++)
            //{
            //    Arrxy[listx[i].index - 1] = listx[i].count;
            //}
            //for (int i = 1; i <= Arrzy.Length; i++)
            //{
            //    if (i == Arrxy.Length)
            //    {
            //        data2 += ("[" + i + "," + Arrxy[i - 1] + "]");
            //    }
            //    else
            //    {
            //        data2 += ("[" + i + "," + Arrxy[i - 1] + "]" + ",");
            //    }
            //}

            List<SIMPLECAS> CaseList = SimpleCaseBLL.GetSIMPLECASByMum();
            List<Daycount> listc = (from w in CaseList
                                    group w by w.CASETIME.Value.Day into g
                                    select new Daycount { index = g.Key, count = g.Count() }).ToList();
            int[] Arrcy = new int[length];
            for (int i = 1; i < listc.Count(); i++)
            {
                Arrcy[listc[i].index - 1] = listc[i].count;
            }
            for (int i = 1; i <= Arrzy.Length; i++)
            {
                if (i == Arrcy.Length)
                {
                    data3 += ("[" + i + "," + Arrcy[i - 1] + "]");
                }
                else
                {
                    data3 += ("[" + i + "," + Arrcy[i - 1] + "]" + ",");
                }
            }

            //string returnData = data + "@" + data1 + "@" + data2;
            string returnData = data + "@" + data1 + "@" + data2 + "@" + data3;
            return returnData;
        }
        public class Daycount
        {
            public int index { get; set; }
            public int count { get; set; }
        }
        /// <summary>
        /// 获得公示公告内容
        /// </summary>
        public ActionResult GetGSGGList()
        {
            List<GSGGPendModels> GGList = DBHelper.GetGSGGList();
            return View(THIS_VIEW_PATH + "GetGSGGPendList.cshtml", GGList);
        }

        /// <summary>
        /// 获取执法队员数量
        /// </summary>
        /// <returns></returns>
        public static int ZFDYCount()
        {
            int A = ZFRYBLL.GetZFGKUSERLATESTPOSITIONSByMum();
            return A;
        }
        /// <summary>
        /// 获取一般案件数量
        /// </summary>
        /// <returns></returns>
        public static int CaseListCount()
        {
            int A = WorkflowBLL.CaseListCount();
            return A;
        }
        ///// <summary>
        ///// 获得行政审批的数目
        ///// </summary>
        ///// <returns></returns>
        //public static int XZSPListCount()
        //{
        //    int A = WorkflowInstanceBLL
        //        .XZSPListCount();
        //    return A;
        //}
        /// <summary>
        /// 获得执法事件的数目
        /// </summary>
        /// <returns></returns>
        public static int zfsjListCount()
        {
            int A = ZFSJWorkflowInstanceBLL
                .zfsjListCount();
            return A;
        }
        /// <summary>
        /// 获取简易事件的条数
        /// </summary>
        /// <returns></returns>
        public static int simpCaseCount()
        {
            int A = SimpleCaseBLL
                .simpCaseCount();
            return A;
        }
        /// <summary>
        /// 获得违停案件的条数
        /// </summary>
        /// <returns></returns>
        public static int CaseParkCount()
        {
            int A = ParkingCaseBLL
                .CaseParkCount();
            return A;
        }
    }
}
