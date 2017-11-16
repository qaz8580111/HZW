using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.GroupBLLs
{
    public class GroupBLL
    {
        /// <summary>
        /// 获取所有的用户组
        /// </summary>
        /// <returns></returns>
        public static List<GROUP> GetAllGroups()
        {
            PLEEntities db = new PLEEntities();
            List<GROUP> allGroup = db.GROUPS.ToList();
            return allGroup;
        }

        /// <summary>
        /// 获取用户组节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes(string _userID)
        {
            PLEEntities db = new PLEEntities();
            decimal userID = 0.0M;
            IQueryable<GROUP> _groups = null;

            if (!string.IsNullOrWhiteSpace(_userID))
            {
                userID = decimal.Parse(_userID);

                _groups = from g in db.GROUPS
                          from ug in db.USERGROUPs
                          where g.GROUPID == ug.GROUPID
                          && ug.USERID == userID
                          select g;
            }

            List<TreeModel> treeModels = new List<TreeModel>();
            //根节点
            List<GROUP> rootGroup = GetAllGroups()
                .Where(t => t.PARENTID == null)
                .ToList();

            //遍历根节点
            foreach (var group in rootGroup)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = group.GROUPNAME,
                    title = group.GROUPNAME,
                    value = group.GROUPID.ToString(),
                    open = true,
                    type = "root"
                };

                treeModels.Add(rootTreeModel);

                AddTreeNode(rootTreeModel, group.GROUPID, _groups);

            }

            return treeModels;
        }

        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="parentTree">父节点</param>
        /// <param name="parentID">父节点标识</param>
        public static void AddTreeNode(TreeModel parentTree, decimal parentID, IQueryable<GROUP> _groups)
        {
            //获得当前根节点下的所有的子节点
            List<GROUP> groups = GetAllGroups().Where(t => t.PARENTID == parentID)
                .ToList();

            if (groups == null || groups.Count() == 0)
            {
                return;
            }

            foreach (var group in groups)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = group.GROUPNAME,
                    title = group.GROUPNAME,
                    value = group.GROUPID.ToString()
                };

                if (_groups != null)
                {
                    foreach (GROUP g in _groups)
                    {
                        if (group.GROUPID == g.GROUPID)
                        {
                            treeModel.@checked = true;
                        }
                    }
                }

                parentTree.children.Add(treeModel);

                AddTreeNode(treeModel, group.GROUPID, _groups);
            }
        }

        /// <summary>
        /// 根据父类用户组标识获取子类用户组列表
        /// </summary>
        /// <param name="groupID">父类用户组标识</param>
        /// <returns>子类用户组列表</returns>
        public static List<GROUP> GetChildGroup(decimal? groupID)
        {
            List<GROUP> list = GetAllGroups().ToList();
            if (groupID == null)
            {
                GROUP group = list.FirstOrDefault(t => t.PARENTID == null);
                list = list.Where(t => t.PARENTID == group.GROUPID).ToList();
            }
            else
            {
                list = list.Where(t => t.PARENTID == groupID).ToList();
            }

            return list;
        }

        /// <summary>
        /// 根据parentID查询用户组
        /// </summary>
        /// <param name="parentID">parentID</param>
        /// <returns></returns>
        public static GROUP GetGroupByParentID(decimal? parentID)
        {
            GROUP result = GetAllGroups().SingleOrDefault(t => t.PARENTID == parentID);
            return result;
        }

        /// <summary>
        /// 根据组标识获取组
        /// </summary>
        /// <param name="groupID">组标识</param>
        /// <returns></returns>
        public static GROUP GetGroupByGroupID(decimal groupID)
        {
            GROUP group = GetAllGroups().SingleOrDefault(t => t.GROUPID == groupID);

            return group;
        }

        /// <summary>
        /// 添加用户组
        /// </summary>
        public static void AddGroup(GROUP group)
        {
            PLEEntities db = new PLEEntities();
            db.GROUPS.Add(group);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改组
        /// </summary>
        /// <param name="group"></param>
        public static void ModifyGroup(GROUP group)
        {
            PLEEntities db = new PLEEntities();
            GROUP g = db.GROUPS.SingleOrDefault(t => t.GROUPID == group.GROUPID);
            g.GROUPNAME = group.GROUPNAME;
            db.SaveChanges();

        }

        public static string GetChildGroupCount(decimal? groupID)
        {
            PLEEntities db = new PLEEntities();

            return db.GROUPS.Where(t => t.PARENTID == groupID)
                .Count().ToString();
        }

        public static void DeleteGroup(decimal groupID)
        {
            PLEEntities db = new PLEEntities();
            GROUP group = db.GROUPS.SingleOrDefault(t => t.GROUPID == groupID);
            db.GROUPS.Remove(group);
            db.SaveChanges();
        }

        public static void AddUserGroup(decimal userID, decimal GroupID)
        {
            PLEEntities db = new PLEEntities();

            USERGROUP ug = new USERGROUP()
            {
                GROUPID = GroupID,
                USERID = userID
            };

            db.USERGROUPs.Add(ug);

            db.SaveChanges();
        }

        /// <summary>
        /// 根据用户标识获取用户组
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns></returns>
        public static IQueryable<GROUP> GetUserGroupsByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            var GroupNames = from g in db.GROUPS
                             from ug in db.USERGROUPs
                             where g.GROUPID == ug.GROUPID && ug.USERID == userID
                             select g;

            return GroupNames;
        }
    }
}
