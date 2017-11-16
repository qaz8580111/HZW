using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.BLL.XTBGBLL;
using ZGM.Model.CustomModels;

namespace ZGM.Web.Controllers.XTBG
{
    public class ztreeController : ApiController
    {




        //递归获取用户通讯录
        public void Get(List<TreeModel> newmodel, TreeModel pmodel)
        {
            //用户通讯录列表
            List<Dictionary<string, string>> dic_AllUserList = new List<Dictionary<string, string>>();
            foreach (TreeModel c in newmodel)
            {
                if (c.children.Count == 0)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();

                    dic.Add("contactId", c.value);
                    dic.Add("contactName", c.name);
                    dic.Add("departId", c.pId);
                    dic.Add("departName", pmodel.title);
                    dic.Add("photourl", c.smsNumber);
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
        public string GetUsers(decimal userid)
        {
            try
            {
                //用户通讯录列表
                List<Dictionary<string, string>> dic_AllUserList = new List<Dictionary<string, string>>();
                List<TreeModel> list = OA_CONTACTSBLL.treeList(userid);
                foreach (TreeModel item in list)
                {
                    if (item.children.Count == 0)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("contactId", item.value);
                        dic.Add("headphoto", item.smsNumber);
                        dic.Add("departId", item.pId);
                        dic.Add("contactName", "");
                        dic.Add("departName", item.title);
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
