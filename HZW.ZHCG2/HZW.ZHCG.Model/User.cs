using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public int? UnitID { get; set; }
        public string UnitName { get; set; }
        public int? UserTypeID { get; set; }
        public string UserTypeName { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string NewLoginPwd { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int[] RoleIDArr { get; set; }
    }
}
