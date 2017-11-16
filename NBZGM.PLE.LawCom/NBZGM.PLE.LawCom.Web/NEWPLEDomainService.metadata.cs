
namespace Taizhou.PLE.LawCom.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // MetadataTypeAttribute 将 ACITIVITYDEFINITIONMetadata 标识为类
    //，以携带 ACITIVITYDEFINITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ACITIVITYDEFINITION.ACITIVITYDEFINITIONMetadata))]
    public partial class ACITIVITYDEFINITION
    {

        // 通过此类可将自定义特性附加到
        //ACITIVITYDEFINITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ACITIVITYDEFINITIONMetadata
        {

            // 元数据类不会实例化。
            private ACITIVITYDEFINITIONMetadata()
            {
            }

            public EntityCollection<ACTIVITYINSTANCE> ACTIVITYINSTANCES { get; set; }

            public EntityCollection<ACTIVITYPERMISSION> ACTIVITYPERMISSIONS { get; set; }

            public string ADDESC { get; set; }

            public decimal ADID { get; set; }

            public string ADNAME { get; set; }

            public EntityCollection<DOCDEFINITIONRELATION> DOCDEFINITIONRELATIONS { get; set; }

            public Nullable<decimal> TIMELIMITS { get; set; }

            public Nullable<decimal> WDID { get; set; }

            public WORKFLOWDEFINITION WORKFLOWDEFINITION { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ACTIVITYDEFINITIONDOCPHASMetadata 标识为类
    //，以携带 ACTIVITYDEFINITIONDOCPHAS 类的其他元数据。
    [MetadataTypeAttribute(typeof(ACTIVITYDEFINITIONDOCPHAS.ACTIVITYDEFINITIONDOCPHASMetadata))]
    public partial class ACTIVITYDEFINITIONDOCPHAS
    {

        // 通过此类可将自定义特性附加到
        //ACTIVITYDEFINITIONDOCPHAS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ACTIVITYDEFINITIONDOCPHASMetadata
        {

            // 元数据类不会实例化。
            private ACTIVITYDEFINITIONDOCPHASMetadata()
            {
            }

            public decimal ADID { get; set; }

            public decimal DPID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ACTIVITYINSTANCEMetadata 标识为类
    //，以携带 ACTIVITYINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ACTIVITYINSTANCE.ACTIVITYINSTANCEMetadata))]
    public partial class ACTIVITYINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //ACTIVITYINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ACTIVITYINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private ACTIVITYINSTANCEMetadata()
            {
            }

            public ACITIVITYDEFINITION ACITIVITYDEFINITION { get; set; }

            public ACTIVITYINSTANCE ACTIVITYINSTANCE1 { get; set; }

            public EntityCollection<ACTIVITYINSTANCE> ACTIVITYINSTANCES1 { get; set; }

            public ACTIVITYSTATUS ACTIVITYSTATUS { get; set; }

            public Nullable<decimal> ACTIVITYSTATUSID { get; set; }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<decimal> ASSIGNUSERID { get; set; }

            public Nullable<DateTime> DELIVERYTIME { get; set; }

            public EntityCollection<DOCINSTANCE> DOCINSTANCES { get; set; }

            public Nullable<DateTime> EXPIRATIONTIME { get; set; }

            public Nullable<decimal> FROMUSERID { get; set; }

            public string PREVIOUSAIID { get; set; }

            public Nullable<DateTime> PROCESSTIME { get; set; }

            public Nullable<decimal> PROCESSUSERID { get; set; }

            public string WIID { get; set; }

            public WORKFLOWINSTANCE WORKFLOWINSTANCE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ACTIVITYPERMISSIONMetadata 标识为类
    //，以携带 ACTIVITYPERMISSION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ACTIVITYPERMISSION.ACTIVITYPERMISSIONMetadata))]
    public partial class ACTIVITYPERMISSION
    {

        // 通过此类可将自定义特性附加到
        //ACTIVITYPERMISSION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ACTIVITYPERMISSIONMetadata
        {

            // 元数据类不会实例化。
            private ACTIVITYPERMISSIONMetadata()
            {
            }

            public ACITIVITYDEFINITION ACITIVITYDEFINITION { get; set; }

            public ACTIVITYPERMISSIONTYPE ACTIVITYPERMISSIONTYPE { get; set; }

            public Nullable<decimal> ACTIVITYPERMISSIONTYPEID { get; set; }

            public decimal ADID { get; set; }

            public Nullable<decimal> POSITIONID { get; set; }

            public decimal REGIONID { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ACTIVITYPERMISSIONTYPEMetadata 标识为类
    //，以携带 ACTIVITYPERMISSIONTYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ACTIVITYPERMISSIONTYPE.ACTIVITYPERMISSIONTYPEMetadata))]
    public partial class ACTIVITYPERMISSIONTYPE
    {

        // 通过此类可将自定义特性附加到
        //ACTIVITYPERMISSIONTYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ACTIVITYPERMISSIONTYPEMetadata
        {

            // 元数据类不会实例化。
            private ACTIVITYPERMISSIONTYPEMetadata()
            {
            }

            public EntityCollection<ACTIVITYPERMISSION> ACTIVITYPERMISSIONS { get; set; }

            public decimal ACTIVITYPERMISSIONTYPEID { get; set; }

            public string ACTIVITYPERMISSIONTYPENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ACTIVITYSTATUSMetadata 标识为类
    //，以携带 ACTIVITYSTATUS 类的其他元数据。
    [MetadataTypeAttribute(typeof(ACTIVITYSTATUS.ACTIVITYSTATUSMetadata))]
    public partial class ACTIVITYSTATUS
    {

        // 通过此类可将自定义特性附加到
        //ACTIVITYSTATUS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ACTIVITYSTATUSMetadata
        {

            // 元数据类不会实例化。
            private ACTIVITYSTATUSMetadata()
            {
            }

            public EntityCollection<ACTIVITYINSTANCE> ACTIVITYINSTANCES { get; set; }

            public decimal ACTIVITYSTATUSID { get; set; }

            public string ACTIVITYSTATUSNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 APPVERSIONMetadata 标识为类
    //，以携带 APPVERSION 类的其他元数据。
    [MetadataTypeAttribute(typeof(APPVERSION.APPVERSIONMetadata))]
    public partial class APPVERSION
    {

        // 通过此类可将自定义特性附加到
        //APPVERSION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class APPVERSIONMetadata
        {

            // 元数据类不会实例化。
            private APPVERSIONMetadata()
            {
            }

            public decimal VERSIONCODE { get; set; }

            public string VERSIONNAME { get; set; }

            public Nullable<decimal> VERSIONSIZE { get; set; }

            public string VERSIONURL { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ARTICLEMetadata 标识为类
    //，以携带 ARTICLE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ARTICLE.ARTICLEMetadata))]
    public partial class ARTICLE
    {

        // 通过此类可将自定义特性附加到
        //ARTICLE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ARTICLEMetadata
        {

            // 元数据类不会实例化。
            private ARTICLEMetadata()
            {
            }

            public Nullable<decimal> APPROVALSTATUSID { get; set; }

            public Nullable<DateTime> APPROVALTIME { get; set; }

            public Nullable<decimal> APPROVALUSERID { get; set; }

            public decimal ARTICLEID { get; set; }

            public string AUTHOR { get; set; }

            public Nullable<decimal> CATEGORYID { get; set; }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public Nullable<decimal> CREATEDUSERID { get; set; }

            public PORTALCATEGORy PORTALCATEGORy { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public string TITLE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CARSYNCPOSITIONMetadata 标识为类
    //，以携带 CARSYNCPOSITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(CARSYNCPOSITION.CARSYNCPOSITIONMetadata))]
    public partial class CARSYNCPOSITION
    {

        // 通过此类可将自定义特性附加到
        //CARSYNCPOSITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CARSYNCPOSITIONMetadata
        {

            // 元数据类不会实例化。
            private CARSYNCPOSITIONMetadata()
            {
            }

            public Nullable<decimal> ACC { get; set; }

            public string CARNO { get; set; }

            public decimal CARSYNCPOSITIONID { get; set; }

            public Nullable<decimal> DIRECTION { get; set; }

            public Nullable<decimal> ISHIGHQUALTITY { get; set; }

            public Nullable<decimal> LAT { get; set; }

            public Nullable<decimal> LON { get; set; }

            public Nullable<DateTime> POSITIONINGTIME { get; set; }

            public Nullable<decimal> SPEED { get; set; }

            public string STATUSDESC { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CARTYPEMetadata 标识为类
    //，以携带 CARTYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(CARTYPE.CARTYPEMetadata))]
    public partial class CARTYPE
    {

        // 通过此类可将自定义特性附加到
        //CARTYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CARTYPEMetadata
        {

            // 元数据类不会实例化。
            private CARTYPEMetadata()
            {
            }

            public decimal CARTYPEID { get; set; }

            public string CARTYPENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CASEPHONESMSMetadata 标识为类
    //，以携带 CASEPHONESMS 类的其他元数据。
    [MetadataTypeAttribute(typeof(CASEPHONESMS.CASEPHONESMSMetadata))]
    public partial class CASEPHONESMS
    {

        // 通过此类可将自定义特性附加到
        //CASEPHONESMS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CASEPHONESMSMetadata
        {

            // 元数据类不会实例化。
            private CASEPHONESMSMetadata()
            {
            }

            public string AIID { get; set; }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> DESPATCHERID { get; set; }

            public string ID { get; set; }

            public Nullable<decimal> SENDEEID { get; set; }

            public Nullable<decimal> TYPEID { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CASESOURCEMetadata 标识为类
    //，以携带 CASESOURCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(CASESOURCE.CASESOURCEMetadata))]
    public partial class CASESOURCE
    {

        // 通过此类可将自定义特性附加到
        //CASESOURCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CASESOURCEMetadata
        {

            // 元数据类不会实例化。
            private CASESOURCEMetadata()
            {
            }

            public decimal CASESOURCEID { get; set; }

            public string CASESOURCENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CONSTRSITEMetadata 标识为类
    //，以携带 CONSTRSITE 类的其他元数据。
    [MetadataTypeAttribute(typeof(CONSTRSITE.CONSTRSITEMetadata))]
    public partial class CONSTRSITE
    {

        // 通过此类可将自定义特性附加到
        //CONSTRSITE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CONSTRSITEMetadata
        {

            // 元数据类不会实例化。
            private CONSTRSITEMetadata()
            {
            }

            public string CONSTRCOMPANY { get; set; }

            public string CONSTRCONTACT { get; set; }

            public string CONSTRPHONE { get; set; }

            public decimal CONSTRSITEID { get; set; }

            public string CONSTRSITENAME { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<decimal> ISDELETED { get; set; }

            public Nullable<decimal> ISSYNC { get; set; }

            public Nullable<decimal> NIJIANG { get; set; }

            public string OWNERCOMPANY { get; set; }

            public string OWNERCONTACT { get; set; }

            public string OWNERPHONE { get; set; }

            public string PROJECTADDRESS { get; set; }

            public string PROJECTLICENSE { get; set; }

            public string PROJECTNAME { get; set; }

            public string PROJECTSCALE { get; set; }

            public EntityCollection<TRANSLICENS> TRANSLICENSES { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> ZHATU { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CONTACTMetadata 标识为类
    //，以携带 CONTACT 类的其他元数据。
    [MetadataTypeAttribute(typeof(CONTACT.CONTACTMetadata))]
    public partial class CONTACT
    {

        // 通过此类可将自定义特性附加到
        //CONTACT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CONTACTMetadata
        {

            // 元数据类不会实例化。
            private CONTACTMetadata()
            {
            }

            public string ADDRESS { get; set; }

            public Nullable<decimal> CONTACTGROUPID { get; set; }

            public decimal CONTACTID { get; set; }

            public string CONTACTNAME { get; set; }

            public CONTACTSGROUP CONTACTSGROUP { get; set; }

            public string GDNUMBERS { get; set; }

            public string PHONENUMBER { get; set; }

            public string REMARK { get; set; }

            public string WORKDW { get; set; }
        }
    }

    // MetadataTypeAttribute 将 CONTACTSGROUPMetadata 标识为类
    //，以携带 CONTACTSGROUP 类的其他元数据。
    [MetadataTypeAttribute(typeof(CONTACTSGROUP.CONTACTSGROUPMetadata))]
    public partial class CONTACTSGROUP
    {

        // 通过此类可将自定义特性附加到
        //CONTACTSGROUP 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CONTACTSGROUPMetadata
        {

            // 元数据类不会实例化。
            private CONTACTSGROUPMetadata()
            {
            }

            public EntityCollection<CONTACT> CONTACTS { get; set; }

            public decimal CONTACTSGROUPID { get; set; }

            public string CONTACTSGROUPNAME { get; set; }

            public Nullable<decimal> CREATEDUSERID { get; set; }

            public Nullable<decimal> SEQNO { get; set; }
        }
    }

    // MetadataTypeAttribute 将 DOCDEFINITIONMetadata 标识为类
    //，以携带 DOCDEFINITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(DOCDEFINITION.DOCDEFINITIONMetadata))]
    public partial class DOCDEFINITION
    {

        // 通过此类可将自定义特性附加到
        //DOCDEFINITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DOCDEFINITIONMetadata
        {

            // 元数据类不会实例化。
            private DOCDEFINITIONMetadata()
            {
            }

            public decimal DDID { get; set; }

            public string DDNAME { get; set; }

            public EntityCollection<DOCDEFINITIONRELATION> DOCDEFINITIONRELATIONS { get; set; }

            public EntityCollection<DOCINSTANCE> DOCINSTANCES { get; set; }

            public Nullable<decimal> ISAUTOBUILD { get; set; }

            public Nullable<decimal> ISREQUIRED { get; set; }

            public Nullable<decimal> ISUNIQUE { get; set; }

            public Nullable<decimal> RELEVANT { get; set; }

            public Nullable<decimal> SEQNO { get; set; }
        }
    }

    // MetadataTypeAttribute 将 DOCDEFINITIONRELATIONMetadata 标识为类
    //，以携带 DOCDEFINITIONRELATION 类的其他元数据。
    [MetadataTypeAttribute(typeof(DOCDEFINITIONRELATION.DOCDEFINITIONRELATIONMetadata))]
    public partial class DOCDEFINITIONRELATION
    {

        // 通过此类可将自定义特性附加到
        //DOCDEFINITIONRELATION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DOCDEFINITIONRELATIONMetadata
        {

            // 元数据类不会实例化。
            private DOCDEFINITIONRELATIONMetadata()
            {
            }

            public ACITIVITYDEFINITION ACITIVITYDEFINITION { get; set; }

            public decimal ADID { get; set; }

            public decimal DDID { get; set; }

            public DOCDEFINITION DOCDEFINITION { get; set; }

            public Nullable<decimal> ISREQUIRED { get; set; }
        }
    }

    // MetadataTypeAttribute 将 DOCINSTANCEMetadata 标识为类
    //，以携带 DOCINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(DOCINSTANCE.DOCINSTANCEMetadata))]
    public partial class DOCINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //DOCINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DOCINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private DOCINSTANCEMetadata()
            {
            }

            public ACTIVITYINSTANCE ACTIVITYINSTANCE { get; set; }

            public string AIID { get; set; }

            public string ASSEMBLYNAME { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public Nullable<decimal> DDID { get; set; }

            public string DOCBH { get; set; }

            public DOCDEFINITION DOCDEFINITION { get; set; }

            public string DOCINSTANCEID { get; set; }

            public string DOCNAME { get; set; }

            public string DOCPATH { get; set; }

            public Nullable<decimal> DOCTYPEID { get; set; }

            public Nullable<decimal> DPID { get; set; }

            public string TYPENAME { get; set; }

            public string VALUE { get; set; }

            public string WIID { get; set; }

            public WORKFLOWINSTANCE WORKFLOWINSTANCE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 DOCPHASMetadata 标识为类
    //，以携带 DOCPHAS 类的其他元数据。
    [MetadataTypeAttribute(typeof(DOCPHAS.DOCPHASMetadata))]
    public partial class DOCPHAS
    {

        // 通过此类可将自定义特性附加到
        //DOCPHAS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DOCPHASMetadata
        {

            // 元数据类不会实例化。
            private DOCPHASMetadata()
            {
            }

            public decimal DOCPHASEID { get; set; }

            public string DOCPHASENAME { get; set; }

            public Nullable<decimal> SEQNO { get; set; }
        }
    }

    // MetadataTypeAttribute 将 DUMPINGSITEMetadata 标识为类
    //，以携带 DUMPINGSITE 类的其他元数据。
    [MetadataTypeAttribute(typeof(DUMPINGSITE.DUMPINGSITEMetadata))]
    public partial class DUMPINGSITE
    {

        // 通过此类可将自定义特性附加到
        //DUMPINGSITE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class DUMPINGSITEMetadata
        {

            // 元数据类不会实例化。
            private DUMPINGSITEMetadata()
            {
            }

            public string ADDRESS { get; set; }

            public Nullable<DateTime> APPLIEDDATE { get; set; }

            public string CONTACT { get; set; }

            public decimal DUMPINGSITEID { get; set; }

            public string DUMPINGSITENAME { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<decimal> ISDELETED { get; set; }

            public Nullable<decimal> ISSYNC { get; set; }

            public string OWNER { get; set; }

            public string PHONE { get; set; }

            public EntityCollection<TRANSLICENS> TRANSLICENSES { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 FUNCTIONMetadata 标识为类
    //，以携带 FUNCTION 类的其他元数据。
    [MetadataTypeAttribute(typeof(FUNCTION.FUNCTIONMetadata))]
    public partial class FUNCTION
    {

        // 通过此类可将自定义特性附加到
        //FUNCTION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FUNCTIONMetadata
        {

            // 元数据类不会实例化。
            private FUNCTIONMetadata()
            {
            }

            public string CODE { get; set; }

            public FUNCTION FUNCTION1 { get; set; }

            public decimal FUNCTIONID { get; set; }

            public EntityCollection<FUNCTION> FUNCTIONS1 { get; set; }

            public string ICONPATH { get; set; }

            public string NAME { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public string PATH { get; set; }

            public EntityCollection<ROLEFUNCTION> ROLEFUNCTIONS { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public string URL { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWCASENAMEMetadata 标识为类
    //，以携带 GGFWCASENAME 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWCASENAME.GGFWCASENAMEMetadata))]
    public partial class GGFWCASENAME
    {

        // 通过此类可将自定义特性附加到
        //GGFWCASENAME 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWCASENAMEMetadata
        {

            // 元数据类不会实例化。
            private GGFWCASENAMEMetadata()
            {
            }

            public decimal CID { get; set; }

            public string CNAME { get; set; }

            public EntityCollection<GGFWMONTHLYREPORT> GGFWMONTHLYREPORTS { get; set; }

            public string REMARK { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWEVENTMetadata 标识为类
    //，以携带 GGFWEVENT 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWEVENT.GGFWEVENTMetadata))]
    public partial class GGFWEVENT
    {

        // 通过此类可将自定义特性附加到
        //GGFWEVENT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWEVENTMetadata
        {

            // 元数据类不会实例化。
            private GGFWEVENTMetadata()
            {
            }

            public string AUDIOFILE { get; set; }

            public Nullable<decimal> CLASSBID { get; set; }

            public Nullable<decimal> CLASSSID { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<DateTime> DBAJCLSJ { get; set; }

            public string DBAJCLYJ { get; set; }

            public Nullable<decimal> DBAJZPR { get; set; }

            public string DBAJZPYJ { get; set; }

            public Nullable<decimal> DEALINGUSERID { get; set; }

            public string EVENTADDRESS { get; set; }

            public string EVENTCONTENT { get; set; }

            public decimal EVENTID { get; set; }

            public string EVENTSOURCE { get; set; }

            public string EVENTTITLE { get; set; }

            public Nullable<DateTime> FXSJ { get; set; }

            public string GEOMETRY { get; set; }

            public EntityCollection<GGFWTOZFZD> GGFWTOZFZDS { get; set; }

            public string GUIDONLY { get; set; }

            public Nullable<decimal> ISDBAJ { get; set; }

            public Nullable<decimal> JRZB { get; set; }

            public Nullable<DateTime> OVERTIME { get; set; }

            public string PHONE { get; set; }

            public string PICTURES { get; set; }

            public string REPORTPERSON { get; set; }

            public Nullable<decimal> STATUE { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWMONTHLYREPORTMetadata 标识为类
    //，以携带 GGFWMONTHLYREPORT 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWMONTHLYREPORT.GGFWMONTHLYREPORTMetadata))]
    public partial class GGFWMONTHLYREPORT
    {

        // 通过此类可将自定义特性附加到
        //GGFWMONTHLYREPORT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWMONTHLYREPORTMetadata
        {

            // 元数据类不会实例化。
            private GGFWMONTHLYREPORTMetadata()
            {
            }

            public Nullable<decimal> CID { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string FRACTION { get; set; }

            public GGFWCASENAME GGFWCASENAME { get; set; }

            public Nullable<decimal> GGFWSID { get; set; }

            public GGFWSOURCE GGFWSOURCE { get; set; }

            public decimal MREPORTID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWSOURCEMetadata 标识为类
    //，以携带 GGFWSOURCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWSOURCE.GGFWSOURCEMetadata))]
    public partial class GGFWSOURCE
    {

        // 通过此类可将自定义特性附加到
        //GGFWSOURCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWSOURCEMetadata
        {

            // 元数据类不会实例化。
            private GGFWSOURCEMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public EntityCollection<GGFWMONTHLYREPORT> GGFWMONTHLYREPORTS { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public decimal SOURCEID { get; set; }

            public string SOURCENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWSTATUEMetadata 标识为类
    //，以携带 GGFWSTATUE 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWSTATUE.GGFWSTATUEMetadata))]
    public partial class GGFWSTATUE
    {

        // 通过此类可将自定义特性附加到
        //GGFWSTATUE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWSTATUEMetadata
        {

            // 元数据类不会实例化。
            private GGFWSTATUEMetadata()
            {
            }

            public decimal STATUEID { get; set; }

            public string STATUENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWTOZFZDMetadata 标识为类
    //，以携带 GGFWTOZFZD 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWTOZFZD.GGFWTOZFZDMetadata))]
    public partial class GGFWTOZFZD
    {

        // 通过此类可将自定义特性附加到
        //GGFWTOZFZD 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWTOZFZDMetadata
        {

            // 元数据类不会实例化。
            private GGFWTOZFZDMetadata()
            {
            }

            public string ARCHIVING { get; set; }

            public Nullable<DateTime> ARCHIVINGTIME { get; set; }

            public Nullable<decimal> ARCHIVINGUSER { get; set; }

            public string COMMENTS { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> EVENTID { get; set; }

            public GGFWEVENT GGFWEVENT { get; set; }

            public Nullable<decimal> ISCURRENT { get; set; }

            public string REFUSECONTENT { get; set; }

            public decimal TOZFZDID { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string WIID { get; set; }

            public Nullable<decimal> ZDUSERID { get; set; }

            public ZFSJWORKFLOWINSTANCE ZFSJWORKFLOWINSTANCE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GGFWXFDOCMetadata 标识为类
    //，以携带 GGFWXFDOC 类的其他元数据。
    [MetadataTypeAttribute(typeof(GGFWXFDOC.GGFWXFDOCMetadata))]
    public partial class GGFWXFDOC
    {

        // 通过此类可将自定义特性附加到
        //GGFWXFDOC 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GGFWXFDOCMetadata
        {

            // 元数据类不会实例化。
            private GGFWXFDOCMetadata()
            {
            }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public string DOCCODE { get; set; }

            public string DOCID { get; set; }

            public string DOCNAME { get; set; }

            public string DOCURL { get; set; }

            public Nullable<decimal> EVETID { get; set; }

            public Nullable<decimal> TYPEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 GROUPMetadata 标识为类
    //，以携带 GROUP 类的其他元数据。
    [MetadataTypeAttribute(typeof(GROUP.GROUPMetadata))]
    public partial class GROUP
    {

        // 通过此类可将自定义特性附加到
        //GROUP 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GROUPMetadata
        {

            // 元数据类不会实例化。
            private GROUPMetadata()
            {
            }

            public Nullable<DateTime> CREATEDATE { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public decimal GROUPID { get; set; }

            public string GROUPNAME { get; set; }

            public Nullable<decimal> PARENTID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ILLEGALCLASSMetadata 标识为类
    //，以携带 ILLEGALCLASS 类的其他元数据。
    [MetadataTypeAttribute(typeof(ILLEGALCLASS.ILLEGALCLASSMetadata))]
    public partial class ILLEGALCLASS
    {

        // 通过此类可将自定义特性附加到
        //ILLEGALCLASS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ILLEGALCLASSMetadata
        {

            // 元数据类不会实例化。
            private ILLEGALCLASSMetadata()
            {
            }

            public decimal ILLEGALCLASSID { get; set; }

            public string ILLEGALCLASSNAME { get; set; }

            public Nullable<decimal> ILLEGALCLASSTYPEID { get; set; }

            public Nullable<decimal> ILLEGALCODE { get; set; }

            public EntityCollection<ILLEGALITEM> ILLEGALITEMS { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public string PATH { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ILLEGALITEMMetadata 标识为类
    //，以携带 ILLEGALITEM 类的其他元数据。
    [MetadataTypeAttribute(typeof(ILLEGALITEM.ILLEGALITEMMetadata))]
    public partial class ILLEGALITEM
    {

        // 通过此类可将自定义特性附加到
        //ILLEGALITEM 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ILLEGALITEMMetadata
        {

            // 元数据类不会实例化。
            private ILLEGALITEMMetadata()
            {
            }

            public string FZZE { get; set; }

            public ILLEGALCLASS ILLEGALCLASS { get; set; }

            public Nullable<decimal> ILLEGALCLASSID { get; set; }

            public string ILLEGALCODE { get; set; }

            public decimal ILLEGALITEMID { get; set; }

            public string ILLEGALITEMNAME { get; set; }

            public string PENALTYCONTENT { get; set; }

            public string WEIZE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 LAYERTYPEMetadata 标识为类
    //，以携带 LAYERTYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(LAYERTYPE.LAYERTYPEMetadata))]
    public partial class LAYERTYPE
    {

        // 通过此类可将自定义特性附加到
        //LAYERTYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LAYERTYPEMetadata
        {

            // 元数据类不会实例化。
            private LAYERTYPEMetadata()
            {
            }

            public decimal ID { get; set; }

            public string NAME { get; set; }

            public string TYPEVALUE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 LEADERWEEKWORKPLANMetadata 标识为类
    //，以携带 LEADERWEEKWORKPLAN 类的其他元数据。
    [MetadataTypeAttribute(typeof(LEADERWEEKWORKPLAN.LEADERWEEKWORKPLANMetadata))]
    public partial class LEADERWEEKWORKPLAN
    {

        // 通过此类可将自定义特性附加到
        //LEADERWEEKWORKPLAN 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LEADERWEEKWORKPLANMetadata
        {

            // 元数据类不会实例化。
            private LEADERWEEKWORKPLANMetadata()
            {
            }

            public Nullable<DateTime> ENDDATE { get; set; }

            public Nullable<DateTime> MODIFYTIME { get; set; }

            public Nullable<decimal> MODIFYUSERID { get; set; }

            public string ONDUTYDEPT { get; set; }

            public string ONDUTYLEADER { get; set; }

            public decimal PLANID { get; set; }

            public Nullable<DateTime> PLANTIME { get; set; }

            public Nullable<decimal> PLANUSERID { get; set; }

            public Nullable<DateTime> STARTDATE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 MAPINFOMetadata 标识为类
    //，以携带 MAPINFO 类的其他元数据。
    [MetadataTypeAttribute(typeof(MAPINFO.MAPINFOMetadata))]
    public partial class MAPINFO
    {

        // 通过此类可将自定义特性附加到
        //MAPINFO 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MAPINFOMetadata
        {

            // 元数据类不会实例化。
            private MAPINFOMetadata()
            {
            }

            public string CONTAIN { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string ELEMENTADDRESS { get; set; }

            public string ELEMENTID { get; set; }

            public decimal ID { get; set; }

            public Nullable<decimal> LAYERID { get; set; }

            public string LONGLAT { get; set; }

            public Nullable<decimal> MAPTYPE { get; set; }

            public Nullable<decimal> STATE { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string VALUEDATE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 MENUMetadata 标识为类
    //，以携带 MENU 类的其他元数据。
    [MetadataTypeAttribute(typeof(MENU.MENUMetadata))]
    public partial class MENU
    {

        // 通过此类可将自定义特性附加到
        //MENU 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MENUMetadata
        {

            // 元数据类不会实例化。
            private MENUMetadata()
            {
            }

            public Nullable<decimal> APPID { get; set; }

            public string ICON { get; set; }

            public string MENUCODE { get; set; }

            public decimal MENUID { get; set; }

            public string NAME { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public string URL { get; set; }
        }
    }

    // MetadataTypeAttribute 将 MESSAGEMetadata 标识为类
    //，以携带 MESSAGE 类的其他元数据。
    [MetadataTypeAttribute(typeof(MESSAGE.MESSAGEMetadata))]
    public partial class MESSAGE
    {

        // 通过此类可将自定义特性附加到
        //MESSAGE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MESSAGEMetadata
        {

            // 元数据类不会实例化。
            private MESSAGEMetadata()
            {
            }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public Nullable<decimal> FROMUSERID { get; set; }

            public decimal ISDELETED { get; set; }

            public decimal MESSAGEID { get; set; }

            public Nullable<DateTime> READTIME { get; set; }

            public string SENDCHANNELS { get; set; }

            public string SMSNUMBER { get; set; }

            public string TITLE { get; set; }

            public Nullable<decimal> TOUSERID { get; set; }

            public Nullable<decimal> TYPEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 MESSAGETOUSERMetadata 标识为类
    //，以携带 MESSAGETOUSER 类的其他元数据。
    [MetadataTypeAttribute(typeof(MESSAGETOUSER.MESSAGETOUSERMetadata))]
    public partial class MESSAGETOUSER
    {

        // 通过此类可将自定义特性附加到
        //MESSAGETOUSER 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MESSAGETOUSERMetadata
        {

            // 元数据类不会实例化。
            private MESSAGETOUSERMetadata()
            {
            }

            public decimal MESSAGEID { get; set; }

            public decimal USERID { get; set; }

            public decimal USERTYPEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ONDUTyMetadata 标识为类
    //，以携带 ONDUTy 类的其他元数据。
    [MetadataTypeAttribute(typeof(ONDUTy.ONDUTyMetadata))]
    public partial class ONDUTy
    {

        // 通过此类可将自定义特性附加到
        //ONDUTy 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ONDUTyMetadata
        {

            // 元数据类不会实例化。
            private ONDUTyMetadata()
            {
            }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public decimal ID { get; set; }

            public string ONDUTYNAME { get; set; }

            public Nullable<DateTime> ONDUTYTIME { get; set; }

            public string ONROUTEID { get; set; }

            public Nullable<DateTime> OVERTIME { get; set; }

            public Nullable<decimal> STATUID { get; set; }

            public USER USER { get; set; }

            public EntityCollection<USERONDUTy> USERONDUTIES { get; set; }
        }
    }

    // MetadataTypeAttribute 将 PHONEERRORMetadata 标识为类
    //，以携带 PHONEERROR 类的其他元数据。
    [MetadataTypeAttribute(typeof(PHONEERROR.PHONEERRORMetadata))]
    public partial class PHONEERROR
    {

        // 通过此类可将自定义特性附加到
        //PHONEERROR 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PHONEERRORMetadata
        {

            // 元数据类不会实例化。
            private PHONEERRORMetadata()
            {
            }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public decimal ID { get; set; }

            public Nullable<decimal> STATUID { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 PORTALCATEGORyMetadata 标识为类
    //，以携带 PORTALCATEGORy 类的其他元数据。
    [MetadataTypeAttribute(typeof(PORTALCATEGORy.PORTALCATEGORyMetadata))]
    public partial class PORTALCATEGORy
    {

        // 通过此类可将自定义特性附加到
        //PORTALCATEGORy 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PORTALCATEGORyMetadata
        {

            // 元数据类不会实例化。
            private PORTALCATEGORyMetadata()
            {
            }

            public EntityCollection<ARTICLE> ARTICLES { get; set; }

            public decimal CATEGORYID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string NAME { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public Nullable<decimal> SEQNO { get; set; }
        }
    }

    // MetadataTypeAttribute 将 QUESTIONIDMetadata 标识为类
    //，以携带 QUESTIONID 类的其他元数据。
    [MetadataTypeAttribute(typeof(QUESTIONID.QUESTIONIDMetadata))]
    public partial class QUESTIONID
    {

        // 通过此类可将自定义特性附加到
        //QUESTIONID 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class QUESTIONIDMetadata
        {

            // 元数据类不会实例化。
            private QUESTIONIDMetadata()
            {
            }

            public Nullable<decimal> PARENTID { get; set; }

            public Nullable<decimal> TYPEDJ { get; set; }

            public decimal TYPEID { get; set; }

            public string TYPENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 QUESTIONLISTMetadata 标识为类
    //，以携带 QUESTIONLIST 类的其他元数据。
    [MetadataTypeAttribute(typeof(QUESTIONLIST.QUESTIONLISTMetadata))]
    public partial class QUESTIONLIST
    {

        // 通过此类可将自定义特性附加到
        //QUESTIONLIST 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class QUESTIONLISTMetadata
        {

            // 元数据类不会实例化。
            private QUESTIONLISTMetadata()
            {
            }

            public string DLID { get; set; }

            public decimal ID { get; set; }

            public string PICT1 { get; set; }

            public string PICT2 { get; set; }

            public string PICT3 { get; set; }

            public string SBREN { get; set; }

            public Nullable<DateTime> SBTIME { get; set; }

            public string SFDD { get; set; }

            public string SJXQ { get; set; }

            public string XLID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 RCDCEVENTMetadata 标识为类
    //，以携带 RCDCEVENT 类的其他元数据。
    [MetadataTypeAttribute(typeof(RCDCEVENT.RCDCEVENTMetadata))]
    public partial class RCDCEVENT
    {

        // 通过此类可将自定义特性附加到
        //RCDCEVENT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RCDCEVENTMetadata
        {

            // 元数据类不会实例化。
            private RCDCEVENTMetadata()
            {
            }

            public Nullable<decimal> CLASSBID { get; set; }

            public Nullable<decimal> CLASSSID { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string EVENTADDRESS { get; set; }

            public string EVENTCONTENT { get; set; }

            public decimal EVENTID { get; set; }

            public string EVENTSOURCE { get; set; }

            public string EVENTTITLE { get; set; }

            public Nullable<DateTime> FXSJ { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<decimal> GRADE { get; set; }

            public string GUIDONLY { get; set; }

            public string PICTURES { get; set; }

            public EntityCollection<RCDCTOZFZD> RCDCTOZFZDS { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 RCDCTOZFZDMetadata 标识为类
    //，以携带 RCDCTOZFZD 类的其他元数据。
    [MetadataTypeAttribute(typeof(RCDCTOZFZD.RCDCTOZFZDMetadata))]
    public partial class RCDCTOZFZD
    {

        // 通过此类可将自定义特性附加到
        //RCDCTOZFZD 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RCDCTOZFZDMetadata
        {

            // 元数据类不会实例化。
            private RCDCTOZFZDMetadata()
            {
            }

            public string ARCHIVING { get; set; }

            public Nullable<DateTime> ARCHIVINGTIME { get; set; }

            public Nullable<decimal> ARCHIVINGUSER { get; set; }

            public string COMMENTS { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> EVENTID { get; set; }

            public Nullable<decimal> ISCURRENT { get; set; }

            public RCDCEVENT RCDCEVENT { get; set; }

            public string REFUSECONTENT { get; set; }

            public Nullable<decimal> STATUE { get; set; }

            public decimal TOZFZDID { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string WIID { get; set; }

            public Nullable<decimal> ZDUSERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 RECIPEMetadata 标识为类
    //，以携带 RECIPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(RECIPE.RECIPEMetadata))]
    public partial class RECIPE
    {

        // 通过此类可将自定义特性附加到
        //RECIPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RECIPEMetadata
        {

            // 元数据类不会实例化。
            private RECIPEMetadata()
            {
            }

            public string BREAKFAST { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string DINNER { get; set; }

            public string LUNCH { get; set; }

            public Nullable<DateTime> RECIPEDATE { get; set; }

            public decimal RECIPEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ROLEMetadata 标识为类
    //，以携带 ROLE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ROLE.ROLEMetadata))]
    public partial class ROLE
    {

        // 通过此类可将自定义特性附加到
        //ROLE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ROLEMetadata
        {

            // 元数据类不会实例化。
            private ROLEMetadata()
            {
            }

            public Nullable<decimal> APPID { get; set; }

            public string DESCRIPTION { get; set; }

            public EntityCollection<ROLEFUNCTION> ROLEFUNCTIONS { get; set; }

            public decimal ROLEID { get; set; }

            public string ROLENAME { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public EntityCollection<USERROLE> USERROLES { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ROLEFUNCTIONMetadata 标识为类
    //，以携带 ROLEFUNCTION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ROLEFUNCTION.ROLEFUNCTIONMetadata))]
    public partial class ROLEFUNCTION
    {

        // 通过此类可将自定义特性附加到
        //ROLEFUNCTION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ROLEFUNCTIONMetadata
        {

            // 元数据类不会实例化。
            private ROLEFUNCTIONMetadata()
            {
            }

            public FUNCTION FUNCTION { get; set; }

            public decimal FUNCTIONID { get; set; }

            public string REDUNDANCY { get; set; }

            public ROLE ROLE { get; set; }

            public decimal ROLEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ROLEMENUMetadata 标识为类
    //，以携带 ROLEMENU 类的其他元数据。
    [MetadataTypeAttribute(typeof(ROLEMENU.ROLEMENUMetadata))]
    public partial class ROLEMENU
    {

        // 通过此类可将自定义特性附加到
        //ROLEMENU 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ROLEMENUMetadata
        {

            // 元数据类不会实例化。
            private ROLEMENUMetadata()
            {
            }

            public decimal MENUID { get; set; }

            public decimal ROLEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SCHEDULEMetadata 标识为类
    //，以携带 SCHEDULE 类的其他元数据。
    [MetadataTypeAttribute(typeof(SCHEDULE.SCHEDULEMetadata))]
    public partial class SCHEDULE
    {

        // 通过此类可将自定义特性附加到
        //SCHEDULE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SCHEDULEMetadata
        {

            // 元数据类不会实例化。
            private SCHEDULEMetadata()
            {
            }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATEDITME { get; set; }

            public Nullable<decimal> CREATEDUSERID { get; set; }

            public Nullable<DateTime> ENDTIME { get; set; }

            public Nullable<decimal> ISALLDAYEVENT { get; set; }

            public Nullable<decimal> OWNER { get; set; }

            public decimal SCHEDULEID { get; set; }

            public Nullable<decimal> SCHEDULESOURCEID { get; set; }

            public Nullable<decimal> SCHEDULETYPEID { get; set; }

            public Nullable<decimal> SHARETYPEID { get; set; }

            public Nullable<DateTime> STARTTIME { get; set; }

            public string TITLE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SCHEDULETYPEMetadata 标识为类
    //，以携带 SCHEDULETYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(SCHEDULETYPE.SCHEDULETYPEMetadata))]
    public partial class SCHEDULETYPE
    {

        // 通过此类可将自定义特性附加到
        //SCHEDULETYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SCHEDULETYPEMetadata
        {

            // 元数据类不会实例化。
            private SCHEDULETYPEMetadata()
            {
            }

            public string COLOR { get; set; }

            public Nullable<decimal> ISABLEDELETE { get; set; }

            public decimal SCHEDULETYPEID { get; set; }

            public string SCHEDULETYPENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SIMPLECASMetadata 标识为类
    //，以携带 SIMPLECAS 类的其他元数据。
    [MetadataTypeAttribute(typeof(SIMPLECAS.SIMPLECASMetadata))]
    public partial class SIMPLECAS
    {

        // 通过此类可将自定义特性附加到
        //SIMPLECAS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SIMPLECASMetadata
        {

            // 元数据类不会实例化。
            private SIMPLECASMetadata()
            {
            }

            public string BANKACCOUNT { get; set; }

            public string BANKACCOUNTNAME { get; set; }

            public string CASEADDRESS { get; set; }

            public Nullable<DateTime> CASETIME { get; set; }

            public string DSRGENDER { get; set; }

            public string DSRIDNUMBER { get; set; }

            public string DSRLX { get; set; }

            public string DSRNAME { get; set; }

            public Nullable<decimal> FKJE { get; set; }

            public string FZRADDRESS { get; set; }

            public string FZRNAME { get; set; }

            public string FZRPOSITION { get; set; }

            public Nullable<decimal> ILLEGALITEMID { get; set; }

            public string JDSBH { get; set; }

            public string JKYH { get; set; }

            public Nullable<decimal> LAT { get; set; }

            public Nullable<decimal> LON { get; set; }

            public string PHONEID { get; set; }

            public decimal SIMPLECASEID { get; set; }

            public EntityCollection<SIMPLECASEPICTURE> SIMPLECASEPICTURES { get; set; }

            public Nullable<decimal> UNTIID { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string ZFRNAME { get; set; }

            public Nullable<DateTime> ZFSJ { get; set; }

            public string ZFZH { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SIMPLECASEPICTUREMetadata 标识为类
    //，以携带 SIMPLECASEPICTURE 类的其他元数据。
    [MetadataTypeAttribute(typeof(SIMPLECASEPICTURE.SIMPLECASEPICTUREMetadata))]
    public partial class SIMPLECASEPICTURE
    {

        // 通过此类可将自定义特性附加到
        //SIMPLECASEPICTURE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SIMPLECASEPICTUREMetadata
        {

            // 元数据类不会实例化。
            private SIMPLECASEPICTUREMetadata()
            {
            }

            public byte[] PICTURE { get; set; }

            public Nullable<decimal> PICTURETYPE { get; set; }

            public SIMPLECAS SIMPLECAS { get; set; }

            public decimal SIMPLECASEID { get; set; }

            public decimal SIMPLECASEPICTUREID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SINGNINMetadata 标识为类
    //，以携带 SINGNIN 类的其他元数据。
    [MetadataTypeAttribute(typeof(SINGNIN.SINGNINMetadata))]
    public partial class SINGNIN
    {

        // 通过此类可将自定义特性附加到
        //SINGNIN 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SINGNINMetadata
        {

            // 元数据类不会实例化。
            private SINGNINMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public string ID { get; set; }

            public string PHONEID { get; set; }

            public string PHONEIMEI { get; set; }

            public string SINGNINADDRESS { get; set; }

            public Nullable<DateTime> SINGNINTIME { get; set; }

            public USER USER { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SPECIALACTIVITICEMetadata 标识为类
    //，以携带 SPECIALACTIVITICE 类的其他元数据。
    [MetadataTypeAttribute(typeof(SPECIALACTIVITICE.SPECIALACTIVITICEMetadata))]
    public partial class SPECIALACTIVITICE
    {

        // 通过此类可将自定义特性附加到
        //SPECIALACTIVITICE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SPECIALACTIVITICEMetadata
        {

            // 元数据类不会实例化。
            private SPECIALACTIVITICEMetadata()
            {
            }

            public string ADATA { get; set; }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public Nullable<DateTime> CRETATIME { get; set; }

            public SPECIALACTIVITYDEFINITON SPECIALACTIVITYDEFINITON { get; set; }

            public EntityCollection<SPECIALTOZFSJ> SPECIALTOZFSJS { get; set; }

            public SPECIALWORKFLOWINSTANCE SPECIALWORKFLOWINSTANCE { get; set; }

            public Nullable<decimal> STATUS { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public USER USER { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SPECIALACTIVITYDEFINITONMetadata 标识为类
    //，以携带 SPECIALACTIVITYDEFINITON 类的其他元数据。
    [MetadataTypeAttribute(typeof(SPECIALACTIVITYDEFINITON.SPECIALACTIVITYDEFINITONMetadata))]
    public partial class SPECIALACTIVITYDEFINITON
    {

        // 通过此类可将自定义特性附加到
        //SPECIALACTIVITYDEFINITON 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SPECIALACTIVITYDEFINITONMetadata
        {

            // 元数据类不会实例化。
            private SPECIALACTIVITYDEFINITONMetadata()
            {
            }

            public decimal ADID { get; set; }

            public string NAME { get; set; }

            public EntityCollection<SPECIALACTIVITICE> SPECIALACTIVITICES { get; set; }

            public EntityCollection<SPECIALWORKFLOWINSTANCE> SPECIALWORKFLOWINSTANCES { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SPECIALTOZFSJMetadata 标识为类
    //，以携带 SPECIALTOZFSJ 类的其他元数据。
    [MetadataTypeAttribute(typeof(SPECIALTOZFSJ.SPECIALTOZFSJMetadata))]
    public partial class SPECIALTOZFSJ
    {

        // 通过此类可将自定义特性附加到
        //SPECIALTOZFSJ 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SPECIALTOZFSJMetadata
        {

            // 元数据类不会实例化。
            private SPECIALTOZFSJMetadata()
            {
            }

            public Nullable<decimal> BIGCLASSID { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public Nullable<DateTime> HANDLETIME { get; set; }

            public decimal ID { get; set; }

            public Nullable<decimal> ISCURRENT { get; set; }

            public Nullable<decimal> SMALLCLASSID { get; set; }

            public SPECIALACTIVITICE SPECIALACTIVITICE { get; set; }

            public string SPECIALAIID { get; set; }

            public Nullable<decimal> STATEID { get; set; }

            public USER USER { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string ZFSJWIID { get; set; }

            public string ZXDCTITLE { get; set; }

            public string ZXDCWIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 SPECIALWORKFLOWINSTANCEMetadata 标识为类
    //，以携带 SPECIALWORKFLOWINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(SPECIALWORKFLOWINSTANCE.SPECIALWORKFLOWINSTANCEMetadata))]
    public partial class SPECIALWORKFLOWINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //SPECIALWORKFLOWINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SPECIALWORKFLOWINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private SPECIALWORKFLOWINSTANCEMetadata()
            {
            }

            public Nullable<decimal> ADID { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public Nullable<DateTime> CRETETIME { get; set; }

            public string EXPLAIN { get; set; }

            public string FILES { get; set; }

            public Nullable<DateTime> OVERTIME { get; set; }

            public EntityCollection<SPECIALACTIVITICE> SPECIALACTIVITICES { get; set; }

            public SPECIALACTIVITYDEFINITON SPECIALACTIVITYDEFINITON { get; set; }

            public Nullable<DateTime> STARTTIME { get; set; }

            public Nullable<decimal> STATUS { get; set; }

            public string TITEL { get; set; }

            public USER USER { get; set; }

            public string WDATA { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREESTOREMetadata 标识为类
    //，以携带 STREESTORE 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREESTORE.STREESTOREMetadata))]
    public partial class STREESTORE
    {

        // 通过此类可将自定义特性附加到
        //STREESTORE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREESTOREMetadata
        {

            // 元数据类不会实例化。
            private STREESTOREMetadata()
            {
            }

            public string ADDRESS { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<decimal> ISGSWSXKZ { get; set; }

            public Nullable<decimal> ISHJPL { get; set; }

            public Nullable<decimal> ISMTZP { get; set; }

            public Nullable<decimal> ISPSXKZ { get; set; }

            public string PICTUREURLS { get; set; }

            public string SHOPNAME { get; set; }

            public string SHOPPHONE { get; set; }

            public string SHOPUSERNAME { get; set; }

            public decimal STREESTOREID { get; set; }

            public STREESTORETYPE STREESTORETYPE { get; set; }

            public string STREESTORETYPEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREESTORETYPEMetadata 标识为类
    //，以携带 STREESTORETYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREESTORETYPE.STREESTORETYPEMetadata))]
    public partial class STREESTORETYPE
    {

        // 通过此类可将自定义特性附加到
        //STREESTORETYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREESTORETYPEMetadata
        {

            // 元数据类不会实例化。
            private STREESTORETYPEMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public string PARENTID { get; set; }

            public string PATH { get; set; }

            public EntityCollection<STREESTORE> STREESTORES { get; set; }

            public string STREESTORETYPEID { get; set; }

            public string TYPENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETMetadata 标识为类
    //，以携带 STREET 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREET.STREETMetadata))]
    public partial class STREET
    {

        // 通过此类可将自定义特性附加到
        //STREET 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETMetadata
        {

            // 元数据类不会实例化。
            private STREETMetadata()
            {
            }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public Nullable<decimal> STATUID { get; set; }

            public string STREETADDRESS { get; set; }

            public string STREETCAPTAIN { get; set; }

            public EntityCollection<STREETEXAMINE> STREETEXAMINES { get; set; }

            public string STREETEXPLAIN { get; set; }

            public decimal STREETID { get; set; }

            public string STREETNAME { get; set; }

            public string STREETPERSON { get; set; }

            public STREETTYPE STREETTYPE { get; set; }

            public Nullable<decimal> STREETTYPEID { get; set; }

            public Nullable<decimal> UNITID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETASSESSMENTTYPEMetadata 标识为类
    //，以携带 STREETASSESSMENTTYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREETASSESSMENTTYPE.STREETASSESSMENTTYPEMetadata))]
    public partial class STREETASSESSMENTTYPE
    {

        // 通过此类可将自定义特性附加到
        //STREETASSESSMENTTYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETASSESSMENTTYPEMetadata
        {

            // 元数据类不会实例化。
            private STREETASSESSMENTTYPEMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public decimal ID { get; set; }

            public Nullable<decimal> MARK { get; set; }

            public string NAME { get; set; }

            public Nullable<decimal> PARENTID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETBASMetadata 标识为类
    //，以携带 STREETBAS 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREETBAS.STREETBASMetadata))]
    public partial class STREETBAS
    {

        // 通过此类可将自定义特性附加到
        //STREETBAS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETBASMetadata
        {

            // 元数据类不会实例化。
            private STREETBASMetadata()
            {
            }

            public string ASSESSTITLE { get; set; }

            public string AUDITOPINION { get; set; }

            public Nullable<DateTime> AUDITORTIME { get; set; }

            public string AUDITORUSERID { get; set; }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public string FILES { get; set; }

            public Nullable<decimal> STATUID { get; set; }

            public decimal STREETBASEID { get; set; }

            public EntityCollection<STREETEXAMINE> STREETEXAMINES { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETEXAMINEMetadata 标识为类
    //，以携带 STREETEXAMINE 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREETEXAMINE.STREETEXAMINEMetadata))]
    public partial class STREETEXAMINE
    {

        // 通过此类可将自定义特性附加到
        //STREETEXAMINE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETEXAMINEMetadata
        {

            // 元数据类不会实例化。
            private STREETEXAMINEMetadata()
            {
            }

            public decimal ID { get; set; }

            public Nullable<decimal> STATUID { get; set; }

            public STREET STREET { get; set; }

            public STREETBAS STREETBAS { get; set; }

            public Nullable<decimal> STREETBASEID { get; set; }

            public Nullable<decimal> STREETID { get; set; }

            public EntityCollection<STREETPROBLEM> STREETPROBLEMS { get; set; }

            public EntityCollection<STREETUSER> STREETUSERS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETPROBLEMMetadata 标识为类
    //，以携带 STREETPROBLEM 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREETPROBLEM.STREETPROBLEMMetadata))]
    public partial class STREETPROBLEM
    {

        // 通过此类可将自定义特性附加到
        //STREETPROBLEM 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETPROBLEMMetadata
        {

            // 元数据类不会实例化。
            private STREETPROBLEMMetadata()
            {
            }

            public Nullable<decimal> ACTUALPOINTS { get; set; }

            public string ADDRESS { get; set; }

            public Nullable<decimal> BIGCLASSID { get; set; }

            public string CONTENT { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string DESCRIPTION { get; set; }

            public Nullable<DateTime> EXAMINETIME { get; set; }

            public string FILES { get; set; }

            public decimal ID { get; set; }

            public string LATANDLONG { get; set; }

            public string OPINION { get; set; }

            public Nullable<decimal> POINTSS { get; set; }

            public Nullable<decimal> REPORTID { get; set; }

            public Nullable<decimal> SMALLCLASSID { get; set; }

            public Nullable<decimal> STATUID { get; set; }

            public STREETEXAMINE STREETEXAMINE { get; set; }

            public Nullable<decimal> STREETEXAMINEID { get; set; }

            public Nullable<decimal> SUBCLASSID { get; set; }

            public string TITLE { get; set; }

            public Nullable<decimal> TYPEID { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETTYPEMetadata 标识为类
    //，以携带 STREETTYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREETTYPE.STREETTYPEMetadata))]
    public partial class STREETTYPE
    {

        // 通过此类可将自定义特性附加到
        //STREETTYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETTYPEMetadata
        {

            // 元数据类不会实例化。
            private STREETTYPEMetadata()
            {
            }

            public EntityCollection<STREET> STREETS { get; set; }

            public decimal STREETTYPEID { get; set; }

            public string STREETTYPENAME { get; set; }

            public string STREETTYPEPONT { get; set; }
        }
    }

    // MetadataTypeAttribute 将 STREETUSERMetadata 标识为类
    //，以携带 STREETUSER 类的其他元数据。
    [MetadataTypeAttribute(typeof(STREETUSER.STREETUSERMetadata))]
    public partial class STREETUSER
    {

        // 通过此类可将自定义特性附加到
        //STREETUSER 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class STREETUSERMetadata
        {

            // 元数据类不会实例化。
            private STREETUSERMetadata()
            {
            }

            public decimal ID { get; set; }

            public STREETEXAMINE STREETEXAMINE { get; set; }

            public Nullable<decimal> STREETEXAMINEID { get; set; }

            public USER USER { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TABLE1Metadata 标识为类
    //，以携带 TABLE1 类的其他元数据。
    [MetadataTypeAttribute(typeof(TABLE1.TABLE1Metadata))]
    public partial class TABLE1
    {

        // 通过此类可将自定义特性附加到
        //TABLE1 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TABLE1Metadata
        {

            // 元数据类不会实例化。
            private TABLE1Metadata()
            {
            }

            public string ID { get; set; }

            public Nullable<DateTime> TIME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TJGHZFMetadata 标识为类
    //，以携带 TJGHZF 类的其他元数据。
    [MetadataTypeAttribute(typeof(TJGHZF.TJGHZFMetadata))]
    public partial class TJGHZF
    {

        // 通过此类可将自定义特性附加到
        //TJGHZF 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TJGHZFMetadata
        {

            // 元数据类不会实例化。
            private TJGHZFMetadata()
            {
            }

            public Nullable<decimal> AYOTHER { get; set; }

            public Nullable<decimal> AYXCFX { get; set; }

            public Nullable<decimal> BJ { get; set; }

            public string CHECKUSSER { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string CREATEUSER { get; set; }

            public Nullable<decimal> FK { get; set; }

            public decimal ID { get; set; }

            public Nullable<decimal> LA { get; set; }

            public Nullable<decimal> MSSW { get; set; }

            public Nullable<decimal> MSWFSR { get; set; }

            public Nullable<decimal> OBQSZFZCALL { get; set; }

            public Nullable<decimal> OBQSZFZCZJ { get; set; }

            public Nullable<decimal> ODDFY { get; set; }

            public Nullable<decimal> OSQFYZXSQ { get; set; }

            public Nullable<decimal> OSQFYZXZJ { get; set; }

            public Nullable<decimal> OXZFYSS { get; set; }

            public Nullable<decimal> OYS { get; set; }

            public Nullable<decimal> OZLTZJS { get; set; }

            public Nullable<decimal> OZZTZ { get; set; }

            public Nullable<DateTime> TJTIME { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> WFMJ { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TRANSLICENSMetadata 标识为类
    //，以携带 TRANSLICENS 类的其他元数据。
    [MetadataTypeAttribute(typeof(TRANSLICENS.TRANSLICENSMetadata))]
    public partial class TRANSLICENS
    {

        // 通过此类可将自定义特性附加到
        //TRANSLICENS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TRANSLICENSMetadata
        {

            // 元数据类不会实例化。
            private TRANSLICENSMetadata()
            {
            }

            public CONSTRSITE CONSTRSITE { get; set; }

            public Nullable<decimal> CONSTRSITEID { get; set; }

            public DUMPINGSITE DUMPINGSITE { get; set; }

            public Nullable<decimal> DUMPINGSITEID { get; set; }

            public Nullable<DateTime> ENDDATE { get; set; }

            public Nullable<decimal> ISSYNC { get; set; }

            public Nullable<decimal> NIJIANG { get; set; }

            public Nullable<DateTime> STARTDATE { get; set; }

            public EntityCollection<TRANSLICENSECARCOMPANy> TRANSLICENSECARCOMPANIES { get; set; }

            public EntityCollection<TRANSLICENSECAR> TRANSLICENSECARS { get; set; }

            public decimal TRANSLICENSEID { get; set; }

            public EntityCollection<TRANSLICENSEROAD> TRANSLICENSEROADS { get; set; }

            public string TRANSLINE { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> ZHATU { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TRANSLICENSECARMetadata 标识为类
    //，以携带 TRANSLICENSECAR 类的其他元数据。
    [MetadataTypeAttribute(typeof(TRANSLICENSECAR.TRANSLICENSECARMetadata))]
    public partial class TRANSLICENSECAR
    {

        // 通过此类可将自定义特性附加到
        //TRANSLICENSECAR 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TRANSLICENSECARMetadata
        {

            // 元数据类不会实例化。
            private TRANSLICENSECARMetadata()
            {
            }

            public decimal CARID { get; set; }

            public string REDUNDANCY { get; set; }

            public TRANSLICENS TRANSLICENS { get; set; }

            public decimal TRANSLICENSEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TRANSLICENSECARCOMPANyMetadata 标识为类
    //，以携带 TRANSLICENSECARCOMPANy 类的其他元数据。
    [MetadataTypeAttribute(typeof(TRANSLICENSECARCOMPANy.TRANSLICENSECARCOMPANyMetadata))]
    public partial class TRANSLICENSECARCOMPANy
    {

        // 通过此类可将自定义特性附加到
        //TRANSLICENSECARCOMPANy 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TRANSLICENSECARCOMPANyMetadata
        {

            // 元数据类不会实例化。
            private TRANSLICENSECARCOMPANyMetadata()
            {
            }

            public decimal CARCOMPANYID { get; set; }

            public string REDUNDANCY { get; set; }

            public TRANSLICENS TRANSLICENS { get; set; }

            public decimal TRANSLICENSEID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TRANSLICENSEROADMetadata 标识为类
    //，以携带 TRANSLICENSEROAD 类的其他元数据。
    [MetadataTypeAttribute(typeof(TRANSLICENSEROAD.TRANSLICENSEROADMetadata))]
    public partial class TRANSLICENSEROAD
    {

        // 通过此类可将自定义特性附加到
        //TRANSLICENSEROAD 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TRANSLICENSEROADMetadata
        {

            // 元数据类不会实例化。
            private TRANSLICENSEROADMetadata()
            {
            }

            public TRANSLICENS TRANSLICENS { get; set; }

            public Nullable<decimal> TRANSLICENSEID { get; set; }

            public decimal TRANSLICENSEROADID { get; set; }

            public TRANSROAD TRANSROAD { get; set; }

            public Nullable<decimal> TRANSROADID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TRANSRECORDMetadata 标识为类
    //，以携带 TRANSRECORD 类的其他元数据。
    [MetadataTypeAttribute(typeof(TRANSRECORD.TRANSRECORDMetadata))]
    public partial class TRANSRECORD
    {

        // 通过此类可将自定义特性附加到
        //TRANSRECORD 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TRANSRECORDMetadata
        {

            // 元数据类不会实例化。
            private TRANSRECORDMetadata()
            {
            }

            public Nullable<decimal> CARID { get; set; }

            public Nullable<decimal> CONSTRSITEID { get; set; }

            public Nullable<DateTime> CONSTRSITETIME { get; set; }

            public Nullable<decimal> DUMPINGSITEID { get; set; }

            public Nullable<DateTime> DUMPINGSITETIME { get; set; }

            public Nullable<decimal> TRANSLICENSID { get; set; }

            public decimal TRANSRECORDID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 TRANSROADMetadata 标识为类
    //，以携带 TRANSROAD 类的其他元数据。
    [MetadataTypeAttribute(typeof(TRANSROAD.TRANSROADMetadata))]
    public partial class TRANSROAD
    {

        // 通过此类可将自定义特性附加到
        //TRANSROAD 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class TRANSROADMetadata
        {

            // 元数据类不会实例化。
            private TRANSROADMetadata()
            {
            }

            public string ENDTIME { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<decimal> SPEED { get; set; }

            public string STARTTIME { get; set; }

            public EntityCollection<TRANSLICENSEROAD> TRANSLICENSEROADS { get; set; }

            public decimal TRANSROADID { get; set; }

            public string TRANSROADNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 UNITMetadata 标识为类
    //，以携带 UNIT 类的其他元数据。
    [MetadataTypeAttribute(typeof(UNIT.UNITMetadata))]
    public partial class UNIT
    {

        // 通过此类可将自定义特性附加到
        //UNIT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UNITMetadata
        {

            // 元数据类不会实例化。
            private UNITMetadata()
            {
            }

            public string ABBREVIATION { get; set; }

            public EntityCollection<ACTIVITYPERMISSION> ACTIVITYPERMISSIONS { get; set; }

            public EntityCollection<CONSTRSITE> CONSTRSITES { get; set; }

            public string DESCRIPTION { get; set; }

            public EntityCollection<DUMPINGSITE> DUMPINGSITES { get; set; }

            public string DWZC { get; set; }

            public Nullable<decimal> ISADMINUNIT { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public string PATH { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public EntityCollection<SPECIALACTIVITICE> SPECIALACTIVITICES { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public EntityCollection<TRANSLICENS> TRANSLICENSES { get; set; }

            public UNIT UNIT1 { get; set; }

            public decimal UNITID { get; set; }

            public string UNITNAME { get; set; }

            public EntityCollection<UNIT> UNITS1 { get; set; }

            public UNITTYPE UNITTYPE { get; set; }

            public Nullable<decimal> UNITTYPEID { get; set; }

            public EntityCollection<USER> USERS { get; set; }

            public EntityCollection<WORKFLOWINSTANCE> WORKFLOWINSTANCES { get; set; }

            public EntityCollection<XCJGSIGNIN> XCJGSIGNINS { get; set; }

            public EntityCollection<ZFGKCAR> ZFGKCARS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 UNITTYPEMetadata 标识为类
    //，以携带 UNITTYPE 类的其他元数据。
    [MetadataTypeAttribute(typeof(UNITTYPE.UNITTYPEMetadata))]
    public partial class UNITTYPE
    {

        // 通过此类可将自定义特性附加到
        //UNITTYPE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class UNITTYPEMetadata
        {

            // 元数据类不会实例化。
            private UNITTYPEMetadata()
            {
            }

            public EntityCollection<UNIT> UNITS { get; set; }

            public decimal UNITTYPEID { get; set; }

            public string UNITTYPENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERMetadata 标识为类
    //，以携带 USER 类的其他元数据。
    [MetadataTypeAttribute(typeof(USER.USERMetadata))]
    public partial class USER
    {

        // 通过此类可将自定义特性附加到
        //USER 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERMetadata
        {

            // 元数据类不会实例化。
            private USERMetadata()
            {
            }

            public string ACCOUNT { get; set; }

            public Nullable<decimal> CATEGORYID { get; set; }

            public EntityCollection<ONDUTy> ONDUTIES { get; set; }

            public string PASSWORD { get; set; }

            public Nullable<decimal> REGIONID { get; set; }

            public string RTXACCOUNT { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public EntityCollection<SINGNIN> SINGNINS { get; set; }

            public string SMSNUMBERS { get; set; }

            public EntityCollection<SPECIALACTIVITICE> SPECIALACTIVITICES { get; set; }

            public EntityCollection<SPECIALTOZFSJ> SPECIALTOZFSJS { get; set; }

            public EntityCollection<SPECIALWORKFLOWINSTANCE> SPECIALWORKFLOWINSTANCES { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public EntityCollection<STREETUSER> STREETUSERS { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public USERARCHIVE USERARCHIVE { get; set; }

            public USERCATEGORy USERCATEGORy { get; set; }

            public Nullable<decimal> USERCATEGORYID { get; set; }

            public decimal USERID { get; set; }

            public string USERNAME { get; set; }

            public EntityCollection<USERONDUTy> USERONDUTIES { get; set; }

            public USERPOSITION USERPOSITION { get; set; }

            public Nullable<decimal> USERPOSITIONID { get; set; }

            public EntityCollection<USERROLE> USERROLES { get; set; }

            public string WORKZZ { get; set; }

            public WTUSERRELATION WTUSERRELATION { get; set; }

            public EntityCollection<XCJGUSERTASK> XCJGUSERTASKS { get; set; }

            public EntityCollection<ZFGKUSERHISTORYPOSITION> ZFGKUSERHISTORYPOSITIONS { get; set; }

            public ZFGKUSERLATESTPOSITION ZFGKUSERLATESTPOSITION { get; set; }

            public string ZFZBH { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERARCHIVEMetadata 标识为类
    //，以携带 USERARCHIVE 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERARCHIVE.USERARCHIVEMetadata))]
    public partial class USERARCHIVE
    {

        // 通过此类可将自定义特性附加到
        //USERARCHIVE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERARCHIVEMetadata
        {

            // 元数据类不会实例化。
            private USERARCHIVEMetadata()
            {
            }

            public string AVATAR { get; set; }

            public USER USER { get; set; }

            public decimal USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERCATEGORyMetadata 标识为类
    //，以携带 USERCATEGORy 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERCATEGORy.USERCATEGORyMetadata))]
    public partial class USERCATEGORy
    {

        // 通过此类可将自定义特性附加到
        //USERCATEGORy 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERCATEGORyMetadata
        {

            // 元数据类不会实例化。
            private USERCATEGORyMetadata()
            {
            }

            public decimal USERCATEGORYID { get; set; }

            public string USERCATEGORYNAME { get; set; }

            public EntityCollection<USER> USERS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERGROUPMetadata 标识为类
    //，以携带 USERGROUP 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERGROUP.USERGROUPMetadata))]
    public partial class USERGROUP
    {

        // 通过此类可将自定义特性附加到
        //USERGROUP 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERGROUPMetadata
        {

            // 元数据类不会实例化。
            private USERGROUPMetadata()
            {
            }

            public decimal GROUPID { get; set; }

            public string REDUNDANCY { get; set; }

            public decimal USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERONDUTyMetadata 标识为类
    //，以携带 USERONDUTy 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERONDUTy.USERONDUTyMetadata))]
    public partial class USERONDUTy
    {

        // 通过此类可将自定义特性附加到
        //USERONDUTy 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERONDUTyMetadata
        {

            // 元数据类不会实例化。
            private USERONDUTyMetadata()
            {
            }

            public Nullable<DateTime> CREATETIME { get; set; }

            public Nullable<decimal> CREATEUSERID { get; set; }

            public decimal ID { get; set; }

            public ONDUTy ONDUTy { get; set; }

            public Nullable<decimal> ONDUTYID { get; set; }

            public USER USER { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public USERPOSITION USERPOSITION { get; set; }

            public Nullable<decimal> USERPOSITIONID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERPHONESIGNINMetadata 标识为类
    //，以携带 USERPHONESIGNIN 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERPHONESIGNIN.USERPHONESIGNINMetadata))]
    public partial class USERPHONESIGNIN
    {

        // 通过此类可将自定义特性附加到
        //USERPHONESIGNIN 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERPHONESIGNINMetadata
        {

            // 元数据类不会实例化。
            private USERPHONESIGNINMetadata()
            {
            }

            public string ID { get; set; }

            public string IMEINUM { get; set; }

            public string LATANDLONG { get; set; }

            public string PHONETIME { get; set; }

            public Nullable<DateTime> SIGNTIME { get; set; }

            public Nullable<decimal> USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERPOSITIONMetadata 标识为类
    //，以携带 USERPOSITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERPOSITION.USERPOSITIONMetadata))]
    public partial class USERPOSITION
    {

        // 通过此类可将自定义特性附加到
        //USERPOSITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERPOSITIONMetadata
        {

            // 元数据类不会实例化。
            private USERPOSITIONMetadata()
            {
            }

            public EntityCollection<USERONDUTy> USERONDUTIES { get; set; }

            public decimal USERPOSITIONID { get; set; }

            public string USERPOSITIONNAME { get; set; }

            public EntityCollection<USER> USERS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERROLEMetadata 标识为类
    //，以携带 USERROLE 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERROLE.USERROLEMetadata))]
    public partial class USERROLE
    {

        // 通过此类可将自定义特性附加到
        //USERROLE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERROLEMetadata
        {

            // 元数据类不会实例化。
            private USERROLEMetadata()
            {
            }

            public string REDUNDANCY { get; set; }

            public ROLE ROLE { get; set; }

            public decimal ROLEID { get; set; }

            public USER USER { get; set; }

            public decimal USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 USERS_DELMetadata 标识为类
    //，以携带 USERS_DEL 类的其他元数据。
    [MetadataTypeAttribute(typeof(USERS_DEL.USERS_DELMetadata))]
    public partial class USERS_DEL
    {

        // 通过此类可将自定义特性附加到
        //USERS_DEL 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class USERS_DELMetadata
        {

            // 元数据类不会实例化。
            private USERS_DELMetadata()
            {
            }

            public string ACCOUNT { get; set; }

            public Nullable<decimal> CATEGORYID { get; set; }

            public string PASSWORD { get; set; }

            public Nullable<decimal> REGIONID { get; set; }

            public string RTXACCOUNT { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public string SMSNUMBERS { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> USERCATEGORYID { get; set; }

            public decimal USERID { get; set; }

            public string USERNAME { get; set; }

            public Nullable<decimal> USERPOSITIONID { get; set; }

            public string ZFZBH { get; set; }
        }
    }

    // MetadataTypeAttribute 将 WORKFLOWDEFINITIONMetadata 标识为类
    //，以携带 WORKFLOWDEFINITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(WORKFLOWDEFINITION.WORKFLOWDEFINITIONMetadata))]
    public partial class WORKFLOWDEFINITION
    {

        // 通过此类可将自定义特性附加到
        //WORKFLOWDEFINITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class WORKFLOWDEFINITIONMetadata
        {

            // 元数据类不会实例化。
            private WORKFLOWDEFINITIONMetadata()
            {
            }

            public EntityCollection<ACITIVITYDEFINITION> ACITIVITYDEFINITIONS { get; set; }

            public string WDDESC { get; set; }

            public decimal WDID { get; set; }

            public string WDNAME { get; set; }

            public EntityCollection<WORKFLOWINSTANCE> WORKFLOWINSTANCES { get; set; }
        }
    }

    // MetadataTypeAttribute 将 WORKFLOWINSTANCEMetadata 标识为类
    //，以携带 WORKFLOWINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(WORKFLOWINSTANCE.WORKFLOWINSTANCEMetadata))]
    public partial class WORKFLOWINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //WORKFLOWINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class WORKFLOWINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private WORKFLOWINSTANCEMetadata()
            {
            }

            public EntityCollection<ACTIVITYINSTANCE> ACTIVITYINSTANCES { get; set; }

            public Nullable<decimal> CASESOURCEID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public EntityCollection<DOCINSTANCE> DOCINSTANCES { get; set; }

            public Nullable<decimal> ILLEGALITEMID { get; set; }

            public string PARENTWIID { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public Nullable<decimal> WDID { get; set; }

            public string WICODE { get; set; }

            public string WIID { get; set; }

            public string WINAME { get; set; }

            public WORKFLOWDEFINITION WORKFLOWDEFINITION { get; set; }

            public EntityCollection<WORKFLOWPEROPERTy> WORKFLOWPEROPERTIES { get; set; }

            public WORKFLOWSTATUS WORKFLOWSTATUS { get; set; }

            public Nullable<decimal> WORKFLOWSTATUSID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 WORKFLOWPEROPERTyMetadata 标识为类
    //，以携带 WORKFLOWPEROPERTy 类的其他元数据。
    [MetadataTypeAttribute(typeof(WORKFLOWPEROPERTy.WORKFLOWPEROPERTyMetadata))]
    public partial class WORKFLOWPEROPERTy
    {

        // 通过此类可将自定义特性附加到
        //WORKFLOWPEROPERTy 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class WORKFLOWPEROPERTyMetadata
        {

            // 元数据类不会实例化。
            private WORKFLOWPEROPERTyMetadata()
            {
            }

            public string ASSEMBLYNAME { get; set; }

            public string KEY { get; set; }

            public string TYPENAME { get; set; }

            public string VALUE { get; set; }

            public string WIID { get; set; }

            public WORKFLOWINSTANCE WORKFLOWINSTANCE { get; set; }

            public string WPID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 WORKFLOWSTATUSMetadata 标识为类
    //，以携带 WORKFLOWSTATUS 类的其他元数据。
    [MetadataTypeAttribute(typeof(WORKFLOWSTATUS.WORKFLOWSTATUSMetadata))]
    public partial class WORKFLOWSTATUS
    {

        // 通过此类可将自定义特性附加到
        //WORKFLOWSTATUS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class WORKFLOWSTATUSMetadata
        {

            // 元数据类不会实例化。
            private WORKFLOWSTATUSMetadata()
            {
            }

            public EntityCollection<WORKFLOWINSTANCE> WORKFLOWINSTANCES { get; set; }

            public decimal WORKFLOWSTATUSID { get; set; }

            public string WORKFLOWSTATUSNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 WTUSERRELATIONMetadata 标识为类
    //，以携带 WTUSERRELATION 类的其他元数据。
    [MetadataTypeAttribute(typeof(WTUSERRELATION.WTUSERRELATIONMetadata))]
    public partial class WTUSERRELATION
    {

        // 通过此类可将自定义特性附加到
        //WTUSERRELATION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class WTUSERRELATIONMetadata
        {

            // 元数据类不会实例化。
            private WTUSERRELATIONMetadata()
            {
            }

            public USER USER { get; set; }

            public decimal USERID { get; set; }

            public string WTUNITID { get; set; }

            public string WTUSERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XCJGAREAMetadata 标识为类
    //，以携带 XCJGAREA 类的其他元数据。
    [MetadataTypeAttribute(typeof(XCJGAREA.XCJGAREAMetadata))]
    public partial class XCJGAREA
    {

        // 通过此类可将自定义特性附加到
        //XCJGAREA 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XCJGAREAMetadata
        {

            // 元数据类不会实例化。
            private XCJGAREAMetadata()
            {
            }

            public string AREADESCRIPTION { get; set; }

            public decimal AREAID { get; set; }

            public string AREANAME { get; set; }

            public Nullable<decimal> AREAOWNERTYPE { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<decimal> SSDDID { get; set; }

            public Nullable<decimal> SSZDID { get; set; }

            public EntityCollection<XCJGCARTASK> XCJGCARTASKS { get; set; }

            public EntityCollection<XCJGUSERTASK> XCJGUSERTASKS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XCJGCARTASKMetadata 标识为类
    //，以携带 XCJGCARTASK 类的其他元数据。
    [MetadataTypeAttribute(typeof(XCJGCARTASK.XCJGCARTASKMetadata))]
    public partial class XCJGCARTASK
    {

        // 通过此类可将自定义特性附加到
        //XCJGCARTASK 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XCJGCARTASKMetadata
        {

            // 元数据类不会实例化。
            private XCJGCARTASKMetadata()
            {
            }

            public Nullable<decimal> AREAID { get; set; }

            public decimal CARID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public Nullable<decimal> ENDHOUR { get; set; }

            public Nullable<decimal> ENDMINUTE { get; set; }

            public string JOBCONTENT { get; set; }

            public Nullable<decimal> ROUTEID { get; set; }

            public Nullable<decimal> SSQJID { get; set; }

            public Nullable<decimal> SSZDID { get; set; }

            public Nullable<decimal> STARTHOUR { get; set; }

            public Nullable<decimal> STARTMINUTE { get; set; }

            public DateTime TASKDATE { get; set; }

            public XCJGAREA XCJGAREA { get; set; }

            public XCJGROUTE XCJGROUTE { get; set; }

            public ZFGKCAR ZFGKCAR { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XCJGROUTEMetadata 标识为类
    //，以携带 XCJGROUTE 类的其他元数据。
    [MetadataTypeAttribute(typeof(XCJGROUTE.XCJGROUTEMetadata))]
    public partial class XCJGROUTE
    {

        // 通过此类可将自定义特性附加到
        //XCJGROUTE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XCJGROUTEMetadata
        {

            // 元数据类不会实例化。
            private XCJGROUTEMetadata()
            {
            }

            public string GEOMETRY { get; set; }

            public string ROUTEDESCRIPTION { get; set; }

            public decimal ROUTEID { get; set; }

            public string ROUTENAME { get; set; }

            public Nullable<decimal> ROUTEOWNERTYPE { get; set; }

            public Nullable<decimal> SSDDID { get; set; }

            public Nullable<decimal> SSZDID { get; set; }

            public EntityCollection<XCJGCARTASK> XCJGCARTASKS { get; set; }

            public EntityCollection<XCJGUSERTASK> XCJGUSERTASKS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XCJGSIGNINMetadata 标识为类
    //，以携带 XCJGSIGNIN 类的其他元数据。
    [MetadataTypeAttribute(typeof(XCJGSIGNIN.XCJGSIGNINMetadata))]
    public partial class XCJGSIGNIN
    {

        // 通过此类可将自定义特性附加到
        //XCJGSIGNIN 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XCJGSIGNINMetadata
        {

            // 元数据类不会实例化。
            private XCJGSIGNINMetadata()
            {
            }

            public string ADDRESSNAME { get; set; }

            public Nullable<decimal> ENDHOUR { get; set; }

            public Nullable<decimal> ENDMINUTE { get; set; }

            public string GEOMETRY { get; set; }

            public Nullable<DateTime> SIGNINDATE { get; set; }

            public decimal SIGNINID { get; set; }

            public Nullable<decimal> SSZDID { get; set; }

            public Nullable<decimal> STARTHOUR { get; set; }

            public Nullable<decimal> STARTMINUTE { get; set; }

            public UNIT UNIT { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XCJGUSERTASKMetadata 标识为类
    //，以携带 XCJGUSERTASK 类的其他元数据。
    [MetadataTypeAttribute(typeof(XCJGUSERTASK.XCJGUSERTASKMetadata))]
    public partial class XCJGUSERTASK
    {

        // 通过此类可将自定义特性附加到
        //XCJGUSERTASK 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XCJGUSERTASKMetadata
        {

            // 元数据类不会实例化。
            private XCJGUSERTASKMetadata()
            {
            }

            public Nullable<decimal> AREAID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public Nullable<decimal> ENDHOUR { get; set; }

            public Nullable<decimal> ENDMINUTE { get; set; }

            public string JOBCONTENT { get; set; }

            public Nullable<decimal> ROUTEID { get; set; }

            public Nullable<decimal> SSQJID { get; set; }

            public Nullable<decimal> SSZDID { get; set; }

            public Nullable<decimal> STARTHOUR { get; set; }

            public Nullable<decimal> STARTMINUTE { get; set; }

            public DateTime TASKDATE { get; set; }

            public USER USER { get; set; }

            public decimal USERID { get; set; }

            public XCJGAREA XCJGAREA { get; set; }

            public XCJGROUTE XCJGROUTE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPACTDEFMetadata 标识为类
    //，以携带 XZSPACTDEF 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPACTDEF.XZSPACTDEFMetadata))]
    public partial class XZSPACTDEF
    {

        // 通过此类可将自定义特性附加到
        //XZSPACTDEF 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPACTDEFMetadata
        {

            // 元数据类不会实例化。
            private XZSPACTDEFMetadata()
            {
            }

            public string ADCODE { get; set; }

            public string ADDESCRIPTION { get; set; }

            public decimal ADID { get; set; }

            public string ADNAME { get; set; }

            public string DEFAULTPOSITIONID { get; set; }

            public Nullable<decimal> NEXTADID { get; set; }

            public Nullable<decimal> SEQNO { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }

            public Nullable<decimal> WDID { get; set; }

            public EntityCollection<XZSPACTIST> XZSPACTISTS { get; set; }

            public XZSPWFDEF XZSPWFDEF { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPACTISTMetadata 标识为类
    //，以携带 XZSPACTIST 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPACTIST.XZSPACTISTMetadata))]
    public partial class XZSPACTIST
    {

        // 通过此类可将自定义特性附加到
        //XZSPACTIST 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPACTISTMetadata
        {

            // 元数据类不会实例化。
            private XZSPACTISTMetadata()
            {
            }

            public string ADATA { get; set; }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<decimal> APID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string PREVIONSAIID { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }

            public string TODEPTID { get; set; }

            public string TOPOSITIONID { get; set; }

            public string TOUSERID { get; set; }

            public string WIID { get; set; }

            public XZSPACTDEF XZSPACTDEF { get; set; }

            public XZSPSTU XZSPSTU { get; set; }

            public XZSPWFIST XZSPWFIST { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPACTISTS_DELETEMetadata 标识为类
    //，以携带 XZSPACTISTS_DELETE 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPACTISTS_DELETE.XZSPACTISTS_DELETEMetadata))]
    public partial class XZSPACTISTS_DELETE
    {

        // 通过此类可将自定义特性附加到
        //XZSPACTISTS_DELETE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPACTISTS_DELETEMetadata
        {

            // 元数据类不会实例化。
            private XZSPACTISTS_DELETEMetadata()
            {
            }

            public string ADATA { get; set; }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<decimal> APID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string PREVIONSAIID { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }

            public string TODEPTID { get; set; }

            public string TOPOSITIONID { get; set; }

            public string TOUSERID { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPACTISTS_DELETE1Metadata 标识为类
    //，以携带 XZSPACTISTS_DELETE1 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPACTISTS_DELETE1.XZSPACTISTS_DELETE1Metadata))]
    public partial class XZSPACTISTS_DELETE1
    {

        // 通过此类可将自定义特性附加到
        //XZSPACTISTS_DELETE1 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPACTISTS_DELETE1Metadata
        {

            // 元数据类不会实例化。
            private XZSPACTISTS_DELETE1Metadata()
            {
            }

            public string ADATA { get; set; }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<decimal> APID { get; set; }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string PREVIONSAIID { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }

            public string TODEPTID { get; set; }

            public string TOPOSITIONID { get; set; }

            public string TOUSERID { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPACTIVITYDEFINITIONMetadata 标识为类
    //，以携带 XZSPACTIVITYDEFINITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPACTIVITYDEFINITION.XZSPACTIVITYDEFINITIONMetadata))]
    public partial class XZSPACTIVITYDEFINITION
    {

        // 通过此类可将自定义特性附加到
        //XZSPACTIVITYDEFINITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPACTIVITYDEFINITIONMetadata
        {

            // 元数据类不会实例化。
            private XZSPACTIVITYDEFINITIONMetadata()
            {
            }

            public string ADCODE { get; set; }

            public string ADDESCRIPTION { get; set; }

            public decimal ADID { get; set; }

            public string ADNAME { get; set; }

            public Nullable<decimal> DEFAULPOSITIONID { get; set; }

            public Nullable<decimal> NEXTADID { get; set; }

            public decimal SEQNO { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }

            public Nullable<decimal> WDID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPAPPROVALPROJECTMetadata 标识为类
    //，以携带 XZSPAPPROVALPROJECT 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPAPPROVALPROJECT.XZSPAPPROVALPROJECTMetadata))]
    public partial class XZSPAPPROVALPROJECT
    {

        // 通过此类可将自定义特性附加到
        //XZSPAPPROVALPROJECT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPAPPROVALPROJECTMetadata
        {

            // 元数据类不会实例化。
            private XZSPAPPROVALPROJECTMetadata()
            {
            }

            public string APDESCRIPTION { get; set; }

            public decimal APID { get; set; }

            public string APNAME { get; set; }

            public string KZXX { get; set; }

            public Nullable<decimal> PROJECTID { get; set; }

            public Nullable<decimal> WDID { get; set; }

            public string XCHCQK { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPKZHCMetadata 标识为类
    //，以携带 XZSPKZHC 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPKZHC.XZSPKZHCMetadata))]
    public partial class XZSPKZHC
    {

        // 通过此类可将自定义特性附加到
        //XZSPKZHC 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPKZHCMetadata
        {

            // 元数据类不会实例化。
            private XZSPKZHCMetadata()
            {
            }

            public Nullable<decimal> APID { get; set; }

            public string HCXXMODEL { get; set; }

            public string ID { get; set; }

            public string KZXXMODEL { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPNEWACTIVITYDEFINITIONMetadata 标识为类
    //，以携带 XZSPNEWACTIVITYDEFINITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPNEWACTIVITYDEFINITION.XZSPNEWACTIVITYDEFINITIONMetadata))]
    public partial class XZSPNEWACTIVITYDEFINITION
    {

        // 通过此类可将自定义特性附加到
        //XZSPNEWACTIVITYDEFINITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPNEWACTIVITYDEFINITIONMetadata
        {

            // 元数据类不会实例化。
            private XZSPNEWACTIVITYDEFINITIONMetadata()
            {
            }

            public string ADANAME { get; set; }

            public decimal ADID { get; set; }

            public Nullable<decimal> NEXTADID { get; set; }

            public Nullable<decimal> SEQNO { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPNEWTABMetadata 标识为类
    //，以携带 XZSPNEWTAB 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPNEWTAB.XZSPNEWTABMetadata))]
    public partial class XZSPNEWTAB
    {

        // 通过此类可将自定义特性附加到
        //XZSPNEWTAB 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPNEWTABMetadata
        {

            // 元数据类不会实例化。
            private XZSPNEWTABMetadata()
            {
            }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public string ATTACHMENT1 { get; set; }

            public string ATTACHMENT2 { get; set; }

            public string ATTACHMENT3 { get; set; }

            public string ID { get; set; }

            public string PQR { get; set; }

            public Nullable<DateTime> PQSJ { get; set; }

            public string PQYJ { get; set; }

            public Nullable<decimal> PQZD { get; set; }

            public Nullable<decimal> STUTASID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPNEWWORKFLOWINSTANCEMetadata 标识为类
    //，以携带 XZSPNEWWORKFLOWINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPNEWWORKFLOWINSTANCE.XZSPNEWWORKFLOWINSTANCEMetadata))]
    public partial class XZSPNEWWORKFLOWINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //XZSPNEWWORKFLOWINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPNEWWORKFLOWINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private XZSPNEWWORKFLOWINSTANCEMetadata()
            {
            }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string EVENTDESCRIPTION { get; set; }

            public string EVENTTITLE { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public string TOUSER { get; set; }

            public Nullable<DateTime> UPDATETIME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPPROJECTNAMEMetadata 标识为类
    //，以携带 XZSPPROJECTNAME 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPPROJECTNAME.XZSPPROJECTNAMEMetadata))]
    public partial class XZSPPROJECTNAME
    {

        // 通过此类可将自定义特性附加到
        //XZSPPROJECTNAME 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPPROJECTNAMEMetadata
        {

            // 元数据类不会实例化。
            private XZSPPROJECTNAMEMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public decimal PROJECTID { get; set; }

            public string PROJECTNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPSTUMetadata 标识为类
    //，以携带 XZSPSTU 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPSTU.XZSPSTUMetadata))]
    public partial class XZSPSTU
    {

        // 通过此类可将自定义特性附加到
        //XZSPSTU 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPSTUMetadata
        {

            // 元数据类不会实例化。
            private XZSPSTUMetadata()
            {
            }

            public decimal SID { get; set; }

            public string WSCODE { get; set; }

            public string WSDESCRIPTION { get; set; }

            public string WSNAME { get; set; }

            public EntityCollection<XZSPACTIST> XZSPACTISTS { get; set; }

            public EntityCollection<XZSPWFIST> XZSPWFISTS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPWFDEFMetadata 标识为类
    //，以携带 XZSPWFDEF 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPWFDEF.XZSPWFDEFMetadata))]
    public partial class XZSPWFDEF
    {

        // 通过此类可将自定义特性附加到
        //XZSPWFDEF 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPWFDEFMetadata
        {

            // 元数据类不会实例化。
            private XZSPWFDEFMetadata()
            {
            }

            public string WDCODE { get; set; }

            public string WDDESCRIPTION { get; set; }

            public decimal WDID { get; set; }

            public string WDNAME { get; set; }

            public EntityCollection<XZSPACTDEF> XZSPACTDEFS { get; set; }

            public EntityCollection<XZSPWFIST> XZSPWFISTS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPWFISTMetadata 标识为类
    //，以携带 XZSPWFIST 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPWFIST.XZSPWFISTMetadata))]
    public partial class XZSPWFIST
    {

        // 通过此类可将自定义特性附加到
        //XZSPWFIST 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPWFISTMetadata
        {

            // 元数据类不会实例化。
            private XZSPWFISTMetadata()
            {
            }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string CURRENTAIID { get; set; }

            public string DTWZ { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public string WDATA { get; set; }

            public Nullable<decimal> WDID { get; set; }

            public string WIID { get; set; }

            public EntityCollection<XZSPACTIST> XZSPACTISTS { get; set; }

            public XZSPSTU XZSPSTU { get; set; }

            public XZSPWFDEF XZSPWFDEF { get; set; }

            public string XZSPWSBH { get; set; }

            public string ZFZDNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZSPWFISTS_DELETEMetadata 标识为类
    //，以携带 XZSPWFISTS_DELETE 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZSPWFISTS_DELETE.XZSPWFISTS_DELETEMetadata))]
    public partial class XZSPWFISTS_DELETE
    {

        // 通过此类可将自定义特性附加到
        //XZSPWFISTS_DELETE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZSPWFISTS_DELETEMetadata
        {

            // 元数据类不会实例化。
            private XZSPWFISTS_DELETEMetadata()
            {
            }

            public Nullable<DateTime> CREATEDTIME { get; set; }

            public string CURRENTAIID { get; set; }

            public string DTWZ { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public string WDATA { get; set; }

            public Nullable<decimal> WDID { get; set; }

            public string WIID { get; set; }

            public string XZSPWSBH { get; set; }

            public string ZFZDNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZZFLISTSHMetadata 标识为类
    //，以携带 XZZFLISTSH 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZZFLISTSH.XZZFLISTSHMetadata))]
    public partial class XZZFLISTSH
    {

        // 通过此类可将自定义特性附加到
        //XZZFLISTSH 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZZFLISTSHMetadata
        {

            // 元数据类不会实例化。
            private XZZFLISTSHMetadata()
            {
            }

            public string CLASSID { get; set; }

            public Nullable<DateTime> DTTIME { get; set; }

            public string SHUSEER { get; set; }

            public decimal SID { get; set; }

            public string TBUSER { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZZFQUESTIONCLASSMetadata 标识为类
    //，以携带 XZZFQUESTIONCLASS 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZZFQUESTIONCLASS.XZZFQUESTIONCLASSMetadata))]
    public partial class XZZFQUESTIONCLASS
    {

        // 通过此类可将自定义特性附加到
        //XZZFQUESTIONCLASS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZZFQUESTIONCLASSMetadata
        {

            // 元数据类不会实例化。
            private XZZFQUESTIONCLASSMetadata()
            {
            }

            public decimal CLASSID { get; set; }

            public string CLASSNAME { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public decimal TYPECODE { get; set; }

            public Nullable<decimal> TYPEID { get; set; }

            public EntityCollection<XZZFTABLIST> XZZFTABLISTS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 XZZFTABLISTMetadata 标识为类
    //，以携带 XZZFTABLIST 类的其他元数据。
    [MetadataTypeAttribute(typeof(XZZFTABLIST.XZZFTABLISTMetadata))]
    public partial class XZZFTABLIST
    {

        // 通过此类可将自定义特性附加到
        //XZZFTABLIST 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class XZZFTABLISTMetadata
        {

            // 元数据类不会实例化。
            private XZZFTABLISTMetadata()
            {
            }

            public Nullable<decimal> ANYOTHER { get; set; }

            public Nullable<decimal> AYXCFX { get; set; }

            public Nullable<decimal> CASEBJ { get; set; }

            public Nullable<decimal> CASEFAKY { get; set; }

            public Nullable<decimal> CASELA { get; set; }

            public Nullable<decimal> CASEMSWFCWY { get; set; }

            public Nullable<decimal> CASEMSWFSDY { get; set; }

            public Nullable<decimal> CASEOTHER { get; set; }

            public Nullable<decimal> CASEQZCSJ { get; set; }

            public Nullable<decimal> CASESQFYZX { get; set; }

            public Nullable<decimal> CASEZLTYJ { get; set; }

            public Nullable<decimal> CASEZZTZ { get; set; }

            public Nullable<decimal> CLASSID { get; set; }

            public Nullable<DateTime> DTTIME { get; set; }

            public decimal ID { get; set; }

            public string SHUSER { get; set; }

            public Nullable<decimal> SIMPLEFKJ { get; set; }

            public Nullable<decimal> SIMPLEFKY { get; set; }

            public string TBUSER { get; set; }

            public Nullable<decimal> UNITNAMEID { get; set; }

            public XZZFQUESTIONCLASS XZZFQUESTIONCLASS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZBRZLISTMetadata 标识为类
    //，以携带 ZBRZLIST 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZBRZLIST.ZBRZLISTMetadata))]
    public partial class ZBRZLIST
    {

        // 通过此类可将自定义特性附加到
        //ZBRZLIST 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZBRZLISTMetadata
        {

            // 元数据类不会实例化。
            private ZBRZLISTMetadata()
            {
            }

            public string COLUMN1DTWZ { get; set; }

            public Nullable<DateTime> CQDT { get; set; }

            public string ID { get; set; }

            public string SBSJ { get; set; }

            public string SINGQK { get; set; }

            public string ZBRY { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFGKCARMetadata 标识为类
    //，以携带 ZFGKCAR 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFGKCAR.ZFGKCARMetadata))]
    public partial class ZFGKCAR
    {

        // 通过此类可将自定义特性附加到
        //ZFGKCAR 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFGKCARMetadata
        {

            // 元数据类不会实例化。
            private ZFGKCARMetadata()
            {
            }

            public decimal CARID { get; set; }

            public string CARNO { get; set; }

            public UNIT UNIT { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public EntityCollection<XCJGCARTASK> XCJGCARTASKS { get; set; }

            public EntityCollection<ZFGKCARHISTORYPOSITION> ZFGKCARHISTORYPOSITIONS { get; set; }

            public ZFGKCARLATESTPOSITION ZFGKCARLATESTPOSITION { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFGKCARHISTORYPOSITIONMetadata 标识为类
    //，以携带 ZFGKCARHISTORYPOSITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFGKCARHISTORYPOSITION.ZFGKCARHISTORYPOSITIONMetadata))]
    public partial class ZFGKCARHISTORYPOSITION
    {

        // 通过此类可将自定义特性附加到
        //ZFGKCARHISTORYPOSITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFGKCARHISTORYPOSITIONMetadata
        {

            // 元数据类不会实例化。
            private ZFGKCARHISTORYPOSITIONMetadata()
            {
            }

            public decimal CARID { get; set; }

            public Nullable<decimal> LAT { get; set; }

            public Nullable<decimal> LON { get; set; }

            public DateTime POSITIONTIME { get; set; }

            public ZFGKCAR ZFGKCAR { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFGKCARLATESTPOSITIONMetadata 标识为类
    //，以携带 ZFGKCARLATESTPOSITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFGKCARLATESTPOSITION.ZFGKCARLATESTPOSITIONMetadata))]
    public partial class ZFGKCARLATESTPOSITION
    {

        // 通过此类可将自定义特性附加到
        //ZFGKCARLATESTPOSITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFGKCARLATESTPOSITIONMetadata
        {

            // 元数据类不会实例化。
            private ZFGKCARLATESTPOSITIONMetadata()
            {
            }

            public decimal CARID { get; set; }

            public Nullable<decimal> LAT { get; set; }

            public Nullable<decimal> LON { get; set; }

            public Nullable<DateTime> POSITIONTIME { get; set; }

            public ZFGKCAR ZFGKCAR { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFGKUSERHISTORYPOSITIONMetadata 标识为类
    //，以携带 ZFGKUSERHISTORYPOSITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFGKUSERHISTORYPOSITION.ZFGKUSERHISTORYPOSITIONMetadata))]
    public partial class ZFGKUSERHISTORYPOSITION
    {

        // 通过此类可将自定义特性附加到
        //ZFGKUSERHISTORYPOSITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFGKUSERHISTORYPOSITIONMetadata
        {

            // 元数据类不会实例化。
            private ZFGKUSERHISTORYPOSITIONMetadata()
            {
            }

            public string IMEICODE { get; set; }

            public Nullable<decimal> LAT { get; set; }

            public Nullable<decimal> LON { get; set; }

            public DateTime POSITIONTIME { get; set; }

            public USER USER { get; set; }

            public decimal USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFGKUSERLATESTPOSITIONMetadata 标识为类
    //，以携带 ZFGKUSERLATESTPOSITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFGKUSERLATESTPOSITION.ZFGKUSERLATESTPOSITIONMetadata))]
    public partial class ZFGKUSERLATESTPOSITION
    {

        // 通过此类可将自定义特性附加到
        //ZFGKUSERLATESTPOSITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFGKUSERLATESTPOSITIONMetadata
        {

            // 元数据类不会实例化。
            private ZFGKUSERLATESTPOSITIONMetadata()
            {
            }

            public string IMEICODE { get; set; }

            public Nullable<decimal> LAT { get; set; }

            public Nullable<decimal> LON { get; set; }

            public Nullable<DateTime> POSITIONTIME { get; set; }

            public USER USER { get; set; }

            public decimal USERID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJACTIVITYDEFINITIONMetadata 标识为类
    //，以携带 ZFSJACTIVITYDEFINITION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJACTIVITYDEFINITION.ZFSJACTIVITYDEFINITIONMetadata))]
    public partial class ZFSJACTIVITYDEFINITION
    {

        // 通过此类可将自定义特性附加到
        //ZFSJACTIVITYDEFINITION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJACTIVITYDEFINITIONMetadata
        {

            // 元数据类不会实例化。
            private ZFSJACTIVITYDEFINITIONMetadata()
            {
            }

            public string ADCODE { get; set; }

            public string ADDESCRIPTION { get; set; }

            public decimal ADID { get; set; }

            public string ADNAME { get; set; }

            public Nullable<decimal> NEXTADID { get; set; }

            public string SEQNO { get; set; }

            public Nullable<DateTime> TIMELIMIT { get; set; }

            public EntityCollection<ZFSJACTIVITYINSTANCE> ZFSJACTIVITYINSTANCES { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJACTIVITYINSTANCEMetadata 标识为类
    //，以携带 ZFSJACTIVITYINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJACTIVITYINSTANCE.ZFSJACTIVITYINSTANCEMetadata))]
    public partial class ZFSJACTIVITYINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //ZFSJACTIVITYINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJACTIVITYINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private ZFSJACTIVITYINSTANCEMetadata()
            {
            }

            public string ADATA { get; set; }

            public Nullable<decimal> ADID { get; set; }

            public string AIID { get; set; }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string IMEICODE { get; set; }

            public string PREVIONSAIID { get; set; }

            public Nullable<DateTime> SJTIMELIMIT { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }

            public string TOUSERID { get; set; }

            public string WIID { get; set; }

            public ZFSJACTIVITYDEFINITION ZFSJACTIVITYDEFINITION { get; set; }

            public ZFSJWORKFLOWINSTANCE ZFSJWORKFLOWINSTANCE { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJCHARTBYQLMetadata 标识为类
    //，以携带 ZFSJCHARTBYQL 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJCHARTBYQL.ZFSJCHARTBYQLMetadata))]
    public partial class ZFSJCHARTBYQL
    {

        // 通过此类可将自定义特性附加到
        //ZFSJCHARTBYQL 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJCHARTBYQLMetadata
        {

            // 元数据类不会实例化。
            private ZFSJCHARTBYQLMetadata()
            {
            }

            public Nullable<decimal> CLASSID { get; set; }

            public string CLASSNAME { get; set; }

            public Nullable<DateTime> DTTIME { get; set; }

            public Nullable<decimal> SJ96310 { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public decimal ZCID { get; set; }

            public Nullable<decimal> ZFSJCOUNTS { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJCHECKWAYMetadata 标识为类
    //，以携带 ZFSJCHECKWAY 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJCHECKWAY.ZFSJCHECKWAYMetadata))]
    public partial class ZFSJCHECKWAY
    {

        // 通过此类可将自定义特性附加到
        //ZFSJCHECKWAY 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJCHECKWAYMetadata
        {

            // 元数据类不会实例化。
            private ZFSJCHECKWAYMetadata()
            {
            }

            public string CHECKNAME { get; set; }

            public string DESCRIPTION { get; set; }

            public decimal ID { get; set; }

            public Nullable<decimal> PROCESSID { get; set; }

            public ZFSJPROCESSWAY ZFSJPROCESSWAY { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJPROCESSWAYMetadata 标识为类
    //，以携带 ZFSJPROCESSWAY 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJPROCESSWAY.ZFSJPROCESSWAYMetadata))]
    public partial class ZFSJPROCESSWAY
    {

        // 通过此类可将自定义特性附加到
        //ZFSJPROCESSWAY 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJPROCESSWAYMetadata
        {

            // 元数据类不会实例化。
            private ZFSJPROCESSWAYMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public decimal ID { get; set; }

            public string PROCESSWAYNAME { get; set; }

            public EntityCollection<ZFSJCHECKWAY> ZFSJCHECKWAYs { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJQUESTIONCLASSMetadata 标识为类
    //，以携带 ZFSJQUESTIONCLASS 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJQUESTIONCLASS.ZFSJQUESTIONCLASSMetadata))]
    public partial class ZFSJQUESTIONCLASS
    {

        // 通过此类可将自定义特性附加到
        //ZFSJQUESTIONCLASS 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJQUESTIONCLASSMetadata
        {

            // 元数据类不会实例化。
            private ZFSJQUESTIONCLASSMetadata()
            {
            }

            public Nullable<decimal> CLASSCODE { get; set; }

            public string CLASSID { get; set; }

            public string CLASSNAME { get; set; }

            public Nullable<decimal> CLASSTYPEID { get; set; }

            public Nullable<decimal> GRADE { get; set; }

            public Nullable<decimal> PARENTID { get; set; }

            public string PATH { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJSOURCEMetadata 标识为类
    //，以携带 ZFSJSOURCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJSOURCE.ZFSJSOURCEMetadata))]
    public partial class ZFSJSOURCE
    {

        // 通过此类可将自定义特性附加到
        //ZFSJSOURCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJSOURCEMetadata
        {

            // 元数据类不会实例化。
            private ZFSJSOURCEMetadata()
            {
            }

            public string DESCRIPTION { get; set; }

            public decimal ID { get; set; }

            public string SOURCENAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJSTATUMetadata 标识为类
    //，以携带 ZFSJSTATU 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJSTATU.ZFSJSTATUMetadata))]
    public partial class ZFSJSTATU
    {

        // 通过此类可将自定义特性附加到
        //ZFSJSTATU 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJSTATUMetadata
        {

            // 元数据类不会实例化。
            private ZFSJSTATUMetadata()
            {
            }

            public decimal SID { get; set; }

            public string WSCODE { get; set; }

            public string WSDESCRIPTION { get; set; }

            public string WSNAME { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJSUMMARYINFORMATIONMetadata 标识为类
    //，以携带 ZFSJSUMMARYINFORMATION 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJSUMMARYINFORMATION.ZFSJSUMMARYINFORMATIONMetadata))]
    public partial class ZFSJSUMMARYINFORMATION
    {

        // 通过此类可将自定义特性附加到
        //ZFSJSUMMARYINFORMATION 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJSUMMARYINFORMATIONMetadata
        {

            // 元数据类不会实例化。
            private ZFSJSUMMARYINFORMATIONMetadata()
            {
            }

            public string EVENTADDRESS { get; set; }

            public string EVENTSOURCE { get; set; }

            public string EVENTTITLE { get; set; }

            public string GEOMETRY { get; set; }

            public string IMEICODE { get; set; }

            public string REPORTPERSON { get; set; }

            public Nullable<DateTime> REPORTTIME { get; set; }

            public string SSDD { get; set; }

            public string SSZD { get; set; }

            public Nullable<decimal> UNITID { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string WIID { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJTIMELIMITMetadata 标识为类
    //，以携带 ZFSJTIMELIMIT 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJTIMELIMIT.ZFSJTIMELIMITMetadata))]
    public partial class ZFSJTIMELIMIT
    {

        // 通过此类可将自定义特性附加到
        //ZFSJTIMELIMIT 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJTIMELIMITMetadata
        {

            // 元数据类不会实例化。
            private ZFSJTIMELIMITMetadata()
            {
            }

            public decimal ADID { get; set; }

            public string ADNAME { get; set; }

            public string EVENTSOURCETYPE { get; set; }

            public string ID { get; set; }

            public Nullable<decimal> TIMELIMIT { get; set; }
        }
    }

    // MetadataTypeAttribute 将 ZFSJWORKFLOWINSTANCEMetadata 标识为类
    //，以携带 ZFSJWORKFLOWINSTANCE 类的其他元数据。
    [MetadataTypeAttribute(typeof(ZFSJWORKFLOWINSTANCE.ZFSJWORKFLOWINSTANCEMetadata))]
    public partial class ZFSJWORKFLOWINSTANCE
    {

        // 通过此类可将自定义特性附加到
        //ZFSJWORKFLOWINSTANCE 类的属性。
        //
        // 例如，下面的代码将 Xyz 属性标记为
        //必需属性并指定有效值的格式:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ZFSJWORKFLOWINSTANCEMetadata
        {

            // 元数据类不会实例化。
            private ZFSJWORKFLOWINSTANCEMetadata()
            {
            }

            public Nullable<DateTime> CREATETIME { get; set; }

            public string CURRENTAIID { get; set; }

            public Nullable<decimal> EVENTSOURCEID { get; set; }

            public string EVENTSOURCEPKID { get; set; }

            public EntityCollection<GGFWTOZFZD> GGFWTOZFZDS { get; set; }

            public string PHONEID { get; set; }

            public Nullable<decimal> STATUSID { get; set; }

            public Nullable<decimal> UNTIID { get; set; }

            public Nullable<DateTime> UPDATETIME { get; set; }

            public Nullable<decimal> USERID { get; set; }

            public string WDATA { get; set; }

            public string WIID { get; set; }

            public EntityCollection<ZFSJACTIVITYINSTANCE> ZFSJACTIVITYINSTANCES { get; set; }
        }
    }
}
