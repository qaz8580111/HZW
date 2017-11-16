using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class UserController : ApiController
    {
        UserBLL userBLL = new UserBLL();
        /// <summary>
        /// /api/User/UserLogin?account=&password=
        /// 登入验证
        /// 反馈信息为：登入成功；密码错误；用户名错误
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public string UserLogin(string account, string password)
        {
            string message = userBLL.UserLogin(account, password);
            return message;
        }

        /// <summary>
        /// /api/User/UserLogin?account=&password=
        /// 登入验证
        /// 反馈信息为：登入成功；密码错误；用户名错误
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public UserModel UserLogin2(string account, string password)
        {
            UserModel User = userBLL.UserLogin2(account, password);
            return User;
        }

        /// <summary>
        /// /api/User/GetUsersByPage?userName=&unitId=&takeNum=&skipNum=
        /// 分页获取人员列表
        /// 参数可选
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="takeNum"></param>
        /// <param name="skipNum"></param>
        /// <returns></returns>
        //[System.Web.Http.HttpGet]
        public List<UserModel> GetUsersByPage(string userName, decimal? unitId, decimal? skipNum, decimal? takeNum)
        {
            List<UserModel> users = userBLL.GetUsersByPage(userName, unitId, skipNum, takeNum);
            return users;
        }

        /// <summary>
        /// /api/User/GetUsersCount?userName=&unitId=
        /// 获取用户数量
        /// 参数可选
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUsersCount(string userName, decimal? unitId)
        {
            int count = userBLL.GetUsersCount(userName, unitId);
            return count;
        }

        /// <summary>
        /// /api/User/GetUserByUserId?userId=
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserModel GetUserByUserId(decimal userId)
        {
            UserModel user = userBLL.GetUserByUserId(userId);
            return user;
        }

        /// <summary>
        /// /api/User/GetUserPositions?userId=&startTime=&endTime=
        /// 获取人员历史定位列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<UserPositionModel> GetUserPositions(decimal userId, DateTime? startTime, DateTime? endTime)
        {
            List<UserPositionModel> ups = userBLL.GetUserPositions(userId, startTime, endTime);
            return ups;
        }

        /// <summary>
        /// /api/User/GetOnlineCount?seconds=
        /// 获取在线人员数
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public int GetOnlineCount(decimal? seconds)
        {
            int count = userBLL.GetOnlineCount(seconds);
            return count;
        }

        /// <summary>
        /// /api/User/GetUnitOnline?parentId=&seconds=
        /// 获取部门在线统计
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitOnline(decimal? parentId, decimal? seconds)
        {
            List<R_UserUnitModel> result = userBLL.GetUnitOnline(parentId, seconds);
            return result;
        }

        /// <summary>
        /// /api/User/GetUnitAll?parentId=
        /// 获取部门人员数量
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitAll(decimal? parentId)
        {
            List<R_UserUnitModel> result = userBLL.GetUnitAll(parentId);
            return result;
        }
        /// <summary>
        /// /api/User/GetUnitStat?parentId=&seconds=
        /// 获取部门人员在线和总人数
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitStat(decimal? parentId, decimal? seconds)
        {
            List<R_UserUnitModel> result = userBLL.GetUnitStat(parentId, seconds);
            return result;
        }

        /// <summary>
        /// /api/User/GetUserStatSub?parentId=&startTime=&endTime=
        /// 获取人员在线、离线、报警分段统计数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_StatModel> GetUserStatSub(decimal? parentId, DateTime? startTime, DateTime? endTime)
        {
            List<R_StatModel> result = userBLL.GetUserStatSub(parentId, startTime, endTime);
            return result;
        }

        /// <summary>
        /// /api/User/GetAlarms?userId=&skipNum=&takeNum=
        /// 根据用户标识获取报警信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserAlarmModel> GetAlarmsByPage(decimal? userId, decimal? skipNum, decimal? takeNum)
        {
            List<UserAlarmModel> result = userBLL.GetAlarmsByPage(userId, skipNum, takeNum);
            return result;
        }

        /// <summary>
        /// /api/User/GetAlarmsCount?userId=
        /// 获取报警数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetAlarmsCount(decimal? userId)
        {
            int count = userBLL.GetAlarmsCount(userId);
            return count;
        }

        /// <summary>
        /// /api/User/GetAlarmsByUserId?userId=&startTime=&endTime=
        /// 获取指定时间之间的报警
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<UserAlarmModel> GetAlarmsByUserId(decimal? userId, DateTime? startTime, DateTime? endTime)
        {
            List<UserAlarmModel> result = userBLL.GetAlarmsByUserId(userId, startTime, endTime);
            return result;
        }

    }
}
