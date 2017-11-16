using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model.CustomModels;
using ZGM.Model;

namespace ZGM.BLL.CAMERABLLs
{
    public class CameraBLL
    {
        /// <summary>
        /// 获取所有监控
        /// </summary>
        /// <returns></returns>
        public static IQueryable<FI_CAMERA_THEME> GetAllCameras()
        {
            Entities db = new Entities();

            IQueryable<FI_CAMERA_THEME> results = db.FI_CAMERA_THEME;

            return results;
        }


        /// <summary>
        /// 获取所有监控元素单位
        /// </summary>
        /// <returns></returns>
        public static IQueryable<FI_CAMERA_UNIT> GetAllitemCameras()
        {
            Entities db = new Entities();

            IQueryable<FI_CAMERA_UNIT> results = db.FI_CAMERA_UNIT;

            return results;
        }


        /// <summary>
        /// 获取所有区域
        /// </summary>
        /// <returns></returns>
        public static IQueryable<FI_CAMERA_REGIONS> GetAllRegionitemCameras()
        {
            Entities db = new Entities();

            IQueryable<FI_CAMERA_REGIONS> results = db.FI_CAMERA_REGIONS;

            return results;
        }


        /// <summary>
        /// 获取监控专题树
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeNodes()
        {
            List<TreeModel> treeModels = new List<TreeModel>();
            //根节点
            List<FI_CAMERA_THEME> results = GetAllCameras().Where(t => t.PARENTID == null || t.PARENTID==0).OrderBy(t => t.THEMEID).ToList();

            //遍历根节点
            foreach (var r in results)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = r.NAME,
                    title = r.NAME,
                    id = r.THEMEID.ToString(),
                    value = r.THEMEID.ToString(),
                    pId=r.PARENTID.ToString(),
                    open = true
                };

                treeModels.Add(rootTreeModel);

                AddTreeNode(rootTreeModel, r.THEMEID);

            }

