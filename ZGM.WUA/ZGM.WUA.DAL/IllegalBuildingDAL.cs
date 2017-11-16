using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class IllegalBuildingDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 分页获取违建
        /// 参数可选
        /// </summary>
        /// <param name="IBUnitName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<IllegalBuildingModel> GetIllegalBuildingsByPage(string IBUnitName, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<WJGL_NONBUILDINGS> ibs = db.WJGL_NONBUILDINGS;
            if (!string.IsNullOrEmpty(IBUnitName))
                ibs = ibs.Where(t => t.WJUNIT.Contains(IBUnitName));
            ibs = ibs.OrderBy(t => t.STATE);
            if (skipNum != null && takeNum != null)
                ibs = ibs.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<IllegalBuildingModel> result = from t in ibs
                                                      from i in db.SYS_IDENTITYS
                                                      from z in db.SYS_ZONES
                                                      where i.IDENTITYID == t.IDENTITYID
                                                      && z.ZONEID == t.ZONEID
                                                      select new IllegalBuildingModel
                                                      {
                                                          IBId = t.WJID,
                                                          IBUnitName = t.WJUNIT,
                                                          State = t.STATE == 1 ? "已拆" : "未拆",
                                                          IdentityId = t.IDENTITYID,
                                                          IdentityName = i.IDENTITYNAME,
                                                          ZoneId = t.ZONEID,
                                                          ZoneName = z.ZONENAME,
                                                          Tel = t.TEL,
                                                          Address = t.WJADDRESS,
                                                          WJTime = t.WJTIME,
                                                          WJType = t.WJTYPE,
                                                          WJFame = t.WJFRAME,
                                                          WJFloor = t.WJFLOOR,
                                                          LandType = t.LANDTYPE,
                                                          WJUse = t.WJUSE,
                                                          LandArea = t.LANDAREA,
                                                          BuildingArea = t.BUILDINGAREA,
                                                          RemoveTime = t.REMOVETIME,
                                                          RemoveArea = t.REMOVEAREA,
                                                          ConstructionProject = t.CONSTRUCTIONPROJECT,
                                                          SixCase = t.SIXCASE,
                                                          RectificationCase = t.RECTIFICATIONCASE == 1 ? "是" : "否",
                                                          RectificationTime = t.RECTIFICATIONTIME,
                                                          X = t.X2000,
                                                          Y = t.Y2000,
                                                          CreateTime = t.CREATETIME,
                                                          CreateUser = t.CREATEUSER,
                                                          Remark1 = t.REMARK1,
                                                          Remark2 = t.REMARK2,
                                                          Remark3 = t.REMARK3
                                                      };
            return result.ToList();
        }
        /// <summary>
        /// 获取违建数量
        /// 参数可选
        /// </summary>
        /// <param name="IBUnitName"></param>
        /// <returns></returns>
        public int GetIllegalBuildingsCount(string IBUnitName)
        {
            IQueryable<WJGL_NONBUILDINGS> ibs = db.WJGL_NONBUILDINGS;
            if (!string.IsNullOrEmpty(IBUnitName))
                ibs = ibs.Where(t => t.WJUNIT.Contains(IBUnitName));
            int count = ibs.Count();
            return count;
        }
        /// <summary>
        /// 根据违建标识获取违建
        /// </summary>
        /// <param name="IBId"></param>
        /// <returns></returns>
        public IllegalBuildingModel GetIllegalBuilding(string IBId)
        {
            IQueryable<IllegalBuildingModel> result = from t in db.WJGL_NONBUILDINGS
                                                      from i in db.SYS_IDENTITYS
                                                      from z in db.SYS_ZONES
                                                      where i.IDENTITYID == t.IDENTITYID
                                                      && z.ZONEID == t.ZONEID
                                                      && t.WJID == IBId
                                                      select new IllegalBuildingModel
                                                      {
                                                          IBId = t.WJID,
                                                          IBUnitName = t.WJUNIT,
                                                          State = t.STATE == 1 ? "已拆" : "未拆",
                                                          IdentityId = t.IDENTITYID,
                                                          IdentityName = i.IDENTITYNAME,
                                                          ZoneId = t.ZONEID,
                                                          ZoneName = z.ZONENAME,
                                                          Tel = t.TEL,
                                                          Address = t.WJADDRESS,
                                                          WJTime = t.WJTIME,
                                                          WJType = t.WJTYPE,
                                                          WJFame = t.WJFRAME,
                                                          WJFloor = t.WJFLOOR,
                                                          LandType = t.LANDTYPE,
                                                          WJUse = t.WJUSE,
                                                          LandArea = t.LANDAREA,
                                                          BuildingArea = t.BUILDINGAREA,
                                                          RemoveTime = t.REMOVETIME,
                                                          RemoveArea = t.REMOVEAREA,
                                                          ConstructionProject = t.CONSTRUCTIONPROJECT,
                                                          SixCase = t.SIXCASE,
                                                          RectificationCase = t.RECTIFICATIONCASE == 1 ? "是" : "否",
                                                          RectificationTime = t.RECTIFICATIONTIME,
                                                          X = t.X2000,
                                                          Y = t.Y2000,
                                                          CreateTime = t.CREATETIME,
                                                          CreateUser = t.CREATEUSER,
                                                          Remark1 = t.REMARK1,
                                                          Remark2 = t.REMARK2,
                                                          Remark3 = t.REMARK3
                                                      };
            if (result.Count() == 1)
                return result.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 根据违建标识获取附件
        /// </summary>
        /// <param name="IBId"></param>
        /// <returns></returns>
        public List<IllegalBuildingFileModel> GetIllegalBuildingFiles(string IBId)
        {
            IQueryable<IllegalBuildingFileModel> result = db.WJGL_PICTURES
                .Where(t => t.WJID == IBId)
                .Select(t => new IllegalBuildingFileModel
                {
                    FileId = t.PICTUREID,
                    WJId = t.WJID,
                    SSWJ = t.SSWJ,
                    Type = t.WJPICTURETYPE == 1 ? "改造前" : "改造后",
                    FileName = t.FILENAME,
                    FilePath = t.FILEPATH,
                    CreateTime = t.CREATETIME
                });
            return result.ToList();
        }
    }
}
