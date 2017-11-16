using HZW.ZHCG.BLL;
using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HZW.ZHCG.WebAPI.Controllers
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

        /// <summary>
        /// 返回监控页码/api/Camera/getcameraListPageIndex?pagesize=10 || /api/Camera/getcameraListPageIndex?filtrateName="兴慈"
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="filtrateName">筛选字符</param>
        /// <returns>返回页码</returns>
        [HttpGet]
        public int getcameraListPageIndex(int pagesize, string filtrateName)
        {
            return cameraBLL.getcameraListPageIndex(pagesize, filtrateName);
        }

        /// <summary>
        /// 返回监控列表/api/Camera/getcameraList?pagesize=10&pageindex=1
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页码</param>
        /// <returns>返回监控数据</returns>
        [HttpGet]
        public List<fi_camera_info> getcameraList(int pagesize, int pageindex)
        {
            List<fi_camera_info> list = cameraBLL.getcameraList(pagesize, pageindex);
            return list;
        }

        /// <summary>
        /// 返回筛选监控列表/api/Camera/getcameraList?pagesize=10&pageindex=1&="兴慈"
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页码</param>
        /// <param name="filtrateName">筛选字符</param>
        /// <returns></returns>
        [HttpGet]
        public List<fi_camera_info> getcameraList(int pagesize, int pageindex, string filtrateName)
        {
            List<fi_camera_info> list = cameraBLL.getcameraList(pagesize, pageindex, filtrateName);
            return list;
        }

        /// <summary>
        /// 获取监控详情
        /// </summary>
        /// <param name="id">唯一标识id</param>
        /// <returns>返回数据</returns>
        [HttpGet]
        public fi_camera_info getdetails(int id)
        {
            return cameraBLL.getdetails(id);
        }

        /// <summary>
        /// 获取监控树/api/Camera/getListTree
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<cameratree> getListTree(string name)
        {
            return cameraBLL.getListTree(name);
        }

        /// <summary>
        /// 获取监控区域/api/Camera/getaregionlist
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<string> getaregionlist()
        {
            return cameraBLL.getaregionlist();
        }

        /// <summary>
        /// 获取一个长字符串监控区域对应监控数量/api/Camera/getNumberByRegion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<int> getNumberByRegion()
        {
            return cameraBLL.getNumberByRegion();
        }

        /// <summary>
        /// 网格监控数据/api/Camera/getCameraGridData
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<cameraGrid> getCameraGridData()
        {
            return cameraBLL.getCameraGridData();
        }

        /// <summary>
        /// 返回监控类型数据/api/Camera/getCameraTypeNumber
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<cameraPiecharts> getCameraTypeNumber()
        {
            return cameraBLL.getCameraTypeNumber();
        }

        /// <summary>
        /// 返回监控类型统计/api/Camera/getCameraTypeCount
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int getCameraTypeCount()
        {
            return cameraBLL.getCameraTypeCount();
        }

        /// <summary>
        /// 查询无人机列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<cameratree> getwrj(string filter, int pageIndex, int pageSize)
        {
            return cameraBLL.getwrj(filter, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取无人机页码
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int getwrjPage(string filter, int pageSize)
        {
            return cameraBLL.getwrj(filter, pageSize);
        }

        /// <summary>
        /// 周边监控
        /// </summary>
        /// <param name="cameraid"></param>
        /// <returns></returns>
        public List<fi_camera_info> getcamera(string cameraid)
        {
            return cameraBLL.getcamera(cameraid);
        }

        /// <summary>
        /// 周边监控
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="r">周边距离</param>
        /// <returns></returns>
        public List<fi_camera_info> getcamera(double lon, double lat, double? r)
        {
            double DR = 500;
            if (r != null)
                DR = Convert.ToDouble(r);
            return cameraBLL.getcamera(lon, lat, DR);
        }
    }
}