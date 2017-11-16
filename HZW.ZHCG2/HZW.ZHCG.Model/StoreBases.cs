using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.Model
{
    public class  StoreBases
    {
        public int storeid { get; set; }
        public string idtype { get; set; }
        public string storename { get; set; }
        /// <summary>
        /// 店家类型
        /// </summary>
        public int typeid { get; set; }
        public string address { get; set; }
        public string grometry { get; set; }
        public string person { get; set; }
        public string businessperson { get; set; }
        public string businesscontact { get; set; }
        public string registnum { get; set; }
        public string registname { get; set; }
        public string registcontact { get; set; }
        public string businessscope { get; set; }
        public DateTime? registdate { get; set; }
        public DateTime? businessenddate { get; set; }
       /// <summary>
       /// 营业照(有,无)
       /// </summary>
        public Nullable<int> businesslicense { get; set; }
        /// <summary>
        /// 卫生证（有无)
        /// </summary>
        public Nullable<int> healthcard { get; set; }

        public string mqsbperson { get; set; }
        public string gridnum { get; set; }
        public string gridperson { get; set; }
        public string gridcontact { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public Nullable<int> status { get; set; }

        public string typename { get; set; }
        public int? createuserid { get; set; }

        public string remark { get; set; }
        public string remark2 { get; set; }
    }
}
