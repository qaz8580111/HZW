using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.BLL.UnitBLLs;
using ZGM.BLL.UserBLLs;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.XTBGBLL
{
   public class OA_CONTACTSBLL
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public static IQueryable<OA_CONTACTS> GetAllCameras()
        {
            Entities db = new Entities();

            IQueryable<OA_CONTACTS> results = db.OA_CONTACTS;

            return results;
        }

       /// <summary>
       /// 合并树ztree
       /// </summary>
       /// <param name="userid"></param>
       /// <returns></returns>
        public static List<TreeModel> treeList(decimal userid)
        {
         List<TreeModel> User= GetTreeNodes();
         List<TreeModel> Packet = GetTreeNodesPacket(userid);
         List<TreeModel> list = new List<TreeModel>();
         list.AddRange(User);
         list.AddRange(Packet);
         list.Count();
         return list;
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
            List<SYS_USERS> allUsers =UserBLL.GetAllUsers()
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //根节点 所有单位parentId为null的根节点
            List<SYS_UNITS> rootUnits = allUnits.Where(t => t.PARENTID == null && t.UNITID == 1)
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
                    title = user.USERNAME,
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
        /// 获取组织结构树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodesPacket(decimal userid)
        {

            List<TreeModel> treeModels = new List<TreeModel>();

            //查出所有单位
            List<OA_CONTACTGROUPS> allUnits = OA_CONTACTGROUPSBLL.GetAllCameras().Where(t=>t.CREATEDUSERID==userid).ToList();
            //查出所有用户
            List<OA_CONTACTS> allUsers = GetAllCameras().ToList();

            //根节点 所有单位parentId为null的根节点
            List<OA_CONTACTGROUPS> rootUnits = allUnits
                .ToList();

            //遍历根节点
            foreach (var unit in rootUnits)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = unit.CONTACTGROUPNAME,
                    title = unit.CONTACTGROUPNAME,
                    value = unit.CONTACTGROUPID.ToString(),
                    open = true,
                    type = "组织",
                };
                //循环向（根）父节点添加子节点
                treeModels.Add(rootTreeModel);

                AddTreeNodePacket(allUnits, allUsers, rootTreeModel);

            }

            return treeModels;
        }


        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="allUnits">所有部门</param>
        /// <param name="allUsers">所有人员</param>
        /// <param name="parentTree">父节点</param>
        public static void AddTreeNodePacket(List<OA_CONTACTGROUPS> allUnits, List<OA_CONTACTS> allUsers, TreeModel parentTree)
        {
            //获得当前节点下的子节点
            List<OA_CONTACTGROUPS> childrenUnits = allUnits.Where(t => t.CONTACTGROUPID == decimal.Parse(parentTree.value)).OrderBy(t => t.SEQNO)
                .ToList();

            //获得当前节点下的部门人员
            List<OA_CONTACTS> childrenUsers = allUsers.Where(t => t.CONTACTGROUPID == decimal.Parse(parentTree.value)).ToList();

            foreach (OA_CONTACTS item in childrenUsers)
            {
                SYS_USERS usermodel = UserBLL.GetUserByUserID(decimal.Parse(item.USERID.ToString()));
                TreeModel treeModel = new TreeModel
                {
                    name = usermodel.USERNAME,
                    title = usermodel.USERNAME,
                    value = item.USERID.ToString(),
                    smsNumber=usermodel.PHONE,
                    unitId = item.CONTACTGROUPID.ToString()
                };

                //循环向父节点添加人员
                parentTree.children.Add(treeModel);
            }
        }




   


       /// <summary>
       /// 删除分组下面的人员
       /// </summary>
       /// <param name="CONTACTGROUPID"></param>
        public static void DeleteCONTACTS(string CONTACTGROUPID)
        {
            Entities db = new Entities();
            decimal id = decimal.Parse(CONTACTGROUPID);

            List<OA_CONTACTS> lists = db.OA_CONTACTS.Where(t => t.CONTACTGROUPID == id).ToList();

            foreach (var item in lists)
            {
                db.OA_CONTACTS.Remove(item);
            }
            db.SaveChanges();

        }

        /// <summary>
        /// 删除人员
        /// </summary>
        /// <param name="CONTACTGROUPID"></param>
        public static void DeleteCONTACTSUser(string Userid ,string ParentIDPid)
        {
            Entities db = new Entities();
            decimal id = decimal.Parse(Userid);
            decimal ParentIDP = decimal.Parse(ParentIDPid);
            List<OA_CONTACTS> lists = db.OA_CONTACTS.Where(t => t.USERID == id && t.CONTACTGROUPID == ParentIDP).ToList();

            foreach (var item in lists)
            {
                db.OA_CONTACTS.Remove(item);
            }
            db.SaveChanges();

        }


        /// <summary>
        /// 添加分组
        /// </summary>
        /// <param name="model"></param>
        public static void AddCONTACTS(OA_CONTACTS model)
        {
            Entities db = new Entities();
            db.OA_CONTACTS.Add(model);
            db.SaveChanges();
        }
    }
}
