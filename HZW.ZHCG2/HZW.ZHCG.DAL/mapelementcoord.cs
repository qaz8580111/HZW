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
    
    public partial class mapelementcoord
    {
        public int mapelementcategoryid { get; set; }
        public int mapelementid { get; set; }
        public string shape1 { get; set; }
        public string shape2 { get; set; }
        public Nullable<System.DateTime> satellitetime { get; set; }
        public Nullable<decimal> longitude { get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> x { get; set; }
        public Nullable<decimal> y { get; set; }
        public Nullable<int> altitude { get; set; }
        public Nullable<int> speed { get; set; }
        public Nullable<int> direction { get; set; }
        public Nullable<int> accuracy { get; set; }
        public Nullable<int> satellites { get; set; }
        public Nullable<System.DateTime> reportedtime { get; set; }
        public Nullable<System.DateTime> createdtime { get; set; }
    }
}