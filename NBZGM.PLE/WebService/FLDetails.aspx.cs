using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebService
{
    public partial class FLDetails : System.Web.UI.Page
    {
        public string wz { get; set; }
        public string fz { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            wz = this.Request["wz"];
            fz = this.Request["fz"];
        }
    }
}