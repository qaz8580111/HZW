using ESRI.ArcGIS.Client.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class MapController : ApiController
    {
        MapBLL mapBLL = new MapBLL();

        /// <summary>
        /// /api/Map/GetCircumElements?men=&envelope=
        /// 获取周边元素，包含人、车、案件
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumElements(string mem, string envelope)
        {
            MapElementModel mapElement;

            if (string.IsNullOrEmpty(mem))
            {
                mapElement = null;
            }
            else
            {
                mapElement = JsonConvert.DeserializeObject<MapElementModel>(mem);
            }
            MapElementModel mapEnvelope = JsonConvert.DeserializeObject<MapElementModel>(envelope);

            List<MapElementModel> result1 = mapBLL.GetCircumUsers(mapElement, mapEnvelope);
            List<MapElementModel> result2 = mapBLL.GetCircumCars(mapElement, mapEnvelope);
            List<MapElementModel> result3 = mapBLL.GetCircumTasks(mapElement, mapEnvelope);
            List<MapElementModel> result4 = mapBLL.GetCircumCameras(mapElement, mapEnvelope);

            List<MapElementModel> result = new List<MapElementModel>();
            result.AddRange(result1);
            result.AddRange(result2);
            result.AddRange(result3);
            result.AddRange(result4);
            return result;
        }

        /// <summary>
        /// /api/Map/GetCircumUsers?mem=&envelope=
        /// 获取周边人员
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumUsers(string mem, string envelope)
        {
            MapElementModel mapElement = JsonConvert.DeserializeObject<MapElementModel>(mem);
            MapElementModel mapEnvelope = JsonConvert.DeserializeObject<MapElementModel>(envelope);

            List<MapElementModel> result = mapBLL.GetCircumUsers(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// /api/Map/GetCircumCars?mem=&envelope=
        /// 获取周边车辆
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCars(string mem, string envelope)
        {
            MapElementModel mapElement = JsonConvert.DeserializeObject<MapElementModel>(mem);
            MapElementModel mapEnvelope = JsonConvert.DeserializeObject<MapElementModel>(envelope);

            List<MapElementModel> result = mapBLL.GetCircumCars(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// /api/Map/GetCircumTasks?mem=&envelope=
        /// 获取周边案件
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumTasks(string mem, string envelope)
        {
            MapElementModel mapElement = JsonConvert.DeserializeObject<MapElementModel>(mem);
            MapElementModel mapEnvelope = JsonConvert.DeserializeObject<MapElementModel>(envelope);

            List<MapElementModel> result = mapBLL.GetCircumTasks(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// /api/Map/GetCircumCameras?mem=&envelope=
        /// 获取周边监控
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCameras(string mem, string envelope)
        {
            MapElementModel mapElement = JsonConvert.DeserializeObject<MapElementModel>(mem);
            MapElementModel mapEnvelope = JsonConvert.DeserializeObject<MapElementModel>(envelope);

            List<MapElementModel> result = mapBLL.GetCircumCameras(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// /api/Map/GetCricumCamerasPaly?mem=&envelope=
        /// 获取周边监控播放
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="envelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCricumCamerasPaly(string mem, string envelope)
        {
            MapElementModel mapElement = JsonConvert.DeserializeObject<MapElementModel>(mem);
            MapElementModel mapEnvelope = JsonConvert.DeserializeObject<MapElementModel>(envelope);

            List<MapElementModel> result = mapBLL.GetCircumCamerasPlay(mapElement, mapEnvelope);
            return result;
        }
    }
}
