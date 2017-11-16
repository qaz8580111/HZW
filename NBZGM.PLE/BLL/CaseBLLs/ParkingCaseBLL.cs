using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ParkingCaseModels;
namespace Taizhou.PLE.BLL.CaseBLLs
{
    public class ParkingCaseBLL
    {
        /// <summary>
        /// 根据日期及违停次数获取违停案件列表
        /// </summary>
        /// <param name="strStartDate">起始日期</param>
        /// <param name="strEndDate">结束日期</param>
        /// <param name="strWTCS">违停次数</param>
        /// <returns>违停案件列表</returns>
        public static IQueryable<ParkingCase> GetPakringCasesByDateAndWTCS
            (string strStartDate, string strEndDate, string strWTCS)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            //起始日期 & 结束日期
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(1).AddMinutes(-1);
            DateTime tempDate;
            //违停次数
            decimal wtcs = 0;
            decimal tempWTCS;

            if (DateTime.TryParse(strStartDate, out tempDate))
            {
                startDate = tempDate;
            }

            if (DateTime.TryParse(strEndDate, out tempDate))
            {
                endDate = tempDate.AddDays(1).AddMinutes(-1);
            }

            if (decimal.TryParse(strWTCS, out tempWTCS))
            {
                wtcs = tempWTCS;
            }

            var result = from v in db.HT_VEH_VIOREC
                         where v.WFSJ >= startDate
                            && v.WFSJ <= endDate
                         group v by v.HPHM into g
                         where g.Count() >= wtcs
                         select new
                         {
                             g.Key,
                             parkingCount = g.Count()
                         };

            IQueryable<ParkingCase> results = from viorec in db.HT_VEH_VIOREC
                                              from en in db.HT_CD_ENUM
                                              from r in result
                                              where en.CAT == "号牌种类"
                                                 && en.KEY == viorec.HPZL
                                                 && r.Key == viorec.HPHM
                                                 && viorec.WFSJ >= startDate
                                                 && viorec.WFSJ <= endDate
                                              orderby r.parkingCount descending, r.Key,
                                              viorec.WFSJ descending
                                              select new ParkingCase
                                              {
                                                  XLH = viorec.XLH,
                                                  carNo = viorec.HPHM,
                                                  carType = en.VALUE,
                                                  caseTime = viorec.WFSJ,
                                                  caseAddress = viorec.WFDD,
                                                  CJDW = viorec.SJSS,
                                                  CLDW = viorec.CLJG,
                                                  CLZT = viorec.JDZT,
                                                  FZSHR = viorec.FZSHR,
                                                  FZSHSJ = viorec.FZSHSJ,
                                                  FZSHYJ = viorec.FZSHYJ,
                                                  JDJG = viorec.JDJG,
                                                  CSBJ = viorec.CSBJ
                                              };

            return results;
        }

