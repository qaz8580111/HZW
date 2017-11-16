using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;

namespace Taizhou.PLE.WorkflowLib
{
    public abstract class WorkflowBase
    {
        public Workflow Workflow { get; protected set; }

        protected void CommitStatus()
        {
            var results = from t in this.Workflow.Activities.Values
                          where t.ActivityStatus == ActivityStatusEnum.Active
                          select t;

            if (results.Count() == 0)
            {
                this.Workflow.WorkflowStatus = WorkflowStatusEnum.Completed;
                this.Workflow.CommitChanges();
            }
        }

        protected Activity CreateActivity(Activity activity, decimal adid, decimal? AssignUserID)
        {
            Activity next = new Activity();

            next.AIID = Guid.NewGuid().ToString("N");
            next.ADID = adid;

            if (activity != null)
            {
                next.PreviousAIID = activity.AIID;
                next.PreviousActivity = activity;
            }

            next.AssignUserID = AssignUserID;
            next.ActivityStatus = ActivityStatusEnum.Active;
            next.DeliveryTime = DateTime.Now;

            if (next.Definition.TimeLimits != null)
            {
                next.ExpirationTime = GetExpirationTimeLimit(next.DeliveryTime
                    , (int)next.Definition.TimeLimits);
            }

            this.Workflow.Activities.Add(next.AIID, next);

            this.BindEvent(adid, next);

            return next;
        }

        public static DateTime GetExpirationTimeLimit(DateTime now, int dayCount)
        {
            //超期日期
            DateTime limitTime = now;

            for (int i = 1; i <= dayCount; i++)
            {
                //获取明天是星期几
                string dayOfWeek = GetDayOfWeek(limitTime.AddDays(1).DayOfWeek);

                limitTime = limitTime.AddDays(1);

                if (dayOfWeek == "六" || dayOfWeek == "日")
                {
                    i--;
                }
            }

            return limitTime;
        }


        public static string GetDayOfWeek(DayOfWeek dayOfWeek)
        {
            string strDayOfWeek = "";

            switch ((int)dayOfWeek)
            {
                case 0:
                    strDayOfWeek = "日";
                    break;
                case 1:
                    strDayOfWeek = "一";
                    break;
                case 2:
                    strDayOfWeek = "二";
                    break;
                case 3:
                    strDayOfWeek = "三";
                    break;
                case 4:
                    strDayOfWeek = "四";
                    break;
                case 5:
                    strDayOfWeek = "五";
                    break;
                default:
                    strDayOfWeek = "六";
                    break;
            }

            return strDayOfWeek;
        }

        protected abstract void BindEvent(decimal adid, Activity activity);
    }
}
