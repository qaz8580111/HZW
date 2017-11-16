using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.LogBLLs;
using Taizhou.PLE.Model.WebServiceModels;
using WebService.Model;

namespace WebService.BLL
{
    public class IPCaseBLL
    {
        /// <summary>
        /// 上报违停案件
        /// </summary>
        /// <param name="ipCase">违停案件对象</param>
        public static void SaveIPCase(IPCase ipCase)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            decimal XLH = 0;
            string ErrCode, ErrStr;

            db.PRO_SAVE_CAMERACOLLATE(ipCase.carNo, ipCase.carType, ipCase.caseTime,
        ipCase.WTUnitID, ipCase.addressCode, ipCase.address, "7003",
        "不按规定停放影响其他车辆和行人通行的", ipCase.WTUserID, ipCase.WTUserID,
        "000", "G", ipCase.documentCode, 0, out XLH, null, null, null, null,
        null, null, null, null, null, ipCase.lon, ipCase.lat, out ErrCode, out ErrStr);

            string sql = "insert into ht_veh_picrec values(:XLH,:IMG1,:IMG2,:IMG3,:IMG4,:XGZJXH)";

            var Pars = new OracleParameter[] 
            { 
                new OracleParameter 
                {
                 ParameterName="XLH",
                 DbType = System.Data.DbType.Decimal,
                 Value = XLH
                },
                new OracleParameter 
                {
                 ParameterName="IMG1",
                 DbType = System.Data.DbType.Binary,
                 Value = ipCase.picture1
                },
                new OracleParameter 
                {
                 ParameterName="IMG2",
                 DbType = System.Data.DbType.Binary,
                 Value = ipCase.picture2
                },
                new OracleParameter 
                {
                 ParameterName="IMG3",
                 DbType = System.Data.DbType.Binary,
                 Value = ipCase.picture3
                },
                new OracleParameter 
                {
                 ParameterName="IMG4",
                 DbType = System.Data.DbType.Binary,
                 Value = ipCase.picture4
                },
                new OracleParameter 
                {
                 ParameterName="XGZJXH",
                 DbType = System.Data.DbType.String,
                 Value = null
                }
            };

            db.Database.ExecuteSqlCommand(sql, Pars);
        }

        /// <summary>
        /// 根据所属单位及经纬度获取道路编号及违法地点
        /// </summary>
        /// <param name="ssdw">所属单位</param>
        /// <param name="strLon">经度</param>
        /// <param name="strLat">纬度</param>
        /// <returns>道路编号及违法地点</returns>
        public static string GetDDBHByLonAndLat(string ssdw, string strLon, string strLat)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            double temp = 0d, lon = 0d, lat = 0d;

            if (double.TryParse(strLon, out temp))
            {
                lon = temp;
            }

            if (double.TryParse(strLat, out temp))
            {
                lat = temp;
            }

            if (lon == 0 || lat == 0)
            {
                return null;
            }

            string sql = @"select Cast(dis.disdance AS Decimal(18,3)),
dis.ddbh,dis.wfdd from
(select sqrt((power(substr(v.clbh,1,instr(v.clbh,'|') - 1) - :lon,2) + 
power(substr(v.clbh,instr(v.clbh,'|') + 1,length(v.clbh)) - :lat,2))) disdance,v.ddbh,v.wfdd 
from ht_veh_viorec v where v.sjss = :ssdw and v.clbh is not null) dis order by disdance";

            var Pars = new OracleParameter[] 
            { 
                new OracleParameter 
                {
                 ParameterName="lon",
                 DbType = System.Data.DbType.Double,
                 Value = lon
                },
                new OracleParameter 
                {
                 ParameterName="lat",
                 DbType = System.Data.DbType.Double,
                 Value = lat
                },
                new OracleParameter 
                {
                 ParameterName="ssdw",
                 DbType = System.Data.DbType.String,
                 Value = ssdw
                }
            };

            try
            {
                Disdance dance = db.Database.SqlQuery<Disdance>(sql, Pars).First();

                return dance != null ? dance.ddbh + "|" + dance.wfdd : "";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}