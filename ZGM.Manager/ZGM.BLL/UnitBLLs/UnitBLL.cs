using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.Common.Enums;
using ZGM.Model;
using ZGM.Model.CustomModels;

namespace ZGM.BLL.UnitBLLs
{
    public class UnitBLL
    {
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
        /// 获取一个新的单位标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewUnitID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_UNITID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有单位
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SYS_UNITS> GetAllUnits()
        {
            Entities db = new Entities();

            IQueryable<SYS_UNITS> unitList = db.SYS_UNITS.Where(t => t
                .STATUSID == (decimal)StatusEnum.Normal).OrderBy(a => a.SEQNUM);

            return unitList;
        }

        /// <summary>
        /// 根据unittypeid获取单位
        /// </summary>unittypeid  行政单位类型标识
        /// <returns></returns>
        public static IQueryable<SYS_UNITS> GetAllUnitsByUnitTypeID(decimal unittypeid)
        {
            Entities db = new Entities();

            IQueryable<SYS_UNITS> unitList = db.SYS_UNITS.Where(t => t
                .STATUSID == (decimal)StatusEnum.Normal && t.UNITTYPEID == unittypeid).OrderBy(a => a.SEQNUM);
            return unitList;
        }
        /// <summary>
        /// 根据中队获取分队
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static IQueryable<SYS_UNITS> IQuerableGetUnitByDeptID(decimal deptID)
        {
            Entities db = new Entities();
            IQueryable<SYS_UNITS> list = db.SYS_UNITS.Where(t => t.PARENTID == deptID && t
                .STATUSID == (decimal)StatusEnum.Normal);
            return list;
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="unit">单位对象实例</param>
        public static void AddUnit(SYS_UNITS unit)
        {
            Entities db = new Entities();
            db.SYS_UNITS.Add(unit);
            db.SaveChanges();
        }

        /// <summary>
        /// 修改行政单位
        /// </summary>
        /// <param name="_unit">行政单位</param>
        public static void ModifyUnit(SYS_UNITS _unit)
        {
            Entities db = new Entities();

            SYS_UNITS unit = db.SYS_UNITS
                .SingleOrDefault(t => t.UNITID == _unit.UNITID
                    && t.STATUSID == (decimal)StatusEnum.Normal);

            unit.UNITNAME = _unit.UNITNAME;
            unit.UNITTYPEID = _unit.UNITTYPEID;
            unit.SEQNUM = _unit.SEQNUM;
            unit.ABBREVIATION = _unit.ABBREVIATION;
            unit.DESCRIPTION = _unit.DESCRIPTION;
            db.SaveChanges();
        }

        /// <summary>
        /// 根据行政单位标识删除行政单位
        /// </summary>
        /// <param name="unitID">行政单位标识</param>
        public static void DeleteUnit(decimal unitID)
        {
            Entities db = new Entities();

            SYS_UNITS unit = db.SYS_UNITS.FirstOrDefault(t => t.UNITID == unitID
                && t.STATUSID == (decimal)StatusEnum.Normal);

            unit.STATUSID = (decimal)StatusEnum.Deleted;

            db.SaveChanges();
        }

        /// <summary>
        /// 获取树节点
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes()
        {
            List<TreeModel> treeModels = new List<TreeModel>();
            //根节点
            List<SYS_UNITS> rootUnits = GetAllUnits().Where(t => t.PARENTID == null
                && t.STATUSID == (decimal)StatusEnum.Normal)
                .ToList();

            //遍历根节点
            foreach (var unit in rootUnits)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = unit.ABBREVIATION,
                    title = unit.UNITNAME,
                    id = unit.UNITID.ToString(),
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
            List<SYS_UNITS> units = GetAllUnits().Where(t => t.PARENTID == parentID
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
        /// 获取子类单位
        /// </summary>
        /// <param name="parentUnitID">父类单位标识</param>
        /// <returns>子类列表</returns>
        public static IQueryable<SYS_UNITS> GetChildUnit(decimal? parentUnitID)
        {
            Entities db = new Entities();

            IQueryable<SYS_UNITS> result = db.SYS_UNITS
                .Where(t => t.PARENTID == parentUnitID
                    && t.STATUSID == (decimal)StatusEnum.Normal)
                .OrderBy(t => t.SEQNUM);

            return result;
        }
        /// <summary>
        /// 根据单位标识获取该单位的父节点标识
        /// </summary>
        /// <param name="unitID">单位标识</param>
        /// <returns>父节点标识</returns>
        public static decimal GetParentIDByUnitID(decimal unitID)
        {
            Entities db = new Entities();
            SYS_UNITS model = db.SYS_UNITS.SingleOrDefault
                (t => t.UNITID == unitID && t.STATUSID == 1);
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
            Entities db = new Entities();
            SYS_UNITS model = db.SYS_UNITS.SingleOrDefault
               (t => t.UNITID == unitID && t.STATUSID == 1);
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
        /// 获取中队及中队下的分队
        /// </summary>
        /// <returns></returns>
        public static IQueryable<SYS_UNITS> GetMidUnit()
        {
            Entities db = new Entities();
            IQueryable<SYS_UNITS> list = db.SYS_UNITS.Where(t => t.PATH.Contains("\\1\\17") && t.STATUSID == (decimal)StatusEnum.Normal).OrderBy(a => a.UNITID);
            return list;
        }

        /// <summary>
        /// 根据部门名字获取标识
        /// </summary>
        /// <returns></returns>
        public static decimal? GetUnitIdByUnitName(string UnitName)
        {
            Entities db = new Entities();
            decimal? unitid = db.SYS_UNITS.FirstOrDefault(t => t.UNITNAME == UnitName).UNITID;
            return unitid;
        }
    }
}
