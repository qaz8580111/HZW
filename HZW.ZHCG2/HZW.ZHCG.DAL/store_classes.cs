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
    
    public partial class store_classes
    {
        public store_classes()
        {
            this.store_bases = new HashSet<store_bases>();
        }
    
        public int type_id { get; set; }
        public string type_name { get; set; }
        public Nullable<int> seqno { get; set; }
        public string firstletter { get; set; }
        public string icon { get; set; }
    
        public virtual ICollection<store_bases> store_bases { get; set; }
    }
}
