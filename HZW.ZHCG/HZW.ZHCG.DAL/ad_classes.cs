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
    
    public partial class ad_classes
    {
        public ad_classes()
        {
            this.ad_outadverts = new HashSet<ad_outadverts>();
        }
    
        public int type_id { get; set; }
        public string type_name { get; set; }
        public Nullable<int> seqno { get; set; }
    
        public virtual ICollection<ad_outadverts> ad_outadverts { get; set; }
    }
}
