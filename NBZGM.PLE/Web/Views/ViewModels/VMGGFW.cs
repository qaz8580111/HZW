using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.ZFSJWorkflowModel.Base;

namespace Web.ViewModels
{
    public class VMGGFW
    {
        public decimal EVENTID { get; set; }
        public string EVENTTITLE { get; set; }
        public string REPORTPERSON { get; set; }
        public string PHONE { get; set; }
        public string EVENTSOURCE { get; set; }
        public string EVENTADDRESS { get; set; }
        public string EVENTCONTENT { get; set; }
        public string AUDIOFILE { get; set; }
        public string GEOMETRY { get; set; }
        public string PICTURES { get; set; }
        public Nullable<decimal> USERID { get; set; }
        public Nullable<System.DateTime> CREATETIME { get; set; }
        public Nullable<decimal> CLASSBID { get; set; }
        public string CLASSBNAME { get; set; }
        public Nullable<decimal> CLASSSID { get; set; }
        public string CLASSSNAME { get; set; }
        public Nullable<decimal> STATUE { get; set; }
        public Nullable<System.DateTime> FXSJ { get; set; }
        public Nullable<decimal> ISAUDIT { get; set; }
        public string AUDITOPINION { get; set; }
        public Nullable<System.DateTime> AUDITIME { get; set; }
        public Decimal ISSUEDOPINION { get; set; }
        public string WIID { get; set; }
        public Nullable<decimal> SSDD { get; set; }
        public Nullable<decimal> SSQJID { get; set; }
        public Nullable<DateTime> OVERTIME { get; set; }
        public string COMMENTS { get; set; }
        public string USERNAME { get; set; }
        public string ARCHIVING { get; set; }
        public string DBAJZPYJ { get; set; }
        public string DBAJCLYJ { get; set; }
        public Nullable<decimal> ISDBAJ { get; set; }
        public Nullable<decimal> DBAJZPR { get; set; }
        public string DBAJZPRName { get; set; }
        public Nullable<DateTime> DBAJCLSJ { get; set; }

        public string ZSYDDNAME { get; set; }
        public Nullable<decimal> ARCHIVINGUSER { get; set; }
        /// <summary>
        /// 相关材料
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}