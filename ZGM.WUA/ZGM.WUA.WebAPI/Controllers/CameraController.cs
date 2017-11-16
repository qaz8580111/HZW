using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class CameraController : ApiController
    {
        CameraBLL cameraBLL = new CameraBLL();

        /// <summary>
        /// /api/Camera/GetCameraUnits?unitId=
        /// 获取监控部门
        /// 参数,-1：全部；null：Control层
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraUnitModel> GetCameraUnits(decimal? unitId)
        {
            List<CameraUnitModel> result = cameraBLL.GetCameraUnits(unitId);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraInfos?unitId=&cameraName=
        /// 根据部门标识获取监控
        /// 参数可选
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraInfoModel> GetCameraInfos(decimal? unitId, string cameraName)
        {
            List<CameraInfoModel> result = cameraBLL.GetCameraInfos(unitId, cameraName);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraInfo?cameraId=
        /// 根据监控标识获取监控
        /// </summary>
        /// <param name="cameraId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfo(decimal cameraId)
        {
            CameraInfoModel camera = cameraBLL.GetCameraInfo(cameraId);
            return camera;
        }

        /// <summary>
        /// /api/Camera/GetCameraInfoByTHCId?tchId=
        /// 根据专题标识获取监控信息
        /// </summary>
        /// <param name="tchId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfoByTHCId(decimal tchId)
        {
            /// 根据专题标识获取监控信息
            CameraInfoModel camera = cameraBLL.GetCameraInfoByTHCId(tchId);
            return camera;
        }

        /// <summary>
        /// /api/Camera/GetCameraThemeItems?themeId=&cameraName=
        /// 获取专题元素
        /// </summary>
        /// <param name="themeId"></param>
        /// <param name="cameraName"></param>
        /// <returns></returns>
        public List<CameraThemeItemModel> GetCameraThemeItems(decimal? themeId, string cameraName)
        {
            List<CameraThemeItemModel> result = cameraBLL.GetCameraThemeItems(themeId, cameraName);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraThemeItem?cameraThemeItemId=
        /// 获取专题元素对象
        /// </summary>
        /// <param name="cameraThemeItemId"></param>
        /// <returns></returns>
        public CameraThemeItemModel GetCameraThemeItem(decimal cameraThemeItemId)
        {
            CameraThemeItemModel result = cameraBLL.GetCameraThemeItem(cameraThemeItemId);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraThemes?themeId=
        /// 获取专题
        /// P.S.:null为一级目录，-1为全部结构
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public List<CameraThemeModel> GetCameraThemes(decimal? themeId)
        {
            List<CameraThemeModel> result = cameraBLL.GetCameraThemes(themeId);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraUnitsJson?unitId=
        /// 获取监控部门
        /// Json转化
        /// </summary>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraUnitsJson(decimal? unitId)
        {
            List<CameraJsonModel> result = cameraBLL.GetCameraUnitsJson(unitId);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraInfosJson?unitId=
        /// 根据部门标识获取监控
        /// Json转化
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraInfosJson(decimal? unitId)
        {
            List<CameraJsonModel> result = cameraBLL.GetCameraInfosJson(unitId);
            return result;
        }

        /// <summary>
        /// /api/Camera/GetCameraUnitsJson
        /// 获取监控完整结构
        /// </summary>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraUnitsJson()
        {
            List<CameraJsonModel> result = cameraBLL.GetCameraUnitsJson();
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCamerasJson?cameraName=
        /// 根据监控名称获取监控
        /// </summary>
        /// <param name="cameraName"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCamerasJson(string cameraName)
        {
            List<CameraJsonModel> result = cameraBLL.GetCamerasJson(cameraName);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraThemesJson?themeId=
        /// 获取监控专题Json化
        /// P.S.:null为一级目录，-1为全部结构
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraThemesJson(decimal? themeId)
        {
            List<CameraJsonModel> result = cameraBLL.GetCameraThemesJson(themeId);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraThemeItemsJson?themeId=
        /// 获取监控专题元素
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraThemeItemsJson(decimal? themeId)
        {
            List<CameraJsonModel> result = cameraBLL.GetCameraThemeItemsJson(themeId);
            return result;
        }
        /// <summary>
        /// /api/Camera/GetCameraThemesJson
        /// 获取监控专题完整树
        /// </summary>
        /// <returns></returns>
        public List<CameraJsonModel> GetCameraThemesJson()
        {
            List<CameraJsonModel> result = cameraBLL.GetCameraThemesJson();
            return result;
        }

        /// <summary>
        /// 获取监控数量
        /// </summary>
        /// <returns></returns>
        public int GetCamerasCount()
        {
            int count = cameraBLL.GetCamerasCount();
            return count;
        }
    }
}
