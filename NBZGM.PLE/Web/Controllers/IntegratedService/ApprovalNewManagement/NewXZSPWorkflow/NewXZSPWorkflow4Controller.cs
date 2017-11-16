using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPNewModels;
using Taizhou.PLE.Web.Process.NewXZSPProess;

namespace Web.Controllers.IntegratedService.ApprovalNewManagement.XZSPWorkflow
{
    public class NewXZSPWorkflow4Controller : Controller
    {
        //
        // GET: /XZSPWorkflow4/
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/ApprovalNewManagement/NewXZSPWorkflow/";
        TotalWorkflows ttwork = new TotalWorkflows();

        public ActionResult Index(string AIID, decimal ADID, string ID)
        {
            PLEEntities db = new PLEEntities();
            //获取第三流程中的派遣人id
            string pqr = db.XZSPNEWTABs.SingleOrDefault(t => t.ADID == 3 && t.AIID == AIID).PQR;
            //根据第三步中的派遣人id得到他的部门ID
            decimal tt = UnitBLL.GetUnitIDByUserID(decimal.Parse(pqr));

            List<SelectListItem> DYCLR = UserBLL.GetZDRYByUnitID(tt, 8)
                 .Select(c => new SelectListItem()
                 {
                     Text = c.USERNAME,
                     Value = c.USERID.ToString(),
                 }).ToList();

            DYCLR.Insert(0, new SelectListItem()
            {
                Text = "请选择",
                Value = "",
                Selected = true
            });
            ViewBag.DYCLR = DYCLR;
            ttwork.Workflow4 = new Workflow4();
            return PartialView(THIS_VIEW_PATH + "NewXZSPWorkflow4.cshtml", ttwork.Workflow4);
        }

        [HttpPost]
        public ActionResult Commit(Workflow4 Workflow4)
        {
            //获取存片存储的位置
            string XZSPNEWOriginalPath = System.Configuration.ConfigurationManager
                .AppSettings["XZSPNEWOriginalPath"].ToString();
            //缩略图
            string XZSPNEWFilesPath = System.Configuration.ConfigurationManager
                .AppSettings["XZSPNEWFilesPath"].ToString();

            //获取3长图片的描述
            string pic1Text = Request["pic1Text"];
            string pic2Text = Request["pic2Text"];
            string pic3Text = Request["pic3Text"];
            //获取3张图片的对应的控件
            HttpPostedFileBase pic1 = Request.Files["pic1"];
            HttpPostedFileBase pic2 = Request.Files["pic2"];
            HttpPostedFileBase pic3 = Request.Files["pic3"];
            //3张图片的路径+描述，格式为 图片路径+{$$}+图片描述
            string pic1TextP = "", pic2TextP = "", pic3TextP = "";
            if (pic1 != null && pic1.ContentLength > 0)
                pic1TextP = GetAttachmentModelList(pic1, XZSPNEWOriginalPath, XZSPNEWFilesPath, pic1Text);
            if (pic2 != null && pic2.ContentLength > 0)
                pic2TextP = GetAttachmentModelList(pic2, XZSPNEWOriginalPath, XZSPNEWFilesPath, pic2Text);
            if (pic3 != null && pic3.ContentLength > 0)
                pic3TextP = GetAttachmentModelList(pic3, XZSPNEWOriginalPath, XZSPNEWFilesPath, pic3Text);

            NewXZSPProess newxzspproess = new NewXZSPProess();
            Workflow4.pic1TextP = pic1TextP;
            Workflow4.pic2TextP = pic2TextP;
            Workflow4.pic3TextP = pic3TextP;
            newxzspproess.XZSPWorkflow4(Workflow4);
            return RedirectToAction("NewApproval", "NewApproval");
        }

        /// <summary>
        ///上传图片
        /// </summary>
        /// <param name="files">上传文件</param>
        /// <param name="strOriginalPath">路径</param>
        /// <param name="destnation">缩略图路径</param>
        /// <param name="picName">上传文件名称</param>
        /// <returns></returns>
        public string GetAttachmentModelList(HttpPostedFileBase file, string strOriginalPath, string destnation, string picName)
        {
            string pathOrName = "";
            if (file != null && file.ContentLength > 0)
            {
                DateTime dt = DateTime.Now;
                //文件类型
                string fileType = file.ContentType;

                //上传的是图片
                if (fileType.Equals("image/x-png")
                    || fileType.Equals("image/png")
                    || fileType.Equals("image/GIF")
                    || fileType.Equals("image/peg")
                    || fileType.Equals("image/jpeg")
                    || fileType.Equals("image/pjpeg"))
                {
                    string originalPath = Path.Combine(strOriginalPath, dt.ToString("yyyyMMdd"));
                    string destinatePath = Path.Combine(destnation, dt.ToString("yyyyMMdd"));

                    if (!Directory.Exists(originalPath))
                    {
                        Directory.CreateDirectory(originalPath);
                    }

                    if (!Directory.Exists(destinatePath))
                    {
                        Directory.CreateDirectory(destinatePath);
                    }

                    string fileName = Guid.NewGuid().ToString("N") + Path.GetFileName(file.FileName);

                    string sFilePath = Path.Combine(originalPath, fileName);
                    string dFilePath = Path.Combine(destinatePath, fileName);

                    if (System.IO.File.Exists(sFilePath))
                    {
                        System.IO.File.Delete(sFilePath);
                    }

                    if (System.IO.File.Exists(dFilePath))
                    {
                        System.IO.File.Delete(dFilePath);
                    }

                    file.SaveAs(sFilePath);

                    ImageCompress.CompressPicture(sFilePath, dFilePath, 1580, 0, "W");

                    //定义访问图片的WEB路径
                    string relativePictutePATH = Path.Combine(dt.ToString("yyyyMMdd"), fileName);
                    relativePictutePATH = relativePictutePATH.Replace('\\', '/');

                    pathOrName = relativePictutePATH + "{$$}" + picName;
                }
            }
            return pathOrName;
        }

    }
}
