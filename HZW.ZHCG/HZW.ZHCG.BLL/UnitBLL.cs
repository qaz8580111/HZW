﻿using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.BLL
{
    public class UnitBLL
    {
        private UnitDAL dal = new UnitDAL();
        private UserDAL userDal = new UserDAL();
        private UnitTypeDAL utDal = new UnitTypeDAL();

        public List<TreeUnit> GetTreeUnits(List<Filter> filters)
        {
            List<TreeUnit> dataList = dal.GetUnits(filters)
                .Select(t => new TreeUnit
                {
                    ID = t.ID,
                    Name = t.Name,
                    text = t.Name,
                    Path = t.Path,
                    UnitTypeID = t.UnitTypeID,
                    UnitTypeName = t.UnitTypeName,
                    ParentID = t.ParentID,
                }).OrderBy(t => t.ID).ToList();

            List<TreeUnit> list = new List<TreeUnit>();

            if (dataList.Count > 0)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    TreeUnit item = dataList[i];

                    if (item.ParentID == null)
                    {
                        item = GetUnitChildren(dataList, item);
                        list.Add(item);
                    }
                }
            }

            return list;
        }

        public List<TreeUnit> GetCurrentTreeUnits(int unitID)
        {
            List<Filter> filters = new List<Filter>()
            {
                new Filter() { property="Path",value="/"+unitID+"/"}
            };

            List<TreeUnit> dataList = dal.GetUnits(filters)
                .Select(t => new TreeUnit
                {
                    ID = t.ID,
                    Name = t.Name,
                    text = t.Name,
                    Path = t.Path,
                    UnitTypeID = t.UnitTypeID,
                    UnitTypeName = t.UnitTypeName,
                    ParentID = t.ParentID,
                }).OrderBy(t => t.ID).ToList();

            List<TreeUnit> list = new List<TreeUnit>();

            if (dataList.Count > 0)
            {
                for (int i = 0; i < dataList.Count; i++)
                {
                    TreeUnit item = dataList[i];

                    if (item.ID == unitID)
                    {
                        item = GetUnitChildren(dataList, item);
                        list.Add(item);
                    }
                }
            }

            return list;
        }


        public List<UnitModel> GetUnits()
        {
            return dal.GetUnits();
        }

        /// <summary>
        /// 查询单位分页列表
        /// </summary>
        public Paging<List<UnitModel>> GetUnits(List<Filter> filters, int start, int limit)
        {
            List<UnitModel> items = dal.GetUnits(filters, start, limit);
            int total = dal.GetUnitCount(filters);

            Paging<List<UnitModel>> paging = new Paging<List<UnitModel>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }


        /// <summary>
        /// 添加单位
        /// </summary>
        public void AddUnit(UnitModel unit)
        {
            dal.AddUnit(unit);
        }

        /// <summary>
        /// 修改单位
        /// </summary>
        public void EditUnit(UnitModel unit)
        {
            dal.EditUnit(unit);
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <returns>1.操作成功；2.下级部门存在；3.部门用户存在；</returns>
        public int DeleteUnit(int id)
        {
            List<UnitModel> unitList = dal.GetUnitsByPath(string.Format("/{0}/", id));

            if (unitList.Count > 1)
                return 2;

            List<User> userList = userDal.GetUsersByUnitID(id);

            if (userList.Count > 0)
                return 3;

            dal.DeleteUnit(id);
            return 1;
        }

        /// <summary>
        /// 获得单位类型列表
        /// </summary>
        public List<UnitType> GetUnitTypes()
        {
            return utDal.GetUnitTypes();
        }

        private TreeUnit GetUnitChildren(List<TreeUnit> dataList, TreeUnit item)
        {
            List<TreeUnit> list = new List<TreeUnit>();

            for (int i = 0; i < dataList.Count; i++)
            {
                TreeUnit childrenItem = dataList[i];

                if (childrenItem.ParentID != null && childrenItem.ParentID == item.ID)
                {
                    childrenItem = GetUnitChildren(dataList, childrenItem);
                    list.Add(childrenItem);
                }
            }

            if (list.Count > 0)
            {
                item.expanded = true;
                item.leaf = false;
            }
            else
            {
                item.expanded = false;
                item.leaf = true;
            }

            item.children = list;

            return item;
        }


    }
}
