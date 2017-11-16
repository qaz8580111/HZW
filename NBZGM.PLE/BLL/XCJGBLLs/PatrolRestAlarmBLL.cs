using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.XCJGBLLs
{
    public class PatrolRestAlarmBLL
    {
        public void Add(XCJGRESTALARM model)
        {
            PLEEntities db = new PLEEntities();
            db.XCJGRESTALARMS.Add(model);
            db.SaveChanges();
        }

        public void Update(string RALARMID, decimal ISVALID)
        {
            PLEEntities db = new PLEEntities();
            XCJGRESTALARM result = db.XCJGRESTALARMS.SingleOrDefault(a => a.RALARMID == RALARMID);
            if (result != null)
            {
                result.ISVALID = ISVALID;
                result.DEALTIME = DateTime.Now;
                db.SaveChanges();
            }
        }

        public void Delete(string RALARMID)
        {
            PLEEntities db = new PLEEntities();
            XCJGRESTALARM result = db.XCJGRESTALARMS.SingleOrDefault(a => a.RALARMID == RALARMID);
            if (result != null)
            {
                db.XCJGRESTALARMS.Remove(result);
                db.SaveChanges();
            }
        }

        public XCJGRESTALARM GetSingle(string RALARMID)
        {
            PLEEntities db = new PLEEntities();
            XCJGRESTALARM result = db.XCJGRESTALARMS.SingleOrDefault(a => a.RALARMID == RALARMID);
            return result;
        }

        public IQueryable<XCJGRESTALARM> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<XCJGRESTALARM> result = db.XCJGRESTALARMS;
            return result;
        }

        public IQueryable<RestAlarmViewModel> GetListRestAlarmV()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<RestAlarmViewModel> result = from ra in db.XCJGRESTALARMS
                                                    from u in db.USERS
                                                    where ra.USERID == u.USERID
                                                    select new RestAlarmViewModel
                                                    {
                                                        RALARMID = ra.RALARMID,
                                                        USERID = ra.USERID,
                                                        ALARMTIME = ra.ALARMTIME,
                                                        LONLAT = ra.LONLAT,
                                                        ALARMTYPE = ra.ALARMTYPE,
                                                        ISVALID = ra.ISVALID,
                                                        DEALTIME = ra.DEALTIME,
                                                        UserName = u.USERNAME,
                                                    };
            return result;

        }
    }
}
