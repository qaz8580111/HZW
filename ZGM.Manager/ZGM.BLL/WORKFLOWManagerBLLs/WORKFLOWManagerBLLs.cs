using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.BLL.UserBLLs;
using ZGM.BLL.XTGL;
using ZGM.Model;
using ZGM.Model.CustomModels;
using ZGM.Common;

namespace ZGM.BLL.WORKFLOWManagerBLLs
{
    public class WORKFLOWManagerBLLs
    {
        /// <summary>
        /// 活动流程
        /// </summary>
        /// <param name="WFID">流程ID</param>
        /// <param name="WFSID">实例编号</param>
        /// <returns></returns>
        public WorkFlowClass ProcessIndex(string WFID, string WFDID, string WFSID, string WFSAID, string TYPE, string IsMainWF)
        {
            WorkFlowClass workflow = new WorkFlowClass();
            if (!string.IsNullOrEmpty(WFID))
            {
                WF_WORKFLOWS wfModel = new WF_WORKFLOWSBLL().GetSingle(WFID);
                if (wfModel != null)
                {
                    workflow.currentActivityName = wfModel.WFNAME;
                    //跳转的方法
                    workflow.FunctionName = wfModel.TABLENAME;
                    workflow.WFID = WFID;
                    workflow.WFDID = WFDID;
                    workflow.WFSID = WFSID;
                    workflow.WFSAID = WFSAID;
                    workflow.TYPE = TYPE;
                    workflow.GetTableColumns = new WF_WORKFLOWSBLL().GetTableColumns(wfModel.TABLENAME);
                }
                else
                {
                    workflow.WorkFlowState = 1;//表示该流程不存在
                }

                if (wfModel != null && !string.IsNullOrEmpty(wfModel.WFPIC))
                    workflow.WFPIC = wfModel.WFPIC;
                else
                    workflow.WFPIC = "/Content/oaManager/noWorkFlowPic.jpg";

                IList<WF_WORKFLOWDETAILS> wfdList = new WF_WORKFLOWDETAILBLL()
                    .GetList().Where(a => a.WFID == WFID).ToList();//获取当前流程所有环节
                if (wfdList == null && wfdList.Count() <= 0)
                {
                    workflow.WorkFlowState = 2;
                    return workflow;
                }
                //获取主要内容
                if (!string.IsNullOrEmpty(WFSID))
                {
                    workflow.contentPath = new WF_WORKFLOWSPECIFICSBLL().GetContentPath(WFSID);
                }

                #region 获取下一步流程

                //下一步流程编号集合 格式为：【，流程编号，流程编号，流程编号，】
                string nextWFDIDS = string.Empty;
                WF_WORKFLOWDETAILS wfdModel = new WF_WORKFLOWDETAILBLL().GetSingle(WFDID);
                if (wfdModel != null)
                    nextWFDIDS = wfdModel.NEXTID;
                else
                {
                    workflow.WorkFlowState = 2;
                    return workflow;
                }

                if (!string.IsNullOrEmpty(nextWFDIDS))
                {
                    #region 下一步的所有流程
                    //下一步流程 格式为：【;流程编号,流程名称,是否有下一个流程;流程编号,流程名称,是否有下一个流程;流程编号,流程名称,是否有下一个流程;】
                    string nextWFDIDNames = "";

                    string[] nextWFDIDS_split = nextWFDIDS.Split(',');
                    foreach (string NEXTID in nextWFDIDS_split)
                    {
                        if (!string.IsNullOrEmpty(NEXTID))
                        {
                            string temp = "";
                            string WFDNAME = string.Empty;
                            WF_WORKFLOWDETAILS wfdMpdel = wfdList.FirstOrDefault(a => a.WFDID == NEXTID);
                            if (wfdMpdel == null || string.IsNullOrEmpty(wfdMpdel.WFDNAME))
                            {
                                workflow.WorkFlowState = 2;
                                return workflow;
                            }
                            else
                                WFDNAME = wfdMpdel.WFDNAME;
                            int isNextW = 0;//是否还有下步流程，如果没有则表示结束
                            if (string.IsNullOrEmpty(wfdMpdel.NEXTID))
                                isNextW = 1;

                            temp += NEXTID + "," + WFDNAME + "," + isNextW;

                            if (!string.IsNullOrEmpty(nextWFDIDNames))
                                nextWFDIDNames += temp + ";";
                            else
                                nextWFDIDNames += ";" + temp + ";";
                        }
                    }

                    workflow.nextWFDIDNames = nextWFDIDNames;//下一步流程 
                    #endregion

                    #region 查找下一步默认的用户 如果默认下一个步骤只有一步，并且明确制定用户，则直接将对应的用户加载出来
                    string UserIDs = string.Empty;
                    string UserNames = string.Empty;
                    string NextWFDID = string.Empty;

                    if (!string.IsNullOrEmpty(wfdModel.NEXTIDDEFAULT))
                    {
                        string[] nextDefaultUser = wfdModel.NEXTIDDEFAULTUSER.Split(',');
                        for (int i = 0; i < nextDefaultUser.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(nextDefaultUser[i]))
                            {
                                UserNames += ",";
                                if (nextDefaultUser[i] == "0")
                                    UserNames += "暂无,";
                                else
                                    UserNames += UserBLL.GetUserNameByUserID(Convert.ToDecimal(nextDefaultUser[i])) + ",";
                            }
                        }

                        workflow.NextWFUSERIDS = wfdModel.NEXTIDDEFAULTUSER;
                        workflow.NextUserNames = UserNames;
                        workflow.NextWFDID = wfdModel.NEXTIDDEFAULT;
                    }
                    if (!string.IsNullOrEmpty(wfdModel.NEXTIDDEFAULTDEPT))
                    {
                        string[] nextDefaultDept = wfdModel.NEXTIDDEFAULTDEPT.Split(',');
                        for (int i = 0; i < nextDefaultDept.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(nextDefaultDept[i]))
                            {
                                UserNames += ",";
                                if (nextDefaultDept[i] == "0")
                                    UserNames += "暂无,";
                                else
                                    UserNames += UserBLL.GetUserNameByDeptID(Convert.ToDecimal(nextDefaultDept[i])) + ",";
                                UserNames = "";
                            }
                        }
                        workflow.NextWFUSERIDS = wfdModel.NEXTIDDEFAULTUSER;
                        workflow.NextUserNames = UserNames;
                        workflow.NextWFDID = wfdModel.NEXTIDDEFAULT;
                    }

                    if (!string.IsNullOrEmpty(wfdModel.NEXTIDDEFAULTROLE))
                    {
                        string[] nextDefaultRole = wfdModel.NEXTIDDEFAULTROLE.Split(',');
                        for (int i = 0; i < nextDefaultRole.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(nextDefaultRole[i]))
                            {
                                UserNames += ",";
                                if (nextDefaultRole[i] == "0")
                                    UserNames += "暂无,";
                                else
                                    UserNames += UserBLL.GetUserNameByRoleID(Convert.ToDecimal(nextDefaultRole[i])) + ",";

                            }
                        }
                        workflow.NextWFUSERIDS = wfdModel.NEXTIDDEFAULTUSER;
                        workflow.NextUserNames = UserNames;
                        workflow.NextWFDID = wfdModel.NEXTIDDEFAULT;
                    }

