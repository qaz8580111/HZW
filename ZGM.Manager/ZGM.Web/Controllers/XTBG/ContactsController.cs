using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.XTBG;
using ZGM.BLL.XTBGBLL;
using ZGM.Common;
using ZGM.Model;
using ZGM.Model.CustomModels;
using OpenMas;
namespace ZGM.Web.Controllers.XTBG
{
    public class ContactsController : Controller
    {
        //
        // GET: /Contacts/

        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            //获取会议地址
            SelectListItem item = new SelectListItem
            {
                Text = "手工输入",
                Value = "TJLSDZ"
            };
            List<SelectListItem> MeetingAddress = OA_MEETINGSBLL.GetMeetingAddress().ToList().
                Select(c => new SelectListItem()
                {
                    Text = c.ADDRESSNAME,
                    Value = c.ADDRESSID.ToString()
                }).ToList();
            MeetingAddress.Add(item);
            ViewBag.MeetingAddress = MeetingAddress;
            List<SelectListItem> Meetingaddress = OA_MEETINGSBLL.GetMeetingAddress().ToList().
               Select(c => new SelectListItem()
               {
                   Text = c.ADDRESSNAME,
                   Value = c.ADDRESSID.ToString()
               }).ToList();
            ViewBag.Meetingaddres = Meetingaddress;
            return View();
        }
        /// <summary>
        /// 获取所有树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCameraTrees()
        {
            //释放内存资源
            Dispose();
            decimal uaerId = SessionManager.User.UserID;
            List<TreeModel> treeModels = OA_CONTACTSBLL.treeList(uaerId);
            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取单位树
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCameraTree()
        {
            //释放内存资源
            Dispose();
            decimal uaerId = SessionManager.User.UserID;
            List<TreeModel> treeModels = OA_CONTACTSBLL.GetTreeNodesPacket(uaerId);
            return Json(treeModels, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加分组
        /// </summary>
        /// <returns></returns>
        public int AddFZ()
        {
            string FZMC = Request["FZMC"];
            OA_CONTACTGROUPS CONTACTGROUPS = new OA_CONTACTGROUPS();
            CONTACTGROUPS.CONTACTGROUPNAME = FZMC;
            CONTACTGROUPS.CREATEDUSERID = SessionManager.User.UserID;
            OA_CONTACTGROUPSBLL.AddCONTACTGROUPS(CONTACTGROUPS);
            return 1;
        }
        /// <summary>
        /// 删除整个分组
        /// </summary>
        public int DeleteGroup()
        {
            string Parentid = Request["Parentid"];
            OA_CONTACTSBLL.DeleteCONTACTS(Parentid);
            OA_CONTACTGROUPSBLL.DeleteCONTACTGROUPS(Parentid);
            return 1;
        }
        /// <summary>
        /// 删除人员
        /// </summary>
        /// <returns></returns>
        public int DeleteUser()
        {
            string Parentid = Request["Parentid"];
            string ParentIDPid = Request["ParentIDPid"];
            OA_CONTACTSBLL.DeleteCONTACTSUser(Parentid, ParentIDPid);

            return 1;
        }
        /// <summary>
        /// 添加人员
        /// </summary>
        public int AddUser()
        {
            string RY_userids = Request["RY_userids"];
            string Parentid = Request["Parentid"];
            string[] UserId = RY_userids.Split(',');
            foreach (var item in UserId)
            {
                OA_CONTACTS model = new OA_CONTACTS();
                model.USERID = decimal.Parse(item);
                model.CONTACTGROUPID = decimal.Parse(Parentid);
                OA_CONTACTSBLL.AddCONTACTS(model);
            }
            return 1;
        }

        /// <summary>
        /// 提交消息
        /// </summary>
        public void SendMessage()
        {
            string OpenMasUrl = ConfigManager.OpenMasUrl;                  //OpenMas二次开发接口
            string ExtendCode = ConfigManager.ExtendCode;                  //扩展号
            string ApplicationID = ConfigManager.ApplicationID;            //应用账号
            string Password = ConfigManager.Password;
            //应用账号对应的密码
            //创建OpenMas二次开发接口的代理类
            Sms client = new Sms(OpenMasUrl);

            string RECEIVEUSERS = Request["SelectUserIds"];
            string RECEIVEUSERSNAME = Request["SelectUserNames"];
            string CONTENT = Request["SMS_CONTENT"];
            string phones = Request["phones"];
            string grcheckbox = Request["grcheckbox"];
            string kscheckbox = Request["kscheckbox"];
            string bsccheckbox = Request["bsccheckbox"];
            string dgwcheckbox = Request["dgwcheckbox"];

            string suffix = "";//短信后缀
            string sender = SessionManager.User.UserName;//发送人
            string Departments = SessionManager.User.UnitName;//职位

            string manualphones = Request["manualphones"];
            manualphones = manualphones.Replace("，", ",");
            phones = phones + manualphones;

            string[] SMS_phones = phones.Split(',');
            SMS_MESSAGES sms_model = new SMS_MESSAGES();
            sms_model.CONTENT = CONTENT;
            sms_model.SMSTYPE = 1;
            sms_model.RECEIVEUSERS = "," + RECEIVEUSERS + ",";
            sms_model.SENDUSERID = SessionManager.User.UserID;
            sms_model.SENDTIME = DateTime.Now;
            sms_model.PHONES = phones;
            string username = "";//手机号不存在的人员姓名


            #region 短信后缀
            if (!string.IsNullOrEmpty(grcheckbox))
            {
                suffix = "  【" + Departments + "  " + sender + "】";
                sms_model.SENDIDENTITY = suffix;
                if (!string.IsNullOrEmpty(grcheckbox) && !string.IsNullOrEmpty(kscheckbox))
                {
                    suffix = "  【" + Departments + "  " + sender + "】";
                    sms_model.SENDIDENTITY = suffix;
                }
            }
            else if (!string.IsNullOrEmpty(kscheckbox))
            {
                suffix = "  【" + Departments + "  " + sender + "】";
                sms_model.SENDIDENTITY = suffix;
            }
            else if (!string.IsNullOrEmpty(bsccheckbox))
            {
                suffix = "  联系人：" + sender + "【街道办事处 发】";
                sms_model.SENDIDENTITY = suffix;
                if (!string.IsNullOrEmpty(bsccheckbox) && !string.IsNullOrEmpty(dgwcheckbox))
                {
                    suffix = "  联系人：" + sender + "【街道党工委 街道办事处 发】";
                    sms_model.SENDIDENTITY = suffix;
                }
            }
            else if (!string.IsNullOrEmpty(dgwcheckbox))
            {
                suffix = "  联系人：" + sender + "【街道党工委 发】";
                sms_model.SENDIDENTITY = suffix;
            }
            #endregion


            string megContent = CONTENT + suffix;
            string returnvalue = "";
            if (!string.IsNullOrEmpty(grcheckbox) || !string.IsNullOrEmpty(kscheckbox))
            {
                sms_model.ISAUDIT = 1;
                returnvalue = "smssend";
                string messageId = client.SendMessage(SMS_phones, megContent, ExtendCode, ApplicationID, Password);
                sms_model.MESSAGEID = messageId;
            }
            else
            {
                sms_model.ISAUDIT = 2;
                returnvalue = "smsaudit";
            }


            ////发送短消息
            //string[] SelectUserId = RECEIVEUSERS.Split(',');

            //for (int i = 0; i < SelectUserId.Count(); i++)
            //{
            //    //RECEIVEUSERSNAME += UserBLL.GetUserNameByUserID(decimal.Parse(SelectUserId[i])) + ",";
            //    SYS_USERS uSmodel = UserBLL.GetUserByUserID(decimal.Parse(SelectUserId[i]));
            //    if (uSmodel != null && string.IsNullOrEmpty(uSmodel.PHONE))
            //    {
            //        username += uSmodel.USERNAME + ",";
            //    }
            //}
            RECEIVEUSERSNAME = RECEIVEUSERSNAME +"," + manualphones;
            sms_model.RECEIVEUSERSNAME = RECEIVEUSERSNAME;
            sms_model.SOURCE = "短信";
            //if (username != "")
            //{
            //    username = username.Substring(0, username.Length - 1);
            //}
            //sms_model.REMARK = username;
            SMS_MESSAGESBLL.AddMessages(sms_model);
            Response.Write("<script>parent.AddCallBack('" + returnvalue + "')</script>");
        }





        public JsonResult GetCountactsByDateUserID()
        {

            UserInfo model = UserBLL.GetUserInfoByUser(decimal.Parse(Request["ID"]), Request["NAME"]);
            string Str = string.Empty;
            if (model != null)
            {
                Str += "姓名:";
                Str += model.UserName;
                Str += "$$部门:";
                Str += model.UnitName;
                Str += "$$电话:";
                Str += model.Phone == null ? "" : model.Phone;
            }
            return Json(new
            {
                //str_jh = str_jh,
                //str_sj = str_sj,
                str_Content = Str
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
