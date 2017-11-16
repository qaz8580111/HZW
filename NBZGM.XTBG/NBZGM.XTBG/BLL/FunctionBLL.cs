using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NBZGM.XTBG.Models;

namespace NBZGM.XTBG.BLL
{
    public class FunctionBLL
    {
        public static IQueryable<SYS_FUNCTIONS> GetList()
        {
            NBZGMEntities db = new NBZGMEntities();
            return db.SYS_FUNCTIONS;
        }
        public static IQueryable<SYS_FUNCTIONS> GetFunctionsByUserID(decimal UserID)
        {
            NBZGMEntities db = new NBZGMEntities();
            IQueryable<SYS_FUNCTIONS> list = from ur in db.SYS_USERROLES
                                             from rf in db.SYS_ROLEFUNCTIONS
                                             from f in db.SYS_FUNCTIONS
                                             where ur.ROLEID == rf.ROLEID
                                             && rf.FUNCTIONID == f.FUNCTIONID
                                             && ur.USERID == UserID
                                             orderby f.SEQNUM
                                             select f;
            return list;
        }
    }
}