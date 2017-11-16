using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.XTBG;
using ZGM.BLL.XTBGBLL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Model.PhoneModel;
using ZGM.Model.XTBGModels;


namespace ZGM.PhoneAPI.Controllers.XTBG
{
    public class MeetingController : ApiController
    {



        /// <summary>
        /// 添加会议
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Add(MeetingModel models)
        {
            try
            {
                string[] SelectUserId = models.SelectUserIds.Split(',');
                var MettingsID = OA_MEETINGSBLL.GetNewMeetingID();
                //#region 获取文件上传文件
                //var file = Request.Files;
                //string myPath = System.Configuration.ConfigurationManager.AppSettings["XTGLMeetingFile"].ToString();
                //List<FileUploadClass> list_file = new Process.FileUpload.FileUpload().UploadImages(file, myPath);
                //#endregion
                OA_MEETINGS model = new OA_MEETINGS();
                model.MEETINGID = MettingsID;
                model.CREATEUSER = models.CREATEUSER;
                model.CREATETIME = DateTime.Now;
                model.ISCANCEL = 1;
                model.ETIME = models.ETIME;
                model.STIME = models.STIME;
                model.MEETINGTITLE = models.MEETINGTITLE;
                model.ADDRESSID = models.ADDRESSID;
                model.CONTENT = models.CONTENT;
                model.LEAVEAUDITUSER = models.LEAVEAUDITUSER;

                OA_MEETINGSBLL.AddMEETINGS(model);

                string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLMeetingFile"];
                //附件上传
                if (models.FileStr1 != null && models.FileStr1.Length != 0)
                {
                    string[] spilt = models.FileStr1.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType1, FilePath);

                        OA_ATTRACHS attrach = new OA_ATTRACHS();
                        attrach.ATTRACHSOURCE = 2;
                        attrach.ATTRACHNAME = FC.OriginalName;
                        attrach.ATTRACHPATH = FC.OriginalPath;
                        attrach.ATTRACHTYPE = FC.OriginalType;
                        attrach.SOURCETABLEID = MettingsID;
                        OA_ATTRACHSBLL.AddATTRACHS(attrach);
                    }
                }

