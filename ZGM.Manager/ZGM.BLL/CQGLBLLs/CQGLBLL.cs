using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.ViewModels;

namespace ZGM.BLL.CQGLBLLs
{
    public class CQGLBLL
    {
        /// <summary>
        /// 获取一个新的项目标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewProjectID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_PROJECTID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的丈量摸底标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewCQHouseID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_HOUSEID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的过渡标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewCQTransitionID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_TRANSITIONID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的抽签安置标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewCQDrawID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_DRAWID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的企业拆迁标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewEnterpriseID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_EPID.NEXTVAL FROM DUAL";

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个新的企业支付标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetNewEPMoneyID()
        {
            Entities db = new Entities();

            string sql = "SELECT SEQ_EPMONEYID.NEXTVAL FROM DUAL"; 

            return db.Database.SqlQuery<decimal>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 查询项目列表
        /// </summary>
        /// <returns></returns>
        public static List<CQGL_PROJECTS> GetSearchData(string ProjectName, string ProjectUser, string StartTime, string EndTime, string EStartTime, string EEndTime)
        {
            Entities db = new Entities();
            IQueryable<CQGL_PROJECTS> list = db.CQGL_PROJECTS.Where(t => t.STATE < 3).OrderByDescending(t => t.CREATETIME);

            if (!string.IsNullOrEmpty(ProjectName))
                list = list.Where(t => t.PROJECTNAME.Contains(ProjectName));
            if (!string.IsNullOrEmpty(ProjectUser))
                list = list.Where(t => t.PROJECTUSER.Contains(ProjectUser));
            if (!string.IsNullOrEmpty(StartTime))
            {
                DateTime STime = DateTime.Parse(StartTime);
                list = list.Where(t => t.STARTTIME >= STime);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime ETime = DateTime.Parse(EndTime);
                list = list.Where(t => t.STARTTIME <= ETime);
            }
            if (!string.IsNullOrEmpty(EStartTime))
            {
                DateTime ESTime = DateTime.Parse(EStartTime);
                list = list.Where(t => t.ENDTIME >= ESTime);
            }
            if (!string.IsNullOrEmpty(EEndTime))
            {
                DateTime EETime = DateTime.Parse(EEndTime);
                list = list.Where(t => t.ENDTIME <= EETime);
            }

            return list.ToList();
        }

        /// <summary>
        /// 查询住宅拆迁列表
        /// </summary>
        /// <returns></returns>
        public static IQueryable<VMCQGL> GetSearchHouseData(string ProjectName, string ProjectUser,string HouseHolder,string STime, string ETime, decimal HouseStatus)
        {
            Entities db = new Entities();

            IQueryable<VMCQGL> list = (from h in db.CQGL_HOUSES
                                       join p in db.CQGL_PROJECTS
                                       on h.PROJECTID equals p.PROJECTID into temp
                                       from i in temp.DefaultIfEmpty()
                                       join s in db.CQGL_SIGNS
                                       on h.HOUSEID equals s.HOUSEID into temp2
                                       from i2 in temp2.DefaultIfEmpty()
                                       join cc in db.CQGL_CHECKOUT
                                       on h.HOUSEID equals cc.HOUSEID into icc
                                       join cd in db.CQGL_DRAWS
                                       on h.HOUSEID equals cd.HOUSEID into icd
                                       join ct in db.CQGL_TRANSITIONS
                                       on h.HOUSEID equals ct.HOUSEID into ict
                                       join ch in db.CQGL_HOUSES
                                       on h.HOUSEID equals ch.HOUSEID into ich
                                       orderby h.CREATETIME descending
                                       select new VMCQGL
                                       {
                                           HouseId = h.HOUSEID,
                                           PROJECTNAME = i.PROJECTNAME,
                                           PROJECTUSER = i.PROJECTUSER,
                                           HouseHolder = h.HOUSEHOLDER,
                                           STARTTIME = i.STARTTIME,
                                           ENDTIME = i.ENDTIME,
                                           SignTime = i2.SIGNTIME,            
                                           StatusId = icc.Count()>0?5:icd.Count()>0?4:ict.Count()>0?3:i2.SIGNID !=null?2:ich.Count()>0?1:0,
                                          FILEPATH = (i2.SIGNID!=null ? "none" : i.FILEPATH),
                                       });

            if (!string.IsNullOrEmpty(ProjectName))
                list = list.Where(t => t.PROJECTNAME.Contains(ProjectName));
            if (!string.IsNullOrEmpty(ProjectUser))
                list = list.Where(t => t.PROJECTUSER.Contains(ProjectUser));
            if (!string.IsNullOrEmpty(HouseHolder))
                list = list.Where(t => t.HouseHolder.Contains(HouseHolder));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime StartTime = DateTime.Parse(STime);
                list = list.Where(t => t.StatusId > 1 && (t.SignTime >= StartTime));
            }
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime EndTime = DateTime.Parse(ETime);
                list = list.Where(t => t.StatusId > 1 && (t.SignTime <= EndTime));
            }
            if (HouseStatus != 0)
                list = list.Where(t => t.StatusId == HouseStatus);

