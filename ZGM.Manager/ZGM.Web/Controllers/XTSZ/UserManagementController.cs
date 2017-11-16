using Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Model;
using ZGM.BLL.UserPositionBLLs;
using ZGM.BLL.RoleBLLs;
using System.IO;
using ZGM.Web.Process.ImageUpload;
using System.Configuration;
using ZGM.Model.CustomModels;
using ZGM.Common.Enums;
using System.Data;
using System.Text;
using NPOI.SS.UserModel;
using System.IO;
using ZGM.BLL.GroupBLL;

namespace ZGM.Web.Controllers.XTSZ
{
    public class UserManagementController : Controller
    {
        //
        // GET: /UserManagement/

        public ActionResult Index(string UnitID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            ViewData["UnitID"] = "1";

            if (!string.IsNullOrWhiteSpace(UnitID))
            {
                ViewData["UnitID"] = UnitID;
            }
            return View();
        }

        /// <summary>
        /// 获取用户列表并且进行分页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUsers(int? iDisplayStart
           , int? iDisplayLength, int? secho)
        {
            IQueryable<SYS_USERS> users = null;
            //如果为0或者小于0，说明点击的是单位，如果不为0  这此字段代表用户所在的单位
            decimal isUserUnitId;
            decimal.TryParse(Request["isUser"], out isUserUnitId);
            //单位标识==单位或者用户编号，当isUserUnitId为0 则是单位编号，如果大于0 则表示用户编号
            string strUnitID = this.Request.QueryString["UnitID"];

            decimal? unitID = null;

            if (!string.IsNullOrWhiteSpace(strUnitID))
            {
                unitID = decimal.Parse(strUnitID);
            }

            if (isUserUnitId > 0)//查询用户
            {
                users = UserBLL.GetAllUsers().Where(a => a.USERID == unitID)
                   .OrderByDescending(a => a.SEQNO);
            }
            else//查询单位
            {
                users = UserBLL.GetTotalUsersByUnitID(unitID).OrderByDescending(a => a.SEQNO);
            }
            var list = users
                      .Skip((int)iDisplayStart.Value)
                      .Take((int)iDisplayLength.Value)
                      .Select(t => new
                      {
                          UserID = t.USERID,
                          UserName = t.USERNAME,
                          UserAccount = t.ACCOUNT,
                          UnitName = t.SYS_UNITS.UNITNAME,
                          ZFZBH = t.ZFZBH,
                          PositionName = t.SYS_USERPOSITIONS.USERPOSITIONNAME,
                          SeqNo = t.SEQNO
                      })
                      .OrderByDescending(a => a.SeqNo);


            return Json(new
            {
                sEcho = secho,
                iTotalRecords = users.Count(),
                iTotalDisplayRecords = users.Count(),
                aaData = list
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///获取用户管理树 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetUserManageTree()
        {
            List<TreeModel> treeModels = UserBLL.GetTreeNodes();

            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 显示添加用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddUser()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
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
            List<SelectListItem> userPositonList = UserPositionBLL.GetAllPositon()
              .ToList().Select(c => new SelectListItem
              {
                  Text = c.USERPOSITIONNAME,
                  Value = c.USERPOSITIONID.ToString()
              }).ToList();
            //小组列表
            List<SelectListItem> userGroupList = new GroupBLL().GetMajorProjectsLists()
                 .ToList().Select(c => new SelectListItem
                 {
                     Text = c.NAME,
                     Value = c.ID.ToString()
                 }).ToList();
            userPositonList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            userGroupList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });

            #region 获取角色信息
            //List<SelectListItem> list = new List<SelectListItem>();
            //list = RoleBLL.GetAllRoles().ToList()
            //    .Select(c => new SelectListItem
            //    {
            //        Value = c.ROLEID.ToString(),
            //        Text = c.ROLENAME
            //    }).ToList();
            //ViewBag.RoleList = list;
            #endregion
            ViewBag.unitList = unitList;
            ViewBag.userPositonList = userPositonList;
            ViewBag.userGroupList = userGroupList;
            return PartialView();
        }

        /// <summary>
        /// 提交添加用户表单
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult CommitAddUser(SYS_USERS vwUser)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取图片
            HttpFileCollectionBase files = Request.Files;
            List<FileClass> fileClass = new List<FileClass>();
            if (files.Count > 0 && files != null && files[0].ContentLength > 0)
            {
                string Ofilepath = ConfigurationManager.AppSettings["UserImageOriginalPath"];
                string ffilepath = ConfigurationManager.AppSettings["UserImageFilesPath"];
                string sfilepath = ConfigurationManager.AppSettings["UserImageSmallPath"];
                fileClass = new ImageUpload().UploadImages(files, Ofilepath, ffilepath, sfilepath);
            }

            SYS_USERS user = new SYS_USERS();
            user.USERID = UserBLL.GetNewUserID();
            user.UNITID = vwUser.UNITID;
            user.USERNAME = vwUser.USERNAME;
            user.USERPOSITIONID = vwUser.USERPOSITIONID;
            user.GROUPID = vwUser.GROUPID;
            user.ACCOUNT = vwUser.ACCOUNT;
            user.PASSWORD = vwUser.PASSWORD;
            user.PHONE = vwUser.PHONE;
            user.SPHONE = vwUser.SPHONE;
            user.SEQNO = vwUser.SEQNO;
            user.STATUSID = (decimal)StatusEnum.Normal;
            user.ZFZBH = vwUser.ZFZBH;
            user.AVATAR = fileClass.Count > 0 && fileClass[0] != null ? fileClass[0].OriginalPath : "";
            user.SLAVATAR = fileClass.Count > 0 && fileClass[0] != null ? fileClass[0].FilesPath : "";
            user.SMALLAVATAR = fileClass.Count > 0 && fileClass[0] != null ? fileClass[0].SmallPath : "";
            user.SEX = vwUser.SEX;
            user.BIRTHDAY = vwUser.BIRTHDAY;

            UserBLL.AddUser(user);

            #region 获取用户角色权限
            //string UserRloleID = Request.Form["GetChkboxValue"];
            //string[] array = UserRloleID.Split(',');
            //string RoleID;
            //for (int i = 0; i < array.Length; i++)
            //{
            //    RoleID = array[i];
            //    if (!string.IsNullOrEmpty(RoleID))
            //    {
            //        bool okInsert = UserBLL.InsertRoleId(user.USERID, decimal.Parse(RoleID));
            //    }
            //}
            #endregion
            return RedirectToAction("Index", new { UnitID = vwUser.UNITID });
        }