        /// <summary>
        /// 获取所有单位列表
        /// </summary>
        /// <returns>所有单位列表</returns>
        public static IQueryable<HT_CD_GOV> GetGOVs()
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            return db.HT_CD_GOV.Where(t => t.XZQH != "3310990000")
                .OrderBy(t => t.XZQH);
        }

        /// <summary>
        /// 根据处理状态标识获取处理状态名称
        /// </summary>
        /// <param name="clzt">处理状态标识</param>
        /// <returns>处理状态名称</returns>
        public static string GetCLZTNameByCLZT(string clzt)
        {
            ///处理状态名称
            string CLZTName = "";

            if (clzt.Equals("1"))
            {
                CLZTName = "已校对";
            }
            else if (clzt.Equals("2"))
            {
                CLZTName = "已通知";
            }
            else if (clzt.Equals("3"))
            {
                CLZTName = "已确认";
            }
            else if (clzt.Equals("4"))
            {
                CLZTName = "已处理";
            }

            return CLZTName;
        }

        /// <summary>
        /// 根据违停案件序列号获取该违停案件对象
        /// </summary>
        /// <param name="XLH">违停案件序列号</param>
        /// <returns>该违停案件对象</returns>
        public static ParkingCase GetPakringCaseByXLH(decimal XLH)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            ParkingCase parkingCase = (from viorec in db.HT_VEH_VIOREC
                                       from en in db.HT_CD_ENUM
                                       from gov in db.HT_CD_GOV
                                       from vc in db.HT_CD_VIOCODE
                                       where en.CAT == "号牌种类"
                                          && en.KEY == viorec.HPZL
                                          && viorec.SJSS == gov.XZQH
                                          && vc.WFXW == viorec.WFXW
                                          && viorec.XLH == XLH
                                       select new ParkingCase
                                       {
                                           XLH = XLH,
                                           carNo = viorec.HPHM,
                                           carType = en.VALUE,
                                           caseTime = viorec.WFSJ,
                                           caseAddress = viorec.WFDD,
                                           CJDW = gov.JC,
                                           CLZT = viorec.JDZT,
                                           WFXWName = vc.XWMS,
                                           CJR = viorec.CJR,
                                           DSR = viorec.DSR,
                                           DSRDH = viorec.DSRDH,
                                           DSRDZ = viorec.DSRDZ,
                                           FPHM = viorec.FPHM,
                                           FKJE = viorec.FKJE,
                                           JDR = viorec.JDR,
                                           JDRQ = viorec.JDRQ,
                                           FZSHR = viorec.FZSHR,
                                           FZSHSJ = viorec.FZSHSJ,
                                           FZSHYJ = viorec.FZSHYJ,
                                           CSBJ = viorec.CSBJ,
                                           CLR = viorec.CFR,
                                           CLSJ = viorec.CLSJ,
                                           CLDW = viorec.CLJG,
                                           WFXH = viorec.WFXH,
                                           JSR = viorec.JSR,
                                           JSSJ = viorec.JSSJ,
                                           JDJG = viorec.JDJG
                                       }).SingleOrDefault();

            return parkingCase;
        }

        /// <summary>
        /// 根据用户标识获取用户名称
        /// </summary>
        /// <param name="userID">用户标识</param>
        /// <returns>用户名称</returns>
        public static string GetUserNameByUserID(string userID)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            HT_USER_NEW user = db.HT_USER_NEW
                .Where(t => t.USERID == userID)
                .SingleOrDefault();

            return user != null ? user.IUSERNAME : "";
        }

        /// <summary>
        /// 根据序列号获取图片列表
        /// </summary>
        /// <param name="XLH">序列号</param>
        /// <returns>图片列表</returns>
        public static Picture GetPicutresByXLH(decimal XLH)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            string sql = "select * from ht_veh_picrec where xlh = {0}";

            sql = string.Format(sql, XLH);

            return db.Database.SqlQuery<Picture>(sql).First();
        }

        /// <summary>
        /// 根据数据状态标识获取数据状态名称
        /// </summary>
        /// <param name="csbj">数据状态标识</param>
        /// <returns>数据状态名称</returns>
        public static string GetCSBJNameByCSBJ(string csbj)
        {
            ///数据状态名称
            string CSBJName = "";

            if (csbj.Equals("0"))
            {
                CSBJName = "未上传";
            }
            else if (csbj.Equals("1"))
            {
                CSBJName = "已上传";
            }
            else if (csbj.Equals("2"))
            {
                CSBJName = "上传失败";
            }
            else if (csbj.Equals("3"))
            {
                CSBJName = "交警处理成功";
            }
            else if (csbj.Equals("4"))
            {
                CSBJName = "交警解锁成功";
            }
            else if (csbj.Equals("5"))
            {
                CSBJName = "交警处理失败";
            }

            return CSBJName;
        }

        /// <summary>
        /// 根据单位编号获取单位简称
        /// </summary>
        /// <param name="cldw">单位编号</param>
        /// <returns>单位简称</returns>
        public static string GetGOVNameByCLDW(string cldw)
        {
            TZ_ZFJEntities db = new TZ_ZFJEntities();

            HT_CD_GOV gov = db.HT_CD_GOV
                .Where(t => t.XZQH == cldw)
                .SingleOrDefault();

            return gov != null ? gov.JC : "";
        }

        /// <summary>
        /// 获得违停案件数据
        /// </summary>
        /// <returns></returns>
        public static int CaseParkCount()
        {
            TZ_ZFJEntities tz = new TZ_ZFJEntities();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return tz.HT_VEH_VIOREC.Where(t => t.SJSCSJ > dt).Count();
        }
    }
}
