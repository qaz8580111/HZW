using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.BLL.PublicService
{
    public  class GGFWStatueBLL
    {
        public static string GetNameByID(decimal ID)
        {
            PLEEntities db = new PLEEntities();
            GGFWSTATUE result = db.GGFWSTATUES.FirstOrDefault(a => a.STATUEID == ID);
            return result.STATUENAME;
        }

        /// <summary>
        /// 根据公共服务事件中的来源获取执法事件中对应事件来源编号
        /// </summary>
        /// <param name="GGFWSourceId">公共服务事件中的来源编号</param>
        /// <returns></returns>
        public static decimal GetZFSJSourceIdByGGFWSourceId(decimal GGFWSourceId)
        {
            decimal result = 0;
            string sourceId = GGFWSourceId.ToString();
            switch (sourceId)
            {
                case "4"://政府直线电话
                    result = 5;
                    break;
                case "5"://96310
                    result = 6;
                    break;
                case "6"://来访
                    result = 7;
                    break;
                case "7"://来信
                    result = 8;
                    break;
                case "8"://领导接待日
                    result = 9;
                    break;
                case "9"://局长信箱
                    result = 10;
                    break;
                case "10"://交办件
                    result = 11;
                    break;
                case "11"://政府信息公开网
                    result = 12;
                    break;
                case "12"://网上举报
                    result = 13;
                    break;
                case "13"://网上咨询
                    result = 14;
                    break;
                case "14"://其他
                    result = 15;
                    break;
                default:
                    if (GGFWSourceId <= 3)//前边3个能对应上  1.数字城管 2.语音热线 3.信访投诉
                        result = GGFWSourceId;
                    else
                        result = 15;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 获取所有状态
        /// </summary>
        /// <returns></returns>
        public static IQueryable<GGFWSTATUE> GetList()
        {
            PLEEntities db = new PLEEntities();
            IQueryable<GGFWSTATUE> list = db.GGFWSTATUES;
            return list;
        }
    }
}
