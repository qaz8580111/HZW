using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taizhou.PLE.Model;
using Taizhou.PLE.BLL;
using Taizhou.PLE.BLL.PublicService;
using Taizhou.PLE.BLL.ZFSJBLL;

namespace GGFWZFSJConsole
{
    /// <summary>
    /// 更新超期时间
    /// </summary>
    public class UpdateZFSJTime
    {
        public void DealTime()
        {
            //获取状态为 开始处理  已经处理 
            IList<GGFWEVENT> list = GGFWEventBLL.GetGGFWEvents().Where(a => a.STATUE >= 3 && a.STATUE <= 4 && a.WIID != null).ToList();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //if (list[i].EVENTID != 510)
                    //    continue;

                    if (!string.IsNullOrEmpty(list[i].WIID))
                    {
                        IList<ZFSJACTIVITYINSTANCE> listAc = ZFSJActivityInstanceBLL.GetListByWiid(list[i].WIID).ToList();//获取对应事件的处理过程
                        if (listAc != null && listAc.Count > 0)
                        {
                            ZFSJWORKFLOWINSTANCE model = ZFSJWorkflowInstanceBLL.GetWorkflowInstanceByWIID(list[i].WIID);

                            for (int j = 0; j < listAc.Count; j++)
                            {
                                DateTime OVERTIME;
                                if (list[i].OVERTIME != null && DateTime.TryParse(list[i].OVERTIME.ToString(), out OVERTIME) && listAc[j].SJTIMELIMIT == null)
                                {
                                    //更新当前超期时间
                                    ZFSJActivityInstanceBLL.UpdateSJTIMELIMIT(listAc[j].AIID, Convert.ToDateTime(list[i].OVERTIME));
                                }

                                //更新当前处理人
                                if (model != null && model.CURRENTAIID == listAc[j].AIID && !string.IsNullOrEmpty(listAc[j].TOUSERID))
                                {
                                    GGFWEventBLL.UpdateDEALINGUSERID(list[i].EVENTID, Convert.ToDecimal(listAc[j].TOUSERID));
                                }
                            }
                        }
                    }
                    Console.WriteLine((i + 1) + "/" + list.Count + ",公共服务事件编号为：" + list[i].EVENTID + ",执法事件编号为：" + list[i].WIID + "，更新超期时间和当前处理人");
                }
            }
            else
            {
                Console.WriteLine("没有要更新的超期时间和当前处理人");
            }
        }

    }
}
