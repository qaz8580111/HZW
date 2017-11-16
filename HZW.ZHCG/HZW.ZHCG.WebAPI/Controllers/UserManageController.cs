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
    public class UserManageController : ApiController
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
            string result = bll.Login(user.Account, user.PassWord);
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
        public List<UserPosition> GetUserTypes()
        {
            return bll.GetUserPositions();
        }
    }
}