using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Workflows;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CaseDocModels;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.BLL.UserBLLs;

namespace Web.Controllers.IntegratedService.CaseManagement.Workflow.Document
{
    /// <summary>
    ///现场照片(图片)证据
    /// </summary>
    public class DocXCZPTPZJController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/IntegratedService/CaseManagement/Document/";

        public ActionResult Index(string WIID, string DDID,
            string AIID, string ADID)
        {
            XCZPTPZJ xczptpzj = new XCZPTPZJ();
            if (!string.IsNullOrWhiteSpace(WIID))
            {
                CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
                ViewBag.WICode = caseWorkflow.CaseForm.WICode;
                //获取检查勘验笔录里的数据，获取拍摄地点和时间是用到
                XCJCKYBL xcjckybl = DocBLL.GetLatestXCJCKYBL(WIID);
                //XCZPTPZJ xczptpzj = new XCZPTPZJ();
                //判断检查勘验笔录里是否有数据
                if (xcjckybl != null)
                {
                    //获取拍摄地点
                    xczptpzj.PSDD = xcjckybl.JCKYDD;
                    //获取拍摄绘制时间
                    string pshzsj = xcjckybl.StartTimeYMD + " " + xcjckybl.StartKCSJ;

                    if (!string.IsNullOrWhiteSpace(pshzsj))
                    {
                        xczptpzj.PSHZSJ = Convert.ToDateTime(pshzsj);
                    }
                }
            }
           

            //根据当前登录用户的用户标识获取该单位下的所有成员
            List<USER> listUser = UserBLL.GetTotalUsersByUnitID(SessionManager.User.UnitID).ToList();

            //执法人员1成员列表 
            List<SelectListItem> ZFRMC1 = listUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = string.Format("{0},{1},{2}", t.USERID.ToString(), t.USERNAME, t.ZFZBH),
                Selected = t.USERID == SessionManager.User.UserID ? true : false
            }).ToList();

            ViewBag.ZFRMC1 = ZFRMC1;

            //执法人员2成员列表
            List<SelectListItem> ZFRMC2 = listUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = string.Format("{0},{1},{2}", t.USERID.ToString(), t.USERNAME, t.ZFZBH),
                Selected = false
            }).ToList();

            ViewBag.ZFRMC2 = ZFRMC2;

            return PartialView(THIS_VIEW_PATH + "XCZPTPZJ.cshtml", xczptpzj);
        }


        public ActionResult Edit(string WIID, string DDID,
            string AIID, string ADID, string DIID, long rad)
        {
            ViewBag.DDID = DDID;
            ViewBag.WIID = WIID;
            ViewBag.AIID = AIID;
            ViewBag.ADID = ADID;
            ViewBag.DIID = DIID;

            CaseWorkflow caseWorkflow = new CaseWorkflow(WIID);
            ViewBag.WICode = caseWorkflow.CaseForm.WICode;

            //根据文书标示获取文书
            DOCINSTANCE docInstance = DocBLL.GetDocInstanceByDDID(DIID);
            XCZPTPZJ xczptpzj = (XCZPTPZJ)Serializer.Deserialize(docInstance.ASSEMBLYNAME,
                docInstance.TYPENAME, docInstance.VALUE);

            ViewBag.PICTUREURL = xczptpzj.PictureUrl;

            //根据当前用户的用户标示获取该单位下的所有成员
            List<USER> listUser = UserBLL.GetTotalUsersByUnitID(SessionManager.User.UnitID).ToList();

            //执法人1
            List<SelectListItem> ZFRY1 = listUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = string.Format("{0},{1},{2}", t.USERID.ToString(), t.USERNAME, t.ZFZBH)
            }).ToList();
            //ZFRY1.FirstOrDefault(t => t.Value == xczptpzj.ZFRY1).Selected = true;
            ViewBag.ZFRY11 = ZFRY1;

            //执法人2
            List<SelectListItem> ZFRY2 = listUser.Select(t => new SelectListItem
            {
                Text = t.USERNAME,
                Value = string.Format("{0},{1},{2}", t.USERID.ToString(), t.USERNAME, t.ZFZBH)
            }).ToList();
            //ZFRY2.FirstOrDefault(t => t.Value == xczptpzj.ZFRY2).Selected = true;
            ViewBag.ZFRY22 = ZFRY2;

            return PartialView(THIS_VIEW_PATH + "EditXCZPTPZJ.cshtml", xczptpzj);
        }

        //现场照片(图片)证据
        [HttpPost]
        public ActionResult CommitDocumentXCZPTPZJ(XCZPTPZJ _xczptuzj)
        {
            string strDocTypeID = this.Request.Form["bulidDocType"];
            decimal docTypeID = 0.0M;
            decimal.TryParse(strDocTypeID, out docTypeID);
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string WICode = this.Request.Form["WICode"];

            string savePDFFilePath = "";
            DOCINSTANCE docInstance = null;
            Attachment attachment = null;
            XCZPTPZJ xczptuzj = new XCZPTPZJ();
            if (docTypeID == 1)
            {
                List<Attachment> attachments = AttachmentBLL
                    .UploadAttachment(this.Request.Files, WICode);

                //修改上传的图片
                if (attachments.Count() > 0)
                {
                    attachment = attachments.First();
                    //图片 URL
                    xczptuzj.PictureUrl = attachment.Path;
                }

                //拍摄地点
                xczptuzj.PSDD = _xczptuzj.PSDD;
                //拍摄内容
                xczptuzj.PSNR = _xczptuzj.PSNR;
                //执法人员1
                xczptuzj.ZFRY1 = _xczptuzj.ZFRY1;
                //执法人员2
                xczptuzj.ZFRY2 = _xczptuzj.ZFRY2;
                //拍摄绘制时间
                xczptuzj.PSHZSJ = _xczptuzj.PSHZSJ;

                //上传图片宽高
                string path = attachment.Path;
                System.Drawing.Image Img = System.Drawing.Image.FromFile(path);
                double uploadWidth = Img.Width;
                double uploadHeight = Img.Height;

                //修改后图片宽高
                int width = 0;
                int height = 0;

                double uploadPicRatio = uploadWidth / uploadHeight;
                double templateRatio = 440.0 / 280.0;

                //上传图宽高比大于模板宽高比时 以模板的宽为标准
                if (uploadPicRatio > templateRatio)
                {
                    width = 440;
                    double ratio = 440 / uploadWidth;
                    height = Convert.ToInt32(uploadHeight * ratio);
                }
                else
                {
                    height = 280;
                    double ratio = 280 / uploadHeight;
                    width = Convert.ToInt32(uploadWidth * ratio);
                }

                savePDFFilePath = DocBuildBLL.DocBuildXCZPTPZJ(
                    SessionManager.User.RegionName, WICode, xczptuzj, width, height);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.PDF,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    VALUE = Serializer.Serialize(xczptuzj),
                    ASSEMBLYNAME = xczptuzj.GetType().Assembly.FullName,
                    TYPENAME = xczptuzj.GetType().FullName,
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "现场照片(图片)证据"
                };
            }
            else
            {
                savePDFFilePath = DocBuildBLL.BuildDocByFiles(
                    SessionManager.User.RegionName, WICode, "现场照片(图片)证据",
                    this.Request.Files);

                docInstance = new DOCINSTANCE()
                {
                    DOCINSTANCEID = Guid.NewGuid().ToString("N"),
                    DDID = decimal.Parse(ddid),
                    DOCTYPEID = (decimal)DocTypeEnum.Image,
                    AIID = aiid,
                    DPID = DocBLL.GetDPIDByADID(decimal.Parse(adid)),
                    WIID = wiid,
                    DOCPATH = savePDFFilePath,
                    CREATEDTIME = DateTime.Now,
                    DOCNAME = "现场照片(图片)证据"
                };
            }

            //添加文书
            DocBLL.AddDocInstance(docInstance, false);

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

        //修改现场照片(图片)证据
        [HttpPost]
        public ActionResult CommitEditDocumentXCZPTPZJ(XCZPTPZJ _xczptuzj)
        {
            string ddid = this.Request.Form["DDID"];
            string wiid = this.Request.Form["WIID"];
            string aiid = this.Request.Form["AIID"];
            string adid = this.Request.Form["ADID"];
            string ajbh = this.Request.Form["WICode"];
            string docID = this.Request.Form["DIID"];
            string pictureUrl = this.Request.Form["PictureUrl"];

            Attachment attachment = null;
            List<Attachment> attachments = AttachmentBLL
                    .UploadAttachment(this.Request.Files, ajbh);

            //修改上传的图片
            if (attachments.Count() > 0)
            {
                attachment = attachments.First();
                //图片 URL
                _xczptuzj.PictureUrl = attachment.Path;
            }
            else
            {
                _xczptuzj.PictureUrl = pictureUrl;
            }

            //上传图片宽高
            string path = _xczptuzj.PictureUrl;
            System.Drawing.Image Img = System.Drawing.Image.FromFile(path);
            double uploadWidth = Img.Width;
            double uploadHeight = Img.Height;

            //修改后图片宽高
            int width = 0;
            int height = 0;

            double uploadPicRatio = uploadWidth / uploadHeight;
            double templateRatio = 440.0 / 280.0;

            //上传图宽高比大于模板宽高比时 以模板的宽为标准
            if (uploadPicRatio > templateRatio)
            {
                width = 440;
                double ratio = 440 / uploadWidth;
                height = Convert.ToInt32(uploadHeight * ratio);
            }
            else
            {
                height = 280;
                double ratio = 280 / uploadHeight;
                width = Convert.ToInt32(uploadWidth * ratio);
            }

            //生成文书路径
            string savePDFFilePath = DocBuildBLL.DocBuildXCZPTPZJ(SessionManager.User.RegionName,
                ajbh, _xczptuzj, width, height);

            DOCINSTANCE docInstance = new DOCINSTANCE()
            {
                DOCINSTANCEID = docID,
                DOCPATH = savePDFFilePath,
                VALUE = Serializer.Serialize(_xczptuzj),
                DOCNAME = "现场照片(图片)证据"
            };

            //修改文书
            DocBLL.EditDocInstance(docInstance);

            return RedirectToAction("WorkflowProcess", "Workflow",
                new
                {
                    WIID = wiid,
                    AIID = aiid,
                    DDID = ddid,
                    DIID = docInstance.DOCINSTANCEID
                });
        }

        //修改时获取现场照片(图片)证据照片
        public FilePathResult GetXCZPTPZJImg(string filePath)
        {
            string[] contentType1 = filePath.Split('.');
            string contentType = contentType1[contentType1.Length - 1];
            if (contentType == "jpg" || contentType == "jpeg")
            {
                return File(Server.UrlDecode(filePath), "image/jpeg");
            }
            else if (contentType == "png")
            {
                return File(Server.UrlDecode(filePath), "image/png");
            }
            else if (contentType == "gif")
            {
                return File(Server.UrlDecode(filePath), "image/gif");
            }
            else if (contentType == "bmp")
            {
                return File(Server.UrlDecode(filePath), "image/bmp");
            }
            else
            {
                return null;
            }
        }
    }
}
