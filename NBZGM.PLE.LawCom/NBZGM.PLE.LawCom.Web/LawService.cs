using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.LawCom.Web.Complex;

namespace Taizhou.PLE.LawCom.Web
{
    public partial class NEWPLEDomainService
    {
        #region 车辆、队员和事件
        /// <summary>
        /// 获取全部执法车辆
        /// (为40部门下）
        /// </summary>
        /// <returns></returns>
        public IQueryable<Car> GetAllCars()
        {
            IQueryable<Car> result = from t in this.ObjectContext.ZFGKCARS
                                     from u in this.ObjectContext.UNITS
                                     join _t in this.ObjectContext.ZFGKCARLATESTPOSITIONS
                                     on t.CARID equals _t.CARID into tbl
                                     from l in tbl.DefaultIfEmpty()
                                     where t.UNITID == u.UNITID
                                     && u.PARENTID == 10000
                                     select new Car
                                     {
                                         ID = t.CARID,
                                         CarNumber = t.CARNO,
                                         UnitID = t.UNITID,
                                         UnitName = u.UNITNAME,
                                         X = (double)l.LON,
                                         Y = (double)l.LAT,
                                         PositionDateTime = l.POSITIONTIME
                                     };
            return result;
        }

        /// <summary>
        /// 获取最新的定位车辆
        /// </summary>
        /// <returns></returns>
        public IQueryable<Car> GetLatestCars()
        {

            IQueryable<Car> result = from t in this.ObjectContext.ZFGKCARS
                                     from u in this.ObjectContext.UNITS
                                     from l in this.ObjectContext.ZFGKCARLATESTPOSITIONS
                                     where t.CARID == l.CARID
                                     && t.UNITID == u.UNITID
                                     select new Car
                                     {
                                         ID = t.CARID,
                                         CarNumber = t.CARNO,
                                         UnitID = t.UNITID,
                                         UnitName = u.UNITNAME,
                                         X = (double)l.LON,
                                         Y = (double)l.LAT,
                                         PositionDateTime = l.POSITIONTIME
                                     };
            return result;
        }

        /// <summary>
        /// 通过车辆ID获取最新定位信息
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public IQueryable<Car> GetLatestCarByCarID(decimal CarID)
        {
            IQueryable<Car> car = from t in this.ObjectContext.ZFGKCARS
                                  from u in this.ObjectContext.UNITS
                                  from l in this.ObjectContext.ZFGKCARLATESTPOSITIONS
                                  where t.CARID == CarID &&
                                  t.UNITID == u.UNITID &&
                                  l.CARID == CarID
                                  select new Car
                                  {
                                      ID = t.CARID,
                                      CarNumber = t.CARNO,
                                      UnitID = t.UNITID,
                                      UnitName = u.UNITNAME,
                                      X = (double)l.LON,
                                      Y = (double)l.LAT,
                                      PositionDateTime = l.POSITIONTIME
                                  };
            return car;
        }

        #region 部门
        /// <summary>
        /// 获取全部单位
        /// </summary>
        /// <returns></returns>
        public IQueryable<Unit> GetAllUnits()
        {
            IQueryable<Unit> result = from t in this.ObjectContext.UNITS
                                      select new Unit
                                      {
                                          ID = t.UNITID,
                                          Name = t.UNITNAME,
                                          ParentID = t.PARENTID,
                                          AbbrName = t.ABBREVIATION,
                                          SeqNo = t.SEQNO,
                                          UnitTypeId = t.UNITTYPEID
                                      };
            return result;
        }

        public IQueryable<Unit> GetAllUnits4LawCars()
        {
            IQueryable<Unit> result = from c in this.ObjectContext.ZFGKCARS
                                      from u in this.ObjectContext.UNITS
                                      where c.UNITID == u.UNITID
                                      select new Unit
                                      {
                                          ID = u.UNITID,
                                          Name = u.UNITNAME,
                                          ParentID = u.PARENTID,
                                          AbbrName = u.ABBREVIATION,
                                          SeqNo = u.SEQNO,
                                          UnitTypeId = u.UNITTYPEID
                                      };
            return result.Distinct();
        }

