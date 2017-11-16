using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class UserModel
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
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public string Avatar { get; set; }
        public int? Regionid { get; set; }
        public string RegionidName { get; set; }
        public int? MapElementBizType { get; set; }
        public int? MapElementDeviceType { get; set; }
        public string Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int? IsOnline { get; set; }

    }
}
