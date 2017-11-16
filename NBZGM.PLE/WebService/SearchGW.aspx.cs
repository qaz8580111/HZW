using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebService
{
    /// <summary>
    /// 查询公文信息
    /// </summary>
    public partial class SearchGW : System.Web.UI.Page
    {
        public string account { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.account = this.Request["account"];
        }
    }
}