using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class CameraBLL
    {
        CameraDAL cameraDAL = new CameraDAL();
        #region 同步
        /// <summary>
        /// 添加监控部门
        /// 返回值：1成功，0失败
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int AddCameraUnit(CameraUnitModel unit)
        {
            int result = cameraDAL.AddCameraUnit(unit);
            return result;
        }

        /// <summary>
        /// 添加监控区域
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public int AddCameraRegion(CameraRegionModel region)
        {
            int result = cameraDAL.AddCameraRegion(region);
            return result;
        }
        /// <summary>
        /// 添加监控
        /// 返回值：1成功，0失败
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public int AddCameraInfo(CameraInfoModel camera)
        {
            int result = cameraDAL.AddCameraInfo(camera);
            return result;
        }

        /// <summary>
        /// 添加监控设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public int AddCameraDev(CameraDevModel device)
        {
            int result = cameraDAL.AddCameraDev(device);
            return result;
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
            List<CameraUnitModel> result = cameraDAL.GetCameraUnits(unitId);
            return result;
        }
        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraRegionModel> GetCameraRegions(decimal? unitId)
        {
            IQueryable<CameraRegionModel> result = cameraDAL.GetCameraRegions(unitId);
            return result.ToList();
        }
        /// <summary>
        /// 根据部门标识和监控名称获取监控
        /// 参数可选
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraInfoModel> GetCameraInfos(decimal? unitId, string cameraName)
        {
            List<CameraInfoModel> result = cameraDAL.GetCameraInfos(unitId, cameraName);
            return result;
        }
        /// <summary>
        /// 根据监控标识获取监控
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfo(decimal cameraId)
        {
            CameraInfoModel camera = cameraDAL.GetCameraInfo(cameraId);
            //camera.Parameter = "<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>" + camera.CameraId
            //        + "</Id><IndexCode>" + camera.IndexCode
            //        + "</IndexCode><Name>" + camera.CameraName
            //        + "</Name><ChanNo>0</ChanNo><Matrix Code='' Id='0' /></Camera><Dev regtype='0' devtype='10070'><Id>" + camera.DeviceId
            //        + "</Id><IndexCode>" + camera.IndexCode
            //        + "</IndexCode><Addr IP='172.16.7.101' Port='8000' /><Auth User='admin' Pwd='12345' /></Dev><Vag IP='172.16.2.36' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='1'><Vtdu IP='172.16.2.37' Port='554' /></Media><Privilege Priority='50' Code='7' /><Option><Talk>1</Talk><PreviewType>0</PreviewType></Option></Message>";

            return camera;
        }

        /// <summary>
        /// 根据专题标识获取监控信息
        /// </summary>
        /// <param name="tchId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfoByTHCId(decimal tchId)
        {
            CameraInfoModel camera = cameraDAL.GetCameraInfoByTHCId(tchId);
            return camera;
        }
        /// <summary>
        /// 获取监控专题
        /// P.S.:null为一级目录，-1为全部结构
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public List<CameraThemeModel> GetCameraThemes(decimal? themeId)
        {
            IQueryable<CameraThemeModel> result = cameraDAL.GetCameraThemes(themeId);
            return result.ToList();
        }
        /// <summary>
        /// 获取专题元素
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="cameraName"></param>
        /// <returns></returns>
        public List<CameraThemeItemModel> GetCameraThemeItems(decimal? themeId, string cameraName)
        {
            IQueryable<CameraThemeItemModel> result = cameraDAL.GetCameraThemeItems(themeId, cameraName);
            return result.ToList();
        }
        /// <summary>
        /// 获取专题元素对象
        /// </summary>
        /// <param name="cameraThemeItemId"></param>
        /// <returns></returns>
        public CameraThemeItemModel GetCameraThemeItem(decimal cameraThemeItemId)
        {
            CameraThemeItemModel result = cameraDAL.GetCameraThemeItem(cameraThemeItemId);
            return result;
        }
        /// <summary>
        /// 获取监控部门
        /// Json转化
        /// </summary>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraUnitsJson(decimal? unitId)
        {
            List<CameraUnitModel> result = cameraDAL.GetCameraUnits(unitId);
            List<CameraJsonModel> jsons = new List<CameraJsonModel>();
            foreach (var item in result)
            {
                jsons.Add(new CameraJsonModel
                {
                    id = item.UnitId.ToString(),
                    text = item.UnitName,
                    state = "closed",
                    iconCls = "icoNoImg",
                });
            }
            return jsons;
        }

       

        /// <summary>
        /// 获取区域Json
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraRegionsJson(decimal? unitId)
        {
            List<CameraRegionModel> result = cameraDAL.GetCameraRegions(unitId).ToList();
            List<CameraJsonModel> jsons = new List<CameraJsonModel>();
            foreach (var item in result)
            {
                jsons.Add(new CameraJsonModel
                {
                    id = item.RegionId.ToString(),
                    text = item.RegionName,
                    state = "closed",
                    iconCls = "icoNoImg",
                });
            }
            return jsons;
        }

        /// <summary>
        /// 根据部门标识获取监控
        /// Json转化
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraInfosJson(decimal? unitId)
        {
            List<CameraInfoModel> result = cameraDAL.GetCameraInfos(unitId, null);
            List<CameraJsonModel> jsons = new List<CameraJsonModel>();
            foreach (var item in result)
            {
                jsons.Add(new CameraJsonModel
                {
                    id = item.CameraId.ToString(),
                    text = item.CameraName,
                    iconCls = item.CameraTypeName == "快球" ? "icoKQCamera" : "icoQJCamera",
                    attributes = item
                });
            }
            return jsons;
        }

        /// <summary>
        /// list类型转换为DataTable
        /// </summary>
        /// <param name="list">要转换的集合</param>
        /// <returns>返回DataTable结果</returns>
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取监控完整结构
        /// </summary>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraUnitsJson()
        {
            List<CameraJsonModel> controls = this.GetCameraUnitsJson(null);
            foreach (var item in controls)
            {
                List<CameraJsonModel> regions = this.GetCameraRegionsJson(Convert.ToDecimal(item.id));
                foreach (var re in regions)
                {
                    List<CameraJsonModel> cameras = this.GetCameraInfosJson(Convert.ToDecimal(re.id));
                    if (cameras.Count > 0)
                    {
                        DataTable table = ToDataTableTow(cameras);
                        DataView view1 = table.DefaultView;
                        view1.Sort = string.Format("{0} {1}", "text", "ASC");
                        DataTable tableone = view1.ToTable();
                        List<CameraJsonModel> list = new List<CameraJsonModel>();
                        foreach (DataRow rs in tableone.Rows)
                        {
                            CameraJsonModel model = new CameraJsonModel();
                            model.text = rs["text"].ToString();
                            model.id = rs["id"].ToString();
                            model.iconCls = rs["iconCls"].ToString();
                            model.attributes = rs["attributes"];
                            list.Add(model);
                        }
                        re.children = list;
                    }
                    else
                    {
                        re.children = cameras;
                    }
                }
                item.children = regions;
            }
            return controls;
        }
        /// <summary>
        /// 根据监控名称获取监控
        /// </summary>
        /// <param name="cameraName"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCamerasJson(string cameraName)
        {
            List<CameraInfoModel> result = cameraDAL.GetCameraInfos(null, cameraName);
            List<CameraJsonModel> jsons = new List<CameraJsonModel>();
            foreach (var item in result)
            {
                jsons.Add(new CameraJsonModel
                {
                    id = item.CameraId.ToString(),
                    text = item.CameraName,
                    iconCls = item.CameraTypeName == "快球" ? "icoKQCamera" : "icoQJCamera",
                    attributes = item
                });
            }
            return jsons;
        }
        /// <summary>
        /// 获取专题Json化
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraThemesJson(decimal? themeId)
        {
            List<CameraThemeModel> result = this.GetCameraThemes(themeId);
            List<CameraJsonModel> jsons = new List<CameraJsonModel>();
            foreach (CameraThemeModel item in result)
            {
                jsons.Add(new CameraJsonModel
                {
                    id = item.ThemeId.ToString(),
                    text = item.CameraName,
                    state = "closed",
                    iconCls = "icoNoImg"
                });
            }
            return jsons;
        }
        /// <summary>
        /// 获取专题元素Json化
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraThemeItemsJson(decimal? themeId)
        {
            List<CameraThemeItemModel> result = this.GetCameraThemeItems(themeId, null);
            List<CameraJsonModel> jsons = new List<CameraJsonModel>();
            foreach (CameraThemeItemModel item in result)
            {
                jsons.Add(new CameraJsonModel
                {
                    id = item.THCId.ToString(),
                    text = item.CameraName,
                    iconCls = item.CameraTypeName == "快球" ? "icoKQCamera" : "icoQJCamera",
                    attributes = item
                });
            }
            return jsons;
        }
        /// <summary>
        /// 获取监控专题完整树
        /// </summary>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraThemesJson()
        {
            List<CameraJsonModel> themes = this.GetCameraThemesJson(null);
            foreach (CameraJsonModel item in themes)
            {
                List<CameraJsonModel> themeSubs = this.GetCameraThemesJson(Convert.ToDecimal(item.id));
                foreach (CameraJsonModel ts in themeSubs)
                {
                    List<CameraJsonModel> themeItems = this.GetCameraThemeItemsJson(Convert.ToDecimal(ts.id));
                    ts.children = themeItems;
                }
                item.children = themeSubs;
            }
            return themes;
        }

        /// <summary>
        /// 获取监控数量
        /// </summary>
        /// <returns></returns>
        public int GetCamerasCount()
        {
            int count = cameraDAL.GetCamerasCount();
            return count;
        }

        #endregion
    }
}