            return treeModels;
        }


        /// <summary>
        /// 获取监控元素
        /// </summary>
        /// <returns>监控元素列表</returns>
        public static IQueryable<FI_CAMERA_INFO> GetAllitems()
        {
            Entities db = new Entities();

            IQueryable<FI_CAMERA_INFO> results = db.FI_CAMERA_INFO;

            return results;
        }


        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="parentTree">父节点</param>
        /// <param name="parentID">父节点标识</param>
        public static void AddTreeNode(TreeModel parentTree, decimal parentID)
        {
            //获得当前根节点下的所有的子节点
            List<FI_CAMERA_THEME> units = GetAllCameras().Where(t => t.PARENTID == parentID).ToList();

            if (units == null || units.Count() == 0)
            {
                return;
            }

            foreach (var unit in units)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = unit.NAME,
                    title = unit.NAME,
                    pId = unit.PARENTID.ToString(),
                    value = unit.THEMEID.ToString()
                };

                parentTree.children.Add(treeModel);

                AddTreeNode(treeModel, unit.THEMEID);

            }

        }


        /// <summary>
        /// 获取监控元素树
        /// </summary>
        /// <returns></returns>
        public static List<TreeModel> GetTreeitemNodes()
        {

            List<TreeModel> treeModels = new List<TreeModel>();

            List<FI_CAMERA_UNIT> results = GetAllitemCameras().ToList();

            List<FI_CAMERA_REGIONS> allUsers = GetAllRegionitemCameras().ToList();

            List<FI_CAMERA_UNIT> rootUnits = results.Where(t => t.PARENTID == null)
                .ToList();

            //遍历根节点
            foreach (var unit in rootUnits)
            {
                TreeModel rootTreeModel = new TreeModel
                {
                    name = unit.UNITNAME,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString(),
                    open = true,
                    type = "组织",
                };


                //循环向（根）父节点添加子节点
                treeModels.Add(rootTreeModel);

                AddTreeitemNode(results, allUsers, rootTreeModel);

            }

            return treeModels;
        }


        /// <summary>
        /// 添加父节点下的子节点
        /// </summary>
        /// <param name="allUnits">所有监控专题</param>
        /// <param name="allUsers">所有监控元素</param>
        /// <param name="parentTree">父节点</param>
        public static void AddTreeitemNode(List<FI_CAMERA_UNIT> allUnits, List<FI_CAMERA_REGIONS> allUsers, TreeModel parentTree)
        {
            List<FI_CAMERA_UNIT> childrenUnits = allUnits.Where(t => t.PARENTID == decimal.Parse(parentTree.value)).ToList();
            List<FI_CAMERA_REGIONS> childrenUsers = allUsers.Where(t => t.UNITID == decimal.Parse(parentTree.value)).ToList();

            foreach (FI_CAMERA_REGIONS user in childrenUsers)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = user.REGIONNAME,
                    title = user.REGIONNAME,
                    value = user.REGIONID.ToString(),
                    type = "组织"
                };
                decimal? zivalue = decimal.Parse(treeModel.value);
                List<FI_CAMERA_INFO> allInfoUsers = GetAllitems().Where(t => t.REGION_ID == zivalue).ToList();
                //循环向父节点添加人员
                parentTree.children.Add(treeModel);
                AddTreeCamaraitemNode(allUsers, allInfoUsers, treeModel);
            }

            foreach (var unit in childrenUnits)
            {
                TreeModel treeModel = new TreeModel
                {
                    name = unit.UNITNAME,
                    title = unit.UNITNAME,
                    value = unit.UNITID.ToString(),
                    type = "组织"
                };

                //循环向父节点添加
                parentTree.children.Add(treeModel);

                AddTreeitemNode(allUnits, allUsers, treeModel);
            }
        }


        /// <summary>
        /// 添加各个监控
        /// </summary>
        /// <param name="allUnits">所有父节点</param>
        /// <param name="allUsers">所有子节点</param>
        /// <param name="parentTree">父节点</param>
        public static void AddTreeCamaraitemNode(List<FI_CAMERA_REGIONS> allUnits, List<FI_CAMERA_INFO> allUsers, TreeModel parentTree)
        {

            foreach (FI_CAMERA_INFO user in allUsers)
            {
                TreeModel LasttreeModel = new TreeModel
                {
                    name = user.NAME,
                    title = user.NAME,
                    value = user.CAMERA_ID.ToString(),
                };

                //循环向父节点添加
                parentTree.children.Add(LasttreeModel);
            }
        }


        /// <summary>
        /// 根据监控元素类型获取监控
        /// </summary>
        /// <param name="THEMEID">监控元素类型</param>
        /// <returns>监控</returns>
        public static IQueryable<FI_CAMERA_ITEM> GetInfoByID(decimal? THEMEID)
        {
            Entities db = new Entities();

            IQueryable<FI_CAMERA_ITEM> results = db.FI_CAMERA_ITEM.Where(t => t.THEMEID == THEMEID);

            return results;
        }


        /// <summary>
        /// 添加监控专题
        /// </summary>
        /// <param name="fitheme"></param>
        /// <returns></returns>
        public static int addcameratheme(FI_CAMERA_THEME fitheme)
        {
            Entities db = new Entities();
            db.FI_CAMERA_THEME.Add(fitheme);
            return db.SaveChanges();
        }


        //删除监控专题之前，判断其是否含有监控元素
        public static decimal getcandelete(decimal id)
        {
            Entities db = new Entities();
            var infos = from i in db.FI_CAMERA_THEME
                        join fci in db.FI_CAMERA_ITEM
                        on i.THEMEID equals fci.THEMEID into ifci
                        where i.PARENTID == id || i.THEMEID == id
                        select new
                        {
                            FICount = ifci.Count()
                        };
            decimal counts = 0;
            if (infos.Count() > 0)
            {
                foreach (var item in infos)
                {
                    counts += item.FICount;
                }
            }
             
            return counts;
        }


        //删除监控专题
        public static int deletecameratheme(decimal id)
        {
            Entities db = new Entities();
            var infos = from i in db.FI_CAMERA_THEME
                     where i.THEMEID == id || i.PARENTID == id
                     select i;
            foreach (var item in infos)
            {
                db.FI_CAMERA_THEME.Remove(item);
            }
            return db.SaveChanges();
        }


        /// <summary>
        /// 查询监控专题的数量
        /// </summary>
        /// <returns></returns>
        public static decimal camerathemecount()
        {
            Entities db = new Entities();
            decimal count=0;
            var countresult = (from i in db.FI_CAMERA_THEME
                               orderby i.THEMEID descending
                               select i).FirstOrDefault();
            if (countresult!=null)
            {
                count = countresult.THEMEID;
            }
            return count;
        }


        /// <summary>
        /// 获取子类单位
        /// </summary>
        /// <param name="parentUnitID">父类单位标识</param>
        /// <returns>子类列表</returns>
        public static IQueryable<FI_CAMERA_ITEM> GetChildUnit(decimal? parentUnitID)
        {
            Entities db = new Entities();
           IQueryable<FI_CAMERA_ITEM> result=from i in db.FI_CAMERA_ITEM
                   from b in db.FI_CAMERA_THEME
                   where i.THEMEID==b.THEMEID && b.PARENTID==parentUnitID
                   select i ;
            return result;
        }


        /// <summary>
        /// 根据ID查监控元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FI_CAMERA_INFO GetCAMERA_INFObyId(decimal id)
        {
            Entities db = new Entities();

            FI_CAMERA_INFO info = db.FI_CAMERA_INFO.FirstOrDefault(t => t.CAMERA_ID == id);
            return info;
        }


        /// <summary>
        /// 获取一个元素标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetFI_THCIDID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_FICAMERAITEMID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }


        /// <summary>
        /// 添加监控元素
        /// </summary>
        /// <param name="fiitem"></param>
        public static void addcameraitem(FI_CAMERA_ITEM fiitem)
        {
            Entities db = new Entities();
            db.FI_CAMERA_ITEM.Add(fiitem);
            db.SaveChanges();
        }


        /// <summary>
        /// 根据ID删除监控元素
        /// </summary>
        /// <param name="id"></param>
        public static void deletecameraitem(decimal id)
        {
            Entities db = new Entities();
            FI_CAMERA_ITEM info = (from i in db.FI_CAMERA_ITEM
                                  where i.THCID == id
                                  select i).FirstOrDefault();
            db.FI_CAMERA_ITEM.Remove(info);

            db.SaveChanges();
        }


        /// <summary>
        /// 根据ID查询监控专题最大的排序值
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static decimal? getCAMERA_themebyId(decimal id)
        {
            Entities db = new Entities();
            decimal? maxnumber = 0;
            var info = db.FI_CAMERA_ITEM.Where(t => t.THEMEID == id).OrderByDescending(d => d.SORTNUM).FirstOrDefault();
            if (info!=null)
            {
                maxnumber=info.SORTNUM;
            }
            return maxnumber;
        }
    
    }
}
