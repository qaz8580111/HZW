using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model.CustomModels;
using Taizhou.PLE.BLL.UnitBLLs;
using System.Data.Objects.SqlClient;
using System.Data.Entity.Validation;
using Taizhou.PLE.BLL.GroupBLLs;
using Taizhou.PLE.Model.WebServiceModels;

namespace Taizhou.PLE.BLL.UserBLLs
{
    public class UserBLL
    {
        public static UserInfo GetUserInfoByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            var results =
                 from u in db.USERS
                 join unit1 in db.UNITS on u.UNITID equals unit1.UNITID
                 join unit2 in db.UNITS on u.REGIONID equals unit2.UNITID
                 join up in db.USERPOSITIONS
                     on u.USERPOSITIONID equals up.USERPOSITIONID into upTemp
                 from up in upTemp.DefaultIfEmpty()
                 where u.USERID == userID
                 select new UserInfo
                 {
                     UserID = u.USERID,
                     UserName = u.USERNAME,
                     UnitID = unit1.UNITID,
                     UnitName = unit1.UNITNAME,
                     RegionID = unit2.UNITID,
                     RegionName = unit2.UNITNAME,
                     PositionID = up.USERPOSITIONID,
                     PositionName = up.USERPOSITIONNAME
                 };

            return results.SingleOrDefault();
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<USER> GetAllUsers()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<USER> results = db.USERS.Where(t => t
                .STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);

            return results;
        }

        public static USER GetUserByUserID_All(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            USER result = db.USERS.SingleOrDefault(t => t
               .USERID == userID);

            return result;
        }

