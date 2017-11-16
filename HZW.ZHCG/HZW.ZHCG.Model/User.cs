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
        public string UserName { get; set; }
        public int UnitID { get; set; }
        public string UnitName { get; set; }
        public int? UserPositionID { get; set; }
        public string UserPositionName { get; set; }
        public string Account { get; set; }
        public string PassWord { get; set; }
        public string NewPwd { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? SeqNO { get; set; }
        public string Avatar { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int[] RoleIDArr { get; set; }
    }
}
