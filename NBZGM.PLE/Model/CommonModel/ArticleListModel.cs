using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.Model.CommonModel
{
    public class ArticleListModel
    {
        public decimal articleID { get; set; }
        public string title { get; set; }
        public string createDuserName { get; set; }
        public string author { get; set; }
        public Nullable<System.DateTime> createdTime { get; set; }
        public Nullable<decimal> isDelete { get; set; }
        public Nullable<decimal> approvalStatusID { get; set; }
        public string approvalUserName { get; set; }
        public Nullable<System.DateTime> approvalTime { get; set; }
        public string category { get; set; }
    }
}
