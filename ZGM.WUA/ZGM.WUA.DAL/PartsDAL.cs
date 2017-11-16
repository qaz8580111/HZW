using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class PartsDAL
    {
        ZGMEntities db = new ZGMEntities();

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
            IQueryable<BD_JCDLXX> roads = db.BD_JCDLXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(roadName))
                roads = roads.Where(t => t.JCDL_NAME.Contains(roadName));
            roads = roads.OrderBy(t => t.JCDL_NAME);
            if (skipNum != null && takeNum != null)
                roads = roads.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<PartsRoadModel> result = from t in roads
                                                select new PartsRoadModel
                                                {
                                                    RoadId = t.JCDL_ID,
                                                    RoadName = t.JCDL_NAME,
                                                    SWMXId = t.SWMX_ID,
                                                    DLTX = t.DLTX,
                                                    X = t.ZXDXZB,
                                                    Y = t.ZXDYZB,
                                                    GD = t.GD,
                                                    SFYX = t.SFYX,
                                                    CreateTime = t.CJSJ
                                                };
            return result.ToList();
        }
        /// <summary>
        /// 获取道路数量
        /// 参数可选
        /// </summary>
        /// <param name="roadName"></param>
        /// <returns></returns>
        public int GetRoadsCount(string roadName)
        {
            IQueryable<BD_JCDLXX> roads = db.BD_JCDLXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(roadName))
                roads = roads.Where(t => t.JCDL_NAME.Contains(roadName));
            int count = roads.Count();
            return count;
        }
        /// <summary>
        /// 根据道路标识获取道路
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public PartsRoadModel GetRoadByRoadId(decimal roadId)
        {
            PartsRoadModel road = db.BD_JCDLXX
                .Where(t => t.JCDL_ID == roadId)
                .Select(t => new PartsRoadModel
                {
                    RoadId = t.JCDL_ID,
                    RoadName = t.JCDL_NAME,
                    SWMXId = t.SWMX_ID,
                    DLTX = t.DLTX,
                    X = t.ZXDXZB,
                    Y = t.ZXDYZB,
                    GD = t.GD,
                    SFYX = t.SFYX,
                    CreateTime = t.CJSJ
                }).SingleOrDefault();
            return road;
        }
        /// <summary>
        /// 根据道路标识获取道路详情
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public PartsRoadModel GetRoadDetailByRoadId(decimal roadId)
        {
            IQueryable<BD_JCLDCXDXX> cxds = from t in db.BD_JCDLXX
                                            from cxd in db.BD_JCLDCXDXX
                                            where t.JCDL_ID == roadId
                                            && cxd.SSJCDL_ID == t.JCDL_ID
                                            select cxd;
            decimal? cxdkd = cxds.Sum(t => t.CXDKD);
            int cc = cxds.Count();
            decimal? cxdkd2 = Convert.ToDecimal(cxdkd / cc);
            cxdkd2 = Math.Round(cxdkd2.Value, 2);

            List<decimal> cxdIds = cxds.Select(t => t.JCLDCXD_ID).ToList();
            List<decimal?> ids = new List<decimal?>();
            foreach (decimal item in cxdIds)
            {
                ids.Add(item);
            }
            IQueryable<BD_JCLDRXDXX> rxds = from rxd in db.BD_JCLDRXDXX
                                            where ids.Contains(rxd.SSJCLDCXD_ID)
                                            select rxd;
            decimal? cxdcd = cxds.Sum(c => c.CXDCD);
            cxdcd = Math.Round(cxdcd.Value, 2);
            decimal? cxdmj = cxds.Sum(c => c.CXDMJ);
            cxdmj = Math.Round(cxdmj.Value, 2);
            decimal? rxdc = rxds.Sum(r => r.RXDC);
            rxdc = Math.Round(rxdc.Value, 2);
            decimal? rxdk = rxds.Sum(r => r.RXDK) / rxds.Count();
            rxdk = Math.Round(rxdk.Value, 2);
            decimal? rxdmj = rxds.Sum(r => r.RXDMJ);
            rxdmj = Math.Round(rxdmj.Value, 2);
            IQueryable<PartsRoadModel> road = from t in db.BD_JCDLXX
                                              where t.JCDL_ID == roadId
                                              select new PartsRoadModel
                                              {
                                                  RoadId = t.JCDL_ID,
                                                  RoadName = t.JCDL_NAME,
                                                  SWMXId = t.SWMX_ID,
                                                  DLTX = t.DLTX,
                                                  X = t.ZXDXZB,
                                                  Y = t.ZXDYZB,
                                                  GD = t.GD,
                                                  SFYX = t.SFYX,
                                                  CreateTime = t.CJSJ,
                                                  CXDCD = cxdcd,
                                                  CXDKD = cxdkd2,
                                                  CXDMJ = cxdmj,
                                                  RXDC = rxdc,
                                                  RXDK = rxdk,
                                                  RXDMJ = rxdmj
                                              };
            return road.SingleOrDefault();
        }
        /// <summary>
        /// 根据道路标识获取路口路段列表
        /// </summary>
        /// <param name="roadId"></param>
        /// <returns></returns>
        public List<PartsRoadPartModel> GetRoadPartsByRoadId(decimal roadId)
        {
            BD_JCDLXX road = db.BD_JCDLXX.Where(t => t.JCDL_ID == roadId).SingleOrDefault();
            if (road == null)
                return null;
            string[] strs = road.SWMX_ID.Split('|');
            decimal[] ids = new decimal[strs.Length];
            for (int i = 0; i < strs.Length; i++)
            {
                ids[i] = Convert.ToInt32(strs[i]);
            }

            IQueryable<PartsRoadPartModel> roadParts = from t in db.BD_JCLKLDCXDXX
                                                       where ids.Contains(t.SWMX_ID == null ? -1 : t.SWMX_ID.Value)
                                                       select new PartsRoadPartModel
                                                       {
                                                           RoadPartId = t.JCLKLDCXD_ID,
                                                           RoadPartName = t.JCLKLDCXDMS,
                                                           SWMXId = t.SWMX_ID,
                                                           DLTX = t.DLTX,
                                                           X = t.ZXDXZB,
                                                           Y = t.ZXDYZB,
                                                           GD = t.GD,
                                                           isCrossing = t.SFLK.Equals("1") ? "是" : "否",
                                                           SFYX = t.SFYX,
                                                           CreateTime = t.CJSJ
                                                       };
            return roadParts.ToList();
        }
        /// <summary>
        /// 根据三维模型标识获取道路
        /// </summary>
        /// <param name="SWMXId"></param>
        /// <returns></returns>
        public PartsRoadModel GetRoadBySWMXId(decimal SWMXId)
        {
            string id = SWMXId.ToString();
            IQueryable<PartsRoadModel> roads = db.BD_JCDLXX
                .Where(t => t.SWMX_ID.Contains(id))
                .Select(t => new PartsRoadModel
                {
                    RoadId = t.JCDL_ID,
                    RoadName = t.JCDL_NAME,
                    SWMXId = t.SWMX_ID,
                    DLTX = t.DLTX,
                    X = t.ZXDXZB,
                    Y = t.ZXDYZB,
                    GD = t.GD,
                    SFYX = t.SFYX,
                    CreateTime = t.CJSJ
                });
            if (roads.Count() > 0)
                return roads.FirstOrDefault();
            return null;
        }
        /// <summary>
        /// 根据三维模型标识获取道路和路口路段
        /// </summary>
        /// <param name="SWMXId"></param>
        /// <returns></returns>
        public object[] GetRoadAndRoadPartsBySWMXId(decimal SWMXId)
        {
            PartsRoadModel road = this.GetRoadBySWMXId(SWMXId);
            if (road == null)
                return null;
            List<PartsRoadPartModel> roadParts = this.GetRoadPartsByRoadId(road.RoadId);
            if (roadParts.Count == 0)
                return null;
            object[] objs = new object[2];
            objs[0] = road;
            objs[1] = roadParts;
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
            IQueryable<BD_JCQLXX> bridges = db.BD_JCQLXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(bridgeName))
                bridges = bridges.Where(t => t.JCQL_NAME.Contains(bridgeName));
            bridges = bridges.OrderBy(t => t.JCQL_NAME);
            if (skipNum != null && takeNum != null)
                bridges = bridges.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<PartsBridgeModel> result = from t in bridges
                                                  select new PartsBridgeModel
                                                  {
                                                      BridgeId = t.JCQL_ID,
                                                      BridgeName = t.JCQL_NAME,
                                                      X = t.ZXDXZB,
                                                      Y = t.ZXDYZB
                                                  };
            return result.ToList();
        }
        /// <summary>
        /// 获取桥梁数量
        /// </summary>
        /// <param name="bridgeName"></param>
        /// <returns></returns>
        public int GetBirdgesCount(string bridgeName)
        {
            IQueryable<BD_JCQLXX> bridges = db.BD_JCQLXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(bridgeName))
                bridges = bridges.Where(t => t.JCQL_NAME.Contains(bridgeName));
            int count = bridges.Count();
            return count;
        }
        /// <summary>
        /// 根据桥梁标识获取桥梁基础信息
        /// </summary>
        /// <param name="bridgeId"></param>
        /// <returns></returns>
        public PartsBridgeModel GetBridgeByBridgeId(decimal bridgeId)
        {
            IQueryable<PartsBridgeModel> result = db.BD_JCQLXX
                .Where(t => t.JCQL_ID == bridgeId)
                .Select(t => new PartsBridgeModel
                {
                    BridgeId = t.JCQL_ID,
                    BridgeName = t.JCQL_NAME,
                    X = t.ZXDXZB,
                    Y = t.ZXDYZB,
                    DLTX = t.DLTX
                });
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }
        /// <summary>
        /// 根据桥梁标识获取桥梁详情
        /// </summary>
        /// <param name="bridgeId"></param>
        /// <returns></returns>
        public PartsBridgeModel GetBridgeDetail(decimal bridgeId)
        {
            IQueryable<PartsBridgeModel> result = from t in db.BP_QLJCXX
                                                  join hzdj in db.SYS_ZDXX
                                                  on t.HZDJ_TYPE equals hzdj.ZDXX_ID
                                                  into temp
                                                  from hzdj in temp.DefaultIfEmpty()
                                                  join qmpzcl in db.SYS_ZDXX
                                                  on t.QMPZCL_TYPE equals qmpzcl.ZDXX_ID
                                                  into temp2
                                                  from qmpzcl in temp2.DefaultIfEmpty()
                                                  join rxdpzcl in db.SYS_ZDXX
                                                  on t.RXDPZCL_TYPE equals rxdpzcl.ZDXX_ID
                                                  into temp3
                                                  from rxdpzcl in temp3.DefaultIfEmpty()
                                                  join yhlx in db.SYS_ZDXX
                                                  on t.YHLX_TYPE equals yhlx.ZDXX_ID
                                                  into temp4
                                                  from yhlx in temp4.DefaultIfEmpty()
                                                  join yhdj in db.SYS_ZDXX
                                                  on t.YHDJ_TYPE equals yhdj.ZDXX_ID
                                                  into temp5
                                                  from yhdj in temp5.DefaultIfEmpty()
                                                  where t.JCQL_ID == bridgeId
                                                  select new PartsBridgeModel
                                                  {
                                                      BridgeId = t.JCQL_ID,
                                                      HZDJ_TYPE = hzdj.ZDXX_NAME,
                                                      QLCD = t.QLCD,
                                                      KSKJ = t.KSKJ,
                                                      CXDK = t.CXDK,
                                                      CXDMJ = t.CXDMJ,
                                                      QMPZCL_TYPE = qmpzcl.ZDXX_NAME,
                                                      RXDK = t.RXDK,
                                                      RXDMJ = t.RXDMJ,
                                                      RXDPZCL_TYPE = rxdpzcl.ZDXX_NAME,
                                                      YHLX_TYPE = yhlx.ZDXX_NAME,
                                                      YHDJ_TYPE = yhdj.ZDXX_NAME,
                                                      QMBG = t.QMBG,
                                                      LDBG = t.LDBG,
                                                      CSW = t.CSW,
                                                      CKJG = t.CKJG,
                                                      BZ = t.BZ,
                                                      JGRQ = t.JGRQ,
                                                      GZRQ = t.GZRQ,
                                                      YJRQ = t.YJRQ
                                                  };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
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
            IQueryable<BP_LDLDJCXX> jclds = db.BP_LDLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(loadName))
                jclds = jclds.Where(t => t.LD_NAME.Contains(loadName));
            jclds = jclds.OrderBy(t => t.LD_NAME);
            if (skipNum != null && takeNum != null)
                jclds = jclds.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<PartsStreetLampLoadModel> result = from t in jclds
                                                          join gx in db.BP_LDLDJCLKLDCXDGX
                                                          on t.LDLD_ID equals gx.LDLD_ID
                                                          into temp
                                                          from gx in temp.DefaultIfEmpty()
                                                          join cxd in db.BD_JCLKLDCXDXX
                                                          on gx.JCLKLDCXD_ID equals cxd.JCLKLDCXD_ID
                                                          into temp2
                                                          from cxd in temp2.DefaultIfEmpty()
                                                          select new PartsStreetLampLoadModel
                                                          {
                                                              SLLId = t.LDLD_ID,
                                                              SLLName = t.LD_NAME,
                                                              QQD = t.QQD,
                                                              GG = t.GG,
                                                              KZXBH = t.KZXBH,
                                                              KZXZLDD = t.KZXZLDD,
                                                              LDGBH = t.LDGBH,
                                                              GS = t.GS,
                                                              CKJG = t.CKJG,
                                                              BZ = t.BZ,
                                                              JGRQ = t.JGRQ,
                                                              YJRQ = t.YJRQ,
                                                              SFLJSC = t.SFLJSC,
                                                              X = cxd.ZXDXZB,
                                                              Y = cxd.ZXDYZB
                                                          };
            List<PartsStreetLampLoadModel> result1 = new List<PartsStreetLampLoadModel>();
            foreach (var item in result)
            {
                if (result1.Select(t => t.SLLId).Contains(item.SLLId))
                    continue;
                result1.Add(item);
            }
            return result1;
        }
        /// <summary>
        /// 获取路灯路段数量
        /// 参数可选
        /// </summary>
        /// <param name="loadName"></param>
        /// <returns></returns>
        public int GetStreetLampLoadsCount(string loadName)
        {
            IQueryable<BP_LDLDJCXX> jclds = db.BP_LDLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(loadName))
                jclds = jclds.Where(t => t.LD_NAME.Contains(loadName));
            int count = jclds.Count();
            return count;
        }
        /// <summary>
        /// 根据路灯路段标识获取路灯路段
        /// </summary>
        /// <param name="SLLId"></param>
        /// <returns></returns>
        public PartsStreetLampLoadModel GetStreetLampLoad(decimal SLLId)
        {
            IQueryable<BP_LDLDJCXX> jclds = db.BP_LDLDJCXX.Where(t => t.SFLJSC == 0);
            IQueryable<PartsStreetLampLoadModel> result = from t in jclds
                                                          join gx in db.BP_LDLDJCLKLDCXDGX
                                                          on t.LDLD_ID equals gx.LDLD_ID
                                                          into temp
                                                          from gx in temp.DefaultIfEmpty()
                                                          join cxd in db.BD_JCLKLDCXDXX
                                                          on gx.JCLKLDCXD_ID equals cxd.JCLKLDCXD_ID
                                                          into temp2
                                                          from cxd in temp2.DefaultIfEmpty()
                                                          where t.LDLD_ID == SLLId
                                                          select new PartsStreetLampLoadModel
                                                          {
                                                              SLLId = t.LDLD_ID,
                                                              SLLName = t.LD_NAME,
                                                              QQD = t.QQD,
                                                              GG = t.GG,
                                                              KZXBH = t.KZXBH,
                                                              KZXZLDD = t.KZXZLDD,
                                                              LDGBH = t.LDGBH,
                                                              GS = t.GS,
                                                              CKJG = t.CKJG,
                                                              BZ = t.BZ,
                                                              JGRQ = t.JGRQ,
                                                              YJRQ = t.YJRQ,
                                                              SFLJSC = t.SFLJSC,
                                                              X = cxd.ZXDXZB,
                                                              Y = cxd.ZXDYZB
                                                          };
            if (result.Count() > 0)
                return result.FirstOrDefault();
            return null;
        }
        /// <summary>
        /// 根据路灯路段标识获取路灯统计
        /// </summary>
        /// <param name="SLLId"></param>
        /// <returns></returns>
        public List<PartsStreetLampTJModel> GetStreetLampTJs(decimal SLLId)
        {
            IQueryable<PartsStreetLampTJModel> result = from t in db.BP_LDTJJCXX
                                                        join dglx in db.SYS_ZDXX
                                                        on t.DGLX_TYPE equals dglx.ZDXX_ID
                                                        into temp
                                                        from dglx in temp.DefaultIfEmpty()
                                                        join dplx in db.SYS_ZDXX
                                                        on t.DPLX_TYPE equals dplx.ZDXX_ID
                                                        into temp2
                                                        from dplx in temp2.DefaultIfEmpty()
                                                        join gl in db.SYS_ZDXX
                                                        on t.GL_TYPE equals gl.ZDXX_ID
                                                        into temp3
                                                        from gl in temp3.DefaultIfEmpty()
                                                        where t.LDLD_ID == SLLId
                                                        select new PartsStreetLampTJModel
                                                        {
                                                            SLLId = t.LDLD_ID,
                                                            DGLX_TYPE = dglx.ZDXX_NAME,
                                                            DPLX_TYPE = dplx.ZDXX_NAME,
                                                            GL_TYPE = gl.ZDXX_NAME,
                                                            GS = t.GS
                                                        };
            return result.ToList();
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
            IQueryable<BD_JCJGDXX> jcjgds = db.BD_JCJGDXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(lampName))
                jcjgds = jcjgds.Where(t => t.JCJGD_NAME.Contains(lampName));
            jcjgds = jcjgds.OrderBy(t => t.JCJGD_NAME);
            if (skipNum != null && takeNum != null)
                jcjgds = jcjgds.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<PartsLandscapeLampModel> result = from t in jcjgds
                                                         from bp in db.BP_JGDGCJCXX
                                                         where bp.JGDGC_ID == t.JCJGD_ID
                                                         select new PartsLandscapeLampModel
                                                         {
                                                             LLId = t.JCJGD_ID,
                                                             LLName = t.JCJGD_NAME,
                                                             SWMX_ID = t.SWMX_ID,
                                                             X = t.ZXDXZB,
                                                             Y = t.ZXDYZB,
                                                             CJSJ = t.CJSJ,
                                                             KZXSL = bp.KZXSL,
                                                             KZXZLDD = bp.KZXZLDD,
                                                             ZGL = bp.ZGL,
                                                             CKJG = bp.CKJG,
                                                             BZ = bp.BZ,
                                                             JGRQ = bp.JGRQ,
                                                             YJRQ = bp.YJRQ
                                                         };
            return result.ToList();
        }
        /// <summary>
        /// 获取景观灯数量
        /// </summary>
        /// <param name="lampName"></param>
        /// <returns></returns>
        public int GetLandscapeLampsCount(string lampName)
        {
            IQueryable<BD_JCJGDXX> jcjgds = db.BD_JCJGDXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(lampName))
                jcjgds = jcjgds.Where(t => t.JCJGD_NAME.Contains(lampName));
            return jcjgds.Count();
        }
        /// <summary>
        /// 根据景观灯标识获取景观灯
        /// </summary>
        /// <param name="LLId"></param>
        /// <returns></returns>
        public PartsLandscapeLampModel GetLandscapeLampByLLId(decimal LLId)
        {
            IQueryable<PartsLandscapeLampModel> result = from t in db.BD_JCJGDXX
                                                         from bp in db.BP_JGDGCJCXX
                                                         where t.JCJGD_ID == LLId
                                                         && bp.JGDGC_ID == t.JCJGD_ID
                                                         select new PartsLandscapeLampModel
                                                         {
                                                             LLId = t.JCJGD_ID,
                                                             LLName = t.JCJGD_NAME,
                                                             SWMX_ID = t.SWMX_ID,
                                                             X = t.ZXDXZB,
                                                             Y = t.ZXDYZB,
                                                             CJSJ = t.CJSJ,
                                                             KZXSL = bp.KZXSL,
                                                             KZXZLDD = bp.KZXZLDD,
                                                             ZGL = bp.ZGL,
                                                             CKJG = bp.CKJG,
                                                             BZ = bp.BZ,
                                                             JGRQ = bp.JGRQ,
                                                             YJRQ = bp.YJRQ
                                                         };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }
        /// <summary>
        /// 根据景观灯标识获取景观灯统计
        /// </summary>
        /// <param name="LLId"></param>
        /// <returns></returns>
        public List<PartsLandscapeLampTJModel> GetLandscapeLampTJs(decimal LLId)
        {
            IQueryable<PartsLandscapeLampTJModel> result = from t in db.BP_JGDTJJCXX
                                                           join dplx in db.SYS_ZDXX
                                                           on t.DPLX_TYPE equals dplx.ZDXX_ID
                                                           into temp
                                                           from dplx in temp.DefaultIfEmpty()
                                                           join gl in db.SYS_ZDXX
                                                           on t.GL_TYPE equals gl.ZDXX_ID
                                                           into temp3
                                                           from gl in temp3.DefaultIfEmpty()
                                                           where t.JGDGC_ID == LLId
                                                           select new PartsLandscapeLampTJModel
                                                           {
                                                               LLId = t.JGDGC_ID,
                                                               DPLX_TYPE = dplx.ZDXX_NAME,
                                                               GL_TYPE = gl.ZDXX_NAME,
                                                               ZS = t.ZS
                                                           };
            return result.ToList();
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
            IQueryable<BD_JCBZXX> jcbzs = db.BD_JCBZXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(pumpName))
                jcbzs = jcbzs.Where(t => t.BZ_NAME.Contains(pumpName));
            jcbzs = jcbzs.OrderBy(t => t.BZ_NAME);
            if (skipNum != null && takeNum != null)
                jcbzs = jcbzs.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<PartsPumpModel> result = from t in jcbzs
                                                from bp in db.BP_BZJCXX
                                                join bzlx in db.SYS_ZDXX
                                                on bp.BZLX_TYPE equals bzlx.ZDXX_ID
                                                into temp
                                                from bzlx in temp.DefaultIfEmpty()
                                                where bp.JCBZ_ID == t.JCBZ_ID
                                                select new PartsPumpModel
                                                {
                                                    PumpId = t.JCBZ_ID,
                                                    PumpName = t.BZ_NAME,
                                                    SWMX_ID = t.SWMX_ID,
                                                    DLTX = t.DLTX,
                                                    X = t.ZXDXZB,
                                                    Y = t.ZXDYZB,
                                                    CJSJ = t.CJSJ,
                                                    BZLX_TYPE = bzlx.ZDXX_NAME,
                                                    SBGL = bp.SBGL,
                                                    FDJGL = bp.FDJGL,
                                                    SBSL = bp.SBSL,
                                                    CSKGJ = bp.CSKGJ,
                                                    CSKWZ = bp.CSKWZ,
                                                    DWSJLL = bp.DWSJLL,
                                                    DZXXXX = bp.DZXXXX,
                                                    CKJG = bp.CKJG,
                                                    BZ = bp.BZ,
                                                    JGRQ = bp.JGRQ,
                                                    YJRQ = bp.YJRQ
                                                };
            return result.ToList();
        }
        /// <summary>
        /// 获取泵站数量
        /// 参数可选
        /// </summary>
        /// <param name="pumpName"></param>
        /// <returns></returns>
        public int GetPumpsCount(string pumpName)
        {
            IQueryable<BD_JCBZXX> jcbzs = db.BD_JCBZXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(pumpName))
                jcbzs = jcbzs.Where(t => t.BZ_NAME.Contains(pumpName));
            int count = jcbzs.Count();
            return count;
        }
        /// <summary>
        /// 根据泵站标识获取泵站
        /// </summary>
        /// <param name="pumpId"></param>
        /// <returns></returns>
        public PartsPumpModel GetPumpByPumpId(decimal pumpId)
        {
            IQueryable<PartsPumpModel> result = from t in db.BD_JCBZXX
                                                from bp in db.BP_BZJCXX
                                                join bzlx in db.SYS_ZDXX
                                                on bp.BZLX_TYPE equals bzlx.ZDXX_ID
                                                into temp
                                                from bzlx in temp.DefaultIfEmpty()
                                                where t.JCBZ_ID == pumpId
                                                && bp.JCBZ_ID == t.JCBZ_ID
                                                select new PartsPumpModel
                                                {
                                                    PumpId = t.JCBZ_ID,
                                                    PumpName = t.BZ_NAME,
                                                    SWMX_ID = t.SWMX_ID,
                                                    DLTX = t.DLTX,
                                                    X = t.ZXDXZB,
                                                    Y = t.ZXDYZB,
                                                    CJSJ = t.CJSJ,
                                                    BZLX_TYPE = bzlx.ZDXX_NAME,
                                                    SBGL = bp.SBGL,
                                                    FDJGL = bp.FDJGL,
                                                    SBSL = bp.SBSL,
                                                    CSKGJ = bp.CSKGJ,
                                                    CSKWZ = bp.CSKWZ,
                                                    DWSJLL = bp.DWSJLL,
                                                    DZXXXX = bp.DZXXXX,
                                                    CKJG = bp.CKJG,
                                                    BZ = bp.BZ,
                                                    JGRQ = bp.JGRQ,
                                                    YJRQ = bp.YJRQ
                                                };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
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
            IQueryable<BP_JGLDJCXX> jglds = db.BP_JGLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(coverLoadName))
                jglds = jglds.Where(t => t.LD_NAME.Contains(coverLoadName));
            jglds = jglds.OrderBy(t => t.LD_NAME);
            if (skipNum != null && takeNum != null)
                jglds = jglds.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));
            IQueryable<PartsCoverLoadModel> result = from t in jglds
                                                     join gx in db.BP_JGLDJCLKLDCXDGX
                                                     on t.JGLD_ID equals gx.JGLD_ID
                                                     into temp
                                                     from gx in temp.DefaultIfEmpty()
                                                     join cxd in db.BD_JCLKLDCXDXX
                                                     on gx.JCLKLDCXD_ID equals cxd.JCLKLDCXD_ID
                                                     into temp2
                                                     from cxd in temp2.DefaultIfEmpty()
                                                     join jgxz in db.SYS_ZDXX
                                                     on t.JGXZ_TYPE equals jgxz.ZDXX_ID
                                                     into temp3
                                                     from jgxz in temp3.DefaultIfEmpty()
                                                     select new PartsCoverLoadModel
                                                     {
                                                         CoverLoadId = t.JGLD_ID,
                                                         CoverLoadName = t.LD_NAME,
                                                         JGXZ_TYPE = jgxz.ZDXX_NAME,
                                                         QQD = t.QQD,
                                                         CKJG = t.CKJG,
                                                         BZ = t.BZ,
                                                         JGRQ = t.JGRQ,
                                                         YJRQ = t.YJRQ,
                                                         X = cxd.ZXDXZB,
                                                         Y = cxd.ZXDYZB
                                                     };
            List<PartsCoverLoadModel> result1 = new List<PartsCoverLoadModel>();
            foreach (PartsCoverLoadModel item in result)
            {
                if (result1.Select(t => t.CoverLoadId).Contains(item.CoverLoadId))
                    continue;
                result1.Add(item);
            }
            return result1;
        }
        /// <summary>
        /// 获取井盖路段数量
        /// </summary>
        /// <param name="coverLoadName"></param>
        /// <returns></returns>
        public int GetCoverLoadsCount(string coverLoadName)
        {
            IQueryable<BP_JGLDJCXX> jglds = db.BP_JGLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(coverLoadName))
                jglds = jglds.Where(t => t.LD_NAME.Contains(coverLoadName));
            int count = jglds.Count();
            return count;
        }
        /// <summary>
        /// 根据井盖路段标识获取井盖路段
        /// </summary>
        /// <param name="coverLoadId"></param>
        /// <returns></returns>
        public PartsCoverLoadModel GetCoverLoadByCoverLoadId(decimal coverLoadId)
        {
            IQueryable<PartsCoverLoadModel> result = from t in db.BP_JGLDJCXX
                                                     join gx in db.BP_JGLDJCLKLDCXDGX
                                                     on t.JGLD_ID equals gx.JGLD_ID
                                                     into temp
                                                     from gx in temp.DefaultIfEmpty()
                                                     join cxd in db.BD_JCLKLDCXDXX
                                                     on gx.JCLKLDCXD_ID equals cxd.JCLKLDCXD_ID
                                                     into temp2
                                                     from cxd in temp2.DefaultIfEmpty()
                                                     join jgxz in db.SYS_ZDXX
                                                     on t.JGXZ_TYPE equals jgxz.ZDXX_ID
                                                     into temp3
                                                     from jgxz in temp3.DefaultIfEmpty()
                                                     where t.JGLD_ID == coverLoadId
                                                     select new PartsCoverLoadModel
                                                     {
                                                         CoverLoadId = t.JGLD_ID,
                                                         CoverLoadName = t.LD_NAME,
                                                         JGXZ_TYPE = jgxz.ZDXX_NAME,
                                                         QQD = t.QQD,
                                                         CKJG = t.CKJG,
                                                         BZ = t.BZ,
                                                         JGRQ = t.JGRQ,
                                                         YJRQ = t.YJRQ,
                                                         X = cxd.ZXDXZB,
                                                         Y = cxd.ZXDYZB
                                                     };
            if (result.Count() > 0)
                return result.FirstOrDefault();
            return null;
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
            IQueryable<BP_GYLDJCXX> pgs = db.BP_GYLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(parkGreenName))
                pgs = pgs.Where(t => t.GYLD_NAME.Contains(parkGreenName));
            pgs = pgs.OrderBy(t => t.GYLD_NAME);
            if (skipNum != null && takeNum != null)
                pgs = pgs.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<PartsParkGreenModel> result = from t in pgs
                                                     join gylddj in db.SYS_ZDXX
                                                     on t.GYLDDJ_TYPE equals gylddj.ZDXX_ID
                                                     into temp
                                                     from gylddj in temp.DefaultIfEmpty()
                                                     select new PartsParkGreenModel
                                                     {
                                                         ParkGreenId = t.GYLD_ID,
                                                         PartGreenName = t.GYLD_NAME,
                                                         GYLDDJ_TYPE = gylddj.ZDXX_NAME,
                                                         QYMS = t.QYMS,
                                                         MJ = t.MJ,
                                                         DLTX = t.DLTX,
                                                         X = t.ZXDXZB,
                                                         Y = t.ZXDYZB,
                                                         CKJG = t.CKJG,
                                                         BZ = t.BZ,
                                                         JGRQ = t.JGRQ,
                                                         YJRQ = t.YJRQ
                                                     };
            return result.ToList();
        }
        /// <summary>
        /// 获取公园绿地数量
        /// 参数可选
        /// </summary>
        /// <param name="parkGreenName"></param>
        /// <returns></returns>
        public int GetParkGreensCount(string parkGreenName)
        {
            IQueryable<BP_GYLDJCXX> pgs = db.BP_GYLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(parkGreenName))
                pgs = pgs.Where(t => t.GYLD_NAME.Contains(parkGreenName));
            int count = pgs.Count();
            return count;
        }
        /// <summary>
        /// 根据公园绿地标识获取公园绿地
        /// </summary>
        /// <param name="parkGreenId"></param>
        /// <returns></returns>
        public PartsParkGreenModel GetParkGreenByParkGreenId(decimal parkGreenId)
        {
            IQueryable<BP_GYLDJCXX> pgs = db.BP_GYLDJCXX.Where(t => t.SFLJSC == 0);
            IQueryable<PartsParkGreenModel> result = from t in pgs
                                                     join gylddj in db.SYS_ZDXX
                                                     on t.GYLDDJ_TYPE equals gylddj.ZDXX_ID
                                                     into temp
                                                     from gylddj in temp.DefaultIfEmpty()
                                                     where t.GYLD_ID == parkGreenId
                                                     select new PartsParkGreenModel
                                                     {
                                                         ParkGreenId = t.GYLD_ID,
                                                         PartGreenName = t.GYLD_NAME,
                                                         GYLDDJ_TYPE = gylddj.ZDXX_NAME,
                                                         QYMS = t.QYMS,
                                                         MJ = t.MJ,
                                                         DLTX = t.DLTX,
                                                         X = t.ZXDXZB,
                                                         Y = t.ZXDYZB,
                                                         CKJG = t.CKJG,
                                                         BZ = t.BZ,
                                                         JGRQ = t.JGRQ,
                                                         YJRQ = t.YJRQ
                                                     };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
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
            IQueryable<BP_DLLDJCXX> lgs = db.BP_DLLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(loadGreenName))
                lgs = lgs.Where(t => t.DLLD_NAME.Contains(loadGreenName));
            lgs = lgs.OrderBy(t => t.DLLD_NAME);
            if (skipNum != null && takeNum != null)
                lgs = lgs.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<PartsLoadGreenModel> result = from t in lgs
                                                     join dllddj in db.SYS_ZDXX
                                                     on t.DLLDDJ_TYPE equals dllddj.ZDXX_ID
                                                     into temp
                                                     from dllddj in temp.DefaultIfEmpty()
                                                     join gx in db.BP_DLLDJCLKLDCXDGX
                                                     on t.DLLD_ID equals gx.DLLD_ID
                                                     into temp2
                                                     from gx in temp2.DefaultIfEmpty()
                                                     join cxd in db.BD_JCLKLDCXDXX
                                                     on gx.JCLKLDCXD_ID equals cxd.JCLKLDCXD_ID
                                                     into temp3
                                                     from cxd in temp3.DefaultIfEmpty()
                                                     select new PartsLoadGreenModel
                                                     {
                                                         LoadGreenId = t.DLLD_ID,
                                                         LoadGreenName = t.DLLD_NAME,
                                                         DLLDDJ_TYPE = dllddj.ZDXX_NAME,
                                                         QYMS = t.QYMS,
                                                         MJ = t.MJ,
                                                         CKJG = t.CKJG,
                                                         BZ = t.BZ,
                                                         JGRQ = t.JGRQ,
                                                         YJRQ = t.YJRQ,
                                                         X = cxd.ZXDXZB,
                                                         Y = cxd.ZXDYZB
                                                     };
            List<PartsLoadGreenModel> result1 = new List<PartsLoadGreenModel>();
            foreach (PartsLoadGreenModel item in result)
            {
                if (result1.Select(t => t.LoadGreenId).Contains(item.LoadGreenId))
                    continue;
                result1.Add(item);
            }
            return result1.ToList();
        }
        /// <summary>
        /// 获取道路绿地数量
        /// 参数可选
        /// </summary>
        /// <param name="loadGreenName"></param>
        /// <returns></returns>
        public int GetLoadGreensCount(string loadGreenName)
        {

            IQueryable<BP_DLLDJCXX> lgs = db.BP_DLLDJCXX.Where(t => t.SFLJSC == 0);
            if (!string.IsNullOrEmpty(loadGreenName))
                lgs = lgs.Where(t => t.DLLD_NAME.Contains(loadGreenName));
            int count = lgs.Count();
            return count;
        }
        /// <summary>
        /// 根据道路绿地标识获取道路绿地
        /// </summary>
        /// <param name="loadGreenId"></param>
        /// <returns></returns>
        public PartsLoadGreenModel GetLoadGreenByLoadGreenId(decimal loadGreenId)
        {
            IQueryable<BP_DLLDJCXX> lgs = db.BP_DLLDJCXX.Where(t => t.SFLJSC == 0);
            IQueryable<PartsLoadGreenModel> result = from t in lgs
                                                     join dllddj in db.SYS_ZDXX
                                                     on t.DLLDDJ_TYPE equals dllddj.ZDXX_ID
                                                     into temp
                                                     from dllddj in temp.DefaultIfEmpty()
                                                     join gx in db.BP_DLLDJCLKLDCXDGX
                                                     on t.DLLD_ID equals gx.DLLD_ID
                                                     into temp2
                                                     from gx in temp2.DefaultIfEmpty()
                                                     join cxd in db.BD_JCLKLDCXDXX
                                                     on gx.JCLKLDCXD_ID equals cxd.JCLKLDCXD_ID
                                                     into temp3
                                                     from cxd in temp3.DefaultIfEmpty()
                                                     where t.DLLD_ID == loadGreenId
                                                     select new PartsLoadGreenModel
                                                     {
                                                         LoadGreenId = t.DLLD_ID,
                                                         LoadGreenName = t.DLLD_NAME,
                                                         DLLDDJ_TYPE = dllddj.ZDXX_NAME,
                                                         QYMS = t.QYMS,
                                                         MJ = t.MJ,
                                                         CKJG = t.CKJG,
                                                         BZ = t.BZ,
                                                         JGRQ = t.JGRQ,
                                                         YJRQ = t.YJRQ
                                                     };
            if (result.Count() > 0)
                return result.FirstOrDefault();
            return null;
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
            IQueryable<BD_JCFHLDXX> pgs = db.BD_JCFHLDXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(protectionGreenName))
                pgs = pgs.Where(t => t.JCFHLD_NAME.Contains(protectionGreenName));
            pgs = pgs.OrderBy(t => t.JCFHLD_NAME);
            if (skipNum != null && takeNum != null)
                pgs = pgs.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<PartsProtectionGreenModel> result = from t in pgs
                                                           from bp in db.BP_FHLDJCXX
                                                           join fhlddj in db.SYS_ZDXX
                                                           on bp.FHLDDJ_TYPE equals fhlddj.ZDXX_ID
                                                           into temp
                                                           from fhlddj in temp.DefaultIfEmpty()
                                                           join fhldlx in db.SYS_ZDXX
                                                           on bp.FHLDLX_TYPE equals fhldlx.ZDXX_ID
                                                           into temp2
                                                           from fhldlx in temp2.DefaultIfEmpty()
                                                           join gx in db.BD_JCFHLDDFHLDGX
                                                           on t.JCFHLD_ID equals gx.JCFHLD_ID
                                                           into temp3
                                                           from gx in temp3.DefaultIfEmpty()
                                                           join ld in db.BD_JCFHLDDXX
                                                           on gx.JCFHLDD_ID equals ld.JCFHLDD_ID
                                                           into temp4
                                                           from ld in temp4.DefaultIfEmpty()
                                                           where t.JCFHLD_ID == bp.FHLD_ID
                                                           select new PartsProtectionGreenModel
                                                           {
                                                               ProtectionGreenId = t.JCFHLD_ID,
                                                               ProtectGreenName = t.JCFHLD_NAME,
                                                               SWMX_ID = t.SWMX_ID,
                                                               DLTX = t.DLTX,//这里是无数据的，需要拼接数据的，目前先不处理，有需求的时候再处理
                                                               X = t.ZXDXZB,
                                                               Y = t.ZXDYZB,
                                                               CJSJ = t.CJSJ,
                                                               FHLDDJ_TYPE = fhlddj.ZDXX_NAME,
                                                               FHLDLX_TYPE = fhldlx.ZDXX_NAME,
                                                               QYMS = bp.QYMS,
                                                               MJ = bp.MJ,
                                                               CKJG = bp.CKJG,
                                                               BZ = bp.BZ,
                                                               JGRQ = bp.JGRQ,
                                                               YJRQ = bp.YJRQ
                                                           };
            List<PartsProtectionGreenModel> result1 = new List<PartsProtectionGreenModel>();
            foreach (PartsProtectionGreenModel item in result)
            {
                if (result1.Select(t => t.ProtectionGreenId).Contains(item.ProtectionGreenId))
                    continue;
                result1.Add(item);
            }
            return result1.ToList();
        }
        /// <summary>
        /// 获取防护绿地数量
        /// 参数可选
        /// </summary>
        /// <param name="protectionGreenName"></param>
        /// <returns></returns>
        public int GetProtectionGreensCount(string protectionGreenName)
        {
            IQueryable<BD_JCFHLDXX> pgs = db.BD_JCFHLDXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(protectionGreenName))
                pgs = pgs.Where(t => t.JCFHLD_NAME.Contains(protectionGreenName));
            int count = pgs.Count();
            return count;
        }
        /// <summary>
        /// 根据防护绿地标识获取防护绿地
        /// </summary>
        /// <param name="protectionGreenId"></param>
        /// <returns></returns>
        public PartsProtectionGreenModel GetProtectionGreen(decimal protectionGreenId)
        {
            IQueryable<BD_JCFHLDXX> pgs = db.BD_JCFHLDXX.Where(t => t.SFYX == "1");
            IQueryable<PartsProtectionGreenModel> result = from t in pgs
                                                           from bp in db.BP_FHLDJCXX
                                                           join fhlddj in db.SYS_ZDXX
                                                           on bp.FHLDDJ_TYPE equals fhlddj.ZDXX_ID
                                                           into temp
                                                           from fhlddj in temp.DefaultIfEmpty()
                                                           join fhldlx in db.SYS_ZDXX
                                                           on bp.FHLDLX_TYPE equals fhldlx.ZDXX_ID
                                                           into temp2
                                                           from fhldlx in temp2.DefaultIfEmpty()
                                                           join gx in db.BD_JCFHLDDFHLDGX
                                                           on t.JCFHLD_ID equals gx.JCFHLD_ID
                                                           into temp3
                                                           from gx in temp3.DefaultIfEmpty()
                                                           join ld in db.BD_JCFHLDDXX
                                                           on gx.JCFHLDD_ID equals ld.JCFHLDD_ID
                                                           into temp4
                                                           from ld in temp4.DefaultIfEmpty()
                                                           where t.JCFHLD_ID == bp.FHLD_ID
                                                           && t.JCFHLD_ID == protectionGreenId
                                                           select new PartsProtectionGreenModel
                                                           {
                                                               ProtectionGreenId = t.JCFHLD_ID,
                                                               ProtectGreenName = t.JCFHLD_NAME,
                                                               SWMX_ID = t.SWMX_ID,
                                                               DLTX = t.DLTX,//这里是无数据的，需要拼接数据的，目前先不处理，有需求的时候再处理
                                                               X = t.ZXDXZB,
                                                               Y = t.ZXDYZB,
                                                               CJSJ = t.CJSJ,
                                                               FHLDDJ_TYPE = fhlddj.ZDXX_NAME,
                                                               FHLDLX_TYPE = fhldlx.ZDXX_NAME,
                                                               QYMS = bp.QYMS,
                                                               MJ = bp.MJ,
                                                               CKJG = bp.CKJG,
                                                               BZ = bp.BZ,
                                                               JGRQ = bp.JGRQ,
                                                               YJRQ = bp.YJRQ
                                                           };
            if (result.Count() > 0)
                return result.FirstOrDefault();
            return null;
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
            IQueryable<BD_JCGCXX> toilts = db.BD_JCGCXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(toiltName))
                toilts = toilts.Where(t => t.JCGC_NAME.Contains(toiltName));
            toilts = toilts.OrderBy(t => t.JCGC_NAME);
            if (skipNum != null && takeNum != null)
                toilts = toilts.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<PartsToiltModel> result = from t in toilts
                                                 from bp in db.BP_GCJCXX
                                                 join xj in db.SYS_ZDXX
                                                 on bp.XJ_TYPE equals xj.ZDXX_ID
                                                 into temp
                                                 from xj in temp.DefaultIfEmpty()
                                                 where t.JCGC_ID == bp.JCGC_ID
                                                 select new PartsToiltModel
                                                 {
                                                     ToiltId = t.JCGC_ID,
                                                     ToiltName = t.JCGC_NAME,
                                                     SWMX_ID = t.SWMX_ID,
                                                     DLTX = t.DLTX,
                                                     X = t.ZXDXZB,
                                                     Y = t.ZXDYZB,
                                                     CJSJ = t.CJSJ,
                                                     GCDZ = bp.GCDZ,
                                                     GCMJ = bp.GCMJ,
                                                     XJ_TYPE = xj.ZDXX_NAME,
                                                     MKWS = bp.MKWS,
                                                     WKWS = bp.WKWS,
                                                     SFYMYS = bp.SFYMYS,
                                                     SFYCJRZY = bp.SFYCJRZY,
                                                     CKJG = bp.CKJG,
                                                     BZ = bp.BZ,
                                                     JGRQ = bp.JGRQ,
                                                     YJRQ = bp.YJRQ
                                                 };
            return result.ToList();
        }
        /// <summary>
        /// 获取公厕数量
        /// </summary>
        /// <param name="toiltName"></param>
        /// <returns></returns>
        public int GetToiltsCount(string toiltName)
        {
            IQueryable<BD_JCGCXX> toilts = db.BD_JCGCXX.Where(t => t.SFYX == "1");
            if (!string.IsNullOrEmpty(toiltName))
                toilts = toilts.Where(t => t.JCGC_NAME.Contains(toiltName));
            int count = toilts.Count();
            return count;
        }
        /// <summary>
        /// 根据公厕标识获取公厕
        /// </summary>
        /// <param name="toiltId"></param>
        /// <returns></returns>
        public PartsToiltModel GetToilt(decimal toiltId)
        {
            IQueryable<BD_JCGCXX> toilts = db.BD_JCGCXX.Where(t => t.SFYX == "1");
            IQueryable<PartsToiltModel> result = from t in toilts
                                                 from bp in db.BP_GCJCXX
                                                 join xj in db.SYS_ZDXX
                                                 on bp.XJ_TYPE equals xj.ZDXX_ID
                                                 into temp
                                                 from xj in temp.DefaultIfEmpty()
                                                 where t.JCGC_ID == bp.JCGC_ID
                                                 && t.JCGC_ID == toiltId
                                                 select new PartsToiltModel
                                                 {
                                                     ToiltId = t.JCGC_ID,
                                                     ToiltName = t.JCGC_NAME,
                                                     SWMX_ID = t.SWMX_ID,
                                                     DLTX = t.DLTX,
                                                     X = t.ZXDXZB,
                                                     Y = t.ZXDYZB,
                                                     CJSJ = t.CJSJ,
                                                     GCDZ = bp.GCDZ,
                                                     GCMJ = bp.GCMJ,
                                                     XJ_TYPE = xj.ZDXX_NAME,
                                                     MKWS = bp.MKWS,
                                                     WKWS = bp.WKWS,
                                                     SFYMYS = bp.SFYMYS,
                                                     SFYCJRZY = bp.SFYCJRZY,
                                                     CKJG = bp.CKJG,
                                                     BZ = bp.BZ,
                                                     JGRQ = bp.JGRQ,
                                                     YJRQ = bp.YJRQ
                                                 };
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
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
            IQueryable<BP_HDJCXX> rivers = from t in db.BP_HDJCXX
                                           from bd in db.BD_JCHDXX
                                           where bd.JCHD_ID == t.JCHD_ID
                                           && bd.SFYX == "1"
                                           select t;

            if (!string.IsNullOrEmpty(riverName))
                rivers = rivers.Where(t => t.JCHD_NAME.Contains(riverName));
            rivers = rivers.OrderBy(t => t.JCHD_NAME);
            if (skipNum != null && takeNum != null)
                rivers = rivers.Skip(Convert.ToInt32(skipNum)).Take(Convert.ToInt32(takeNum));

            IQueryable<PartsRiverModel> result = from t in db.BD_JCHDXX
                                                 from bp in rivers
                                                 join hdlx in db.SYS_ZDXX
                                                 on bp.HDLX_TYPE equals hdlx.ZDXX_ID
                                                 into temp
                                                 from hdlx in temp.DefaultIfEmpty()
                                                 join szdj in db.SYS_ZDXX
                                                 on bp.SZDJ_TYPE equals szdj.ZDXX_ID
                                                 into temp2
                                                 from szdj in temp2.DefaultIfEmpty()
                                                 join bjdj in db.SYS_ZDXX
                                                 on bp.BJDJ_TYPE equals bjdj.ZDXX_ID
                                                 into temp3
                                                 from bjdj in temp3.DefaultIfEmpty()
                                                 join gx in db.BD_JCSXHDJCXXGX
                                                 on t.JCHD_ID equals gx.JCHD_ID
                                                 into temp4
                                                 from gx in temp4.DefaultIfEmpty()
                                                 join sz in db.BD_JCSXXX
                                                 on gx.JCSX_ID equals sz.JCSX_ID
                                                 into temp5
                                                 from sz in temp5.DefaultIfEmpty()
                                                 where t.JCHD_ID == bp.JCHD_ID
                                                 select new PartsRiverModel
                                                 {
                                                     RiverId = t.JCHD_ID,
                                                     RiverName = t.JCHD_NAME,
                                                     SWMX_ID = t.SWMX_ID,
                                                     X = t.ZXDXZB,
                                                     Y = t.ZXDYZB,
                                                     CJSJ = t.CJSJ,
                                                     HDCD = bp.HDCD,
                                                     HDKD = bp.HDKD,
                                                     HDMJ = bp.HDMJ,
                                                     HDLX_TYPE = hdlx.ZDXX_NAME,
                                                     SZDJ_TYPE = szdj.ZDXX_NAME,
                                                     BJDJ_TYPE = bjdj.ZDXX_NAME,
                                                     SZYHSM = bp.SZYHSM,
                                                     HDQD = bp.HDQD,
                                                     HDZD = bp.HDZD,
                                                     BHDZH = bp.BHDZH
                                                 };
            List<PartsRiverModel> result1 = new List<PartsRiverModel>();
            foreach (PartsRiverModel item in result)
            {
                if (result1.Select(t => t.RiverId).Contains(item.RiverId))
                    continue;
                result1.Add(item);
            }
            return result1.ToList();
        }
        /// <summary>
        /// 获取河道数量
        /// 参数可选
        /// </summary>
        /// <param name="riverName"></param>
        /// <returns></returns>
        public int GetRiversCount(string riverName)
        {
            IQueryable<BP_HDJCXX> rivers = from t in db.BP_HDJCXX
                                           from bd in db.BD_JCHDXX
                                           where bd.JCHD_ID == t.JCHD_ID
                                           && bd.SFYX == "1"
                                           select t;
            if (!string.IsNullOrEmpty(riverName))
                rivers = rivers.Where(t => t.JCHD_NAME.Contains(riverName));
            int count = rivers.Count();
            return count;
        }
        /// <summary>
        /// 根据河道标识获取河道
        /// </summary>
        /// <param name="riverId"></param>
        /// <returns></returns>
        public PartsRiverModel GetRiver(decimal riverId)
        {
            IQueryable<BP_HDJCXX> rivers = db.BP_HDJCXX;
            IQueryable<PartsRiverModel> result = from t in db.BD_JCHDXX
                                                 from bp in rivers
                                                 join hdlx in db.SYS_ZDXX
                                                 on bp.HDLX_TYPE equals hdlx.ZDXX_ID
                                                 into temp
                                                 from hdlx in temp.DefaultIfEmpty()
                                                 join szdj in db.SYS_ZDXX
                                                 on bp.SZDJ_TYPE equals szdj.ZDXX_ID
                                                 into temp2
                                                 from szdj in temp2.DefaultIfEmpty()
                                                 join bjdj in db.SYS_ZDXX
                                                 on bp.BJDJ_TYPE equals bjdj.ZDXX_ID
                                                 into temp3
                                                 from bjdj in temp3.DefaultIfEmpty()
                                                 join gx in db.BD_JCSXHDJCXXGX
                                                 on t.JCHD_ID equals gx.JCHD_ID
                                                 into temp4
                                                 from gx in temp4.DefaultIfEmpty()
                                                 join sz in db.BD_JCSXXX
                                                 on gx.JCSX_ID equals sz.JCSX_ID
                                                 into temp5
                                                 from sz in temp5.DefaultIfEmpty()
                                                 where t.JCHD_ID == bp.JCHD_ID
                                                 && t.JCHD_ID == riverId
                                                 select new PartsRiverModel
                                                 {
                                                     RiverId = t.JCHD_ID,
                                                     RiverName = t.JCHD_NAME,
                                                     SWMX_ID = t.SWMX_ID,
                                                     X = t.ZXDXZB,
                                                     Y = t.ZXDYZB,
                                                     CJSJ = t.CJSJ,
                                                     HDCD = bp.HDCD,
                                                     HDKD = bp.HDKD,
                                                     HDMJ = bp.HDMJ,
                                                     HDLX_TYPE = hdlx.ZDXX_NAME,
                                                     SZDJ_TYPE = szdj.ZDXX_NAME,
                                                     BJDJ_TYPE = bjdj.ZDXX_NAME,
                                                     SZYHSM = bp.SZYHSM,
                                                     HDQD = bp.HDQD,
                                                     HDZD = bp.HDZD,
                                                     BHDZH = bp.BHDZH
                                                 };
            if (result.Count() > 0)
                return result.FirstOrDefault();
            return null;
        }
        #endregion
    }
}
