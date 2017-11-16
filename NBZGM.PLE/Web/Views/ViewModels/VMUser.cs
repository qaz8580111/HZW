using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModels
{
    public class VMUser
    {
        public decimal UserID { get; set; }
        public string UserName { get; set; }
        public string Account { get; set; }
        public string RTXAccount { get; set; }
        public string SMSNumbers { get; set; }
        public string Password { get; set; }
        public decimal? UserPositionID { get; set; }
        public decimal? UserCategoryID { get; set; }
        public decimal? SeqNo { get; set; }
        public decimal? UnitID { get; set; }
        public string ZFZBH { get; set; }
        public string WorkZZ { get; set; }

    }
}