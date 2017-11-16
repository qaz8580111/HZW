using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CustomModels;
using System.Data.Entity;
using Common.CommonModel;
using ZGM.BLL.UnitBLLs;

namespace ZGM.BLL.UserBLLs
{
    public class UserBLL
    {
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        public static void AddUser(SYS_USERS model)
        {
            Entities db = new Entities();
            db.SYS_USERS.Add(model);
            db.SaveChanges();
        }


        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="ysmm">原始密码</param>
        /// <param name="xmm">新密码</param>
        /// <param name="userId"></param>
        /// <returns>布尔型</returns>
        public static bool ModifyUserPassword(string OLDPassword,
            string NEWPassword, decimal userId)
        {
            bool flag = true;

            Entities db = new Entities();

            SYS_USERS user = db.SYS_USERS.FirstOrDefault(t => t.USERID == userId);
            if (user != null)
            {
                if (user.PASSWORD == OLDPassword)
                {
                    user.PASSWORD = NEWPassword;
                    db.SaveChanges();
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 获取一个新的用户标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewUserID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_USERID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 根据用户名获取用户roleId
        /// </summary>
        /// <param name="userId">需要修改的用户</param>
        /// <returns></returns>
        public static List<SYS_USERROLES> GetUserRolesByUserId(decimal userId)
        {
            Entities db = new Entities();
            List<SYS_USERROLES> list = db.SYS_USERROLES.Where(t => t.USERID == userId).ToList();
            return list;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="_user">用户</param>
        public static void ModifyUser(SYS_USERS _user)
        {
            Entities db = new Entities();

            SYS_USERS user = db.SYS_USERS.SingleOrDefault(t => t.USERID == _user.USERID
                && t.STATUSID == (decimal)StatusEnum.Normal);

            user.UNITID = _user.UNITID;
            user.USERNAME = _user.USERNAME;
            user.USERPOSITIONID = _user.USERPOSITIONID;
            user.ACCOUNT = _user.ACCOUNT;
            user.GROUPID = _user.GROUPID;
            // user.PASSWORD = _user.PASSWORD;
            user.PHONE = _user.PHONE;
            user.SPHONE = _user.SPHONE;
            user.SEQNO = _user.SEQNO;
            user.ZFZBH = _user.ZFZBH;
            if (!string.IsNullOrEmpty(_user.AVATAR))
            {
                user.AVATAR = _user.AVATAR;
            }
            if (!string.IsNullOrEmpty(_user.SLAVATAR))
            {
                user.SLAVATAR = _user.SLAVATAR;
            }
            if (!string.IsNullOrEmpty(_user.SMALLAVATAR))
            {
                user.SMALLAVATAR = _user.SMALLAVATAR;
            }

            user.SEX = _user.SEX;
            user.BIRTHDAY = _user.BIRTHDAY;

            db.SaveChanges();
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        public static void DeleteUser(decimal userID)
        {
            Entities db = new Entities();

            SYS_USERS user = db.SYS_USERS
                .SingleOrDefault(t => t.USERID == userID
                    && t.STATUSID == (decimal)StatusEnum.Normal);

            user.STATUSID = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }

        /// <summary>
        /// 根据用户ID 获取单条用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByUserID(decimal userID)
        {
            Entities db = new Entities();
            var results =
                 from u in db.SYS_USERS
                 join unit1 in db.SYS_UNITS on u.UNITID equals unit1.UNITID
                 join up in db.SYS_USERPOSITIONS
                     on u.USERPOSITIONID equals up.USERPOSITIONID into upTemp
                 from up in upTemp.DefaultIfEmpty()
                 where u.USERID == userID
                 select new UserInfo
                 {
                     UserID = u.USERID,
                     RoleIDS = u.SYS_USERROLES,
                     UserName = u.USERNAME,
                     UnitID = unit1.UNITID,
                     UnitName = unit1.UNITNAME,
                     PositionID = up.USERPOSITIONID,
                     PositionName = up.USERPOSITIONNAME,
                     PhoneIMEI = u.PHONEIMEI,
                     Phone=u.PHONE
                 };

            return results.FirstOrDefault();
        }

        /// <summary>
        /// 根据用户ID,和姓名 获取单条用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns></returns>
        public static UserInfo GetUserInfoByUser(decimal userID,string name)
        {
            Entities db = new Entities();
            var results =
                 from u in db.SYS_USERS
                 join unit1 in db.SYS_UNITS on u.UNITID equals unit1.UNITID
                 join up in db.SYS_USERPOSITIONS
                     on u.USERPOSITIONID equals up.USERPOSITIONID into upTemp
                 from up in upTemp.DefaultIfEmpty()
                 where u.USERID == userID && u.USERNAME == name
                 select new UserInfo
                 {
                     UserID = u.USERID,
                     RoleIDS = u.SYS_USERROLES,
                     UserName = u.USERNAME,
                     UnitID = unit1.UNITID,
                     UnitName = unit1.UNITNAME,
                     PositionID = up.USERPOSITIONID,
                     PositionName = up.USERPOSITIONNAME,
                     PhoneIMEI = u.PHONEIMEI,
                     Phone = u.PHONE
                 };

            return results.FirstOrDefault();
        }


        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="password">密码</param>
        /// <returns>根据账号密码获取登录用户</returns>
        public static UserInfo Login(string account, string password)
        {
            Entities db = new Entities();
            var results =
               from u in db.SYS_USERS
               join unit1 in db.SYS_UNITS on u.UNITID equals unit1.UNITID

               join up in db.SYS_USERPOSITIONS
                   on u.USERPOSITIONID equals up.USERPOSITIONID into upTemp
               from up in upTemp.DefaultIfEmpty()
               where u.ACCOUNT.ToUpper() == account
               && u.PASSWORD == password
               && u.STATUSID == (decimal)StatusEnum.Normal
               select new UserInfo
               {
                   UserID = u.USERID,
                   UserName = u.USERNAME,
                   RoleIDS = u.SYS_USERROLES,
                   UnitID = unit1.UNITID,
                   UnitName = unit1.UNITNAME,
                   UserPhoto = u.SLAVATAR,
                   PositionID = up.USERPOSITIONID,
                   PositionName = up.USERPOSITIONNAME,
                   UnitTypeId = (decimal)unit1.UNITTYPEID,
                   UnitPath = unit1.PATH,
                   Phone = u.PHONE,
                   Avatar = u.AVATAR
               };

            return results.FirstOrDefault();
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static bool CheckUserIsExist(string account, string password)
        {
            bool flag = false;
            Entities db = new Entities();
            SYS_USERS user = db.SYS_USERS.SingleOrDefault(t => t.ACCOUNT == account
                && t.PASSWORD == password && t.STATUSID == (decimal)StatusEnum.Normal);

            if (user != null)
            {
                flag = true;
            }

            return flag;
        }

        /// <summary>
        /// 判断帐号是否可以修改
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static bool validationAccountIsCanEdit(string account, decimal userID)
        {
            Entities db = new Entities();

            IQueryable<SYS_USERS> users = db.SYS_USERS.Where(t => t.ACCOUNT == account
                && t.STATUSID == (decimal)StatusEnum.Normal);

            if (users == null || users.Count() < 0)
            {
                return false;
            }

            var result = from t in users
                         where t.USERID != userID
                         select t;

            if (result.Count() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 根据用户标识获取用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static SYS_USERS GetUserByUserID(decimal userID)
        {
            Entities db = new Entities();

            return db.SYS_USERS.SingleOrDefault(t => t.USERID == userID
                && t.STATUSID == (decimal)StatusEnum.Normal);
        }

        /// <summary>
        /// 根据部门获取用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static SYS_USERS GetUserByDeptID(decimal deptID)
        {
            Entities db = new Entities();

            return db.SYS_USERS.SingleOrDefault(t => t.UNITID == deptID
                && t.STATUSID == (decimal)StatusEnum.Normal);
        }


        /// <summary>
        /// 根据手机号获取用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static SYS_USERS GetUserModel(string PHONE)
        {
            Entities db = new Entities();

            return db.SYS_USERS.SingleOrDefault(t => t.PHONE == PHONE
                && t.STATUSID == (decimal)StatusEnum.Normal);
        }

        /// <summary>
        /// 根据分队获取队员
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static IQueryable<SYS_USERS> IQuerableGetUserByDeptID(decimal deptID)
        {
            Entities db = new Entities();
            IQueryable<SYS_USERS> list = db.SYS_USERS.Where(t => t.UNITID == deptID && t.STATUSID == 1);
            return list;
        }


        /// <summary>
        /// 根据部门获取用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static SYS_USERS GetUserByRoleID(decimal roleID)
        {
            Entities db = new Entities();

            return db.SYS_USERS.SingleOrDefault(t => t.USERPOSITIONID == roleID
                && t.STATUSID == (decimal)StatusEnum.Normal);
        }


        /// <summary>
        /// 根据用户标识获取用户名
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static string GetUserNameByUserID(decimal userID)
        {
            SYS_USERS model = GetUserByUserID(userID);
            if (model != null)
                return model.USERNAME;
            else
                return "";
        }


        /// <summary>
        /// 根据部门编号获取用户名
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static string GetUserNameByDeptID(decimal deptID)
        {
            SYS_USERS model = GetUserByDeptID(deptID);
            if (model != null)
                return model.USERNAME;
            else
                return "";
        }
        public static decimal? GetUnitID(decimal UserID)
        {

            Entities db = new Entities();

            var model = db.SYS_USERS.SingleOrDefault(t => t.USERID == UserID);
            if (model != null)
                return model.UNITID;
            else
                return 0;

        }

        /// <summary>
        /// 根据角色ID获取用户名
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static string GetUserNameByRoleID(decimal roleID)
        {
            SYS_USERS model = GetUserByRoleID(roleID);
            if (model != null)
                return model.USERNAME;
            else
                return "";
        }
        /// <summary>
        /// 获取指挥中心人员
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SYS_USERS> GetZHZXUser()
        {
            Entities db = new Entities();
            IQueryable<SYS_USERS> list = db.SYS_USERS.Where(t => t.UNITID == 16);
            return list;
        }
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<SYS_USERS> GetAllUsers()
        {
            Entities db = new Entities();

            IQueryable<SYS_USERS> results = db.SYS_USERS.Where(t => t
                .STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);

            return results;
        }
        /// <summary>
        /// 判断帐号是否可以存在
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns></returns>
        public static bool validationAccountIsExist(string account)
        {
            Entities db = new Entities();

            if (db.SYS_USERS.Where(t => t.ACCOUNT == account
                && t.STATUSID == (decimal)StatusEnum.Normal)
                .Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断帐号是否可以存在
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns></returns>
        public static bool validationZFZBHIsExist(string ZFZBH)
        {
            Entities db = new Entities();

            if (db.SYS_USERS.Where(t => t.ZFZBH == ZFZBH
                && t.STATUSID == (decimal)StatusEnum.Normal)
                .Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断帐号是否可以修改
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static bool validationZFZHIsCanEdit(string ZFZH, decimal userID)
        {
            Entities db = new Entities();

            IQueryable<SYS_USERS> users = db.SYS_USERS.Where(t => t.ZFZBH == ZFZH
                && t.STATUSID == (decimal)StatusEnum.Normal);

            if (users == null || users.Count() < 0)
            {
                return false;
            }

            var result = from t in users
                         where t.USERID != userID
                         select t;

            if (result.Count() > 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 获取单位下的用户
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns></returns>
        public static IQueryable<SYS_USERS> GetTotalUsersByUnitID(decimal? unitID)
        {
            string strUnitID = "\\" + unitID + "\\";
            Entities db = new Entities();

            var results = from un in db.SYS_UNITS
                          from u in db.SYS_USERS
                          where un.PATH.Contains(strUnitID)
                          && un.UNITID == u.UNITID
                          select u;

            return results.Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);
        }
        /// <summary>
        /// 获取组织结构树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes()
        {

            List<TreeModel> treeModels = new List<TreeModel>();

            //查出所有单位
            List<SYS_UNITS> allUnits = UnitBLL.GetAllUnits()
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();
            //查出所有用户
            List<SYS_USERS> allUsers = GetAllUsers()
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //根节点 所有单位parentId为null的根节点
            List<SYS_UNITS> rootUnits = allUnits.Where(t => t.UNITID == 1 && (t.PARENTID == null || t.PARENTID == 0))
                .ToList();

            //遍历根节点
            foreach (var unit in rootUnits)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = unit.ABBREVIATION,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString(),
                    open = true,
                    type = "组织",
                };

                //循环向（根）父节点添加子节点
                treeModels.Add(rootTreeModel);

                AddTreeNode(allUnits, allUsers, rootTreeModel);

            }

            return treeModels;
        }

        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="allUnits">所有部门</param>
        /// <param name="allUsers">所有人员</param>
        /// <param name="parentTree">父节点</param>
        public static void AddTreeNode(List<SYS_UNITS> allUnits, List<SYS_USERS> allUsers, TreeModel parentTree)
        {
            //获得当前节点下的子节点
            List<SYS_UNITS> childrenUnits = allUnits.Where(t => t.PARENTID == decimal.Parse(parentTree.value)).OrderBy(t => t.SEQNUM)
                .ToList();

            //获得当前节点下的部门人员
            List<SYS_USERS> childrenUsers = allUsers.Where(t => t.UNITID == decimal.Parse(parentTree.value)).OrderBy(t => t.SEQNO)
                .ToList();

            foreach (SYS_USERS user in childrenUsers)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = user.USERNAME,
                    title ="姓名："+ user.USERNAME+"<br/>电话："+user.PHONE,
                    value = user.USERID.ToString(),
                    smsNumber = user.PHONE,
                    unitId = user.UNITID.ToString()
                };

                //循环向父节点添加人员
                parentTree.children.Add(treeModel);
            }

            foreach (var unit in childrenUnits)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = unit.ABBREVIATION,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString(),
                    type = "组织"
                };

                //循环向父节点添加部门
                parentTree.children.Add(treeModel);
                AddTreeNode(allUnits, allUsers, treeModel);
            }
        }

        /// <summary>
        /// 执行添加用户角色UserRoles
        /// </summary>
        /// <param name="UserId">当前用户</param>
        /// <param name="RoleId">用户拥有角色</param>
        /// <returns></returns>
        public static bool InsertRoleId(decimal UserId, decimal RoleId)
        {
            Entities db = new Entities();
            SYS_USERROLES roles = new SYS_USERROLES()
            {
                USERID = UserId,
                ROLEID = RoleId,
            };
            db.SYS_USERROLES.Add(roles);
            int i = db.SaveChanges();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询当前并且删除用户权限表信息
        /// </summary>
        /// <param name="UserId">当前用户</param>
        /// <returns></returns>
        public static bool deleteRoleIdByUserId(decimal UserId)
        {
            Entities db = new Entities();
            List<SYS_USERROLES> rolelist = db.SYS_USERROLES.Where(t => t.USERID == UserId).ToList();
            if (rolelist.Count > 0)
            {
                foreach (SYS_USERROLES u in rolelist)
                {
                    db.SYS_USERROLES.Remove(u);
                }
            }
            int i = db.SaveChanges();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns></returns>
        public static bool CheckAccountIsExist(string account)
        {
            bool flag = false;
            Entities db = new Entities();
            SYS_USERS user = db.SYS_USERS.SingleOrDefault(t => t.ACCOUNT == account && t.STATUSID == (decimal)StatusEnum.Normal);

            if (user != null)
            {
                flag = true;
            }

            return flag;
        }



        /// <summary>
        /// 判断账号是否存在，是否有前台权限，是否是街道人员
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string account)
        {
            Entities db = new Entities();
            IQueryable<UserInfo> list = db.SYS_USERS.Where(a => a.STATUSID == (decimal)StatusEnum.Normal && a.ACCOUNT == account)
            .Select(u => new UserInfo
            {
                RoleIDS = u.SYS_USERROLES,
                PositionID = u.USERPOSITIONID,

            });

            return list.FirstOrDefault();
        }
    }
}