        public IQueryable<Unit> GetAllUnits4EventLaws()
        {
            IQueryable<Unit> result = from e in this.ObjectContext.ZFSJSUMMARYINFORMATIONS
                                      from u in this.ObjectContext.UNITS
                                      where e.UNITID == u.UNITID
                                      select new Unit
                                      {
                                          ID = u.UNITID,
                                          Name = u.UNITNAME,
                                          ParentID = u.PARENTID,
                                          AbbrName = u.ABBREVIATION,
                                          SeqNo = u.SEQNO,
                                          UnitTypeId = u.UNITTYPEID
                                      };

            return result.Distinct();
        }
        #endregion


        /// <summary>
        /// 获取全部用户
        /// (为40部门下）
        /// </summary>
        /// <returns></returns>
        public IQueryable<Person> GetAllPersons()
        {
            IQueryable<Person> result = from t in this.ObjectContext.USERS
                                        from u in this.ObjectContext.UNITS
                                        join _t in this.ObjectContext.ZFGKUSERLATESTPOSITIONS
                                        on t.USERID equals _t.USERID into tbl
                                        from p in tbl.DefaultIfEmpty()
                                        where t.UNITID == u.UNITID
                                        && (u.PARENTID == 10000 || u.PARENTID == 1150)
                                        select new Person
                                        {
                                            UserID = t.USERID,
                                            UserName = t.USERNAME,
                                            UnitID = t.UNITID,
                                            UnitName = u.UNITNAME,
                                            SmsNumber = t.SMSNUMBERS,
                                            PositionTime = p.POSITIONTIME,
                                            Lon = (double)p.LON,
                                            Lat = (double)p.LAT
                                        };
            return result;
        }

        public IQueryable<Person> GetLatestPersons()
        {
            IQueryable<Person> result = from t in this.ObjectContext.USERS
                                        from u in this.ObjectContext.UNITS
                                        from l in this.ObjectContext.ZFGKUSERLATESTPOSITIONS
                                        where t.USERID == l.USERID
                                        && t.UNITID == u.UNITID
                                        select new Person
                                        {
                                            UserID = t.USERID,
                                            UserName = t.USERNAME,
                                            UnitID = u.UNITID,
                                            UnitName = u.UNITNAME,
                                            PositionTime = l.POSITIONTIME,
                                            Lon = (double)l.LON,
                                            Lat = (double)l.LAT
                                        };
            return result;
        }

        /// <summary>
        /// 根据用户ID获取最新定位信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Person GetLatestPersonByUserID(decimal UserID)
        {
            IQueryable<Person> person = from t in this.ObjectContext.USERS
                                        from u in this.ObjectContext.UNITS
                                        from l in this.ObjectContext.ZFGKUSERLATESTPOSITIONS
                                        where t.USERID == UserID &&
                                        t.UNITID == u.UNITID &&
                                        l.USERID == UserID
                                        select new Person
                                        {
                                            UserID = t.USERID,
                                            UserName = t.USERNAME,
                                            UnitID = u.UNITID,
                                            UnitName = u.UNITNAME,
                                            PositionTime = l.POSITIONTIME,
                                            Lon = (double)l.LON,
                                            Lat = (double)l.LAT
                                        };
            return person.SingleOrDefault();
        }

        /// <summary>
        /// 获取全部执法事件
        /// （使用部门名称关联，不安型）
        /// </summary>
        /// <returns></returns>
        public IQueryable<EventLaw> GetAllEventLaws()
        {
            IQueryable<EventLaw> result = from e in this.ObjectContext.ZFSJSUMMARYINFORMATIONS
                                          from u in this.ObjectContext.UNITS
                                          where e.UNITID == u.UNITID
                                          select new EventLaw()
                                          {
                                              EventID = e.WIID,
                                              EventTitle = e.EVENTTITLE,
                                              EventAddress = e.EVENTADDRESS,
                                              EventSource = e.EVENTSOURCE,
                                              UnitID = u.UNITID,
                                              SSDD = e.SSDD,
                                              SSZD = e.SSZD,
                                              Geometry = e.GEOMETRY,
                                              ReportTime = e.REPORTTIME,
                                              ReportPerson = e.REPORTPERSON
                                          };
            return result;
        }
        #endregion
        #region 执法案件和行政审批
        /// <summary>
        /// 获取全部执法案件
        /// </summary>
        /// <returns></returns>
        public IQueryable<WORKFLOWINSTANCE> GetAllZFAJ()
        {
            IQueryable<WORKFLOWINSTANCE> result = this.ObjectContext.WORKFLOWINSTANCES;
            return result;
        }