        /// <summary>
        /// 根据unitid和职务获取名字
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="positionId"></param>
        /// <returns></returns>
        public static string name(decimal unitId, int positionId)
        {
            PLEEntities db = new PLEEntities();
            USER user = db.USERS.SingleOrDefault(a => a.UNITID == unitId && a.USERPOSITIONID == positionId);
            if (user != null)
            {
                return user.USERNAME;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据用户标识获取用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static USER GetUserByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            return db.USERS.SingleOrDefault(t => t.USERID == userID
                && t.STATUSID == (decimal)StatusEnum.Normal);
        }

        /// <summary>
        /// 根据用户标识获取用户名
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static string GetUserNameByUserID(decimal userID)
        {
            USER model = GetUserByUserID(userID);
            if (model != null)
                return model.USERNAME;
            else
                return "";
        }

        /// <summary>
        /// 获取自定义用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>根据用户ID获取自定义用户</returns>
        public static Taizhou.PLE.Model.CaseWorkflowModels.User GetUserByID(decimal id)
        {
            PLEEntities db = new PLEEntities();
            USER user = db.USERS.SingleOrDefault(t => t.USERID == id);

            Taizhou.PLE.Model.CaseWorkflowModels.User result =
                new Taizhou.PLE.Model.CaseWorkflowModels.User
                {
                    UserID = user.USERID,
                    UserName = user.USERNAME
                };

            return result;
        }

        /// <summary>
        /// 根据用户ID查询用户数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static USER GetUserSingleByUserId(decimal userId)
        {
            PLEEntities db = new PLEEntities();
            USER userModel = db.USERS.SingleOrDefault(a => a.USERID == userId);
            return userModel;
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public static IQueryable<USER> GetAllUser()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<USER> list = db.USERS;
            return list;
        }

        /// <summary>
        /// 根据unitID标识和职位ID获取中队人员
        /// </summary>
        /// <returns></returns>
        public static List<USER> GetZDRYByUnitID(decimal UNITID)
        {
            PLEEntities db = new PLEEntities();
            List<USER> results = db.USERS
                .Where(t => t.UNITID == UNITID
                    && t.STATUSID == (decimal)StatusEnum.Normal)
                    .OrderBy(t=>t.SEQNO).ToList();
            return results;
        }

        /// <summary>
        /// 根据unitID标识和职位ID获取中队人员的中队长
        /// </summary>
        /// <returns></returns>
        public static List<USER> GetZDRYByUnitID(decimal UNITID,decimal UserpositionID)
        {
            PLEEntities db = new PLEEntities();
            List<USER> results = db.USERS
                .Where(t => t.UNITID == UNITID
                    && t.STATUSID == (decimal)StatusEnum.Normal && t.USERPOSITIONID==UserpositionID)
                    .OrderBy(t => t.SEQNO).ToList();
            return results;
        }
        /// <summary>
        /// 根据中队标识和职务获取大队长用户信息
        /// </summary>
        /// <param name="parentUnitID">中队标识</param>
        /// <param name="PositionID">职务标识</param>
        /// <returns>大队长信息</returns>
        public static USER GetUserBySubUnitIDAndPositionID(
            decimal unitID, decimal PositionID)
        {
            PLEEntities db = new PLEEntities();

            USER user = (from u in db.USERS
                         from un in db.UNITS
                         where u.UNITID == un.PARENTID
                             && u.USERPOSITIONID == PositionID
                             && un.UNITID == unitID
                         select u
                         ).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户</param>
        public static void AddUser(USER user, string avatar, string[] userGroupAttr)
        {
            PLEEntities db = new PLEEntities();

            decimal userID = GetNewUserID();

            user.USERID = userID;

            foreach (string userGroup in userGroupAttr)
            {
                if (userGroup == "")
                    continue;

                GroupBLL.AddUserGroup(userID, decimal.Parse(userGroup));
            }

            USERARCHIVE attachment = new USERARCHIVE()
            {
                USERID = userID,
                AVATAR = avatar
            };

            db.USERARCHIVES.Add(attachment);

            db.USERS.Add(user);



            db.SaveChanges();
        }

        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        public static void DeleteUser(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            USER user = db.USERS
                .SingleOrDefault(t => t.USERID == userID
                    && t.STATUSID == (decimal)StatusEnum.Normal);

            user.STATUSID = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="_user">用户</param>
        public static void ModifyUser(USER _user, string avatar, string userGroupIDS)
        {
            PLEEntities db = new PLEEntities();

            USER user = db.USERS.SingleOrDefault(t => t.USERID == _user.USERID
                && t.STATUSID == (decimal)StatusEnum.Normal);

            user.USERNAME = _user.USERNAME;
            user.ACCOUNT = _user.ACCOUNT;
            user.USERPOSITIONID = _user.USERPOSITIONID;
            user.USERCATEGORYID = _user.USERCATEGORYID;
            user.WORKZZ = _user.WORKZZ;
            user.SMSNUMBERS = _user.SMSNUMBERS;
            user.RTXACCOUNT = _user.RTXACCOUNT;
            user .ZFZBH = _user.ZFZBH;
            user.SEQNO = _user.SEQNO;

            if (!string.IsNullOrWhiteSpace(avatar))
            {
                USERARCHIVE attachment = db.USERARCHIVES
                    .SingleOrDefault(t => t.USERID == _user.USERID);

                //判断数据库里面时候有该用户
                if (attachment == null)
                {
                    attachment = new USERARCHIVE();
                    attachment.USERID = _user.USERID;
                    attachment.AVATAR = avatar;
                    db.USERARCHIVES.Add(attachment);
                }
                else
                {
                    attachment.AVATAR = avatar;
                }
            }

            IQueryable<USERGROUP> ugs = db.USERGROUPs
                .Where(t => t.USERID == _user.USERID);

            foreach (USERGROUP ug in ugs)
            {
                db.USERGROUPs.Remove(ug);
            }

            if (!string.IsNullOrWhiteSpace(userGroupIDS))
            {
                foreach (string groupID in userGroupIDS.Split(','))
                {
                    USERGROUP ug = new USERGROUP()
                    {
                        USERID = _user.USERID,
                        GROUPID = decimal.Parse(groupID)
                    };

                    db.USERGROUPs.Add(ug);
                }
            }

            db.SaveChanges();
        }

        /// <summary>
        /// 获取组织结构树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes()
        {

            List<TreeModel> treeModels = new List<TreeModel>();

            //查出所有单位
            List<UNIT> allUnits = UnitBLL.GetAllUnits()
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();
            //查出所有用户
            List<USER> allUsers = GetAllUsers()
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //根节点 所有单位parentId为null的根节点
            List<UNIT> rootUnits = allUnits.Where(t => t.PARENTID == null&&t.UNITID==10000)
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
        public static void AddTreeNode(List<UNIT> allUnits, List<USER> allUsers, TreeModel parentTree)
        {
            //获得当前节点下的子节点
            List<UNIT> childrenUnits = allUnits.Where(t => t.PARENTID == decimal.Parse(parentTree.value)).OrderBy(t=>t.SEQNO)
                .ToList();

            //获得当前节点下的部门人员
            List<USER> childrenUsers = allUsers.Where(t => t.UNITID == decimal.Parse(parentTree.value)).OrderBy(t=>t.SEQNO)
                .ToList();

            foreach (USER user in childrenUsers)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = user.USERNAME,
                    title = user.USERNAME,
                    value = user.USERID.ToString(),
                    smsNumber = user.SMSNUMBERS
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
        /// 获取用户组树
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodesByGroup(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            //获取所有用户
            List<USER> allUsers = db.USERS.ToList();
            //获取所有用户组
            List<USERGROUP> allUserGroup = db.USERGROUPs.ToList();

            //获得所有组的节点
            List<TreeModel> treeModelList =
                (from model in db.GROUPS.Where(t => t.CREATEUSERID == userID).ToList()
                 select new TreeModel
                 {
                     name = model.GROUPNAME,
                     title = model.GROUPNAME,
                     value = model.GROUPID.ToString(),
                     pId = model.PARENTID == null ? "-1" : model.PARENTID.ToString(),
                     id = model.GROUPID.ToString(),
                     open = true,
                     type = "root"
                 }).ToList();

            foreach (TreeModel tm in treeModelList)
            {
                decimal groupID = decimal.Parse(tm.value);

                //根据用户组标识查找
                List<TreeModel> userGroups =
                    (from ug in allUserGroup
                     .Where(t => t.GROUPID == groupID).ToList()
                     select new TreeModel
                     {
                         name = allUsers.FirstOrDefault(t => t.USERID == ug.USERID)
                         == null ? "" : allUsers.FirstOrDefault(t => t.USERID == ug.USERID)
                         .USERNAME,
                         smsNumber = allUsers.FirstOrDefault(t => t.USERID == ug.USERID)
                         == null ? "" : allUsers.FirstOrDefault(t => t.USERID == ug.USERID)
                         .SMSNUMBERS,
                         pId = ug.GROUPID.ToString(),
                         value = ug.USERID.ToString()
                     }).ToList();

                tm.children = userGroups;
            }
            return treeModelList;
        }


        /// <summary>
        /// 获取单位下的用户
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns></returns>
        public static IQueryable<USER> GetTotalUsersByUnitID(decimal? unitID)
        {
           // string strUnitID = "\\" + unitID + "\\";
            string strUnitID = unitID.ToString();
            PLEEntities db = new PLEEntities();

            var results = from un in db.UNITS
                          from u in db.USERS
                          where un.PATH.Contains(strUnitID)
                          && un.UNITID == u.UNITID
                          select u;

            return results.Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);
        }

        /// <summary>
        /// 根据单位标识获取该单位下的所有用户
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns>同一单位的用户列表</returns>
        public static IQueryable<USER> GetUsersByUserID(decimal? userID)
        {
            PLEEntities db = new PLEEntities();

            var user = db.USERS.SingleOrDefault(t => t.USERID == userID);

            var results = db.USERS.Where(t => t.UNITID == user.UNITID
                && t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);

            return results;
        }

        /// <summary>
        /// 获取职务列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<USERPOSITION> GetAllPositon()
        {
            PLEEntities db = new PLEEntities();

            IQueryable<USERPOSITION> results = db.USERPOSITIONS.OrderBy(t => t.USERPOSITIONID);

            return results;
        }

        /// <summary>
        /// 判断帐号是否可以存在
        /// </summary>
        /// <param name="account">帐号</param>
        /// <returns></returns>
        public static bool validationAccountIsExist(string account)
        {
            PLEEntities db = new PLEEntities();

            if (db.USERS.Where(t => t.ACCOUNT == account
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
        /// 判断RTX帐号是否存在
        /// </summary>
        /// <param name="RTXaccount">RTX帐号</param>
        /// <returns></returns>
        public static bool validationRTXAccountIsExist(string RTXaccount)
        {
            PLEEntities db = new PLEEntities();

            if (db.USERS.Where(t => t.RTXACCOUNT == RTXaccount
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
        public static bool validationAccountIsCanEdit(string account, decimal userID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<USER> users = db.USERS.Where(t => t.ACCOUNT == account
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
        /// 判断RTX帐号是否可以修改
        /// </summary>
        /// <param name="RTXAccount">RTX帐号</param>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static bool validationRTXAccountIsCanEdit(string RTXAccount, decimal userID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<USER> users = db.USERS.Where(t => t.RTXACCOUNT == RTXAccount
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
        /// 获取一个新的用户标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewUserID()
        {
            PLEEntities db = new PLEEntities();

            string sql = "SELECT SEQ_USERID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        public static USERARCHIVE GetUserAttachmentByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            USERARCHIVE userAttachment = db.USERARCHIVES
                .SingleOrDefault(t => t.USERID == userID);

            return userAttachment;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="ysmm">原始密码</param>
        /// <param name="xmm">新密码</param>
        /// <param name="userId"></param>
        /// <returns>布尔型</returns>
        public static bool ModifyUserPassword(string ysmm,
            string xmm, decimal userId)
        {
            bool flag = true;

            PLEEntities db = new PLEEntities();

            USER user = db.USERS.SingleOrDefault(t => t.USERID == userId);

            if (user.PASSWORD == ysmm)
            {
                user.PASSWORD = xmm;
                db.SaveChanges();
            }
            else
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 登录校验
        /// </summary>
        /// <param name="account">帐号</param>
        /// <param name="password">密码</param>
        /// <returns>根据账号密码获取登录用户</returns>
        public static UserInfo Login(string account, string password)
        {
            PLEEntities db = new PLEEntities();

            var results =
               from u in db.USERS
               join unit1 in db.UNITS on u.UNITID equals unit1.UNITID
               join unit2 in db.UNITS on u.REGIONID equals unit2.UNITID
               join up in db.USERPOSITIONS
                   on u.USERPOSITIONID equals up.USERPOSITIONID into upTemp
               from up in upTemp.DefaultIfEmpty()
               where u.ACCOUNT == account
               && u.PASSWORD == password
               && u.STATUSID == (decimal)StatusEnum.Normal
               select new UserInfo
               {
                   UserID = u.USERID,
                   UserName = u.USERNAME,
                   UnitID = unit1.UNITID,
                   UnitName = unit1.UNITNAME,
                   RegionID = unit2.UNITID,
                   RegionName = unit2.UNITNAME,
                   PositionID = up.USERPOSITIONID,
                   PositionName = up.USERPOSITIONNAME
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

            PLEEntities db = new PLEEntities();

            USER user = db.USERS.SingleOrDefault(t => t.ACCOUNT == account
                && t.PASSWORD == password && t.STATUSID == (decimal)StatusEnum.Normal);

            if (user != null)
            {
                flag = true;
            }

            return flag;
        }

        /// <summary>
        /// 根据用户标识获取用户手机号码
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns>用户手机号码</returns>
        public static string GetUserSmsNumbersByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            var result = db.USERS.SingleOrDefault(t => t.STATUSID
                == (decimal)StatusEnum.Normal && t.USERID == userID);

            return result == null ? "" : result.SMSNUMBERS;
        }

        /// <summary>
        /// 获取所有的干部类别
        /// </summary>
        /// <returns></returns>
        public static IQueryable<USERCATEGORy> GetAllUserCategories()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<USERCATEGORy> result = db.USERCATEGORIES;
            return result;
        }

        public static IQueryable<USER> GetUserByPositonID(decimal positionID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<USER> results = db.USERS
                .Where(t => t.USERPOSITIONID == positionID).OrderBy(t => t.USERID);

            return results;
        }

        /// <summary>
        /// 根据LeaderPositionID 返回用户集合
        /// </summary>
        /// <param name="LeaderPositionID">LeaderPositionID</param>
        /// <returns>用户集合</returns>
        public static IQueryable<USER> GetUserListByPosition(decimal LeaderPositionID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<USER> userList = db.USERS.Where(t => t.USERPOSITIONID == LeaderPositionID);

            return userList;
        }
        /// <summary>
        /// 根据单位类别标识返回单位列表
        /// </summary>
        /// <param name="unitID">单位类别标识</param>
        /// <returns>单位列表</returns>
        public static List<USER> GetUnitByUserTypeID(decimal unitid)
        {
            PLEEntities db = new PLEEntities();
            List<USER> userlists = db.USERS.Where
               (t => t.UNITID == unitid).ToList();
            return userlists;
        }
        /// <summary>
        /// 根据UnitID 返回用户集合
        /// </summary>
        /// <returns>用户集合</returns>
        public static IQueryable<USER> GetUsersByUnitID(decimal unitID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<USER> results = db.USERS
                .Where(t => t.UNITID == unitID
                    && t.STATUSID == (int)StatusEnum.Normal);

            return results;
        }

        /// <summary>
        /// 获取当前用户下用户通讯录
        /// </summary>
        /// <param name="UserID">用户标识</param>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodesByUserID(decimal UserID)
        {
            PLEEntities db = new PLEEntities();
            //所有联系人
            List<CONTACT> contact = db.CONTACTS.ToList();
            //获取通讯录组
            List<TreeModel> ContactsGroupsTreeModel =
                (from uab in db.CONTACTSGROUPS
                     .Where(t => t.CREATEDUSERID == UserID).OrderBy(t => t.SEQNO).ToList()
                 select new TreeModel
                 {
                     name = uab.CONTACTSGROUPNAME,
                     type = "root",
                     open = true,
                     id = uab.CONTACTSGROUPID.ToString(),
                     value = uab.CONTACTSGROUPID.ToString(),
                     title = uab.CONTACTSGROUPNAME
                 }).ToList();

            //遍历通讯录组向通讯录组里添加联系人
            foreach (TreeModel tm in ContactsGroupsTreeModel)
            {
                decimal contactGroupId = decimal.Parse(tm.value);

                List<TreeModel> contactTreeModel =
                    (from ab in contact.Where(t => t.CONTACTGROUPID == contactGroupId)
                         .ToList()
                     select new TreeModel
                     {
                         name = ab.CONTACTNAME + "(" + (ab.PHONENUMBER == "" || ab.PHONENUMBER == null ? "无号码" : ab.PHONENUMBER) + ")",
                         smsNumber = ab.PHONENUMBER,
                         pId = ab.CONTACTGROUPID.ToString(),
                         value = ab.CONTACTGROUPID.ToString(),
                         id = "-" + ab.CONTACTGROUPID
                     }).ToList();

                tm.children = contactTreeModel;
            }

            return ContactsGroupsTreeModel;
        }

        /// <summary>
        /// 通过单位标识和职务标识获取用户标识目前不需要
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="userPositionID"></param>
        /// <returns></returns>
        //public static decimal GetUserIDByUnitIDANDPositionID(string strUnitID, decimal userPositionID)
        //{
        //    decimal unitID = decimal.Parse(strUnitID);
        //    PLEEntities db = new PLEEntities();
        //    USER user = db.USERS.SingleOrDefault(t => t.UNITID == unitID
        //        && t.USERPOSITIONID == userPositionID);
        //    return user.USERID;
        //}
        /// <summary>
        /// 根据单位标示返回同一个单位下的所有用户
        /// </summary>
        /// <param name="UnitID">单位标示</param>
        /// <returns></returns>
        public static List<USER> GetUsersByUserUnitID(decimal UnitID)
        {
            PLEEntities db = new PLEEntities();
            List<USER> list = db.USERS.Where(u => u.UNITID == UnitID
                && u.STATUSID == (decimal)StatusEnum.Normal).ToList();
            return list;
        }

        /// <summary>
        /// 根据登录账号及登录密码获取用户对象
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <returns>用户对象</returns>
        public static Taizhou.PLE.Model.WebServiceModels.User SignIn
            (string account, string password)
        {
            PLEEntities db = new PLEEntities();

            decimal statusID = (decimal)StatusEnum.Normal;

            Taizhou.PLE.Model.WebServiceModels.User user =
                (from u in db.USERS
                 where u.ACCOUNT == account
                    && u.PASSWORD == password
                    && u.STATUSID == statusID
                 select new Taizhou.PLE.Model.WebServiceModels.User
                 {
                     userID = (int)u.USERID,
                     userName = u.USERNAME,
                     unitID = (int)u.UNITID,
                     account = u.ACCOUNT,
                     password = u.PASSWORD,
                     userPositionID = u.USERPOSITIONID != null ?
                     (int?)u.USERPOSITIONID : null,
                     statusID = u.STATUSID != null ? (int?)u.STATUSID : null,
                     seqno = u.SEQNO != null ? (int?)u.SEQNO : null,
                     RTXAccount = u.RTXACCOUNT,
                     SMSNumbers = u.SMSNUMBERS,
                     userCategoryID = u.USERCATEGORYID != null ?
                     (int?)u.USERCATEGORYID : null,
                     categoryID = u.CATEGORYID != null ?
                     (int?)u.CATEGORYID : null,
                     regionID = u.REGIONID != null ? (int?)u.REGIONID : null
                 }).SingleOrDefault();

            if (user != null)
            {
                user.unitName = UnitBLL
                    .GetUnitNameByUnitID((decimal)user.unitID);

                WTUSERRELATION relation = db.WTUSERRELATIONS
                    .SingleOrDefault(t => t.USERID == user.userID);

                user.WTUserID = relation != null ? relation.WTUSERID : null;
                user.WTUnitID = relation != null ? relation.WTUNITID : null;
            }

            return user;
        }

        /// <summary>
        /// 根据工作单位标示返回当下的所有用户
        /// </summary>
        /// <param name="uid">单位标识</param>
        /// <returns>用户集合</returns>
        public static IQueryable<USER> GetUserListByUID(decimal? uid)
        {
            string unitsid = "\\" + uid.ToString() + "\\";
            PLEEntities db = new PLEEntities();
            var results = from user in db.USERS
                          join units in db.UNITS on user.UNITID equals units.UNITID
                          where units.PATH.Contains(unitsid)
                          select user;
            return results;
        }
    }
}
