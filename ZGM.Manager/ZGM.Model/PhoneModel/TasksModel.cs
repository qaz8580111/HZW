using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGM.Model.PhoneModel
{
   public class TasksModel:OA_TASKS
    {
       /// <summary>
       /// 派遣人员ID
       /// </summary>
       public string SelectUserIds { get; set; }
       /// <summary>
       /// 上传文件1
       /// </summary>
       public string FileStr1 { get; set; }

       /// <summary>
       /// 上传文件1类型
       /// </summary>
       public string FileType1 { get; set; }

       /// <summary>
       /// 上传文件2
       /// </summary>
       public string FileStr2 { get; set; }

       /// <summary>
       /// 上传文件2类型
       /// </summary>
       public string FileType2 { get; set; }

       /// <summary>
       /// 上传文件3
       /// </summary>
       public string FileStr3 { get; set; }

       /// <summary>
       /// 上传文件3类型
       /// </summary>
       public string FileType3 { get; set; }

    }

   public class TaskListInformation {
       /// <summary>
       /// 登陆人ID
       /// </summary>
       public int USERID { get; set; }
       /// <summary>
       /// 第几页
       /// </summary>
       public int page { get; set; }
       /// <summary>
       /// 根据标题查询
       /// </summary>
       public string title { get; set; }
   
   }

   public class TaskSendContent
   {
       public string TASKID { get; set; }
       public string wfdid { get; set; }
       public string wfsaid { get; set; }
       public string wfsid { get; set; }
       /// <summary>
       /// 流程创建人员
       /// </summary>
       public decimal userId { get; set; }
       /// <summary>
       /// 派遣人员
       /// </summary>
       public string SelectUserIds { get; set; }
       /// <summary>
       /// 意见
       /// </summary>
       public string opinion { get; set; }
       /// <summary>
       /// 下一环节人员ID
       /// </summary>
       public string nextuserid { get; set; }
       /// <summary>
       /// 上传文件1
       /// </summary>
       public string FileStr1 { get; set; }

       /// <summary>
       /// 上传文件1类型
       /// </summary>
       public string FileType1 { get; set; }

       /// <summary>
       /// 上传文件2
       /// </summary>
       public string FileStr2 { get; set; }

       /// <summary>
       /// 上传文件2类型
       /// </summary>
       public string FileType2 { get; set; }

       /// <summary>
       /// 上传文件3
       /// </summary>
       public string FileStr3 { get; set; }

       /// <summary>
       /// 上传文件3类型
       /// </summary>
       public string FileType3 { get; set; }

       /// <summary>
       /// 环节编号
       /// </summary>
       public string Link { get; set; }
   }
}
