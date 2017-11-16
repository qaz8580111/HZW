using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ZHCGBLL
    {
        ZHCGDAL dal = new ZHCGDAL();

        /// <summary>
        /// 获取比taskId大的100条案件
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public List<ZHCGTask> GetTasks(decimal? taskId, string streetCode)
        {
            IQueryable<ZHCGTask> result = dal.GetTasks(taskId, streetCode);
            return result.ToList();
        }

        /// <summary>
        /// 根据案件标识获取案件
        /// </summary>
        /// <param name="taskNum"></param>
        /// <returns></returns>
        public List<ZHCGTask> GetTaskByTaskNum(string taskNum)
        {
            IQueryable<ZHCGTask> result = dal.GetTaskByTaskNum(taskNum);
            return result.ToList();
        }

        /// <summary>
        /// 获取比mediaId大的1条附件
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public List<ZHCGMedia> GetMedias(decimal? mediaId)
        {
            IQueryable<ZHCGMedia> result = dal.GetMedias(mediaId);
            return result.ToList();
        }

        /// <summary>
        /// 添加案件处理申请
        /// </summary>
        /// <param name="disposal"></param>
        /// <returns>1:成功;0:失败</returns>
        public int AddDisposal(ZHCGDisposal disposal)
        {
            int result = dal.AddDisposal(disposal);
            return result;
        }

        /// <summary>
        /// 添加附件照片
        /// </summary>
        /// <param name="media"></param>
        /// <returns>1:成功;0:失败</returns>
        public int AddMedia(ZHCGMedia media)
        {
            int result = dal.AddMedia(media);
            return result;
        }

        /// <summary>
        /// 更新案件信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="state"></param>
        /// <param name="disposeDate"></param>
        /// <param name="disposeMemo"></param>
        /// <param name="disposeId"></param>
        /// <param name="disposeName"></param>
        /// <returns></returns>
        public int UpdateZHCGTask(decimal? taskId, string state, DateTime? disposeDate, string disposeMemo, string disposeId, string disposeName)
        {
            int result = dal.UpdateZHCGTask(taskId, state, disposeDate, disposeMemo, disposeId, disposeName);
            return result;
        }
        /// <summary>
        /// 更新案件信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="state"></param>
        /// <param name="disposeDate"></param>
        /// <param name="disposeMemo"></param>
        /// <param name="disposeId"></param>
        /// <param name="disposeName"></param>
        /// <returns></returns>
        public int UpdateZHCGTask(decimal? taskId, string state, DateTime? disposeDate, string disposeMemo, string disposeId, string disposeName, string DEALUNIT)
        {
            int result = dal.UpdateZHCGTask(taskId, state, disposeDate, disposeMemo, disposeId, disposeName, DEALUNIT);
            return result;
        }

        
        /// <summary>
        /// 更新附件状态
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="isUsed"></param>
        /// <returns></returns>
        public int UpdateMedia(string taskNum, string isUsed)
        {
            int result = dal.UpdateMedia(taskNum, isUsed);
            return result;
        }
    }
}