                    #endregion
                }
                #endregion
            }
            return workflow;
        }

        /// <summary>
        /// 流程提交
        /// </summary>
        /// <param name="workflow">流程实例模型</param>
        /// <param name="TableModel">具体表模型</param>
        /// <returns></returns>
        public string WF_Submit(WorkFlowClass workflow, object TableModel)
        {

            #region 获取数据
            string tableName = workflow.FunctionName;//获取表名
            string WFID = workflow.WFID;//工作流编号
            string WFDID = workflow.WFDID;//工作流环节编号
            string WFSID = workflow.WFSID;//活动实例编号
            string WFSAID = workflow.WFSAID;//当前环节实例编号
            string dealContent = workflow.DEALCONTENT;//会签意见
            string NextWFDID = workflow.NextWFDID;//下一个环节编号
            string NextWFUSERIDS = workflow.NextWFUSERIDS;//获取下一个环节的用户
            string IsSendMsg = workflow.IsSendMsg;//是否发送短信
            string Remark = workflow.Remark;
            #endregion

            #region 增加/修改 对应的流程
            string TABLENAMEID = string.Empty;//对应表数据编号
            string WFSNAME = string.Empty;//活动流程名称
            switch (workflow.FunctionName)
            {
                case "XTGL_ZFSJS"://执法事件
                    TABLENAMEID = function_XTGL_ZFSJ(out WFSNAME, workflow.WFSID, (XTGL_ZFSJS)TableModel);
                    break;
                case "OA_TASKS"://办公事件
                    TABLENAMEID = function_OA_TASKS(out WFSNAME, workflow.WFSID, (OA_TASKS)TableModel);
                    break;
                case "GCGL_SIMPLES"://简易工程
                    TABLENAMEID = function_GCGL_SIMPLES(out WFSNAME, workflow.WFSID, (GCGL_SIMPLES)TableModel);
                    break;
                default:
                    break;
            }
            #endregion

            #region 检查是否存在当前实例，如果不存在则增加当前活动实例

            WF_WORKFLOWSPECIFICS WFSModel = null;
            if (!string.IsNullOrEmpty(workflow.WFSID))
            {
                WFSModel = new WF_WORKFLOWSPECIFICSBLL().GetSingle(workflow.WFSID);
                if (WFSModel == null)
                    WFSID = "";
                else
                {
                    WFSModel.WFSNAME = WFSNAME;
                    new WF_WORKFLOWSPECIFICSBLL().Update(WFSModel);
                }
            }

            if (string.IsNullOrEmpty(WFSID))
            {
                WFSModel = new WF_WORKFLOWSPECIFICS();
                WFSModel.WFSID = GetNewId();
                WFSModel.WFID = WFID;
                WFSModel.TABLENAME = tableName;
                WFSModel.CREATEUSERID = workflow.WFCreateUserID;
                WFSModel.CREATETIME = DateTime.Now;
                WFSModel.STATUS = 1;
                WFSModel.TABLENAMEID = TABLENAMEID;
                WFSModel.WFSNAME = WFSNAME;
                new WF_WORKFLOWSPECIFICSBLL().Add(WFSModel);
                WFSID = WFSModel.WFSID;
            }
            #endregion

            #region 增加活动实例具体的流程

            #region 更新当前环节
            decimal oldStatus;//当前环节是否已经处理完成 1：未处理，2已经处理
            //更新或者增加当前流程的具体事例---返回当前环节编号
            WFSAID = function_WF_DealCurentActivity(WFSAID, WFSID, workflow.WFCreateUserID.Value, 2, WFDID, out oldStatus);
            //更新或者增加当前环节用户处理的意见---返回当前环节用户需要处理的编号
            string WFSUID = function_WF_DealCurentActivityUser(WFSAID, workflow.WFCreateUserID.Value, dealContent, 2, DateTime.Now, "false", WFSNAME, workflow.WFCreateUserID.Value, Remark);
            //增加当前处理用户的附件
            switch (workflow.FunctionName)
            {
                case "XTGL_ZFSJS"://执法事件
                    function_WF_WorkFlowAttrach_zfsj(WFSUID, workflow.files, workflow.FileSource);
                    break;
                case "OA_TASKS"://办公事件
                    function_WF_WorkFlowAttrach_tasks(WFSUID, workflow.fileUpload, workflow.FileSource);
                    break;
                case "GCGL_SIMPLES"://简易工程
                    function_WF_WorkFlowAttrach_simples(WFSUID, workflow.fileUpload, workflow.FileSource);
                    break;
                default:
                    break;
            }

            #endregion
            //当前状态为处理，则说明是第一个人处理，则需要增加下一个环节，如果当前环节已经处理，则不需要增加下一个环节
            string NextWFSAID = "";
            if (oldStatus == 1)
            {
                //获取该环节是否为最后一个环节
                WF_WORKFLOWDETAILS wfdModel = new WF_WORKFLOWDETAILBLL().GetSingle(NextWFDID);
                //如果下一个环节的子环节存在，则状态为执行中，内容为空，否则下一个环节状态为结束，内容为已结束
                int status_wfsa = 0;
                string content_wfsa = string.Empty;
                if (wfdModel != null && !string.IsNullOrEmpty(wfdModel.NEXTID))
                {
                    status_wfsa = 1;
                    content_wfsa = "";
                }
                else
                {
                    //结束该流程
                    WFSModel = new WF_WORKFLOWSPECIFICSBLL().GetSingle(WFSID);
                    if (WFSModel != null)
                    {
                        WFSModel.STATUS = 2;
                        new WF_WORKFLOWSPECIFICSBLL().Update(WFSModel);
                    }
                    status_wfsa = 2;
                    content_wfsa = "已结束";
                }

                #region 增加下一个环节

                //增加环节
                NextWFSAID = function_WF_DealCurentActivity("", WFSID, workflow.WFCreateUserID.Value, status_wfsa, NextWFDID, out oldStatus);

                #region 更新活动实例的当前环节编号
                WFSModel = new WF_WORKFLOWSPECIFICSBLL().GetSingle(WFSID);
                if (WFSModel != null)
                {
                    WFSModel.CURRENTWFSAID = NextWFSAID;
                    WFSModel.STATUS = status_wfsa;
                    new WF_WORKFLOWSPECIFICSBLL().Update(WFSModel);
                }
                #endregion

                //增加下一个环节的能操作的用户
                if (!string.IsNullOrEmpty(NextWFUSERIDS))
                {
                    string[] NextWFUSERIDS_split = NextWFUSERIDS.Split(',');
                    foreach (var item in NextWFUSERIDS_split)
                    {
                        decimal userid;
                        DateTime? dealTime = null;
                        if (decimal.TryParse(item, out userid))
                        {
                            if (status_wfsa == 2)
                            {
                                userid = workflow.WFCreateUserID.Value;
                                dealTime = DateTime.Now;
                                IsSendMsg = "false";
                            }
                            //增加流程能操作的用户
                            function_WF_DealCurentActivityUser(NextWFSAID, userid, content_wfsa, status_wfsa, dealTime, IsSendMsg, WFSNAME, workflow.WFCreateUserID, Remark);
                        }
                    }
                }

                #endregion
            }
            #endregion
            return WFSID + "," + NextWFSAID;
        }


        /// <summary>
        /// 更新或增加当前流程的具体事例
        /// </summary>
        /// <param name="CurrentWFSAID">环节实例编号</param>
        /// <param name="UserID">当前处理的用户</param>
        /// <param name="STATUS">当前环节状态</param>
        /// <param name="WFDID">环节编号</param>
        /// <param name="oldStatus">返回当前环节是否已经处理完成 1：未处理，2已经处理</param>
        /// <returns>返回当前环节编号</returns>
        public string function_WF_DealCurentActivity(string WFSAID, string WFSID, decimal UserID, decimal STATUS, string WFDID, out decimal oldStatus)
        {
            oldStatus = 1;
            WF_WORKFLOWSPECIFICACTIVITYS wfsaModel = new WF_WORKFLOWSPECIFICACTIVITYSBLL().GetSingle(WFSAID);
            #region 处理当前环节
            if (wfsaModel != null)//当前环节，如果存在，则修改状态，和更新处理人
            {
                if (wfsaModel.STATUS != 2)//状态为已经更新，则就不需要更新
                {
                    //更新环节
                    wfsaModel.STATUS = 2;
                    wfsaModel.DEALTIME = DateTime.Now;
                    wfsaModel.DEALUSERID = UserID;
                    new WF_WORKFLOWSPECIFICACTIVITYSBLL().Update(wfsaModel);
                }
                else
                {
                    oldStatus = 2;
                }

                WFSAID = wfsaModel.WFSAID;
            }
            else//否则增加状态
            {
                //增加当前环节
                wfsaModel = new WF_WORKFLOWSPECIFICACTIVITYS();
                wfsaModel.WFSAID = GetNewId();
                wfsaModel.CREATETIME = DateTime.Now;
                wfsaModel.WFSID = WFSID;
                wfsaModel.STATUS = STATUS;
                wfsaModel.DEALUSERID = UserID;
                wfsaModel.DEALTIME = DateTime.Now;
                wfsaModel.WFDID = WFDID;
                new WF_WORKFLOWSPECIFICACTIVITYSBLL().Add(wfsaModel);

                WFSAID = wfsaModel.WFSAID;
            }
            #endregion

            return WFSAID;
        }

        /// <summary>
        /// 更新当前环节用户处理的意见
        /// </summary>
        /// <param name="WFSAID">当前环节实例编号</param>
        /// <param name="UserID">处理的人员编号</param>
        /// <param name="CONTENT">处理的意见</param>
        /// <param name="STATUS">当前状态</param>
        /// <param name="STATUS">处理时间</param>
        /// <param name="IsSendMsg">是否发送短信</param>
        /// <param name="OATitle">公文标题</param>
        /// <param name="ISMAINWF">是否主流程</param>
        /// <returns></returns>
        public string function_WF_DealCurentActivityUser(string WFSAID, decimal UserID, string CONTENT, decimal STATUS, DateTime? DEALTIME, string IsSendMsg, string OATitle, decimal? WFCreateUserID, string Remark)
        {
            WF_WORKFLOWSPECIFICUSERSBLL wfsuBLL = new WF_WORKFLOWSPECIFICUSERSBLL();
            string WFSUID = string.Empty;
            //更新当前环节登录用户的状态及处理的意见
            WF_WORKFLOWSPECIFICUSERS wfsuModel = wfsuBLL.GetList()
                .FirstOrDefault(a => a.WFSAID == WFSAID && a.USERID == UserID);
            if (wfsuModel != null)
            {
                wfsuModel.CONTENT = CONTENT;
                wfsuModel.DEALTIME = DEALTIME;
                wfsuModel.STATUS = 2;
                wfsuModel.REMARK = Remark;
                wfsuModel.ISMAINUSER = 1;//是主控人
                wfsuBLL.Update(wfsuModel);
                //返回当前处理人对应的编号
                WFSUID = wfsuModel.WFSUID;
            }
            else
            {
                wfsuModel = new WF_WORKFLOWSPECIFICUSERS();
                wfsuModel.WFSUID = GetNewId();
                wfsuModel.USERID = UserID;
                wfsuModel.CONTENT = CONTENT;
                wfsuModel.DEALTIME = DEALTIME;
                wfsuModel.REMARK = Remark;
                wfsuModel.STATUS = STATUS;
                wfsuModel.WFSAID = WFSAID;
                wfsuModel.CREATEUSERID = WFCreateUserID;
                wfsuModel.CREATETIME = DateTime.Now;
                wfsuBLL.Add(wfsuModel);
                //返回当前处理人对应的编号
                WFSUID = wfsuModel.WFSUID;

                #region 是否发送短信
                //if (IsSendMsg == "true")
                //{
                //    SYS_USERS uSmodel = UserBLL.GetUserByUserID(UserID);
                //    if (uSmodel != null && !string.IsNullOrEmpty(uSmodel.PHONE))
                //    {
                //        string phone = uSmodel.PHONE;

                //        //phone = "15858196099";
                //        string megContent = "您有一条新的工作需要办理，工作名称为:" + OATitle;

                //        SMSUtility.SendMessage(phone, megContent + "[" +uSmodel.USERNAME + "]", DateTime.Now.Ticks);
                //    }
                //}
                #endregion

            }
            return WFSUID;
        }

        /// <summary>
        /// 增加当前处理用户的附件
        /// </summary>
        /// <param name="WFSUID">当前环节处理用户对应的编号</param>
        public void function_WF_WorkFlowAttrach_zfsj(string WFSUID, List<FileClass> files, decimal? fileSource)
        {
            if (files != null && files.Count > 0)
            {
                foreach (FileClass item in files)
                {
                    WF_WORKFLOWSPECIFICUSERFILES wfsufModel = new WF_WORKFLOWSPECIFICUSERFILES();
                    wfsufModel.FILEID = GetNewId();
                    wfsufModel.WFSUID = WFSUID;
                    wfsufModel.FILESOURCE = fileSource;
                    wfsufModel.FILEPATH = item.OriginalPath;
                    wfsufModel.FILENAME = item.OriginalName;
                    wfsufModel.FILEREMARK = "";
                    wfsufModel.FILETYPE = item.OriginalType;
                    new WF_WORKFLOWSPECIFICUSERSFILESBLL().Add(wfsufModel);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// 增加当前处理用户的附件
        /// </summary>
        /// <param name="WFSUID">当前环节处理用户对应的编号</param>
        public void function_WF_WorkFlowAttrach_tasks(string WFSUID, List<FileUploadClass> files, decimal? fileSource)
        {
            if (files != null && files.Count > 0)
            {
                foreach (FileUploadClass item in files)
                {
                    WF_WORKFLOWSPECIFICUSERFILES wfsufModel = new WF_WORKFLOWSPECIFICUSERFILES();
                    wfsufModel.FILEID = GetNewId();
                    wfsufModel.WFSUID = WFSUID;
                    wfsufModel.FILESOURCE = fileSource;
                    wfsufModel.FILEPATH = item.OriginalPath;
                    wfsufModel.FILENAME = item.OriginalName;
                    wfsufModel.FILEREMARK = "";
                    wfsufModel.FILETYPE = item.OriginalType;
                    new WF_WORKFLOWSPECIFICUSERSFILESBLL().Add(wfsufModel);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }



        /// <summary>
        /// 增加当前处理用户的附件
        /// </summary>
        /// <param name="WFSUID">当前环节处理用户对应的编号</param>
        public void function_WF_WorkFlowAttrach_simples(string WFSUID, List<FileUploadClass> files, decimal? fileSource)
        {
            if (files != null && files.Count > 0)
            {
                foreach (FileUploadClass item in files)
                {
                    WF_WORKFLOWSPECIFICUSERFILES wfsufModel = new WF_WORKFLOWSPECIFICUSERFILES();
                    wfsufModel.FILEID = GetNewId();
                    wfsufModel.WFSUID = WFSUID;
                    wfsufModel.FILESOURCE = fileSource;
                    wfsufModel.FILEPATH = item.OriginalPath;
                    wfsufModel.FILENAME = item.OriginalName;
                    wfsufModel.FILEREMARK = "";
                    wfsufModel.FILETYPE = item.OriginalType;
                    new WF_WORKFLOWSPECIFICUSERSFILESBLL().Add(wfsufModel);
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// 协同管理执法的逻辑处理
        /// </summary>
        /// <param name="WFSNAME">流程详细名称</param>
        /// <param name="WFSID">流程详细ID</param>
        /// <param name="zfsj">要处理的表(ZFSJ)</param>
        /// <returns></returns>
        public string function_XTGL_ZFSJ(out string WFSNAME, string WFSID, XTGL_ZFSJS zfsj)
        {
            Entities dbzfsj = new Entities();
            XTGL_ZFSJS xtglzfsjmodel = null;
            string INDID = string.Empty;
            WF_WORKFLOWSPECIFICS wfsmodel = new WF_WORKFLOWSPECIFICSBLL().GetSingle(WFSID);
            if (wfsmodel != null && !string.IsNullOrEmpty(wfsmodel.TABLENAMEID))
            {
                xtglzfsjmodel = dbzfsj.XTGL_ZFSJS.SingleOrDefault(t => t.ZFSJID == wfsmodel.TABLENAMEID);

                if (xtglzfsjmodel == null)
                {
                    xtglzfsjmodel = new XTGL_ZFSJS();
                    INDID = GetNewId();
                }
                if (zfsj.DISPOSELIMIT != null)
                {
                    xtglzfsjmodel.DISPOSELIMIT = zfsj.DISPOSELIMIT;
                    xtglzfsjmodel.OVERTIME = zfsj.OVERTIME;
                    new XTGL_ZFSJSBLL().UpdateASSIGNTIME(xtglzfsjmodel);//如果有处理期限，则更新处理期限
                }
            }
            else
            {
                xtglzfsjmodel = new XTGL_ZFSJS();
                INDID = GetNewId();
            }

            if (!string.IsNullOrEmpty(INDID))//说明不存在
            {
                xtglzfsjmodel.ZFSJID = INDID;
                xtglzfsjmodel.WFID = zfsj.WFID;
                xtglzfsjmodel.EVENTTITLE = zfsj.EVENTTITLE;
                xtglzfsjmodel.SOURCEID = zfsj.SOURCEID;
                xtglzfsjmodel.CONTACT = zfsj.CONTACT;
                xtglzfsjmodel.CONTACTPHONE = zfsj.CONTACTPHONE;
                xtglzfsjmodel.EVENTADDRESS = zfsj.EVENTADDRESS;
                xtglzfsjmodel.EVENTCONTENT = zfsj.EVENTCONTENT;
                xtglzfsjmodel.BCLASSID = zfsj.BCLASSID;
                xtglzfsjmodel.SCLASSID = zfsj.SCLASSID;
                xtglzfsjmodel.FOUNDTIME = zfsj.FOUNDTIME;
                xtglzfsjmodel.OVERTIME = zfsj.OVERTIME;
                xtglzfsjmodel.LEVELNUM = zfsj.LEVELNUM;
                xtglzfsjmodel.CREATETTIME = zfsj.CREATETTIME;
                xtglzfsjmodel.CREATEUSERID = zfsj.CREATEUSERID;
                xtglzfsjmodel.IMEICODE = zfsj.IMEICODE;
                xtglzfsjmodel.DISPOSELIMIT = zfsj.DISPOSELIMIT;
                xtglzfsjmodel.GEOMETRY = zfsj.GEOMETRY;
                xtglzfsjmodel.REMARK1 = zfsj.REMARK1;
                xtglzfsjmodel.REMARK2 = zfsj.REMARK2;
                xtglzfsjmodel.REMARK3 = zfsj.REMARK3;
                xtglzfsjmodel.X2000 = zfsj.X2000;
                xtglzfsjmodel.Y2000 = zfsj.Y2000;
                xtglzfsjmodel.X84 = zfsj.X84;
                xtglzfsjmodel.Y84 = zfsj.Y84;
                xtglzfsjmodel.ZONEID = zfsj.ZONEID;
                xtglzfsjmodel.EVENTCODE = zfsj.EVENTCODE;

            }
            if (!string.IsNullOrEmpty(INDID))
            {
                //添加
                dbzfsj.XTGL_ZFSJS.Add(xtglzfsjmodel);
                dbzfsj.SaveChanges();
            }
            else
            {
                //更新
                XTGL_ZFSJS xtgl_zfsjmodel1 = dbzfsj.XTGL_ZFSJS.SingleOrDefault(a => a.ZFSJID == INDID);
                if (xtgl_zfsjmodel1 != null)
                {
                    xtgl_zfsjmodel1.WFID = zfsj.WFID;
                    xtgl_zfsjmodel1.EVENTTITLE = zfsj.EVENTTITLE;
                    xtgl_zfsjmodel1.SOURCEID = zfsj.SOURCEID;
                    xtgl_zfsjmodel1.CONTACT = zfsj.CONTACT;
                    xtgl_zfsjmodel1.CONTACTPHONE = zfsj.CONTACTPHONE;
                    xtgl_zfsjmodel1.EVENTADDRESS = zfsj.EVENTADDRESS;
                    xtgl_zfsjmodel1.EVENTCONTENT = zfsj.EVENTCONTENT;
                    xtgl_zfsjmodel1.BCLASSID = zfsj.BCLASSID;
                    xtgl_zfsjmodel1.SCLASSID = zfsj.SCLASSID;
                    xtgl_zfsjmodel1.FOUNDTIME = zfsj.FOUNDTIME;
                    xtgl_zfsjmodel1.OVERTIME = zfsj.OVERTIME;
                    xtgl_zfsjmodel1.LEVELNUM = zfsj.LEVELNUM;
                    xtgl_zfsjmodel1.CREATETTIME = zfsj.CREATETTIME;
                    xtgl_zfsjmodel1.CREATEUSERID = zfsj.CREATEUSERID;
                    xtgl_zfsjmodel1.IMEICODE = zfsj.IMEICODE;
                    xtgl_zfsjmodel1.DISPOSELIMIT = zfsj.DISPOSELIMIT;
                    xtgl_zfsjmodel1.GEOMETRY = zfsj.GEOMETRY;
                    xtgl_zfsjmodel1.REMARK1 = zfsj.REMARK1;
                    xtgl_zfsjmodel1.REMARK2 = zfsj.REMARK2;
                    xtgl_zfsjmodel1.REMARK3 = zfsj.REMARK3;
                    xtgl_zfsjmodel1.X2000 = zfsj.X2000;
                    xtgl_zfsjmodel1.Y2000 = zfsj.Y2000;
                    xtgl_zfsjmodel1.X84 = zfsj.X84;
                    xtgl_zfsjmodel1.Y84 = zfsj.Y84;
                    xtgl_zfsjmodel1.ZONEID = zfsj.ZONEID;
                    xtgl_zfsjmodel1.EVENTCODE = zfsj.EVENTCODE;
                    dbzfsj.SaveChanges();
                }
            }


            if (string.IsNullOrEmpty(INDID))
            {
                INDID = xtglzfsjmodel.ZFSJID;
            }

            WFSNAME = zfsj.EVENTTITLE;
            return INDID;
        }

        /// <summary>
        /// 协同办公任务逻辑处理
        /// </summary>
        /// <param name="WFSNAME">流程详细名称</param>
        /// <param name="WFSID">流程详细ID</param>
        /// <param name="zfsj">要处理的表(ZFSJ)</param>
        /// <returns></returns>
        public string function_OA_TASKS(out string WFSNAME, string WFSID, OA_TASKS tasks)
        {
            Entities dbzfsj = new Entities();
            OA_TASKS xtbgtasksmodel = null;
            string INDID = string.Empty;
            WF_WORKFLOWSPECIFICS wfsmodel = new WF_WORKFLOWSPECIFICSBLL().GetSingle(WFSID);
            if (wfsmodel != null && !string.IsNullOrEmpty(wfsmodel.TABLENAMEID))
            {
                xtbgtasksmodel = dbzfsj.OA_TASKS.SingleOrDefault(t => t.TASKID == wfsmodel.TABLENAMEID);

                if (xtbgtasksmodel == null)
                {
                    xtbgtasksmodel = new OA_TASKS();
                    INDID = GetNewId();
                }
                //if (zfsj.DISPOSELIMIT != null)
                //{
                //    xtglzfsjmodel.DISPOSELIMIT = zfsj.DISPOSELIMIT;
                //    xtglzfsjmodel.OVERTIME = zfsj.OVERTIME;
                //    new XTGL_ZFSJSBLL().UpdateASSIGNTIME(xtglzfsjmodel);//如果有处理期限，则更新处理期限
                //}
            }
            else
            {
                xtbgtasksmodel = new OA_TASKS();
                INDID = GetNewId();
            }

            if (!string.IsNullOrEmpty(INDID))//说明不存在
            {
                xtbgtasksmodel.TASKID = INDID;
                xtbgtasksmodel.TASKTITLE = tasks.TASKTITLE;
                xtbgtasksmodel.FINISHTIME = tasks.FINISHTIME;
                xtbgtasksmodel.TASKCONTENT = tasks.TASKCONTENT;
                xtbgtasksmodel.IMPORTANT = tasks.IMPORTANT;
                xtbgtasksmodel.WFID = tasks.WFID;
                xtbgtasksmodel.REMARK1 = tasks.REMARK1;
                xtbgtasksmodel.REMARK2 = tasks.REMARK2;
                xtbgtasksmodel.REMARK3 = tasks.REMARK3;
                xtbgtasksmodel.CREATEUSERID = tasks.CREATEUSERID;
                xtbgtasksmodel.CREATETIME = tasks.CREATETIME;
                xtbgtasksmodel.STATUS = 0;
            }
            if (!string.IsNullOrEmpty(INDID))
            {
                //添加
                dbzfsj.OA_TASKS.Add(xtbgtasksmodel);
                dbzfsj.SaveChanges();
            }
            else
            {
                //更新
                OA_TASKS xtbg_tasksmodel1 = dbzfsj.OA_TASKS.SingleOrDefault(a => a.TASKID == INDID);
                if (xtbg_tasksmodel1 != null)
                {
                    xtbg_tasksmodel1.TASKTITLE = tasks.TASKTITLE;
                    xtbg_tasksmodel1.FINISHTIME = tasks.FINISHTIME;
                    xtbg_tasksmodel1.TASKCONTENT = tasks.TASKCONTENT;
                    xtbg_tasksmodel1.IMPORTANT = tasks.IMPORTANT;
                    xtbg_tasksmodel1.WFID = tasks.WFID;
                    xtbg_tasksmodel1.REMARK1 = tasks.REMARK1;
                    xtbg_tasksmodel1.REMARK2 = tasks.REMARK2;
                    xtbg_tasksmodel1.REMARK3 = tasks.REMARK3;
                    xtbg_tasksmodel1.STATUS = 0;
                    dbzfsj.SaveChanges();
                }
            }


            if (string.IsNullOrEmpty(INDID))
            {
                INDID = xtbgtasksmodel.TASKID;
            }

            WFSNAME = tasks.TASKTITLE;
            return INDID;
        }

        /// <summary>
        /// 简易工程逻辑处理
        /// </summary>
        /// <param name="WFSNAME">流程详细名称</param>
        /// <param name="WFSID">流程详细ID</param>
        /// <param name="zfsj">要处理的表(ZFSJ)</param>
        /// <returns></returns>
        public string function_GCGL_SIMPLES(out string WFSNAME, string WFSID, GCGL_SIMPLES tasks)
        {
            Entities dbzfsj = new Entities();
            GCGL_SIMPLES jygctasksmodel = null;
            string INDID = string.Empty;
            WF_WORKFLOWSPECIFICS wfsmodel = new WF_WORKFLOWSPECIFICSBLL().GetSingle(WFSID);
            if (wfsmodel != null && !string.IsNullOrEmpty(wfsmodel.TABLENAMEID))
            {
                string tablenameid = wfsmodel.TABLENAMEID;
                jygctasksmodel = dbzfsj.GCGL_SIMPLES.SingleOrDefault(t => t.SIMPLEGCID == tablenameid);

                if (jygctasksmodel == null)
                {
                    jygctasksmodel = new GCGL_SIMPLES();
                    INDID = GetNewId();
                }
                //if (zfsj.DISPOSELIMIT != null)
                //{
                //    xtglzfsjmodel.DISPOSELIMIT = zfsj.DISPOSELIMIT;
                //    xtglzfsjmodel.OVERTIME = zfsj.OVERTIME;
                //    new XTGL_ZFSJSBLL().UpdateASSIGNTIME(xtglzfsjmodel);//如果有处理期限，则更新处理期限
                //}
            }
            else
            {
                jygctasksmodel = new GCGL_SIMPLES();
                INDID = GetNewId();
            }

            if (!string.IsNullOrEmpty(INDID))//说明不存在
            {
                jygctasksmodel.SIMPLEGCID = INDID;
                jygctasksmodel.GCNAME = tasks.GCNAME;
                jygctasksmodel.GCNUMBER = tasks.GCNUMBER;
                jygctasksmodel.STARTTIME = tasks.STARTTIME;
                jygctasksmodel.ENDTIME = tasks.ENDTIME;
                jygctasksmodel.CONTENT = tasks.CONTENT;
                jygctasksmodel.WORKPLAN = tasks.WORKPLAN;
                jygctasksmodel.MONEY = tasks.MONEY;
                jygctasksmodel.CREATETIME = tasks.CREATETIME;
                jygctasksmodel.CREATEUSER = tasks.CREATEUSER;
                jygctasksmodel.GEOMETRY = tasks.GEOMETRY;

            }
            if (!string.IsNullOrEmpty(INDID))
            {
                //添加
                dbzfsj.GCGL_SIMPLES.Add(jygctasksmodel);
                dbzfsj.SaveChanges();
            }
            else
            {
                //更新
                GCGL_SIMPLES jygc_tasksmodel1 = dbzfsj.GCGL_SIMPLES.SingleOrDefault(a => a.SIMPLEGCID == INDID);
                if (jygc_tasksmodel1 != null)
                {
                    jygc_tasksmodel1.GCNAME = tasks.GCNAME;
                    jygc_tasksmodel1.GCNUMBER = tasks.GCNUMBER;
                    jygc_tasksmodel1.STARTTIME = tasks.STARTTIME;
                    jygc_tasksmodel1.ENDTIME = tasks.ENDTIME;
                    jygc_tasksmodel1.CONTENT = tasks.CONTENT;
                    jygc_tasksmodel1.WORKPLAN = tasks.WORKPLAN;
                    jygc_tasksmodel1.MONEY = tasks.MONEY;
                    jygc_tasksmodel1.GEOMETRY = tasks.GEOMETRY;
                    dbzfsj.SaveChanges();
                }
            }


            if (string.IsNullOrEmpty(INDID))
            {
                INDID = jygctasksmodel.SIMPLEGCID.ToString();
            }

            WFSNAME = tasks.GCNAME;
            return INDID;
        }

        /// <summary>
        /// 根据传入的条件查询事件
        /// </summary>
        /// <param name="stypeid">查询的标识 0:全部事件 1:待审核事件 2:代办理事件</param>
        /// <returns></returns>
        public IList<XTGL_ZFSJS> GetXTGLZFSJSInfo(int stypeid)
        {
            Entities xtglzfsjinfo = new Entities();

            IQueryable<XTGL_ZFSJS> fas = xtglzfsjinfo.XTGL_ZFSJS;
            IList<XTGL_ZFSJS> zfsjlist = fas.ToList();

            if (stypeid == 0)
            {

            }
            else if (stypeid == 1)
            {

            }
            else if (stypeid == 2)
            {

            }

            return zfsjlist;

        }


        /// <summary>
        /// 获取的编号
        /// </summary>
        private string GetNewId()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(10000, 99999);
        }
    }
}