        public IQueryable<XZSPWFIST> GetAllXZSP()
        {
            IQueryable<XZSPWFIST> result = this.ObjectContext.XZSPWFISTS;
            return result;
        }
        #endregion
        #region 巡查区域和巡查路线
        /// <summary>
        /// 根据队员ID获取巡查区域
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public IQueryable<XCJGAREA> GetPatrolAreasByUserID(decimal UserID, DateTime Day)
        {
            IQueryable<XCJGAREA> results = (from u in this.ObjectContext.USERS
                                            from xa in this.ObjectContext.XCJGAREAS
                                            from xu in this.ObjectContext.XCJGUSERTASKS
                                            where u.USERID == UserID
                                            && u.USERID == xu.USERID
                                            && xu.AREAID == xa.AREAID
                                            && xu.TASKDATE == Day
                                            select xa);
            return results;
        }

        /// <summary>
        /// 根据队员ID获取巡查路线
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public IQueryable<XCJGROUTE> GetPatrolRoutesByUserID(decimal UserID, DateTime Day)
        {
            IQueryable<XCJGROUTE> results = (from u in this.ObjectContext.USERS
                                             from xr in this.ObjectContext.XCJGROUTES
                                             from xu in this.ObjectContext.XCJGUSERTASKS
                                             where u.USERID == UserID
                                             && u.USERID == xu.USERID
                                             && xu.ROUTEID == xr.ROUTEID
                                             && xu.TASKDATE == Day
                                             select xr);
            return results;
        }

        /// <summary>
        /// 根据车辆ID获取巡查区域
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public IQueryable<XCJGAREA> GetPatrolAreasByCarID(decimal CarID)
        {
            IQueryable<XCJGAREA> results = (from c in this.ObjectContext.ZFGKCARS
                                            from xa in this.ObjectContext.XCJGAREAS
                                            from xc in this.ObjectContext.XCJGCARTASKS
                                            where c.CARID == CarID
                                            && c.CARID == xc.CARID
                                            && xc.AREAID == xa.AREAID
                                            select xa);
            return results;
        }

        /// <summary>
        /// 根据车辆ID获取巡查路线
        /// </summary>
        /// <param name="CarID"></param>
        /// <returns></returns>
        public IQueryable<XCJGROUTE> GetPatrolRoutesByCarID(decimal CarID)
        {
            IQueryable<XCJGROUTE> results = (from c in this.ObjectContext.ZFGKCARS
                                             from xr in this.ObjectContext.XCJGROUTES
                                             from xc in this.ObjectContext.XCJGCARTASKS
                                             where c.CARID == CarID
                                             && c.CARID == xc.CARID
                                             && xc.ROUTEID == xr.ROUTEID
                                             select xr);
            return results;
        }
        #endregion

        #region 历史轨迹
        public IQueryable<ZFGKCARHISTORYPOSITION> GetCarHistoryPositionsByCarID(decimal CarID, DateTime startTime, DateTime endTime)
        {
            IQueryable<ZFGKCARHISTORYPOSITION> results = (from c in this.ObjectContext.ZFGKCARS
                                                          from ch in this.ObjectContext.ZFGKCARHISTORYPOSITIONS
                                                          where c.CARID == CarID
                                                          && c.CARID == ch.CARID
                                                          && ch.POSITIONTIME >= startTime
                                                          && ch.POSITIONTIME <= endTime
                                                          select ch);
            return results;
        }

        public IQueryable<ZFGKUSERHISTORYPOSITION> GetPersonHistoryPosotionByUserID(decimal UserID, DateTime startTime, DateTime endTime)
        {
            IQueryable<ZFGKUSERHISTORYPOSITION> results = (from u in this.ObjectContext.USERS
                                                           from uh in this.ObjectContext.ZFGKUSERHISTORYPOSITIONS
                                                           where u.USERID == UserID
                                                           && u.USERID == uh.USERID
                                                           && uh.POSITIONTIME >= startTime
                                                           && uh.POSITIONTIME <= endTime
                                                           orderby uh.POSITIONTIME
                                                           select uh);
            return results;
        }
        #endregion

    }
}