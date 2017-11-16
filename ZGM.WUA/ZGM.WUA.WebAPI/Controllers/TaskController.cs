using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.WebAPI.Controllers
{
    public class TaskController : ApiController
    {
        TaskBLL taskBLL = new TaskBLL();

        /// <summary>
        /// /api/Task/GetTasksByPage?eventAddress=&sourceId=&bClassId=&sClassId=&levelNum=&createUserId=&skipNum=&takeNum=
        /// 分页获取案件列表
        /// 参数可选，参数为null则不考虑该参数
        /// </summary>
        /// <param name="eventAddress"></param>
        /// <param name="sourceId"></param>
        /// <param name="bClassId"></param>
        /// <param name="sClassId"></param>
        /// <param name="levelNum"></param>
        /// <param name="createUserId"></param>
        /// <param name="skipNum"></param>
        /// <param name="takeNum"></param>
        /// <returns></returns>
        public List<TaskModel> GetTasksByPage(string eventAddress, decimal? sourceId
            , decimal? bClassId, decimal? sClassId, decimal? levelNum
            , decimal? createUserId, decimal? skipNum, decimal? takeNum)
        {
            List<TaskModel> tasks = taskBLL.GetTasksByPage(eventAddress, sourceId
                , bClassId, sClassId, levelNum, createUserId, skipNum, takeNum);
            return tasks;
        }

        /// <summary>
        /// /api/Task/GetTasksCount?eventAddress=&sourceId=&bClassId=&sClassId=&levelNum=&createUserId=
        /// 获取案件数量
        /// 参数可选，参数为null则不考虑该参数
        /// </summary>
        /// <param name="eventAddress"></param>
        /// <param name="sourceId"></param>
        /// <param name="bClassId"></param>
        /// <param name="sClassId"></param>
        /// <param name="levelNum"></param>
        /// <param name="createUserId"></param>
        /// <returns></returns>
        public int GetTasksCount(string eventAddress, decimal? sourceId
            , decimal? bClassId, decimal? sClassId, decimal? levelNum
            , decimal? createUserId)
        {
            int count = taskBLL.GetTasksCount(eventAddress, sourceId, bClassId
                , sClassId, levelNum, createUserId);
            return count;
        }

        /// <summary>
        /// /api/Task/GetTaskByTaskId?taskId=
        /// 根据案件标识获取案件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskModel GetTaskByTaskId(string taskId)
        {
            TaskModel task = taskBLL.GetTaskByTaskId(taskId);
            return task;
        }

        /// <summary>
        /// /api/Task/GetSourceStatSum?startTime=&endTime=
        /// 获取案件来源统计总数
        /// 参数可为空，默认时间为今日
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_TaskSourceModel> GetSourceStatSum(DateTime? startTime, DateTime? endTime)
        {
            List<R_TaskSourceModel> rtss = taskBLL.GetSourceStatSum(startTime, endTime);
            return rtss;
        }

        /// <summary>
        /// /api/Task/GetSourceStatSub?startTime=&several=&hours=
        /// 获取今日案件来源分段统计数据
        /// 参数可为空，默认开始时间为7点，分14段，间隔1小时
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="several">分段数</param>
        /// <param name="hours">间隔小时数</param>
        /// <returns></returns>
        public List<R_TaskSourceModel> GetSourceStatSub(DateTime? startTime, decimal? several, decimal? hours)
        {
            List<R_TaskSourceModel> rtss = taskBLL.GetSourceStatSub(startTime, several, hours);
            return rtss;
        }

        /// <summary>
        /// /api/Task/GetTasksStatSub?startTime=&several=&hours=
        /// 获取今日案件分段统计
        /// 包含今日上报、今日办结、超期未处理
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="several"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public List<R_TaskCount> GetTasksStatSub(DateTime? startTime, decimal? several, decimal? hours)
        {
            List<R_TaskCount> result = taskBLL.GetTasksStatSub(startTime, several, hours);
            return result;
        }

        /// <summary>
        /// /api/Task/GetTasksStat?startTime=&endTime=
        /// 获取案件统计数据
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int[] GetTasksStat(DateTime? startTime, DateTime? endTime)
        {
            int[] result = taskBLL.GetTasksStat(startTime, endTime);
            return result;
        }

        /// <summary>
        /// /api/Task/GetFoundFilesByTaskId?taskId=
        /// 获取案件上报附件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<FileModel> GetFoundFilesByTaskId(string taskId)
        {
            List<FileModel> fms = taskBLL.GetFoundFilesByTaskId(taskId);
            foreach (FileModel item in fms)
            {
                //缩小
                string ZFSJSmallPath = ConfigurationManager.AppSettings["ZFSJSmallPath"].ToString();
                //压缩
                string ZFSJFilesPath = ConfigurationManager.AppSettings["ZFSJFilesPath"].ToString();
                //原图
                string ZFSJOriginalPath = ConfigurationManager.AppSettings["ZFSJOriginalPath"].ToString();
                item.FilePathUriSmall = ZFSJSmallPath + item.FilePath;
                item.FilePathUri = ZFSJFilesPath + item.FilePath;
                item.FilePathUriOriginal = ZFSJOriginalPath + item.FilePath;
            }
            return fms;
        }

        /// <summary>
        /// /api/Task/GetDisposeFilesByTaskId?taskId=
        /// 获取案件处置附件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<FileModel> GetDisposeFilesByTaskId(string taskId)
        {
            List<FileModel> dfs = taskBLL.GetDisposeFilesByTaskId(taskId);
            foreach (FileModel item in dfs)
            {
                //缩小
                string ZFSJSmallPath = ConfigurationManager.AppSettings["ZFSJSmallPath"].ToString();
                //压缩
                string ZFSJFilesPath = ConfigurationManager.AppSettings["ZFSJFilesPath"].ToString();
                //原图
                string ZFSJOriginalPath = ConfigurationManager.AppSettings["ZFSJOriginalPath"].ToString();
                item.FilePathUriSmall = ZFSJSmallPath + item.FilePath;
                item.FilePathUri = ZFSJFilesPath + item.FilePath;
                item.FilePathUriOriginal = ZFSJOriginalPath + item.FilePath;
            }
            return dfs;
        }

        /// <summary>
        /// /api/Task/GetTaskDisposes?taskId=
        /// 获取案件处理过程
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<TaskDisposeModel> GetTaskDisposes(string taskId)
        {
            List<TaskDisposeModel> tdms = taskBLL.GetTaskDisposes(taskId);
            return tdms;
        }
    }
}
