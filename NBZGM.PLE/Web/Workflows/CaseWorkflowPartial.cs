using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.BLL.UserBLLs;
using Taizhou.PLE.Common.Enums.CaseEnums;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.CaseWorkflowModels;
using Taizhou.PLE.WorkflowLib;

namespace Web.Workflows
{
    /// <summary>
    /// 案件工作流的部分类(硬编码的方式提交流程表单和控制流程流转)
    /// </summary>
    public partial class CaseWorkflow
    {
        /// <summary>
        /// AD1 执法队员提出立案申请
        /// </summary>
        private void AD1_Submitted(object sender, EventArgs e)
        {
            Form101 form1 = this.CaseForm.FinalForm.Form101;
            Form102 form2 = this.CaseForm.FinalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = form1;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = form1.ProcessUser.UserID;
            activity.ProcessTime = form1.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(this.CaseForm.FinalForm);
            Activity next = this.CreateActivity(activity, ADID102, form1.CBLDID);

            this.CaseForm.FinalForm.Form102 = new Form102()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName,
            };
            this.CaseForm.UnitID = form1.CBDWID;
            this.Workflow.UnitID = form1.CBDWID;
            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD2 办案单位审核立案申请
        /// </summary>
        private void AD2_Submitted(object sender, EventArgs e)
        {
            Form102 form2 = this.CaseForm.FinalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = form2;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = form2.ProcessUser.UserID;
            activity.ProcessTime = form2.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(this.CaseForm.FinalForm);

            //如果办案单位同意立案建议,则流程流转到法制机构审批立案建议
            if (form2.Approved.Value)
            {
                Activity next = this.CreateActivity(activity, ADID103, form2.FZCSHR.UserID);

                this.CaseForm.FinalForm.Form103 = new Form103
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };

                this.Workflow.CommitChanges();
            }
            //如果承办机构负责人不同意立案建议,则流程退至上一步：办案人员提出立案建议
            else
            {
                Activity next = this.CreateActivity(activity, ADID101,
                    this.CaseForm.FinalForm.Form101.ProcessUser.UserID);

                this.CaseForm.FinalForm.Form101.ID = next.AIID;
                this.CaseForm.FinalForm.Form101.ProcessTime = null;
                this.CaseForm.FinalForm.Form101.ProcessUser = null;
                this.CaseForm.FinalForm.Form101.THYJ = form2.LDSPYJ;
                this.Workflow.CommitChanges();
            }
        }

        /// <summary>
        /// AD3 法制处审核立案申请
        /// </summary>
        private void AD3_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form103;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form103.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form103.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            //如果法制处同意立案申请,则流程跳转至下一步：分管领导审核立案申请
            if (finalForm.Form103.Approved.Value)
            {
                Activity next = this.CreateActivity(activity, ADID104,
                        finalForm.Form102.FGLDID);

                finalForm.Form104 = new Form104()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            //如果法制处不同意立案申请,则流程退回至上一步：办案单位审核立案申请
            else
            {
                Activity next = this.CreateActivity(activity, ADID102,
                    this.CaseForm.FinalForm.Form102.ProcessUser.UserID);

                this.CaseForm.FinalForm.Form102.ID = next.AIID;
                this.CaseForm.FinalForm.Form102.ProcessTime = null;
                this.CaseForm.FinalForm.Form102.ProcessUser = null;
                this.CaseForm.FinalForm.Form102.THYJ = finalForm.Form103.FZJGYJ;
            }

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD4 分管领导审核立案申请
        /// </summary>
        private void AD4_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form104;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form104.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form104.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Form101 form101 = this.CaseForm.FinalForm.Form101;
            Form102 form102 = this.CaseForm.FinalForm.Form102;
            Form104 form104 = this.CaseForm.FinalForm.Form104;

            //如果分管副局长同意立案申请,则流程跳转至下一步
            if (form104.Approved)
            {
                if (form101.SFLA == "la")
                {
                    Activity next = this.CreateActivity(activity, ADID105,
                        form102.ZBDY.UserID);

                    finalForm.Form105 = new Form105()
                    {
                        ID = next.AIID,
                        ADID = next.Definition.ADID,
                        ADName = next.Definition.ADName
                    };

                    this.Workflow.CommitChanges();
                }
                else
                {
                    this.Workflow.CommitChanges();
                    this.CommitStatus();
                }
            }
            //如果分管副局长不同意立案申请,则流程退回至上一环节
            else
            {
                Activity next = this.CreateActivity(activity, ADID103,
                    this.CaseForm.FinalForm.Form103.ProcessUser.UserID);

                this.CaseForm.FinalForm.Form103.ID = next.AIID;
                this.CaseForm.FinalForm.Form103.ProcessTime = null;
                this.CaseForm.FinalForm.Form103.ProcessUser = null;
                this.CaseForm.FinalForm.Form103.THYJ = form104.FGFJZYJ;
                this.Workflow.CommitChanges();
            }
        }

