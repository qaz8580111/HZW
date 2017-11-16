using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using Taizhou.PLE.BLL.AppVersionBLLs;
using Taizhou.PLE.BLL.LogBLLs;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.BLL.ZFSJBLL;
//using Taizhou.PLE.BLL.CaseBLLs;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model.WebServiceModels;
using WebService.BLL;
//using Taizhou.PLE.BLL.CaseBLLs.DocBuildBLLs;
using System.IO;
using System.Text;
//using Taizhou.PLE.BLL.WorkFlowBLLs.XZSPBLLs;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPWorkflowModels.XZSPWorkflow;
using Taizhou.PLE.Model;
using System.Configuration;
using Taizhou.PLE.BLL.EnforceTheLaw;


namespace WebService
{
    /// <summary>
    /// PhoneWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PhoneWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account">登录账号</param>
        /// <param name="password">登录密码</param>
        /// <param name="imeiCode">手机IMEI号</param>
        /// <param name="phoneTime">手机时间</param>
        /// <param name="versionCode">应用版本号</param>
        /// <returns>登录错误对象</returns>
        [WebMethod]
        [XmlInclude(typeof(User))]
        [XmlInclude(typeof(AppVersion))]
        public SignInError SignIn(string account, string password,
            string imeiCode, string phoneTime, int versionCode)
        {
            SignInError signInError = null;
            AppVersion appVersion = null;
            User user = null;

            try
            {
                appVersion = AppVersionBLL.GetMaxAppVersion();
                user = UserBLL.SignIn(account, password);
            }
            catch (Exception e)
            {
                //if (File.Exists(Server.MapPath("LoginError.txt")))
                //{
                //    FileStream fs = new FileStream(Server.MapPath("LoginError.txt"), FileMode.Open, FileAccess.Write);
                //    StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                //    sw.WriteLine("登陆时间：" + phoneTime + "错误信息:" + e.Message + "系统触发时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm;ss"));
                //    sw.Close();
                //    fs.Close();
                //    sw.Dispose();
                //    fs.Dispose();
                //}
                string exceptionStr = LogBLL
                    .WriteSignInErrorLog(e, account, imeiCode, phoneTime);
                return signInError = new SignInError
                {
                    ErrorCode = 4,
                    ErrorMessage = e.Message,
                    ErrorData = exceptionStr
                };
            }

            DateTime tempTime = DateTime.Now;
            DateTime dt;

            if (DateTime.TryParse(phoneTime, out tempTime))
            {
                dt = tempTime;
            }
            else
            {
                return signInError = new SignInError
                {
                    ErrorCode = 1,
                    ErrorMessage = "手机时间格式不正确",
                    ErrorData = DateTime.Now.ToString()
                };
            }

            TimeSpan ts1 = new TimeSpan(dt.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();

            if (ts.TotalMinutes > 20)
            {
                signInError = new SignInError
                {
                    ErrorCode = 1,
                    ErrorMessage = "手机时间不正确",
                    ErrorData = DateTime.Now.ToString()
                };
            }
            else if (versionCode < appVersion.versionCode)
            {
                signInError = new SignInError
                {
                    ErrorCode = 2,
                    ErrorMessage = "应用需要升级",
                    ErrorData = appVersion
                };
            }
            else if (user == null)
            {
                signInError = new SignInError
                {
                    ErrorCode = 3,
                    ErrorMessage = "登录账号或密码错误",
                    ErrorData = null
                };
            }
            else if (user != null)
            {
                signInError = new SignInError
                {
                    ErrorCode = 0,
                    ErrorMessage = "用户登录成功",
                    ErrorData = user
                };
            }
            return signInError;
        }

        /// <summary>
        /// 上报违停案件
        /// </summary>
        /// <param name="carNo">车牌号</param>
        /// <param name="carType">车牌种类</param>
        /// <param name="caseTime">违法时间</param>
        /// <param name="address">违法地点</param>
        /// <param name="addressCode">地点编号</param>
        /// <param name="documentCode">抄告单号</param>
        /// <param name="userID">采集人</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="picture1">图片1</param>
        /// <param name="picture2">图片2</param>
        /// <param name="picture3">图片3</param>
        /// <param name="picture4">图片4</param>
        /// <returns>是否成功上报</returns>
        [WebMethod]
        public bool SubmitIPCase(string carNo, string carType, string caseTime,
            string address, string addressCode, string documentCode, string WTUserID,
            string WTUnitID, string lon, string lat, byte[] picture1,
            byte[] picture2, byte[] picture3, byte[] picture4)
        {
            IPCase ipCase = new IPCase
            {
                carNo = carNo,
                carType = carType,
                caseTime = caseTime,
                address = address,
                addressCode = addressCode,
                documentCode = documentCode,
                WTUserID = WTUserID,
                WTUnitID = WTUnitID,
                lon = lon,
                lat = lat,
                picture1 = picture1,
                picture2 = picture2,
                picture3 = picture3,
                picture4 = picture4
            };
            //记录该接口被调用日志
            LogBLL.WriteCalledLog(ipCase);

            try
            {
                IPCaseBLL.SaveIPCase(ipCase);
                return true;
            }
            catch (Exception e)
            {
                LogBLL.WriteErrorLog(e, ipCase);
                return false;
            }
        }

        /// <summary>
        /// 根据所属单位及经纬度获取地点编号及地点描述
        /// </summary>
        /// <param name="ssdw">所属单位</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <returns>地点编号及地点描述</returns>
        [WebMethod]
        public string GetDDBHByLonAndLat(string ssdw, string lon, string lat)
        {
            return IPCaseBLL.GetDDBHByLonAndLat(ssdw, lon, lat);
        }

        /// <summary>
        /// 上报实时位置
        /// </summary>
        /// <param name="userID">执法队员标识</param>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="positionTime">定位时间</param>
        [WebMethod]
        public bool SubmitUserPosition(string userPositions)
        {
            if (string.IsNullOrWhiteSpace(userPositions))
            {
                return false;
            }

            List<UserPosition> list = JsonHelper
                    .JsonDeserialize<List<UserPosition>>(userPositions);

            try
            {
                ZFSJWebServiceBLL.SubmitUserPosition(list);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 上报执法事件
        /// </summary>
        /// <param name="title">事件标题</param>
        /// <param name="address">事件地址</param>
        /// <param name="content">事件内容</param>
        /// <param name="mainClassID">问题大类标识</param>
        /// <param name="sunClassID">问题小类标识</param>
        /// <param name="discoverTime">发现时间</param>
        /// <param name="mapLocation">地图位置（经度|纬度）</param>
        /// <param name="eventPhoto1">事件照片一</param>
        /// <param name="eventPhoto2">事件照片二</param>
        /// <param name="eventPhoto3">事件照片三</param>
        /// <param name="reportTime">上报时间</param>
        /// <param name="userID">上报人员</param>
        /// <returns>是否成功上报</returns>
        [WebMethod]
        public int SubmitEvent(string title, string address, string content,
            int mainClassID, int sunClassID, string discoverTime,
            string mapLocation, byte[] eventPhoto1, byte[] eventPhoto2,
            byte[] eventPhoto3, string reportTime, int userID, string PhoneID)
        {
            // 120.11899645|30.29373176
            string mapLocationMercator = "-1.0|-1.0";
            //mapLocation = "121.500149421418|28.7097760650606";
            if (!string.IsNullOrEmpty(mapLocation))
            {

                string[] mapLocationSplit = mapLocation.Split('|');
                if (mapLocationSplit != null && mapLocationSplit.Length > 0)
                {
                    double x, y;
                    WGS84ToMercator(Convert.ToDouble(mapLocationSplit[0]),
                        Convert.ToDouble(mapLocationSplit[1]), out x, out y);
                    mapLocationMercator = x.ToString() + "|" + y.ToString();
                }
            }

            EnforceLawEvent entity = new EnforceLawEvent
            {
                title = title,
                address = address,
                content = content,
                mainClassID = mainClassID,
                sunClassID = sunClassID,
                discoverTime = discoverTime,
                //mapLocation = mapLocation,
                mapLocation = mapLocationMercator,
                eventPhoto1 = eventPhoto1,
                eventPhoto2 = eventPhoto2,
                eventPhoto3 = eventPhoto3,
                reportTime = reportTime,
                userID = userID,
                PhoneID = PhoneID,
                regionID = Convert.ToInt32(ConfigurationManager.AppSettings["ZHRYBH"])

            };

            try
            {
                return ZFSJWebServiceBLL.SubmitEvent(entity);
            }
            catch (Exception e)
            {
                LogBLL.WriteErrorLog(e, entity);
                return 2;
            }
        }

        /// <summary>
        /// 根据执法队员标识获取待处理事件
        /// </summary>
        /// <param name="userID">执法队员标识</param>
        /// <param name="updateTime">更新时间</param>
        /// <returns>待处理事件数组</returns>
        [WebMethod]
        public string QueryPendingEvents
            (int userID, string updateTime)
        {
            return ZFSJWebServiceBLL
                .QueryPendingEvents(userID, updateTime);
        }


        /// <summary>
        /// 根据执法队员标识获取待处理事件
        /// </summary>`
        /// <param name="userID">执法队员标识</param>
        /// <param name="updateTime">更新时间</param>
        /// <returns>待处理事件数组</returns>
        [WebMethod]
        public int QueryPendingEventsCount
            (int userID, string updateTime)
        {
            return ZFSJWebServiceBLL
                .QueryPendingEventsCount(userID, updateTime);
        }

        ///// <summary>
        ///// 根据流程实例标识判断该流程是否已处理
        ///// </summary>
        ///// <param name="wiid">流程实例标识</param>
        ///// <returns>该流程是否已处理</returns>
        //[WebMethod(Description = "true:已处理，false:未处理")]
        //public int IsProcess(string wiid)
        //{
        //    return ZFSJWebServiceBLL.GetStatusByWIID(wiid);
        //}

        /// <summary>
        /// 根据活动环节标识判断该活动环节是否已处理
        /// </summary>
        /// <param name="wiid">活动环节实例标识</param>
        /// <returns>该活动环节是否已处理</returns>
        [WebMethod(Description = "2:已处理，1:未处理，0:为找到活动")]
        public int IsProcessByAIID(string aiid)
        {
            return ZFSJWebServiceBLL.GetStatusByAIID(aiid);
        }



        /// <summary>
        /// 上报处理事件
        /// </summary>
        /// <param name="wiid">流程实例标识</param>
        /// <param name="processWayID">处理方式</param>
        /// <param name="investigateWayID">查处方式</param>
        /// <param name="caseCode">案卷编号</param>
        /// <param name="opinion">处理意见</param>
        /// <param name="processedPhoto1">处理后照片一</param>
        /// <param name="processedPhoto2">处理后照片二</param>
        /// <param name="processedPhoto3">处理后照片三</param>
        /// <param name="processTime">处理时间</param>
        /// <param name="userID">执法队员标识</param>
        /// <returns>是否成功上报</returns>
        [WebMethod]
        public int SubmitDisposeEvent(string wiid, int processWayID,
            int investigateWayID, string caseCode, string opinion,
            byte[] processedPhoto1, byte[] processedPhoto2,
            byte[] processedPhoto3, string processTime, int userID, string AIID)
        {
            ProcessEvent entity = new ProcessEvent
            {
                wiid = wiid,
                processWayID = processWayID,
                investigateWayID = investigateWayID,
                caseCode = caseCode,
                opinion = opinion,
                processedPhoto1 = processedPhoto1,
                processedPhoto2 = processedPhoto2,
                processedPhoto3 = processedPhoto3,
                processTime = processTime,
                userID = userID,
            };

            try
            {
                return ZFSJWebServiceBLL.SubmitDisposeEvent(entity, AIID);

            }
            catch (Exception e)
            {
                LogBLL.WriteErrorLog(e, entity);
                return 2;
            }
        }

        /// <summary>
        /// 添加简易处理案件
        /// </summary>
        /// <param name="simpleCase">json格式数据</param>
        /// <returns></returns>
        [WebMethod]
        public int SubmitSimpleCase(string simpleCase)
        {
            //try
            //{
            //    SimpleCase simplecase = JsonHelper
            //        .JsonDeserialize<SimpleCase>(simpleCase);
            //    return SimpleCaseWebServiceBLL.SubmitSimpleCase(simplecase);
            //}
            //catch (Exception)
            //{
            //    return 2;
            //}

            return 0;
        }

        /// <summary>
        ///  暂存一般处理案件
        /// </summary>
        /// <param name="phoneWorkflow">一般案件第一环节数据（json）</param>
        /// <param name="docStr">一般案件第一环节文书数据（json）</param>
        /// <returns></returns>
        [WebMethod]
        public bool SavePhoneCaseWorkflow(string phoneWorkflow, string PhoneDocStr)
        {
            try
            {
                //PhoneViewModel1 phoneViewModel1 = new PhoneViewModel1();
                //if (!string.IsNullOrWhiteSpace(phoneWorkflow))
                //{
                //    //反序列化一般案件第一环节数据
                //    phoneViewModel1 = JsonHelper
                //       .JsonDeserialize<PhoneViewModel1>(phoneWorkflow);
                //    PhoneCaseWorkflow phoneCaseWorkflow = new PhoneCaseWorkflow(phoneViewModel1.UserID);
                //    phoneViewModel1.WIID = phoneCaseWorkflow.CaseForm.WIID;
                //    phoneViewModel1.AIID = phoneCaseWorkflow.CaseForm.FinalForm.Form101.ID;
                //    PhoneCaseWorkflow.SavePhoneWorkflow(phoneViewModel1);
                //}
                //if (!string.IsNullOrWhiteSpace(PhoneDocStr))
                //{
                //    //反序列化一般案件第一环节文书数据
                //    List<PhoneDoc> PhoneDocList = JsonHelper.JsonDeserialize<List<PhoneDoc>>(PhoneDocStr);
                //    PhoneCaseWorkflow phonecaseworkflow = new PhoneCaseWorkflow(phoneViewModel1.WIID);
                //    string str = JsonHelper.JsonSerializer<List<PhoneDoc>>(PhoneDocList);
                //    string WIcode = phonecaseworkflow.CaseForm.WICode;
                //    DocWebServiceBLL.SaveDoc(PhoneDocList, WIcode, phoneViewModel1.UserID, phoneViewModel1.WIID, phoneViewModel1.AIID);
                //}
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #region 坐标转换

        private void WGS84ToMercator(double lon, double lat, out double x, out double y)
        {
            x = lon * 20037508.34 / 180;
            y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y = y * 20037508.34 / 180;
        }

        #endregion


        /// <summary>
        /// 行政审批待办任务
        /// </summary>
        /// <param name="UserID">用户编号</param>
        /// <returns>代办任务列表</returns>
        [WebMethod]
        public string GetPhoneApprova(string UserID, string createtime)
        {
            //return JsonHelper.JsonSerializer<List<WebServiceXZSPModel>>(XZSPWebserviceBLL.GetXZSPList(UserID, createtime));
            return "";
        }


        [WebMethod]
        public int SavePhoneApprova(string Json)
        {
            string fileErrorName = "C:\\错误日志\\" + "xzsp-error" + DateTime.Now.Ticks + ".txt";
            using (FileStream fileStream = new FileStream(fileErrorName, FileMode.CreateNew))
            {
                byte[] buffer = Encoding.Default.GetBytes(Json);
                fileStream.Write(buffer, 0, buffer.Length);

            }
            int anwer = 0;
            //anwer = XZSPWebserviceBLL.SavePhoneApprova(Json);
            return anwer;
        }

        /// <summary>
        /// 更新app包根据用户编号
        /// </summary>
        /// <param name="VersionID">版本编号</param>
        /// <returns>下载地址</returns>
        [WebMethod]
        public AppVersion GetVersion(int VersionID)
        {
            return AppVersionBLL.GetMaxAppVersion();
        }

        /// <summary>
        /// 创建日常督查
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="classbig">大类</param>
        /// <param name="classsmall">小类</param>
        /// <param name="userid">用户编号</param>
        /// <param name="grade">分数</param>
        /// <param name="geometry">地图位置</param>
        /// <param name="eventcontent">事件内容</param>
        /// <param name="address">地址</param>
        /// <param name="img1">图片1</param>
        /// <param name="img2">图片2</param>
        /// <param name="img3">图片3</param>
        /// <param name="PhoneID">唯一标示（手机端UUID）</param>
        /// <returns>0失败，1重复，其它为成功</returns>
        [WebMethod]
        public decimal AddRcdcEvent(string title, int classbig, int classsmall, int userid, int grade, string geometry, string eventcontent, string address, byte[] img1, byte[] img2, byte[] img3, string PhoneID)
        {
            if (RCDCEVENTBLL.GetRCDCEventByGuid(PhoneID) > 0)
            {
                return 1;
            }
            RCDCEVENT rcdcevent = new RCDCEVENT();
            rcdcevent.EVENTTITLE = title;
            rcdcevent.CLASSBID = classbig;
            rcdcevent.CLASSSID = classsmall;
            rcdcevent.CREATETIME = DateTime.Now;
            rcdcevent.USERID = userid;
            rcdcevent.GRADE = grade;
            rcdcevent.GEOMETRY = geometry;
            rcdcevent.EVENTCONTENT = eventcontent;
            rcdcevent.GUIDONLY = PhoneID;
            rcdcevent.EVENTSOURCE = "16";
            rcdcevent.EVENTADDRESS = address;
            string picture = "";
            if (img1 != null)
            {
                picture += WebServiceUtility.RCDCFileUpload(img1, "jpg").Replace("\\", "/");
            }
            if (img1 != null)
            {
                picture += picture == "" ? WebServiceUtility.RCDCFileUpload(img2, "jpg").Replace("\\", "/") : ";" + WebServiceUtility.RCDCFileUpload(img2, "jpg").Replace("\\", "/");
            }
            if (img1 != null)
            {
                picture += picture == "" ? WebServiceUtility.RCDCFileUpload(img3, "jpg").Replace("\\", "/") : ";" + WebServiceUtility.RCDCFileUpload(img3, "jpg").Replace("\\", "/");
            }
            rcdcevent.PICTURES = picture;
            if (RCDCEVENTBLL.AddRcdcevent(rcdcevent) > 0)
            {
                return rcdcevent.EVENTID;
            }
            return 0;
        }

        /// <summary>
        /// 日常督查手机端上传图片
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string SaveImg(string img)
        {
            DateTime dt = DateTime.Now;

            string fileName = Guid.NewGuid().ToString("N") + ".jpg";

            string originalPath = Path.Combine(ConfigurationManager.AppSettings["ZFSJOriginalPath"], dt.ToString("yyyyMMdd"));
            string destinatePath = Path.Combine(ConfigurationManager.AppSettings["ZFSJFilesPath"], dt.ToString("yyyyMMdd"));

            string sFilePath = Path.Combine(originalPath, fileName);
            string dFilePath = Path.Combine(destinatePath, fileName);

            if (System.IO.File.Exists(sFilePath))
            {
                System.IO.File.Delete(sFilePath);
            }

            if (System.IO.File.Exists(dFilePath))
            {
                System.IO.File.Delete(dFilePath);
            }
            MemoryStream ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(img));
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            im.Save(sFilePath);

            ImageCompress.CompressPicture
                        (sFilePath, dFilePath, Convert.ToInt32(ConfigurationManager
                .AppSettings["ZFSJPicSize"]), 0, "W");
            sFilePath = sFilePath.Replace("\\", "/");
            sFilePath = sFilePath.Replace(ConfigurationManager.AppSettings["ZFSJOriginalPathTH"], "/");
            return sFilePath;
        }

        /// <summary>
        /// 日常督查手机端上传图片
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string SaveImg(byte[] img)
        {
            DateTime dt = DateTime.Now;
            string fileName = Guid.NewGuid().ToString("N") + ".jpg";

            string originalPath = Path.Combine(ConfigurationManager.AppSettings["ZFSJOriginalPath"], dt.ToString("yyyyMMdd"));
            string destinatePath = Path.Combine(ConfigurationManager.AppSettings["ZFSJFilesPath"], dt.ToString("yyyyMMdd"));

            string sFilePath = Path.Combine(originalPath, fileName);
            string dFilePath = Path.Combine(destinatePath, fileName);

            if (System.IO.File.Exists(sFilePath))
            {
                System.IO.File.Delete(sFilePath);
            }

            if (System.IO.File.Exists(dFilePath))
            {
                System.IO.File.Delete(dFilePath);
            }
            MemoryStream ms = new MemoryStream(img);
            System.Drawing.Image im = System.Drawing.Image.FromStream(ms);
            im.Save(sFilePath);

            ImageCompress.CompressPicture
                        (sFilePath, dFilePath, Convert.ToInt32(ConfigurationManager
                .AppSettings["ZFSJPicSize"]), 0, "W");
            sFilePath = sFilePath.Replace("\\", "/");
            sFilePath = sFilePath.Replace(ConfigurationManager.AppSettings["ZFSJOriginalPathTH"], "/");
            return fileName;
        }
    }
}
