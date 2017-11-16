using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.BLL.ZFSJBLL;
using Taizhou.PLE.BLL.PublicService;

namespace GGFWZFSJConsole
{
    /// <summary>
    /// 将执法事件中删除的事件回退到96310中
    /// </summary>
    public class ZFSJToGGFWBack
    {
        public void DealZFSJDeleteToBack()
        { 
            //获取来源为公共服务的删除事件
           IList<ZFSJWORKFLOWINSTANCE> list = ZFSJWorkflowInstanceBLL.GetList().Where(a => a.STATUSID == 4 && a.EVENTSOURCEID != null && a.EVENTSOURCEID != 0 && a.EVENTSOURCEPKID != null).ToList();
           if (list != null && list.Count > 0)
           {
               for (int i = 0; i < list.Count; i++)
               {
                   decimal eventId;
                   decimal.TryParse(list[i].EVENTSOURCEPKID, out eventId);
                   //修改执法流程中的来源与公共服务编号 为空
                   list[i].EVENTSOURCEID = null;
                   list[i].EVENTSOURCEPKID = null;
                   ZFSJWorkflowInstanceBLL.Update(list[i]);

                   //修改公用服务事件中执法编号及状态
                   GGFWEVENT ggfwModel = GGFWEventBLL.GetGGFWEventByEventID(eventId);
                   if (ggfwModel != null)
                   {
                       ggfwModel.WIID = null;
                       ggfwModel.STATUE = 1;//开始处理
                       GGFWEventBLL.ModifyGGFWEvents(ggfwModel);
                   }
                   //综合科选择中队的意见为空
                   GGFWTOZFZD ggfwTozfzdModel = GGFWToZFZDBLL.single(eventId);
                   if (ggfwTozfzdModel != null)
                   {
                       ggfwTozfzdModel.REFUSECONTENT = null;
                       new GGFWToZFZDBLL().UpdateREFUSECONTENT(ggfwTozfzdModel);
                   }
                   Console.WriteLine((i + 1) + "/" + list.Count + ",当前公共服务事件编号：" + eventId + "执法事件编号：" + list[i].WIID);
               }
               Console.WriteLine("当前来源为公共服务中删除的执法事件已经处理完成!当前时间：" + DateTime.Now);
           }
           else
           {
               Console.WriteLine("当前没有来源为公共服务中删除的执法事件!当前时间：" + DateTime.Now);
           }
        }
    }
}