        /// <summary>
        /// 显示修改用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditUser(string UserID)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            string strUserID = UserID;
            #region 获取修改用户的角色权限
            //List<SYS_USERROLES> listRoleId = UserBLL.GetUserRolesByUserId(decimal.Parse(strUserID));
            //string roleId = "";
            //if (listRoleId.Count > 0)
            //{
            //    for (int i = 0; i < listRoleId.Count; i++)
            //    {
            //        roleId += listRoleId[i].ROLEID.ToString() + ",";
            //    }
            //}
            #endregion
            if (string.IsNullOrEmpty(strUserID))
            {
                return RedirectToAction("Index");
            }

            SYS_USERS user = UserBLL.GetUserByUserID(decimal.Parse(strUserID));



            //获取所有的单位
            List<SelectListItem> unitList = UnitBLL.GetAllUnits().ToList()
                .Select(c => new SelectListItem
                {
                    Text = c.UNITNAME,
                    Value = c.UNITID.ToString()
                }).ToList();
            #region 获取角色信息
            //获取角色信息
            //List<SelectListItem> list = new List<SelectListItem>();
            //list = RoleBLL.GetAllRoles().ToList()
            //    .Select(c => new SelectListItem
            //    {
            //        Value = c.ROLEID.ToString(),
            //        Text = c.ROLENAME
            //    }).ToList();
            //ViewBag.RoleList = list;
            #endregion
            //职务列表
            List<SelectListItem> userPositonList = UserPositionBLL.GetAllPositon()
              .ToList().Select(c => new SelectListItem
              {
                  Text = c.USERPOSITIONNAME,
                  Value = c.USERPOSITIONID.ToString()
              }).ToList();
            //小组列表
            List<SelectListItem> userGroupList = new GroupBLL().GetMajorProjectsLists()
                 .ToList().Select(c => new SelectListItem
                 {
                     Text = c.NAME,
                     Value = c.ID.ToString()
                 }).ToList();
            userGroupList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            string userPhoto = System.Configuration.ConfigurationManager.AppSettings["UserImageSmallPath"].ToString();
            ViewBag.userPhoto = userPhoto + user.SMALLAVATAR;
            ViewBag.unitList = unitList;

