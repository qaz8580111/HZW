using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.Model.CoordinationManager;
using ZGM.Model.CustomModels;
using ZGM.Model.PhoneModel;

namespace ZGM.PhoneAPITest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public void LoginTest(List<HttpPostedFileWrapper> img)
        {
            UserLoginPostModel model = new UserLoginPostModel();
            model.Account = "张伟伟";
            model.PassWord = "123456";
            model.PhoneTime = DateTime.Now.ToString();
          
            //121.330066817003,28.6621381900164;---------121.37824150325,28.669425117348;--------121.333710280668,28.6176069674346;121.330066817003,28.6621381900164
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:17277/api/Login/UserLogin", Newtonsoft.Json.JsonConvert.SerializeObject(model));
            //string jsonresult = HttpHelper.SendDataByPost("http://10.80.2.124:8083/api/UserSignIn/UserExamine", Newtonsoft.Json.JsonConvert.SerializeObject(model));

        }

        public void UserSignInTest(List<HttpPostedFileWrapper> img)
        {
            UserSignInPostModel model = new UserSignInPostModel();
            model.UserId = 10;
            model.UnitId =22;
            model.QueryUserName = "";
            model.Longitude = "";
            model.Latitude = "";
            model.SGID = 518;
            model.Longitude = "120.123268";
            model.Latitude = "30.291575";

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:17277/api/UserSignIn/GetExamineInfoByExamineId", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        public void UserExamineTest(List<HttpPostedFileWrapper> img)
        {
            ExaminePostModel model = new ExaminePostModel();
            model.UserId = 1;
            model.ExamineId = 1;
            model.StartDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToLongDateString());
            model.EndDate = DateTime.Parse(DateTime.Now.ToLongDateString());
            model.JobScore = 3;
            model.SignInScore = 4;
            model.AlarmScore = 5;
            model.Score = 12;

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:17277/api/UserExamine/GetBeExamineUser", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        public void UserLeaveTest(List<HttpPostedFileWrapper> img)
        {
            UserLeavePostModel model = new UserLeavePostModel();
            model.UserId = 1;
            model.UnitId = 6;
            model.PageIndex = 0;
            model.Examiner = "1,2,3,4";
            model.LEId = 1;

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:17277/api/UserLeave/GetOtherLeave", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        public void AnnouncementTest(List<HttpPostedFileWrapper> img)
        {
            OA_POSTNOTICES model = new OA_POSTNOTICES();
            model.NOTICEID = 416;
            model.UserId = 53;
            model.PageIndex = 0;
            model.QueryTitle = "";
            model.NOTICETITLE = "标题"+new Random().Next(10000,99999);
            model.NOTICETYPE = "类型" + new Random().Next(10000, 99999);
            model.AUTHOR = "作者" + new Random().Next(10000, 99999);
            model.CONTENT = "内容" + new Random().Next(10000, 99999);
            model.CREATEDUSER = new Random().Next(1,100);

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:17277/api/Announcement/GetAnnouncementInfoById", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        public void FileTest(List<HttpPostedFileWrapper> img)
        {
            OA_POSTFILE model = new OA_POSTFILE();
            model.FILEID = 256;
            model.UserId = 138;
            model.PageIndex = 0;
            model.QueryTitle = "";
            model.FILENUMBER = "文号" + new Random().Next(10000, 99999);
            model.FILETITLE = "标题" + new Random().Next(10000, 99999);
            model.FILECONTENT = "内容" + new Random().Next(10000, 99999);
            model.CREATEUSERID = 1;
            model.CREATETIME = DateTime.Now;

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:14491/api/File/GetFileInfoById", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }

        public void ListTest(List<HttpPostedFileWrapper> img)
        {
            XTGLXQModel model = new XTGLXQModel();
            model.userId = 5;
            model.ZONEID = 122223;
            model.WFID = "20160407132010001";
            model.X2000 =decimal.Parse(" 121.332066817003");
            model.Y2000 = decimal.Parse("28.6501381900164");
            model.EVENTTITLE = "啊啊啊啊啊啊啊啊啊";
            //121.330066817003,28.6621381900164;---------121.37824150325,28.669425117348;--------121.333710280668,28.6176069674346;121.330066817003,28.6621381900164
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:12544/api/XTGL/Commit", Newtonsoft.Json.JsonConvert.SerializeObject(model));
            //string jsonresult = HttpHelper.SendDataByPost("http://10.80.2.124:8083/api/UserSignIn/GetUserSignIn", Newtonsoft.Json.JsonConvert.SerializeObject(model));

        }


        public void DYlist(List<HttpPostedFileWrapper> img) {
            XTGLXQModel model = new XTGLXQModel();
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:12544/api/UserExamine/GetBeExamineUser", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }
        public void list(List<HttpPostedFileWrapper> img) 
        {
            EnforcementUpcoming model = new EnforcementUpcoming();
            model.ZFSJID = "20160420110917165";
            model.wfdid = "20160407132010002";
            model.wfsaid = "20160420110917228";
            model.wfsid = "20160420110917181";
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:12544/api/XTGL/ViewEvent", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }


        public void GetZFSJSOURCES(List<HttpPostedFileWrapper> img) 
        {
            ListInformation model = new ListInformation();
            model.userId = 10;
            model.Number = 10;
            model.page = 1;

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:12544/api/XTGL/EventProcessingTableList", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }
        /// <summary>
        /// 图片写入
        /// </summary>
        /// <param name="img"></param>
        public void Commit(List<HttpPostedFileWrapper> img) {
            XTGLXQModel eventReport = new XTGLXQModel();
            eventReport.Photo1 = "123";

            string jsonresult = HttpHelper.SendDataByPost("http://10.80.2.124:8083/api/XTGL/Commit", Newtonsoft.Json.JsonConvert.SerializeObject(eventReport));
        }

        public void UserHistoryPosition(List<HttpPostedFileWrapper> img)
        {
            UserHistoryPositionModel eventReport = new UserHistoryPositionModel();
            eventReport.SPEED = 5;
            eventReport.UserId = 5;
            eventReport.GEOMETRY = "120.123337,30.291474 ";
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:12544/api/XTGL/UserHistoryPosition", Newtonsoft.Json.JsonConvert.SerializeObject(eventReport));
        }
        public void csxq(List<HttpPostedFileWrapper> img)
        {
            GetID eventReport = new GetID();
            eventReport.wfsid = "20160425142450176";
            eventReport.ZFSJID = "20160425142450160";
            eventReport.wfdid = "20160407132010006";
            eventReport.wfsaid = "20160425154348051";

            string jsonresult = HttpHelper.SendDataByPost("http://localhost:12544/api/XTGL/ViewEvent", Newtonsoft.Json.JsonConvert.SerializeObject(eventReport));
        }
        public void history()
        {
            GetID eventReport = new GetID();
            eventReport.wfsid = "20160425142450176";
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:7617/api/XTGL/history", Newtonsoft.Json.JsonConvert.SerializeObject(eventReport));
        }

        public void InspectionTableList()
        {
            ListInformation eventReport = new ListInformation();
            eventReport.userId = 70;
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:14491/api/XTGL/InspectionTableListLength", Newtonsoft.Json.JsonConvert.SerializeObject(eventReport));
        }
       
        public void GetMessageBySendPersonAPI()
        {
            MessageModel model = new MessageModel();
            model.RECEIVERID = 138;
            model.SENDERID = 70;
            model.MCOUNTS = 0;
            model.SendMessageName = "陈";
            string jsonresult = HttpHelper.SendDataByPost("http://localhost:14491/api/Message/GetAllMessageByRUIDAndSUID", Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }
    }
}
