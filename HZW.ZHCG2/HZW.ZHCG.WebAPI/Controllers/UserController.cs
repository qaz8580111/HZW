using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HZW.ZHCG.WebAPI.Attributes;
using HZW.ZHCG.Model;
using HZW.ZHCG.BLL;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http.Headers;

namespace HZW.ZHCG.WebAPI.Controllers
{
    [LoggingFilter]
    public class UserController : ApiController
    {
        private UserBLL bll = new UserBLL();

        public List<User> GetUser(int UnitID)
        {
            return bll.GetUser(UnitID);
        }

        [HttpGet]
        public Paging<List<User>> GetUsers(int start, int limit)
        {
            return bll.GetUsers(null, start, limit);
        }

        [HttpGet]
        public Paging<List<User>> GetUsers(string filter, int start, int limit)
        {
            List<Filter> filters = JsonConvert.DeserializeObject<List<Filter>>(filter);
            return bll.GetUsers(filters, start, limit);
        }

        [HttpPost]
        public HttpResponseMessage Login(User user)
        {
            string result = bll.Login(user.LoginName, user.LoginPwd);
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(result)
            };

            return response;
        }

        [HttpPost]
        public HttpResponseMessage ChangePassword(User user)
        {
            int result = bll.ChangePassword(user);
            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(result.ToString())
            };

            return response;
        }

        [HttpPost]
        public HttpResponseMessage LoginTest(User user)
        {
            var resp = new HttpResponseMessage();

            var nv = new NameValueCollection();
            nv["USER_ID"] = "1";
            nv["USER_NAME"] = "管理员";

            var cookie = new CookieHeaderValue("session-id", nv);
            //cookie.Expires = DateTimeOffset.Now.AddDays(1);
            //cookie.Domain = Request.RequestUri.Host;
            //cookie.Path = "/";

            resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return resp;
        }

        [HttpPost]
        public HttpResponseMessage AddUser(User user)
        {
            int result = bll.AddUser(user);

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(result.ToString())
            };
            return response;
        }

        [HttpPost]
        public HttpResponseMessage EditUser(User user)
        {
            int result = bll.EditUser(user);

            HttpResponseMessage response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(result.ToString())
            };
            return response;
        }

        [HttpPost]
        public HttpResponseMessage DeleteUser(int id)
        {
            bll.DeleteUser(id);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public List<UserType> GetUserTypes()
        {
            return bll.GetUserTypes();
        }



        /// <summary>
        /// 展示平台获取人员列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<UserModel>> GetUsersList(int start, int limit)
        {
            return bll.GetUsersList("", start, limit);
        }

        /// <summary>
        /// 展示平台获取人员列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public Paging<List<UserModel>> GetUsersList(string name, int start, int limit)
        {
            return bll.GetUsersList(name, start, limit);
        }

        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public UserModel GetUserModelByID(int id)
        {
            return bll.GetUserModelByID(id);
        }

        /// <summary>
        /// 获取列表的总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetEventListCount(int limit)
        {
            return bll.GetUsersListCount("", limit);
        }

        /// <summary>
        /// 获取查询后列表的总条数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public int GetEventListCount(string name, int limit)
        {
            return bll.GetUsersListCount(name, limit);
        }


        /// <summary>
        /// 返回同步人员信息
        /// </summary>
        /// <returns></returns>
        public List<usertableType> getall()
        {
            return bll.getall();
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<rytable> getalluser(string filter, int pageIndex, int pageSize)
        {
            return bll.getalluser(filter, pageIndex, pageSize);
        }

        /// 获取人员列表页码
        /// </summary>
        /// <returns></returns>
        public int getalluser(string filter, int pageSize)
        {
            return bll.getalluser(filter, pageSize);
        }

        /// <summary>
        /// 人员详情
        /// </summary>
        /// <param name="userid">人员Id</param>
        /// <returns></returns>
        public rytable getUserdetalis(int userid)
        {
            return bll.getUserdetalis(userid);
        }

        /// <summary>
        /// 今日在岗人数
        /// </summary>
        /// <returns></returns>
        
        public int GetCountUserNowDate()
        {
            return bll.GetCountUserNowDate();
        }
    }
}