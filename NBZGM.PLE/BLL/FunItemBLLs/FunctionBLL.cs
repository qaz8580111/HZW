using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Common.Enums;

namespace Taizhou.PLE.BLL.FunItemBLL
{
    public class FunctionBLL
    {
        public static decimal GetNewFunctionID()
        {
            PLEEntities db = new PLEEntities();

            string sql = "SELECT SEQ_FUNCTIONID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获得所有Function
        /// </summary>
        /// <returns></returns>
        public static IQueryable<FUNCTION> GetAllFunctionList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<FUNCTION> funcionList = db.FUNCTIONS.OrderBy(t => t.SEQNO);
            return funcionList;
        }

        /// <summary>
        /// 根据ID查询Function
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        public static FUNCTION GetFunctionByID(decimal functionID)
        {
            PLEEntities db = new PLEEntities();
            FUNCTION function = db.FUNCTIONS
                .SingleOrDefault(t => t.FUNCTIONID == functionID);
            return function;
        }

        /// <summary>
        /// 添加一个Function
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static bool AddFunction(FUNCTION function)
        {
            PLEEntities db = new PLEEntities();
            db.FUNCTIONS.Add(function);
            int result = db.SaveChanges();
            return result > 0 ? true : false;
        }

        public static bool EditFunction(FUNCTION newFunction)
        {
            PLEEntities db = new PLEEntities();

            var function = db.FUNCTIONS
                .SingleOrDefault(t => t.FUNCTIONID == newFunction.FUNCTIONID);

            function.NAME = newFunction.NAME;
            function.CODE = newFunction.CODE;
            function.PARENTID = newFunction.PARENTID;
            function.PATH = newFunction.PATH;
            function.URL = newFunction.URL;
            function.STATUSID = newFunction.STATUSID;
            function.ICONPATH = newFunction.ICONPATH;
            function.SEQNO = newFunction.SEQNO;

            int result = db.SaveChanges();
            return result > 0 ? true : false;
        }

        /// <summary>
        /// 根据用户标识获取该用户可操作的功能项
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns>用户可操作的功能项列表</returns>
        public static List<FUNCTION> GetFunctionsByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();

            return db.FUNCTIONS.Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO).ToList();

            //var results = from ur in db.USERROLES
            //              from rf in db.ROLEFUNCTIONS
            //              from f in db.FUNCTIONS
            //              where ur.USERID == userID
            //                && ur.ROLEID == rf.ROLEID
            //                && rf.FUNCTIONID == f.FUNCTIONID
            //                && f.STATUSID == (decimal)StatusEnum.Normal
            //              orderby f.SEQNO
            //              select f;

            //return results.ToList();
        }

        public static List<FunctionTreeModel> GetTotalFunctionsByRoleID(
            decimal? roleID)
        {
            PLEEntities db = new PLEEntities();

            //所有的功能项
            List<FUNCTION> allFunctions = db.FUNCTIONS
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //该角色已配置的功能项
            List<FUNCTION> checkedFunctions = new List<FUNCTION>();

            if (roleID != null)
            {
                checkedFunctions =
                    (from rf in db.ROLEFUNCTIONS
                     from f in db.FUNCTIONS
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
                    open = allFun.PARENTID == null ? true : false,
                    @checked = fun != null ? true : false,
                    pId = allFun.PARENTID == null ? 0 : allFun.PARENTID.Value
                }).ToList();
            return list;
        }

        public static List<FunctionTreeModel> GetTotalFunctionsByRoleID(List<decimal> roleIDs)
        {

            PLEEntities db = new PLEEntities();

            //所有的功能项
            List<FUNCTION> allFunctions = db.FUNCTIONS
                .Where(t => t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //该用户已经拥有的角色已配置的功能项
            List<FUNCTION> checkedFunctions = new List<FUNCTION>();

            foreach (decimal roleID in roleIDs)
            {
                List<FUNCTION> Functions =
                    (from rf in db.ROLEFUNCTIONS
                     from f in db.FUNCTIONS
                     where rf.FUNCTIONID == f.FUNCTIONID
                         && rf.ROLEID == roleID
                         && f.STATUSID == (decimal)StatusEnum.Normal
                     select f
                           ).ToList();

                foreach (FUNCTION function in Functions)
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
                    chkDisabled=true
                }).ToList();
            return list;

        }

        public static List<TreeModel> GetTreeNodes()
        {
            List<TreeModel> treeModels = new List<TreeModel>();

            List<FUNCTION> rootFunctions = GetAllFunctionList()
                .Where(t => t.PARENTID == null).ToList();

            //遍历根节点
            foreach (var function in rootFunctions)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = "<span style='color:#333;'><i class='" + function.ICONPATH + "'></i></span>"
                    + function.NAME,
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
            List<FUNCTION> functions = GetAllFunctionList().Where(t => t.PARENTID
                == parentID).ToList();

            foreach (var function in functions)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = "<span style='color:#333;'><i class='" + function.ICONPATH + "'></i></span>"
                    + function.NAME,
                    title = function.CODE,
                    value = function.FUNCTIONID.ToString(),
                };

                parentTree.children.Add(treeModel);
                AddTreeNode(treeModel, function.FUNCTIONID);
            }

        }

        public static IQueryable<FUNCTION> GetFunctionList(decimal? parentID)
        {
            PLEEntities db = new PLEEntities();
            IQueryable<FUNCTION> functionList = db.FUNCTIONS.OrderBy(t => t.SEQNO);

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

    }
}
