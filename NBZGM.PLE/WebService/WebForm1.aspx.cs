using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.WebServiceModels;
using Taizhou.PLE.Model;

namespace WebService
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PhoneWebService p = new PhoneWebService();
            decimal count = p.AddRcdcEvent("英语u", 27311, 27319, 83, 1, "120.11909323|30.29386743", "不能看看", "地方干活", "", "", "", "22a6459e-4cf5-4ae9-8e86-c6c1161fb82a");
            // p.SaveImg("11");
            //PLEEntities db = new PLEEntities();
            //int uploadwidth = 0;
            //int uploadheight = 0;
            //byte[] img = db.SIMPLECASEPICTURES.First().PICTURE;
            //string str = WebServiceUtility.FileUpload(img, "jpg", "123123", ref uploadwidth, ref uploadheight);
            //new PhoneWebService().QueryPendingEvents(83,null);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            PhoneWebService phoneweb = new PhoneWebService();
            //    string phoneworkflow = "{\"UserID\":\"88\",\"AY\":\"测试8.15_2\"}";
            //    string phonedocstr = "[{\"DocStr\":\"[{\\\"PSDD\\\":\\\"测试5\\\"},{\\\"PSDD\\\":\\\"测试6\\\"}]\",\"TypeID\":\"2\"}]";
            //    bool res = phoneweb.SavePhoneCaseWorkflow(phoneworkflow, phonedocstr);

            //    if (res)
            //    {
            //        Response.Write("<script>alert('成功')</script>");
            //    }

            //    else
            //    {
            //        Response.Write("<script>alert('失败')</script>");
            //    }

            XZSPThere xzsp = new XZSPThere();
            xzsp.ADID = 1;
            xzsp.AIID = "";
            xzsp.DYYJ = "";
            xzsp.UserID = "83";
            xzsp.PhotoList = new List<string>();
            xzsp.PhotoList.Add("qqq333455rrrtty");
            xzsp.PhotoList.Add("4455rrttyyuuuiig");
            xzsp.PhotoList.Add("qqq224445dder");
            xzsp.ZFZDName = "直属一大队三中队";
            string ss = JsonHelper.JsonSerializer<XZSPThere>(xzsp);

        }

        /// <summary>
        /// 测试一般案件接口
        /// </summary>
        public void GetStr()
        {
            PLEEntities db = new PLEEntities();
            byte[] img = db.SIMPLECASEPICTURES.First().PICTURE;
            MemoryStream ms = new System.IO.MemoryStream(img);
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);

            im.Save("D://新建文件夹//xxx.jpg");
        }

        public void GetStr(string img, string fliepath, string fliename, string type)
        {
            MemoryStream ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(img));
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            im.Save(fliepath + fliename + "." + type);
        }


        public void GetStr(byte[] img, string fliepath, string fliename, string type)
        {
            MemoryStream ms = new System.IO.MemoryStream(img);
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            im.Save(fliepath + fliename + "." + type);
        }
    }
}