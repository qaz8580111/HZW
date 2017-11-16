using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.BLL.StreeStoreBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Common.Enums.XZSPEnums;
using Taizhou.PLE.Common.Enums.ZFSJEnums;
using Web.Process.ZFSJProcess;
using Web.ViewModels;
using Taizhou.PLE.Model;


namespace Web.Controllers.SysManagement.BasicManagement
{
    public class StreeStoreController : Controller
    {
        //
        // GET: /StreeStore/
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/StreeStore/";

        public ActionResult Index()
        {
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult GetJsonDaLeiAll()
        {
           
            var stree = StreeStoreTypeBLL.GetSTREESTORETYPESByParentID().ToList();
            return Json(stree, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJsonZhongLeiAll()
        {
            string parentID = Request.QueryString["DaLei"];
            IList<STREESTORETYPE> liststree = StreeStoreTypeBLL.GetSTREESTORETYPESByParentID(parentID).ToList();
            var list = from s in liststree
                       select new
                       {
                           STREESTORETYPEID = s.STREESTORETYPEID,
                           TYPENAME = s.TYPENAME
                       };
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJsonXiaoLeiAll()
        {

            string parentID = Request.QueryString["ZhongLei"];
            IList<STREESTORETYPE> liststree = StreeStoreTypeBLL.GetSTREESTORETYPESByParentID(parentID).ToList();
            var list = from s in liststree
                       select new
           {
               STREESTORETYPEID = s.STREESTORETYPEID,
               TYPENAME = s.TYPENAME
           };
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public ActionResult YJDJProcess()
        {
            string type = Request["type"];
            ViewBag.type = type;
            return View(THIS_VIEW_PATH + "YJDJProcess.cshtml");
        }

        /// <summary>
        /// 沿街店家信息列表
        /// </summary>
        public JsonResult YJDJList(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            string StoreName = Request.QueryString["StoreName"];
            string Adress = Request.QueryString["Adress"];
            string Operating = "<div>";

            Operating += @"
<a class='btn btn-primary btn-small' 
href='/StreeStore/YJDJProcess?STREESTOREID={0}&&type=2' 
title='查看店家信息'><i class='icon-search padding-null'></i></a>";

            Operating += @"
<a class='btn btn-primary btn-small' 
href='/StreeStore/YJDJProcess?STREESTOREID={0}&&type=1' 
title='修改店家信息'><i class='icon-edit padding-null'></i></a>
";
            Operating += @"
<a class='btn btn-danger btn-small' href='#' 
title='删除店家信息' onclick='DeleteStree({0})'>
<i class='icon-trash padding-null'></i></a>
";

            Operating += "</div>";
            List<STREESTORE> instances = StreeStoreBLL.GetSTREESTORES().ToList();
            List<STREESTORE> pendingTasklist = instances;
            if (!string.IsNullOrEmpty(StoreName))
            {
                pendingTasklist = pendingTasklist.Where(a => a.SHOPNAME.Contains(StoreName)).ToList();
            }
            if (!string.IsNullOrEmpty(Adress))
            {
                pendingTasklist = pendingTasklist.Where(a => a.ADDRESS.Contains(Adress)).ToList();
            }

            int count = pendingTasklist.Count();

            pendingTasklist = pendingTasklist
               .Skip((int)iDisplayStart)
               .Take((int)iDisplayLength).ToList();

            int? seqno = iDisplayStart + 1;

            var list = from t in pendingTasklist
                       select new
                       {
                           SEQNO = seqno++,
                           STREESTOREID = t.STREESTOREID,
                           ADDRESS = t.ADDRESS,
                           SHOPNAME = t.SHOPNAME,
                           SHOPUSERNAME = t.SHOPUSERNAME,
                           STREESTORETYPEID = StreeStoreTypeBLL.GetSTREESTORETYPESName(t.STREESTORETYPEID),
                           SHOPPHONE = t.SHOPPHONE,
                           ISMTZP = t.ISMTZP,
                           ISGSWSXKZ = t.ISGSWSXKZ,
                           ISPSXKZ = t.ISPSXKZ,
                           ISHJPL = t.ISHJPL,
                           PICTUREURLS = t.PICTUREURLS,
                           GEOMETRY = t.GEOMETRY,
                           operating = string.Format(Operating, t.STREESTOREID)
                       };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = list
            }, JsonRequestBehavior.AllowGet);

        }

     

        public ActionResult Add()
        {
            VMStreeStore model = new VMStreeStore();
            return View(THIS_VIEW_PATH + "Add.cshtml");
        }

        //
        // POST: /StreeStore/Create

        [HttpPost]
        public ActionResult Add(VMStreeStore VMStreeStore)
        {
            string PicturePath = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (files != null && files.Count > 0)
            {
                DateTime dt = DateTime.Now;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    if (file != null)
                    {
                        //文件类型
                        string fileType = file.ContentType;

                        //上传的是图片
                        if (fileType.Equals("image/x-png") || fileType.Equals("image/png")
                            || fileType.Equals("image/GIF") || fileType.Equals("image/peg")
                            || fileType.Equals("image/jpeg"))
                        {
                            string originalPath = Path.Combine(ConfigurationManager.AppSettings["YJDJOriginalPath"], dt.ToString("yyyyMMdd"));
                            string destinatePath = Path.Combine(ConfigurationManager.AppSettings["YJDJFilesPath"], dt.ToString("yyyyMMdd"));

                            if (!Directory.Exists(originalPath))
                                Directory.CreateDirectory(originalPath);

                            if (!Directory.Exists(destinatePath))
                                Directory.CreateDirectory(destinatePath);

                            string oldfileName = Path.GetFileName(file.FileName);
                            int startIndex = oldfileName.LastIndexOf(".");
                            string extend = oldfileName.Substring(startIndex);
                            string fileName = Guid.NewGuid().ToString("N") + extend;

                            string sFilePath = Path.Combine(originalPath, fileName);
                            string dFilePath = Path.Combine(destinatePath, fileName);

                            if (System.IO.File.Exists(sFilePath))
                                System.IO.File.Delete(sFilePath);

                            if (System.IO.File.Exists(dFilePath))
                                System.IO.File.Delete(dFilePath);

                            file.SaveAs(sFilePath);

                            ImageCompress.CompressPicture(sFilePath, dFilePath, Convert.ToInt32(ConfigurationManager
                .AppSettings["YJDJPicSize"]), 0, "W");

                            //定义访问图片的WEB路径
                            string relativePictutePATH = Path.Combine(dt.ToString("yyyyMMdd"), fileName);
                            relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                            if (!string.IsNullOrEmpty(PicturePath))
                                PicturePath += ";";
                            PicturePath = PicturePath + relativePictutePATH;
                        }
                    }
                }
            }

            STREESTORE stree = new STREESTORE
            {

                SHOPNAME = VMStreeStore.SHOPNAME,
                ADDRESS = VMStreeStore.ADDRESS,
                SHOPUSERNAME = VMStreeStore.SHOPUSERNAME,
                SHOPPHONE = VMStreeStore.SHOPPHONE,
                STREESTORETYPEID = VMStreeStore.STREESTORETYPEID,
                ISMTZP = VMStreeStore.ISMTZP,
                ISGSWSXKZ = VMStreeStore.ISGSWSXKZ,
                ISHJPL = VMStreeStore.ISHJPL,
                ISPSXKZ = VMStreeStore.ISPSXKZ,
                GEOMETRY = VMStreeStore.GEOMETRY,
                PICTUREURLS = PicturePath
            };
            StreeStoreBLL.Addstreestore(stree);
            return RedirectToAction("Index");
        }

        //
        // GET: /StreeStore/Edit/5
        public ActionResult Edit()
        {
            string STREEID =Request["STREESTOREID"];
            STREESTORE stree = StreeStoreBLL.GetSTREESTORESByID(decimal.Parse(STREEID));
            if (stree != null)
            {
                VMStreeStore VMStreeStore = new VMStreeStore
                {
                    STREESTOREID=stree.STREESTOREID,
                    SHOPNAME = stree.SHOPNAME,
                    ADDRESS = stree.ADDRESS,
                    SHOPUSERNAME = stree.SHOPUSERNAME,
                    SHOPPHONE = stree.SHOPPHONE,
                    STREESTORETYPEID = stree.STREESTORETYPEID,
                    ISMTZP = Convert.ToInt32(stree.ISMTZP),
                    ISGSWSXKZ = Convert.ToInt32(stree.ISGSWSXKZ),
                    ISHJPL = Convert.ToInt32(stree.ISHJPL),
                    ISPSXKZ = Convert.ToInt32(stree.ISPSXKZ),
                    GEOMETRY = stree.GEOMETRY,
                    PICTUREURLS = stree.PICTUREURLS
                };
                if (stree.STREESTORETYPE != null)
                {
                    STREESTORETYPE streestoretypeZ = StreeStoreTypeBLL.GetSTREESTORETYPESByID(stree.STREESTORETYPE.PARENTID);
                    if (streestoretypeZ != null)
                    {
                        ViewBag.streestoretypeZ = stree.STREESTORETYPE.PARENTID;
                        ViewBag.streestoretypeB = streestoretypeZ.PARENTID;
                    }
                }

                ViewBag.type = 1;

                return View(THIS_VIEW_PATH + "Edit.cshtml", VMStreeStore);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //
        // POST: /StreeStore/Edit/5
        [HttpPost]
        public ActionResult Edit(VMStreeStore VMStreeStore)
        {
            string PicturePath = string.Empty;
            HttpFileCollectionBase files = Request.Files;
            if (files != null && files.Count > 0)
            {
                DateTime dt = DateTime.Now;
                for (int i = 0; i < files.Count; i++)
                {
                    #region 添加单个图片
                    HttpPostedFileBase file = files[i];
                    if (file != null)
                    {
                        //文件类型
                        string fileType = file.ContentType;

                        //上传的是图片
                        if (fileType.Equals("image/x-png") || fileType.Equals("image/png")
                            || fileType.Equals("image/GIF") || fileType.Equals("image/peg")
                            || fileType.Equals("image/jpeg"))
                        {
                            string originalPath = Path.Combine(ConfigurationManager.
                                AppSettings["YJDJOriginalPath"], dt.ToString("yyyyMMdd"));
                            string destinatePath = Path.Combine(ConfigurationManager.
                                AppSettings["YJDJFilesPath"], dt.ToString("yyyyMMdd"));

                            if (!Directory.Exists(originalPath))
                                Directory.CreateDirectory(originalPath);

                            if (!Directory.Exists(destinatePath))
                                Directory.CreateDirectory(destinatePath);

                            string oldfileName = Path.GetFileName(file.FileName);
                            int startIndex = oldfileName.LastIndexOf(".");
                            string extend = oldfileName.Substring(startIndex);
                            string fileName = Guid.NewGuid().ToString("N") + extend;

                            string sFilePath = Path.Combine(originalPath, fileName);
                            string dFilePath = Path.Combine(destinatePath, fileName);

                            if (System.IO.File.Exists(sFilePath))
                                System.IO.File.Delete(sFilePath);

                            if (System.IO.File.Exists(dFilePath))
                                System.IO.File.Delete(dFilePath);

                            file.SaveAs(sFilePath);

                            ImageCompress.CompressPicture(sFilePath, dFilePath, Convert.ToInt32(ConfigurationManager
                .AppSettings["YJDJPicSize"]), 0, "W");

                            //定义访问图片的WEB路径
                            string relativePictutePATH = Path.Combine(dt.ToString("yyyyMMdd"), fileName);
                            relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                            if (!string.IsNullOrEmpty(PicturePath))
                                PicturePath += ";";
                            PicturePath = PicturePath + relativePictutePATH;
                        }
                    }
                    #endregion
                }
            }
            int STREEID;
            int.TryParse(Request["STREESTOREID"], out STREEID);
            if (STREEID != 0)
            {
                STREESTORE sModel = StreeStoreBLL.GetSTREESTORESByID(STREEID);
                if (sModel != null)
                {
                    #region 赋值

                    if (string.IsNullOrEmpty(PicturePath))
                        PicturePath = sModel.PICTUREURLS;

                    STREESTORE stree = new STREESTORE
                    {
                        STREESTOREID = STREEID,
                        SHOPNAME = VMStreeStore.SHOPNAME,
                        ADDRESS = VMStreeStore.ADDRESS,
                        SHOPUSERNAME = VMStreeStore.SHOPUSERNAME,
                        SHOPPHONE = VMStreeStore.SHOPPHONE,
                        STREESTORETYPEID = VMStreeStore.STREESTORETYPEID,
                        ISMTZP = Convert.ToInt32(VMStreeStore.ISMTZP),
                        ISGSWSXKZ = Convert.ToInt32(VMStreeStore.ISGSWSXKZ),
                        ISHJPL = Convert.ToInt32(VMStreeStore.ISHJPL),
                        ISPSXKZ = Convert.ToInt32(VMStreeStore.ISPSXKZ),
                        GEOMETRY = VMStreeStore.GEOMETRY,
                        PICTUREURLS = PicturePath
                    };


                    #endregion

                    StreeStoreBLL.Modifystreestore(stree);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Select()
        {
            string STREEID = this.Request.QueryString["STREESTOREID"];

            STREESTORE stree = StreeStoreBLL.GetSTREESTORESByID(decimal.Parse(STREEID));
            if (stree != null)
            {
                VMStreeStore VMStreeStore = new VMStreeStore
                {
                    SHOPNAME = stree.SHOPNAME,
                    ADDRESS = stree.ADDRESS,
                    SHOPUSERNAME = stree.SHOPUSERNAME,
                    SHOPPHONE = stree.SHOPPHONE,
                    STREESTORETYPEID = stree.STREESTORETYPEID,
                    ISMTZP = Convert.ToInt32(stree.ISMTZP),
                    ISGSWSXKZ = Convert.ToInt32(stree.ISGSWSXKZ),
                    ISHJPL = Convert.ToInt32(stree.ISHJPL),
                    ISPSXKZ = Convert.ToInt32(stree.ISPSXKZ),
                    GEOMETRY = stree.GEOMETRY,
                    PICTUREURLS = stree.PICTUREURLS
                };
                if (stree.STREESTORETYPE != null)
                {
                    ViewBag.streestoretypeC = stree.STREESTORETYPE.TYPENAME;
                    STREESTORETYPE streestoretypeS =  StreeStoreTypeBLL.GetSTREESTORETYPESByID(stree.STREESTORETYPE.PARENTID);
                    if (streestoretypeS != null)
                    {
                        ViewBag.streestoretypeS = streestoretypeS.TYPENAME;
                        ViewBag.streestoretypeB = StreeStoreTypeBLL.GetSTREESTORETYPESName(streestoretypeS.PARENTID);
                    }
                }
                ViewBag.type = 2;
                return View(THIS_VIEW_PATH+"Select.cshtml", VMStreeStore);

            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //
        // GET: /StreeStore/Delete/5
        public ActionResult PicSelect()
        {
            string STREEID = this.Request.QueryString["STREESTOREID"];

            STREESTORE stree = StreeStoreBLL.GetSTREESTORESByID(decimal.Parse(STREEID));
            if (stree != null)
            {
                VMStreeStore VMStreeStore = new VMStreeStore
                {
                    SHOPNAME = stree.SHOPNAME,
                    ADDRESS = stree.ADDRESS,
                    SHOPUSERNAME = stree.SHOPUSERNAME,
                    SHOPPHONE = stree.SHOPPHONE,
                    STREESTORETYPEID = stree.STREESTORETYPEID,
                    ISMTZP = Convert.ToInt32(stree.ISMTZP),
                    ISGSWSXKZ = Convert.ToInt32(stree.ISGSWSXKZ),
                    ISHJPL = Convert.ToInt32(stree.ISHJPL),
                    ISPSXKZ = Convert.ToInt32(stree.ISPSXKZ),
                    GEOMETRY = stree.GEOMETRY,
                    PICTUREURLS = stree.PICTUREURLS
                };



                return View(THIS_VIEW_PATH+"PicSelect.cshtml", VMStreeStore);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public string Delete()
        {
            string STREESTOREID = Request.QueryString["STREESTOREID"];
            decimal SID = 0;
            decimal.TryParse(STREESTOREID, out SID);
            if (SID != 0)
            {
                StreeStoreBLL.Deletestreestore(SID);
                return "1";
            }
            else
            {
                return "0";
            }

        }


    }
}
