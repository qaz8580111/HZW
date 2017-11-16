using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model;
using ZGM.Model.PhoneModel;
using ZGM.Model.XTBGModels;
using ZGM.BLL.XTBG;
using ZGM.BLL.XTBGBLL;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model.CustomModels;
using ZGM.PhoneAPI.XTGL;
using ZGM.BLL.WORKFLOWManagerBLLs;
using ZGM.Model.CoordinationManager;
using ZGM.BLL.XTGLBLL;
using ZGM.BLL.MessageBLL;
using ZGM.BLL.PhoneBLLs;

namespace ZGM.PhoneAPI.Controllers.XTBG
{
    public class XTBGStatisticsController : ApiController
    {
        /// <summary>
        /// 获取首页所有数量
        /// </summary>
        public XTBGStatisticsModel GetALLXTBGCount(decimal UserId)
        {
            try
            {
                XTBGStatisticsModel model = new XTBGStatisticsModel();
                //获取未读公告数量
                model.NoticeCount = OA_NoticeBLL.GetUnReadNoticeCount(UserId);

                //获取未读会议数量
                model.MeetingCount = OA_MEETINGSBLL.GetMeetinglist(UserId).Where(a => a.ISREAD == 2 && a.ISCANCEL == 1).Count() + OA_MEETINGSBLL.GetMyCheckMeetinglist(UserId).Count();
                //获取未办任务数量
                IEnumerable<TasksListModel> List1 = OA_TASKSBLL.GetAllEvent(UserId).Where(a => a.wfdid == "20160517094110002" && a.nextuserid == UserId);//社区主任派遣
                IEnumerable<TasksListModel> List2 = OA_TASKSBLL.GetAllEvent(UserId).Where(a => a.wfdid == "20160517094110003");//队员处理
                IEnumerable<TasksListModel> List3 = OA_TASKSBLL.GetAllEvent(UserId).Where(a => a.wfdid == "20160517094110004" && a.nextuserid == UserId);//社区主任审核
                IEnumerable<TasksListModel> List4 = OA_TASKSBLL.GetAllEvent(UserId).Where(a => a.wfdid == "20160517094110005");//科室审核
                IEnumerable<TasksListModel> List5 = OA_TASKSBLL.GetAllEvent(UserId).Where(a => a.wfdid == "20160517094110007");//科室派遣
                IEnumerable<TasksListModel> List6 = OA_TASKSBLL.GetAllEvent(UserId).Where(a => a.wfdid == "20160517094110008");//党政办审核

                var USERROLEID = SystemPhoneBLL.GetPhoneUserRoles(UserId).USERROLEID;
                if (USERROLEID.Contains("\\27\\"))
                {
                    model.TaskCount = List1.Count() + List3.Count();
                }
                else if (USERROLEID.Contains("\\28\\"))
                {
                    model.TaskCount = List2.Count();
                }
                else if (USERROLEID.Contains("\\32\\"))
                {
                    model.TaskCount = List4.Count() + List5.Count();
                }
                else if (USERROLEID.Contains("\\33\\"))
                {
                    model.TaskCount =List6.Count();
                }
                else
                {
                    model.TaskCount =0;
                }

               //model.TaskCount = List1.Count() + List2.Count() + List3.Count() + List4.Count();
                
                //获取未读文件数量
                model.FileCount = OA_FileBLL.GetUnReadFileCount(UserId);

                //获取待审核请假数量
                model.LeaveCount = UserLeaveBLL.GetDelayExamineLeaveCount(UserId);

                //获取未处理报警数量
                model.PoliceCount = AlarmBLL.GetPoliceCount(UserId);

               
                //获取未处理事件数量
                ListInformation modelevent = new ListInformation();
                WF_WORKFLOWDETAILBLL WF = new WF_WORKFLOWDETAILBLL();
                int eventcount1 = WF.GetUnFinishedEvent(UserId, "20160407132010002").Count();
                int eventcount2 = WF.GetUnFinishedEvent(UserId, "20160407132010003").Count();
                int eventcount3 = WF.GetUnFinishedEvent(UserId, "20160407132010004").Count();
                var INSPECTIONIDEAS_List = XTGL_INSPECTIONIDEASBLL.GetAddINSPECTIONIDEASList();
                IEnumerable<EnforcementUpcoming> List = null;
                List<EnforcementUpcoming> lists = new List<EnforcementUpcoming>();
                string wfdid="20160407132010003";
                foreach (var item in INSPECTIONIDEAS_List)
                {
                    List = WF.GetDCSJEventList(UserId,wfdid).Where(a => a.ZFSJID.Contains(item.ZFSJID));
                    EnforcementUpcoming li = new EnforcementUpcoming();
                    foreach (var items in List)
                    {
                        li.EVENTTITLE = items.EVENTTITLE;
                        li.wfid = items.wfid;
                        li.wfsid = items.wfsid;
                        li.wfname = items.wfname;
                        li.wfdid = items.wfdid;
                        li.wfsaid = items.wfsaid;
                        li.wfsname = items.wfsname;
                        li.username = items.username;
                        li.wfdname = items.wfdname;
                        li.ZFSJID = items.ZFSJID;

                    }
                    if (li.wfdid != null && li.wfsid != null)
                    {
                        lists.Add(li);
                    }
                    else
                    {
                        lists = new List<EnforcementUpcoming>();
                    }
                }
                int eventcount4 = lists != null ? lists.Count() : 0;//获取总行数
                int eventcount5 = WF.GetAllEvent(UserId).Where(t => t.wfdid == "20160407132010003" && t.ISOVERDUE == 1 && t.status == 1).Count();
                model.EventCount = eventcount1 + eventcount2 + eventcount3 + eventcount4 + eventcount5;

                //获取未处理事件数量
                //MessageModel mmodel = new MessageModel();
                //mmodel.RECEIVERID = UserId;
                //model.MessageCount = MessageBLL.GetIsReaderMessageCounts(mmodel);

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取首页轮播图片
        /// </summary>
        public List<SYS_PHOTECAROUSELS> GetIndexPictures()
        {
            List<SYS_PHOTECAROUSELS> list = SystemPhoneBLL.GetIndexPicturesInfo();
            return list;
        }

    }
}
