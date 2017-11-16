using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class UserBLL
    {
        UserDAL userDAL = new UserDAL();
        #region 用户登入
        /// <summary>
        /// 用户登入验证
        /// 反馈信息为：登入成功；密码错误；用户名错误
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>反馈信息</returns>
        public string UserLogin(string account, string password)
        {
            account = this.CheckOutAccount(account);
            password = this.CheckOutPassword(password);
            string result = userDAL.UserLogin(account, password);
            return result;
        }

        /// <summary>
        /// 用户登入验证
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>反馈信息</returns>
        public UserModel UserLogin2(string account, string password)
        {
            account = this.CheckOutAccount(account);
            password = this.CheckOutPassword(password);
            UserModel result = userDAL.UserLogin2(account, password);
            return result;
        }

        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="password">用户输入的密码</param>
        /// <returns>校验后的密码</returns>
        private string CheckOutPassword(string password)
        {
            //可做加密
            string result = password == null ? "" : password.Trim();
            return result;
        }

        /// <summary>
        /// 校验账号
        /// </summary>
        /// <param name="account">用户输入的账号</param>
        /// <returns>校验后的账号</returns>
        private string CheckOutAccount(string account)
        {
            string result = account == null ? "" : account.Trim();
            return result;
        }
        #endregion
        #region 获取用户数据
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<UserModel> GetUsersByPage(string userName, decimal? unitId, decimal? skipNum, decimal? takeNum)
        {
            userName = this.CheckOutuserName(userName);
            List<UserModel> users = userDAL.GetUsersByPage(userName, unitId, skipNum, takeNum);
            return users;
        }

        /// <summary>
        /// 获取用户数量
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public int GetUsersCount(string userName, decimal? unitId)
        {
            userName = this.CheckOutuserName(userName);
            int count = userDAL.GetUsersCount(userName, unitId);
            return count;
        }

        /// <summary>
        /// 校验用户姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string CheckOutuserName(string userName)
        {
            string name = userName == null ? "" : userName.Trim();
            return name;
        }

        /// <summary>
        /// 根据用户标识获取用户信息
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public UserModel GetUserByUserId(decimal UserId)
        {
            UserModel user = userDAL.GetUserByUserId(UserId);
            return user;
        }

        /// <summary>
        /// 获取人员历史定位列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<UserPositionModel> GetUserPositions(decimal userId, DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Now.AddMinutes(-30);
            if (endTime == null)
                endTime = DateTime.Now;

            List<UserPositionModel> ups = userDAL.GetUserPositions(userId, startTime, endTime);
            return ups;
        }

        /// <summary>
        /// 获取在线人员数
        /// 15分钟
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public int GetOnlineCount(decimal? seconds)
        {
            if (seconds == null)
                seconds = 15 * 60;//15分钟
            int count = userDAL.GetOnlineCount(seconds);
            return count;
        }

        /// <summary>
        /// 获取部门在线统计数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitOnline(decimal? parentId, decimal? seconds)
        {
            if (parentId == null)
                parentId = 17;
            if (seconds == null)
                seconds = 15 * 60;
            List<R_UserUnitModel> result = userDAL.GetUnitOnline(parentId, seconds);
            return result;
        }

        /// <summary>
        /// 获取部门人员数量
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitAll(decimal? parentId)
        {
            if (parentId == null)
                parentId = 17;
            List<R_UserUnitModel> result = userDAL.GetUnitAll(parentId);
            return result;
        }

        /// <summary>
        /// 获取部门人员在线和总人数
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public List<R_UserUnitModel> GetUnitStat(decimal? parentId, decimal? seconds)
        {
            if (parentId == null)
                parentId = 17;
            if (seconds == null)
                seconds = 15 * 60;
            List<R_UserUnitModel> all = userDAL.GetUnitAll(parentId);
            List<R_UserUnitModel> online = userDAL.GetUnitOnline(parentId, seconds);
            List<R_UserUnitModel> result = new List<R_UserUnitModel>();
            foreach (R_UserUnitModel item in all)
            {
                R_UserUnitModel r = online.Where(t => t.UnitId == item.UnitId).SingleOrDefault();
                r.All = item.All;
                result.Add(r);
            }
            return result.ToList();
        }

        /// <summary>
        /// 获取今日人员在线、离线、报警分段统计
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_StatModel> GetUserStatSub(decimal? parentId, DateTime? startTime, DateTime? endTime)
        {
            if (parentId == null)
                parentId = 17;
            if (startTime == null)
                startTime = DateTime.Today.AddHours(7);
            if (endTime == null)
                endTime = DateTime.Today.AddHours(20);
            List<R_StatModel> result = userDAL.GetUserStatSub(parentId, startTime, endTime);
            return result;
        }

        /// <summary>
        /// 根据用户标识获取报警信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserAlarmModel> GetAlarmsByPage(decimal? userId, decimal? skipNum, decimal? takeNum)
        {
            IQueryable<UserAlarmModel> result = userDAL.GetAlarmsByPage(userId, skipNum, takeNum);
            return result.ToList();
        }

        /// <summary>
        /// 获取报警数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetAlarmsCount(decimal? userId)
        {
            int count = userDAL.GetAlarmsCount(userId);
            return count;
        }

        /// <summary>
        /// 获取指定时间之间的报警
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<UserAlarmModel> GetAlarmsByUserId(decimal? userId, DateTime? startTime, DateTime? endTime)
        {
            IQueryable<UserAlarmModel> result = userDAL.GetAlarmsByUserId(userId, startTime, endTime);
            return result.ToList();
        }
        #endregion
    }
}