                if (models.FileStr2 != null && models.FileStr2.Length != 0)
                {
                    string[] spilt = models.FileStr2.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType2, FilePath);
                        OA_ATTRACHS attrach = new OA_ATTRACHS();
                        attrach.ATTRACHSOURCE = 2;
                        attrach.ATTRACHNAME = FC.OriginalName;
                        attrach.ATTRACHPATH = FC.OriginalPath;
                        attrach.ATTRACHTYPE = FC.OriginalType;
                        attrach.SOURCETABLEID = MettingsID;
                        OA_ATTRACHSBLL.AddATTRACHS(attrach);
                    }
                }

                if (models.FileStr3 != null && models.FileStr3.Length != 0)
                {

                    string[] spilt = models.FileStr3.Split(',');
                    if (spilt.Length > 1)
                    {
                        byte[] myByte = Convert.FromBase64String(spilt[1]);
                        FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType3, FilePath);
                        OA_ATTRACHS attrach = new OA_ATTRACHS();
                        attrach.ATTRACHSOURCE = 2;
                        attrach.ATTRACHNAME = FC.OriginalName;
                        attrach.ATTRACHPATH = FC.OriginalPath;
                        attrach.ATTRACHTYPE = FC.OriginalType;
                        attrach.SOURCETABLEID = MettingsID;
                        OA_ATTRACHSBLL.AddATTRACHS(attrach);
                    }
                }
                //循环插入参会人员
                foreach (var item in SelectUserId)
                {
                    OA_USERMEETINGS userMeeting = new OA_USERMEETINGS();
                    userMeeting.MEETINGID = MettingsID;
                    userMeeting.USERID = decimal.Parse(item);
                    userMeeting.ISREAD = 2;
                    if (userMeeting.USERID == models.CREATEUSER)
                    {
                        userMeeting.ISREAD = 1;
                    }
                    userMeeting.ISLEAVE = 1;
                    userMeeting.ISAPPROVE = 0;//是否批准默认值不能为否
                    userMeeting.ISPARTIN = 3;
                    OA_USERMEETINGSBLL.AddMEETINGS(userMeeting);

                    OA_SCHEDULES schedules = new OA_SCHEDULES();
                    schedules.OWNER = decimal.Parse(item);
                    schedules.TITLE = models.MEETINGTITLE;
                    schedules.CONTENT = models.CONTENT;
                    schedules.SCHEDULESOURCE = "会议";
                    schedules.STARTTIME = models.STIME;
                    schedules.ENDTIME = models.ETIME;
                    schedules.SHARETYPEID = 0;
                    schedules.CREATEDUSERID = models.CREATEUSER;
                    schedules.CREATEDITME = DateTime.Now;
                    OA_ScheduleBLL.AddScedule(schedules);
                }
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }

        }

        /// <summary>
        /// 上传
        /// </summary>
        public string Minutes(MeetingModel models)
        {
            decimal Meetingids = 0;
            decimal.TryParse(models.MEETINGID.ToString(), out Meetingids);

            string FilePath = System.Configuration.ConfigurationManager.AppSettings["XTGLMeetingSummaryFile"];
            //附件上传
            if (models.FileStr1 != null && models.FileStr1.Length != 0)
            {
                string[] spilt = models.FileStr1.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType1, FilePath);

                    OA_ATTRACHS attrach = new OA_ATTRACHS();
                    attrach.ATTRACHSOURCE = 3;
                    attrach.ATTRACHNAME = FC.OriginalName;
                    attrach.ATTRACHPATH = FC.OriginalPath;
                    attrach.ATTRACHTYPE = FC.OriginalType;
                    attrach.SOURCETABLEID = Meetingids;
                    OA_ATTRACHSBLL.AddATTRACHS(attrach);
                }
            }

            if (models.FileStr2 != null && models.FileStr2.Length != 0)
            {
                string[] spilt = models.FileStr2.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType2, FilePath);
                    OA_ATTRACHS attrach = new OA_ATTRACHS();
                    attrach.ATTRACHSOURCE = 3;
                    attrach.ATTRACHNAME = FC.OriginalName;
                    attrach.ATTRACHPATH = FC.OriginalPath;
                    attrach.ATTRACHTYPE = FC.OriginalType;
                    attrach.SOURCETABLEID = Meetingids;
                    OA_ATTRACHSBLL.AddATTRACHS(attrach);
                }
            }

            if (models.FileStr3 != null && models.FileStr3.Length != 0)
            {

                string[] spilt = models.FileStr3.Split(',');
                if (spilt.Length > 1)
                {
                    byte[] myByte = Convert.FromBase64String(spilt[1]);
                    FileUploadClass FC = FileFactory.FileSave(myByte, models.FileType3, FilePath);
                    OA_ATTRACHS attrach = new OA_ATTRACHS();
                    attrach.ATTRACHSOURCE = 3;
                    attrach.ATTRACHNAME = FC.OriginalName;
                    attrach.ATTRACHPATH = FC.OriginalPath;
                    attrach.ATTRACHTYPE = FC.OriginalType;
                    attrach.SOURCETABLEID = Meetingids;
                    OA_ATTRACHSBLL.AddATTRACHS(attrach);
                }
            }

            return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
        }


        /// <summary>
        /// 查询详情清单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ConferenceList> SelectMeetingTableList(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMeetinglist(Id);
                if (!string.IsNullOrEmpty(model.title))
                    List = List.Where(t => t.MEETINGTITLE.Contains(model.title));
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new ConferenceList()
                    {
                        #region 获取
                        ISCANCEL = a.ISCANCEL,
                        USERID = a.USERID,
                        MEETINGID = a.MEETINGID,
                        STIME = Convert.ToDateTime(a.STIME),
                        ETIME = Convert.ToDateTime(a.ETIME),
                        MEETINGTITLE = a.MEETINGTITLE,
                        ADDRESSID = a.ADDRESSID,
                        ADDRESSNAME=a.ADDRESSNAME,
                        CONTENT = a.CONTENT,
                        USERNUM = OA_USERMEETINGSBLL.GetMeetingListNum(a.MEETINGID),
                        LEAVEAUDITUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.LEAVEAUDITUSER.ToString())),
                        CREATEUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.CREATEUSER.ToString()))
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 会议清单列表数量
        /// </summary>
        /// <param name="model">model.idread  1、已阅 2、未阅</param>
        /// <returns></returns>
        public string SelectMeetingTableListCount(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;

                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMeetinglist(Id);
                return List.Count().ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// 查询已读，未读列表
        /// </summary>
        /// <param name="model">model.idread  1、已阅 2、未阅</param>
        /// <returns></returns>
        public List<ConferenceList> SelectReadMeetingTableList(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMeetinglist(Id).Where(a => a.ISREAD == model.idread && a.ISCANCEL == 1);

                if (!string.IsNullOrEmpty(model.title))
                    List = List.Where(t => t.MEETINGTITLE.Contains(model.title));
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new ConferenceList()
                    {
                        #region 获取
                        ISCANCEL = a.ISCANCEL,
                        USERID = a.USERID,
                        MEETINGID = a.MEETINGID,
                        STIME = Convert.ToDateTime(a.STIME),
                        ETIME = Convert.ToDateTime(a.ETIME),
                        MEETINGTITLE = a.MEETINGTITLE,
                        ADDRESSID = a.ADDRESSID,
                        CONTENT = a.CONTENT,
                        USERNUM = OA_USERMEETINGSBLL.GetMeetingListNum(a.MEETINGID),
                        LEAVEAUDITUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.LEAVEAUDITUSER.ToString())),
                        CREATEUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.CREATEUSER.ToString()))
                        #endregion
                    });

                // data = data.Where(t => t.CREATEUSER != model.userId);
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 查询已读，未读列表(条数)
        /// </summary>
        /// <param name="model">model.idread  1、已阅 2、未阅</param>
        /// <returns></returns>
        public string SelectReadMeetingCount(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMeetinglist(Id).Where(a => a.ISREAD == model.idread && a.ISCANCEL == 1);
                return List.Count().ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// 已读未读总条数
        /// </summary>
        /// <param name="model">model.idread  1、已阅 2、未阅</param>
        /// <returns></returns>
        public string SelectReadMeetingListCount(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMeetinglist(Id);
                return List.Count().ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }


        /// <summary>
        /// 自己创建的会议数量
        /// </summary>
        /// <param name="model">model.idread  1、已阅 2、未阅</param>
        /// <returns></returns>
        public string SelectMyMeetingTableListCount(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;

                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMyMeetinglist(Id).OrderByDescending(t => t.CREATETIME);
                return List.Count().ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }
        /// <summary>
        /// 查询自己创建的会议列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ConferenceList> SelectMyMeetingTableList(MeetingListInformation model)
        {
            try
            {
                string title = model.title;
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMyMeetinglist(Id).OrderByDescending(t => t.CREATETIME);
                if (!string.IsNullOrEmpty(title))
                    List = List.Where(t => t.MEETINGTITLE.Contains(title));
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new ConferenceList()
                    {
                        #region 获取
                        ISCANCEL = a.ISCANCEL,
                        USERID = a.USERID,
                        MEETINGID = a.MEETINGID,
                        STIME = Convert.ToDateTime(a.STIME),
                        ETIME = Convert.ToDateTime(a.ETIME),
                        MEETINGTITLE = a.MEETINGTITLE,
                        ADDRESSID = a.ADDRESSID,
                        CONTENT = a.CONTENT,
                        USERNUM = OA_USERMEETINGSBLL.GetMeetingListNum(a.MEETINGID),
                        LEAVEAUDITUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.LEAVEAUDITUSER.ToString())),
                        CREATEUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.CREATEUSER.ToString()))
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }



        /// <summary>
        /// 提交请假
        /// </summary>
        [HttpPost]
        public string LeaveToSubmitInformation(AskForLeaveClass model)
        {
            try
            {
                string MEETINGID = model.MEETINGID.ToString();
                string LEAVEAUDITUSER = model.LEAVEAUDITUSERID.ToString();
                string LEAVECONTENT = model.LEAVECONTENT;
                decimal USERID = model.USERID;
                decimal Meetingids = 0;
                decimal.TryParse(MEETINGID, out Meetingids);
                decimal Leaveaudituser = 0;
                decimal.TryParse(LEAVEAUDITUSER, out Leaveaudituser);
                OA_USERMEETINGS usermeetings = new OA_USERMEETINGS();
                usermeetings.USERID = USERID;
                usermeetings.MEETINGID = Meetingids;
                usermeetings.LEAVECONTENT = LEAVECONTENT;
                usermeetings.LEAVETIME = DateTime.Now;
                usermeetings.ISLEAVE = 2;
                // usermeetings.ISAPPROVE = 1;
                OA_USERMEETINGSBLL.EditUserMeetings(usermeetings);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }

        }

        /// <summary>
        /// 提交取消会议信息
        /// </summary>
        [HttpPost]
        public string DeleteMeetinga(CancelMeetingClass model)
        {
            try
            {
                decimal MEETINGID = model.MEETINGID;
                string CANCELLATIONREASON = model.CANCELLATIONREASON;

                OA_MEETINGS meetings = new OA_MEETINGS();
                meetings.MEETINGID = MEETINGID;
                meetings.CANCELLATIONREASON = CANCELLATIONREASON;
                meetings.DELETECONFERENCETIME = DateTime.Now;
                meetings.ISCANCEL = 2;
                OA_MEETINGSBLL.DeleteConference(meetings);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }

        }



        /// <summary>
        /// 查询待审核会议列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ConferenceList> CheckMyMeetingTableList(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMyCheckMeetinglist(Id).OrderByDescending(t => t.CREATETIME);
                // string titles="";
                //if (!string.IsNullOrEmpty(model.title))
                //{
                //    titles = model.title;
                //    List = from i in List
                //           where i.MEETINGTITLE.Contains(titles)
                //           select i;
                //}
                if (!string.IsNullOrEmpty(model.title))
                    List = List.Where(t => t.MEETINGTITLE.Contains(model.title));
                var data = List.Skip(model.page * 10).Take(10)
                    .Select(a => new ConferenceList()
                    {
                        #region 获取
                        ISCANCEL = a.ISCANCEL,
                        USERID = a.USERID,
                        MEETINGID = a.MEETINGID,
                        STIME = Convert.ToDateTime(a.STIME),
                        ETIME = Convert.ToDateTime(a.ETIME),
                        MEETINGTITLE = a.MEETINGTITLE,
                        ADDRESSID = a.ADDRESSID,
                        CONTENT = a.CONTENT,
                        USERNUM = OA_USERMEETINGSBLL.GetMeetingListNum(a.MEETINGID),
                        LEAVEAUDITUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.LEAVEAUDITUSER.ToString())),
                        CREATEUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(a.CREATEUSER.ToString()))
                        #endregion
                    });
                return data.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 待审核会议列表数量
        /// </summary>
        /// <param name="model">model.idread  1、已阅 2、未阅</param>
        /// <returns></returns>
        public string CheckMyMeetingTableListCount(MeetingListInformation model)
        {
            try
            {
                decimal Id = model.userId;
                IEnumerable<ConferenceList> List = OA_MEETINGSBLL.GetMyCheckMeetinglist(Id);
                return List.Count().ToString();
            }
            catch (Exception)
            {
                return "0";
            }
        }


        /// <summary>
        /// 请假审核
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string LeaveReview(LeaveReviewClass models)
        {
            try
            {
                decimal USERID = models.USERID;
                decimal MEETINGID = models.MEETINGID;
                string CONTENT = models.CONTENT;

                OA_USERMEETINGS model = new OA_USERMEETINGS();
                model.USERID = USERID;
                model.MEETINGID = MEETINGID;
                model.APPROVETIME = DateTime.Now;
                model.APPROVECONTENT = CONTENT;
                model.ISPARTIN = 2;
                model.ISAPPROVE = models.ISAPPROVE;//是否同意请假 1同意请假 2 不同意请假
                if (models.ISAPPROVE == 1)
                {
                    model.ISPARTIN = 2;
                }
                else
                {
                    model.ISPARTIN = 1;
                }
                OA_USERMEETINGSBLL.LeaveTheMeeting(model);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }

        }

        /// <summary>
        /// 会议详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string QueryPendingEventsDetail(LeaveReviewClass model)
        {
            try
            {
                OA_USERMEETINGS oau = OA_MEETINGSBLL.GetUserMeetingByMUID(model.MEETINGID, model.USERID);
                List<ConferenceList> list = new List<ConferenceList>();
                if (oau != null)
                {
                    list = OA_MEETINGSBLL.GetMeeting(model.MEETINGID).Where(a => a.USERID == model.USERID).ToList();//待审核的
                }
                else
                {
                    list = OA_MEETINGSBLL.GetMeeting(model.MEETINGID).Where(a => a.LEAVEAUDITUSER == model.USERID || a.USERID == model.USERID || a.CREATEUSER == model.USERID).ToList();
                }


                Dictionary<string, object> oadic = new Dictionary<string, object>();
                List<Dictionary<string, object>> diclist = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> oalist = new List<Dictionary<string, object>>();
                foreach (var item in list)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    var MEETINGID = item.MEETINGID;
                    var MEETINGTITLE = item.MEETINGTITLE;
                    var STIME = item.STIME;
                    var ETIME = item.ETIME;
                    var ADDRESS =OA_MEETINGSBLL.GetMeetingAdderssName(item.ADDRESSID);
                    var CONTENT = item.CONTENT;
                    var CREATEUSER = item.CREATEUSER;
                    var LEAVEAUDITUSER = UserBLL.GetUserNameByUserID(decimal.Parse(item.LEAVEAUDITUSER.ToString()));
                    var LEAVEAUDITUSERNAME = UserBLL.GetUserNameByUserID(decimal.Parse(item.LEAVEAUDITUSER.ToString()));
                    var ISCANCEL = item.ISCANCEL;
                    var ISLEAVE = item.ISLEAVE;
                    var ISAPPROVE = item.ISAPPROVE;
                    var ISPARTIN = item.ISPARTIN;

                    dic.Add("ISAPPROVE", ISAPPROVE);
                    dic.Add("ISLEAVE", ISLEAVE);
                    dic.Add("ISCANCEL", ISCANCEL);
                    dic.Add("CREATEUSER", CREATEUSER);
                    dic.Add("ISPARTIN", ISPARTIN);
                    dic.Add("MEETINGID", MEETINGID);
                    dic.Add("MEETINGTITLE", MEETINGTITLE);
                    dic.Add("STIME", STIME);
                    dic.Add("ETIME", ETIME);
                    dic.Add("ADDRESS", ADDRESS);
                    dic.Add("CONTENT", CONTENT);
                    dic.Add("LEAVEAUDITUSER", LEAVEAUDITUSER);
                    dic.Add("LEAVEAUDITUSERNAME", LEAVEAUDITUSERNAME);

                    diclist.Add(dic);
                }

                List<OA_ATTRACHS> oa_list = OA_MEETINGSBLL.GetFilePath(model.MEETINGID).Where(a => a.ATTRACHSOURCE == 2).OrderBy(t => t.ATTRACHSOURCE).ToList();
                List<OA_ATTRACHS> oa_list1 = OA_MEETINGSBLL.GetFilePath(model.MEETINGID).Where(a => a.ATTRACHSOURCE == 3).OrderByDescending(t => t.ATTRACHID).ToList();
                //foreach (var item in oa_list)
                //{
                //    if (item.ATTRACHSOURCE == 2)
                //    {
                //        oadic.Add("ATTRACHPATH2", item.ATTRACHPATH);
                //        oadic.Add("ATTRACHNAME2", item.ATTRACHNAME);
                //    }
                //    else if (item.ATTRACHSOURCE == 3)
                //    {
                //        oadic.Add("ATTRACHPATH3", item.ATTRACHPATH);
                //        oadic.Add("ATTRACHNAME3", item.ATTRACHNAME);
                //    }
                //    oalist.Add(oadic);
                //}

                for (int i = 0; i < oa_list.Count; i++)
                {
                    if (i == 0)
                    {
                        oadic.Add("ATTRACHPATH21", oa_list[i].ATTRACHPATH);
                        oadic.Add("ATTRACHNAME21", oa_list[i].ATTRACHNAME);
                    }
                    if (i == 1)
                    {
                        oadic.Add("ATTRACHPATH22", oa_list[i].ATTRACHPATH);
                        oadic.Add("ATTRACHNAME22", oa_list[i].ATTRACHNAME);
                    }
                    if (i == 2)
                    {
                        oadic.Add("ATTRACHPATH23", oa_list[i].ATTRACHPATH);
                        oadic.Add("ATTRACHNAME23", oa_list[i].ATTRACHNAME);
                    }

                }
                for (int j = 0; j < oa_list1.Count; j++)
                {
                    if (j == 0)
                    {
                        oadic.Add("ATTRACHPATH31", oa_list1[j].ATTRACHPATH);
                        oadic.Add("ATTRACHNAME31", oa_list1[j].ATTRACHNAME);
                    }
                    if (j == 1)
                    {
                        oadic.Add("ATTRACHPATH32", oa_list1[j].ATTRACHPATH);
                        oadic.Add("ATTRACHNAME32", oa_list1[j].ATTRACHNAME);
                    }
                    if (j == 2)
                    {
                        oadic.Add("ATTRACHPATH33", oa_list1[j].ATTRACHPATH);
                        oadic.Add("ATTRACHNAME33", oa_list1[j].ATTRACHNAME);
                    }
                }

                oalist.Add(oadic);




                Entities db = new Entities();
                OA_USERMEETINGS oumodel = new OA_USERMEETINGS();
                if (db.OA_USERMEETINGS.FirstOrDefault(t => t.MEETINGID == model.MEETINGID && t.USERID == model.USERID) != null)
                {
                    var usermeetingmodel = db.OA_USERMEETINGS.FirstOrDefault(t => t.MEETINGID == model.MEETINGID && t.USERID == model.USERID);
                    usermeetingmodel.ISREAD = 1;
                    db.SaveChanges();
                }



                string oajson = JsonConvert.SerializeObject(oalist);
                string json = JsonConvert.SerializeObject(diclist);
                return "{\"resData\":" + json + ",\"oaresData\":" + oajson + ",\"resCode\":\"1\"}";

            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
                throw;
            }

        }


        /// <summary>
        /// 查询参会人员
        /// </summary>
        /// <param name="iDisplayStart"></param>
        /// <param name="iDisplayLength"></param>
        /// <param name="secho"></param>
        /// <returns></returns>
        [HttpPost]
        public List<ConferenceList> UserTableList(MeetingListInformation modle)
        {
            string MeetingID = modle.MEETINGID.ToString();
            decimal Meetingids = 0;
            decimal.TryParse(MeetingID, out Meetingids);
            List<ConferenceList> List = OA_MEETINGSBLL.GetUserMeetingList(Meetingids).ToList();
            var data = List.Select(a => new ConferenceList()
                {
                    #region 获取
                    MEETINGID = a.MEETINGID,
                    USERID = a.USERID,
                    USERNAME = a.USERNAME,
                    STRINGLEAVETIME = string.IsNullOrEmpty(a.LEAVETIME.ToString()) ? "无" : a.LEAVETIME.Value.ToString("yyyy-MM-dd HH:mm"),
                    ISAPPROVE = a.ISAPPROVE,
                    ISLEAVE = a.ISLEAVE,
                    LEAVECONTENT = string.IsNullOrEmpty(a.LEAVECONTENT) ? "无" : a.LEAVECONTENT,
                    ISPARTIN = a.ISPARTIN,
                    APPROVECONTENT = string.IsNullOrEmpty(a.APPROVECONTENT) ? "无" : a.APPROVECONTENT,
                    #endregion
                });

            if (modle.ISLEAVE == 2)
            {
                var aa = (from i in data
                          where i.ISLEAVE == 2 // && i.ISAPPROVE == 2
                          select i).ToList();
                return aa;
                // return data.Where(t=>t.ISLEAVE==2&&t.ISAPPROVE==1).ToList();

            }
            else
            {
                return data.ToList();
            }

        }

        /// <summary>
        /// 参会
        /// </summary>
        /// <returns></returns>
        public string Participants(LeaveReviewClass model)
        {
            try
            {
                decimal meetingid = decimal.Parse(model.MEETINGID.ToString());
                decimal userid = model.USERID;
                OA_USERMEETINGSBLL.Participants(meetingid, userid);
                return "{\"msg\":\"成功！\",\"resCode\":\"1\"}";
            }
            catch (Exception)
            {
                return "{\"msg\":\"json数据不正确\",\"resCode\":\"0\"}";
            }
        }

        /// <summary>
        /// 获取会议地点
        /// </summary>
        /// <returns></returns>
        public List<MeetingAddresses> GetMeetingAddresses()
        {
            Entities db = new Entities();
            List<MeetingAddresses> list = OA_MEETINGSBLL.GetMeetingAddresses();

            return list;
        }

    }
}