        /// <summary>
        /// AD5 主办队员调查取证
        /// </summary>
        private void AD5_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form105;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form105.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form105.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Form102 form102 = this.CaseForm.FinalForm.Form102;
            Activity next = this.CreateActivity(activity, ADID106,
                form102.XBDY.UserID);

            finalForm.Form106 = new Form106()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD6 协办队员确认调查取证
        /// </summary>
        private void AD6_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form106;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form106.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form106.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID107,
                form102.ZBDY.UserID);

            finalForm.Form107 = new Form107()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD7 主办队员提出处理意见
        /// </summary>
        private void AD7_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form107;
            Form102 form102 = finalForm.Form102;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form107.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form107.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID108,
                form102.XBDY.UserID);

            finalForm.Form108 = new Form108()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD8 协办队员确认处理意见
        /// </summary>
        private void AD8_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form108;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form108.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form108.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            if (finalForm.Form108.Approved)
            {

                Activity next = this.CreateActivity(activity, ADID109, finalForm.Form101.CBLDID);

                finalForm.Form109 = new Form109()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            else
            {
                Activity next = this.CreateActivity(activity, ADID107,
                    finalForm.Form102.ZBDY.UserID);

                finalForm.Form107.ID = next.AIID;
                finalForm.Form107.ProcessUser = null;
                finalForm.Form107.ProcessTime = null;
                finalForm.Form107.THYJ = finalForm.Form108.XBDYYJ;
            }

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD9 办案单位审核处理意见
        /// </summary>
        private void AD9_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form109;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form109.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form109.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            if (finalForm.Form109.Approved)
            {
                Activity next = this.CreateActivity(activity, ADID110, finalForm.Form102.FZCSHR.UserID);

                finalForm.Form110 = new Form110()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            else
            {
                Activity next = this.CreateActivity(activity, ADID108,
                    finalForm.Form102.XBDY.UserID);

                finalForm.Form108.ID = next.AIID;
                finalForm.Form108.ProcessUser = null;
                finalForm.Form108.ProcessTime = null;
                finalForm.Form108.THYJ = finalForm.Form109.BADWYJ;
            }

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD10 法制处审核处理意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AD10_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form110;
            Form101 form1 = CaseForm.FinalForm.Form101;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form110.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form110.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            if (finalForm.Form110.Approved)
            {
                //如果为重大案件,则下一步流程环节为集体讨论
                if (form1.SFWZDAN == "s")
                {
                    Activity next = this.CreateActivity(activity, ADID111, finalForm.Form102.FZCSHR.UserID);
                    finalForm.Form111 = new Form111()
                    {
                        ID = next.AIID,
                        ADID = next.Definition.ADID,
                        ADName = next.Definition.ADName
                    };
                }
                //如果为非重大案件,则下一步流程环节为分管副局长审核处理意见
                else
                {
                    Activity next = this.CreateActivity(activity, ADID112, finalForm.Form102.FGLDID);
                    finalForm.Form112 = new Form112()
                    {
                        ID = next.AIID,
                        ADID = next.Definition.ADID,
                        ADName = next.Definition.ADName
                    };
                }
            }
            else
            {
                Activity next = this.CreateActivity(activity, ADID109, finalForm.Form101.CBLDID);

                finalForm.Form109.ID = next.AIID;
                finalForm.Form109.ProcessUser = null;
                finalForm.Form109.ProcessTime = null;
                finalForm.Form109.THYJ = finalForm.Form110.FZCYJ;
            }

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD11 集体讨论(法制处)
        /// </summary>
        private void AD11_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form111;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form111.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form111.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID112, finalForm.Form102.FGLDID);

            finalForm.Form112 = new Form112()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD12 分管副局长审核处理意见
        /// </summary>
        private void AD12_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            Form107 form107 = finalForm.Form107;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form112;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form112.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form112.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            if (finalForm.Form112.Approved)
            {
                //如果案件处理方式为行政处罚的,流程跳至主办队员行政处罚事先告知环节
                if (form107.CLFS == "xzcf")
                {
                    Activity next = this.CreateActivity(activity, ADID114,
                        form102.ZBDY.UserID);

                    finalForm.Form114 = new Form114()
                    {
                        ID = next.AIID,
                        ADID = next.Definition.ADID,
                        ADName = next.Definition.ADName
                    };
                }
                //如果案件处理方式为非行政处罚的,流程跳至送达通知环节
                else
                {
                    Activity next = this.CreateActivity(activity, ADID113,
                        form102.ZBDY.UserID);

                    finalForm.Form113 = new Form113()
                    {
                        ID = next.AIID,
                        ADID = next.Definition.ADID,
                        ADName = next.Definition.ADName
                    };
                }
            }
            else
            {
                Activity next = this.CreateActivity(activity, ADID110, form102.FZCSHR.UserID);

                finalForm.Form110.ID = next.AIID;
                finalForm.Form110.ProcessTime = null;
                finalForm.Form110.ProcessUser = null;
                finalForm.Form110.THYJ = finalForm.Form112.FGFJZYJ;
            }

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD13 送达通知(不予行政处罚的情况)
        /// </summary>
        private void AD13_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form113;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form113.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form113.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            this.Workflow.CommitChanges();

            //流程结束
            this.CommitStatus();
        }

        /// <summary>
        /// AD14 行政处罚事先告知 (主办队员)
        /// </summary>
        private void AD14_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form114;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form114.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form114.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID115,
                form102.ZBDY.UserID);

            finalForm.Form115 = new Form115()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD15 当事人意见反馈(主办队员)
        /// </summary>
        private void AD15_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form115;
            Form115 form115 = finalForm.Form115;
            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form115.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form115.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            //放弃陈述申辩或听证
            if (form115.DSRYJ == "0")
            {
                Activity next = this.CreateActivity(activity, ADID116,
              form102.ZBDY.UserID);

                finalForm.Form116 = new Form116()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            //要求陈述申辩
            else if (form115.DSRYJ == "1")
            {
                Activity next = this.CreateActivity(activity, ADID131,
            form102.ZBDY.UserID);

                finalForm.Form131 = new Form131()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            //符合听证条件，当事人提出听证申请
            else if (form115.DSRYJ == "2")
            {
                Activity next = this.CreateActivity(activity, ADID132,
            form102.ZBDY.UserID);

                finalForm.Form132 = new Form132()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD16 制作处罚决定书(主办队员)
        /// </summary>
        private void AD16_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form116;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form116.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form116.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID117,
                form102.XBDY.UserID);

            finalForm.Form117 = new Form117()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD17 确认处罚决定书(协办队员)
        /// </summary>
        private void AD17_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form117;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form117.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form117.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID118, finalForm.Form101.CBLDID);

            finalForm.Form118 = new Form118()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// AD18 办案单位确认处罚决定
        /// </summary>
        private void AD18_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form118;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form118.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form118.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID119, finalForm.Form102.FZCSHR.UserID);

            finalForm.Form119 = new Form119()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 法制处确认处罚决定
        /// </summary>
        private void AD19_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form119;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form119.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form119.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID120, finalForm.Form102.FGLDID);

            finalForm.Form120 = new Form120()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 分管副局长确认处罚决定
        /// </summary>
        private void AD20_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form120;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form120.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form120.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            Activity next = this.CreateActivity(activity, ADID133, null);

            finalForm.Form133 = new Form133()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 送达行政处罚决定书
        /// </summary>
        private void AD21_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form121;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form121.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form121.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            //当事人执行方式为当事人自己履行，流程转至主办队员提出结案申请
            if (finalForm.Form121.DSRZXFS == 1)
            {
                Activity next = this.CreateActivity(activity, ADID122,
                    form102.ZBDY.UserID);

                finalForm.Form122 = new Form122()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            //当事人提起行政复议或者行政诉讼
            else if (finalForm.Form121.DSRZXFS == 2)
            {
                Activity next = this.CreateActivity(activity, ADID127,
                    form102.ZBDY.UserID);

                finalForm.Form127 = new Form127()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            //行政强制执行
            else if (finalForm.Form121.DSRZXFS == 3)
            {
                Activity next = this.CreateActivity(activity, ADID128,
                   form102.ZBDY.UserID);

                finalForm.Form128 = new Form128()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            //申请法院强制执行
            else if (finalForm.Form121.DSRZXFS == 4)
            {
                Activity next = this.CreateActivity(activity, ADID129,
                    form102.ZBDY.UserID);

                finalForm.Form129 = new Form129()
                {
                    ID = next.AIID,
                    ADID = next.Definition.ADID,
                    ADName = next.Definition.ADName
                };
            }
            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 主办队员提出结案申请
        /// </summary>
        private void AD22_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form122;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form122.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form122.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID123, form102.XBDY.UserID);

            finalForm.Form123 = new Form123()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 协办队员确认结案申请
        /// </summary>
        private void AD23_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form123;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form123.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form123.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID124, finalForm.Form101.CBLDID);

            finalForm.Form124 = new Form124()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 办案单位确认结案申请
        /// </summary>
        private void AD24_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form124;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form124.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form124.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID125, finalForm.Form102.FZCSHR.UserID);

            finalForm.Form125 = new Form125()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 法制处确认结案申请
        /// </summary>
        private void AD25_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form125;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form125.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form125.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID126, finalForm.Form102.FGLDID);

            finalForm.Form126 = new Form126()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 分管副局长确认结案意见
        /// </summary>
        private void AD26_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form126;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form126.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form126.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);
            this.Workflow.CommitChanges();
            this.CommitStatus();
        }

        /// <summary>
        /// 当事人提起行政复议或者行政诉讼
        /// </summary>
        private void AD27_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form127;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form127.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form127.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID130, form102.ZBDY.UserID);

            finalForm.Form130 = new Form130()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 当事人提起行政复议或者行政诉讼
        /// </summary>
        private void AD28_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form128;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form128.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form128.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID130, form102.ZBDY.UserID);

            finalForm.Form130 = new Form130()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 当事人提起行政复议或者行政诉讼
        /// </summary>
        private void AD29_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form129;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form129.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form129.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID130, form102.ZBDY.UserID);

            finalForm.Form130 = new Form130()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 当事人提起行政复议或者行政诉讼
        /// </summary>
        private void AD30_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form130;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form130.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form130.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID122, form102.ZBDY.UserID);

            finalForm.Form122 = new Form122()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 要求陈述申辩
        /// </summary>
        private void AD31_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form131;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form131.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form131.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID116, form102.ZBDY.UserID);

            finalForm.Form116 = new Form116()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 符合听证条件，当事人提出听证申请
        /// </summary>
        private void AD32_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form132;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form132.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form132.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID116, form102.ZBDY.UserID);

            finalForm.Form116 = new Form116()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }

        /// <summary>
        /// 文书环节
        /// </summary>
        private void AD33_Submitted(object sender, EventArgs e)
        {
            TotalForm finalForm = this.CaseForm.FinalForm;
            Form102 form102 = finalForm.Form102;
            this.CaseForm.FinalForm.CurrentForm = finalForm.Form132;

            Activity activity = (Activity)sender;
            activity.ActivityStatus = ActivityStatusEnum.Inactive;
            activity.ProcessUserID = finalForm.Form133.ProcessUser.UserID;
            activity.ProcessTime = finalForm.Form133.ProcessTime;
            activity.CommitChanges();

            this.CaseForm.ProcessForms.Add(finalForm);

            Activity next = this.CreateActivity(activity, ADID121, form102.ZBDY.UserID);

            finalForm.Form121 = new Form121()
            {
                ID = next.AIID,
                ADID = next.Definition.ADID,
                ADName = next.Definition.ADName
            };

            this.Workflow.CommitChanges();
        }
    }
}