using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CommonModel;

namespace Web.Controllers.IntegratedService.CaseManagement
{
    public class IllegalManagementController : Controller
    {
        //
        // GET: /Illegal/

        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/WorkflowConfig/IllegalManagement/";
        //违法行为类别管理
        #region
        public ActionResult Index(string ID, string Type)
        {
            ViewBag.ID = "0";
            ViewBag.Type = "root";

            if (!string.IsNullOrWhiteSpace(ID))
            {
                ViewBag.ID = ID;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                ViewBag.Type = Type;
            }

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 获取违法行为类别树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetIllegalTree()
        {
            //获取大类数据
            List<ILLEGALCLASS> dlList = IllegalItemBLL
                .GetIllegalClassesByParentID(null).ToList();

            List<TreeModel> dlNodes = new List<TreeModel>();

            //遍历大类并查询下面所有的小类
            foreach (ILLEGALCLASS dlItem in dlList)
            {
                //获取小类数据
                List<ILLEGALCLASS> xlList = IllegalItemBLL
                .GetIllegalClassesByParentID(dlItem.ILLEGALCLASSID).ToList();

                List<TreeModel> xlNodes = new List<TreeModel>();

                //遍历小类并查询下面所有的子类
                foreach (ILLEGALCLASS xlItem in xlList)
                {
                    //获取子类数据
                    List<ILLEGALCLASS> zlList = IllegalItemBLL
                .GetIllegalClassesByParentID(xlItem.ILLEGALCLASSID).ToList();

                    List<TreeModel> zlNodes = new List<TreeModel>();
                    //遍历违法行为代码
                    foreach (ILLEGALCLASS zlItem in zlList)
                    {
                        //定义子类节点
                        TreeModel zlNode = new TreeModel()
                        {
                            name = zlItem.ILLEGALCODE + zlItem.ILLEGALCLASSNAME,
                            title = zlItem.ILLEGALCODE + zlItem.ILLEGALCLASSNAME,
                            value = zlItem.ILLEGALCLASSID.ToString(),
                            type = "zl",
                            open = false
                        };

                        zlNodes.Add(zlNode);
                    }

                    //定义小类节点
                    TreeModel xlNode = new TreeModel()
                    {
                        name = xlItem.ILLEGALCODE + xlItem.ILLEGALCLASSNAME,
                        title = xlItem.ILLEGALCODE + xlItem.ILLEGALCLASSNAME,
                        value = xlItem.ILLEGALCLASSID.ToString(),
                        type = "xl",
                        open = false,
                        children = zlNodes
                    };
                    xlNodes.Add(xlNode);
                }

                //定义大类节点
                TreeModel dlNode = new TreeModel()
                {
                    name = dlItem.ILLEGALCODE + dlItem.ILLEGALCLASSNAME,
                    title = dlItem.ILLEGALCODE + dlItem.ILLEGALCLASSNAME,
                    value = dlItem.ILLEGALCLASSID.ToString(),
                    type = "dl",
                    open = false,
                    children = xlNodes
                };
                dlNodes.Add(dlNode);
            }

            return Json(dlNodes, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 大小子类列表数据
        /// </summary>
        /// <returns></returns>
        public JsonResult IllegalClassList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            string ID = this.Request.QueryString["ID"];
            string Type = this.Request.QueryString["type"];
            decimal IllegalClassID = 0.0M;

            IQueryable<ILLEGALCLASS> IllegalClasses = null;

            //查询子类或者小类
            if (!string.IsNullOrWhiteSpace(ID) && ID != "0")
            {
                IllegalClassID = decimal.Parse(ID);

                //根据上一级类标识获取该类下的所有小类或者子类数据
                IllegalClasses = IllegalItemBLL
                .GetIllegalClassesByParentID(IllegalClassID);
            }
            else
            {
                //获取所有的大类数据
                IllegalClasses = IllegalItemBLL
                    .GetIllegalClassesByParentID(null);
            }

            var list = IllegalClasses.ToList()
              .Skip((int)iDisplayStart.Value)
              .Take((int)iDisplayLength.Value)
              .Select(t => new
              {
                  IllegalClassID = t.ILLEGALCLASSID,
                  Code = t.ILLEGALCODE,
                  Type = IllegalItemBLL
                  .GetIllegalTypeNameByIllegalTypeID((decimal)t.ILLEGALCLASSTYPEID),
                  Name = t.ILLEGALCLASSNAME
              });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = IllegalClasses.Count(),
                iTotalDisplayRecords = IllegalClasses.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加大小子类页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddIllegalClass()
        {
            //违法行为类别标识(1:添加大类;2:添加小类;3:添加子类)
            string illegalClassTypeID = this.Request.Form["IllegalClassTypeID"];
            string ID = this.Request.Form["ID"];
            string type = this.Request.Form["Type"];
            ViewBag.ID = ID;
            ViewBag.type = type;

            //获取所有的大类
            List<SelectListItem> dlList = IllegalItemBLL.GetIllegalClassesByParentID(null)
                .ToList().Select(c => new SelectListItem()
                {
                    Text = c.ILLEGALCLASSNAME,
                    Value = c.ILLEGALCLASSID.ToString()
                }).ToList();

            dlList.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "请选择大类"
            });

            //所属大类的绑定
            ViewBag.dlList = dlList;

            //添加大类
            if (illegalClassTypeID == "1")
            {
                return PartialView(THIS_VIEW_PATH + "AddIllegalDL.cshtml");
            }
            //添加小类
            else if (illegalClassTypeID == "2")
            {
                return PartialView(THIS_VIEW_PATH + "AddIllegalXL.cshtml");
            }
            //添加子类
            else
            {
                return PartialView(THIS_VIEW_PATH + "AddIllegalZL.cshtml");
            }
        }

        /// <summary>
        /// 添加违法行为类别大类
        /// </summary>
        /// <param name="illegalClass">大类</param>
        /// <returns></returns>
        public ActionResult CommitAddIllegalDL(ILLEGALCLASS illegalClass)
        {
            //获取一个新的违法行为标识
            decimal illegalClassID = IllegalItemBLL.GetNewIllegalClassID();

            ILLEGALCLASS IllegalClass = new ILLEGALCLASS()
            {
                ILLEGALCLASSID = illegalClassID,
                ILLEGALCLASSNAME = illegalClass.ILLEGALCLASSNAME,
                ILLEGALCLASSTYPEID = 1,
                ILLEGALCODE = illegalClass.ILLEGALCODE,
                PATH = "\\" + illegalClassID.ToString() + "\\"
            };

            IllegalItemBLL.AddIllegalClass(IllegalClass);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 添加违法行为类别小类
        /// </summary>
        /// <param name="illegalClass">小类</param>
        /// <returns></returns>
        public ActionResult CommitAddIllegalXL(ILLEGALCLASS illegalClass)
        {
            //获取一个新的违法行为标识
            decimal illegalClassID = IllegalItemBLL.GetNewIllegalClassID();

            //获取所属大类
            ILLEGALCLASS DL = IllegalItemBLL
                .GetIllegalClassByID((decimal)illegalClass.PARENTID);

            ILLEGALCLASS IllegalClass = new ILLEGALCLASS()
            {
                ILLEGALCLASSID = illegalClassID,
                ILLEGALCLASSNAME = illegalClass.ILLEGALCLASSNAME,
                ILLEGALCLASSTYPEID = 2,
                ILLEGALCODE = illegalClass.ILLEGALCODE,
                PARENTID = illegalClass.PARENTID,
                PATH = DL.PATH + illegalClassID.ToString() + "\\"
            };

            IllegalItemBLL.AddIllegalClass(IllegalClass);

            return RedirectToAction("Index", new
            {
                ID = illegalClass.PARENTID,
                Type = "dl"
            });
        }

        /// <summary>
        /// 添加违法行为类别子类
        /// </summary>
        /// <param name="illegalClass">子类</param>
        /// <returns></returns>
        public ActionResult CommitAddIllegalZL(ILLEGALCLASS illegalClass)
        {
            //获取一个新的类标识
            decimal illegalClassID = IllegalItemBLL.GetNewIllegalClassID();

            //获取所属小类
            ILLEGALCLASS XL = IllegalItemBLL
                .GetIllegalClassByID((decimal)illegalClass.PARENTID);

            ILLEGALCLASS IllegalClass = new ILLEGALCLASS()
            {
                ILLEGALCLASSID = illegalClassID,
                ILLEGALCLASSNAME = illegalClass.ILLEGALCLASSNAME,
                ILLEGALCLASSTYPEID = 3,
                ILLEGALCODE = illegalClass.ILLEGALCODE,
                PARENTID = illegalClass.PARENTID,
                PATH = XL.PATH + illegalClassID.ToString() + "\\"
            };

            IllegalItemBLL.AddIllegalClass(IllegalClass);

            return RedirectToAction("Index", new
            {
                ID = illegalClass.PARENTID,
                Type = "xl"
            });
        }

        /// <summary>
        /// 大小子类级联
        /// </summary>
        /// <returns></returns>
        public JsonResult GetIllegalClasses()
        {
            string strIllegaClassID = this.Request.QueryString["IllegaClassID"];
            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegaClassID, out illegaClassID))
            {
                return null;
            }

            IQueryable<ILLEGALCLASS> illegalClasses = IllegalItemBLL
                .GetIllegalClassesByParentID(illegaClassID);

            return Json(illegalClasses.Select(t => new
            {
                ID = t.ILLEGALCLASSID,
                Name = t.ILLEGALCLASSNAME
            }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 编辑违法行为类别页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditIllegalClass()
        {
            string ID = this.Request.Form["ID"];
            string type = this.Request.Form["Type"];
            ViewBag.ID = ID;
            ViewBag.type = type;

            string strIllegalClassID = this.Request.Form["illegalClassID"];
            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegalClassID, out illegaClassID))
            {
                return null;
            }

            ILLEGALCLASS illegalClass = IllegalItemBLL
                .GetIllegalClassByID(illegaClassID);

            if (illegalClass.ILLEGALCLASSTYPEID == 1)
            {
                return PartialView(THIS_VIEW_PATH +
                    "EditIllegalDL.cshtml", illegalClass);
            }
            else if (illegalClass.ILLEGALCLASSTYPEID == 2)
            {
                //所属大类
                ILLEGALCLASS IllegalDL = IllegalItemBLL
                    .GetParentClassByParentID(illegalClass.PARENTID.Value);
                ViewBag.ssdl = IllegalDL.ILLEGALCLASSNAME;

                return PartialView(THIS_VIEW_PATH +
                    "EditIllegalXL.cshtml", illegalClass);
            }
            else
            {
                //所属小类
                ILLEGALCLASS IllegalXL = IllegalItemBLL
                    .GetParentClassByParentID(illegalClass.PARENTID.Value);
                ViewBag.ssxl = IllegalXL.ILLEGALCLASSNAME;

                //所属大类
                ILLEGALCLASS IllegalDL = IllegalItemBLL
                    .GetParentClassByParentID(IllegalXL.PARENTID.Value);
                ViewBag.ssdl = IllegalDL.ILLEGALCLASSNAME;

                return PartialView(THIS_VIEW_PATH + "EditIllegalZL.cshtml",
                    illegalClass);
            }

        }

        /// <summary>
        /// 修改大类
        /// </summary>
        /// <param name="illegalClass">大类</param>
        /// <returns></returns>
        public ActionResult CommitEditIllegalDL(ILLEGALCLASS illegalClass)
        {
            //获取大类标识
            string strIllegalClassID = this.Request.Form["illegalClassID"];

            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegalClassID, out illegaClassID))
            {
                return null;
            }

