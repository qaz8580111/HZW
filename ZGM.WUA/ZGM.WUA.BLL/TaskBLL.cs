using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;
using ZGM.WUA.DAL;

namespace ZGM.WUA.BLL
{
    public class TaskBLL
    {
        TaskDAL taskDAL = new TaskDAL();

        /// <summary>
        ///  分页获取案件列表
        ///  参数可选，参数为null则不考虑该参数
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
            DateTime startTime = DateTime.Today;
            DateTime endTime = startTime.AddDays(1);
            IQueryable<TaskModel> tasks = taskDAL.GetTasksByPage(eventAddress, sourceId
                , bClassId, sClassId, levelNum, createUserId
                , startTime, endTime, skipNum, takeNum);
            return tasks.ToList();
        }

        /// <summary>
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
            DateTime startTime = DateTime.Today;
            DateTime endTime = startTime.AddDays(1);
            int count = taskDAL.GetTasksCount(eventAddress, sourceId, bClassId
                , sClassId, levelNum, createUserId, startTime, endTime);
            return count;
        }

        /// <summary>
        /// 根据案件标识获取案件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskModel GetTaskByTaskId(string taskId)
        {
            TaskModel task = taskDAL.GetTaskByTaskId(taskId);
            return task;
        }

        /// <summary>
        /// 获取案件来源统计总数
        /// 默认时间为今日
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<R_TaskSourceModel> GetSourceStatSum(DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Today;
            if (endTime == null)
                endTime = DateTime.Today.AddDays(1);
            List<R_TaskSourceModel> rtss = taskDAL.GetSourceStatSum(startTime, endTime);
            return rtss;
        }

        /// <summary>
        /// 获取今日案件来源分段统计数据
        /// 参数可为空，默认开始时间为7点，分14段，间隔1小时
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="several">分段数</param>
        /// <param name="hours">间隔小时数</param>
        /// <returns></returns>
        public List<R_TaskSourceModel> GetSourceStatSub(DateTime? startTime, decimal? several, decimal? hours)
        {
            if (startTime == null)
                startTime = DateTime.Today.AddHours(7);
            if (several == null)
                several = 14;
            if (hours == null)
                hours = 1;
            DateTime endTime = startTime.Value.AddHours(Convert.ToDouble((several + 1) * hours));

            List<R_TaskSourceModel> rtss = taskDAL.GetSourceStatSub(startTime, endTime, several, hours);
            return rtss;
        }

        /// <summary>
        /// 获取今日案件分段统计
        /// 包含今日上报、今日办结、超期未处理
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="several"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public List<R_TaskCount> GetTasksStatSub(DateTime? startTime, decimal? several, decimal? hours)
        {
            if (startTime == null)
                startTime = DateTime.Today.AddHours(7);
            if (several == null)
                several = 14;
            if (hours == null)
                hours = 1;

            DateTime endTime = startTime.Value.AddHours(Convert.ToDouble((several + 1) * hours));

            List<R_TaskCount> result = taskDAL.GetTasksStatSub(startTime, endTime, several, hours);
            return result;
        }

        /// <summary>
        /// 获取案件统计
        /// 今日上报，今日紧急，今日办结，今日超期未处理
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public int[] GetTasksStat(DateTime? startTime, DateTime? endTime)
        {
            if (startTime == null)
                startTime = DateTime.Today;
            if (endTime == null)
                endTime = startTime.Value.AddDays(1);
            int foundCount = taskDAL.GetFoundCount(startTime, endTime);
            int exigencyCount = taskDAL.GetExigencyCount(startTime, endTime);
            int finishCount = taskDAL.GetFinishCount(startTime, endTime);
            int overdueCount = taskDAL.GetOverdueCount(startTime, endTime);
            int[] result = new int[] { foundCount, exigencyCount, finishCount, overdueCount };
            return result;
        }

        /// <summary>
        /// 获取案件上报附件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<FileModel> GetFoundFilesByTaskId(string taskId)
        {
            List<FileModel> result = taskDAL.GetFoundFilesByTaskId(taskId);
            return result;
        }

        /// <summary>
        /// 获取案件处置附件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<FileModel> GetDisposeFilesByTaskId(string taskId)
        {
            List<FileModel> result = taskDAL.GetDisposeFilesByTaskId(taskId);
            return result;
        }

        /// <summary>
        /// 获取案件处理过程
        /// </summary>
        /// <param name="ZFSJId"></param>
        /// <param name="WFDID"></param>
        /// <returns></returns>
        public List<TaskDisposeModel> GetTaskDisposes(string ZFSJId)
        {
            //string foundWFDID = "20160407132010001";
            //string disptchWFDID = "20160407132010002";
            //string disposeWFDID = "20160407132010003";
            //string checkWFDID = "20160407132010004";
            //string clearWFDID = "20160407132010005";
            //TaskDisposeModel disptch = null;
            //TaskDisposeModel dispose = null;
            //TaskDisposeModel check = null;
            //TaskDisposeModel clear = null;
            List<TaskDisposeModel> tdms = new List<TaskDisposeModel>();
            tdms = taskDAL.GetTaskDispose(ZFSJId);
            //if (found != null)
            //    disptch = taskDAL.GetTaskDispose(ZFSJId, disptchWFDID);
            //if (disptch != null)
            //    dispose = taskDAL.GetTaskDispose(ZFSJId, disposeWFDID);
            //if (dispose != null)
            //    check = taskDAL.GetTaskDispose(ZFSJId, checkWFDID);
            //if (check != null)
            //    clear = taskDAL.GetTaskDispose(ZFSJId, clearWFDID);

            //List<TaskDisposeModel> tdms = new List<TaskDisposeModel>();
            //if (found != null)
            //    tdms.Add(found);
            //if (disptch != null)
            //    tdms.Add(disptch);
            //if (dispose != null)
            //    tdms.Add(dispose);
            //if (check != null)
            //    tdms.Add(check);
            //if (clear != null)
            //    tdms.Add(clear);
            return tdms;
        }
    }
}
