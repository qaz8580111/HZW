using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.XZSPBLLs;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.XZSPModels;
using Taizhou.PLE.Model.XZSPNewModels;

namespace Taizhou.PLE.Web.Process.NewXZSPProess
{
    public class NewXZSPProess
    {
        // 定义内部成员
        #region
        public string AIID { get; set; }
        public string ADID { get; set; }
        #endregion
        /// <summary>
        /// 点击提交时
        /// </summary>
        /// <param name="ttwork">内容</param>
        /// <param name="ADID">当前活动的流程编号</param>
        /// <param name="NextADID">下一个活动的流程编号</param>
        /// <param name="NextToUserID">下一个处理人编号</param>
        /// <param name="AIID">主表唯一编号</param>
        public static void sumbit(TotalWorkflows ttwork, decimal ADID, decimal NextADID, decimal NextToUserID, string AIID)
        {
            //每次提交，当前数据改成完成状态，加入一条数据为活动状态
            //完成状态
            decimal complete = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Complete.GetHashCode();
            //活动状态
            decimal active = Taizhou.PLE.Common.Enums.XZSPEnums.StatusEnum.Active.GetHashCode();
            //当前当前活动的信息
            CreatedActivityInstance(ttwork, ADID, AIID, complete);

            //当前活动添加完成之后，修改主表对应的ADID,ToUserID,StatusID
            decimal StatusID = 0;
            if (ADID == 5)
            {
                StatusID = 2;
            }
            else
            {
                StatusID = 1;
            }
            decimal NADID = NextADID;
            decimal ToUserID = NextToUserID;

            ActivityInstanceBLL.NewUpdate(ToUserID, NextADID, AIID, StatusID);
        }
        /// <summary>
        /// 添加一条新的数据
        /// </summary>
        public static void CreatedActivityInstance(TotalWorkflows ttwork, decimal ADID, string AIID, decimal active)
        {
            if (ADID == 1)//指派大队
            {
                XZSPNEWTAB activityInstance = new XZSPNEWTAB()
                {
                    AIID = AIID,
                    ADID = 1,
                    STUTASID = active,
                    PQSJ = DateTime.Now,
                    PQR = ttwork.Workflow1.PQR,
                    PQYJ = ttwork.Workflow1.DDSHYJ
                };
                ActivityInstanceBLL.NewAdd(activityInstance);
            }
            else if (ADID == 2)//指派中队
            {
                XZSPNEWTAB activityInstance = new XZSPNEWTAB()
                {
                    AIID = AIID,
                    ADID = 2,
                    STUTASID = active,
                    PQSJ = DateTime.Now,
                    PQR = ttwork.Workflow2.ZFZDRY,
                    PQYJ = ttwork.Workflow2.ZDSHYJ
                };
                ActivityInstanceBLL.NewAdd(activityInstance);
            }
            else if (ADID == 3)//指派队员
            {
                XZSPNEWTAB activityInstance = new XZSPNEWTAB()
                {
                    AIID = AIID,
                    ADID = 3,
                    STUTASID = active,
                    PQSJ = DateTime.Now,
                    PQR = ttwork.Workflow3.ZPDY,
                    PQYJ = ttwork.Workflow3.DYYJ
                };
                ActivityInstanceBLL.NewAdd(activityInstance);
            }
            else if (ADID == 4)//队员处理
            {
                XZSPNEWTAB activityInstance = new XZSPNEWTAB()
                {
                    AIID = AIID,
                    ADID = 4,
                    STUTASID = active,
                    PQSJ = DateTime.Now,
                    PQR = ttwork.Workflow4.DYCLR,
                    PQYJ = ttwork.Workflow4.DYYJ,
                    ATTACHMENT1 = ttwork.Workflow4.pic1TextP,
                    ATTACHMENT2 = ttwork.Workflow4.pic2TextP,
                    ATTACHMENT3 = ttwork.Workflow4.pic3TextP
                };
                ActivityInstanceBLL.NewAdd(activityInstance);
            }
            else if (ADID == 5)//中队审核
            {
                XZSPNEWTAB activityInstance = new XZSPNEWTAB()
                {
                    AIID = AIID,
                    ADID = 5,
                    STUTASID = active,
                    PQSJ = DateTime.Now,
                    PQR = ttwork.Workflow5.ZDZ,
                    PQYJ = ttwork.Workflow5.ZDSHYJ
                };
                ActivityInstanceBLL.NewAdd(activityInstance);
            }
        }

