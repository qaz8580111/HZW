using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.PhoneModel;



namespace ZGM.BLL.ContactBLLs
{
    public class ContactBLL
    {

        /// <summary>
        /// 获取组织结构树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes(decimal UserUnitID)
        {
            List<TreeModel> treeModels = new List<TreeModel>();
            SYS_UNITS model = GetUnitByUnitID(UserUnitID).FirstOrDefault();
            if (model == null)
            {
                return treeModels;
            }
            //查出所有单位11
            List<SYS_UNITS> allUnits = GetAllUnits()
              .Where(a => a.PATH.Split('\\')[1] == model.PATH.Split('\\')[1] || a.UNITID == 1)
                .Where(t => t.STATUSID == 1)
                .OrderBy(a => a.SEQNUM)
                .ToList(); 
            //查出所有用户
            List<SYS_USERS> allUsers = GetAllUsers()
                .Where(t => t.STATUSID == 1)
                .ToList();
            //根节点 所有单位parentId为null的根节点
            List<SYS_UNITS> rootUnits = allUnits.Where(t => t.PARENTID == null && t.UNITID == 1)
                .OrderBy(a => a.SEQNUM)
                .ToList();
            //遍历根节点
            foreach (var unit in rootUnits)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = unit.ABBREVIATION,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString(),
                    pId = unit.PARENTID.ToString(),
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
        /// 根据单位标识获取单位
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns>单位</returns>
        public static IQueryable<SYS_UNITS> GetUnitByUnitID(decimal? unitID)
        {
            Entities db = new Entities();

            IQueryable<SYS_UNITS> results = db.SYS_UNITS
                .Where(t => t.UNITID == unitID && t.STATUSID == 1);

            return results;
        }

        /// <summary>
        /// 获取所有单位
        /// </summary>
        /// <returns></returns>
        public static List<SYS_UNITS> GetAllUnits()
        {
            Entities db = new Entities();

            List<SYS_UNITS> unitList = db.SYS_UNITS.Where(t => t
                .STATUSID == 1).ToList();

            return unitList;
        }

        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns>用户列表</returns>
        public static IQueryable<SYS_USERS> GetAllUsers()
        {
            Entities db = new Entities();

            IQueryable<SYS_USERS> results = db.SYS_USERS.Where(t => t
                .STATUSID == 1)
                .OrderBy(t => t.SEQNO);

            return results;
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
                    smsNumber = user.AVATAR,
                    sex = user.SEX,
                    birthday = user.BIRTHDAY.HasValue ? user.BIRTHDAY.Value.ToShortDateString() : "",
                    zfzbh = user.ZFZBH,
                    pId = user.UNITID.ToString(),
                    phone = user.PHONE,
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
                    pId = unit.PARENTID.ToString(),
                    value = unit.UNITID.ToString(),
                    type = "组织"
                };

                //循环向父节点添加部门
                parentTree.children.Add(treeModel);
                AddTreeNode(allUnits, allUsers, treeModel);
            }
        }
    }
}
