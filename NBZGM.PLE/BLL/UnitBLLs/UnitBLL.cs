using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Common.Enums;
using Taizhou.PLE.Model.CommonModel;
using Taizhou.PLE.Common.Enums.CaseEnums;


namespace Taizhou.PLE.BLL.UnitBLLs
{
    public class UnitBLL
    {
        /// <summary>
        /// 根据单位标识获取单位
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns>单位</returns>
        public static IQueryable<UNIT> GetUnitByUnitID(decimal? unitID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<UNIT> results = db.UNITS
                .Where(t => t.UNITID == unitID && t.STATUSID == 1);

            return results;
        }

        /// <summary>
        /// 获取所有单位
        /// </summary>
        /// <returns></returns>
        public static List<UNIT> GetAllUnits()
        {
            PLEEntities db = new PLEEntities();

            List<UNIT> unitList = db.UNITS.Where(t => t
                .STATUSID == (decimal)StatusEnum.Normal).ToList();

            return unitList;
        }

        /// <summary>
        /// 获取子类单位
        /// </summary>
        /// <param name="parentUnitID">父类单位标识</param>
        /// <returns>子类列表</returns>
        public static IQueryable<UNIT> GetChildUnit(decimal? parentUnitID)
        {
            PLEEntities db = new PLEEntities();

            IQueryable<UNIT> result = db.UNITS
                .Where(t => t.PARENTID == parentUnitID
                    && t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO);

            return result;
        }

        /// <summary>
        /// 获取树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes()
        {
            List<TreeModel> treeModels = new List<TreeModel>();
            //根节点
            List<UNIT> rootUnits = GetAllUnits().Where(t => t.PARENTID == null
                && t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //遍历根节点
            foreach (var unit in rootUnits)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = unit.ABBREVIATION,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString(),
                    open = true
                };

                treeModels.Add(rootTreeModel);

                AddTreeNode(rootTreeModel, unit.UNITID);

            }

            return treeModels;
        }

        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="parentTree">父节点</param>
        /// <param name="parentID">父节点标识</param>
        public static void AddTreeNode(TreeModel parentTree, decimal parentID)
        {
            //获得当前根节点下的所有的子节点
            List<UNIT> units = GetAllUnits().Where(t => t.PARENTID == parentID
                && t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            if (units == null || units.Count() == 0)
            {
                return;
            }

            foreach (var unit in units)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = unit.ABBREVIATION,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString()
                };

                parentTree.children.Add(treeModel);

                AddTreeNode(treeModel, unit.UNITID);

            }

        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="unit">单位对象实例</param>
        public static void AddUnit(UNIT unit)
        {
            PLEEntities db = new PLEEntities();

            db.UNITS.Add(unit);

            db.SaveChanges();
        }

        /// <summary>
        /// 根据行政单位标识删除行政单位
        /// </summary>
        /// <param name="unitID">行政单位标识</param>
        public static void DeleteUnit(decimal unitID)
        {
            PLEEntities db = new PLEEntities();

            UNIT unit = db.UNITS.SingleOrDefault(t => t.UNITID == unitID
                && t.STATUSID == (decimal)StatusEnum.Normal);

            unit.STATUSID = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }

        /// <summary>
        /// 修改行政单位
        /// </summary>
        /// <param name="_unit">行政单位</param>
        public static void ModifyUnit(UNIT _unit)
        {
            PLEEntities db = new PLEEntities();

            UNIT unit = db.UNITS
                .SingleOrDefault(t => t.UNITID == _unit.UNITID
                    && t.STATUSID == (decimal)StatusEnum.Normal);

            unit.UNITNAME = _unit.UNITNAME;
            unit.UNITTYPEID = _unit.UNITTYPEID;
            unit.SEQNO = _unit.SEQNO;

            db.SaveChanges();
        }

        /// <summary>
        /// 获取一个新的单位标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewUnitID()
        {
            PLEEntities db = new PLEEntities();

            string sql = "SELECT SEQ_UNITID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// /获取承办单位(大队或中队)
        /// </summary>
        /// <returns></returns>
        public static List<UNIT> GetCBDW()
        {
            PLEEntities db = new PLEEntities();

            List<UNIT> units = db.UNITS
                .Where(t => t.UNITTYPEID == (decimal)UnitTypeEnum.DD
                    || t.UNITTYPEID == (decimal)UnitTypeEnum.ZD)
                    .OrderBy(t => t.SEQNO).ToList();

            return units;
        }

        /// <summary>
        /// 根据父类标识获取单位
        /// </summary>
        /// <param name="parentID">父类标识</param>
        /// <returns></returns>
        public static List<UNIT> GetUnitsByParentID(decimal? parentID)
        {
            PLEEntities db = new PLEEntities();
            List<UNIT> results = db.UNITS
                .Where(t => t.PARENTID == parentID
                    && t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNO)
                .ToList();

            return results;

        }

        /// <summary>
        /// 根据用户标识获取单位标识
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static decimal GetUnitIDByUserID(decimal userID)
        {
            PLEEntities db = new PLEEntities();
            decimal unitID = db.USERS.SingleOrDefault
                (t => t.USERID == userID).UNITID.Value;
            return unitID;
        }

        /// <summary>
        /// 根据单位标识获取该单位的父节点标识
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns>父节点标识</returns>
        public static decimal GetParentIDByUnitID(decimal unitID)
        {
            PLEEntities db = new PLEEntities();
            UNIT model = db.UNITS.SingleOrDefault
                (t => t.UNITID == unitID);
            if (model != null && model.PARENTID != null)
            {
                return model.PARENTID.Value;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据单位标识获取单位名称
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns>单位名称</returns>
        public static string GetUnitNameByUnitID(decimal unitID)
        {
            PLEEntities db = new PLEEntities();
            UNIT model = db.UNITS.SingleOrDefault
               (t => t.UNITID == unitID);
            if (model != null && model.PARENTID != null)
            {
                return model.UNITNAME;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据单位类别标识返回单位列表
        /// </summary>
        /// <param name="unitID">单位类别标识</param>
        /// <returns>单位列表</returns>
        public static List<UNIT> GetUnitByUnitTypeID(decimal unittypeid)
        {
            PLEEntities db = new PLEEntities();
            List<UNIT> unitlist = db.UNITS.Where
               (t => t.UNITTYPEID == unittypeid).ToList();
            return unitlist;
        }

        /// <summary>
        /// 根据父类标识获取中队单位
        /// </summary>
        /// <param name="parentID">父类标识</param>
        /// <returns></returns>
        public static List<UNIT> GetZDUnitsByParentID(decimal? parentID)
        {
            PLEEntities db = new PLEEntities();
            List<UNIT> results = db.UNITS
                .Where(t => t.PARENTID == parentID
                    && t.STATUSID == (decimal)StatusEnum.Normal && t.UNITTYPEID == 5)
                .OrderBy(t => t.SEQNO)
                .ToList();

            return results;

        }
    }
}