        /// <summary>
        /// 流程第一步
        /// </summary>
        public void XZSPWorkflow1(Workflow1 Workflow1)
        {
            BaseXZSPWorkflows TXWorkflows = new BaseXZSPWorkflows();
            TotalWorkflows ttwork = new TotalWorkflows();
            ttwork.Workflow1 = Workflow1;
            decimal NextADID = 2;
            decimal NextToUserId = 0;
            decimal.TryParse(Workflow1.PQR, out NextToUserId);
            sumbit(ttwork, ttwork.Workflow1.ADID, NextADID, NextToUserId, ttwork.Workflow1.AIID);
        }

        /// <summary>
        /// 流程第二步
        /// </summary>
        /// <param name="Workflow1"></param>
        public void XZSPWorkflow2(Workflow2 Workflow2)
        {
            BaseXZSPWorkflows TXWorkflows = new BaseXZSPWorkflows();
            TotalWorkflows ttwork = new TotalWorkflows();
            ttwork.Workflow2 = Workflow2;
            decimal NextADID = 3;
            decimal NextToUserId = 0;
            decimal.TryParse(Workflow2.ZFZD, out NextToUserId);
            sumbit(ttwork, ttwork.Workflow2.ADID, NextADID, NextToUserId, ttwork.Workflow2.AIID);
        }

        /// <summary>
        /// 流程第三步
        /// </summary>
        /// <param name="Workflow1"></param>
        public void XZSPWorkflow3(Workflow3 Workflow3)
        {
            BaseXZSPWorkflows TXWorkflows = new BaseXZSPWorkflows();
            TotalWorkflows ttwork = new TotalWorkflows();
            ttwork.Workflow3 = Workflow3;
            decimal NextADID = 4;
            decimal NextToUserId = 0;
            decimal.TryParse(Workflow3.ZPDY, out NextToUserId);
            sumbit(ttwork, ttwork.Workflow3.ADID, NextADID, NextToUserId, ttwork.Workflow3.AIID);
        }

        /// <summary>
        /// 流程第四步
        /// </summary>
        /// <param name="Workflow1"></param>
        public void XZSPWorkflow4(Workflow4 Workflow4)
        {
            BaseXZSPWorkflows TXWorkflows = new BaseXZSPWorkflows();
            TotalWorkflows ttwork = new TotalWorkflows();
            ttwork.Workflow4 = Workflow4;
            decimal NextADID = 5;
            decimal NextToUserId = 0;
            decimal.TryParse(Workflow4.DYCLR, out NextToUserId);
            sumbit(ttwork, ttwork.Workflow4.ADID, NextADID, NextToUserId, ttwork.Workflow4.AIID);
        }

        /// <summary>
        /// 流程第五步
        /// </summary>
        /// <param name="XZSPWorkflow5"></param>
        public void XZSPWorkflow5(Workflow5 Workflow5)
        {
            BaseXZSPWorkflows TXWorkflows = new BaseXZSPWorkflows();
            TotalWorkflows ttwork = new TotalWorkflows();
            ttwork.Workflow5 = Workflow5;
            decimal NextADID = 6;
            decimal NextToUserId = 0;
            sumbit(ttwork, ttwork.Workflow5.ADID, NextADID, NextToUserId, ttwork.Workflow5.AIID);
        }


    }
}