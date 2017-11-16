using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class CameraDAL
    {
        ZGMEntities db = new ZGMEntities();
        #region 同步
        /// <summary>
        /// 添加监控控制中心
        /// 返回值：1成功，0失败
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int AddCameraUnit(CameraUnitModel unit)
        {
            db.FI_CAMERA_UNIT.Add(new FI_CAMERA_UNIT
            {
                UNITID = unit.UnitId,
                UNITNAME = unit.UnitName,
                PARENTID = unit.ParentId
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加监控区域
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public int AddCameraRegion(CameraRegionModel region)
        {
            db.FI_CAMERA_REGIONS.Add(new FI_CAMERA_REGIONS
            {
                REGIONID = region.RegionId,
                REGIONNAME = region.RegionName,
                UNITID = region.UnitId
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加监控
        /// 返回值：1成功，0失败
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public int AddCameraInfo(CameraInfoModel camera)
        {
            db.FI_CAMERA_INFO.Add(new FI_CAMERA_INFO
            {
                CAMERA_ID = camera.CameraId,
                NAME = camera.CameraName,
                DEVICE_ID = camera.DeviceId,
                REGION_ID = camera.RegionId,
                INDEX_CODE = camera.IndexCode,
                CAMERA_TYPE = camera.CameraTypeId,
                PARAMETER = camera.Parameter,
                PLAYBACK = camera.PlayBack,
                LONGITUDE = camera.X,
                LATITUDE = camera.Y
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        /// <summary>
        /// 添加监控设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public int AddCameraDev(CameraDevModel device)
        {
            db.FI_CAMERA_DEV.Add(new FI_CAMERA_DEV
            {
                DEVICEID = device.DeviceId,
                INDEXCODE = device.IndexCode,
                TYPECODE = device.TypeCode,
                NAME = device.Name
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion
        #region api
        /// <summary>
        /// 获取监控部门
        /// 参数,-1：全部；null：Control层
        /// </summary>
        /// <returns></returns>
        public List<CameraUnitModel> GetCameraUnits(decimal? unitId)
        {
            IQueryable<FI_CAMERA_UNIT> units = db.FI_CAMERA_UNIT;
            if (unitId == null)
                units = units.Where(t => t.PARENTID == (decimal?)null);
            if (unitId != -1 && unitId != null)
                units = units.Where(t => t.PARENTID == unitId);

            IQueryable<CameraUnitModel> result = units
                .Select(t => new CameraUnitModel
                {
                    UnitId = t.UNITID,
                    UnitName = t.UNITNAME,
                    ParentId = t.PARENTID
                });
            return result.ToList();
        }

        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IQueryable<CameraRegionModel> GetCameraRegions(decimal? unitId)
        {
            IQueryable<CameraRegionModel> result = db.FI_CAMERA_REGIONS
                .Where(t => t.UNITID == unitId)
                .Select(t => new CameraRegionModel
                {
                    RegionId = t.REGIONID,
                    RegionName = t.REGIONNAME,
                    UnitId = t.UNITID
                });
            return result;
        }
        /// <summary>
        /// 根据部门标识和监控名称获取监控
        /// 参数可选
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraInfoModel> GetCameraInfos(decimal? unitId, string cameraName)
        {
            IQueryable<FI_CAMERA_INFO> cameras = db.FI_CAMERA_INFO;
            if (unitId != null)
                cameras = cameras.Where(t => t.REGION_ID == unitId);
            if (!string.IsNullOrEmpty(cameraName))
                cameras = cameras.Where(t => t.NAME.Contains(cameraName));

            IQueryable<CameraInfoModel> result = cameras
                .Select(t => new CameraInfoModel
                {
                    CameraId = t.CAMERA_ID,
                    CameraName = t.NAME,
                    DeviceId = t.DEVICE_ID,
                    RegionId = t.REGION_ID,
                    IndexCode = t.INDEX_CODE,
                    CameraTypeId = t.CAMERA_TYPE,
                    CameraTypeName = t.CAMERA_TYPE == 0 ? "枪机" : t.CAMERA_TYPE == 1 ? "半球" : t.CAMERA_TYPE == 2 ? "快球" : "云台",
                    X = t.LONGITUDE,
                    Y = t.LATITUDE,
                    Scope = t.SCOPE,
                    PlayBack = t.PLAYBACK,
                    Parameter = t.PARAMETER
                });
              return result.ToList();
        }
        /// <summary>
        /// 根据监控标识获取监控
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfo(decimal cameraId)
        {
            IQueryable<CameraInfoModel> cameras = db.FI_CAMERA_INFO
                .Where(t => t.CAMERA_ID == cameraId)
                .Select(t => new CameraInfoModel
                {
                    CameraId = t.CAMERA_ID,
                    CameraName = t.NAME,
                    DeviceId = t.DEVICE_ID,
                    RegionId = t.REGION_ID,
                    IndexCode = t.INDEX_CODE,
                    CameraTypeId = t.CAMERA_TYPE,
                    CameraTypeName = t.CAMERA_TYPE == 0 ? "枪机" : t.CAMERA_TYPE == 1 ? "半球" : t.CAMERA_TYPE == 2 ? "快球" : "云台",
                    X = t.LONGITUDE,
                    Y = t.LATITUDE,
                    Parameter = t.PARAMETER,
                    PlayBack = t.PLAYBACK,
                    Scope = t.SCOPE
                });
            if (cameras.Count() == 1)
                return cameras.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 根据专题标识获取监控信息
        /// </summary>
        /// <param name="tchId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfoByTHCId(decimal tchId)
        {
            IQueryable<CameraInfoModel> cameras = from t in db.FI_CAMERA_INFO
                                                  from h in db.FI_CAMERA_ITEM
                                                  where h.THCID == tchId
                                                  && h.CAMERAID == t.CAMERA_ID
                                                  select new CameraInfoModel
                                                  {
                                                      CameraId = t.CAMERA_ID,
                                                      CameraName = t.NAME,
                                                      DeviceId = t.DEVICE_ID,
                                                      RegionId = t.REGION_ID,
                                                      IndexCode = t.INDEX_CODE,
                                                      CameraTypeId = t.CAMERA_TYPE,
                                                      CameraTypeName = t.CAMERA_TYPE == 0 ? "枪机" : t.CAMERA_TYPE == 1 ? "半球" : t.CAMERA_TYPE == 2 ? "快球" : "云台",
                                                      X = t.LONGITUDE,
                                                      Y = t.LATITUDE,
                                                      Parameter = t.PARAMETER,
                                                      PlayBack = t.PLAYBACK,
                                                      Scope = t.SCOPE
                                                  };
            if (cameras.Count() == 1)
                return cameras.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 获取监控专题
        /// P.S.:null为一级目录，-1为全部结构
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public IQueryable<CameraThemeModel> GetCameraThemes(decimal? themeId)
        {
            IQueryable<FI_CAMERA_THEME> themes = db.FI_CAMERA_THEME;
            if (themeId == null)
                themes = themes.Where(t => t.PARENTID == (decimal?)null);
            if (themeId != -1 && themeId != null)
                themes = themes.Where(t => t.PARENTID == themeId);

            IQueryable<CameraThemeModel> result = themes
                .Select(t => new CameraThemeModel
                {
                    ThemeId = t.THEMEID,
                    CameraName = t.NAME,
                    ParentId = t.PARENTID,
                    Isline = t.ISLINE,
                    Note = t.NOTE
                });
            return result;
        }
        /// <summary>
        /// 获取专题对象
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="cameraName"></param>
        /// <returns></returns>
        public IQueryable<CameraThemeItemModel> GetCameraThemeItems(decimal? themeId, string cameraName)
        {
            IQueryable<FI_CAMERA_ITEM> cameras = db.FI_CAMERA_ITEM;
            if (themeId != null)
                cameras = cameras.Where(t => t.THEMEID == themeId);
            if (!string.IsNullOrEmpty(cameraName))
                cameras = cameras.Where(t => t.NAME.Contains(cameraName));
            cameras = cameras.OrderBy(t => t.SORTNUM);

            IQueryable<CameraThemeItemModel> result = cameras
                .Select(t => new CameraThemeItemModel
                {
                    THCId = t.THCID,
                    ThemeId = t.THEMEID,
                    SortNum = t.SORTNUM,
                    CameraId = t.CAMERAID,
                    CameraName = t.NAME,
                    IndexCode = t.INDEX_CODE,
                    CameraType = t.CAMERA_TYPE,
                    CameraTypeName = t.CAMERA_TYPE == 0 ? "枪机" : t.CAMERA_TYPE == 1 ? "半球" : t.CAMERA_TYPE == 2 ? "快球" : "云台",
                    X = t.LONGITUDE,
                    Y = t.LATITUDE
                });
            return result;
        }
        /// <summary>
        /// 获取专题元素
        /// </summary>
        /// <param name="cameraThemeItemId"></param>
        /// <returns></returns>
        public CameraThemeItemModel GetCameraThemeItem(decimal cameraThemeItemId)
        {
            IQueryable<CameraThemeItemModel> result = db.FI_CAMERA_ITEM
                .Where(t => t.THCID == cameraThemeItemId)
                .Select(t => new CameraThemeItemModel
                {
                    THCId = t.THCID,
                    ThemeId = t.THEMEID,
                    SortNum = t.SORTNUM,
                    CameraId = t.CAMERAID,
                    CameraName = t.NAME,
                    IndexCode = t.INDEX_CODE,
                    CameraType = t.CAMERA_TYPE,
                    CameraTypeName = t.CAMERA_TYPE == 0 ? "枪机" : t.CAMERA_TYPE == 1 ? "半球" : t.CAMERA_TYPE == 2 ? "快球" : "云台",
                    X = t.LONGITUDE,
                    Y = t.LATITUDE
                });
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 获取监控数量
        /// </summary>
        /// <returns></returns>
        public int GetCamerasCount()
        {
            int count = db.FI_CAMERA_INFO.Count();
            return count;
        }
        #endregion
    }
}