            return list;
        }

        /// <summary>
        /// 查询企业拆迁列表
        /// </summary>
        /// <returns></returns>
        public static List<VMCQGLEP> GetSearchEnterpriseData(string ProjectName, string ProjectUser, string LegalName, string STime,string ETime)
        {
            Entities db = new Entities();
            IQueryable<VMCQGLEP> list = (from e in db.CQGL_ENTERPRISES
                                         join p in db.CQGL_PROJECTS
                                         on e.PROJECTID equals p.PROJECTID
                                         orderby e.CREATETIME descending
                                         select new VMCQGLEP
                                         {
                                             EnterpriseId = e.ENTERPRISEID,
                                             PROJECTNAME = p.PROJECTNAME,
                                             PROJECTUSER = p.PROJECTUSER,
                                             LegalName = e.LEGALNAME,
                                             SignTime = e.SIGNTIME,
                                             STARTTIME = p.STARTTIME,
                                             ENDTIME = p.ENDTIME
                                         });

            if (!string.IsNullOrEmpty(ProjectName))
                list = list.Where(t => t.PROJECTNAME.Contains(ProjectName));
            if (!string.IsNullOrEmpty(ProjectUser))
                list = list.Where(t => t.PROJECTUSER.Contains(ProjectUser));
            if (!string.IsNullOrEmpty(LegalName))
                list = list.Where(t => t.LegalName.Contains(LegalName));
            if (!string.IsNullOrEmpty(STime))
            {
                DateTime startTime = DateTime.Parse(STime).Date;
                list = list.Where(t => t.SignTime >= startTime);
            }
                
            if (!string.IsNullOrEmpty(ETime))
            {
                DateTime endTime = DateTime.Parse(ETime).Date.AddDays(1);
                list = list.Where(t => t.SignTime < endTime);
            }            

            return list.ToList();
        }

