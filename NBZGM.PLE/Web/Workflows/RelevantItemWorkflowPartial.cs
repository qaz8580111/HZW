using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model.RelevantItemWorkflowModels;
using Taizhou.PLE.WorkflowLib;

namespace Web.Workflows
{
    public partial class RelevantItemWorkflow
    {
        private void AD201_Submitted(object sender, EventArgs e)
        {
            RelevantItemForm1 form1 = this.RelevantItemForm.RelevantItemForm1;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = form1.ProcessUser.UserID;
            activity.ProcessTime = form1.ProcessTime;
            activity.CommitChanges();

            Activity next = this.CreateActivity(activity, ADID202, form1.CBLDID);

            this.RelevantItemForm.RelevantItemForm2 = new RelevantItemForm2()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        private void AD202_Submitted(object sender, EventArgs e)
        {
            RelevantItemForm2 form2 = this.RelevantItemForm.RelevantItemForm2;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = form2.ProcessUser.UserID;
            activity.ProcessTime = form2.ProcessTime;
            activity.CommitChanges();

            Activity next = this.CreateActivity(activity, ADID203, form2.FGLDID);

            this.RelevantItemForm.RelevantItemForm3 = new RelevantItemForm3()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        private void AD203_Submitted(object sender, EventArgs e)
        {
            RelevantItemForm3 form3 = this.RelevantItemForm.RelevantItemForm3;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = form3.ProcessUser.UserID;
            activity.ProcessTime = form3.ProcessTime;
            activity.CommitChanges();

            this.Workflow.CommitChanges();
            this.CommitStatus();
        }
    }
}