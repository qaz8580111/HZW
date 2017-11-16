using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model.PhoneModel;

namespace ZGM.PhoneAPI.Controllers.Contact
{
    public class ContactsController : ApiController
    {
        //用户通讯录列表
        List<Dictionary<string, string>> dic_AllUserList = new List<Dictionary<string, string>>();

        //递归获取用户通讯录
        public void Get(List<TreeModel> newmodel, TreeModel pmodel)
        {
            foreach (TreeModel c in newmodel)
            {
                if (c.children.Count == 0)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                   
                    dic.Add("mobile", c.phone);                  
                    dic.Add("contactId", c.value);
                    dic.Add("contactName", c.name);
                    dic.Add("departId", c.pId);
                    dic.Add("departName", pmodel.title);
                    dic.Add("sex", c.sex);
                    dic.Add("zfzbh",c.zfzbh);
                    dic.Add("birthday",c.birthday);
                    dic_AllUserList.Add(dic);
                }
                else
                {
                    Get(c.children, c);
                }
            }
        }

        [HttpGet]
        /// <summary>
        /// 获取用户通讯录
        /// </summary> 
        /// <returns></returns>
        public string GetUsers(decimal unitid)
        {
            try
            {

                List<TreeModel> list = BLL.ContactBLLs.ContactBLL.GetTreeNodes(unitid);
                foreach (TreeModel item in list)
                {
                    if (item.children.Count == 0)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("mobile", item.phone);
                        dic.Add("contactId", item.value);
                        dic.Add("departId", item.pId);
                        dic.Add("contactName", "");
                        dic.Add("departName", item.title);
                        dic.Add("sex", item.sex);
                        dic.Add("zfzbh",item.zfzbh);
                        dic.Add("birthday", item.birthday);
                        dic_AllUserList.Add(dic);
                    }
                    else
                    {
                        Get(list, item);
                    }
                }
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(dic_AllUserList);
                return "{\"resData\":" + json + ",\"resCode\":\"1\"}";
            }
            catch
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }

    }
}
