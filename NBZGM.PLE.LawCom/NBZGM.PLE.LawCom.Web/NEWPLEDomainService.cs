
namespace Taizhou.PLE.LawCom.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // 使用 NEWPLEEntities 上下文实现应用程序逻辑。
    // TODO: 将应用程序逻辑添加到这些方法中或其他方法中。
    // TODO: 连接身份验证(Windows/ASP.NET Forms)并取消注释以下内容，以禁用匿名访问
    //还可考虑添加角色，以根据需要限制访问。
    // [RequiresAuthentication]
    [EnableClientAccess()]
    public partial class NEWPLEDomainService : LinqToEntitiesDomainService<NEWPLEEntities>
    {

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ACITIVITYDEFINITIONS”查询添加顺序。
        public IQueryable<ACITIVITYDEFINITION> GetACITIVITYDEFINITIONS()
        {
            return this.ObjectContext.ACITIVITYDEFINITIONS;
        }

        public void InsertACITIVITYDEFINITION(ACITIVITYDEFINITION aCITIVITYDEFINITION)
        {
            if ((aCITIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCITIVITYDEFINITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ACITIVITYDEFINITIONS.AddObject(aCITIVITYDEFINITION);
            }
        }

        public void UpdateACITIVITYDEFINITION(ACITIVITYDEFINITION currentACITIVITYDEFINITION)
        {
            this.ObjectContext.ACITIVITYDEFINITIONS.AttachAsModified(currentACITIVITYDEFINITION, this.ChangeSet.GetOriginal(currentACITIVITYDEFINITION));
        }

        public void DeleteACITIVITYDEFINITION(ACITIVITYDEFINITION aCITIVITYDEFINITION)
        {
            if ((aCITIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCITIVITYDEFINITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ACITIVITYDEFINITIONS.Attach(aCITIVITYDEFINITION);
                this.ObjectContext.ACITIVITYDEFINITIONS.DeleteObject(aCITIVITYDEFINITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ACTIVITYDEFINITIONDOCPHASES”查询添加顺序。
        public IQueryable<ACTIVITYDEFINITIONDOCPHAS> GetACTIVITYDEFINITIONDOCPHASES()
        {
            return this.ObjectContext.ACTIVITYDEFINITIONDOCPHASES;
        }

        public void InsertACTIVITYDEFINITIONDOCPHAS(ACTIVITYDEFINITIONDOCPHAS aCTIVITYDEFINITIONDOCPHAS)
        {
            if ((aCTIVITYDEFINITIONDOCPHAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYDEFINITIONDOCPHAS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ACTIVITYDEFINITIONDOCPHASES.AddObject(aCTIVITYDEFINITIONDOCPHAS);
            }
        }

        public void UpdateACTIVITYDEFINITIONDOCPHAS(ACTIVITYDEFINITIONDOCPHAS currentACTIVITYDEFINITIONDOCPHAS)
        {
            this.ObjectContext.ACTIVITYDEFINITIONDOCPHASES.AttachAsModified(currentACTIVITYDEFINITIONDOCPHAS, this.ChangeSet.GetOriginal(currentACTIVITYDEFINITIONDOCPHAS));
        }

        public void DeleteACTIVITYDEFINITIONDOCPHAS(ACTIVITYDEFINITIONDOCPHAS aCTIVITYDEFINITIONDOCPHAS)
        {
            if ((aCTIVITYDEFINITIONDOCPHAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYDEFINITIONDOCPHAS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ACTIVITYDEFINITIONDOCPHASES.Attach(aCTIVITYDEFINITIONDOCPHAS);
                this.ObjectContext.ACTIVITYDEFINITIONDOCPHASES.DeleteObject(aCTIVITYDEFINITIONDOCPHAS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ACTIVITYINSTANCES”查询添加顺序。
        public IQueryable<ACTIVITYINSTANCE> GetACTIVITYINSTANCES()
        {
            return this.ObjectContext.ACTIVITYINSTANCES;
        }

        public void InsertACTIVITYINSTANCE(ACTIVITYINSTANCE aCTIVITYINSTANCE)
        {
            if ((aCTIVITYINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ACTIVITYINSTANCES.AddObject(aCTIVITYINSTANCE);
            }
        }

        public void UpdateACTIVITYINSTANCE(ACTIVITYINSTANCE currentACTIVITYINSTANCE)
        {
            this.ObjectContext.ACTIVITYINSTANCES.AttachAsModified(currentACTIVITYINSTANCE, this.ChangeSet.GetOriginal(currentACTIVITYINSTANCE));
        }

        public void DeleteACTIVITYINSTANCE(ACTIVITYINSTANCE aCTIVITYINSTANCE)
        {
            if ((aCTIVITYINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ACTIVITYINSTANCES.Attach(aCTIVITYINSTANCE);
                this.ObjectContext.ACTIVITYINSTANCES.DeleteObject(aCTIVITYINSTANCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ACTIVITYPERMISSIONS”查询添加顺序。
        public IQueryable<ACTIVITYPERMISSION> GetACTIVITYPERMISSIONS()
        {
            return this.ObjectContext.ACTIVITYPERMISSIONS;
        }

        public void InsertACTIVITYPERMISSION(ACTIVITYPERMISSION aCTIVITYPERMISSION)
        {
            if ((aCTIVITYPERMISSION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYPERMISSION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ACTIVITYPERMISSIONS.AddObject(aCTIVITYPERMISSION);
            }
        }

        public void UpdateACTIVITYPERMISSION(ACTIVITYPERMISSION currentACTIVITYPERMISSION)
        {
            this.ObjectContext.ACTIVITYPERMISSIONS.AttachAsModified(currentACTIVITYPERMISSION, this.ChangeSet.GetOriginal(currentACTIVITYPERMISSION));
        }

        public void DeleteACTIVITYPERMISSION(ACTIVITYPERMISSION aCTIVITYPERMISSION)
        {
            if ((aCTIVITYPERMISSION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYPERMISSION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ACTIVITYPERMISSIONS.Attach(aCTIVITYPERMISSION);
                this.ObjectContext.ACTIVITYPERMISSIONS.DeleteObject(aCTIVITYPERMISSION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ACTIVITYPERMISSIONTYPES”查询添加顺序。
        public IQueryable<ACTIVITYPERMISSIONTYPE> GetACTIVITYPERMISSIONTYPES()
        {
            return this.ObjectContext.ACTIVITYPERMISSIONTYPES;
        }

        public void InsertACTIVITYPERMISSIONTYPE(ACTIVITYPERMISSIONTYPE aCTIVITYPERMISSIONTYPE)
        {
            if ((aCTIVITYPERMISSIONTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYPERMISSIONTYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ACTIVITYPERMISSIONTYPES.AddObject(aCTIVITYPERMISSIONTYPE);
            }
        }

        public void UpdateACTIVITYPERMISSIONTYPE(ACTIVITYPERMISSIONTYPE currentACTIVITYPERMISSIONTYPE)
        {
            this.ObjectContext.ACTIVITYPERMISSIONTYPES.AttachAsModified(currentACTIVITYPERMISSIONTYPE, this.ChangeSet.GetOriginal(currentACTIVITYPERMISSIONTYPE));
        }

        public void DeleteACTIVITYPERMISSIONTYPE(ACTIVITYPERMISSIONTYPE aCTIVITYPERMISSIONTYPE)
        {
            if ((aCTIVITYPERMISSIONTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYPERMISSIONTYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ACTIVITYPERMISSIONTYPES.Attach(aCTIVITYPERMISSIONTYPE);
                this.ObjectContext.ACTIVITYPERMISSIONTYPES.DeleteObject(aCTIVITYPERMISSIONTYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ACTIVITYSTATUSES”查询添加顺序。
        public IQueryable<ACTIVITYSTATUS> GetACTIVITYSTATUSES()
        {
            return this.ObjectContext.ACTIVITYSTATUSES;
        }

        public void InsertACTIVITYSTATUS(ACTIVITYSTATUS aCTIVITYSTATUS)
        {
            if ((aCTIVITYSTATUS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYSTATUS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ACTIVITYSTATUSES.AddObject(aCTIVITYSTATUS);
            }
        }

        public void UpdateACTIVITYSTATUS(ACTIVITYSTATUS currentACTIVITYSTATUS)
        {
            this.ObjectContext.ACTIVITYSTATUSES.AttachAsModified(currentACTIVITYSTATUS, this.ChangeSet.GetOriginal(currentACTIVITYSTATUS));
        }

        public void DeleteACTIVITYSTATUS(ACTIVITYSTATUS aCTIVITYSTATUS)
        {
            if ((aCTIVITYSTATUS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aCTIVITYSTATUS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ACTIVITYSTATUSES.Attach(aCTIVITYSTATUS);
                this.ObjectContext.ACTIVITYSTATUSES.DeleteObject(aCTIVITYSTATUS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“APPVERSIONS”查询添加顺序。
        public IQueryable<APPVERSION> GetAPPVERSIONS()
        {
            return this.ObjectContext.APPVERSIONS;
        }

        public void InsertAPPVERSION(APPVERSION aPPVERSION)
        {
            if ((aPPVERSION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aPPVERSION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.APPVERSIONS.AddObject(aPPVERSION);
            }
        }

        public void UpdateAPPVERSION(APPVERSION currentAPPVERSION)
        {
            this.ObjectContext.APPVERSIONS.AttachAsModified(currentAPPVERSION, this.ChangeSet.GetOriginal(currentAPPVERSION));
        }

        public void DeleteAPPVERSION(APPVERSION aPPVERSION)
        {
            if ((aPPVERSION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aPPVERSION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.APPVERSIONS.Attach(aPPVERSION);
                this.ObjectContext.APPVERSIONS.DeleteObject(aPPVERSION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ARTICLES”查询添加顺序。
        public IQueryable<ARTICLE> GetARTICLES()
        {
            return this.ObjectContext.ARTICLES;
        }

        public void InsertARTICLE(ARTICLE aRTICLE)
        {
            if ((aRTICLE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aRTICLE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ARTICLES.AddObject(aRTICLE);
            }
        }

        public void UpdateARTICLE(ARTICLE currentARTICLE)
        {
            this.ObjectContext.ARTICLES.AttachAsModified(currentARTICLE, this.ChangeSet.GetOriginal(currentARTICLE));
        }

        public void DeleteARTICLE(ARTICLE aRTICLE)
        {
            if ((aRTICLE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(aRTICLE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ARTICLES.Attach(aRTICLE);
                this.ObjectContext.ARTICLES.DeleteObject(aRTICLE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CARSYNCPOSITIONS”查询添加顺序。
        public IQueryable<CARSYNCPOSITION> GetCARSYNCPOSITIONS()
        {
            return this.ObjectContext.CARSYNCPOSITIONS;
        }

        public void InsertCARSYNCPOSITION(CARSYNCPOSITION cARSYNCPOSITION)
        {
            if ((cARSYNCPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cARSYNCPOSITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CARSYNCPOSITIONS.AddObject(cARSYNCPOSITION);
            }
        }

        public void UpdateCARSYNCPOSITION(CARSYNCPOSITION currentCARSYNCPOSITION)
        {
            this.ObjectContext.CARSYNCPOSITIONS.AttachAsModified(currentCARSYNCPOSITION, this.ChangeSet.GetOriginal(currentCARSYNCPOSITION));
        }

        public void DeleteCARSYNCPOSITION(CARSYNCPOSITION cARSYNCPOSITION)
        {
            if ((cARSYNCPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cARSYNCPOSITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CARSYNCPOSITIONS.Attach(cARSYNCPOSITION);
                this.ObjectContext.CARSYNCPOSITIONS.DeleteObject(cARSYNCPOSITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CARTYPES”查询添加顺序。
        public IQueryable<CARTYPE> GetCARTYPES()
        {
            return this.ObjectContext.CARTYPES;
        }

        public void InsertCARTYPE(CARTYPE cARTYPE)
        {
            if ((cARTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cARTYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CARTYPES.AddObject(cARTYPE);
            }
        }

        public void UpdateCARTYPE(CARTYPE currentCARTYPE)
        {
            this.ObjectContext.CARTYPES.AttachAsModified(currentCARTYPE, this.ChangeSet.GetOriginal(currentCARTYPE));
        }

        public void DeleteCARTYPE(CARTYPE cARTYPE)
        {
            if ((cARTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cARTYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CARTYPES.Attach(cARTYPE);
                this.ObjectContext.CARTYPES.DeleteObject(cARTYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CASEPHONESMSES”查询添加顺序。
        public IQueryable<CASEPHONESMS> GetCASEPHONESMSES()
        {
            return this.ObjectContext.CASEPHONESMSES;
        }

        public void InsertCASEPHONESMS(CASEPHONESMS cASEPHONESMS)
        {
            if ((cASEPHONESMS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cASEPHONESMS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CASEPHONESMSES.AddObject(cASEPHONESMS);
            }
        }

        public void UpdateCASEPHONESMS(CASEPHONESMS currentCASEPHONESMS)
        {
            this.ObjectContext.CASEPHONESMSES.AttachAsModified(currentCASEPHONESMS, this.ChangeSet.GetOriginal(currentCASEPHONESMS));
        }

        public void DeleteCASEPHONESMS(CASEPHONESMS cASEPHONESMS)
        {
            if ((cASEPHONESMS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cASEPHONESMS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CASEPHONESMSES.Attach(cASEPHONESMS);
                this.ObjectContext.CASEPHONESMSES.DeleteObject(cASEPHONESMS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CASESOURCES”查询添加顺序。
        public IQueryable<CASESOURCE> GetCASESOURCES()
        {
            return this.ObjectContext.CASESOURCES;
        }

        public void InsertCASESOURCE(CASESOURCE cASESOURCE)
        {
            if ((cASESOURCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cASESOURCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CASESOURCES.AddObject(cASESOURCE);
            }
        }

        public void UpdateCASESOURCE(CASESOURCE currentCASESOURCE)
        {
            this.ObjectContext.CASESOURCES.AttachAsModified(currentCASESOURCE, this.ChangeSet.GetOriginal(currentCASESOURCE));
        }

        public void DeleteCASESOURCE(CASESOURCE cASESOURCE)
        {
            if ((cASESOURCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cASESOURCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CASESOURCES.Attach(cASESOURCE);
                this.ObjectContext.CASESOURCES.DeleteObject(cASESOURCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CONSTRSITES”查询添加顺序。
        public IQueryable<CONSTRSITE> GetCONSTRSITES()
        {
            return this.ObjectContext.CONSTRSITES;
        }

        public void InsertCONSTRSITE(CONSTRSITE cONSTRSITE)
        {
            if ((cONSTRSITE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cONSTRSITE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CONSTRSITES.AddObject(cONSTRSITE);
            }
        }

        public void UpdateCONSTRSITE(CONSTRSITE currentCONSTRSITE)
        {
            this.ObjectContext.CONSTRSITES.AttachAsModified(currentCONSTRSITE, this.ChangeSet.GetOriginal(currentCONSTRSITE));
        }

        public void DeleteCONSTRSITE(CONSTRSITE cONSTRSITE)
        {
            if ((cONSTRSITE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cONSTRSITE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CONSTRSITES.Attach(cONSTRSITE);
                this.ObjectContext.CONSTRSITES.DeleteObject(cONSTRSITE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CONTACTS”查询添加顺序。
        public IQueryable<CONTACT> GetCONTACTS()
        {
            return this.ObjectContext.CONTACTS;
        }

        public void InsertCONTACT(CONTACT cONTACT)
        {
            if ((cONTACT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cONTACT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CONTACTS.AddObject(cONTACT);
            }
        }

        public void UpdateCONTACT(CONTACT currentCONTACT)
        {
            this.ObjectContext.CONTACTS.AttachAsModified(currentCONTACT, this.ChangeSet.GetOriginal(currentCONTACT));
        }

        public void DeleteCONTACT(CONTACT cONTACT)
        {
            if ((cONTACT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cONTACT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CONTACTS.Attach(cONTACT);
                this.ObjectContext.CONTACTS.DeleteObject(cONTACT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“CONTACTSGROUPS”查询添加顺序。
        public IQueryable<CONTACTSGROUP> GetCONTACTSGROUPS()
        {
            return this.ObjectContext.CONTACTSGROUPS;
        }

        public void InsertCONTACTSGROUP(CONTACTSGROUP cONTACTSGROUP)
        {
            if ((cONTACTSGROUP.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cONTACTSGROUP, EntityState.Added);
            }
            else
            {
                this.ObjectContext.CONTACTSGROUPS.AddObject(cONTACTSGROUP);
            }
        }

        public void UpdateCONTACTSGROUP(CONTACTSGROUP currentCONTACTSGROUP)
        {
            this.ObjectContext.CONTACTSGROUPS.AttachAsModified(currentCONTACTSGROUP, this.ChangeSet.GetOriginal(currentCONTACTSGROUP));
        }

        public void DeleteCONTACTSGROUP(CONTACTSGROUP cONTACTSGROUP)
        {
            if ((cONTACTSGROUP.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(cONTACTSGROUP, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.CONTACTSGROUPS.Attach(cONTACTSGROUP);
                this.ObjectContext.CONTACTSGROUPS.DeleteObject(cONTACTSGROUP);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“DOCDEFINITIONS”查询添加顺序。
        public IQueryable<DOCDEFINITION> GetDOCDEFINITIONS()
        {
            return this.ObjectContext.DOCDEFINITIONS;
        }

        public void InsertDOCDEFINITION(DOCDEFINITION dOCDEFINITION)
        {
            if ((dOCDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCDEFINITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DOCDEFINITIONS.AddObject(dOCDEFINITION);
            }
        }

        public void UpdateDOCDEFINITION(DOCDEFINITION currentDOCDEFINITION)
        {
            this.ObjectContext.DOCDEFINITIONS.AttachAsModified(currentDOCDEFINITION, this.ChangeSet.GetOriginal(currentDOCDEFINITION));
        }

        public void DeleteDOCDEFINITION(DOCDEFINITION dOCDEFINITION)
        {
            if ((dOCDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCDEFINITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.DOCDEFINITIONS.Attach(dOCDEFINITION);
                this.ObjectContext.DOCDEFINITIONS.DeleteObject(dOCDEFINITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“DOCDEFINITIONRELATIONS”查询添加顺序。
        public IQueryable<DOCDEFINITIONRELATION> GetDOCDEFINITIONRELATIONS()
        {
            return this.ObjectContext.DOCDEFINITIONRELATIONS;
        }

        public void InsertDOCDEFINITIONRELATION(DOCDEFINITIONRELATION dOCDEFINITIONRELATION)
        {
            if ((dOCDEFINITIONRELATION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCDEFINITIONRELATION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DOCDEFINITIONRELATIONS.AddObject(dOCDEFINITIONRELATION);
            }
        }

        public void UpdateDOCDEFINITIONRELATION(DOCDEFINITIONRELATION currentDOCDEFINITIONRELATION)
        {
            this.ObjectContext.DOCDEFINITIONRELATIONS.AttachAsModified(currentDOCDEFINITIONRELATION, this.ChangeSet.GetOriginal(currentDOCDEFINITIONRELATION));
        }

        public void DeleteDOCDEFINITIONRELATION(DOCDEFINITIONRELATION dOCDEFINITIONRELATION)
        {
            if ((dOCDEFINITIONRELATION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCDEFINITIONRELATION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.DOCDEFINITIONRELATIONS.Attach(dOCDEFINITIONRELATION);
                this.ObjectContext.DOCDEFINITIONRELATIONS.DeleteObject(dOCDEFINITIONRELATION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“DOCINSTANCES”查询添加顺序。
        public IQueryable<DOCINSTANCE> GetDOCINSTANCES()
        {
            return this.ObjectContext.DOCINSTANCES;
        }

        public void InsertDOCINSTANCE(DOCINSTANCE dOCINSTANCE)
        {
            if ((dOCINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DOCINSTANCES.AddObject(dOCINSTANCE);
            }
        }

        public void UpdateDOCINSTANCE(DOCINSTANCE currentDOCINSTANCE)
        {
            this.ObjectContext.DOCINSTANCES.AttachAsModified(currentDOCINSTANCE, this.ChangeSet.GetOriginal(currentDOCINSTANCE));
        }

        public void DeleteDOCINSTANCE(DOCINSTANCE dOCINSTANCE)
        {
            if ((dOCINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.DOCINSTANCES.Attach(dOCINSTANCE);
                this.ObjectContext.DOCINSTANCES.DeleteObject(dOCINSTANCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“DOCPHASES”查询添加顺序。
        public IQueryable<DOCPHAS> GetDOCPHASES()
        {
            return this.ObjectContext.DOCPHASES;
        }

        public void InsertDOCPHAS(DOCPHAS dOCPHAS)
        {
            if ((dOCPHAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCPHAS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DOCPHASES.AddObject(dOCPHAS);
            }
        }

        public void UpdateDOCPHAS(DOCPHAS currentDOCPHAS)
        {
            this.ObjectContext.DOCPHASES.AttachAsModified(currentDOCPHAS, this.ChangeSet.GetOriginal(currentDOCPHAS));
        }

        public void DeleteDOCPHAS(DOCPHAS dOCPHAS)
        {
            if ((dOCPHAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dOCPHAS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.DOCPHASES.Attach(dOCPHAS);
                this.ObjectContext.DOCPHASES.DeleteObject(dOCPHAS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“DUMPINGSITES”查询添加顺序。
        public IQueryable<DUMPINGSITE> GetDUMPINGSITES()
        {
            return this.ObjectContext.DUMPINGSITES;
        }

        public void InsertDUMPINGSITE(DUMPINGSITE dUMPINGSITE)
        {
            if ((dUMPINGSITE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dUMPINGSITE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.DUMPINGSITES.AddObject(dUMPINGSITE);
            }
        }

        public void UpdateDUMPINGSITE(DUMPINGSITE currentDUMPINGSITE)
        {
            this.ObjectContext.DUMPINGSITES.AttachAsModified(currentDUMPINGSITE, this.ChangeSet.GetOriginal(currentDUMPINGSITE));
        }

        public void DeleteDUMPINGSITE(DUMPINGSITE dUMPINGSITE)
        {
            if ((dUMPINGSITE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(dUMPINGSITE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.DUMPINGSITES.Attach(dUMPINGSITE);
                this.ObjectContext.DUMPINGSITES.DeleteObject(dUMPINGSITE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“FUNCTIONS”查询添加顺序。
        public IQueryable<FUNCTION> GetFUNCTIONS()
        {
            return this.ObjectContext.FUNCTIONS;
        }

        public void InsertFUNCTION(FUNCTION fUNCTION)
        {
            if ((fUNCTION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fUNCTION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.FUNCTIONS.AddObject(fUNCTION);
            }
        }

        public void UpdateFUNCTION(FUNCTION currentFUNCTION)
        {
            this.ObjectContext.FUNCTIONS.AttachAsModified(currentFUNCTION, this.ChangeSet.GetOriginal(currentFUNCTION));
        }

        public void DeleteFUNCTION(FUNCTION fUNCTION)
        {
            if ((fUNCTION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(fUNCTION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.FUNCTIONS.Attach(fUNCTION);
                this.ObjectContext.FUNCTIONS.DeleteObject(fUNCTION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWCASENAMES”查询添加顺序。
        public IQueryable<GGFWCASENAME> GetGGFWCASENAMES()
        {
            return this.ObjectContext.GGFWCASENAMES;
        }

        public void InsertGGFWCASENAME(GGFWCASENAME gGFWCASENAME)
        {
            if ((gGFWCASENAME.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWCASENAME, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWCASENAMES.AddObject(gGFWCASENAME);
            }
        }

        public void UpdateGGFWCASENAME(GGFWCASENAME currentGGFWCASENAME)
        {
            this.ObjectContext.GGFWCASENAMES.AttachAsModified(currentGGFWCASENAME, this.ChangeSet.GetOriginal(currentGGFWCASENAME));
        }

        public void DeleteGGFWCASENAME(GGFWCASENAME gGFWCASENAME)
        {
            if ((gGFWCASENAME.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWCASENAME, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWCASENAMES.Attach(gGFWCASENAME);
                this.ObjectContext.GGFWCASENAMES.DeleteObject(gGFWCASENAME);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWEVENTS”查询添加顺序。
        public IQueryable<GGFWEVENT> GetGGFWEVENTS()
        {
            return this.ObjectContext.GGFWEVENTS;
        }

        public void InsertGGFWEVENT(GGFWEVENT gGFWEVENT)
        {
            if ((gGFWEVENT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWEVENT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWEVENTS.AddObject(gGFWEVENT);
            }
        }

        public void UpdateGGFWEVENT(GGFWEVENT currentGGFWEVENT)
        {
            this.ObjectContext.GGFWEVENTS.AttachAsModified(currentGGFWEVENT, this.ChangeSet.GetOriginal(currentGGFWEVENT));
        }

        public void DeleteGGFWEVENT(GGFWEVENT gGFWEVENT)
        {
            if ((gGFWEVENT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWEVENT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWEVENTS.Attach(gGFWEVENT);
                this.ObjectContext.GGFWEVENTS.DeleteObject(gGFWEVENT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWMONTHLYREPORTS”查询添加顺序。
        public IQueryable<GGFWMONTHLYREPORT> GetGGFWMONTHLYREPORTS()
        {
            return this.ObjectContext.GGFWMONTHLYREPORTS;
        }

        public void InsertGGFWMONTHLYREPORT(GGFWMONTHLYREPORT gGFWMONTHLYREPORT)
        {
            if ((gGFWMONTHLYREPORT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWMONTHLYREPORT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWMONTHLYREPORTS.AddObject(gGFWMONTHLYREPORT);
            }
        }

        public void UpdateGGFWMONTHLYREPORT(GGFWMONTHLYREPORT currentGGFWMONTHLYREPORT)
        {
            this.ObjectContext.GGFWMONTHLYREPORTS.AttachAsModified(currentGGFWMONTHLYREPORT, this.ChangeSet.GetOriginal(currentGGFWMONTHLYREPORT));
        }

        public void DeleteGGFWMONTHLYREPORT(GGFWMONTHLYREPORT gGFWMONTHLYREPORT)
        {
            if ((gGFWMONTHLYREPORT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWMONTHLYREPORT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWMONTHLYREPORTS.Attach(gGFWMONTHLYREPORT);
                this.ObjectContext.GGFWMONTHLYREPORTS.DeleteObject(gGFWMONTHLYREPORT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWSOURCES”查询添加顺序。
        public IQueryable<GGFWSOURCE> GetGGFWSOURCES()
        {
            return this.ObjectContext.GGFWSOURCES;
        }

        public void InsertGGFWSOURCE(GGFWSOURCE gGFWSOURCE)
        {
            if ((gGFWSOURCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWSOURCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWSOURCES.AddObject(gGFWSOURCE);
            }
        }

        public void UpdateGGFWSOURCE(GGFWSOURCE currentGGFWSOURCE)
        {
            this.ObjectContext.GGFWSOURCES.AttachAsModified(currentGGFWSOURCE, this.ChangeSet.GetOriginal(currentGGFWSOURCE));
        }

        public void DeleteGGFWSOURCE(GGFWSOURCE gGFWSOURCE)
        {
            if ((gGFWSOURCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWSOURCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWSOURCES.Attach(gGFWSOURCE);
                this.ObjectContext.GGFWSOURCES.DeleteObject(gGFWSOURCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWSTATUES”查询添加顺序。
        public IQueryable<GGFWSTATUE> GetGGFWSTATUES()
        {
            return this.ObjectContext.GGFWSTATUES;
        }

        public void InsertGGFWSTATUE(GGFWSTATUE gGFWSTATUE)
        {
            if ((gGFWSTATUE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWSTATUE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWSTATUES.AddObject(gGFWSTATUE);
            }
        }

        public void UpdateGGFWSTATUE(GGFWSTATUE currentGGFWSTATUE)
        {
            this.ObjectContext.GGFWSTATUES.AttachAsModified(currentGGFWSTATUE, this.ChangeSet.GetOriginal(currentGGFWSTATUE));
        }

        public void DeleteGGFWSTATUE(GGFWSTATUE gGFWSTATUE)
        {
            if ((gGFWSTATUE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWSTATUE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWSTATUES.Attach(gGFWSTATUE);
                this.ObjectContext.GGFWSTATUES.DeleteObject(gGFWSTATUE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWTOZFZDS”查询添加顺序。
        public IQueryable<GGFWTOZFZD> GetGGFWTOZFZDS()
        {
            return this.ObjectContext.GGFWTOZFZDS;
        }

        public void InsertGGFWTOZFZD(GGFWTOZFZD gGFWTOZFZD)
        {
            if ((gGFWTOZFZD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWTOZFZD, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWTOZFZDS.AddObject(gGFWTOZFZD);
            }
        }

        public void UpdateGGFWTOZFZD(GGFWTOZFZD currentGGFWTOZFZD)
        {
            this.ObjectContext.GGFWTOZFZDS.AttachAsModified(currentGGFWTOZFZD, this.ChangeSet.GetOriginal(currentGGFWTOZFZD));
        }

        public void DeleteGGFWTOZFZD(GGFWTOZFZD gGFWTOZFZD)
        {
            if ((gGFWTOZFZD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWTOZFZD, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWTOZFZDS.Attach(gGFWTOZFZD);
                this.ObjectContext.GGFWTOZFZDS.DeleteObject(gGFWTOZFZD);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GGFWXFDOCS”查询添加顺序。
        public IQueryable<GGFWXFDOC> GetGGFWXFDOCS()
        {
            return this.ObjectContext.GGFWXFDOCS;
        }

        public void InsertGGFWXFDOC(GGFWXFDOC gGFWXFDOC)
        {
            if ((gGFWXFDOC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWXFDOC, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GGFWXFDOCS.AddObject(gGFWXFDOC);
            }
        }

        public void UpdateGGFWXFDOC(GGFWXFDOC currentGGFWXFDOC)
        {
            this.ObjectContext.GGFWXFDOCS.AttachAsModified(currentGGFWXFDOC, this.ChangeSet.GetOriginal(currentGGFWXFDOC));
        }

        public void DeleteGGFWXFDOC(GGFWXFDOC gGFWXFDOC)
        {
            if ((gGFWXFDOC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gGFWXFDOC, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GGFWXFDOCS.Attach(gGFWXFDOC);
                this.ObjectContext.GGFWXFDOCS.DeleteObject(gGFWXFDOC);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“GROUPS”查询添加顺序。
        public IQueryable<GROUP> GetGROUPS()
        {
            return this.ObjectContext.GROUPS;
        }

        public void InsertGROUP(GROUP gROUP)
        {
            if ((gROUP.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gROUP, EntityState.Added);
            }
            else
            {
                this.ObjectContext.GROUPS.AddObject(gROUP);
            }
        }

        public void UpdateGROUP(GROUP currentGROUP)
        {
            this.ObjectContext.GROUPS.AttachAsModified(currentGROUP, this.ChangeSet.GetOriginal(currentGROUP));
        }

        public void DeleteGROUP(GROUP gROUP)
        {
            if ((gROUP.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(gROUP, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.GROUPS.Attach(gROUP);
                this.ObjectContext.GROUPS.DeleteObject(gROUP);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ILLEGALCLASSES”查询添加顺序。
        public IQueryable<ILLEGALCLASS> GetILLEGALCLASSES()
        {
            return this.ObjectContext.ILLEGALCLASSES;
        }

        public void InsertILLEGALCLASS(ILLEGALCLASS iLLEGALCLASS)
        {
            if ((iLLEGALCLASS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(iLLEGALCLASS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ILLEGALCLASSES.AddObject(iLLEGALCLASS);
            }
        }

        public void UpdateILLEGALCLASS(ILLEGALCLASS currentILLEGALCLASS)
        {
            this.ObjectContext.ILLEGALCLASSES.AttachAsModified(currentILLEGALCLASS, this.ChangeSet.GetOriginal(currentILLEGALCLASS));
        }

        public void DeleteILLEGALCLASS(ILLEGALCLASS iLLEGALCLASS)
        {
            if ((iLLEGALCLASS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(iLLEGALCLASS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ILLEGALCLASSES.Attach(iLLEGALCLASS);
                this.ObjectContext.ILLEGALCLASSES.DeleteObject(iLLEGALCLASS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ILLEGALITEMS”查询添加顺序。
        public IQueryable<ILLEGALITEM> GetILLEGALITEMS()
        {
            return this.ObjectContext.ILLEGALITEMS;
        }

        public void InsertILLEGALITEM(ILLEGALITEM iLLEGALITEM)
        {
            if ((iLLEGALITEM.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(iLLEGALITEM, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ILLEGALITEMS.AddObject(iLLEGALITEM);
            }
        }

        public void UpdateILLEGALITEM(ILLEGALITEM currentILLEGALITEM)
        {
            this.ObjectContext.ILLEGALITEMS.AttachAsModified(currentILLEGALITEM, this.ChangeSet.GetOriginal(currentILLEGALITEM));
        }

        public void DeleteILLEGALITEM(ILLEGALITEM iLLEGALITEM)
        {
            if ((iLLEGALITEM.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(iLLEGALITEM, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ILLEGALITEMS.Attach(iLLEGALITEM);
                this.ObjectContext.ILLEGALITEMS.DeleteObject(iLLEGALITEM);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“LAYERTYPEs”查询添加顺序。
        public IQueryable<LAYERTYPE> GetLAYERTYPEs()
        {
            return this.ObjectContext.LAYERTYPEs;
        }

        public void InsertLAYERTYPE(LAYERTYPE lAYERTYPE)
        {
            if ((lAYERTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(lAYERTYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.LAYERTYPEs.AddObject(lAYERTYPE);
            }
        }

        public void UpdateLAYERTYPE(LAYERTYPE currentLAYERTYPE)
        {
            this.ObjectContext.LAYERTYPEs.AttachAsModified(currentLAYERTYPE, this.ChangeSet.GetOriginal(currentLAYERTYPE));
        }

        public void DeleteLAYERTYPE(LAYERTYPE lAYERTYPE)
        {
            if ((lAYERTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(lAYERTYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.LAYERTYPEs.Attach(lAYERTYPE);
                this.ObjectContext.LAYERTYPEs.DeleteObject(lAYERTYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“LEADERWEEKWORKPLANS”查询添加顺序。
        public IQueryable<LEADERWEEKWORKPLAN> GetLEADERWEEKWORKPLANS()
        {
            return this.ObjectContext.LEADERWEEKWORKPLANS;
        }

        public void InsertLEADERWEEKWORKPLAN(LEADERWEEKWORKPLAN lEADERWEEKWORKPLAN)
        {
            if ((lEADERWEEKWORKPLAN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(lEADERWEEKWORKPLAN, EntityState.Added);
            }
            else
            {
                this.ObjectContext.LEADERWEEKWORKPLANS.AddObject(lEADERWEEKWORKPLAN);
            }
        }

        public void UpdateLEADERWEEKWORKPLAN(LEADERWEEKWORKPLAN currentLEADERWEEKWORKPLAN)
        {
            this.ObjectContext.LEADERWEEKWORKPLANS.AttachAsModified(currentLEADERWEEKWORKPLAN, this.ChangeSet.GetOriginal(currentLEADERWEEKWORKPLAN));
        }

        public void DeleteLEADERWEEKWORKPLAN(LEADERWEEKWORKPLAN lEADERWEEKWORKPLAN)
        {
            if ((lEADERWEEKWORKPLAN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(lEADERWEEKWORKPLAN, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.LEADERWEEKWORKPLANS.Attach(lEADERWEEKWORKPLAN);
                this.ObjectContext.LEADERWEEKWORKPLANS.DeleteObject(lEADERWEEKWORKPLAN);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“MAPINFOS”查询添加顺序。
        public IQueryable<MAPINFO> GetMAPINFOS()
        {
            return this.ObjectContext.MAPINFOS;
        }

        public void InsertMAPINFO(MAPINFO mAPINFO)
        {
            if ((mAPINFO.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mAPINFO, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MAPINFOS.AddObject(mAPINFO);
            }
        }

        public void UpdateMAPINFO(MAPINFO currentMAPINFO)
        {
            this.ObjectContext.MAPINFOS.AttachAsModified(currentMAPINFO, this.ChangeSet.GetOriginal(currentMAPINFO));
        }

        public void DeleteMAPINFO(MAPINFO mAPINFO)
        {
            if ((mAPINFO.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mAPINFO, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MAPINFOS.Attach(mAPINFO);
                this.ObjectContext.MAPINFOS.DeleteObject(mAPINFO);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“MENUS”查询添加顺序。
        public IQueryable<MENU> GetMENUS()
        {
            return this.ObjectContext.MENUS;
        }

        public void InsertMENU(MENU mENU)
        {
            if ((mENU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mENU, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MENUS.AddObject(mENU);
            }
        }

        public void UpdateMENU(MENU currentMENU)
        {
            this.ObjectContext.MENUS.AttachAsModified(currentMENU, this.ChangeSet.GetOriginal(currentMENU));
        }

        public void DeleteMENU(MENU mENU)
        {
            if ((mENU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mENU, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MENUS.Attach(mENU);
                this.ObjectContext.MENUS.DeleteObject(mENU);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“MESSAGES”查询添加顺序。
        public IQueryable<MESSAGE> GetMESSAGES()
        {
            return this.ObjectContext.MESSAGES;
        }

        public void InsertMESSAGE(MESSAGE mESSAGE)
        {
            if ((mESSAGE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mESSAGE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MESSAGES.AddObject(mESSAGE);
            }
        }

        public void UpdateMESSAGE(MESSAGE currentMESSAGE)
        {
            this.ObjectContext.MESSAGES.AttachAsModified(currentMESSAGE, this.ChangeSet.GetOriginal(currentMESSAGE));
        }

        public void DeleteMESSAGE(MESSAGE mESSAGE)
        {
            if ((mESSAGE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mESSAGE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MESSAGES.Attach(mESSAGE);
                this.ObjectContext.MESSAGES.DeleteObject(mESSAGE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“MESSAGETOUSERS”查询添加顺序。
        public IQueryable<MESSAGETOUSER> GetMESSAGETOUSERS()
        {
            return this.ObjectContext.MESSAGETOUSERS;
        }

        public void InsertMESSAGETOUSER(MESSAGETOUSER mESSAGETOUSER)
        {
            if ((mESSAGETOUSER.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mESSAGETOUSER, EntityState.Added);
            }
            else
            {
                this.ObjectContext.MESSAGETOUSERS.AddObject(mESSAGETOUSER);
            }
        }

        public void UpdateMESSAGETOUSER(MESSAGETOUSER currentMESSAGETOUSER)
        {
            this.ObjectContext.MESSAGETOUSERS.AttachAsModified(currentMESSAGETOUSER, this.ChangeSet.GetOriginal(currentMESSAGETOUSER));
        }

        public void DeleteMESSAGETOUSER(MESSAGETOUSER mESSAGETOUSER)
        {
            if ((mESSAGETOUSER.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(mESSAGETOUSER, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.MESSAGETOUSERS.Attach(mESSAGETOUSER);
                this.ObjectContext.MESSAGETOUSERS.DeleteObject(mESSAGETOUSER);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ONDUTIES”查询添加顺序。
        public IQueryable<ONDUTy> GetONDUTIES()
        {
            return this.ObjectContext.ONDUTIES;
        }

        public void InsertONDUTy(ONDUTy oNDUTy)
        {
            if ((oNDUTy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(oNDUTy, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ONDUTIES.AddObject(oNDUTy);
            }
        }

        public void UpdateONDUTy(ONDUTy currentONDUTy)
        {
            this.ObjectContext.ONDUTIES.AttachAsModified(currentONDUTy, this.ChangeSet.GetOriginal(currentONDUTy));
        }

        public void DeleteONDUTy(ONDUTy oNDUTy)
        {
            if ((oNDUTy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(oNDUTy, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ONDUTIES.Attach(oNDUTy);
                this.ObjectContext.ONDUTIES.DeleteObject(oNDUTy);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“PHONEERRORS”查询添加顺序。
        public IQueryable<PHONEERROR> GetPHONEERRORS()
        {
            return this.ObjectContext.PHONEERRORS;
        }

        public void InsertPHONEERROR(PHONEERROR pHONEERROR)
        {
            if ((pHONEERROR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pHONEERROR, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PHONEERRORS.AddObject(pHONEERROR);
            }
        }

        public void UpdatePHONEERROR(PHONEERROR currentPHONEERROR)
        {
            this.ObjectContext.PHONEERRORS.AttachAsModified(currentPHONEERROR, this.ChangeSet.GetOriginal(currentPHONEERROR));
        }

        public void DeletePHONEERROR(PHONEERROR pHONEERROR)
        {
            if ((pHONEERROR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pHONEERROR, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PHONEERRORS.Attach(pHONEERROR);
                this.ObjectContext.PHONEERRORS.DeleteObject(pHONEERROR);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“PORTALCATEGORIES”查询添加顺序。
        public IQueryable<PORTALCATEGORy> GetPORTALCATEGORIES()
        {
            return this.ObjectContext.PORTALCATEGORIES;
        }

        public void InsertPORTALCATEGORy(PORTALCATEGORy pORTALCATEGORy)
        {
            if ((pORTALCATEGORy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pORTALCATEGORy, EntityState.Added);
            }
            else
            {
                this.ObjectContext.PORTALCATEGORIES.AddObject(pORTALCATEGORy);
            }
        }

        public void UpdatePORTALCATEGORy(PORTALCATEGORy currentPORTALCATEGORy)
        {
            this.ObjectContext.PORTALCATEGORIES.AttachAsModified(currentPORTALCATEGORy, this.ChangeSet.GetOriginal(currentPORTALCATEGORy));
        }

        public void DeletePORTALCATEGORy(PORTALCATEGORy pORTALCATEGORy)
        {
            if ((pORTALCATEGORy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(pORTALCATEGORy, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.PORTALCATEGORIES.Attach(pORTALCATEGORy);
                this.ObjectContext.PORTALCATEGORIES.DeleteObject(pORTALCATEGORy);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“QUESTIONIDS”查询添加顺序。
        public IQueryable<QUESTIONID> GetQUESTIONIDS()
        {
            return this.ObjectContext.QUESTIONIDS;
        }

        public void InsertQUESTIONID(QUESTIONID qUESTIONID)
        {
            if ((qUESTIONID.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(qUESTIONID, EntityState.Added);
            }
            else
            {
                this.ObjectContext.QUESTIONIDS.AddObject(qUESTIONID);
            }
        }

        public void UpdateQUESTIONID(QUESTIONID currentQUESTIONID)
        {
            this.ObjectContext.QUESTIONIDS.AttachAsModified(currentQUESTIONID, this.ChangeSet.GetOriginal(currentQUESTIONID));
        }

        public void DeleteQUESTIONID(QUESTIONID qUESTIONID)
        {
            if ((qUESTIONID.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(qUESTIONID, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.QUESTIONIDS.Attach(qUESTIONID);
                this.ObjectContext.QUESTIONIDS.DeleteObject(qUESTIONID);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“QUESTIONLISTS”查询添加顺序。
        public IQueryable<QUESTIONLIST> GetQUESTIONLISTS()
        {
            return this.ObjectContext.QUESTIONLISTS;
        }

        public void InsertQUESTIONLIST(QUESTIONLIST qUESTIONLIST)
        {
            if ((qUESTIONLIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(qUESTIONLIST, EntityState.Added);
            }
            else
            {
                this.ObjectContext.QUESTIONLISTS.AddObject(qUESTIONLIST);
            }
        }

        public void UpdateQUESTIONLIST(QUESTIONLIST currentQUESTIONLIST)
        {
            this.ObjectContext.QUESTIONLISTS.AttachAsModified(currentQUESTIONLIST, this.ChangeSet.GetOriginal(currentQUESTIONLIST));
        }

        public void DeleteQUESTIONLIST(QUESTIONLIST qUESTIONLIST)
        {
            if ((qUESTIONLIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(qUESTIONLIST, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.QUESTIONLISTS.Attach(qUESTIONLIST);
                this.ObjectContext.QUESTIONLISTS.DeleteObject(qUESTIONLIST);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“RCDCEVENTS”查询添加顺序。
        public IQueryable<RCDCEVENT> GetRCDCEVENTS()
        {
            return this.ObjectContext.RCDCEVENTS;
        }

        public void InsertRCDCEVENT(RCDCEVENT rCDCEVENT)
        {
            if ((rCDCEVENT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rCDCEVENT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RCDCEVENTS.AddObject(rCDCEVENT);
            }
        }

        public void UpdateRCDCEVENT(RCDCEVENT currentRCDCEVENT)
        {
            this.ObjectContext.RCDCEVENTS.AttachAsModified(currentRCDCEVENT, this.ChangeSet.GetOriginal(currentRCDCEVENT));
        }

        public void DeleteRCDCEVENT(RCDCEVENT rCDCEVENT)
        {
            if ((rCDCEVENT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rCDCEVENT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.RCDCEVENTS.Attach(rCDCEVENT);
                this.ObjectContext.RCDCEVENTS.DeleteObject(rCDCEVENT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“RCDCTOZFZDS”查询添加顺序。
        public IQueryable<RCDCTOZFZD> GetRCDCTOZFZDS()
        {
            return this.ObjectContext.RCDCTOZFZDS;
        }

        public void InsertRCDCTOZFZD(RCDCTOZFZD rCDCTOZFZD)
        {
            if ((rCDCTOZFZD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rCDCTOZFZD, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RCDCTOZFZDS.AddObject(rCDCTOZFZD);
            }
        }

        public void UpdateRCDCTOZFZD(RCDCTOZFZD currentRCDCTOZFZD)
        {
            this.ObjectContext.RCDCTOZFZDS.AttachAsModified(currentRCDCTOZFZD, this.ChangeSet.GetOriginal(currentRCDCTOZFZD));
        }

        public void DeleteRCDCTOZFZD(RCDCTOZFZD rCDCTOZFZD)
        {
            if ((rCDCTOZFZD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rCDCTOZFZD, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.RCDCTOZFZDS.Attach(rCDCTOZFZD);
                this.ObjectContext.RCDCTOZFZDS.DeleteObject(rCDCTOZFZD);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“RECIPES”查询添加顺序。
        public IQueryable<RECIPE> GetRECIPES()
        {
            return this.ObjectContext.RECIPES;
        }

        public void InsertRECIPE(RECIPE rECIPE)
        {
            if ((rECIPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rECIPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.RECIPES.AddObject(rECIPE);
            }
        }

        public void UpdateRECIPE(RECIPE currentRECIPE)
        {
            this.ObjectContext.RECIPES.AttachAsModified(currentRECIPE, this.ChangeSet.GetOriginal(currentRECIPE));
        }

        public void DeleteRECIPE(RECIPE rECIPE)
        {
            if ((rECIPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rECIPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.RECIPES.Attach(rECIPE);
                this.ObjectContext.RECIPES.DeleteObject(rECIPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ROLES”查询添加顺序。
        public IQueryable<ROLE> GetROLES()
        {
            return this.ObjectContext.ROLES;
        }

        public void InsertROLE(ROLE rOLE)
        {
            if ((rOLE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rOLE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ROLES.AddObject(rOLE);
            }
        }

        public void UpdateROLE(ROLE currentROLE)
        {
            this.ObjectContext.ROLES.AttachAsModified(currentROLE, this.ChangeSet.GetOriginal(currentROLE));
        }

        public void DeleteROLE(ROLE rOLE)
        {
            if ((rOLE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rOLE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ROLES.Attach(rOLE);
                this.ObjectContext.ROLES.DeleteObject(rOLE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ROLEFUNCTIONS”查询添加顺序。
        public IQueryable<ROLEFUNCTION> GetROLEFUNCTIONS()
        {
            return this.ObjectContext.ROLEFUNCTIONS;
        }

        public void InsertROLEFUNCTION(ROLEFUNCTION rOLEFUNCTION)
        {
            if ((rOLEFUNCTION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rOLEFUNCTION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ROLEFUNCTIONS.AddObject(rOLEFUNCTION);
            }
        }

        public void UpdateROLEFUNCTION(ROLEFUNCTION currentROLEFUNCTION)
        {
            this.ObjectContext.ROLEFUNCTIONS.AttachAsModified(currentROLEFUNCTION, this.ChangeSet.GetOriginal(currentROLEFUNCTION));
        }

        public void DeleteROLEFUNCTION(ROLEFUNCTION rOLEFUNCTION)
        {
            if ((rOLEFUNCTION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rOLEFUNCTION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ROLEFUNCTIONS.Attach(rOLEFUNCTION);
                this.ObjectContext.ROLEFUNCTIONS.DeleteObject(rOLEFUNCTION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ROLEMENUS”查询添加顺序。
        public IQueryable<ROLEMENU> GetROLEMENUS()
        {
            return this.ObjectContext.ROLEMENUS;
        }

        public void InsertROLEMENU(ROLEMENU rOLEMENU)
        {
            if ((rOLEMENU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rOLEMENU, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ROLEMENUS.AddObject(rOLEMENU);
            }
        }

        public void UpdateROLEMENU(ROLEMENU currentROLEMENU)
        {
            this.ObjectContext.ROLEMENUS.AttachAsModified(currentROLEMENU, this.ChangeSet.GetOriginal(currentROLEMENU));
        }

        public void DeleteROLEMENU(ROLEMENU rOLEMENU)
        {
            if ((rOLEMENU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(rOLEMENU, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ROLEMENUS.Attach(rOLEMENU);
                this.ObjectContext.ROLEMENUS.DeleteObject(rOLEMENU);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SCHEDULES”查询添加顺序。
        public IQueryable<SCHEDULE> GetSCHEDULES()
        {
            return this.ObjectContext.SCHEDULES;
        }

        public void InsertSCHEDULE(SCHEDULE sCHEDULE)
        {
            if ((sCHEDULE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sCHEDULE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SCHEDULES.AddObject(sCHEDULE);
            }
        }

        public void UpdateSCHEDULE(SCHEDULE currentSCHEDULE)
        {
            this.ObjectContext.SCHEDULES.AttachAsModified(currentSCHEDULE, this.ChangeSet.GetOriginal(currentSCHEDULE));
        }

        public void DeleteSCHEDULE(SCHEDULE sCHEDULE)
        {
            if ((sCHEDULE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sCHEDULE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SCHEDULES.Attach(sCHEDULE);
                this.ObjectContext.SCHEDULES.DeleteObject(sCHEDULE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SCHEDULETYPES”查询添加顺序。
        public IQueryable<SCHEDULETYPE> GetSCHEDULETYPES()
        {
            return this.ObjectContext.SCHEDULETYPES;
        }

        public void InsertSCHEDULETYPE(SCHEDULETYPE sCHEDULETYPE)
        {
            if ((sCHEDULETYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sCHEDULETYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SCHEDULETYPES.AddObject(sCHEDULETYPE);
            }
        }

        public void UpdateSCHEDULETYPE(SCHEDULETYPE currentSCHEDULETYPE)
        {
            this.ObjectContext.SCHEDULETYPES.AttachAsModified(currentSCHEDULETYPE, this.ChangeSet.GetOriginal(currentSCHEDULETYPE));
        }

        public void DeleteSCHEDULETYPE(SCHEDULETYPE sCHEDULETYPE)
        {
            if ((sCHEDULETYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sCHEDULETYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SCHEDULETYPES.Attach(sCHEDULETYPE);
                this.ObjectContext.SCHEDULETYPES.DeleteObject(sCHEDULETYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SIMPLECASES”查询添加顺序。
        public IQueryable<SIMPLECAS> GetSIMPLECASES()
        {
            return this.ObjectContext.SIMPLECASES;
        }

        public void InsertSIMPLECAS(SIMPLECAS sIMPLECAS)
        {
            if ((sIMPLECAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sIMPLECAS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SIMPLECASES.AddObject(sIMPLECAS);
            }
        }

        public void UpdateSIMPLECAS(SIMPLECAS currentSIMPLECAS)
        {
            this.ObjectContext.SIMPLECASES.AttachAsModified(currentSIMPLECAS, this.ChangeSet.GetOriginal(currentSIMPLECAS));
        }

        public void DeleteSIMPLECAS(SIMPLECAS sIMPLECAS)
        {
            if ((sIMPLECAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sIMPLECAS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SIMPLECASES.Attach(sIMPLECAS);
                this.ObjectContext.SIMPLECASES.DeleteObject(sIMPLECAS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SIMPLECASEPICTURES”查询添加顺序。
        public IQueryable<SIMPLECASEPICTURE> GetSIMPLECASEPICTURES()
        {
            return this.ObjectContext.SIMPLECASEPICTURES;
        }

        public void InsertSIMPLECASEPICTURE(SIMPLECASEPICTURE sIMPLECASEPICTURE)
        {
            if ((sIMPLECASEPICTURE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sIMPLECASEPICTURE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SIMPLECASEPICTURES.AddObject(sIMPLECASEPICTURE);
            }
        }

        public void UpdateSIMPLECASEPICTURE(SIMPLECASEPICTURE currentSIMPLECASEPICTURE)
        {
            this.ObjectContext.SIMPLECASEPICTURES.AttachAsModified(currentSIMPLECASEPICTURE, this.ChangeSet.GetOriginal(currentSIMPLECASEPICTURE));
        }

        public void DeleteSIMPLECASEPICTURE(SIMPLECASEPICTURE sIMPLECASEPICTURE)
        {
            if ((sIMPLECASEPICTURE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sIMPLECASEPICTURE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SIMPLECASEPICTURES.Attach(sIMPLECASEPICTURE);
                this.ObjectContext.SIMPLECASEPICTURES.DeleteObject(sIMPLECASEPICTURE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SINGNINS”查询添加顺序。
        public IQueryable<SINGNIN> GetSINGNINS()
        {
            return this.ObjectContext.SINGNINS;
        }

        public void InsertSINGNIN(SINGNIN sINGNIN)
        {
            if ((sINGNIN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sINGNIN, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SINGNINS.AddObject(sINGNIN);
            }
        }

        public void UpdateSINGNIN(SINGNIN currentSINGNIN)
        {
            this.ObjectContext.SINGNINS.AttachAsModified(currentSINGNIN, this.ChangeSet.GetOriginal(currentSINGNIN));
        }

        public void DeleteSINGNIN(SINGNIN sINGNIN)
        {
            if ((sINGNIN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sINGNIN, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SINGNINS.Attach(sINGNIN);
                this.ObjectContext.SINGNINS.DeleteObject(sINGNIN);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SPECIALACTIVITICES”查询添加顺序。
        public IQueryable<SPECIALACTIVITICE> GetSPECIALACTIVITICES()
        {
            return this.ObjectContext.SPECIALACTIVITICES;
        }

        public void InsertSPECIALACTIVITICE(SPECIALACTIVITICE sPECIALACTIVITICE)
        {
            if ((sPECIALACTIVITICE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALACTIVITICE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SPECIALACTIVITICES.AddObject(sPECIALACTIVITICE);
            }
        }

        public void UpdateSPECIALACTIVITICE(SPECIALACTIVITICE currentSPECIALACTIVITICE)
        {
            this.ObjectContext.SPECIALACTIVITICES.AttachAsModified(currentSPECIALACTIVITICE, this.ChangeSet.GetOriginal(currentSPECIALACTIVITICE));
        }

        public void DeleteSPECIALACTIVITICE(SPECIALACTIVITICE sPECIALACTIVITICE)
        {
            if ((sPECIALACTIVITICE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALACTIVITICE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SPECIALACTIVITICES.Attach(sPECIALACTIVITICE);
                this.ObjectContext.SPECIALACTIVITICES.DeleteObject(sPECIALACTIVITICE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SPECIALACTIVITYDEFINITONS”查询添加顺序。
        public IQueryable<SPECIALACTIVITYDEFINITON> GetSPECIALACTIVITYDEFINITONS()
        {
            return this.ObjectContext.SPECIALACTIVITYDEFINITONS;
        }

        public void InsertSPECIALACTIVITYDEFINITON(SPECIALACTIVITYDEFINITON sPECIALACTIVITYDEFINITON)
        {
            if ((sPECIALACTIVITYDEFINITON.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALACTIVITYDEFINITON, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SPECIALACTIVITYDEFINITONS.AddObject(sPECIALACTIVITYDEFINITON);
            }
        }

        public void UpdateSPECIALACTIVITYDEFINITON(SPECIALACTIVITYDEFINITON currentSPECIALACTIVITYDEFINITON)
        {
            this.ObjectContext.SPECIALACTIVITYDEFINITONS.AttachAsModified(currentSPECIALACTIVITYDEFINITON, this.ChangeSet.GetOriginal(currentSPECIALACTIVITYDEFINITON));
        }

        public void DeleteSPECIALACTIVITYDEFINITON(SPECIALACTIVITYDEFINITON sPECIALACTIVITYDEFINITON)
        {
            if ((sPECIALACTIVITYDEFINITON.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALACTIVITYDEFINITON, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SPECIALACTIVITYDEFINITONS.Attach(sPECIALACTIVITYDEFINITON);
                this.ObjectContext.SPECIALACTIVITYDEFINITONS.DeleteObject(sPECIALACTIVITYDEFINITON);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SPECIALTOZFSJS”查询添加顺序。
        public IQueryable<SPECIALTOZFSJ> GetSPECIALTOZFSJS()
        {
            return this.ObjectContext.SPECIALTOZFSJS;
        }

        public void InsertSPECIALTOZFSJ(SPECIALTOZFSJ sPECIALTOZFSJ)
        {
            if ((sPECIALTOZFSJ.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALTOZFSJ, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SPECIALTOZFSJS.AddObject(sPECIALTOZFSJ);
            }
        }

        public void UpdateSPECIALTOZFSJ(SPECIALTOZFSJ currentSPECIALTOZFSJ)
        {
            this.ObjectContext.SPECIALTOZFSJS.AttachAsModified(currentSPECIALTOZFSJ, this.ChangeSet.GetOriginal(currentSPECIALTOZFSJ));
        }

        public void DeleteSPECIALTOZFSJ(SPECIALTOZFSJ sPECIALTOZFSJ)
        {
            if ((sPECIALTOZFSJ.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALTOZFSJ, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SPECIALTOZFSJS.Attach(sPECIALTOZFSJ);
                this.ObjectContext.SPECIALTOZFSJS.DeleteObject(sPECIALTOZFSJ);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“SPECIALWORKFLOWINSTANCES”查询添加顺序。
        public IQueryable<SPECIALWORKFLOWINSTANCE> GetSPECIALWORKFLOWINSTANCES()
        {
            return this.ObjectContext.SPECIALWORKFLOWINSTANCES;
        }

        public void InsertSPECIALWORKFLOWINSTANCE(SPECIALWORKFLOWINSTANCE sPECIALWORKFLOWINSTANCE)
        {
            if ((sPECIALWORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALWORKFLOWINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.SPECIALWORKFLOWINSTANCES.AddObject(sPECIALWORKFLOWINSTANCE);
            }
        }

        public void UpdateSPECIALWORKFLOWINSTANCE(SPECIALWORKFLOWINSTANCE currentSPECIALWORKFLOWINSTANCE)
        {
            this.ObjectContext.SPECIALWORKFLOWINSTANCES.AttachAsModified(currentSPECIALWORKFLOWINSTANCE, this.ChangeSet.GetOriginal(currentSPECIALWORKFLOWINSTANCE));
        }

        public void DeleteSPECIALWORKFLOWINSTANCE(SPECIALWORKFLOWINSTANCE sPECIALWORKFLOWINSTANCE)
        {
            if ((sPECIALWORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sPECIALWORKFLOWINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.SPECIALWORKFLOWINSTANCES.Attach(sPECIALWORKFLOWINSTANCE);
                this.ObjectContext.SPECIALWORKFLOWINSTANCES.DeleteObject(sPECIALWORKFLOWINSTANCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREESTORES”查询添加顺序。
        public IQueryable<STREESTORE> GetSTREESTORES()
        {
            return this.ObjectContext.STREESTORES;
        }

        public void InsertSTREESTORE(STREESTORE sTREESTORE)
        {
            if ((sTREESTORE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREESTORE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREESTORES.AddObject(sTREESTORE);
            }
        }

        public void UpdateSTREESTORE(STREESTORE currentSTREESTORE)
        {
            this.ObjectContext.STREESTORES.AttachAsModified(currentSTREESTORE, this.ChangeSet.GetOriginal(currentSTREESTORE));
        }

        public void DeleteSTREESTORE(STREESTORE sTREESTORE)
        {
            if ((sTREESTORE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREESTORE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREESTORES.Attach(sTREESTORE);
                this.ObjectContext.STREESTORES.DeleteObject(sTREESTORE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREESTORETYPES”查询添加顺序。
        public IQueryable<STREESTORETYPE> GetSTREESTORETYPES()
        {
            return this.ObjectContext.STREESTORETYPES;
        }

        public void InsertSTREESTORETYPE(STREESTORETYPE sTREESTORETYPE)
        {
            if ((sTREESTORETYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREESTORETYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREESTORETYPES.AddObject(sTREESTORETYPE);
            }
        }

        public void UpdateSTREESTORETYPE(STREESTORETYPE currentSTREESTORETYPE)
        {
            this.ObjectContext.STREESTORETYPES.AttachAsModified(currentSTREESTORETYPE, this.ChangeSet.GetOriginal(currentSTREESTORETYPE));
        }

        public void DeleteSTREESTORETYPE(STREESTORETYPE sTREESTORETYPE)
        {
            if ((sTREESTORETYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREESTORETYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREESTORETYPES.Attach(sTREESTORETYPE);
                this.ObjectContext.STREESTORETYPES.DeleteObject(sTREESTORETYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETS”查询添加顺序。
        public IQueryable<STREET> GetSTREETS()
        {
            return this.ObjectContext.STREETS;
        }

        public void InsertSTREET(STREET sTREET)
        {
            if ((sTREET.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREET, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETS.AddObject(sTREET);
            }
        }

        public void UpdateSTREET(STREET currentSTREET)
        {
            this.ObjectContext.STREETS.AttachAsModified(currentSTREET, this.ChangeSet.GetOriginal(currentSTREET));
        }

        public void DeleteSTREET(STREET sTREET)
        {
            if ((sTREET.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREET, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETS.Attach(sTREET);
                this.ObjectContext.STREETS.DeleteObject(sTREET);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETASSESSMENTTYPES”查询添加顺序。
        public IQueryable<STREETASSESSMENTTYPE> GetSTREETASSESSMENTTYPES()
        {
            return this.ObjectContext.STREETASSESSMENTTYPES;
        }

        public void InsertSTREETASSESSMENTTYPE(STREETASSESSMENTTYPE sTREETASSESSMENTTYPE)
        {
            if ((sTREETASSESSMENTTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETASSESSMENTTYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETASSESSMENTTYPES.AddObject(sTREETASSESSMENTTYPE);
            }
        }

        public void UpdateSTREETASSESSMENTTYPE(STREETASSESSMENTTYPE currentSTREETASSESSMENTTYPE)
        {
            this.ObjectContext.STREETASSESSMENTTYPES.AttachAsModified(currentSTREETASSESSMENTTYPE, this.ChangeSet.GetOriginal(currentSTREETASSESSMENTTYPE));
        }

        public void DeleteSTREETASSESSMENTTYPE(STREETASSESSMENTTYPE sTREETASSESSMENTTYPE)
        {
            if ((sTREETASSESSMENTTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETASSESSMENTTYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETASSESSMENTTYPES.Attach(sTREETASSESSMENTTYPE);
                this.ObjectContext.STREETASSESSMENTTYPES.DeleteObject(sTREETASSESSMENTTYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETBASES”查询添加顺序。
        public IQueryable<STREETBAS> GetSTREETBASES()
        {
            return this.ObjectContext.STREETBASES;
        }

        public void InsertSTREETBAS(STREETBAS sTREETBAS)
        {
            if ((sTREETBAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETBAS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETBASES.AddObject(sTREETBAS);
            }
        }

        public void UpdateSTREETBAS(STREETBAS currentSTREETBAS)
        {
            this.ObjectContext.STREETBASES.AttachAsModified(currentSTREETBAS, this.ChangeSet.GetOriginal(currentSTREETBAS));
        }

        public void DeleteSTREETBAS(STREETBAS sTREETBAS)
        {
            if ((sTREETBAS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETBAS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETBASES.Attach(sTREETBAS);
                this.ObjectContext.STREETBASES.DeleteObject(sTREETBAS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETEXAMINES”查询添加顺序。
        public IQueryable<STREETEXAMINE> GetSTREETEXAMINES()
        {
            return this.ObjectContext.STREETEXAMINES;
        }

        public void InsertSTREETEXAMINE(STREETEXAMINE sTREETEXAMINE)
        {
            if ((sTREETEXAMINE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETEXAMINE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETEXAMINES.AddObject(sTREETEXAMINE);
            }
        }

        public void UpdateSTREETEXAMINE(STREETEXAMINE currentSTREETEXAMINE)
        {
            this.ObjectContext.STREETEXAMINES.AttachAsModified(currentSTREETEXAMINE, this.ChangeSet.GetOriginal(currentSTREETEXAMINE));
        }

        public void DeleteSTREETEXAMINE(STREETEXAMINE sTREETEXAMINE)
        {
            if ((sTREETEXAMINE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETEXAMINE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETEXAMINES.Attach(sTREETEXAMINE);
                this.ObjectContext.STREETEXAMINES.DeleteObject(sTREETEXAMINE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETPROBLEMS”查询添加顺序。
        public IQueryable<STREETPROBLEM> GetSTREETPROBLEMS()
        {
            return this.ObjectContext.STREETPROBLEMS;
        }

        public void InsertSTREETPROBLEM(STREETPROBLEM sTREETPROBLEM)
        {
            if ((sTREETPROBLEM.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETPROBLEM, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETPROBLEMS.AddObject(sTREETPROBLEM);
            }
        }

        public void UpdateSTREETPROBLEM(STREETPROBLEM currentSTREETPROBLEM)
        {
            this.ObjectContext.STREETPROBLEMS.AttachAsModified(currentSTREETPROBLEM, this.ChangeSet.GetOriginal(currentSTREETPROBLEM));
        }

        public void DeleteSTREETPROBLEM(STREETPROBLEM sTREETPROBLEM)
        {
            if ((sTREETPROBLEM.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETPROBLEM, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETPROBLEMS.Attach(sTREETPROBLEM);
                this.ObjectContext.STREETPROBLEMS.DeleteObject(sTREETPROBLEM);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETTYPES”查询添加顺序。
        public IQueryable<STREETTYPE> GetSTREETTYPES()
        {
            return this.ObjectContext.STREETTYPES;
        }

        public void InsertSTREETTYPE(STREETTYPE sTREETTYPE)
        {
            if ((sTREETTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETTYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETTYPES.AddObject(sTREETTYPE);
            }
        }

        public void UpdateSTREETTYPE(STREETTYPE currentSTREETTYPE)
        {
            this.ObjectContext.STREETTYPES.AttachAsModified(currentSTREETTYPE, this.ChangeSet.GetOriginal(currentSTREETTYPE));
        }

        public void DeleteSTREETTYPE(STREETTYPE sTREETTYPE)
        {
            if ((sTREETTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETTYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETTYPES.Attach(sTREETTYPE);
                this.ObjectContext.STREETTYPES.DeleteObject(sTREETTYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“STREETUSERS”查询添加顺序。
        public IQueryable<STREETUSER> GetSTREETUSERS()
        {
            return this.ObjectContext.STREETUSERS;
        }

        public void InsertSTREETUSER(STREETUSER sTREETUSER)
        {
            if ((sTREETUSER.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETUSER, EntityState.Added);
            }
            else
            {
                this.ObjectContext.STREETUSERS.AddObject(sTREETUSER);
            }
        }

        public void UpdateSTREETUSER(STREETUSER currentSTREETUSER)
        {
            this.ObjectContext.STREETUSERS.AttachAsModified(currentSTREETUSER, this.ChangeSet.GetOriginal(currentSTREETUSER));
        }

        public void DeleteSTREETUSER(STREETUSER sTREETUSER)
        {
            if ((sTREETUSER.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(sTREETUSER, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.STREETUSERS.Attach(sTREETUSER);
                this.ObjectContext.STREETUSERS.DeleteObject(sTREETUSER);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TABLE1”查询添加顺序。
        public IQueryable<TABLE1> GetTABLE1()
        {
            return this.ObjectContext.TABLE1;
        }

        public void InsertTABLE1(TABLE1 tABLE1)
        {
            if ((tABLE1.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tABLE1, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TABLE1.AddObject(tABLE1);
            }
        }

        public void UpdateTABLE1(TABLE1 currentTABLE1)
        {
            this.ObjectContext.TABLE1.AttachAsModified(currentTABLE1, this.ChangeSet.GetOriginal(currentTABLE1));
        }

        public void DeleteTABLE1(TABLE1 tABLE1)
        {
            if ((tABLE1.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tABLE1, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TABLE1.Attach(tABLE1);
                this.ObjectContext.TABLE1.DeleteObject(tABLE1);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TJGHZFS”查询添加顺序。
        public IQueryable<TJGHZF> GetTJGHZFS()
        {
            return this.ObjectContext.TJGHZFS;
        }

        public void InsertTJGHZF(TJGHZF tJGHZF)
        {
            if ((tJGHZF.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tJGHZF, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TJGHZFS.AddObject(tJGHZF);
            }
        }

        public void UpdateTJGHZF(TJGHZF currentTJGHZF)
        {
            this.ObjectContext.TJGHZFS.AttachAsModified(currentTJGHZF, this.ChangeSet.GetOriginal(currentTJGHZF));
        }

        public void DeleteTJGHZF(TJGHZF tJGHZF)
        {
            if ((tJGHZF.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tJGHZF, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TJGHZFS.Attach(tJGHZF);
                this.ObjectContext.TJGHZFS.DeleteObject(tJGHZF);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TRANSLICENSES”查询添加顺序。
        public IQueryable<TRANSLICENS> GetTRANSLICENSES()
        {
            return this.ObjectContext.TRANSLICENSES;
        }

        public void InsertTRANSLICENS(TRANSLICENS tRANSLICENS)
        {
            if ((tRANSLICENS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TRANSLICENSES.AddObject(tRANSLICENS);
            }
        }

        public void UpdateTRANSLICENS(TRANSLICENS currentTRANSLICENS)
        {
            this.ObjectContext.TRANSLICENSES.AttachAsModified(currentTRANSLICENS, this.ChangeSet.GetOriginal(currentTRANSLICENS));
        }

        public void DeleteTRANSLICENS(TRANSLICENS tRANSLICENS)
        {
            if ((tRANSLICENS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TRANSLICENSES.Attach(tRANSLICENS);
                this.ObjectContext.TRANSLICENSES.DeleteObject(tRANSLICENS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TRANSLICENSECARS”查询添加顺序。
        public IQueryable<TRANSLICENSECAR> GetTRANSLICENSECARS()
        {
            return this.ObjectContext.TRANSLICENSECARS;
        }

        public void InsertTRANSLICENSECAR(TRANSLICENSECAR tRANSLICENSECAR)
        {
            if ((tRANSLICENSECAR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENSECAR, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TRANSLICENSECARS.AddObject(tRANSLICENSECAR);
            }
        }

        public void UpdateTRANSLICENSECAR(TRANSLICENSECAR currentTRANSLICENSECAR)
        {
            this.ObjectContext.TRANSLICENSECARS.AttachAsModified(currentTRANSLICENSECAR, this.ChangeSet.GetOriginal(currentTRANSLICENSECAR));
        }

        public void DeleteTRANSLICENSECAR(TRANSLICENSECAR tRANSLICENSECAR)
        {
            if ((tRANSLICENSECAR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENSECAR, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TRANSLICENSECARS.Attach(tRANSLICENSECAR);
                this.ObjectContext.TRANSLICENSECARS.DeleteObject(tRANSLICENSECAR);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TRANSLICENSECARCOMPANIES”查询添加顺序。
        public IQueryable<TRANSLICENSECARCOMPANy> GetTRANSLICENSECARCOMPANIES()
        {
            return this.ObjectContext.TRANSLICENSECARCOMPANIES;
        }

        public void InsertTRANSLICENSECARCOMPANy(TRANSLICENSECARCOMPANy tRANSLICENSECARCOMPANy)
        {
            if ((tRANSLICENSECARCOMPANy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENSECARCOMPANy, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TRANSLICENSECARCOMPANIES.AddObject(tRANSLICENSECARCOMPANy);
            }
        }

        public void UpdateTRANSLICENSECARCOMPANy(TRANSLICENSECARCOMPANy currentTRANSLICENSECARCOMPANy)
        {
            this.ObjectContext.TRANSLICENSECARCOMPANIES.AttachAsModified(currentTRANSLICENSECARCOMPANy, this.ChangeSet.GetOriginal(currentTRANSLICENSECARCOMPANy));
        }

        public void DeleteTRANSLICENSECARCOMPANy(TRANSLICENSECARCOMPANy tRANSLICENSECARCOMPANy)
        {
            if ((tRANSLICENSECARCOMPANy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENSECARCOMPANy, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TRANSLICENSECARCOMPANIES.Attach(tRANSLICENSECARCOMPANy);
                this.ObjectContext.TRANSLICENSECARCOMPANIES.DeleteObject(tRANSLICENSECARCOMPANy);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TRANSLICENSEROADS”查询添加顺序。
        public IQueryable<TRANSLICENSEROAD> GetTRANSLICENSEROADS()
        {
            return this.ObjectContext.TRANSLICENSEROADS;
        }

        public void InsertTRANSLICENSEROAD(TRANSLICENSEROAD tRANSLICENSEROAD)
        {
            if ((tRANSLICENSEROAD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENSEROAD, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TRANSLICENSEROADS.AddObject(tRANSLICENSEROAD);
            }
        }

        public void UpdateTRANSLICENSEROAD(TRANSLICENSEROAD currentTRANSLICENSEROAD)
        {
            this.ObjectContext.TRANSLICENSEROADS.AttachAsModified(currentTRANSLICENSEROAD, this.ChangeSet.GetOriginal(currentTRANSLICENSEROAD));
        }

        public void DeleteTRANSLICENSEROAD(TRANSLICENSEROAD tRANSLICENSEROAD)
        {
            if ((tRANSLICENSEROAD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSLICENSEROAD, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TRANSLICENSEROADS.Attach(tRANSLICENSEROAD);
                this.ObjectContext.TRANSLICENSEROADS.DeleteObject(tRANSLICENSEROAD);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TRANSRECORDS”查询添加顺序。
        public IQueryable<TRANSRECORD> GetTRANSRECORDS()
        {
            return this.ObjectContext.TRANSRECORDS;
        }

        public void InsertTRANSRECORD(TRANSRECORD tRANSRECORD)
        {
            if ((tRANSRECORD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSRECORD, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TRANSRECORDS.AddObject(tRANSRECORD);
            }
        }

        public void UpdateTRANSRECORD(TRANSRECORD currentTRANSRECORD)
        {
            this.ObjectContext.TRANSRECORDS.AttachAsModified(currentTRANSRECORD, this.ChangeSet.GetOriginal(currentTRANSRECORD));
        }

        public void DeleteTRANSRECORD(TRANSRECORD tRANSRECORD)
        {
            if ((tRANSRECORD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSRECORD, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TRANSRECORDS.Attach(tRANSRECORD);
                this.ObjectContext.TRANSRECORDS.DeleteObject(tRANSRECORD);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“TRANSROADS”查询添加顺序。
        public IQueryable<TRANSROAD> GetTRANSROADS()
        {
            return this.ObjectContext.TRANSROADS;
        }

        public void InsertTRANSROAD(TRANSROAD tRANSROAD)
        {
            if ((tRANSROAD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSROAD, EntityState.Added);
            }
            else
            {
                this.ObjectContext.TRANSROADS.AddObject(tRANSROAD);
            }
        }

        public void UpdateTRANSROAD(TRANSROAD currentTRANSROAD)
        {
            this.ObjectContext.TRANSROADS.AttachAsModified(currentTRANSROAD, this.ChangeSet.GetOriginal(currentTRANSROAD));
        }

        public void DeleteTRANSROAD(TRANSROAD tRANSROAD)
        {
            if ((tRANSROAD.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tRANSROAD, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.TRANSROADS.Attach(tRANSROAD);
                this.ObjectContext.TRANSROADS.DeleteObject(tRANSROAD);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“UNITS”查询添加顺序。
        public IQueryable<UNIT> GetUNITS()
        {
            return this.ObjectContext.UNITS;
        }

        public void InsertUNIT(UNIT uNIT)
        {
            if ((uNIT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uNIT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UNITS.AddObject(uNIT);
            }
        }

        public void UpdateUNIT(UNIT currentUNIT)
        {
            this.ObjectContext.UNITS.AttachAsModified(currentUNIT, this.ChangeSet.GetOriginal(currentUNIT));
        }

        public void DeleteUNIT(UNIT uNIT)
        {
            if ((uNIT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uNIT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.UNITS.Attach(uNIT);
                this.ObjectContext.UNITS.DeleteObject(uNIT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“UNITTYPES”查询添加顺序。
        public IQueryable<UNITTYPE> GetUNITTYPES()
        {
            return this.ObjectContext.UNITTYPES;
        }

        public void InsertUNITTYPE(UNITTYPE uNITTYPE)
        {
            if ((uNITTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uNITTYPE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.UNITTYPES.AddObject(uNITTYPE);
            }
        }

        public void UpdateUNITTYPE(UNITTYPE currentUNITTYPE)
        {
            this.ObjectContext.UNITTYPES.AttachAsModified(currentUNITTYPE, this.ChangeSet.GetOriginal(currentUNITTYPE));
        }

        public void DeleteUNITTYPE(UNITTYPE uNITTYPE)
        {
            if ((uNITTYPE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uNITTYPE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.UNITTYPES.Attach(uNITTYPE);
                this.ObjectContext.UNITTYPES.DeleteObject(uNITTYPE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERS”查询添加顺序。
        public IQueryable<USER> GetUSERS()
        {
            return this.ObjectContext.USERS;
        }

        public void InsertUSER(USER uSER)
        {
            if ((uSER.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSER, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERS.AddObject(uSER);
            }
        }

        public void UpdateUSER(USER currentUSER)
        {
            this.ObjectContext.USERS.AttachAsModified(currentUSER, this.ChangeSet.GetOriginal(currentUSER));
        }

        public void DeleteUSER(USER uSER)
        {
            if ((uSER.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSER, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERS.Attach(uSER);
                this.ObjectContext.USERS.DeleteObject(uSER);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERARCHIVES”查询添加顺序。
        public IQueryable<USERARCHIVE> GetUSERARCHIVES()
        {
            return this.ObjectContext.USERARCHIVES;
        }

        public void InsertUSERARCHIVE(USERARCHIVE uSERARCHIVE)
        {
            if ((uSERARCHIVE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERARCHIVE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERARCHIVES.AddObject(uSERARCHIVE);
            }
        }

        public void UpdateUSERARCHIVE(USERARCHIVE currentUSERARCHIVE)
        {
            this.ObjectContext.USERARCHIVES.AttachAsModified(currentUSERARCHIVE, this.ChangeSet.GetOriginal(currentUSERARCHIVE));
        }

        public void DeleteUSERARCHIVE(USERARCHIVE uSERARCHIVE)
        {
            if ((uSERARCHIVE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERARCHIVE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERARCHIVES.Attach(uSERARCHIVE);
                this.ObjectContext.USERARCHIVES.DeleteObject(uSERARCHIVE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERCATEGORIES”查询添加顺序。
        public IQueryable<USERCATEGORy> GetUSERCATEGORIES()
        {
            return this.ObjectContext.USERCATEGORIES;
        }

        public void InsertUSERCATEGORy(USERCATEGORy uSERCATEGORy)
        {
            if ((uSERCATEGORy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERCATEGORy, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERCATEGORIES.AddObject(uSERCATEGORy);
            }
        }

        public void UpdateUSERCATEGORy(USERCATEGORy currentUSERCATEGORy)
        {
            this.ObjectContext.USERCATEGORIES.AttachAsModified(currentUSERCATEGORy, this.ChangeSet.GetOriginal(currentUSERCATEGORy));
        }

        public void DeleteUSERCATEGORy(USERCATEGORy uSERCATEGORy)
        {
            if ((uSERCATEGORy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERCATEGORy, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERCATEGORIES.Attach(uSERCATEGORy);
                this.ObjectContext.USERCATEGORIES.DeleteObject(uSERCATEGORy);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERGROUPs”查询添加顺序。
        public IQueryable<USERGROUP> GetUSERGROUPs()
        {
            return this.ObjectContext.USERGROUPs;
        }

        public void InsertUSERGROUP(USERGROUP uSERGROUP)
        {
            if ((uSERGROUP.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERGROUP, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERGROUPs.AddObject(uSERGROUP);
            }
        }

        public void UpdateUSERGROUP(USERGROUP currentUSERGROUP)
        {
            this.ObjectContext.USERGROUPs.AttachAsModified(currentUSERGROUP, this.ChangeSet.GetOriginal(currentUSERGROUP));
        }

        public void DeleteUSERGROUP(USERGROUP uSERGROUP)
        {
            if ((uSERGROUP.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERGROUP, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERGROUPs.Attach(uSERGROUP);
                this.ObjectContext.USERGROUPs.DeleteObject(uSERGROUP);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERONDUTIES”查询添加顺序。
        public IQueryable<USERONDUTy> GetUSERONDUTIES()
        {
            return this.ObjectContext.USERONDUTIES;
        }

        public void InsertUSERONDUTy(USERONDUTy uSERONDUTy)
        {
            if ((uSERONDUTy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERONDUTy, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERONDUTIES.AddObject(uSERONDUTy);
            }
        }

        public void UpdateUSERONDUTy(USERONDUTy currentUSERONDUTy)
        {
            this.ObjectContext.USERONDUTIES.AttachAsModified(currentUSERONDUTy, this.ChangeSet.GetOriginal(currentUSERONDUTy));
        }

        public void DeleteUSERONDUTy(USERONDUTy uSERONDUTy)
        {
            if ((uSERONDUTy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERONDUTy, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERONDUTIES.Attach(uSERONDUTy);
                this.ObjectContext.USERONDUTIES.DeleteObject(uSERONDUTy);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERPHONESIGNINS”查询添加顺序。
        public IQueryable<USERPHONESIGNIN> GetUSERPHONESIGNINS()
        {
            return this.ObjectContext.USERPHONESIGNINS;
        }

        public void InsertUSERPHONESIGNIN(USERPHONESIGNIN uSERPHONESIGNIN)
        {
            if ((uSERPHONESIGNIN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERPHONESIGNIN, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERPHONESIGNINS.AddObject(uSERPHONESIGNIN);
            }
        }

        public void UpdateUSERPHONESIGNIN(USERPHONESIGNIN currentUSERPHONESIGNIN)
        {
            this.ObjectContext.USERPHONESIGNINS.AttachAsModified(currentUSERPHONESIGNIN, this.ChangeSet.GetOriginal(currentUSERPHONESIGNIN));
        }

        public void DeleteUSERPHONESIGNIN(USERPHONESIGNIN uSERPHONESIGNIN)
        {
            if ((uSERPHONESIGNIN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERPHONESIGNIN, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERPHONESIGNINS.Attach(uSERPHONESIGNIN);
                this.ObjectContext.USERPHONESIGNINS.DeleteObject(uSERPHONESIGNIN);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERPOSITIONS”查询添加顺序。
        public IQueryable<USERPOSITION> GetUSERPOSITIONS()
        {
            return this.ObjectContext.USERPOSITIONS;
        }

        public void InsertUSERPOSITION(USERPOSITION uSERPOSITION)
        {
            if ((uSERPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERPOSITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERPOSITIONS.AddObject(uSERPOSITION);
            }
        }

        public void UpdateUSERPOSITION(USERPOSITION currentUSERPOSITION)
        {
            this.ObjectContext.USERPOSITIONS.AttachAsModified(currentUSERPOSITION, this.ChangeSet.GetOriginal(currentUSERPOSITION));
        }

        public void DeleteUSERPOSITION(USERPOSITION uSERPOSITION)
        {
            if ((uSERPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERPOSITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERPOSITIONS.Attach(uSERPOSITION);
                this.ObjectContext.USERPOSITIONS.DeleteObject(uSERPOSITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERROLES”查询添加顺序。
        public IQueryable<USERROLE> GetUSERROLES()
        {
            return this.ObjectContext.USERROLES;
        }

        public void InsertUSERROLE(USERROLE uSERROLE)
        {
            if ((uSERROLE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERROLE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERROLES.AddObject(uSERROLE);
            }
        }

        public void UpdateUSERROLE(USERROLE currentUSERROLE)
        {
            this.ObjectContext.USERROLES.AttachAsModified(currentUSERROLE, this.ChangeSet.GetOriginal(currentUSERROLE));
        }

        public void DeleteUSERROLE(USERROLE uSERROLE)
        {
            if ((uSERROLE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERROLE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERROLES.Attach(uSERROLE);
                this.ObjectContext.USERROLES.DeleteObject(uSERROLE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“USERS_DEL”查询添加顺序。
        public IQueryable<USERS_DEL> GetUSERS_DEL()
        {
            return this.ObjectContext.USERS_DEL;
        }

        public void InsertUSERS_DEL(USERS_DEL uSERS_DEL)
        {
            if ((uSERS_DEL.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERS_DEL, EntityState.Added);
            }
            else
            {
                this.ObjectContext.USERS_DEL.AddObject(uSERS_DEL);
            }
        }

        public void UpdateUSERS_DEL(USERS_DEL currentUSERS_DEL)
        {
            this.ObjectContext.USERS_DEL.AttachAsModified(currentUSERS_DEL, this.ChangeSet.GetOriginal(currentUSERS_DEL));
        }

        public void DeleteUSERS_DEL(USERS_DEL uSERS_DEL)
        {
            if ((uSERS_DEL.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(uSERS_DEL, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.USERS_DEL.Attach(uSERS_DEL);
                this.ObjectContext.USERS_DEL.DeleteObject(uSERS_DEL);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“WORKFLOWDEFINITIONS”查询添加顺序。
        public IQueryable<WORKFLOWDEFINITION> GetWORKFLOWDEFINITIONS()
        {
            return this.ObjectContext.WORKFLOWDEFINITIONS;
        }

        public void InsertWORKFLOWDEFINITION(WORKFLOWDEFINITION wORKFLOWDEFINITION)
        {
            if ((wORKFLOWDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWDEFINITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.WORKFLOWDEFINITIONS.AddObject(wORKFLOWDEFINITION);
            }
        }

        public void UpdateWORKFLOWDEFINITION(WORKFLOWDEFINITION currentWORKFLOWDEFINITION)
        {
            this.ObjectContext.WORKFLOWDEFINITIONS.AttachAsModified(currentWORKFLOWDEFINITION, this.ChangeSet.GetOriginal(currentWORKFLOWDEFINITION));
        }

        public void DeleteWORKFLOWDEFINITION(WORKFLOWDEFINITION wORKFLOWDEFINITION)
        {
            if ((wORKFLOWDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWDEFINITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.WORKFLOWDEFINITIONS.Attach(wORKFLOWDEFINITION);
                this.ObjectContext.WORKFLOWDEFINITIONS.DeleteObject(wORKFLOWDEFINITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“WORKFLOWINSTANCES”查询添加顺序。
        public IQueryable<WORKFLOWINSTANCE> GetWORKFLOWINSTANCES()
        {
            return this.ObjectContext.WORKFLOWINSTANCES;
        }

        public void InsertWORKFLOWINSTANCE(WORKFLOWINSTANCE wORKFLOWINSTANCE)
        {
            if ((wORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.WORKFLOWINSTANCES.AddObject(wORKFLOWINSTANCE);
            }
        }

        public void UpdateWORKFLOWINSTANCE(WORKFLOWINSTANCE currentWORKFLOWINSTANCE)
        {
            this.ObjectContext.WORKFLOWINSTANCES.AttachAsModified(currentWORKFLOWINSTANCE, this.ChangeSet.GetOriginal(currentWORKFLOWINSTANCE));
        }

        public void DeleteWORKFLOWINSTANCE(WORKFLOWINSTANCE wORKFLOWINSTANCE)
        {
            if ((wORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.WORKFLOWINSTANCES.Attach(wORKFLOWINSTANCE);
                this.ObjectContext.WORKFLOWINSTANCES.DeleteObject(wORKFLOWINSTANCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“WORKFLOWPEROPERTIES”查询添加顺序。
        public IQueryable<WORKFLOWPEROPERTy> GetWORKFLOWPEROPERTIES()
        {
            return this.ObjectContext.WORKFLOWPEROPERTIES;
        }

        public void InsertWORKFLOWPEROPERTy(WORKFLOWPEROPERTy wORKFLOWPEROPERTy)
        {
            if ((wORKFLOWPEROPERTy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWPEROPERTy, EntityState.Added);
            }
            else
            {
                this.ObjectContext.WORKFLOWPEROPERTIES.AddObject(wORKFLOWPEROPERTy);
            }
        }

        public void UpdateWORKFLOWPEROPERTy(WORKFLOWPEROPERTy currentWORKFLOWPEROPERTy)
        {
            this.ObjectContext.WORKFLOWPEROPERTIES.AttachAsModified(currentWORKFLOWPEROPERTy, this.ChangeSet.GetOriginal(currentWORKFLOWPEROPERTy));
        }

        public void DeleteWORKFLOWPEROPERTy(WORKFLOWPEROPERTy wORKFLOWPEROPERTy)
        {
            if ((wORKFLOWPEROPERTy.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWPEROPERTy, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.WORKFLOWPEROPERTIES.Attach(wORKFLOWPEROPERTy);
                this.ObjectContext.WORKFLOWPEROPERTIES.DeleteObject(wORKFLOWPEROPERTy);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“WORKFLOWSTATUSES”查询添加顺序。
        public IQueryable<WORKFLOWSTATUS> GetWORKFLOWSTATUSES()
        {
            return this.ObjectContext.WORKFLOWSTATUSES;
        }

        public void InsertWORKFLOWSTATUS(WORKFLOWSTATUS wORKFLOWSTATUS)
        {
            if ((wORKFLOWSTATUS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWSTATUS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.WORKFLOWSTATUSES.AddObject(wORKFLOWSTATUS);
            }
        }

        public void UpdateWORKFLOWSTATUS(WORKFLOWSTATUS currentWORKFLOWSTATUS)
        {
            this.ObjectContext.WORKFLOWSTATUSES.AttachAsModified(currentWORKFLOWSTATUS, this.ChangeSet.GetOriginal(currentWORKFLOWSTATUS));
        }

        public void DeleteWORKFLOWSTATUS(WORKFLOWSTATUS wORKFLOWSTATUS)
        {
            if ((wORKFLOWSTATUS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wORKFLOWSTATUS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.WORKFLOWSTATUSES.Attach(wORKFLOWSTATUS);
                this.ObjectContext.WORKFLOWSTATUSES.DeleteObject(wORKFLOWSTATUS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“WTUSERRELATIONS”查询添加顺序。
        public IQueryable<WTUSERRELATION> GetWTUSERRELATIONS()
        {
            return this.ObjectContext.WTUSERRELATIONS;
        }

        public void InsertWTUSERRELATION(WTUSERRELATION wTUSERRELATION)
        {
            if ((wTUSERRELATION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wTUSERRELATION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.WTUSERRELATIONS.AddObject(wTUSERRELATION);
            }
        }

        public void UpdateWTUSERRELATION(WTUSERRELATION currentWTUSERRELATION)
        {
            this.ObjectContext.WTUSERRELATIONS.AttachAsModified(currentWTUSERRELATION, this.ChangeSet.GetOriginal(currentWTUSERRELATION));
        }

        public void DeleteWTUSERRELATION(WTUSERRELATION wTUSERRELATION)
        {
            if ((wTUSERRELATION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(wTUSERRELATION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.WTUSERRELATIONS.Attach(wTUSERRELATION);
                this.ObjectContext.WTUSERRELATIONS.DeleteObject(wTUSERRELATION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XCJGAREAS”查询添加顺序。
        public IQueryable<XCJGAREA> GetXCJGAREAS()
        {
            return this.ObjectContext.XCJGAREAS;
        }

        public void InsertXCJGAREA(XCJGAREA xCJGAREA)
        {
            if ((xCJGAREA.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGAREA, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XCJGAREAS.AddObject(xCJGAREA);
            }
        }

        public void UpdateXCJGAREA(XCJGAREA currentXCJGAREA)
        {
            this.ObjectContext.XCJGAREAS.AttachAsModified(currentXCJGAREA, this.ChangeSet.GetOriginal(currentXCJGAREA));
        }

        public void DeleteXCJGAREA(XCJGAREA xCJGAREA)
        {
            if ((xCJGAREA.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGAREA, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XCJGAREAS.Attach(xCJGAREA);
                this.ObjectContext.XCJGAREAS.DeleteObject(xCJGAREA);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XCJGCARTASKS”查询添加顺序。
        public IQueryable<XCJGCARTASK> GetXCJGCARTASKS()
        {
            return this.ObjectContext.XCJGCARTASKS;
        }

        public void InsertXCJGCARTASK(XCJGCARTASK xCJGCARTASK)
        {
            if ((xCJGCARTASK.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGCARTASK, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XCJGCARTASKS.AddObject(xCJGCARTASK);
            }
        }

        public void UpdateXCJGCARTASK(XCJGCARTASK currentXCJGCARTASK)
        {
            this.ObjectContext.XCJGCARTASKS.AttachAsModified(currentXCJGCARTASK, this.ChangeSet.GetOriginal(currentXCJGCARTASK));
        }

        public void DeleteXCJGCARTASK(XCJGCARTASK xCJGCARTASK)
        {
            if ((xCJGCARTASK.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGCARTASK, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XCJGCARTASKS.Attach(xCJGCARTASK);
                this.ObjectContext.XCJGCARTASKS.DeleteObject(xCJGCARTASK);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XCJGROUTES”查询添加顺序。
        public IQueryable<XCJGROUTE> GetXCJGROUTES()
        {
            return this.ObjectContext.XCJGROUTES;
        }

        public void InsertXCJGROUTE(XCJGROUTE xCJGROUTE)
        {
            if ((xCJGROUTE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGROUTE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XCJGROUTES.AddObject(xCJGROUTE);
            }
        }

        public void UpdateXCJGROUTE(XCJGROUTE currentXCJGROUTE)
        {
            this.ObjectContext.XCJGROUTES.AttachAsModified(currentXCJGROUTE, this.ChangeSet.GetOriginal(currentXCJGROUTE));
        }

        public void DeleteXCJGROUTE(XCJGROUTE xCJGROUTE)
        {
            if ((xCJGROUTE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGROUTE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XCJGROUTES.Attach(xCJGROUTE);
                this.ObjectContext.XCJGROUTES.DeleteObject(xCJGROUTE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XCJGSIGNINS”查询添加顺序。
        public IQueryable<XCJGSIGNIN> GetXCJGSIGNINS()
        {
            return this.ObjectContext.XCJGSIGNINS;
        }

        public void InsertXCJGSIGNIN(XCJGSIGNIN xCJGSIGNIN)
        {
            if ((xCJGSIGNIN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGSIGNIN, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XCJGSIGNINS.AddObject(xCJGSIGNIN);
            }
        }

        public void UpdateXCJGSIGNIN(XCJGSIGNIN currentXCJGSIGNIN)
        {
            this.ObjectContext.XCJGSIGNINS.AttachAsModified(currentXCJGSIGNIN, this.ChangeSet.GetOriginal(currentXCJGSIGNIN));
        }

        public void DeleteXCJGSIGNIN(XCJGSIGNIN xCJGSIGNIN)
        {
            if ((xCJGSIGNIN.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGSIGNIN, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XCJGSIGNINS.Attach(xCJGSIGNIN);
                this.ObjectContext.XCJGSIGNINS.DeleteObject(xCJGSIGNIN);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XCJGUSERTASKS”查询添加顺序。
        public IQueryable<XCJGUSERTASK> GetXCJGUSERTASKS()
        {
            return this.ObjectContext.XCJGUSERTASKS;
        }

        public void InsertXCJGUSERTASK(XCJGUSERTASK xCJGUSERTASK)
        {
            if ((xCJGUSERTASK.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGUSERTASK, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XCJGUSERTASKS.AddObject(xCJGUSERTASK);
            }
        }

        public void UpdateXCJGUSERTASK(XCJGUSERTASK currentXCJGUSERTASK)
        {
            this.ObjectContext.XCJGUSERTASKS.AttachAsModified(currentXCJGUSERTASK, this.ChangeSet.GetOriginal(currentXCJGUSERTASK));
        }

        public void DeleteXCJGUSERTASK(XCJGUSERTASK xCJGUSERTASK)
        {
            if ((xCJGUSERTASK.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xCJGUSERTASK, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XCJGUSERTASKS.Attach(xCJGUSERTASK);
                this.ObjectContext.XCJGUSERTASKS.DeleteObject(xCJGUSERTASK);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPACTDEFS”查询添加顺序。
        public IQueryable<XZSPACTDEF> GetXZSPACTDEFS()
        {
            return this.ObjectContext.XZSPACTDEFS;
        }

        public void InsertXZSPACTDEF(XZSPACTDEF xZSPACTDEF)
        {
            if ((xZSPACTDEF.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTDEF, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPACTDEFS.AddObject(xZSPACTDEF);
            }
        }

        public void UpdateXZSPACTDEF(XZSPACTDEF currentXZSPACTDEF)
        {
            this.ObjectContext.XZSPACTDEFS.AttachAsModified(currentXZSPACTDEF, this.ChangeSet.GetOriginal(currentXZSPACTDEF));
        }

        public void DeleteXZSPACTDEF(XZSPACTDEF xZSPACTDEF)
        {
            if ((xZSPACTDEF.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTDEF, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPACTDEFS.Attach(xZSPACTDEF);
                this.ObjectContext.XZSPACTDEFS.DeleteObject(xZSPACTDEF);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPACTISTS”查询添加顺序。
        public IQueryable<XZSPACTIST> GetXZSPACTISTS()
        {
            return this.ObjectContext.XZSPACTISTS;
        }

        public void InsertXZSPACTIST(XZSPACTIST xZSPACTIST)
        {
            if ((xZSPACTIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTIST, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPACTISTS.AddObject(xZSPACTIST);
            }
        }

        public void UpdateXZSPACTIST(XZSPACTIST currentXZSPACTIST)
        {
            this.ObjectContext.XZSPACTISTS.AttachAsModified(currentXZSPACTIST, this.ChangeSet.GetOriginal(currentXZSPACTIST));
        }

        public void DeleteXZSPACTIST(XZSPACTIST xZSPACTIST)
        {
            if ((xZSPACTIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTIST, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPACTISTS.Attach(xZSPACTIST);
                this.ObjectContext.XZSPACTISTS.DeleteObject(xZSPACTIST);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPACTISTS_DELETE”查询添加顺序。
        public IQueryable<XZSPACTISTS_DELETE> GetXZSPACTISTS_DELETE()
        {
            return this.ObjectContext.XZSPACTISTS_DELETE;
        }

        public void InsertXZSPACTISTS_DELETE(XZSPACTISTS_DELETE xZSPACTISTS_DELETE)
        {
            if ((xZSPACTISTS_DELETE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTISTS_DELETE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPACTISTS_DELETE.AddObject(xZSPACTISTS_DELETE);
            }
        }

        public void UpdateXZSPACTISTS_DELETE(XZSPACTISTS_DELETE currentXZSPACTISTS_DELETE)
        {
            this.ObjectContext.XZSPACTISTS_DELETE.AttachAsModified(currentXZSPACTISTS_DELETE, this.ChangeSet.GetOriginal(currentXZSPACTISTS_DELETE));
        }

        public void DeleteXZSPACTISTS_DELETE(XZSPACTISTS_DELETE xZSPACTISTS_DELETE)
        {
            if ((xZSPACTISTS_DELETE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTISTS_DELETE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPACTISTS_DELETE.Attach(xZSPACTISTS_DELETE);
                this.ObjectContext.XZSPACTISTS_DELETE.DeleteObject(xZSPACTISTS_DELETE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPACTISTS_DELETE1”查询添加顺序。
        public IQueryable<XZSPACTISTS_DELETE1> GetXZSPACTISTS_DELETE1()
        {
            return this.ObjectContext.XZSPACTISTS_DELETE1;
        }

        public void InsertXZSPACTISTS_DELETE1(XZSPACTISTS_DELETE1 xZSPACTISTS_DELETE1)
        {
            if ((xZSPACTISTS_DELETE1.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTISTS_DELETE1, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPACTISTS_DELETE1.AddObject(xZSPACTISTS_DELETE1);
            }
        }

        public void UpdateXZSPACTISTS_DELETE1(XZSPACTISTS_DELETE1 currentXZSPACTISTS_DELETE1)
        {
            this.ObjectContext.XZSPACTISTS_DELETE1.AttachAsModified(currentXZSPACTISTS_DELETE1, this.ChangeSet.GetOriginal(currentXZSPACTISTS_DELETE1));
        }

        public void DeleteXZSPACTISTS_DELETE1(XZSPACTISTS_DELETE1 xZSPACTISTS_DELETE1)
        {
            if ((xZSPACTISTS_DELETE1.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTISTS_DELETE1, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPACTISTS_DELETE1.Attach(xZSPACTISTS_DELETE1);
                this.ObjectContext.XZSPACTISTS_DELETE1.DeleteObject(xZSPACTISTS_DELETE1);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPACTIVITYDEFINITIONS”查询添加顺序。
        public IQueryable<XZSPACTIVITYDEFINITION> GetXZSPACTIVITYDEFINITIONS()
        {
            return this.ObjectContext.XZSPACTIVITYDEFINITIONS;
        }

        public void InsertXZSPACTIVITYDEFINITION(XZSPACTIVITYDEFINITION xZSPACTIVITYDEFINITION)
        {
            if ((xZSPACTIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTIVITYDEFINITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPACTIVITYDEFINITIONS.AddObject(xZSPACTIVITYDEFINITION);
            }
        }

        public void UpdateXZSPACTIVITYDEFINITION(XZSPACTIVITYDEFINITION currentXZSPACTIVITYDEFINITION)
        {
            this.ObjectContext.XZSPACTIVITYDEFINITIONS.AttachAsModified(currentXZSPACTIVITYDEFINITION, this.ChangeSet.GetOriginal(currentXZSPACTIVITYDEFINITION));
        }

        public void DeleteXZSPACTIVITYDEFINITION(XZSPACTIVITYDEFINITION xZSPACTIVITYDEFINITION)
        {
            if ((xZSPACTIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPACTIVITYDEFINITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPACTIVITYDEFINITIONS.Attach(xZSPACTIVITYDEFINITION);
                this.ObjectContext.XZSPACTIVITYDEFINITIONS.DeleteObject(xZSPACTIVITYDEFINITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPAPPROVALPROJECTS”查询添加顺序。
        public IQueryable<XZSPAPPROVALPROJECT> GetXZSPAPPROVALPROJECTS()
        {
            return this.ObjectContext.XZSPAPPROVALPROJECTS;
        }

        public void InsertXZSPAPPROVALPROJECT(XZSPAPPROVALPROJECT xZSPAPPROVALPROJECT)
        {
            if ((xZSPAPPROVALPROJECT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPAPPROVALPROJECT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPAPPROVALPROJECTS.AddObject(xZSPAPPROVALPROJECT);
            }
        }

        public void UpdateXZSPAPPROVALPROJECT(XZSPAPPROVALPROJECT currentXZSPAPPROVALPROJECT)
        {
            this.ObjectContext.XZSPAPPROVALPROJECTS.AttachAsModified(currentXZSPAPPROVALPROJECT, this.ChangeSet.GetOriginal(currentXZSPAPPROVALPROJECT));
        }

        public void DeleteXZSPAPPROVALPROJECT(XZSPAPPROVALPROJECT xZSPAPPROVALPROJECT)
        {
            if ((xZSPAPPROVALPROJECT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPAPPROVALPROJECT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPAPPROVALPROJECTS.Attach(xZSPAPPROVALPROJECT);
                this.ObjectContext.XZSPAPPROVALPROJECTS.DeleteObject(xZSPAPPROVALPROJECT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPKZHCs”查询添加顺序。
        public IQueryable<XZSPKZHC> GetXZSPKZHCs()
        {
            return this.ObjectContext.XZSPKZHCs;
        }

        public void InsertXZSPKZHC(XZSPKZHC xZSPKZHC)
        {
            if ((xZSPKZHC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPKZHC, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPKZHCs.AddObject(xZSPKZHC);
            }
        }

        public void UpdateXZSPKZHC(XZSPKZHC currentXZSPKZHC)
        {
            this.ObjectContext.XZSPKZHCs.AttachAsModified(currentXZSPKZHC, this.ChangeSet.GetOriginal(currentXZSPKZHC));
        }

        public void DeleteXZSPKZHC(XZSPKZHC xZSPKZHC)
        {
            if ((xZSPKZHC.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPKZHC, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPKZHCs.Attach(xZSPKZHC);
                this.ObjectContext.XZSPKZHCs.DeleteObject(xZSPKZHC);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPNEWACTIVITYDEFINITIONS”查询添加顺序。
        public IQueryable<XZSPNEWACTIVITYDEFINITION> GetXZSPNEWACTIVITYDEFINITIONS()
        {
            return this.ObjectContext.XZSPNEWACTIVITYDEFINITIONS;
        }

        public void InsertXZSPNEWACTIVITYDEFINITION(XZSPNEWACTIVITYDEFINITION xZSPNEWACTIVITYDEFINITION)
        {
            if ((xZSPNEWACTIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPNEWACTIVITYDEFINITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPNEWACTIVITYDEFINITIONS.AddObject(xZSPNEWACTIVITYDEFINITION);
            }
        }

        public void UpdateXZSPNEWACTIVITYDEFINITION(XZSPNEWACTIVITYDEFINITION currentXZSPNEWACTIVITYDEFINITION)
        {
            this.ObjectContext.XZSPNEWACTIVITYDEFINITIONS.AttachAsModified(currentXZSPNEWACTIVITYDEFINITION, this.ChangeSet.GetOriginal(currentXZSPNEWACTIVITYDEFINITION));
        }

        public void DeleteXZSPNEWACTIVITYDEFINITION(XZSPNEWACTIVITYDEFINITION xZSPNEWACTIVITYDEFINITION)
        {
            if ((xZSPNEWACTIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPNEWACTIVITYDEFINITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPNEWACTIVITYDEFINITIONS.Attach(xZSPNEWACTIVITYDEFINITION);
                this.ObjectContext.XZSPNEWACTIVITYDEFINITIONS.DeleteObject(xZSPNEWACTIVITYDEFINITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPNEWTABs”查询添加顺序。
        public IQueryable<XZSPNEWTAB> GetXZSPNEWTABs()
        {
            return this.ObjectContext.XZSPNEWTABs;
        }

        public void InsertXZSPNEWTAB(XZSPNEWTAB xZSPNEWTAB)
        {
            if ((xZSPNEWTAB.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPNEWTAB, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPNEWTABs.AddObject(xZSPNEWTAB);
            }
        }

        public void UpdateXZSPNEWTAB(XZSPNEWTAB currentXZSPNEWTAB)
        {
            this.ObjectContext.XZSPNEWTABs.AttachAsModified(currentXZSPNEWTAB, this.ChangeSet.GetOriginal(currentXZSPNEWTAB));
        }

        public void DeleteXZSPNEWTAB(XZSPNEWTAB xZSPNEWTAB)
        {
            if ((xZSPNEWTAB.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPNEWTAB, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPNEWTABs.Attach(xZSPNEWTAB);
                this.ObjectContext.XZSPNEWTABs.DeleteObject(xZSPNEWTAB);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPNEWWORKFLOWINSTANCES”查询添加顺序。
        public IQueryable<XZSPNEWWORKFLOWINSTANCE> GetXZSPNEWWORKFLOWINSTANCES()
        {
            return this.ObjectContext.XZSPNEWWORKFLOWINSTANCES;
        }

        public void InsertXZSPNEWWORKFLOWINSTANCE(XZSPNEWWORKFLOWINSTANCE xZSPNEWWORKFLOWINSTANCE)
        {
            if ((xZSPNEWWORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPNEWWORKFLOWINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPNEWWORKFLOWINSTANCES.AddObject(xZSPNEWWORKFLOWINSTANCE);
            }
        }

        public void UpdateXZSPNEWWORKFLOWINSTANCE(XZSPNEWWORKFLOWINSTANCE currentXZSPNEWWORKFLOWINSTANCE)
        {
            this.ObjectContext.XZSPNEWWORKFLOWINSTANCES.AttachAsModified(currentXZSPNEWWORKFLOWINSTANCE, this.ChangeSet.GetOriginal(currentXZSPNEWWORKFLOWINSTANCE));
        }

        public void DeleteXZSPNEWWORKFLOWINSTANCE(XZSPNEWWORKFLOWINSTANCE xZSPNEWWORKFLOWINSTANCE)
        {
            if ((xZSPNEWWORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPNEWWORKFLOWINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPNEWWORKFLOWINSTANCES.Attach(xZSPNEWWORKFLOWINSTANCE);
                this.ObjectContext.XZSPNEWWORKFLOWINSTANCES.DeleteObject(xZSPNEWWORKFLOWINSTANCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPPROJECTNAMEs”查询添加顺序。
        public IQueryable<XZSPPROJECTNAME> GetXZSPPROJECTNAMEs()
        {
            return this.ObjectContext.XZSPPROJECTNAMEs;
        }

        public void InsertXZSPPROJECTNAME(XZSPPROJECTNAME xZSPPROJECTNAME)
        {
            if ((xZSPPROJECTNAME.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPPROJECTNAME, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPPROJECTNAMEs.AddObject(xZSPPROJECTNAME);
            }
        }

        public void UpdateXZSPPROJECTNAME(XZSPPROJECTNAME currentXZSPPROJECTNAME)
        {
            this.ObjectContext.XZSPPROJECTNAMEs.AttachAsModified(currentXZSPPROJECTNAME, this.ChangeSet.GetOriginal(currentXZSPPROJECTNAME));
        }

        public void DeleteXZSPPROJECTNAME(XZSPPROJECTNAME xZSPPROJECTNAME)
        {
            if ((xZSPPROJECTNAME.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPPROJECTNAME, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPPROJECTNAMEs.Attach(xZSPPROJECTNAME);
                this.ObjectContext.XZSPPROJECTNAMEs.DeleteObject(xZSPPROJECTNAME);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPSTUS”查询添加顺序。
        public IQueryable<XZSPSTU> GetXZSPSTUS()
        {
            return this.ObjectContext.XZSPSTUS;
        }

        public void InsertXZSPSTU(XZSPSTU xZSPSTU)
        {
            if ((xZSPSTU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPSTU, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPSTUS.AddObject(xZSPSTU);
            }
        }

        public void UpdateXZSPSTU(XZSPSTU currentXZSPSTU)
        {
            this.ObjectContext.XZSPSTUS.AttachAsModified(currentXZSPSTU, this.ChangeSet.GetOriginal(currentXZSPSTU));
        }

        public void DeleteXZSPSTU(XZSPSTU xZSPSTU)
        {
            if ((xZSPSTU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPSTU, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPSTUS.Attach(xZSPSTU);
                this.ObjectContext.XZSPSTUS.DeleteObject(xZSPSTU);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPWFDEFS”查询添加顺序。
        public IQueryable<XZSPWFDEF> GetXZSPWFDEFS()
        {
            return this.ObjectContext.XZSPWFDEFS;
        }

        public void InsertXZSPWFDEF(XZSPWFDEF xZSPWFDEF)
        {
            if ((xZSPWFDEF.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPWFDEF, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPWFDEFS.AddObject(xZSPWFDEF);
            }
        }

        public void UpdateXZSPWFDEF(XZSPWFDEF currentXZSPWFDEF)
        {
            this.ObjectContext.XZSPWFDEFS.AttachAsModified(currentXZSPWFDEF, this.ChangeSet.GetOriginal(currentXZSPWFDEF));
        }

        public void DeleteXZSPWFDEF(XZSPWFDEF xZSPWFDEF)
        {
            if ((xZSPWFDEF.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPWFDEF, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPWFDEFS.Attach(xZSPWFDEF);
                this.ObjectContext.XZSPWFDEFS.DeleteObject(xZSPWFDEF);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPWFISTS”查询添加顺序。
        public IQueryable<XZSPWFIST> GetXZSPWFISTS()
        {
            return this.ObjectContext.XZSPWFISTS;
        }

        public void InsertXZSPWFIST(XZSPWFIST xZSPWFIST)
        {
            if ((xZSPWFIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPWFIST, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPWFISTS.AddObject(xZSPWFIST);
            }
        }

        public void UpdateXZSPWFIST(XZSPWFIST currentXZSPWFIST)
        {
            this.ObjectContext.XZSPWFISTS.AttachAsModified(currentXZSPWFIST, this.ChangeSet.GetOriginal(currentXZSPWFIST));
        }

        public void DeleteXZSPWFIST(XZSPWFIST xZSPWFIST)
        {
            if ((xZSPWFIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPWFIST, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPWFISTS.Attach(xZSPWFIST);
                this.ObjectContext.XZSPWFISTS.DeleteObject(xZSPWFIST);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZSPWFISTS_DELETE”查询添加顺序。
        public IQueryable<XZSPWFISTS_DELETE> GetXZSPWFISTS_DELETE()
        {
            return this.ObjectContext.XZSPWFISTS_DELETE;
        }

        public void InsertXZSPWFISTS_DELETE(XZSPWFISTS_DELETE xZSPWFISTS_DELETE)
        {
            if ((xZSPWFISTS_DELETE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPWFISTS_DELETE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZSPWFISTS_DELETE.AddObject(xZSPWFISTS_DELETE);
            }
        }

        public void UpdateXZSPWFISTS_DELETE(XZSPWFISTS_DELETE currentXZSPWFISTS_DELETE)
        {
            this.ObjectContext.XZSPWFISTS_DELETE.AttachAsModified(currentXZSPWFISTS_DELETE, this.ChangeSet.GetOriginal(currentXZSPWFISTS_DELETE));
        }

        public void DeleteXZSPWFISTS_DELETE(XZSPWFISTS_DELETE xZSPWFISTS_DELETE)
        {
            if ((xZSPWFISTS_DELETE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZSPWFISTS_DELETE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZSPWFISTS_DELETE.Attach(xZSPWFISTS_DELETE);
                this.ObjectContext.XZSPWFISTS_DELETE.DeleteObject(xZSPWFISTS_DELETE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZZFLISTSHS”查询添加顺序。
        public IQueryable<XZZFLISTSH> GetXZZFLISTSHS()
        {
            return this.ObjectContext.XZZFLISTSHS;
        }

        public void InsertXZZFLISTSH(XZZFLISTSH xZZFLISTSH)
        {
            if ((xZZFLISTSH.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZZFLISTSH, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZZFLISTSHS.AddObject(xZZFLISTSH);
            }
        }

        public void UpdateXZZFLISTSH(XZZFLISTSH currentXZZFLISTSH)
        {
            this.ObjectContext.XZZFLISTSHS.AttachAsModified(currentXZZFLISTSH, this.ChangeSet.GetOriginal(currentXZZFLISTSH));
        }

        public void DeleteXZZFLISTSH(XZZFLISTSH xZZFLISTSH)
        {
            if ((xZZFLISTSH.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZZFLISTSH, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZZFLISTSHS.Attach(xZZFLISTSH);
                this.ObjectContext.XZZFLISTSHS.DeleteObject(xZZFLISTSH);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZZFQUESTIONCLASSES”查询添加顺序。
        public IQueryable<XZZFQUESTIONCLASS> GetXZZFQUESTIONCLASSES()
        {
            return this.ObjectContext.XZZFQUESTIONCLASSES;
        }

        public void InsertXZZFQUESTIONCLASS(XZZFQUESTIONCLASS xZZFQUESTIONCLASS)
        {
            if ((xZZFQUESTIONCLASS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZZFQUESTIONCLASS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZZFQUESTIONCLASSES.AddObject(xZZFQUESTIONCLASS);
            }
        }

        public void UpdateXZZFQUESTIONCLASS(XZZFQUESTIONCLASS currentXZZFQUESTIONCLASS)
        {
            this.ObjectContext.XZZFQUESTIONCLASSES.AttachAsModified(currentXZZFQUESTIONCLASS, this.ChangeSet.GetOriginal(currentXZZFQUESTIONCLASS));
        }

        public void DeleteXZZFQUESTIONCLASS(XZZFQUESTIONCLASS xZZFQUESTIONCLASS)
        {
            if ((xZZFQUESTIONCLASS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZZFQUESTIONCLASS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZZFQUESTIONCLASSES.Attach(xZZFQUESTIONCLASS);
                this.ObjectContext.XZZFQUESTIONCLASSES.DeleteObject(xZZFQUESTIONCLASS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“XZZFTABLISTS”查询添加顺序。
        public IQueryable<XZZFTABLIST> GetXZZFTABLISTS()
        {
            return this.ObjectContext.XZZFTABLISTS;
        }

        public void InsertXZZFTABLIST(XZZFTABLIST xZZFTABLIST)
        {
            if ((xZZFTABLIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZZFTABLIST, EntityState.Added);
            }
            else
            {
                this.ObjectContext.XZZFTABLISTS.AddObject(xZZFTABLIST);
            }
        }

        public void UpdateXZZFTABLIST(XZZFTABLIST currentXZZFTABLIST)
        {
            this.ObjectContext.XZZFTABLISTS.AttachAsModified(currentXZZFTABLIST, this.ChangeSet.GetOriginal(currentXZZFTABLIST));
        }

        public void DeleteXZZFTABLIST(XZZFTABLIST xZZFTABLIST)
        {
            if ((xZZFTABLIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(xZZFTABLIST, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.XZZFTABLISTS.Attach(xZZFTABLIST);
                this.ObjectContext.XZZFTABLISTS.DeleteObject(xZZFTABLIST);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZBRZLISTS”查询添加顺序。
        public IQueryable<ZBRZLIST> GetZBRZLISTS()
        {
            return this.ObjectContext.ZBRZLISTS;
        }

        public void InsertZBRZLIST(ZBRZLIST zBRZLIST)
        {
            if ((zBRZLIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zBRZLIST, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZBRZLISTS.AddObject(zBRZLIST);
            }
        }

        public void UpdateZBRZLIST(ZBRZLIST currentZBRZLIST)
        {
            this.ObjectContext.ZBRZLISTS.AttachAsModified(currentZBRZLIST, this.ChangeSet.GetOriginal(currentZBRZLIST));
        }

        public void DeleteZBRZLIST(ZBRZLIST zBRZLIST)
        {
            if ((zBRZLIST.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zBRZLIST, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZBRZLISTS.Attach(zBRZLIST);
                this.ObjectContext.ZBRZLISTS.DeleteObject(zBRZLIST);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFGKCARS”查询添加顺序。
        public IQueryable<ZFGKCAR> GetZFGKCARS()
        {
            return this.ObjectContext.ZFGKCARS;
        }

        public void InsertZFGKCAR(ZFGKCAR zFGKCAR)
        {
            if ((zFGKCAR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKCAR, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFGKCARS.AddObject(zFGKCAR);
            }
        }

        public void UpdateZFGKCAR(ZFGKCAR currentZFGKCAR)
        {
            this.ObjectContext.ZFGKCARS.AttachAsModified(currentZFGKCAR, this.ChangeSet.GetOriginal(currentZFGKCAR));
        }

        public void DeleteZFGKCAR(ZFGKCAR zFGKCAR)
        {
            if ((zFGKCAR.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKCAR, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFGKCARS.Attach(zFGKCAR);
                this.ObjectContext.ZFGKCARS.DeleteObject(zFGKCAR);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFGKCARHISTORYPOSITIONS”查询添加顺序。
        public IQueryable<ZFGKCARHISTORYPOSITION> GetZFGKCARHISTORYPOSITIONS()
        {
            return this.ObjectContext.ZFGKCARHISTORYPOSITIONS;
        }

        public void InsertZFGKCARHISTORYPOSITION(ZFGKCARHISTORYPOSITION zFGKCARHISTORYPOSITION)
        {
            if ((zFGKCARHISTORYPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKCARHISTORYPOSITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFGKCARHISTORYPOSITIONS.AddObject(zFGKCARHISTORYPOSITION);
            }
        }

        public void UpdateZFGKCARHISTORYPOSITION(ZFGKCARHISTORYPOSITION currentZFGKCARHISTORYPOSITION)
        {
            this.ObjectContext.ZFGKCARHISTORYPOSITIONS.AttachAsModified(currentZFGKCARHISTORYPOSITION, this.ChangeSet.GetOriginal(currentZFGKCARHISTORYPOSITION));
        }

        public void DeleteZFGKCARHISTORYPOSITION(ZFGKCARHISTORYPOSITION zFGKCARHISTORYPOSITION)
        {
            if ((zFGKCARHISTORYPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKCARHISTORYPOSITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFGKCARHISTORYPOSITIONS.Attach(zFGKCARHISTORYPOSITION);
                this.ObjectContext.ZFGKCARHISTORYPOSITIONS.DeleteObject(zFGKCARHISTORYPOSITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFGKCARLATESTPOSITIONS”查询添加顺序。
        public IQueryable<ZFGKCARLATESTPOSITION> GetZFGKCARLATESTPOSITIONS()
        {
            return this.ObjectContext.ZFGKCARLATESTPOSITIONS;
        }

        public void InsertZFGKCARLATESTPOSITION(ZFGKCARLATESTPOSITION zFGKCARLATESTPOSITION)
        {
            if ((zFGKCARLATESTPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKCARLATESTPOSITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFGKCARLATESTPOSITIONS.AddObject(zFGKCARLATESTPOSITION);
            }
        }

        public void UpdateZFGKCARLATESTPOSITION(ZFGKCARLATESTPOSITION currentZFGKCARLATESTPOSITION)
        {
            this.ObjectContext.ZFGKCARLATESTPOSITIONS.AttachAsModified(currentZFGKCARLATESTPOSITION, this.ChangeSet.GetOriginal(currentZFGKCARLATESTPOSITION));
        }

        public void DeleteZFGKCARLATESTPOSITION(ZFGKCARLATESTPOSITION zFGKCARLATESTPOSITION)
        {
            if ((zFGKCARLATESTPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKCARLATESTPOSITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFGKCARLATESTPOSITIONS.Attach(zFGKCARLATESTPOSITION);
                this.ObjectContext.ZFGKCARLATESTPOSITIONS.DeleteObject(zFGKCARLATESTPOSITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFGKUSERHISTORYPOSITIONS”查询添加顺序。
        public IQueryable<ZFGKUSERHISTORYPOSITION> GetZFGKUSERHISTORYPOSITIONS()
        {
            return this.ObjectContext.ZFGKUSERHISTORYPOSITIONS;
        }

        public void InsertZFGKUSERHISTORYPOSITION(ZFGKUSERHISTORYPOSITION zFGKUSERHISTORYPOSITION)
        {
            if ((zFGKUSERHISTORYPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKUSERHISTORYPOSITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFGKUSERHISTORYPOSITIONS.AddObject(zFGKUSERHISTORYPOSITION);
            }
        }

        public void UpdateZFGKUSERHISTORYPOSITION(ZFGKUSERHISTORYPOSITION currentZFGKUSERHISTORYPOSITION)
        {
            this.ObjectContext.ZFGKUSERHISTORYPOSITIONS.AttachAsModified(currentZFGKUSERHISTORYPOSITION, this.ChangeSet.GetOriginal(currentZFGKUSERHISTORYPOSITION));
        }

        public void DeleteZFGKUSERHISTORYPOSITION(ZFGKUSERHISTORYPOSITION zFGKUSERHISTORYPOSITION)
        {
            if ((zFGKUSERHISTORYPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKUSERHISTORYPOSITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFGKUSERHISTORYPOSITIONS.Attach(zFGKUSERHISTORYPOSITION);
                this.ObjectContext.ZFGKUSERHISTORYPOSITIONS.DeleteObject(zFGKUSERHISTORYPOSITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFGKUSERLATESTPOSITIONS”查询添加顺序。
        public IQueryable<ZFGKUSERLATESTPOSITION> GetZFGKUSERLATESTPOSITIONS()
        {
            return this.ObjectContext.ZFGKUSERLATESTPOSITIONS;
        }

        public void InsertZFGKUSERLATESTPOSITION(ZFGKUSERLATESTPOSITION zFGKUSERLATESTPOSITION)
        {
            if ((zFGKUSERLATESTPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKUSERLATESTPOSITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFGKUSERLATESTPOSITIONS.AddObject(zFGKUSERLATESTPOSITION);
            }
        }

        public void UpdateZFGKUSERLATESTPOSITION(ZFGKUSERLATESTPOSITION currentZFGKUSERLATESTPOSITION)
        {
            this.ObjectContext.ZFGKUSERLATESTPOSITIONS.AttachAsModified(currentZFGKUSERLATESTPOSITION, this.ChangeSet.GetOriginal(currentZFGKUSERLATESTPOSITION));
        }

        public void DeleteZFGKUSERLATESTPOSITION(ZFGKUSERLATESTPOSITION zFGKUSERLATESTPOSITION)
        {
            if ((zFGKUSERLATESTPOSITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFGKUSERLATESTPOSITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFGKUSERLATESTPOSITIONS.Attach(zFGKUSERLATESTPOSITION);
                this.ObjectContext.ZFGKUSERLATESTPOSITIONS.DeleteObject(zFGKUSERLATESTPOSITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJACTIVITYDEFINITIONs”查询添加顺序。
        public IQueryable<ZFSJACTIVITYDEFINITION> GetZFSJACTIVITYDEFINITIONs()
        {
            return this.ObjectContext.ZFSJACTIVITYDEFINITIONs;
        }

        public void InsertZFSJACTIVITYDEFINITION(ZFSJACTIVITYDEFINITION zFSJACTIVITYDEFINITION)
        {
            if ((zFSJACTIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJACTIVITYDEFINITION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJACTIVITYDEFINITIONs.AddObject(zFSJACTIVITYDEFINITION);
            }
        }

        public void UpdateZFSJACTIVITYDEFINITION(ZFSJACTIVITYDEFINITION currentZFSJACTIVITYDEFINITION)
        {
            this.ObjectContext.ZFSJACTIVITYDEFINITIONs.AttachAsModified(currentZFSJACTIVITYDEFINITION, this.ChangeSet.GetOriginal(currentZFSJACTIVITYDEFINITION));
        }

        public void DeleteZFSJACTIVITYDEFINITION(ZFSJACTIVITYDEFINITION zFSJACTIVITYDEFINITION)
        {
            if ((zFSJACTIVITYDEFINITION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJACTIVITYDEFINITION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJACTIVITYDEFINITIONs.Attach(zFSJACTIVITYDEFINITION);
                this.ObjectContext.ZFSJACTIVITYDEFINITIONs.DeleteObject(zFSJACTIVITYDEFINITION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJACTIVITYINSTANCES”查询添加顺序。
        public IQueryable<ZFSJACTIVITYINSTANCE> GetZFSJACTIVITYINSTANCES()
        {
            return this.ObjectContext.ZFSJACTIVITYINSTANCES;
        }

        public void InsertZFSJACTIVITYINSTANCE(ZFSJACTIVITYINSTANCE zFSJACTIVITYINSTANCE)
        {
            if ((zFSJACTIVITYINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJACTIVITYINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJACTIVITYINSTANCES.AddObject(zFSJACTIVITYINSTANCE);
            }
        }

        public void UpdateZFSJACTIVITYINSTANCE(ZFSJACTIVITYINSTANCE currentZFSJACTIVITYINSTANCE)
        {
            this.ObjectContext.ZFSJACTIVITYINSTANCES.AttachAsModified(currentZFSJACTIVITYINSTANCE, this.ChangeSet.GetOriginal(currentZFSJACTIVITYINSTANCE));
        }

        public void DeleteZFSJACTIVITYINSTANCE(ZFSJACTIVITYINSTANCE zFSJACTIVITYINSTANCE)
        {
            if ((zFSJACTIVITYINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJACTIVITYINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJACTIVITYINSTANCES.Attach(zFSJACTIVITYINSTANCE);
                this.ObjectContext.ZFSJACTIVITYINSTANCES.DeleteObject(zFSJACTIVITYINSTANCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJCHARTBYQLs”查询添加顺序。
        public IQueryable<ZFSJCHARTBYQL> GetZFSJCHARTBYQLs()
        {
            return this.ObjectContext.ZFSJCHARTBYQLs;
        }

        public void InsertZFSJCHARTBYQL(ZFSJCHARTBYQL zFSJCHARTBYQL)
        {
            if ((zFSJCHARTBYQL.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJCHARTBYQL, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJCHARTBYQLs.AddObject(zFSJCHARTBYQL);
            }
        }

        public void UpdateZFSJCHARTBYQL(ZFSJCHARTBYQL currentZFSJCHARTBYQL)
        {
            this.ObjectContext.ZFSJCHARTBYQLs.AttachAsModified(currentZFSJCHARTBYQL, this.ChangeSet.GetOriginal(currentZFSJCHARTBYQL));
        }

        public void DeleteZFSJCHARTBYQL(ZFSJCHARTBYQL zFSJCHARTBYQL)
        {
            if ((zFSJCHARTBYQL.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJCHARTBYQL, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJCHARTBYQLs.Attach(zFSJCHARTBYQL);
                this.ObjectContext.ZFSJCHARTBYQLs.DeleteObject(zFSJCHARTBYQL);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJCHECKWAYs”查询添加顺序。
        public IQueryable<ZFSJCHECKWAY> GetZFSJCHECKWAYs()
        {
            return this.ObjectContext.ZFSJCHECKWAYs;
        }

        public void InsertZFSJCHECKWAY(ZFSJCHECKWAY zFSJCHECKWAY)
        {
            if ((zFSJCHECKWAY.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJCHECKWAY, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJCHECKWAYs.AddObject(zFSJCHECKWAY);
            }
        }

        public void UpdateZFSJCHECKWAY(ZFSJCHECKWAY currentZFSJCHECKWAY)
        {
            this.ObjectContext.ZFSJCHECKWAYs.AttachAsModified(currentZFSJCHECKWAY, this.ChangeSet.GetOriginal(currentZFSJCHECKWAY));
        }

        public void DeleteZFSJCHECKWAY(ZFSJCHECKWAY zFSJCHECKWAY)
        {
            if ((zFSJCHECKWAY.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJCHECKWAY, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJCHECKWAYs.Attach(zFSJCHECKWAY);
                this.ObjectContext.ZFSJCHECKWAYs.DeleteObject(zFSJCHECKWAY);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJPROCESSWAYs”查询添加顺序。
        public IQueryable<ZFSJPROCESSWAY> GetZFSJPROCESSWAYs()
        {
            return this.ObjectContext.ZFSJPROCESSWAYs;
        }

        public void InsertZFSJPROCESSWAY(ZFSJPROCESSWAY zFSJPROCESSWAY)
        {
            if ((zFSJPROCESSWAY.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJPROCESSWAY, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJPROCESSWAYs.AddObject(zFSJPROCESSWAY);
            }
        }

        public void UpdateZFSJPROCESSWAY(ZFSJPROCESSWAY currentZFSJPROCESSWAY)
        {
            this.ObjectContext.ZFSJPROCESSWAYs.AttachAsModified(currentZFSJPROCESSWAY, this.ChangeSet.GetOriginal(currentZFSJPROCESSWAY));
        }

        public void DeleteZFSJPROCESSWAY(ZFSJPROCESSWAY zFSJPROCESSWAY)
        {
            if ((zFSJPROCESSWAY.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJPROCESSWAY, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJPROCESSWAYs.Attach(zFSJPROCESSWAY);
                this.ObjectContext.ZFSJPROCESSWAYs.DeleteObject(zFSJPROCESSWAY);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJQUESTIONCLASSES”查询添加顺序。
        public IQueryable<ZFSJQUESTIONCLASS> GetZFSJQUESTIONCLASSES()
        {
            return this.ObjectContext.ZFSJQUESTIONCLASSES;
        }

        public void InsertZFSJQUESTIONCLASS(ZFSJQUESTIONCLASS zFSJQUESTIONCLASS)
        {
            if ((zFSJQUESTIONCLASS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJQUESTIONCLASS, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJQUESTIONCLASSES.AddObject(zFSJQUESTIONCLASS);
            }
        }

        public void UpdateZFSJQUESTIONCLASS(ZFSJQUESTIONCLASS currentZFSJQUESTIONCLASS)
        {
            this.ObjectContext.ZFSJQUESTIONCLASSES.AttachAsModified(currentZFSJQUESTIONCLASS, this.ChangeSet.GetOriginal(currentZFSJQUESTIONCLASS));
        }

        public void DeleteZFSJQUESTIONCLASS(ZFSJQUESTIONCLASS zFSJQUESTIONCLASS)
        {
            if ((zFSJQUESTIONCLASS.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJQUESTIONCLASS, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJQUESTIONCLASSES.Attach(zFSJQUESTIONCLASS);
                this.ObjectContext.ZFSJQUESTIONCLASSES.DeleteObject(zFSJQUESTIONCLASS);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJSOURCES”查询添加顺序。
        public IQueryable<ZFSJSOURCE> GetZFSJSOURCES()
        {
            return this.ObjectContext.ZFSJSOURCES;
        }

        public void InsertZFSJSOURCE(ZFSJSOURCE zFSJSOURCE)
        {
            if ((zFSJSOURCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJSOURCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJSOURCES.AddObject(zFSJSOURCE);
            }
        }

        public void UpdateZFSJSOURCE(ZFSJSOURCE currentZFSJSOURCE)
        {
            this.ObjectContext.ZFSJSOURCES.AttachAsModified(currentZFSJSOURCE, this.ChangeSet.GetOriginal(currentZFSJSOURCE));
        }

        public void DeleteZFSJSOURCE(ZFSJSOURCE zFSJSOURCE)
        {
            if ((zFSJSOURCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJSOURCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJSOURCES.Attach(zFSJSOURCE);
                this.ObjectContext.ZFSJSOURCES.DeleteObject(zFSJSOURCE);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJSTATUS”查询添加顺序。
        public IQueryable<ZFSJSTATU> GetZFSJSTATUS()
        {
            return this.ObjectContext.ZFSJSTATUS;
        }

        public void InsertZFSJSTATU(ZFSJSTATU zFSJSTATU)
        {
            if ((zFSJSTATU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJSTATU, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJSTATUS.AddObject(zFSJSTATU);
            }
        }

        public void UpdateZFSJSTATU(ZFSJSTATU currentZFSJSTATU)
        {
            this.ObjectContext.ZFSJSTATUS.AttachAsModified(currentZFSJSTATU, this.ChangeSet.GetOriginal(currentZFSJSTATU));
        }

        public void DeleteZFSJSTATU(ZFSJSTATU zFSJSTATU)
        {
            if ((zFSJSTATU.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJSTATU, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJSTATUS.Attach(zFSJSTATU);
                this.ObjectContext.ZFSJSTATUS.DeleteObject(zFSJSTATU);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJSUMMARYINFORMATIONS”查询添加顺序。
        public IQueryable<ZFSJSUMMARYINFORMATION> GetZFSJSUMMARYINFORMATIONS()
        {
            return this.ObjectContext.ZFSJSUMMARYINFORMATIONS;
        }

        public void InsertZFSJSUMMARYINFORMATION(ZFSJSUMMARYINFORMATION zFSJSUMMARYINFORMATION)
        {
            if ((zFSJSUMMARYINFORMATION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJSUMMARYINFORMATION, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJSUMMARYINFORMATIONS.AddObject(zFSJSUMMARYINFORMATION);
            }
        }

        public void UpdateZFSJSUMMARYINFORMATION(ZFSJSUMMARYINFORMATION currentZFSJSUMMARYINFORMATION)
        {
            this.ObjectContext.ZFSJSUMMARYINFORMATIONS.AttachAsModified(currentZFSJSUMMARYINFORMATION, this.ChangeSet.GetOriginal(currentZFSJSUMMARYINFORMATION));
        }

        public void DeleteZFSJSUMMARYINFORMATION(ZFSJSUMMARYINFORMATION zFSJSUMMARYINFORMATION)
        {
            if ((zFSJSUMMARYINFORMATION.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJSUMMARYINFORMATION, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJSUMMARYINFORMATIONS.Attach(zFSJSUMMARYINFORMATION);
                this.ObjectContext.ZFSJSUMMARYINFORMATIONS.DeleteObject(zFSJSUMMARYINFORMATION);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJTIMELIMITS”查询添加顺序。
        public IQueryable<ZFSJTIMELIMIT> GetZFSJTIMELIMITS()
        {
            return this.ObjectContext.ZFSJTIMELIMITS;
        }

        public void InsertZFSJTIMELIMIT(ZFSJTIMELIMIT zFSJTIMELIMIT)
        {
            if ((zFSJTIMELIMIT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJTIMELIMIT, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJTIMELIMITS.AddObject(zFSJTIMELIMIT);
            }
        }

        public void UpdateZFSJTIMELIMIT(ZFSJTIMELIMIT currentZFSJTIMELIMIT)
        {
            this.ObjectContext.ZFSJTIMELIMITS.AttachAsModified(currentZFSJTIMELIMIT, this.ChangeSet.GetOriginal(currentZFSJTIMELIMIT));
        }

        public void DeleteZFSJTIMELIMIT(ZFSJTIMELIMIT zFSJTIMELIMIT)
        {
            if ((zFSJTIMELIMIT.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJTIMELIMIT, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJTIMELIMITS.Attach(zFSJTIMELIMIT);
                this.ObjectContext.ZFSJTIMELIMITS.DeleteObject(zFSJTIMELIMIT);
            }
        }

        // TODO:
        // 考虑约束查询方法的结果。如果需要其他输入，
        //可向此方法添加参数或创建具有不同名称的其他查询方法。
        // 为支持分页，需要向“ZFSJWORKFLOWINSTANCES”查询添加顺序。
        public IQueryable<ZFSJWORKFLOWINSTANCE> GetZFSJWORKFLOWINSTANCES()
        {
            return this.ObjectContext.ZFSJWORKFLOWINSTANCES;
        }

        public void InsertZFSJWORKFLOWINSTANCE(ZFSJWORKFLOWINSTANCE zFSJWORKFLOWINSTANCE)
        {
            if ((zFSJWORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJWORKFLOWINSTANCE, EntityState.Added);
            }
            else
            {
                this.ObjectContext.ZFSJWORKFLOWINSTANCES.AddObject(zFSJWORKFLOWINSTANCE);
            }
        }

        public void UpdateZFSJWORKFLOWINSTANCE(ZFSJWORKFLOWINSTANCE currentZFSJWORKFLOWINSTANCE)
        {
            this.ObjectContext.ZFSJWORKFLOWINSTANCES.AttachAsModified(currentZFSJWORKFLOWINSTANCE, this.ChangeSet.GetOriginal(currentZFSJWORKFLOWINSTANCE));
        }

        public void DeleteZFSJWORKFLOWINSTANCE(ZFSJWORKFLOWINSTANCE zFSJWORKFLOWINSTANCE)
        {
            if ((zFSJWORKFLOWINSTANCE.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(zFSJWORKFLOWINSTANCE, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.ZFSJWORKFLOWINSTANCES.Attach(zFSJWORKFLOWINSTANCE);
                this.ObjectContext.ZFSJWORKFLOWINSTANCES.DeleteObject(zFSJWORKFLOWINSTANCE);
            }
        }
    }
}


