using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model;
using ZGM.BLL.PJKHBLLs;
using ZGM.Model.PhoneModel;
using ZGM.Model.ViewModels;
using ZGM.BLL.QWGLBLLs;
using ZGM.BLL.WORKFLOWManagerBLLs.ZFSJSourcesBLL;

namespace ZGM.PhoneAPI.Controllers.QWGL
{
    public class UserExamineController : ApiController
    {
        /// <summary>
        /// 获取被考核队员
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserExamineInfo> GetBeExamineUser(ExaminePostModel model)
        {
            List<UserExamineInfo> users = ExamineBLL.GetExaminedUser(model.UnitId,model.UserId);
            return users;
        }

        /// <summary>
        /// 获取队员考核详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserExamineInfo GetUserExamine(ExaminePostModel model)
        {
            UserExamineInfo userinfo = new UserExamineInfo();
            if (model.UserId !=0 && model.StartDate!=null && model.EndDate != null)
            {
                userinfo.EventReport = (decimal)ZFSJSOURCESBLL.GetReportEventList(model.UserId, model.StartDate.ToString(), model.EndDate.ToString()).PCount;
                userinfo.EventFinish = (decimal)ZFSJSOURCESBLL.GetReportEventList(model.UserId, model.StartDate.ToString(), model.EndDate.ToString()).PCCount;
                userinfo.EventPercent = userinfo.EventReport == 0 ? "0.0%" : ((double)userinfo.EventFinish / (double)userinfo.EventReport).ToString("0.0%");
                userinfo.Distance = ZFSJSOURCESBLL.GetPersonWalk(model.UserId, model.StartDate.ToString(), model.EndDate.ToString()).ToString();
                userinfo.SignIn = UserSignInBLL.GetSignInTJInfoList(model.UserId, model.StartDate.ToString(), model.EndDate.ToString(), 1);
                userinfo.UnSignIn = UserSignInBLL.GetSignInTJInfoList(model.UserId, model.StartDate.ToString(), model.EndDate.ToString(), 2);
                userinfo.SignInPercent = (userinfo.SignIn + userinfo.UnSignIn) == 0 ? "0.0%" : ((double)userinfo.SignIn / (double)(userinfo.SignIn + userinfo.UnSignIn)).ToString("0.0%");
                userinfo.OverTimeAlarm = AlarmBLL.GetAlarmInTimeList(model.UserId, model.StartDate.ToString(), model.EndDate.ToString(), 1);
                userinfo.OverBoardAlarm = AlarmBLL.GetAlarmInTimeList(model.UserId, model.StartDate.ToString(), model.EndDate.ToString(), 2);
            }

            return userinfo;
        }

        /// <summary>
        /// 管理员考核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserExamineInfo SaveUserExamine(ExaminePostModel model)
        {
            PJKH_EXAMINES examine = new PJKH_EXAMINES();
            UserExamineInfo user = new UserExamineInfo();

            examine.EXAMINETIME = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            examine.USERID = model.ExamineId;
            examine.JOB = model.JobScore;
            examine.SIGNIN = model.SignInScore;
            examine.ALARM = model.AlarmScore;
            examine.SCORE = model.Score;
            examine.CREATETIME = DateTime.Now;
            examine.CREATEUSER = model.UserId;
            
            int result = ExamineBLL.AddExamine(examine);

            if (result > 0)
                user.IsSuccessExamine = true;
            else
                user.IsSuccessExamine = false;

            return user;
        }

        /// <summary>
        /// 我的考核情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserExamineInfo> GetUserExamineList(ExaminePostModel model)
        {
            List<UserExamineInfo> list = ExamineBLL.GetUserExamineList(model.UserId,model.RoleId,model.PageIndex,model.QueryUserName);
            
            return list;
        }

        /// <summary>
        /// 部门考核情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserExamineInfo> GetTeamExamineList(ExaminePostModel model)
        {
            List<UserExamineInfo> list = ExamineBLL.GetTeamExamineList(model.UserId,model.UnitId, model.PageIndex, model.QueryUserName);
            
            return list;
        }

        /// <summary>
        /// 根据考核标识获取考核信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserExamineInfo GetExamineInfoByExamineId(ExaminePostModel GetData)
        {
            UserExamineInfo model = ExamineBLL.GetExamineInfoByExamineId(GetData.ExamineDataId);
            model.ExamineDate = ((DateTime)model.AllDate).ToLongDateString();
            model.CreateUserName = ZGM.BLL.UserBLLs.UserBLL.GetUserNameByUserID(model.CreateUser);
            return model;
        }

    }
}
