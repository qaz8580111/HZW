using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class MapBLL
    {
        MapDAL mapDAL = new MapDAL();

        /// <summary>
        /// 获取周边人员
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumUsers(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            List<MapElementModel> result = mapDAL.GetCircumUsers(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// 获取周边车辆
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCars(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            List<MapElementModel> result = mapDAL.GetCircumCars(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// 获取周边案件
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumTasks(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            List<MapElementModel> result = mapDAL.GetCircumTasks(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// 获取周边监控
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCameras(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            List<MapElementModel> result = mapDAL.GetCircumCameras(mapElement, mapEnvelope);
            return result;
        }

        /// <summary>
        /// 获取周边监控播放
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCamerasPlay(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            List<MapElementModel> result = mapDAL.GetCircumCameras(mapElement, mapEnvelope);
            foreach (MapElementModel item in result)
            {
                item.Type = "CameraPaly";
            }
            return result;
        }
    }
}
