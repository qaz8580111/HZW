using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class PartsBLL
    {
        PartsDAL partsDAL = new PartsDAL();
        #region 道路
        /// <summary>
        /// 分页获取道路列表
        /// 参数可选
        /// </summary>
        /// <param name="roadName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsRoadModel> GetRoadsByPage(string roadName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsRoadModel> roads = partsDAL.GetRoadsByPage(roadName, skipNum, takeNum);
            return roads;
        }
        /// <summary>
        /// 获取道路数量
        /// 参数可选
        /// </summary>
        /// <param name="roadName"></param>
        /// <returns></returns>
        public int GetRoadsCount(string roadName)
        {
            int count = partsDAL.GetRoadsCount(roadName);
            return count;
        }
        /// <summary>
        /// 根据道路标识获取道路
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public PartsRoadModel GetRoadByRoadId(decimal roadId)
        {
            PartsRoadModel road = partsDAL.GetRoadByRoadId(roadId);
            return road;
        }
        /// <summary>
        /// 根据道路标识获取道路详情
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public PartsRoadModel GetRoadDetailByRoadId(decimal roadId)
        {
            PartsRoadModel road = partsDAL.GetRoadDetailByRoadId(roadId);
            return road;
        }
        /// <summary>
        /// 根据道路标识获取路口路段列表
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public List<PartsRoadPartModel> GetRoadPartsByRoadId(decimal roadId)
        {
            List<PartsRoadPartModel> roadParts = partsDAL.GetRoadPartsByRoadId(roadId);
            return roadParts;
        }
        ///// <summary>
        ///// 根据三维模型标识获取道路
        ///// </summary>
        ///// <param name="SWMXId"></param>
        ///// <returns></returns>
        //public PartsRoadModel GetRoadBySWMXId(decimal SWMXId)
        //{
        //    PartsRoadModel road = partsDAL.GetRoadBySWMXId(SWMXId);
        //    return road;
        //}
        /// <summary>
        /// 根据三维模型标识获取道路和路口路段
        /// </summary>
        /// <param name="SWMXId"></param>
        /// <returns></returns>
        public object[] GetRoadAndRoadPartsBySWMXId(decimal SWMXId)
        {
            object[] objs = partsDAL.GetRoadAndRoadPartsBySWMXId(SWMXId);
            return objs;
        }
        #endregion
        #region 桥梁
        /// <summary>
        /// 分页获取桥梁列表
        /// </summary>
        /// <param name="bridgeName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsBridgeModel> GetBirdgesByPage(string bridgeName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsBridgeModel> result = partsDAL.GetBirdgesByPage(bridgeName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取桥梁数量
        /// </summary>
        /// <param name="bridgeName"></param>
        /// <returns></returns>
        public int GetBirdgesCount(string bridgeName)
        {
            int count = partsDAL.GetBirdgesCount(bridgeName);
            return count;
        }
        /// <summary>
        /// 根据桥梁标识获取桥梁基础信息
        /// </summary>
        /// <param name="bridgeId"></param>
        /// <returns></returns>
        public PartsBridgeModel GetBridgeByBridgeId(decimal bridgeId)
        {
            PartsBridgeModel result = partsDAL.GetBridgeByBridgeId(bridgeId);
            return result;
        }
        /// <summary>
        /// 根据桥梁标识获取桥梁详情
        /// </summary>
        /// <param name="bridgeId"></param>
        /// <returns></returns>
        public PartsBridgeModel GetBridgeDetail(decimal bridgeId)
        {
            PartsBridgeModel bridge = partsDAL.GetBridgeDetail(bridgeId);
            return bridge;
        }
        #endregion
        #region 路灯
        /// <summary>
        /// 分页获取路灯路段
        /// 参数可选
        /// </summary>
        /// <param name="loadName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsStreetLampLoadModel> GetStreetLampLoadsByPage(string loadName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsStreetLampLoadModel> result = partsDAL.GetStreetLampLoadsByPage(loadName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取路灯路段数量
        /// 参数可选
        /// </summary>
        /// <param name="loadName"></param>
        /// <returns></returns>
        public int GetStreetLampLoadsCount(string loadName)
        {
            int count = partsDAL.GetStreetLampLoadsCount(loadName);
            return count;
        }
        /// <summary>
        /// 根据路灯路段标识获取路灯路段
        /// </summary>
        /// <param name="SLLId"></param>
        /// <returns></returns>
        public PartsStreetLampLoadModel GetStreetLampLoad(decimal SLLId)
        {
            PartsStreetLampLoadModel result = partsDAL.GetStreetLampLoad(SLLId);
            return result;
        }
        /// <summary>
        /// 根据路灯路段标识获取路灯统计
        /// </summary>
        /// <param name="SLLId"></param>
        /// <returns></returns>
        public List<PartsStreetLampTJModel> GetStreetLampTJs(decimal SLLId)
        {
            List<PartsStreetLampTJModel> result = partsDAL.GetStreetLampTJs(SLLId);
            return result;
        }
        #endregion
        #region 景观灯
        /// <summary>
        /// 分页获取景观灯
        /// 参数可选
        /// </summary>
        /// <param name="lampName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsLandscapeLampModel> GetLandscapeLampsByPage(string lampName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsLandscapeLampModel> result = partsDAL.GetLandscapeLampsByPage(lampName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取景观灯数量
        /// </summary>
        /// <param name="lampName"></param>
        /// <returns></returns>
        public int GetLandscapeLampsCount(string lampName)
        {
            int count = partsDAL.GetLandscapeLampsCount(lampName);
            return count;
        }
        /// <summary>
        /// 根据景观灯标识获取景观灯
        /// </summary>
        /// <param name="LLId"></param>
        /// <returns></returns>
        public PartsLandscapeLampModel GetLandscapeLampByLLId(decimal LLId)
        {
            PartsLandscapeLampModel lamp = partsDAL.GetLandscapeLampByLLId(LLId);
            return lamp;
        }
        /// <summary>
        /// 根据景观灯标识获取景观灯统计
        /// </summary>
        /// <param name="LLId"></param>
        /// <returns></returns>
        public List<PartsLandscapeLampTJModel> GetLandscapeLampTJs(decimal LLId)
        {
            List<PartsLandscapeLampTJModel> result = partsDAL.GetLandscapeLampTJs(LLId);
            return result;
        }
        #endregion
        #region 泵站
        /// <summary>
        /// 分页获取泵站
        /// 参数可选
        /// </summary>
        /// <param name="pumpName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsPumpModel> GetPumpsByPage(string pumpName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsPumpModel> result = partsDAL.GetPumpsByPage(pumpName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取泵站数量
        /// 参数可选
        /// </summary>
        /// <param name="pumpName"></param>
        /// <returns></returns>
        public int GetPumpsCount(string pumpName)
        {
            int count = partsDAL.GetPumpsCount(pumpName);
            return count;
        }
        /// <summary>
        /// 根据泵站标识获取泵站
        /// </summary>
        /// <param name="pumpId"></param>
        /// <returns></returns>
        public PartsPumpModel GetPumpByPumpId(decimal pumpId)
        {
            PartsPumpModel pump = partsDAL.GetPumpByPumpId(pumpId);
            return pump;
        }
        #endregion
        #region 井盖
        /// <summary>
        /// 分页获取井盖路段
        /// 参数可选
        /// </summary>
        /// <param name="coverLoadName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsCoverLoadModel> GetCoverLoadsByPage(string coverLoadName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsCoverLoadModel> result = partsDAL.GetCoverLoadsByPage(coverLoadName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取井盖路段数量
        /// 参数可选
        /// </summary>
        /// <param name="coverLoadName"></param>
        /// <returns></returns>
        public int GetCoverLoadsCount(string coverLoadName)
        {
            int count = partsDAL.GetCoverLoadsCount(coverLoadName);
            return count;
        }
        /// <summary>
        /// 根据井盖路段标识获取井盖路段
        /// </summary>
        /// <param name="coverLoadId"></param>
        /// <returns></returns>
        public PartsCoverLoadModel GetCoverLoadByCoverLoadId(decimal coverLoadId)
        {
            PartsCoverLoadModel load = partsDAL.GetCoverLoadByCoverLoadId(coverLoadId);
            return load;
        }
        #endregion
        #region 公园绿地
        /// <summary>
        /// 分页获取公园绿地
        /// 参数可选
        /// </summary>
        /// <param name="parkGreenName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsParkGreenModel> GetParkGreensByPage(string parkGreenName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsParkGreenModel> result = partsDAL.GetParkGreensByPage(parkGreenName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取公园绿地数量 
        /// 参数可选
        /// </summary>
        /// <param name="parkGreenName"></param>
        /// <returns></returns>
        public int GetParkGreensCount(string parkGreenName)
        {
            int count = partsDAL.GetParkGreensCount(parkGreenName);
            return count;
        }
        /// <summary>
        /// 根据公园绿地标识获取公园绿地
        /// </summary>
        /// <param name="parkGreenId"></param>
        /// <returns></returns>
        public PartsParkGreenModel GetParkGreenByParkGreenId(decimal parkGreenId)
        {
            PartsParkGreenModel pg = partsDAL.GetParkGreenByParkGreenId(parkGreenId);
            return pg;
        }
        #endregion
        #region 道路绿地
        /// <summary>
        /// 分页获取道路绿地
        /// 参数可选
        /// </summary>
        /// <param name="loadGreenName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsLoadGreenModel> GetLoadGreensByPage(string loadGreenName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsLoadGreenModel> result = partsDAL.GetLoadGreensByPage(loadGreenName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取道路绿地数量
        /// </summary>
        /// <param name="loadGreenName"></param>
        /// <returns></returns>
        public int GetLoadGreensCount(string loadGreenName)
        {
            int count = partsDAL.GetLoadGreensCount(loadGreenName);
            return count;
        }
        /// <summary>
        /// 根据道路绿地标识获取道路绿地
        /// </summary>
        /// <param name="loadGreenId"></param>
        /// <returns></returns>
        public PartsLoadGreenModel GetLoadGreenByLoadGreenId(decimal loadGreenId)
        {
            PartsLoadGreenModel lg = partsDAL.GetLoadGreenByLoadGreenId(loadGreenId);
            return lg;
        }
        #endregion
        #region 防护绿地
        /// <summary>
        /// 分页获取防护绿地
        /// 参数可选
        /// </summary>
        /// <param name="protectionGreenName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsProtectionGreenModel> GetProtectionGreensByPage(string protectionGreenName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsProtectionGreenModel> result = partsDAL.GetProtectionGreensByPage(protectionGreenName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取防护绿地数量
        /// 参数可选
        /// </summary>
        /// <param name="protectionGreenName"></param>
        /// <returns></returns>
        public int GetProtectionGreensCount(string protectionGreenName)
        {
            int count = partsDAL.GetProtectionGreensCount(protectionGreenName);
            return count;
        }
        /// <summary>
        /// 根据防护绿地标识获取防护绿地
        /// </summary>
        /// <param name="protectionGreenId"></param>
        /// <returns></returns>
        public PartsProtectionGreenModel GetProtectionGreen(decimal protectionGreenId)
        {
            PartsProtectionGreenModel pg = partsDAL.GetProtectionGreen(protectionGreenId);
            return pg;
        }
        #endregion
        #region 公厕
        /// <summary>
        /// 分页获取公厕
        /// 参数可选
        /// </summary>
        /// <param name="toiltName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsToiltModel> GetToiltsByPage(string toiltName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsToiltModel> result = partsDAL.GetToiltsByPage(toiltName, skipNum, takeNum);
            return result;
        }
        /// <summary>
        /// 获取公厕数量
        /// </summary>
        /// <param name="toiltName"></param>
        /// <returns></returns>
        public int GetToiltsCount(string toiltName)
        {
            int count = partsDAL.GetToiltsCount(toiltName);
            return count;
        }
        /// <summary>
        /// 根据公厕标识获取公厕
        /// </summary>
        /// <param name="toiltId"></param>
        /// <returns></returns>
        public PartsToiltModel GetToilt(decimal toiltId)
        {
            PartsToiltModel toilt = partsDAL.GetToilt(toiltId);
            return toilt;
        }
        #endregion
        #region 河道
        /// <summary>
        /// 分页获取河道
        /// 参数可选
        /// </summary>
        /// <param name="riverName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<PartsRiverModel> GetRiversByPage(string riverName, decimal? skipNum, decimal? takeNum)
        {
            List<PartsRiverModel> rivers = partsDAL.GetRiversByPage(riverName, skipNum, takeNum);
            return rivers;
        }
        /// <summary>
        /// 获取河道数量
        /// 参数可选
        /// </summary>
        /// <param name="riverName"></param>
        /// <returns></returns>
        public int GetRiversCount(string riverName)
        {
            int count = partsDAL.GetRiversCount(riverName);
            return count;
        }
        /// <summary>
        /// 根据河道标识获取河道
        /// </summary>
        /// <param name="riverId"></param>
        /// <returns></returns>
        public PartsRiverModel GetRiver(decimal riverId)
        {
            PartsRiverModel river = partsDAL.GetRiver(riverId);
            return river;
        }
        #endregion
    }
}
