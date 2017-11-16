using Common.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.FunItemBLLs
{
    public class FunctionBLL
    {
        /// <summary>
        /// 根据用户标识获取该用户可操作的功能项
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns>用户可操作的功能项列表</returns>
        public static List<SYS_FUNCTIONS> GetFunctionsByUserID(decimal userID)
        {
            Entities db = new Entities();

            List<SYS_FUNCTIONS> list = db.SYS_FUNCTIONS.Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNUM).ToList();
            return list;
        }

        /// <summary>
        /// 获得所有Function
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SYS_FUNCTIONS> GetAllFunctionList()
        {
            Entities db = new Entities();
            IQueryable<SYS_FUNCTIONS> funcionList = db.SYS_FUNCTIONS.OrderBy(t => t.SEQNUM);
            return funcionList;
        }

        /// <summary>
        /// 获得所有Function列表
        /// </summary>
        /// <returns></returns>
        public static List<SYS_FUNCTIONS> GetAllFunctions()
        {
            Entities db = new Entities();
            List<SYS_FUNCTIONS> funcionList = db.SYS_FUNCTIONS.OrderBy(t => t.SEQNUM).ToList();
            return funcionList;
        }

        /// <summary>
        /// 根据ID查询Function
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        public static SYS_FUNCTIONS GetFunctionByID(decimal functionID)
        {
            Entities db = new Entities();
            SYS_FUNCTIONS function = db.SYS_FUNCTIONS
                .SingleOrDefault(t => t.FUNCTIONID == functionID);
            return function;
        }

        /// <summary>
        /// 获取新建function的ID
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        public static decimal GetNewFunctionID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_FUNCTIONID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 根据父元素查询功能
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static IQueryable<SYS_FUNCTIONS> GetFunctionListByParentID(decimal? parentID)
        {
            Entities db = new Entities();
            IQueryable<SYS_FUNCTIONS> functionList = db.SYS_FUNCTIONS
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal).OrderBy(t => t.SEQNUM);

            if (parentID == null)
            {
                functionList = functionList.Where(t => t.PARENTID == null);
            }
            else
            {
                functionList = functionList.Where(t => t.PARENTID == parentID);
            }
            return functionList;
        }

        /// <summary>
        /// 全部一级菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SYS_FUNCTIONS> GetTopFunctions()
        {
            Entities db = new Entities();

            List<SYS_FUNCTIONS> list = db.SYS_FUNCTIONS.Where(t => t.STATUSID == (decimal)StatusEnum.Normal && t.PARENTID == null)
                .OrderBy(t => t.SEQNUM).ToList();
            return list;
        }
        /// <summary>
        /// 全部一级菜单根据权限(不包括协同单位)
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SYS_FUNCTIONS> GetTopFunctions(decimal UserID)
        {
            Entities db = new Entities();

            List<SYS_FUNCTIONS> list = (from ur in db.SYS_USERROLES
                                        from rf in db.SYS_ROLEFUNCTIONS
                                        from f in db.SYS_FUNCTIONS
                                        where ur.ROLEID == rf.ROLEID
                                        && rf.FUNCTIONID == f.FUNCTIONID
                                        && ur.USERID == UserID
                                        && f.PARENTID == null
                                        && f.FUNCTIONID != 6
                                        && f.CODE.Contains("XTBG") == false
                                        select f).Distinct().OrderBy(t => t.SEQNUM).ToList();
            return list;
        }

        /// <summary>
        /// 二级菜单根据权限
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<SYS_FUNCTIONS> GetLeftFunctions(decimal UserID, decimal topid)
        {
            Entities db = new Entities();

            List<SYS_FUNCTIONS> list = (from ur in db.SYS_USERROLES
                                        from rf in db.SYS_ROLEFUNCTIONS
                                        from f in db.SYS_FUNCTIONS
                                        where ur.ROLEID == rf.ROLEID
                                        && rf.FUNCTIONID == f.FUNCTIONID
                                        && ur.USERID == UserID
                                        && f.PARENTID == topid
                                        select f).Distinct().OrderBy(t => t.SEQNUM).ToList();
            return list;
        }


        /// <summary>
        /// 根据一级菜单，查询二级菜单
        /// </summary>
        /// <param name="topid"></param>
        /// <returns></returns>
        public static List<SYS_FUNCTIONS> GetLeftFunctions(decimal topid)
        {
            Entities db = new Entities();

            List<SYS_FUNCTIONS> list = db.SYS_FUNCTIONS.Where(t => t.STATUSID == (decimal)StatusEnum.Normal && t.PARENTID == topid)
                .OrderBy(t => t.SEQNUM).ToList();
            return list;
        }

        /// <summary>
        /// 根绝角色获取共享想
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public static List<FunctionTreeModel> GetTotalFunctionsByRoleID(
         decimal? roleID)
        {
            Entities db = new Entities();

            //所有的功能项
            List<SYS_FUNCTIONS> allFunctions = db.SYS_FUNCTIONS
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //该角色已配置的功能项
            List<SYS_FUNCTIONS> checkedFunctions = new List<SYS_FUNCTIONS>();

            if (roleID != null)
            {
                checkedFunctions =
                    (from rf in db.SYS_ROLEFUNCTIONS
                     from f in db.SYS_FUNCTIONS
                     where rf.FUNCTIONID == f.FUNCTIONID
                         && rf.ROLEID == roleID
                         && f.STATUSID == (decimal)StatusEnum.Normal
                     select f
                    ).ToList();
            }

            List<FunctionTreeModel> list = (
                from allFun in allFunctions
                join chFun in checkedFunctions
                on allFun.FUNCTIONID equals chFun.FUNCTIONID into temp
                from fun in temp.DefaultIfEmpty()
                select new FunctionTreeModel
                {
                    id = allFun.FUNCTIONID,
                    name = allFun.NAME,
                    open = fun == null ? false : false,
                    //open = allFun.PARENTID == null ? true : false,
                    @checked = fun != null ? true : false,
                    pId = allFun.PARENTID == null ? 0 : allFun.PARENTID.Value
                }).ToList();
            return list;
        }

        public static List<FunctionTreeModel> GetTotalFunctionsByRoleID(List<decimal> roleIDs)
        {

            Entities db = new Entities();

            //所有的功能项
            List<SYS_FUNCTIONS> allFunctions = db.SYS_FUNCTIONS
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //该用户已经拥有的角色已配置的功能项
            List<SYS_FUNCTIONS> checkedFunctions = new List<SYS_FUNCTIONS>();

            foreach (decimal roleID in roleIDs)
            {
                List<SYS_FUNCTIONS> Functions =
                    (from rf in db.SYS_ROLEFUNCTIONS
                     from f in db.SYS_FUNCTIONS
                     where rf.FUNCTIONID == f.FUNCTIONID
                         && rf.ROLEID == roleID
                         && f.STATUSID == (decimal)StatusEnum.Normal
                     select f
                           ).ToList();

                foreach (SYS_FUNCTIONS function in Functions)
                {
                    if (checkedFunctions.Where(t => t.FUNCTIONID == function.FUNCTIONID)
                        .Count() == 0)
                    {
                        checkedFunctions.Add(function);
                    }
                }
            }

            List<FunctionTreeModel> list = (
                from allFun in allFunctions
                join chFun in checkedFunctions
                on allFun.FUNCTIONID equals chFun.FUNCTIONID into temp
                from fun in temp.DefaultIfEmpty()
                select new FunctionTreeModel
                {
                    id = allFun.FUNCTIONID,
                    name = allFun.NAME,
                    open = allFun.PARENTID == null ? true : false,
                    @checked = fun != null ? true : false,
                    pId = allFun.PARENTID == null ? 0 : allFun.PARENTID.Value,
                    chkDisabled = true
                }).ToList();
            return list;

        }


        /// <summary>
        /// 添加一个Function
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static bool AddFunction(SYS_FUNCTIONS function)
        {
            Entities db = new Entities();
            db.SYS_FUNCTIONS.Add(function);
            int result = db.SaveChanges();
            return result > 0 ? true : false;
        }

        public static bool EditFunction(SYS_FUNCTIONS model)
        {
            Entities db = new Entities();

            var function = db.SYS_FUNCTIONS
                .SingleOrDefault(t => t.FUNCTIONID == model.FUNCTIONID);

            function.NAME = model.NAME;
            function.CODE = model.CODE;
            function.PARENTID = model.PARENTID;
            function.PATH = model.PATH;
            function.URL = model.URL;
            function.STATUSID = model.STATUSID;
            function.ICONPATH = model.ICONPATH;
            function.SEQNUM = model.SEQNUM;

            int result = db.SaveChanges();
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 删除功能项
        /// </summary>
        /// <param name="userID">用户标识</param>
        public static void DeleteFunction(decimal FunctionID)
        {
            Entities db = new Entities();

            SYS_FUNCTIONS model = db.SYS_FUNCTIONS
                .SingleOrDefault(t => t.FUNCTIONID == FunctionID
                    && t.STATUSID == (decimal)StatusEnum.Normal);

            model.STATUSID = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }
        /// <summary>
        /// 获取组织结构树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes()
        {
            Entities db = new Entities();

            List<TreeModel> treeModels = new List<TreeModel>();

            //查出所有功能项
            List<SYS_FUNCTIONS> allfunctions = GetTopFunctions();

            //遍历根节点
            foreach (var function in allfunctions)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = function.NAME,
                    title = function.CODE,
                    value = function.FUNCTIONID.ToString(),
                    open = true
                };

                treeModels.Add(rootTreeModel);

                AddTreeNode(rootTreeModel, function.FUNCTIONID);

            }

            return treeModels;
        }
        public static void AddTreeNode(TreeModel parentTree, decimal parentID)
        {
            //获得当前根节点下的所有的子节点
            List<SYS_FUNCTIONS> functions = GetAllFunctions().Where(t => t.PARENTID
                == parentID && t.STATUSID == (decimal)StatusEnum.Normal).ToList();

            foreach (var function in functions)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = function.NAME,
                    title = function.CODE,
                    value = function.FUNCTIONID.ToString(),
                };

                parentTree.children.Add(treeModel);
                AddTreeNode(treeModel, function.FUNCTIONID);
            }

        }
    }
}
