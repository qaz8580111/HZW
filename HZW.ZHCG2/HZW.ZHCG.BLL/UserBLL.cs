using HZW.ZHCG.DAL;
using HZW.ZHCG.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HZW.ZHCG.BLL
{
    public class UserBLL
    {
        private UserDAL dal = new UserDAL();
        private UserTypeDAL utDal = new UserTypeDAL();

        public List<User> GetUser(int UnitID)
        {
            return dal.GetUsersByUnitID(UnitID);
        }

        /// <summary>
        /// 查询用户分页列表
        /// </summary>
        public Paging<List<User>> GetUsers(List<Filter> filters, int start, int limit)
        {
            List<User> items = dal.GetUsers(filters, start, limit);
            int total = dal.GetUserCount(filters);

            Paging<List<User>> paging = new Paging<List<User>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns>1.登陆成功 2.帐号不存在 3.帐号或密码输入错误</returns>
        public string Login(string loginName, string loginPwd)
        {
            List<User> list = dal.GetUsersByLoginName(loginName);

            if (list.Count == 0)
                return "2";

            User user = list.SingleOrDefault(t => t.LoginPwd == loginPwd);

            if (user == null)
                return "3";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["USER_ID"] = user.ID;
            dic["USER_NAME"] = user.DisplayName;
            dic["UNIT_ID"] = user.UnitID;


            string json = JsonConvert.SerializeObject(dic); //"{\"USER_ID\":" + user.ID + ",\"USER_NAME\":\"" + user.DisplayName + "\"}";
            return json;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>1.修改成功；2.密码不正确</returns>
        public int ChangePassword(User user)
        {
            User model = dal.GetUserByID(user.ID);

            if (user.LoginPwd != model.LoginPwd)
                return 2;

            user.LoginPwd = user.NewLoginPwd;
            user.UpdatedTime = DateTime.Now;

            dal.EditUserLoginPwd(user);

            return 1;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns>1 添加成功 2用户名已存在</returns>
        public int AddUser(User user)
        {
            List<User> list = dal.GetUsersByLoginName(user.LoginName);

            if (list.Count > 0)
                return 2;

            user.CreatedTime = user.UpdatedTime = DateTime.Now;
            dal.AddUser(user);

            return 1;
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns>1 添加成功 2用户名已存在</returns>
        public int EditUser(User user)
        {
            List<User> list = dal.GetUsersByLoginName(user.LoginName);

            if (list.Count > 0 && list.Where(t => t.ID == user.ID).SingleOrDefault() == null)
                return 2;

            user.UpdatedTime = DateTime.Now;
            dal.EditUser(user);

            return 1;
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        public void DeleteUser(int id)
        {
            dal.DeleteUser(id);
        }

        /// <summary>
        /// 获得用户类型列表
        /// </summary>
        public List<UserType> GetUserTypes()
        {
            return utDal.GetUserTypes();
        }

        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel GetUserModelByID(int id)
        {
            return dal.GetUserModelByID(id);
        }

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetUsersListCount(string name, int limit)
        {
            return dal.GetUsersListCount(name, limit);
        }
        /// <summary>
        /// 查询用户分页列表
        /// </summary>
        public Paging<List<UserModel>> GetUsersList(string name, int start, int limit)
        {
            List<UserModel> items = dal.GetUsersList(name, start, limit);
            int total = dal.GetUsersListCount(name);

            Paging<List<UserModel>> paging = new Paging<List<UserModel>>();
            paging.Items = items;
            paging.Total = total;

            return paging;
        }


        /// <summary>
        /// 返回同步人员信息
        /// </summary>
        /// <returns></returns>
        public List<usertableType> getall()
        {
            return dal.getall();
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<rytable> getalluser(string filter, int pageIndex, int pageSize)
        {
            return dal.getalluser(filter, pageIndex, pageSize);
        }

        /// 获取人员列表页码
        /// </summary>
        /// <returns></returns>
        public int getalluser(string filter, int pageSize)
        {
            return dal.getalluser(filter, pageSize);
        }

        /// <summary>
        /// 人员详情
        /// </summary>
        /// <param name="userid">人员Id</param>
        /// <returns></returns>
        public rytable getUserdetalis(int userid)
        {
            return dal.getUserdetalis(userid);
        }

        /// <summary>
        /// 今日在岗人数
        /// </summary>
        /// <returns></returns>
        public int GetCountUserNowDate()
        {
            return dal.GetCountUserNowDate();
        }

    }
}