            ILLEGALCLASS IllegalClass = new ILLEGALCLASS()
            {
                ILLEGALCLASSID = illegaClassID,
                ILLEGALCODE = illegalClass.ILLEGALCODE,
                ILLEGALCLASSNAME = illegalClass.ILLEGALCLASSNAME
            };

            IllegalItemBLL.EditIllegalClass(IllegalClass);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 修改小类
        /// </summary>
        /// <param name="illegalClass">小类</param>
        /// <returns></returns>
        public ActionResult CommitEditIllegalXL(ILLEGALCLASS illegalClass)
        {
            //获取小类标识
            string strIllegalClassID = this.Request.Form["illegalClassID"];
            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegalClassID, out illegaClassID))
            {
                return null;
            }

            //获取大类标识
            decimal partentID = (decimal)IllegalItemBLL
                .GetIllegalClassByID(illegaClassID).PARENTID;

            ILLEGALCLASS IllegalClass = new ILLEGALCLASS()
            {
                ILLEGALCLASSID = illegaClassID,
                ILLEGALCODE = illegalClass.ILLEGALCODE,
                ILLEGALCLASSNAME = illegalClass.ILLEGALCLASSNAME
            };

            IllegalItemBLL.EditIllegalClass(IllegalClass);

            return RedirectToAction("Index", new { ID = partentID, Type = "dl" });
        }

        /// <summary>
        /// 修改子类
        /// </summary>
        /// <param name="illegalClass">子类</param>
        /// <returns></returns>
        public ActionResult CommitEditIllegalZL(ILLEGALCLASS illegalClass)
        {
            //获取子类标识
            string strIllegalClassID = this.Request.Form["illegalClassID"];

            decimal illegaClassID = 0.0M;

            if (!decimal.TryParse(strIllegalClassID, out illegaClassID))
            {
                return null;
            }

            //获取小类标识
            decimal partentID = (decimal)IllegalItemBLL
                .GetIllegalClassByID(illegaClassID).PARENTID;

            ILLEGALCLASS IllegalClass = new ILLEGALCLASS()
            {
                ILLEGALCLASSID = illegaClassID,
                ILLEGALCODE = illegalClass.ILLEGALCODE,
                ILLEGALCLASSNAME = illegalClass.ILLEGALCLASSNAME
            };

            IllegalItemBLL.EditIllegalClass(IllegalClass);

            return RedirectToAction("Index", new { ID = partentID, Type = "xl" });
        }

        /// <summary>
        /// 判断大小子类是否能直接删除，
        /// 如果是大类或者是小类判断小类和子类数量
        /// 如果是子类判断违法事项数量
        /// </summary>
        /// <param name="ID">大小子类标识</param>
        /// <returns></returns>
        public decimal GetIllegalClassInfomation(string ID)
        {
            decimal illegalClassID = 0.0M;

            if (!string.IsNullOrWhiteSpace(ID))
            {
                illegalClassID = decimal.Parse(ID);
            }

            //获取所有的子类
            IQueryable<ILLEGALCLASS> illegalClasses = IllegalItemBLL
                 .GetChildrenClassByClassID(illegalClassID);

            //获取当前类
            ILLEGALCLASS illegalClass = IllegalItemBLL
                .GetIllegalClassByID(illegalClassID);

            if (illegalClass.ILLEGALCLASSTYPEID == 3)
            {
                //小类下的所有违法行为
                IQueryable<ILLEGALITEM> illegalItems = IllegalItemBLL
                    .GetIllegalItemByClassID(illegalClassID);

                return illegalItems.Count();
            }

            return illegalClasses.Count();
        }

        /// <summary>
        /// 删除大小子类
        /// </summary>
        public ActionResult DeleteIllegalClass()
        {

            string ID = this.Request.QueryString["ID"];
            string Type = this.Request.QueryString["Type"];
            string strIllegalClassID = this.Request
                .QueryString["IllegalClassID"];
            decimal illegalClassID = 0.0M;

            if (!string.IsNullOrWhiteSpace(strIllegalClassID)
                && decimal.TryParse(strIllegalClassID, out illegalClassID))
            {
                IllegalItemBLL.DeleteIllegalClass(illegalClassID);
            }

            return RedirectToAction("Index", new { ID = ID, Type = Type });
        }
        #endregion

        //违法行为事项管理
        #region
        public ActionResult IllegalItem(string ID, string Type)
        {
            ViewBag.ID = "0";
            ViewBag.Type = "root";

            if (!string.IsNullOrWhiteSpace(ID))
            {
                ViewBag.ID = ID;
            }

            if (!string.IsNullOrWhiteSpace(Type))
            {
                ViewBag.Type = Type;
            }

            return View(THIS_VIEW_PATH + "IllegalItem.cshtml");
        }

        /// <summary>
        /// 获取违法事项列表
        /// </summary>
        /// <returns></returns>
        public JsonResult IllegalItemList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            string ID = this.Request.QueryString["ID"];
            string Type = this.Request.QueryString["type"];
            decimal illegalClassID = 0.0M;

            if (!string.IsNullOrWhiteSpace(ID))
            {
                illegalClassID = decimal.Parse(ID);
            }

            IQueryable<ILLEGALITEM> illegalItemList = null;

            //获取所有的违法事项
            if (!string.IsNullOrWhiteSpace(Type) && Type == "root")
            {
                illegalItemList = IllegalItemBLL.GetAllIllegalClassItem();
            }
            else
            {
                illegalItemList = IllegalItemBLL
                    .GetTotalIllegalClassItemByIllegalClassID(illegalClassID);
            }

            var list = illegalItemList
           .Skip((int)iDisplayStart.Value)
           .Take((int)iDisplayLength.Value)
           .Select(t => new
           {
               IllegalItemID = t.ILLEGALITEMID,
               Code = t.ILLEGALCODE,
               //Type = "违法行为",
               Name = t.ILLEGALITEMNAME
           });

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = illegalItemList.Count(),
                iTotalDisplayRecords = illegalItemList.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加违法行为事项页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddIllegalItem()
        {
            string ID = this.Request.Form["ID"];
            string type = this.Request.Form["Type"];
            ViewBag.ID = ID;
            ViewBag.type = type;

            //获取所有的大类
            List<SelectListItem> dlList = IllegalItemBLL
                .GetIllegalClassesByParentID(null)
                .ToList().Select(c => new SelectListItem()
                {
                    Text = c.ILLEGALCLASSNAME,
                    Value = c.ILLEGALCLASSID.ToString()
                }).ToList();

            dlList.Insert(0, new SelectListItem()
            {
                Value = "0",
                Text = "请选择大类"
            });

            //所属大类的绑定
            ViewBag.dlList = dlList;

            return PartialView(THIS_VIEW_PATH + "AddIllegalItem.cshtml");
        }

        /// <summary>
        /// 添加违法事项
        /// </summary>
        /// <param name="illegalItem">违法事项</param>
        /// <returns></returns>
        public ActionResult CommitAddIllegalItem(ILLEGALITEM illegalItem)
        {
            ILLEGALITEM IllegalItem = new ILLEGALITEM()
            {
                ILLEGALCLASSID = illegalItem.ILLEGALCLASSID,
                ILLEGALITEMNAME = illegalItem.ILLEGALITEMNAME,
                ILLEGALCODE = illegalItem.ILLEGALCODE,
                WEIZE = illegalItem.WEIZE,
                FZZE = illegalItem.FZZE,
                PENALTYCONTENT = illegalItem.PENALTYCONTENT
            };

            IllegalItemBLL.AddIllegalItem(IllegalItem);

            return RedirectToAction("IllegalItem", new
            {
                ID = illegalItem.ILLEGALCLASSID,
                Type = "zl"
            });
        }

        /// <summary>
        /// 编辑违法事项页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditIllegalItem()
        {
            string ID = this.Request.Form["ID"];
            string type = this.Request.Form["Type"];
            ViewBag.ID = ID;
            ViewBag.type = type;

            string strIllegalItemID = this.Request.Form["illegalItemID"];
            decimal illegalItemID = 0.0M;

            if (!decimal.TryParse(strIllegalItemID, out illegalItemID))
            {
                return null;
            }

            ILLEGALITEM illegalItem = IllegalItemBLL
                .GetIllegalItemByIllegalItemID(illegalItemID);
            //所属子类
            ILLEGALCLASS sszl = IllegalItemBLL
                .GetIllegalClassByID(illegalItem.ILLEGALCLASSID.Value);
            ViewBag.sszlName = sszl.ILLEGALCLASSNAME;

            //所属小类
            ILLEGALCLASS ssxl = IllegalItemBLL
                .GetParentClassByParentID(sszl.PARENTID.Value);
            ViewBag.ssxlName = ssxl.ILLEGALCLASSNAME;

            //所属大类
            ILLEGALCLASS ssdl = IllegalItemBLL
                .GetParentClassByParentID(ssxl.PARENTID.Value);
            ViewBag.ssdlName = ssdl.ILLEGALCLASSNAME;

            return PartialView(THIS_VIEW_PATH + "EditIllegalItem.cshtml",
                illegalItem);
        }

        /// <summary>
        /// 编辑违法事项
        /// </summary>
        /// <param name="illegalItem">违法事项</param>
        /// <returns></returns>
        public ActionResult CommitEditIllegalItem(ILLEGALITEM illegalItem)
        {
            string strIllegalItemID = this.Request.Form["illegalItemID"];
            string strIllegalClassID = this.Request.Form["illegalClassID"];
            decimal illegalItemID = 0.0M;
            decimal illegalClassID = 0.0M;

            if (!string.IsNullOrWhiteSpace(strIllegalItemID))
            {
                illegalItemID = decimal.Parse(strIllegalItemID);
            }

            if (!string.IsNullOrWhiteSpace(strIllegalClassID))
            {
                illegalClassID = decimal.Parse(strIllegalClassID);
            }

            ILLEGALITEM IllegalItem = new ILLEGALITEM()
            {
                ILLEGALITEMID = illegalItemID,
                ILLEGALCODE = illegalItem.ILLEGALCODE,
                ILLEGALITEMNAME = illegalItem.ILLEGALITEMNAME,
                WEIZE = illegalItem.WEIZE,
                FZZE = illegalItem.FZZE,
                PENALTYCONTENT = illegalItem.PENALTYCONTENT
            };

            IllegalItemBLL.EdidIllegalItem(IllegalItem);

            return RedirectToAction("IllegalItem", new
            {
                ID = illegalClassID,
                Type = "zl"
            });
        }

        /// <summary>
        /// 删除违法事项
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteIllegalItem()
        {
            string ID = this.Request.QueryString["ID"];
            string Type = this.Request.QueryString["Type"];
            string strIllegalItemID = this.Request.QueryString["IllegalItemID"];

            decimal illegalitemID = 0.0M;

            if (!string.IsNullOrWhiteSpace(strIllegalItemID)
                && decimal.TryParse(strIllegalItemID, out illegalitemID))
            {
                IllegalItemBLL.DeleteIllegalItem(illegalitemID);
            }

            return RedirectToAction("IllegalItem", new { ID = ID, Type = Type });
        }

        #endregion
    }
}
