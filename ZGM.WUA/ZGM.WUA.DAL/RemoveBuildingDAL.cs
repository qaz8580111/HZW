using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class RemoveBuildingDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 分页获取拆迁
        /// 参数可选
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public IQueryable<RemoveBuildingModel> GetRemoveBuildingsByPage(string projectName, DateTime? startTime, DateTime? endTime, decimal? skipNum, decimal? takeNum)
        {
            DateTime year = new DateTime(DateTime.Now.Year, 1, 1);
            IQueryable<CQGL_PROJECTS> projects = db.CQGL_PROJECTS.Where(t => t.STATE != 3);
            if (startTime != null && endTime != null)
                projects = projects.Where(t => (t.STARTTIME <= startTime && t.ENDTIME >= endTime) || t.ENDTIME > year);
            if (!string.IsNullOrEmpty(projectName))
                projects = projects.Where(t => t.PROJECTNAME.Contains(projectName));
            projects = projects.OrderByDescending(t => t.STATE);
            if (skipNum != null && takeNum != null)
                projects = projects.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<RemoveBuildingModel> result = from t in projects
                                                     from u in db.SYS_USERS
                                                     where t.CREATEUSER == u.USERID
                                                     select new RemoveBuildingModel
                                                     {
                                                         ProjectId = t.PROJECTID,
                                                         ProjectName = t.PROJECTNAME,
                                                         ProjectUser = t.PROJECTUSER,
                                                         StartTime = t.STARTTIME,
                                                         EndTime = t.ENDTIME,
                                                         Remarks = t.REMARKS,
                                                         State = t.STATE,
                                                         StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                         FileName = t.FILENAME,
                                                         FilePath = t.FILEPATH,
                                                         CreateTime = t.CREATETIME,
                                                         CreateUserId = t.CREATEUSER,
                                                         CreateUserName = u.USERNAME,
                                                         Geometry = t.GEOMETRY
                                                     };
            return result;
        }

        /// <summary>
        /// 获取拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCount(string projectName, DateTime? startTime, DateTime? endTime)
        {
            DateTime year = new DateTime(DateTime.Now.Year, 1, 1);
            IQueryable<CQGL_PROJECTS> projects = db.CQGL_PROJECTS.Where(t => t.STATE != 3);
            if (startTime != null && endTime != null)
                projects = projects.Where(t => (t.STARTTIME <= startTime && t.ENDTIME >= endTime) || t.ENDTIME > year);
            if (!string.IsNullOrEmpty(projectName))
                projects = projects.Where(t => t.PROJECTNAME.Contains(projectName));
            int count = projects.Count();
            return count;
        }

        /// <summary>
        /// 根据拆迁标识获取拆迁项目
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuilding(decimal projectId)
        {
            IQueryable<RemoveBuildingModel> projects = from t in db.CQGL_PROJECTS
                                                       from u in db.SYS_USERS
                                                       where t.PROJECTID == projectId
                                                       && t.CREATEUSER == u.USERID
                                                       select new RemoveBuildingModel
                                                       {
                                                           ProjectId = t.PROJECTID,
                                                           ProjectName = t.PROJECTNAME,
                                                           ProjectUser = t.PROJECTUSER,
                                                           StartTime = t.STARTTIME,
                                                           EndTime = t.ENDTIME,
                                                           Remarks = t.REMARKS,
                                                           State = t.STATE,
                                                           StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                           FileName = t.FILENAME,
                                                           FilePath = t.FILEPATH,
                                                           CreateTime = t.CREATETIME,
                                                           CreateUserId = t.CREATEUSER,
                                                           CreateUserName = u.USERNAME,
                                                           Geometry = t.GEOMETRY
                                                       };
            if (projects.Count() == 1)
                return projects.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 分页获取住宅拆迁
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public IQueryable<RemoveBuildingModel> GetRemoveBuildingsByPageHouse(string houseHolder, DateTime? startTime, DateTime? endTime, decimal? skipNum, decimal? takeNum)
        {
            DateTime year = new DateTime(DateTime.Now.Year, 1, 1);
            IQueryable<RemoveBuildingModel> projects = from h in db.CQGL_HOUSES
                                                       from t in db.CQGL_PROJECTS
                                                       from u in db.SYS_USERS
                                                       where t.PROJECTID == h.PROJECTID
                                                       && t.STATE != 3
                                                       && u.USERID == t.CREATEUSER
                                                       select new RemoveBuildingModel
                                                               {
                                                                   ProjectId = t.PROJECTID,
                                                                   ProjectName = t.PROJECTNAME,
                                                                   ProjectUser = t.PROJECTUSER,
                                                                   StartTime = t.STARTTIME,
                                                                   EndTime = t.ENDTIME,
                                                                   Remarks = t.REMARKS,
                                                                   State = t.STATE,
                                                                   StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                                   FileName = t.FILENAME,
                                                                   FilePath = t.FILEPATH,
                                                                   CreateTime = t.CREATETIME,
                                                                   CreateUserId = t.CREATEUSER,
                                                                   CreateUserName = u.USERNAME,
                                                                   Geometry = t.GEOMETRY,
                                                                   HouseId = h.HOUSEID,
                                                                   HouseHolder = h.HOUSEHOLDER,
                                                                   HolderPhone = h.HOLDERPHONE,
                                                                   WarrantArea = h.WARRANTAREA,
                                                                   MeasurementArea = h.MEASUREMENTAREA,
                                                                   WithoutArea = h.WITHOUTAREA
                                                               };
            if (startTime != null && endTime != null)
                projects = projects.Where(t => (t.StartTime <= startTime && t.EndTime >= endTime) || t.EndTime > year);
            if (!string.IsNullOrEmpty(houseHolder))
                projects = projects.Where(t => t.HouseHolder.Contains(houseHolder));
            projects = projects.OrderByDescending(t => t.State);
            if (skipNum != null && takeNum != null)
                projects = projects.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            return projects;
        }

        /// <summary>
        /// 获取住宅拆迁的数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCountHouse(string houseHolder, DateTime? startTime, DateTime? endTime)
        {
            DateTime year = new DateTime(DateTime.Now.Year, 1, 1);
            IQueryable<RemoveBuildingModel> projects = from h in db.CQGL_HOUSES
                                                       from t in db.CQGL_PROJECTS
                                                       from u in db.SYS_USERS
                                                       where t.PROJECTID == h.PROJECTID
                                                       && t.STATE != 3
                                                       && u.USERID == t.CREATEUSER
                                                       select new RemoveBuildingModel
                                                       {
                                                           ProjectId = t.PROJECTID,
                                                           ProjectName = t.PROJECTNAME,
                                                           ProjectUser = t.PROJECTUSER,
                                                           StartTime = t.STARTTIME,
                                                           EndTime = t.ENDTIME,
                                                           Remarks = t.REMARKS,
                                                           State = t.STATE,
                                                           StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                           FileName = t.FILENAME,
                                                           FilePath = t.FILEPATH,
                                                           CreateTime = t.CREATETIME,
                                                           CreateUserId = t.CREATEUSER,
                                                           CreateUserName = u.USERNAME,
                                                           Geometry = t.GEOMETRY,
                                                           HouseId = h.HOUSEID,
                                                           HouseHolder = h.HOUSEHOLDER,
                                                           HolderPhone = h.HOLDERPHONE,
                                                           WarrantArea = h.WARRANTAREA,
                                                           MeasurementArea = h.MEASUREMENTAREA,
                                                           WithoutArea = h.WITHOUTAREA
                                                       };
            if (startTime != null && endTime != null)
                projects = projects.Where(t => (t.StartTime <= startTime && t.EndTime >= endTime) || t.EndTime > year);
            if (!string.IsNullOrEmpty(houseHolder))
                projects = projects.Where(t => t.HouseHolder.Contains(houseHolder));
            int count = projects.Count();
            return count;
        }

        /// <summary>
        /// 根据住宅标识获取住宅拆迁
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuildingHouse(decimal? houseId)
        {
            IQueryable<RemoveBuildingModel> projects = from h in db.CQGL_HOUSES
                                                       from t in db.CQGL_PROJECTS
                                                       from u in db.SYS_USERS
                                                       where h.HOUSEID == houseId
                                                       && t.PROJECTID == h.PROJECTID
                                                       && t.STATE != 3
                                                       && u.USERID == t.CREATEUSER
                                                       select new RemoveBuildingModel
                                                       {
                                                           ProjectId = t.PROJECTID,
                                                           ProjectName = t.PROJECTNAME,
                                                           ProjectUser = t.PROJECTUSER,
                                                           StartTime = t.STARTTIME,
                                                           EndTime = t.ENDTIME,
                                                           Remarks = t.REMARKS,
                                                           State = t.STATE,
                                                           StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                           FileName = t.FILENAME,
                                                           FilePath = t.FILEPATH,
                                                           CreateTime = t.CREATETIME,
                                                           CreateUserId = t.CREATEUSER,
                                                           CreateUserName = u.USERNAME,
                                                           Geometry = t.GEOMETRY,
                                                           HouseId = h.HOUSEID,
                                                           HouseHolder = h.HOUSEHOLDER,
                                                           HolderPhone = h.HOLDERPHONE,
                                                           WarrantArea = h.WARRANTAREA,
                                                           MeasurementArea = h.MEASUREMENTAREA,
                                                           WithoutArea = h.WITHOUTAREA
                                                       };
            if (projects.Count() == 1)
                return projects.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 分页获取企业拆迁
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<RemoveBuildingModel> GetRemoveBuildingsByPageEnt(string projectName, DateTime? startTime, DateTime? endTime, decimal? skipNum, decimal? takeNum)
        {
            DateTime year = new DateTime(DateTime.Now.Year, 1, 1);
            IQueryable<RemoveBuildingModel> projects = from e in db.CQGL_ENTERPRISES
                                                       from t in db.CQGL_PROJECTS
                                                       from u in db.SYS_USERS
                                                       where t.PROJECTID == e.PROJECTID
                                                       && t.STATE != 3
                                                       && u.USERID == t.CREATEUSER
                                                       select new RemoveBuildingModel
                                                       {
                                                           ProjectId = t.PROJECTID,
                                                           ProjectName = t.PROJECTNAME,
                                                           ProjectUser = t.PROJECTUSER,
                                                           StartTime = t.STARTTIME,
                                                           EndTime = t.ENDTIME,
                                                           Remarks = t.REMARKS,
                                                           State = t.STATE,
                                                           StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                           FileName = t.FILENAME,
                                                           FilePath = t.FILEPATH,
                                                           CreateTime = t.CREATETIME,
                                                           CreateUserId = t.CREATEUSER,
                                                           CreateUserName = u.USERNAME,
                                                           Geometry = t.GEOMETRY,
                                                           EnterpriseId = e.ENTERPRISEID,
                                                           LegalName = e.LEGALNAME,
                                                           LegalPhone = e.LEGALPHONE,
                                                           LandArea = e.LANDAREA,
                                                           WarrantArea = e.WARRANTAREA,
                                                           MeasurementArea = e.MEASUREMENTAREA,
                                                           WithoutArea = e.WITHOUTAREA,
                                                           SignTime = e.SIGNTIME,
                                                           EmptyTime = e.EMPTYTIME,
                                                           SumMoney = e.SUMMONEY,
                                                           Tax = e.TAX
                                                       };
            if (startTime != null && endTime != null)
                projects = projects.Where(t => (t.StartTime <= startTime && t.EndTime >= endTime) || t.EndTime > year);
            if (!string.IsNullOrEmpty(projectName))
                projects = projects.Where(t => t.ProjectName.Contains(projectName));
            projects = projects.OrderByDescending(t => t.State);
            if (skipNum != null && takeNum != null)
                projects = projects.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            List<RemoveBuildingModel> list = projects.ToList();
            foreach (var item in list)
            {
                List<CQGL_FILES> flist = db.CQGL_FILES.Where(t => t.SOURCEID == item.EnterpriseId && t.GCID == 7).ToList();
                if (flist.Count != 0)
                {
                    foreach (var fitem in flist)
                    {
                        item.FileName += fitem.FILENAME + "|";
                        item.FilePath += fitem.FILEPATH + "|";
                    }
                    item.FileName = item.FileName.Substring(0, item.FileName.Length - 1);
                    item.FilePath = item.FilePath.Substring(0, item.FilePath.Length - 1);
                }
            }

            return list;
        }

        /// <summary>
        /// 获取企业拆迁数量
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int GetRemoveBuildingsCountEnt(string projectName, DateTime? startTime, DateTime? endTime)
        {
            DateTime year = new DateTime(DateTime.Now.Year, 1, 1);
            IQueryable<CQGL_PROJECTS> projects = from e in db.CQGL_ENTERPRISES
                                                 from t in db.CQGL_PROJECTS
                                                 where t.PROJECTID == e.PROJECTID
                                                 && t.STATE != 3
                                                 select t;
            if (startTime != null && endTime != null)
                projects = projects.Where(t => (t.STARTTIME <= startTime && t.ENDTIME >= endTime) || t.ENDTIME > year);
            if (!string.IsNullOrEmpty(projectName))
                projects = projects.Where(t => t.PROJECTNAME.Contains(projectName));
            int count = projects.Count();
            return count;
        }

        /// <summary>
        /// 根据住企业迁标识获取企业拆迁
        /// </summary>
        /// <param name="entId"></param>
        /// <returns></returns>
        public RemoveBuildingModel GetRemoveBuildingEnt(decimal? entId)
        {
            IQueryable<RemoveBuildingModel> projects = from e in db.CQGL_ENTERPRISES
                                                       from t in db.CQGL_PROJECTS
                                                       from u in db.SYS_USERS
                                                       where e.ENTERPRISEID == entId
                                                       && t.PROJECTID == e.PROJECTID
                                                       && t.STATE != 3
                                                       && u.USERID == t.CREATEUSER
                                                       select new RemoveBuildingModel
                                                       {
                                                           ProjectId = t.PROJECTID,
                                                           ProjectName = t.PROJECTNAME,
                                                           ProjectUser = t.PROJECTUSER,
                                                           StartTime = t.STARTTIME,
                                                           EndTime = t.ENDTIME,
                                                           Remarks = t.REMARKS,
                                                           State = t.STATE,
                                                           StateName = t.STATE == 1 ? "正在进行中" : t.STATE == 2 ? "已完结" : "已删除",
                                                           FileName = t.FILENAME,
                                                           FilePath = t.FILEPATH,
                                                           CreateTime = t.CREATETIME,
                                                           CreateUserId = t.CREATEUSER,
                                                           CreateUserName = u.USERNAME,
                                                           Geometry = t.GEOMETRY,
                                                           EnterpriseId = e.ENTERPRISEID,
                                                           LegalName = e.LEGALNAME,
                                                           LegalPhone = e.LEGALPHONE,
                                                           LandArea = e.LANDAREA,
                                                           WarrantArea = e.WARRANTAREA,
                                                           MeasurementArea = e.MEASUREMENTAREA,
                                                           WithoutArea = e.WITHOUTAREA,
                                                           SignTime = e.SIGNTIME,
                                                           EmptyTime = e.EMPTYTIME,
                                                           SumMoney = e.SUMMONEY,
                                                           Tax = e.TAX
                                                       };
            if (projects.Count() == 1)
                return projects.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 根据企业标识获取拆迁支付
        /// </summary>
        /// <param name="EntId"></param>
        /// <returns></returns>
        public List<RemoveBuildingEntMoneyModel> GetRemoveBuildingEntMoneys(decimal EntId)
        {
            IQueryable<RemoveBuildingEntMoneyModel> result = from t in db.CQGL_ENTERPRISEMONEYS
                                                             from u in db.SYS_USERS
                                                             where u.USERID == t.CREATEUSER
                                                             && t.ENTERPRISEID == EntId
                                                             orderby t.PAYPALTIME descending
                                                             select new RemoveBuildingEntMoneyModel
                                                             {
                                                                 OMId = t.OMID,
                                                                 EnterpriseId = t.ENTERPRISEID,
                                                                 PaypalTime = t.PAYPALTIME,
                                                                 ParpalMoney = t.PARPALMONEY,
                                                                 Remarks = t.REMARKS,
                                                                 FileName = t.FILENAME,
                                                                 FilePath = t.FILEPATH,
                                                                 CreateTime = t.CREATETIME,
                                                                 CreateUserId = t.CREATEUSER,
                                                                 CreateUserName = u.USERNAME
                                                             };
            List<RemoveBuildingEntMoneyModel> list = result.ToList();
            foreach (var item in list)
            {
                List<CQGL_FILES> flist = db.CQGL_FILES.Where(t => t.SOURCEID == item.OMId && t.GCID == 8).ToList();
                if (flist.Count != 0)
                {
                    foreach (var fitem in flist)
                    {
                        if (string.IsNullOrEmpty(fitem.FILENAME)) {
                            continue;
                        }
                        item.FileName += fitem.FILENAME + "|";
                        item.FilePath += fitem.FILEPATH + "|";
                    }
                    item.FileName = item.FileName.Substring(0, item.FileName.Length - 1);
                    item.FilePath = item.FilePath.Substring(0, item.FilePath.Length - 1);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取居民拆迁签协
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingHouseSignModel GetRemoveBuildingHouseSign(decimal houseId)
        {
            IQueryable<RemoveBuildingHouseSignModel> result = from t in db.CQGL_SIGNS
                                                              from u in db.SYS_USERS
                                                              where t.CREATEUSER == u.USERID
                                                              && t.HOUSEID == houseId
                                                              select new RemoveBuildingHouseSignModel
                                                              {
                                                                  SignId = t.SIGNID,
                                                                  HouseId = t.HOUSEID,
                                                                  SignTime = t.SIGNTIME,
                                                                  HouseArea = t.HOUSEAREA,
                                                                  WareHouseArea = t.WAREHOUSEAREA,
                                                                  WipeIn = t.WIPEIN,
                                                                  WipeOut = t.WIPEOUT,
                                                                  ExpansionArea = t.EXPANSIONAREA,
                                                                  HouseHoldExpansionArea = t.HOUSEHOLDEXPANSIONAREA,
                                                                  RewardArea = t.REWARDAREA,
                                                                  MarriageArea = t.MARRIAGEAREA,
                                                                  HouseMoney = t.HOUSEMONEY,
                                                                  RewardMoney = t.REWARDMONEY,
                                                                  CreateTime = t.CREATETIME,
                                                                  CreateUserId = t.CREATEUSER,
                                                                  CreateUserName = u.USERNAME,
                                                                  EmptyTime = t.EMPTYTIME
                                                              };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 获取居民拆迁过度
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public IQueryable<RemoveBuildingHouseTransitionModel> GetRemoveBuildingHouseTranstions(decimal houseId)
        {
            IQueryable<RemoveBuildingHouseTransitionModel> result = db.CQGL_TRANSITIONS
                .Where(t => t.HOUSEID == houseId)
                .Select(t => new RemoveBuildingHouseTransitionModel
                {
                    TransitionId = t.TRANSITIONID,
                    HouseId = t.HOUSEID,
                    StartTime = t.STARTTIME,
                    Term = t.TERM,
                    StandardMoney = t.STANDARDMONEY,
                    Money = t.MONEY,
                    PlaceArea = t.PLACEAREA,
                    CreateTime = t.CREATTIME
                });
            result = result.OrderByDescending(t => t.StartTime);
            return result;
        }

        /// <summary>
        /// 根据住宅标识获取抽签
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public List<RemoveBuildingHouseDrawModel> GetRemoveBuildingHouseDraw(decimal houseId)
        {
            IQueryable<RemoveBuildingHouseDrawModel> result = db.CQGL_DRAWS
                .Where(t => t.HOUSEID == houseId)
                .Select(t => new RemoveBuildingHouseDrawModel
                {
                    DrawId = t.DRAWID,
                    HouseId = t.HOUSEID,
                    DrawTime = t.DRAWTIME,
                    HouseCount = t.HOUSECOUNT,
                    OverArea = t.OVERAREA,
                    CreateTime = t.CREATETIME,
                    CreateUserId = t.CREATEUSER,
                });
            List<RemoveBuildingHouseDrawModel> draws = result.ToList();
            foreach (var item in draws)
            {
                IQueryable<RemoveBuildingHouseDrawHouseModel> houses = db.CQGL_DRAWHOUSE
                    .Where(t => t.DRAWID == item.DrawId)
                    .Select(t => new RemoveBuildingHouseDrawHouseModel
                    {
                        DrawHouseId = t.DWHID,
                        DrawId = t.DRAWID,
                        Residential = t.RESIDENTIAL,
                        HouseNumber = t.HOUSENUMBER,
                        Area = t.AREA,
                        Remarks = t.REMARKS,
                        FileName = t.FILENAME,
                        FilePath = t.FILEPATH,
                        CreateTime = t.CREATETIME
                    });
                item.Houses = houses.ToList();
            }
            return draws;
        }

        /// <summary>
        /// 获取拆迁结算
        /// </summary>
        /// <param name="houseId"></param>
        /// <returns></returns>
        public RemoveBuildingHouseCheckoutModel GetRemoveBuildingHouseCheckout(decimal houseId)
        {
            IQueryable<RemoveBuildingHouseCheckoutModel> result = from t in db.CQGL_CHECKOUT
                                                                  where t.HOUSEID == houseId
                                                                  select new RemoveBuildingHouseCheckoutModel
                                                                  {
                                                                      CheckoutId = t.CHID,
                                                                      SignId = t.SIGNID,
                                                                      HouseId = t.HOUSEID,
                                                                      CheckoutTime = t.CHTIME,
                                                                      CheckoutUserName = t.CHUSER,
                                                                      AccountUserName = t.ACCOUNTUSER,
                                                                      Money = t.MONEY,
                                                                      CreateTime = t.CREATETIME,
                                                                      CreateUserId = t.CREATEUSER
                                                                  };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 根据拆迁标识获取附件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FileModel> GetFiles(decimal id)
        {
            List<CQGL_FILES> result = db.CQGL_FILES
                .Where(t => t.SOURCEID == id)
                .ToList();

            List<FileModel> files = new List<FileModel>();
            for (int i = 0; i < result.Count; i++)
            {
                files.Add(new FileModel()
                {
                    FileId = result[i].FILEID.ToString(),
                    FileName = result[i].FILENAME,
                    FilePath = result[i].FILEPATH
                });
            }

            return files;
        }
    }
}
