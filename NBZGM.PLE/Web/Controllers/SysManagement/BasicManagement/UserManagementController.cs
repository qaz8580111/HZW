using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using Web.ViewModels;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.UnitBLLs;
using Taizhou.PLE.BLL.UnitTypeBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model.CommonModel;
using System.Drawing;
using System.Drawing.Imaging;
using Taizhou.PLE.BLL.GroupBLLs;

namespace Web.Controllers.SysManagement.BasicManagement
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManagementController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/UserManagement/";

        public ActionResult Index(string UnitID)
        {
            ViewData["UnitID"] = "10000";

            if (!string.IsNullOrWhiteSpace(UnitID))
            {

                ViewData["UnitID"] = UnitID;
            }

            return View(THIS_VIEW_PATH + "Index.cshtml");
        }


        /// <summary>
        ///获取用户管理树 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUserManageTree(string ContactsType)
        {
            List<TreeModel> treeModels = null;


            switch (ContactsType)
            {
                case "zzjg":
                    treeModels = UserBLL.GetTreeNodes();
                    break;
                case "yhz":
                    treeModels = UserBLL.GetTreeNodesByGroup(SessionManager.User.UserID);
                    break;
                case "grtxl":
                    treeModels = UserBLL.GetTreeNodesByUserID(SessionManager.User.UserID);
                    break;
                default:
                    treeModels = UserBLL.GetTreeNodes();
                    break;
            }
            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户列表并且进行分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsers(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<USER> users = null;
            //单位标识
            string strUnitID = this.Request.QueryString["UnitID"];

            decimal? unitID = null;

            if (!string.IsNullOrWhiteSpace(strUnitID))
            {
                unitID = decimal.Parse(strUnitID);
            }

            users = UserBLL.GetTotalUsersByUnitID(unitID); 
            int num=users.Count();
           if (users.Count() == 0) {
               users = UserBLL.GetAllUsers().Where(a => a.USERID == unitID);
           }
            var list = users
                      .Skip((int)iDisplayStart.Value)
                      .Take((int)iDisplayLength.Value)
                      .Select(t => new
                      {
                          UserID = t.USERID,
                          UserName = t.USERNAME,
                          UserAccount = t.ACCOUNT,
                          UnitName = t.UNIT.UNITNAME,
                          PositionName = t.USERPOSITION.USERPOSITIONNAME,
                          SeqNo = t.SEQNO
                      });


            return Json(new
            {
                sEcho = secho,
                iTotalRecords = users.Count(),
                iTotalDisplayRecords = users.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示添加用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUser()
        {
            //用户所属单位
            ViewBag.UnitID = this.Request.QueryString["unitID"];

            //获取所有的单位
            List<SelectListItem> unitList = UnitBLL.GetAllUnits().ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString()
                }).ToList();

            //职务列表
            List<SelectListItem> userPositonList = UserBLL.GetAllPositon()
              .ToList().Select(c => new SelectListItem
                {
                    Text = c.USERPOSITIONNAME,
                    Value = c.USERPOSITIONID.ToString()
                }).ToList();

            userPositonList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });

            //干部类别列表
            List<SelectListItem> userCategoryList = UserBLL.GetAllUserCategories()
                .ToList().Select(c => new SelectListItem
                {
                    Text = c.USERCATEGORYNAME,
                    Value = c.USERCATEGORYID.ToString()
                }).ToList();

            userCategoryList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });

            ViewBag.unitList = unitList;
            ViewBag.userPositonList = userPositonList;
            ViewBag.userCategoryList = userCategoryList;
            return PartialView(THIS_VIEW_PATH + "AddUser.cshtml");
        }

        /// <summary>
        /// 提交添加用户表单
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult CommitAddUser(VMUser vwUser)
        {
            //图片流
            string avatar = "";
            //获取图片
            HttpPostedFileBase file = this.Request.Files["Avatar"];

            if (!string.IsNullOrWhiteSpace(file.FileName))
            {
                int upPhotoLength = file.ContentLength;
                string upPhotoname = file.FileName;
                string uphototype = file.ContentType;

                if (uphototype.Equals("image/x-png")
                    || uphototype.Equals("image/png")
                    || uphototype.Equals("image/GIF")
                    || uphototype.Equals("image/peg")
                    || uphototype.Equals("image/jpeg"))
                {
                    byte[] PhotoArray = new Byte[upPhotoLength];
                    Stream PhotoStream = file.InputStream;
                    PhotoStream.Read(PhotoArray, 0, upPhotoLength);
                    avatar = uphototype + "|" + Convert.ToBase64String(PhotoArray);
                }
            }
            //else
            //{
            //    string path = @"E:/picture/lss.jpg";

            //    if (!string.IsNullOrWhiteSpace(path))
            //    {
            //        //从指定的路径创建图片对象
            //        Image image = Image.FromFile(path);
            //        //创建其支持储存区为内存的流
            //        MemoryStream imageMem = new MemoryStream();
            //        //将图片已指定的文本格式保存在内存流中
            //        image.Save(imageMem, ImageFormat.Jpeg);
            //        //将流内容写入字节数组中
            //        byte[] imageBytes = imageMem.ToArray();
            //        //将8位无符号整数的数组转换成Base6数字编码的等效字符串
            //        avatar = "" + "|" + Convert.ToBase64String(imageBytes);
            //    }
            //}

            string userGroups = this.Request.Form["UserGroupIDS"];

            string[] userGroupAttr = userGroups.Split(',');

            //所属单位标识
            string unitID = this.Request.Form["UnitID"];

            USER user = new USER()
            {
                UNITID = decimal.Parse(unitID),
                USERNAME = vwUser.UserName,
                USERPOSITIONID = vwUser.UserPositionID,
                USERCATEGORYID = vwUser.UserCategoryID,
                ACCOUNT = vwUser.Account,
                PASSWORD = vwUser.Password,
                RTXACCOUNT = vwUser.RTXAccount,
                SMSNUMBERS = vwUser.SMSNumbers,
                SEQNO = vwUser.SeqNo,
                STATUSID = (decimal)StatusEnum.Normal,
                WORKZZ = vwUser.WorkZZ
             
            };

            UserBLL.AddUser(user, avatar, userGroupAttr);

            return RedirectToAction("Index", new { UnitID = unitID });
        }

        /// <summary>
        /// 显示修改用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUser(string UserID)
        {
            string strUserID = UserID;

            USER user = UserBLL.GetUserByUserID(decimal.Parse(strUserID));

            VMUser vmUser = new VMUser()
            {
                UserID = user.USERID,
                UserName = user.USERNAME,
                Account = user.ACCOUNT,
                SeqNo = user.SEQNO,
                UserPositionID = user.USERPOSITIONID,
                UserCategoryID = user.USERCATEGORYID,
                UnitID = user.UNITID,
                RTXAccount = user.RTXACCOUNT,
                SMSNumbers = user.SMSNUMBERS,
                WorkZZ = user.WORKZZ
               
            };

            //获取所有的单位
            List<SelectListItem> unitList = UnitBLL.GetAllUnits().ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString()
                }).ToList();

            //职务列表
            List<SelectListItem> userPositonList = UserBLL.GetAllPositon()
              .ToList().Select(c => new SelectListItem
              {
                  Text = c.USERPOSITIONNAME,
                  Value = c.USERPOSITIONID.ToString()
              }).ToList();

            //干部类别列表
            List<SelectListItem> userCategoryList = UserBLL.GetAllUserCategories()
                .ToList().Select(c => new SelectListItem
                {
                    Text = c.USERCATEGORYNAME,
                    Value = c.USERCATEGORYID.ToString()
                }).ToList();

            ViewBag.unitList = unitList;

            ViewBag.userPositonList = userPositonList;

            ViewBag.userCategoryList = userCategoryList;
            //用户标识
            ViewBag.UserID = strUserID;

            return PartialView(THIS_VIEW_PATH + "EditUser.cshtml", vmUser);
        }

        public void RenderPhoto(string userID)
        {
            USERARCHIVE attachment = UserBLL.GetUserAttachmentByUserID(decimal.Parse(userID));

            if (attachment != null)
            {
                this.Response.Clear();
                string[] str = attachment.AVATAR.Split('|');
                this.Response.ContentType = str[0];
                this.Response.BinaryWrite(Convert.FromBase64String(str[1]));
            }
            else
            {
                string img = Server.MapPath("/Images/城管执法1.jpg");
                byte[] bytes = System.IO.File.ReadAllBytes(img);
                img = Convert.ToBase64String(bytes);
                this.Response.ContentType = "image/jpg";
                this.Response.BinaryWrite(Convert.FromBase64String(img));
            }
        }

        /// <summary>
        /// 提交修改的行政单位表单
        /// </summary>
        /// <param name="unit">修改的对象实例</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditUser(VMUser vmUser)
        {
            //用户标识
            string userID = this.Request.Form["UserID"];
            //所属单位标识
            string unitID = this.Request.Form["UnitID"];
            //用户组标识
            string userGroupIDS = this.Request.Form["UserGroupIDS"];

            USER user = new USER()
            {
                USERID = decimal.Parse(userID),
                USERNAME = vmUser.UserName,
                USERPOSITIONID = vmUser.UserPositionID,
                USERCATEGORYID = vmUser.UserCategoryID,
                ACCOUNT = vmUser.Account,
                WORKZZ=vmUser.WorkZZ,
                SEQNO = vmUser.SeqNo
            };

            //图片流
            string avatar = "";
            //获取图片
            HttpPostedFileBase file = this.Request.Files["Avatar"];

            if (!string.IsNullOrWhiteSpace(file.FileName))
            {
                int upPhotoLength = file.ContentLength;
                string upPhotoname = file.FileName;
                string uphototype = file.ContentType;

                if (uphototype.Equals("image/x-png")
                    || uphototype.Equals("image/png")
                    || uphototype.Equals("image/GIF")
                    || uphototype.Equals("image/peg")
                    || uphototype.Equals("image/jpeg"))
                {
                    byte[] PhotoArray = new Byte[upPhotoLength];
                    Stream PhotoStream = file.InputStream;
                    PhotoStream.Read(PhotoArray, 0, upPhotoLength);
                    avatar = uphototype + "|" + Convert.ToBase64String(PhotoArray);
                }
            }
            UserBLL.ModifyUser(user, avatar, userGroupIDS);

            return RedirectToAction("Index", new { UnitID = unitID });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public ActionResult DeleteUser()
        {
            //用户标识
            string userID = this.Request.QueryString["UserID"];
            //所属单位标识
            string unitID = this.Request.QueryString["UnitID"];

            UserBLL.DeleteUser(decimal.Parse(userID));

            return RedirectToAction("Index", new { UnitID = unitID });

        }

        /// <summary>
        /// 添加用户时验证帐号是否存在
        /// </summary>
        /// <returns></returns>
        public bool validationAccountIsExist()
        {
            string account = this.Request.QueryString["Account"];

            return UserBLL.validationAccountIsExist(account);
        }

        /// <summary>
        /// 添加用户时验证RTX帐号是否存在
        /// </summary>
        /// <returns></returns>
        public bool validationRTXAccountIsExist()
        {
            string RTXAccount = this.Request.QueryString["RTXAccount"];

            return UserBLL.validationRTXAccountIsExist(RTXAccount);
        }

        /// <summary>
        /// 验证帐号是否可以修改
        /// </summary>
        /// <returns></returns>
        public bool validationAccountIsCanEdit()
        {
            string account = this.Request.QueryString["Account"];
            string userID = this.Request.QueryString["UserID"];

            return UserBLL.validationAccountIsCanEdit(account, decimal.Parse(userID));
        }

        /// <summary>
        /// 验证RTX帐号是否可以修改
        /// </summary>
        /// <returns></returns>
        public bool validationRTXAccountIsCanEdit()
        {
            string RTXAccount = this.Request.QueryString["RTXAccount"];
            string userID = this.Request.QueryString["UserID"];

            return UserBLL.validationRTXAccountIsCanEdit(RTXAccount, decimal.Parse(userID));
        }

        /// <summary>
        /// 修改用户时初始化用户组
        /// </summary>
        /// <returns></returns>
        public string GetUserGroupNames()
        {
            string userID = this.Request.Form["userID"];

            IQueryable<GROUP> groups = GroupBLL
                .GetUserGroupsByUserID(decimal.Parse(userID));

            string groupNames = "";

            foreach (GROUP g in groups)
            {
                groupNames += g.GROUPNAME + ",";
            }

            groupNames = groupNames.TrimEnd(',');

            return groupNames;
        }
    }
}

