//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HZW.ZHCG.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class store_bases
    {
        public int store_id { get; set; }
        public string id_type { get; set; }
        public string store_name { get; set; }
        public int type_id { get; set; }
        public string address { get; set; }
        public string grometry { get; set; }
        public string person { get; set; }
        public string businessperson { get; set; }
        public string businesscontact { get; set; }
        public string registnum { get; set; }
        public string registname { get; set; }
        public string registcontact { get; set; }
        public string businessscope { get; set; }
        public Nullable<System.DateTime> registdate { get; set; }
        public Nullable<System.DateTime> businessenddate { get; set; }
        public Nullable<int> businesslicense { get; set; }
        public Nullable<int> healthcard { get; set; }
        public string mqsbperson { get; set; }
        public string gridnum { get; set; }
        public string gridperson { get; set; }
        public string gridcontact { get; set; }
        public Nullable<System.DateTime> createtime { get; set; }
        public Nullable<int> createuserid { get; set; }
        public Nullable<int> status { get; set; }
        public string remark { get; set; }
        public string remark2 { get; set; }
    
        public virtual store_classes store_classes { get; set; }
    }
}