            ViewBag.userPositonList = userPositonList;
            ViewBag.userGroupList = userGroupList;
            //用户标识
            ViewBag.UserID = strUserID;
            //返回RoleId角色权限数据
            //ViewBag.RoleArray = roleId;

            return PartialView(user);
        }

        /// <summary>
        /// 提交修改的用户表单
        /// </summary>
        /// <param name="unit">修改的对象实例</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommitEditUser(SYS_USERS vwUser)
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //用户标识
            string userID = this.Request.Form["UserID"];
            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Index");
            }
            #region  获取RoleId
            // string hidenvalue = this.Request.Form["hiddenCheckboxValueof"];

            //首先根据UserId删除权限表的UserRole的所有的RoleId
            // bool successful = UserBLL.deleteRoleIdByUserId(decimal.Parse(userID));
            //删除成功后添加
            // string[] array = hidenvalue.Split(',');
            // string v;
            //添加选中的Checkbox权限
            //for (int i = 0; i < array.Length; i++)
            //{
            //    v = array[i];
            //    if (!string.IsNullOrEmpty(v))
            //    {
            //        bool okInsert = UserBLL.InsertRoleId(decimal.Parse(userID), decimal.Parse(v));
            //    }
            //}
            #endregion
            //获取图片
            HttpFileCollectionBase files = Request.Files;
            List<FileClass> fileClass = new List<FileClass>();
            if (files.Count > 0 && files != null && files[0].ContentLength > 0)
            {
                string Ofilepath = ConfigurationManager.AppSettings["UserImageOriginalPath"];
                string ffilepath = ConfigurationManager.AppSettings["UserImageFilesPath"];
                string sfilepath = ConfigurationManager.AppSettings["UserImageSmallPath"];
                fileClass = new ImageUpload().UploadImages(files, Ofilepath, ffilepath, sfilepath);
            }

            SYS_USERS user = new SYS_USERS();

            user.USERID = decimal.Parse(userID);
            user.UNITID = vwUser.UNITID;
            user.USERNAME = vwUser.USERNAME;
            user.USERPOSITIONID = vwUser.USERPOSITIONID;
            user.ACCOUNT = vwUser.ACCOUNT;
            user.GROUPID = vwUser.GROUPID;
            //user.PASSWORD = vwUser.PASSWORD;
            user.PHONE = vwUser.PHONE;
            user.SPHONE = vwUser.SPHONE;
            user.SEQNO = vwUser.SEQNO;
            user.ZFZBH = vwUser.ZFZBH;
            if (fileClass.Count > 0 && fileClass[0] != null)
            {
                user.AVATAR = fileClass[0].OriginalPath;
                user.SLAVATAR = fileClass[0].FilesPath;
                user.SMALLAVATAR = fileClass[0].SmallPath;
            }
            user.SEX = vwUser.SEX;
            user.BIRTHDAY = vwUser.BIRTHDAY;


            UserBLL.ModifyUser(user);
            return RedirectToAction("Index", new { UnitID = vwUser.UNITID });
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public void DeleteUser()
        {
            //用户标识
            string userID = this.Request.QueryString["UserID"];
            //所属单位标识
            string unitID = this.Request.QueryString["UnitID"];
            decimal userid;
            decimal.TryParse(userID, out userid);
            UserBLL.DeleteUser(userid);

            // return RedirectToAction("Index", new { UnitID = unitID });

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
        /// 添加用户时验证执法证号是否存在
        /// </summary>
        /// <returns></returns>
        public bool validationZFZBHIsExist()
        {
            string account = this.Request.QueryString["ZFZBH"];

            return UserBLL.validationZFZBHIsExist(account);
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
        /// 验证执法证号是否可以修改
        /// </summary>
        /// <returns></returns>
        public bool validationZFZHIsCanEdit()
        {

            string ZFZBH = this.Request.QueryString["ZFZBH"];
            string userID = this.Request.QueryString["UserID"];

            return UserBLL.validationAccountIsCanEdit(ZFZBH, decimal.Parse(userID));
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPassWord()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public bool ModifyPassword()
        {
            string OLDPassword = this.Request["OLDPassword"];
            string NEWPassword = this.Request["NEWPassword"];
            decimal userID = SessionManager.User.UserID;

            return UserBLL.ModifyUserPassword(OLDPassword, NEWPassword, userID);
        }

        /// <summary>
        /// Excel文件导入
        /// </summary>
        /// <returns></returns>
        public void ImportExcel()
        {
            var UploadFile = Request.Files["UploadFile"];
            //上传文件保存
            string filespath = CreateFile(System.Configuration.ConfigurationManager.AppSettings["IEFile"]);
            UploadFile.SaveAs(filespath);

            //获取Excel表里的数据
            DataTable dt = ReadExcel(filespath, 2).Tables[0];
            SYS_USERS usersinfo = new SYS_USERS();

            //创建Excel导入日志
            StreamWriter sw = CreateLogFile(System.Configuration.ConfigurationManager.AppSettings["IELFile"]);
            bool infos = false;

            using (System.Transactions.TransactionScope transaction = new System.Transactions.TransactionScope())
            {
                try
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (string.IsNullOrEmpty(dr[0].ToString().Trim()) && string.IsNullOrEmpty(dr[4].ToString().Trim())) continue;
                        //用户姓名
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[0].ToString().Trim(), "用户姓名", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的用户姓名数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'用户姓名');</script>");
                            return;
                        }
                        usersinfo.USERNAME = dr[0].ToString().Trim();

                        // 性别ID
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[1].ToString().Trim(), "性别", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的性别数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'性别');</script>");
                            return;
                        }
                        usersinfo.SEX = dr[1].ToString().Trim();

                        //所属单位                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[2].ToString().Trim(), "所属单位", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的所属单位数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'所属单位');</script>");
                            return;
                        }
                        usersinfo.UNITID = UnitBLL.GetUnitIdByUnitName(dr[2].ToString().Trim());

                        //人员类别                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[3].ToString().Trim(), "人员类别", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的人员类别数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'人员类别');</script>");
                            return;
                        }
                        usersinfo.USERPOSITIONID = UserPositionBLL.GetPositonIdByPositionName(dr[3].ToString().Trim());

                        //登录账号
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[4].ToString().Trim(), "登录账号", sw);
                        bool CheckAccount = UserBLL.CheckAccountIsExist(dr[4].ToString().Trim());
                        if (!infos || CheckAccount)
                        {
                            if (CheckAccount)
                            {
                                sw.WriteLine(usersinfo.USERNAME + "的登录账号重复!数据回滚!");
                                sw.Close();
                                Response.Write("<script>parent.AddCallBack(2,'登录账号');</script>");
                            }
                            return;
                        }
                        usersinfo.ACCOUNT = dr[4].ToString().Trim();

                        //登录密码                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[5].ToString().Trim(), "登录密码", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的登录密码数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'登录密码');</script>");
                            return;
                        }
                        usersinfo.PASSWORD = dr[5].ToString().Trim();

                        //手机号码                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[6].ToString().Trim(), "手机号码", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的手机号码数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'手机号码');</script>");
                            return;
                        }
                        usersinfo.PHONE = dr[6].ToString().Trim() == null ? null : dr[6].ToString().Trim();

                        //手机短号                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[7].ToString().Trim(), "手机短号", sw);
                        usersinfo.SPHONE = dr[7].ToString().Trim();

                        //执法证编号                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[8].ToString().Trim(), "执法证编号", sw);
                        if (!infos)
                        {
                            sw.WriteLine(usersinfo.USERNAME + "的执法证编号数据插入失败!数据回滚!");
                            sw.Close();
                            Response.Write("<script>parent.AddCallBack(2,'执法证编号');</script>");
                            return;
                        }
                        usersinfo.ZFZBH = dr[8].ToString().Trim() == null ? null : dr[8].ToString().Trim();

                        //单位中人员排序号                
                        infos = tiShiMing(dr[0].ToString().Trim(), dr[9].ToString().Trim(), "单位中人员排序号", sw);
                        usersinfo.SEQNO = decimal.Parse(dr[9].ToString().Trim());

                        //帐号状态
                        usersinfo.STATUSID = 1;
                        usersinfo.USERID = UserBLL.GetNewUserID();

                        UserBLL.AddUser(usersinfo);
                        sw.WriteLine(usersinfo.USERNAME + "已成功插入数据!");
                    }
                    transaction.Complete();
                    Response.Write("<script>parent.AddCallBack(1,'');</script>");
                    sw.Close();
                }
                catch (Exception e)
                {
                    transaction.Dispose();
                    sw.WriteLine("数据插入出现错误!错误原因:" + e);
                    Response.Write("<script>parent.AddCallBack(2,'');</script>");
                    sw.Close();
                }
            }
        }

        int ReadExcelEndRow = 0;
        public DataSet ReadExcel(string FileName, int startRow, params NpoiDataType[] ColumnDataType)
        {
            string colNamePix = "F";
            int ertime = 0;
            int intime = 0;
            DataSet ds = new DataSet("ds");
            DataTable dt = new DataTable("dt");
            DataRow dr;
            StringBuilder sb = new StringBuilder();
            using (FileStream stream = new FileStream(@FileName, FileMode.Open, FileAccess.Read))
            {
                IWorkbook workbook = WorkbookFactory.Create(stream);
                ISheet sheet = workbook.GetSheetAt(0);//得到里面第一个sheet
                int j;
                IRow row;
                #region ColumnDataType赋值
                if (ColumnDataType.Length <= 0)
                {
                    row = sheet.GetRow(startRow - 1);//得到第i行
                    ColumnDataType = new NpoiDataType[row.LastCellNum];
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        ICell hs = row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                        ColumnDataType[i] = GetCellDataType(hs); //NpoiDataType.Blank;
                        //if (i == 6)
                        //{
                        //    ColumnDataType[i] = NpoiDataType.Numeric;
                        //}
                    }
                }
                #endregion
                for (j = 0; j < ColumnDataType.Length; j++)
                {
                    Type tp = GetDataTableType(ColumnDataType[j]);
                    //if (j == 6)
                    //{
                    //    tp = typeof(double);
                    //}
                    dt.Columns.Add(colNamePix + (j + 1), tp);
                }

                ReadExcelEndRow = ReadExcelEndRow == 0 ? sheet.PhysicalNumberOfRows : ReadExcelEndRow;
                for (int i = startRow - 1; i <= ReadExcelEndRow; i++)
                {
                    row = sheet.GetRow(i);//得到第i行
                    if (row == null) continue;
                    try
                    {
                        dr = dt.NewRow();

                        for (j = 0; j < ColumnDataType.Length; j++)
                        {
                            object tempObj = GetCellData(ColumnDataType[j], row, j);
                            dr[colNamePix + (j + 1)] = tempObj;
                        }
                        dt.Rows.Add(dr);
                        intime++;
                    }
                    catch (Exception er)
                    {
                        ertime++;
                        sb.Append(string.Format("第{0}行出错：{1}\r\n", i + 1, er.Message));
                        continue;
                    }
                }

                ds.Tables.Add(dt);
            }
            if (ds.Tables[0].Rows.Count == 0 && sb.ToString() != "") throw new Exception(sb.ToString());
            return ds;
        }

        private Type GetDataTableType(NpoiDataType datatype)
        {
            Type tp = typeof(string);//Type.GetType("System.String")
            switch (datatype)
            {
                case NpoiDataType.Bool:
                    tp = typeof(bool);
                    break;
                case NpoiDataType.Datetime:
                    tp = typeof(DateTime);
                    break;
                case NpoiDataType.Numeric:
                    tp = typeof(double);
                    break;
                case NpoiDataType.Error:
                    tp = typeof(string);
                    break;
                case NpoiDataType.Blank:
                    tp = typeof(string);
                    break;
            }
            return tp;
        }

        private object GetCellData(NpoiDataType datatype, IRow row, int column)
        {
            object obj = row.GetCell(column) ?? null;
            if (datatype == NpoiDataType.Datetime)
            {
                string v = "";
                try
                {
                    v = row.GetCell(column).StringCellValue;
                }
                catch (Exception e1)
                {
                    //Errors.WriteError(v + "!" + e1.ToString());
                    v = row.GetCell(column).DateCellValue.ToString("yyyy-MM-dd hh:mm:ss");
                }
                if (v != "")
                {
                    try
                    {
                        obj = row.GetCell(column).DateCellValue.ToString("yyyy-MM-dd hh:mm:ss");

                    }
                    catch (Exception e2)
                    {
                        //Errors.WriteError(obj.ToString() + "!" + e2.ToString());
                        obj = Convert.ToDateTime(v).ToString("yyyy-MM-dd hh:mm:ss");
                    }
                }
                else
                    obj = DBNull.Value;


            }
            if (datatype == NpoiDataType.Numeric)
            {
                obj = DBNull.Value;
                try
                {
                    //if (row.GetCell(column).StringCellValue != "")
                    obj = row.GetCell(column).NumericCellValue;
                }
                catch (Exception e3)
                {
                    //Errors.WriteError(obj.ToString() + "!" + e3.ToString());
                    obj = row.GetCell(column).StringCellValue;
                }

            }
            return obj;
        }

        private NpoiDataType GetCellDataType(ICell hs)
        {
            NpoiDataType dtype;
            DateTime t1;
            string cellvalue = "";

            switch (hs.CellType)
            {
                case CellType.BLANK:
                    dtype = NpoiDataType.String;
                    cellvalue = hs.StringCellValue;
                    break;
                case CellType.BOOLEAN:
                    dtype = NpoiDataType.Bool;
                    break;
                case CellType.NUMERIC:
                    dtype = NpoiDataType.String;
                    try
                    {
                        if (hs.NumericCellValue.ToString().Contains("-") || hs.NumericCellValue.ToString().Contains("/") || hs.ToString().Contains("-") || hs.ToString().Contains("/"))
                        {
                            hs.DateCellValue.ToString();
                            dtype = NpoiDataType.Datetime;
                        }
                    }
                    catch { }
                    cellvalue = hs.NumericCellValue.ToString();
                    break;
                case CellType.STRING:
                    dtype = NpoiDataType.String;
                    cellvalue = hs.StringCellValue;
                    break;
                case CellType.ERROR:
                    dtype = NpoiDataType.Error;
                    break;
                case CellType.FORMULA:
                default:
                    dtype = NpoiDataType.Datetime;
                    break;
            }
            if (cellvalue != "" && DateTime.TryParse(cellvalue, out t1)) dtype = NpoiDataType.Datetime;
            return dtype;
        }

        public enum NpoiDataType
        {
            /// <summary>
            /// 字符串类型-值为1
            /// </summary>
            String,
            /// <summary>
            /// 布尔类型-值为2
            /// </summary>
            Bool,
            /// <summary>
            /// 时间类型-值为3
            /// </summary>
            Datetime,
            /// <summary>
            /// 数字类型-值为4
            /// </summary>
            Numeric,
            /// <summary>
            /// 复杂文本类型-值为5
            /// </summary>
            Richtext,
            /// <summary>
            /// 空白
            /// </summary>
            Blank,
            /// <summary>
            /// 错误
            /// </summary>
            Error
        }

        public bool tiShiMing(string names, object chuanZhi, string tiShi, StreamWriter sw)
        {
            if (chuanZhi == null || chuanZhi == "")
            {
                sw.WriteLine(names + "的" + tiShi + "未写入值");
                return false;
            }
            else
            {
                return true;
            }
        }

        public string CreateFile(string OriginalPath)
        {
            DateTime dt = DateTime.Now;
            if (!Directory.Exists(OriginalPath))
            {
                Directory.CreateDirectory(OriginalPath);
            }
            string OriginalPathYear = OriginalPath + dt.Year;
            if (!Directory.Exists(OriginalPathYear))
            {
                Directory.CreateDirectory(OriginalPathYear);
            }
            string OriginalPathdate = OriginalPathYear + "\\" + dt.ToString("yyyyMMdd");
            if (!Directory.Exists(OriginalPathdate))
            {
                Directory.CreateDirectory(OriginalPathdate);
            }
            return OriginalPathdate + "\\" + dt.ToString("yyyyMMddHHmmss") + ".xlsx";
        }

        public StreamWriter CreateLogFile(string OriginalPath)
        {
            DateTime dt = DateTime.Now;
            StreamWriter sw = null;
            if (!Directory.Exists(OriginalPath))
            {
                Directory.CreateDirectory(OriginalPath);
            }
            string OriginalPathYear = OriginalPath + dt.Year;
            if (!Directory.Exists(OriginalPathYear))
            {
                Directory.CreateDirectory(OriginalPathYear);
            }
            string OriginalPathdate = OriginalPathYear + "\\" + dt.ToString("yyyyMMdd");
            if (!Directory.Exists(OriginalPathdate))
            {
                Directory.CreateDirectory(OriginalPathdate);
            }
            string filepath = OriginalPathdate + "\\" + dt.ToString("yyyyMMddHHmmss") + ".txt";
            sw = System.IO.File.CreateText(filepath);
            return sw;
        }
    }
}
