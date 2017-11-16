using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZGM.WUA.Web
{
    public partial class Index : System.Web.UI.Page
    {
        public string param = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //string param = Request.QueryString["param"].ToString();
            //string key = Request.QueryString["skey"].ToString();
            param = "141";
            string key = "0F28B5D49B3020AFEECD95B4009ADF4C".ToUpper();
            string MD5Str = Twi.COMMON.Core.EncryptionAlgorithm.GetMD5(param).ToUpper();


            if (key != MD5Str)
            {
                Response.Redirect("Error.html", false); 
            }
            //param = MD5Decrypt(param, key);
        }

        
    }
}