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
    
    public partial class base_users
    {
        public int id { get; set; }
        public string code { get; set; }
        public string displayname { get; set; }
        public Nullable<int> unitid { get; set; }
        public Nullable<int> usertypeid { get; set; }
        public string loginname { get; set; }
        public string loginpwd { get; set; }
        public string avatar { get; set; }
        public Nullable<int> regionid { get; set; }
        public Nullable<int> mapelementbiztype { get; set; }
        public Nullable<int> mapelementdevicetype { get; set; }
        public string sex { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string address { get; set; }
        public string mobile { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public Nullable<int> isonline { get; set; }
        public Nullable<System.DateTime> createdtime { get; set; }
        public Nullable<System.DateTime> updatedtime { get; set; }
    }
}
