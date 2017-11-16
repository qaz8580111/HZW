using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Xml.Serialization;
using ZHCGWebAPI.Helper;

namespace ZHCGWebAPI.Controllers
{
    public class ZHCGController : ApiController
    {
        ZHCGBLL bll = new ZHCGBLL();

        /// <summary>
        /// /api/ZHCG/GetTasks?taskId=&streetCode=
        /// 获取案件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<ZHCGTask> GetTasks(decimal? taskId, string streetCode)
        {
            List<ZHCGTask> tasks = bll.GetTasks(taskId, streetCode);
            return tasks;
        }

        /// <summary>
        /// /api/ZHCG/GetMedias?mediaId=
        /// 获取附件
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public List<ZHCGMedia> GetMedias(decimal? mediaId)
        {
            List<ZHCGMedia> medias = bll.GetMedias(mediaId);
            return medias;
        }

        /// <summary>
        /// 案件处置接口
        /// </summary>
        /// <param name="disposal"></param>
        /// <param name="medias"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public string TaskFeedBack(ZHCGCLModel hcgclModel)
        {
            bool isSucess = false;
            egovaService.EgovaServiceSoapClient client = new egovaService.EgovaServiceSoapClient();
            User yh = JsonConvert.DeserializeObject<User>(hcgclModel.user);
            ZHCGDisposal dis = JsonConvert.DeserializeObject<ZHCGDisposal>(hcgclModel.disposal);
            List<ZHCGMedia> mds = JsonConvert.DeserializeObject<List<ZHCGMedia>>(hcgclModel.medias);
            if (dis != null)
            {
                try
                {
                    int result = bll.AddDisposal(dis);
                    if (result != 1)
                    {
                        //记录日志

                        return "处置申请失败，请重新申请";
                    }

                    if (mds != null && mds.Count() != 0)
                    {
                        for (int i = 0; i < mds.Count(); i++)
                        {
                            int result1 = bll.AddMedia(mds[0]);
                            if (result1 != 1)
                            {
                                //记录日志
                            }
                        }
                    }

                    string xml;
                    switch (dis.Type)
                    {
                        //处置
                        case "3":
                            StringBuilder xmlResult = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                            xmlResult.Append("<Request>");
                            xmlResult.AppendFormat(" <function name='TaskFeedBack'/>");
                            xmlResult.AppendFormat("<params>");
                            xmlResult.AppendFormat("<TaskNum>{0}</TaskNum>", dis.TaskNum);
                            xmlResult.AppendFormat("<FeedbackDate>{0}</FeedbackDate>", dis.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                            xmlResult.AppendFormat("<FeedbackMemo>{0}</FeedbackMemo>", dis.Memo);
                            xmlResult.AppendFormat("<FeedbackOpter>{0}</FeedbackOpter>", dis.UnitId);
                            if (mds != null && mds.Count > 0)
                            {
                                //xmlResult.AppendFormat("<FeedbackPictures num='{0}'>", mds.Count);
                                //foreach (FI_ZHCGMEDIAS pic in mds)
                                //{
                                //    xmlResult.AppendFormat("<picture name='{0}'url='{1}'/>", pic.NAME, pic.URL);
                                //}
                                //xmlResult.AppendFormat("</FeedbackPictures >");
                            }
                            else
                            {
                                xmlResult.AppendFormat("<FeedbackPictures num='0'></FeedbackPictures >");
                            }
                            xmlResult.Append("</params>");
                            xmlResult.Append("</Request >");
                            xml = xmlResult.ToString();
                            break;
                        //延期
                        case "5":
                        //挂账
                        case "6":
                            string type = dis.Type == "5" ? "1" : "2";
                            StringBuilder xmlResult1 = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                            xmlResult1.Append("<Request>");
                            xmlResult1.AppendFormat(" <function name=\"ApplyAccredit\"/>");
                            xmlResult1.AppendFormat("<params>");
                            xmlResult1.AppendFormat("<TaskNum>{0}</TaskNum>", dis.TaskNum);
                            xmlResult1.AppendFormat("<ApplayOpter>{0}</ApplayOpter>", dis.UnitId);
                            xmlResult1.AppendFormat("<ApplyFlag>{0}</ApplyFlag>", type);
                            xmlResult1.AppendFormat("<ApplyDelayInfo>{0}</ApplyDelayInfo>", dis.Info);
                            xmlResult1.AppendFormat("<ApplyDate>{0}</ApplyDate>", dis.CreateTime);
                            xmlResult1.AppendFormat("<ApplyMemo>{0}</ApplyMemo>", dis.Memo);
                            xmlResult1.Append("</params>");
                            xmlResult1.Append("</Request >");
                            xml = xmlResult1.ToString();
                            break;
                        //回退
                        case "4":
                            StringBuilder xmlResult3 = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                            xmlResult3.Append("<Request>");
                            xmlResult3.AppendFormat(" <function name='TaskRollBack'/>");
                            xmlResult3.AppendFormat("<params>");
                            xmlResult3.AppendFormat("<TaskNum>{0}</TaskNum>", dis.TaskNum);
                            xmlResult3.AppendFormat("<RollbackOpter>{0}</RollbackOpter>", dis.UnitId);
                            xmlResult3.AppendFormat("<RollbackDate>{0}</RollbackDate>", dis.CreateTime.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                            xmlResult3.AppendFormat("<RollbackMemo>{0}</RollbackMemo>", dis.Memo);
                            xmlResult3.Append("</params>");
                            xmlResult3.Append("</Request>");
                            xml = xmlResult3.ToString();
                            break;
                        default:
                            xml = "";
                            break;
                    }
                    if (xml != "")
                    {
                        string str = client.process("", "", "", xml);
                        ZHCGResult zr;
                        StringReader rdr = new StringReader(str);
                        XmlSerializer serializer = new XmlSerializer(typeof(ZHCGResult));
                        zr = (ZHCGResult)serializer.Deserialize(rdr);

                        //授权申请成功
                        if ("0".Equals(zr.ResultCode))
                        {
                            isSucess = true;
                            ZHCGTask task = bll.GetTaskByTaskNum(dis.TaskNum).Take(1).SingleOrDefault();
                            switch (dis.Type)
                            {
                                case "3":
                                    int result3 = bll.UpdateZHCGTask(task.TASKID, dis.Type, dis.CreateTime, dis.Memo, yh.userID, yh.userName);
                                    break;
                                case "5":
                                case "6":
                                    int result4 = bll.UpdateZHCGTask(task.TASKID, dis.Type, null, null, null, null);
                                    break;
                                case "4":
                                    int result5 = bll.UpdateZHCGTask(task.TASKID, dis.Type, null, null, null, null);
                                    int result6 = bll.UpdateMedia(task.TASKNUM, "0");
                                    break;
                            }

                            LogHelper.WriteLog(typeof(ZHCGController)
                                    , "任务号：" + dis.TaskNum
                                    + ";授权申请成功"
                                    + "申请的状态为：" + dis.Type);
                            return "1";
                        }
                        //授权申请失败
                        else
                        {
                            LogHelper.WriteLog(typeof(ZHCGController)
                                    , "任务号：" + dis.TaskNum
                                    + ";授权申请失败;"
                                    + "申请的状态为：" + dis.Type
                                    + "错误信息：" + JsonConvert.SerializeObject(zr));
                            return "-6|" + zr.ResultDesc;
                        }
                    }
                    else
                    {
                        //申请类型错误
                        LogHelper.WriteLog(typeof(ZHCGController)
                                , "任务号：" + dis.TaskNum
                                + ";申请类型错误"
                                + "申请的状态为：" + dis.Type);
                        return "-2";
                    }
                }
                catch (Exception e)
                {
                    if (isSucess)
                    {
                        //授权申请成功，案件状态修改失败,发生异常
                        LogHelper.WriteLog(typeof(ZHCGController)
                                    , "任务号：" + dis.TaskNum
                                    + ";授权申请成功，案件状态修改失败,发生异常;"
                                    + "申请的状态为：" + dis.Type
                                    + "申请的内容为：" + dis.Info
                                    + "异常信息：" + e.Message);
                        return "-4|" + e.Message;
                    }
                    else
                    {

                        //授权申请失败，发生异常
                        LogHelper.WriteLog(typeof(ZHCGController)
                                    , "任务号：" + dis.TaskNum
                                    + ";授权申请失败,发生异常;"
                                    + "申请的状态为：" + dis.Type +
                                    "异常信息：" + e.Message);
                        return "-5|" + e.Message;
                    }
                }
            }
            else
            {
                //案件处置主体无效
                LogHelper.WriteLog(typeof(ZHCGController), ";案件处置主体无效");
                return "-1";
            }
        }

        /// <summary>
        /// 处理案件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string TaskDeal(ZHCGDealModel model)
        {
            LogHelper.WriteLog(typeof(ZHCGController), "处理案件[已经进入方法]");
            if (model == null || (model != null && string.IsNullOrEmpty(model.RequestData)))
            {
                return "null";
            }
            LogHelper.WriteLog(typeof(ZHCGController), "处理案件[参数进入]：案件编号：" + model.RequestData);
            string RequestData = model.RequestData;
            RequestData = HttpUtility.HtmlDecode(RequestData);
            RequestData = RequestData.Replace("$", "\"").Replace("||", "+");
            ZHCGDealModel resultModel = JsonConvert.DeserializeObject<ZHCGDealModel>(RequestData);

            if (resultModel != null)
            {
                LogHelper.WriteLog(typeof(ZHCGController), "处理案件[开始]：案件编号：" + resultModel.TaskNum
               + "处理时间：" + resultModel.DealTime
               + "处理内容：" + resultModel.Content
               + "处理人员：" + resultModel.UserId + "==" + resultModel.UserName
               + "处理部门：" + resultModel.DeptId + "==" + resultModel.DeptName);

                try
                {
                    ZHCGDisposal dis = new ZHCGDisposal()
                    {
                        TaskNum = resultModel.TaskNum,
                        CreateTime = resultModel.DealTime,
                        Info = resultModel.Content,
                        Memo = resultModel.Content,
                        Type = "3",
                        UnitId = resultModel.DeptId
                    };
                    LogHelper.WriteLog(typeof(ZHCGController), "处理案件[插入环节]：案件编号：" + resultModel.TaskNum);
                    //更新过程表
                    int index = bll.AddDisposal(dis);
                    if (resultModel.MediaList != null && resultModel.MediaList.Count > 0)
                    {
                        for (int i = 0; i < resultModel.MediaList.Count; i++)
                        {
                            //更新图片表
                            bll.AddMedia(resultModel.MediaList[i]);
                            LogHelper.WriteLog(typeof(ZHCGController), "处理案件[插入处理图片" + i + "]：案件编号：" + resultModel.TaskNum);
                        }
                    }
                    //更新事件表
                    bll.UpdateZHCGTask(Convert.ToDecimal(resultModel.TaskNum), "3", resultModel.DealTime, resultModel.Content, resultModel.UserId, resultModel.UserName, resultModel.DeptId);
                    LogHelper.WriteLog(typeof(ZHCGController), "处理案件[更新事件状态]：案件编号：" + resultModel.TaskNum);
                    return "ok";
                }
                catch (Exception)
                {
                    LogHelper.WriteLog(typeof(ZHCGController), "处理案件[失败]：案件编号：" + resultModel.TaskNum);
                    return "err";
                }
            }
            else
            {
                LogHelper.WriteLog(typeof(ZHCGController), "处理案件[无数据传入]：案件编号：" + model.RequestData);
                return "null";
            }
        }
    }
}