        /// <summary>
        /// 添加项目
        /// </summary>
        /// <returns></returns>
        public static int AddProject(CQGL_PROJECTS model)
        {
            Entities db = new Entities();
            db.CQGL_PROJECTS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 检查该拆迁住宅在哪个阶段
        /// </summary>
        /// <returns></returns>
        public static decimal CheckStage(decimal HouseId)
        {
            Entities db = new Entities();
            decimal result = 0;

            List<CQGL_HOUSES> hlist = db.CQGL_HOUSES.Where(t => t.HOUSEID == HouseId).ToList();
            if (hlist.Count > 0)
                result = 1;

            List<CQGL_SIGNS> slist = db.CQGL_SIGNS.Where(t => t.HOUSEID == HouseId).ToList();
            if (slist.Count > 0)
                result = 2;

            List<CQGL_TRANSITIONS> tlist = db.CQGL_TRANSITIONS.Where(t => t.HOUSEID == HouseId).ToList();
            if (tlist.Count > 0)
                result = 3;

            List<CQGL_DRAWS> dlist = db.CQGL_DRAWS.Where(t => t.HOUSEID == HouseId).ToList();
            if (dlist.Count > 0)
                result = 4;

            List<CQGL_CHECKOUT> clist = db.CQGL_CHECKOUT.Where(t => t.HOUSEID == HouseId).ToList();
            if (clist.Count > 0)
                result = 5;

            return result;
        }

        /// <summary>
        /// 添加丈量摸底
        /// </summary>
        /// <returns></returns>
        public static int AddHouse(CQGL_HOUSES model)
        {
            Entities db = new Entities();
            db.CQGL_HOUSES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加签协阶段
        /// </summary>
        /// <returns></returns>
        public static int AddSign(CQGL_SIGNS model)
        {
            Entities db = new Entities();
            db.CQGL_SIGNS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加过渡阶段
        /// </summary>
        /// <returns></returns>
        public static int AddTransition(CQGL_TRANSITIONS model)
        {
            Entities db = new Entities();
            db.CQGL_TRANSITIONS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加抽签安置阶段
        /// </summary>
        /// <returns></returns>
        public static int AddDraw(CQGL_DRAWS model)
        {
            Entities db = new Entities();
            db.CQGL_DRAWS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加安置房
        /// </summary>
        /// <returns></returns>
        public static int AddDrawHouse(CQGL_DRAWHOUSE model)
        {
            Entities db = new Entities();
            db.CQGL_DRAWHOUSE.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加结账阶段
        /// </summary>
        /// <returns></returns>
        public static int AddCheckOut(CQGL_CHECKOUT model)
        {
            Entities db = new Entities();
            db.CQGL_CHECKOUT.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加企业支付
        /// </summary>
        /// <returns></returns>
        public static int AddEnterpriseMoney(CQGL_ENTERPRISEMONEYS model)
        {
            Entities db = new Entities();
            db.CQGL_ENTERPRISEMONEYS.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 添加企业拆迁
        /// </summary>
        /// <returns></returns>
        public static int AddEnterprise(CQGL_ENTERPRISES model)
        {
            Entities db = new Entities();
            db.CQGL_ENTERPRISES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改丈量摸底
        /// </summary>
        /// <returns></returns>
        public static int EditHouse(decimal id,CQGL_HOUSES model)
        {
            Entities db = new Entities();
            CQGL_HOUSES mmodel = db.CQGL_HOUSES.FirstOrDefault(t => t.HOUSEID == id);
            mmodel.HOUSEHOLDER = model.HOUSEHOLDER;
            mmodel.HOLDERPHONE = model.HOLDERPHONE;
            mmodel.WARRANTAREA = model.WARRANTAREA;
            mmodel.MEASUREMENTAREA = model.MEASUREMENTAREA;
            mmodel.WITHOUTAREA = model.WITHOUTAREA;

            return db.SaveChanges();
        }

        /// <summary>
        /// 修改签协阶段
        /// </summary>
        /// <returns></returns>
        public static int EditSign(decimal id, CQGL_SIGNS model)
        {
            Entities db = new Entities();
            CQGL_SIGNS mmodel = db.CQGL_SIGNS.FirstOrDefault(t => t.HOUSEID == id);
            mmodel.SIGNTIME = model.SIGNTIME;
            mmodel.HOUSEAREA = model.HOUSEAREA;
            mmodel.WAREHOUSEAREA = model.WAREHOUSEAREA;
            mmodel.WIPEIN = model.WIPEIN;
            mmodel.WIPEOUT = model.WIPEOUT;
            mmodel.EXPANSIONAREA = model.EXPANSIONAREA;
            mmodel.HOUSEHOLDEXPANSIONAREA = model.HOUSEHOLDEXPANSIONAREA;
            mmodel.REWARDAREA = model.REWARDAREA;
            mmodel.MARRIAGEAREA = model.MARRIAGEAREA;
            mmodel.HOUSEMONEY = model.HOUSEMONEY;
            mmodel.REWARDMONEY = model.REWARDMONEY;
            mmodel.EMPTYTIME = model.EMPTYTIME;

            return db.SaveChanges();
        }

        /// <summary>
        /// 修改过渡阶段
        /// </summary>
        /// <returns></returns>
        public static int EditTransition(decimal id, CQGL_TRANSITIONS model)
        {
            Entities db = new Entities();
            CQGL_TRANSITIONS mmodel = db.CQGL_TRANSITIONS.OrderBy(t=>t.TRANSITIONID).FirstOrDefault(t => t.HOUSEID == id);
            mmodel.STARTTIME = model.STARTTIME;
            mmodel.TERM = model.TERM;
            mmodel.STANDARDMONEY = model.STANDARDMONEY;
            mmodel.MONEY = model.MONEY;
            mmodel.PLACEAREA = model.PLACEAREA;

            return db.SaveChanges();
        }

        /// <summary>
        /// 修改抽签安置阶段
        /// </summary>
        /// <returns></returns>
        public static int EditDraw(decimal id, CQGL_DRAWS model)
        {
            Entities db = new Entities();
            CQGL_DRAWS mmodel = db.CQGL_DRAWS.FirstOrDefault(t => t.HOUSEID == id);
            mmodel.DRAWTIME = model.DRAWTIME;
            mmodel.HOUSECOUNT = model.HOUSECOUNT;
            mmodel.OVERAREA = model.OVERAREA;

            return db.SaveChanges();
        }

        /// <summary>
        /// 修改安置房
        /// </summary>
        /// <returns></returns>
        public static decimal EditDrawHouse(decimal id)
        {
            Entities db = new Entities();
            CQGL_DRAWS mmodel = db.CQGL_DRAWS.FirstOrDefault(t => t.HOUSEID == id);
            //删除原来的安置房
            string sql = "delete from CQGL_DRAWHOUSE where DRAWID = " + mmodel.DRAWID;
            db.Database.ExecuteSqlCommand(sql);

            return mmodel.DRAWID;
        }

        /// <summary>
        /// 修改结账阶段
        /// </summary>
        /// <returns></returns>
        public static int EditCheckOut(decimal id, CQGL_CHECKOUT model)
        {
            Entities db = new Entities();
            CQGL_CHECKOUT mmodel = db.CQGL_CHECKOUT.FirstOrDefault(t => t.HOUSEID == id);
            mmodel.CHTIME = model.CHTIME;
            mmodel.CHUSER = model.CHUSER;
            mmodel.ACCOUNTUSER = model.ACCOUNTUSER;
            mmodel.MONEY = model.MONEY;

            return db.SaveChanges();
        }

        /// <summary>
        /// 修改企业拆迁
        /// </summary>
        /// <returns></returns>
        public static int EditEnterprise(decimal id, CQGL_ENTERPRISES model)
        {
            Entities db = new Entities();
            CQGL_ENTERPRISES mmodel = db.CQGL_ENTERPRISES.FirstOrDefault(t => t.ENTERPRISEID == id);
            mmodel.LEGALNAME = model.LEGALNAME;
            mmodel.LEGALPHONE = model.LEGALPHONE;
            mmodel.LANDAREA = model.LANDAREA;
            mmodel.WARRANTAREA = model.WARRANTAREA;
            mmodel.MEASUREMENTAREA = model.MEASUREMENTAREA;
            mmodel.WITHOUTAREA = model.WITHOUTAREA;
            mmodel.SIGNTIME = model.SIGNTIME;
            mmodel.EMPTYTIME = model.EMPTYTIME;
            mmodel.SUMMONEY = model.SUMMONEY;
            mmodel.TAX = model.TAX;

            return db.SaveChanges();
        }

        /// <summary>
        /// 添加项目文件
        /// </summary>
        /// <returns></returns>
        public static int AddProjectFile(CQGL_FILES model)
        {
            Entities db = new Entities();
            db.CQGL_FILES.Add(model);
            return db.SaveChanges();
        }

        /// <summary>
        /// 修改项目
        /// </summary>
        /// <returns></returns>
        public static int EditProject(decimal id, CQGL_PROJECTS model)
        {
            Entities db = new Entities();
            CQGL_PROJECTS mmodel = db.CQGL_PROJECTS.FirstOrDefault(t => t.PROJECTID == id);
            mmodel.PROJECTNAME = model.PROJECTNAME;
            mmodel.PROJECTUSER = model.PROJECTUSER;
            mmodel.PROJECTAREA = model.PROJECTAREA;
            mmodel.HOUSEHOLDS = model.HOUSEHOLDS;
            mmodel.STARTTIME = model.STARTTIME;
            mmodel.ENDTIME = model.ENDTIME;
            mmodel.REMARKS = model.REMARKS;
            mmodel.GEOMETRY = model.GEOMETRY;

            return db.SaveChanges();
        }

        /// <summary>
        /// 根据项目标识获取项目信息
        /// </summary>
        /// <returns></returns>
        public static VMProject EditShow(decimal id)
        {
            Entities db = new Entities();
            VMProject mmodel = new VMProject();
            CQGL_PROJECTS model = db.CQGL_PROJECTS.FirstOrDefault(t => t.PROJECTID == id);
            mmodel.PROJECTNAME = model.PROJECTNAME;
            mmodel.PROJECTUSER = model.PROJECTUSER;
            mmodel.PROJECTAREA = model.PROJECTAREA;
            mmodel.HOUSEHOLDS = model.HOUSEHOLDS;
            mmodel.StartTimeStr = model.STARTTIME == null ? "": model.STARTTIME.Value.ToString("yyyy-MM-dd");
            mmodel.EndTimeStr = model.ENDTIME == null ? "": model.ENDTIME.Value.ToString("yyyy-MM-dd");
            mmodel.REMARKS = model.REMARKS;
            mmodel.GEOMETRY = model.GEOMETRY;
            VMCQGL_File GetFile = GetFilesNames(id, 1);
            mmodel.FileIdStr = GetFile.FileIdStr;
            mmodel.FILENAME = GetFile.FILENAME;
            mmodel.FILEPATH = GetFile.FILEPATH;

            return mmodel;
        }

        /// <summary>
        /// 获取住宅拆迁的文件
        /// </summary>
        /// <returns></returns>
        public static VMCQGL_File GetFilesNames(decimal id, decimal sourceid)
        {
            Entities db = new Entities();
            List<CQGL_FILES> list = db.CQGL_FILES.Where(t => t.SOURCEID == id && t.GCID == sourceid).ToList();
            VMCQGL_File model = new VMCQGL_File();
            if (list.Count != 0)
            {
                foreach (var item in list)
                {
                    model.FileIdStr += item.FILEID + "|";
                    model.FILENAME += item.FILENAME + "|";
                    model.FILEPATH += item.FILEPATH + "|";
                }
                model.FileIdStr = model.FileIdStr.Substring(0, model.FileIdStr.Length - 1);
                model.FILENAME = model.FILENAME.Substring(0, model.FILENAME.Length - 1);
                model.FILEPATH = model.FILEPATH.Substring(0, model.FILEPATH.Length - 1);
            }

            return model;
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <returns></returns>
        public static int Delete(decimal id)
        {
            Entities db = new Entities();
            CQGL_PROJECTS model = db.CQGL_PROJECTS.FirstOrDefault(t => t.PROJECTID == id);
            model.STATE = 3;
            return db.SaveChanges();
        }

        /// <summary>
        /// 根据住宅标识获取签协标识
        /// </summary>
        /// <returns></returns>
        public static decimal GetSignIdByHouseId(decimal id)
        {
            Entities db = new Entities();
            CQGL_SIGNS model = db.CQGL_SIGNS.FirstOrDefault(t => t.HOUSEID == id);
            return model.SIGNID;
        }

        /// <summary>
        /// 根据住宅标识获取住宅信息
        /// </summary>
        /// <returns></returns>
        public static CQGL_HOUSES GeHouseInfoByHouseId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            CQGL_HOUSES model = db.CQGL_HOUSES.FirstOrDefault(t => t.HOUSEID == id);
            return model;
        }

        /// <summary>
        /// 根据住宅标识获取签协信息
        /// </summary>
        /// <returns></returns>
        public static CQGL_SIGNS GetSignInfoByHouseId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            CQGL_SIGNS model = db.CQGL_SIGNS.FirstOrDefault(t => t.HOUSEID == id);
            return model;
        }

        /// <summary>
        /// 根据住宅标识获取所有过渡费
        /// </summary>
        /// <returns></returns>
        public static List<VMCQGL_TRANSITIONS> GetTransitionsByHouseId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            List<VMCQGL_TRANSITIONS> list = (from ct in db.CQGL_TRANSITIONS
                                             orderby ct.TRANSITIONID descending
                                             where ct.HOUSEID == id
                                             select new VMCQGL_TRANSITIONS
                                             {
                                                 TRANSITIONID = ct.TRANSITIONID,
                                                 STARTTIME = ct.STARTTIME,
                                                 TERM = ct.TERM,
                                                 STANDARDMONEY = ct.STANDARDMONEY,
                                                 MONEY = ct.MONEY,
                                                 PLACEAREA = ct.PLACEAREA,
                                                 CREATTIME = ct.CREATTIME
                                             }).ToList();
            foreach (var item in list)
            {
                VMCQGL_File GetFile = GetFilesNames(item.TRANSITIONID, 4);
                item.FILENAME = GetFile.FILENAME;
                item.FILEPATH = GetFile.FILEPATH;
            }
            
            return list;
        }

        /// <summary>
        /// 根据住宅标识获取首次过渡费信息
        /// </summary>
        /// <returns></returns>
        public static CQGL_TRANSITIONS GetTransitionInfoByHouseId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            CQGL_TRANSITIONS model = db.CQGL_TRANSITIONS.OrderBy(t=>t.TRANSITIONID).FirstOrDefault(t => t.HOUSEID == id);
            return model;
        }

        /// <summary>
        /// 根据住宅标识获取抽签信息
        /// </summary>
        /// <returns></returns>
        public static CQGL_DRAWS GetDrawInfoByHouseId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            CQGL_DRAWS model = db.CQGL_DRAWS.FirstOrDefault(t => t.HOUSEID == id);
            return model;
        }

        /// <summary>
        /// 根据抽签标识获取安置房信息
        /// </summary>
        /// <returns></returns>
        public static List<CQGL_DRAWHOUSE> GetDWHouseInfoByDrawId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            List<CQGL_DRAWHOUSE> list = db.CQGL_DRAWHOUSE.Where(t => t.DRAWID == id).OrderBy(t=>t.DWHID).ToList();
            return list;
        }

        /// <summary>
        /// 根据住宅标识获取结账信息
        /// </summary>
        /// <returns></returns>
        public static CQGL_CHECKOUT GetCheckOutInfoByHouseId(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            CQGL_CHECKOUT model = db.CQGL_CHECKOUT.FirstOrDefault(t => t.HOUSEID == id);
            return model;
        }
        
        /// <summary>
        /// 根据企业标识获取企业信息
        /// </summary>
        /// <returns></returns>
        public static CQGL_ENTERPRISES EditEPShow(decimal id)
        {
            Entities db = new Entities();
            db.Configuration.LazyLoadingEnabled = false;
            CQGL_ENTERPRISES model = db.CQGL_ENTERPRISES.FirstOrDefault(t => t.ENTERPRISEID == id);
            return model;
        }

        /// <summary>
        /// 获取企业支付详情
        /// </summary>
        /// <returns></returns>
        public static List<VMCQGL_EPMoney> GetEPList(decimal id)
        {
            Entities db = new Entities();
            List<VMCQGL_EPMoney> list = (from em in db.CQGL_ENTERPRISEMONEYS
                                         join e in db.CQGL_ENTERPRISES
                                         on em.ENTERPRISEID equals e.ENTERPRISEID
                                         where em.ENTERPRISEID == id
                                         orderby em.CREATETIME descending
                                         select new VMCQGL_EPMoney
                                         {
                                             OMID = em.OMID,
                                             PAYPALTIME = em.PAYPALTIME,
                                             PARPALMONEY = em.PARPALMONEY,
                                             REMARKS = em.REMARKS
                                         }).ToList();
            foreach (var item in list)
            {
                VMCQGL_File GetFile = GetFilesNames((decimal)item.OMID, 8);
                item.FILENAME = GetFile.FILENAME;
                item.FILEPATH = GetFile.FILEPATH;
                item.PayTime = item.PAYPALTIME == null ? "": item.PAYPALTIME.Value.ToString("yyyy-MM-dd");
            }
            return list;
        }

        /// <summary>
        /// 删除数据库文件
        /// </summary>
        /// <param name=""></param>
        public static int DeleteDBFile(string AttrachId)
        {
            Entities db = new Entities();
            string sql = "delete from CQGL_FILES where FILEID = " + AttrachId;
            return db.Database.ExecuteSqlCommand(sql);
        }
    }
}
