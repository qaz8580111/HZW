using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model.PhoneModel;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model;

namespace ZGM.PhoneAPI.Controllers.QWGL
{
    public class UserLeaveController : ApiController
    {
        /// <summary>
        /// 加载用户请假页面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetUserLeaveType(UserLeavePostModel GetData)
        {
            return UserLeaveBLL.GetUserLeaveType();
        }

        /// <summary>
        /// 获取该部门的审批人
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLoginModel> GetUnitExaminer(UserLeavePostModel GetData)
        {
            List<UserLoginModel> list = UserLeaveBLL.GetUnitExaminer(GetData.UnitId);
            return list;
        }

        /// <summary>
        /// 新增用户请假
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserLeaveModel AddUserLeave(UserLeavePostModel GetData)
        {
            QWGL_LEAVES model = new QWGL_LEAVES();
            QWGL_LEAVEEXAMINES lemodel = new QWGL_LEAVEEXAMINES();
            UserLeaveModel ulmodel = new UserLeaveModel();
            model.LEAVEID = UserLeaveBLL.GetNewLEAVEID();
            model.LTYPEID = GetData.LeaveType;
            model.USERID = GetData.UserId;
            model.SDATE = GetData.SDate;
            model.EDATE = GetData.EDate;
            model.LEAVEDAY = GetData.LeaveDay;
            model.LEAVEREASON = GetData.LeaveReason == null ? "" : GetData.LeaveReason;
            model.CREATETIME = DateTime.Now;
            //插入请假数据
            int addresult = UserLeaveBLL.AddUserLeave(model);
            //插入请假审核人数据
            if (addresult > 0 && !string.IsNullOrEmpty(GetData.Examiner))
            {
                if (!GetData.Examiner.Contains(','))
                {
                    lemodel.LEAVEID = model.LEAVEID;
                    lemodel.EXUSERID = decimal.Parse(GetData.Examiner);
                    lemodel.EXRESULT = 0;
                    //插入请假审核数据
                    int addleresult = UserLeaveBLL.AddUserLeaveExaminer(lemodel);
                    if(addleresult >0)
                        ulmodel.IsLeaveSuccess = true;
                }
                else
                {
                    string[] examiners = GetData.Examiner.Split(',');
                    for (int i = 0; i < examiners.Length; i++)
                    {
                        lemodel.LEAVEID = model.LEAVEID;
                        lemodel.EXUSERID = decimal.Parse(examiners[i]);
                        lemodel.EXRESULT = 0;
                        //插入请假审核数据
                        int addleresult = UserLeaveBLL.AddUserLeaveExaminer(lemodel);
                        if (addleresult > 0)
                            ulmodel.IsLeaveSuccess = true;
                    }
                }
            }
            else
            {
                ulmodel.IsLeaveSuccess = false;
            }

            return ulmodel;
        }

        /// <summary>
        /// 获取团队今日请假情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetTeamLeave(UserLeavePostModel GetData)
        {
            List<UserLeaveModel> list = UserLeaveBLL.GetTeamLeaveList(GetData.UnitId,GetData.PageIndex,GetData.QueryUserName);

            return list;
        }

        /// <summary>
        /// 获取他人未审核请假情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetOtherUnLeave(UserLeavePostModel GetData)
        {
            List<UserLeaveModel> list = UserLeaveBLL.GetOtherUnLeaveList(GetData.UserId, GetData.UnitId, GetData.PageIndex, GetData.QueryUserName);

            return list;
        }

        /// <summary>
        /// 获取他人已审核请假情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetOtherAlLeave(UserLeavePostModel GetData)
        {
            List<UserLeaveModel> list = UserLeaveBLL.GetOtherAlLeaveList(GetData.UserId, GetData.UnitId, GetData.PageIndex, GetData.QueryUserName);

            return list;
        }

        /// <summary>
        /// 获取自己请假情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetUserLeave(UserLeavePostModel GetData)
        {
            List<UserLeaveModel> list = UserLeaveBLL.GetUserLeaveList(GetData.UserId, GetData.PageIndex, GetData.QueryUserName);

            return list;
        }

        /// <summary>
        /// 获取待我审批的请假
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetDelayExamineLeaveList(UserLeavePostModel GetData)
        {
            List<UserLeaveModel> list = UserLeaveBLL.GetDelayExamineLeaveList(GetData.UnitId, GetData.UserId, GetData.PageIndex, GetData.QueryUserName);

            return list;
        }

        /// <summary>
        /// 获取我已审批的请假
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserLeaveModel> GetReadyExamineLeaveList(UserLeavePostModel GetData)
        {
            List<UserLeaveModel> list = UserLeaveBLL.GetReadyExamineLeaveList(GetData.UnitId, GetData.UserId, GetData.PageIndex, GetData.QueryUserName);

            return list;
        }

        /// <summary>
        /// 获取审核完成信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserLeaveModel GetExamineLeaveList(UserLeavePostModel GetData)
        {
            UserLeaveModel model = UserLeaveBLL.GetExamineLeaveList((decimal)GetData.LEId);
            model.UserAvatar = model.UserAvatar;
            return model;
        }

        /// <summary>
        /// 新增请假审批
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserLeaveModel AddLeaveExamine(UserLeavePostModel GetData)
        {
            UserLeaveModel model = new UserLeaveModel();
            int result = UserLeaveBLL.UpdateLeaveExamine((decimal)GetData.LEId,GetData.ExamineStatus,GetData.ExamineContent);
            if (result > 0)
                model.IsExamineSuccess = true;
            else
                model.IsExamineSuccess = false;
            return model;
        }

        /// <summary>
        /// 根据用户标识获取信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserLeaveModel GetLeaveExamineByUserId(UserLeavePostModel GetData)
        {
            UserLeaveModel model = UserLeaveBLL.GetLeaveExamineByUserId(GetData.UserId);
            return model;
        }

        /// <summary>
        /// 获取请假列表页的冒泡数量
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public LeaveListCountModel GetLeaveListCount(UserLeavePostModel GetData)
        {
            LeaveListCountModel model = new LeaveListCountModel();
            model.ExamineCount = UserLeaveBLL.GetDelayExamineLeaveCount(GetData.UserId);
            model.TodayTeamCount = UserLeaveBLL.GetTodayTeamCount(GetData.UnitId);
            model.OtherExamineCount = UserLeaveBLL.GetOtherLeaveListCount(GetData.UserId,GetData.UnitId);

            return model;
        }
    }
}
