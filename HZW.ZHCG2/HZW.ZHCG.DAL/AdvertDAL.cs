using HZW.ZHCG.DAL.Enum;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class AdvertDAL
    {
        public List<Advert> GetAdverts(List<Filter> filters, int start, int limit)
        {
            List<Advert> list = new List<Advert>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Advert> queryable = (from a in db.ad_outadverts
                                                join b_join in db.ad_classes on a.type_id equals b_join.type_id into bTemp
                                                from b in bTemp.DefaultIfEmpty()
                                                where a.status == 1
                                                select new Advert()
                                                {
                                                    ID = a.ad_id,
                                                    IDType = a.id_type,
                                                    AdName = a.ad_name,
                                                    TypeID = a.type_id,
                                                    TypeName = b.type_name,
                                                    UnitName = a.unit,
                                                    UnitPerson = a.unitperson,
                                                    UnitPhone = a.unitphone,
                                                    Producers = a.producers,
                                                    Prophone = a.prophone,
                                                    State = a.state,
                                                    ExamUnit = a.examunit,
                                                    ExamDate = a.examdate,
                                                    StartDate = a.startdate,
                                                    EndDate = a.enddate,
                                                    Address = a.address,
                                                    Photo1 = a.photo1,
                                                    Photo2 = a.photo2,
                                                    Photo3 = a.photo3,
                                                    Photo4 = a.photo4,
                                                    FileName1 = a.filename1,
                                                    FilePath1 = a.filepath1,
                                                    Grometry = a.grometry,
                                                    Volume = a.volume,
                                                    VLong = a.@long,
                                                    VHigh = a.high,
                                                    VWide = a.wide,
                                                    Materials = a.materials,
                                                    Curingunit = a.curingunit,
                                                    Superviseunit = a.superviseunit,
                                                    Createtime = a.createtime,
                                                    Createuserid = a.createuserid,
                                                    Status = a.status,
                                                    Remark = a.remark
                                                });

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "IDType":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.IDType.Contains(value));
                                break;
                            case "AdName":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.AdName.Contains(value));
                                break;
                            case "TypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int TypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.TypeID == TypeID);
                                }
                                break;
                            case "State":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int id = Convert.ToInt32(value);
                                    DateTime now = DateTime.Now;
                                    DateTime nowadd = DateTime.Now.AddDays(7);
                                    if (id == 3)
                                        queryable = queryable.Where(t => t.EndDate < now);
                                    else if (id == 2)
                                        queryable = queryable.Where(t => t.EndDate <= nowadd && t.EndDate >= now);
                                    else
                                        queryable = queryable.Where(t => t.EndDate > nowadd);
                                }
                                break;
                        }
                    }
                }

                queryable = queryable.OrderByDescending(t => t.Createtime).Skip(start).Take(limit);

                list = queryable.OrderByDescending(t => t.ID).ToList();

            }

            return list;
        }

        public int GetAdvertCount(List<Filter> filters)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<ad_outadverts> queryable = db.ad_outadverts.Where(t => t.status == 1).AsQueryable();

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "IDType":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.id_type.Contains(value));
                                break;
                            case "AdName":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.ad_name.Contains(value));
                                break;
                            case "TypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int TypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.type_id == TypeID);
                                }
                                break;
                            case "State":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int State = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.state == State);
                                }
                                break;
                        }
                    }
                }

                return queryable.Count();
            }
        }

        public int AddAdvert(Advert advert)
        {
            using (hzwEntities db = new hzwEntities())
            {
                ad_outadverts newAdvert = new ad_outadverts()
                {
                    ad_name = advert.AdName,
                    id_type = advert.IDType,
                    type_id = advert.TypeID,
                    unit = advert.UnitName,
                    unitperson = advert.UnitPerson,
                    unitphone = advert.UnitPhone,
                    producers = advert.Producers,
                    prophone = advert.Prophone,
                    examunit = advert.ExamUnit,
                    examdate = (DateTime)advert.ExamDate,
                    startdate = (DateTime)advert.StartDate,
                    enddate = (DateTime)advert.EndDate,
                    address = advert.Address,
                    photo1 = advert.Photo1,
                    photo2 = advert.Photo2,
                    photo3 = advert.Photo3,
                    photo4 = advert.Photo4,
                    filename1 = advert.FileName1,
                    filepath1 = advert.FilePath1,
                    filename2 = advert.FileName2,
                    filepath2 = advert.FilePath2,
                    filename3 = advert.FileName3,
                    filepath3 = advert.FilePath3,
                    grometry = advert.Grometry,
                    volume = advert.Volume,
                    @long = advert.VLong,
                    high = advert.VHigh,
                    wide = advert.VWide,
                    materials = advert.Materials,
                    curingunit = advert.Curingunit,
                    superviseunit = advert.Superviseunit,
                    createtime = advert.Createtime,
                    createuserid = advert.Createuserid,
                    status = 1,
                    remark = advert.Remark
                };

                db.ad_outadverts.Add(newAdvert);

                return db.SaveChanges();
            }

        }

        public int EditAdvert(Advert advert)
        {
            using (hzwEntities db = new hzwEntities())
            {
                ad_outadverts newAdvert = db.ad_outadverts.Find(advert.ID);

                if (newAdvert != null)
                {
                    newAdvert.ad_name = advert.AdName;
                    newAdvert.id_type = advert.IDType;
                    newAdvert.type_id = advert.TypeID;
                    newAdvert.unit = advert.UnitName;
                    newAdvert.unitperson = advert.UnitPerson;
                    newAdvert.unitphone = advert.UnitPhone;
                    newAdvert.producers = advert.Producers;
                    newAdvert.prophone = advert.Prophone;
                    newAdvert.examunit = advert.ExamUnit;
                    newAdvert.examdate = (DateTime)advert.ExamDate;
                    newAdvert.startdate = (DateTime)advert.StartDate;
                    newAdvert.enddate = (DateTime)advert.EndDate;
                    newAdvert.address = advert.Address;
                    newAdvert.photo1 = advert.Photo1 == null ? newAdvert.photo1 : advert.Photo1;
                    newAdvert.photo2 = advert.Photo2 == null ? newAdvert.photo2 : advert.Photo2;
                    newAdvert.photo3 = advert.Photo3 == null ? newAdvert.photo3 : advert.Photo3;
                    newAdvert.photo4 = advert.Photo4 == null ? newAdvert.photo4 : advert.Photo4;
                    newAdvert.filename1 = advert.FileName1 == null ? newAdvert.filename1 : advert.FileName1;
                    newAdvert.filepath1 = advert.FilePath1 == null ? newAdvert.filepath1 : advert.FilePath1;
                    newAdvert.grometry = advert.Grometry;
                    newAdvert.volume = advert.Volume;
                    newAdvert.@long = advert.VLong;
                    newAdvert.high = advert.VHigh;
                    newAdvert.wide = advert.VWide;
                    newAdvert.materials = advert.Materials;
                    newAdvert.curingunit = advert.Curingunit;
                    newAdvert.superviseunit = advert.Superviseunit;
                    newAdvert.remark = advert.Remark;
                }
                return db.SaveChanges();
            }
        }


        public void DeleteAdvert(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                ad_outadverts advert = db.ad_outadverts.Where(t => t.ad_id == id).SingleOrDefault();
                advert.status = 2;
                db.SaveChanges();
            }
        }

        public List<AdvertType> GetAdvertTypes()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<AdvertType> list = (from a in db.ad_classes
                                         orderby a.type_id
                                         select new AdvertType
                                         {
                                             ID = a.type_id,
                                             Name = a.type_name,
                                             icon = a.icon
                                         }).ToList();
                return list;
            }
        }

        public ad_classes GetAdvertTypesByID(int? ID)
        {
            using (hzwEntities db = new hzwEntities())
            {
                ad_classes model = db.ad_classes.FirstOrDefault(t => t.type_id == ID);
                return model;
            }
        }










        /// <summary>
        /// 前段数据列表查询
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Advert> AdvertAllStore(string name, string type, int start, int limit)
        {
            List<Advert> list = new List<Advert>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Advert> queryable = (from a in db.ad_outadverts
                                                join b_join in db.ad_classes on a.type_id equals b_join.type_id into bTemp
                                                from b in bTemp.DefaultIfEmpty()
                                                where a.status == 1
                                                select new Advert()
                                                {
                                                    ID = a.ad_id,
                                                    IDType = a.id_type,
                                                    AdName = a.ad_name,
                                                    TypeID = a.type_id,
                                                    TypeName = b.type_name,
                                                    UnitName = a.unit,
                                                    UnitPerson = a.unitperson,
                                                    UnitPhone = a.unitphone,
                                                    Producers = a.producers,
                                                    Prophone = a.prophone,
                                                    State = a.state,
                                                    ExamUnit = a.examunit,
                                                    ExamDate = a.examdate,
                                                    StartDate = a.startdate,
                                                    EndDate = a.enddate,
                                                    Address = a.address,
                                                    Photo1 = a.photo1,
                                                    Photo2 = a.photo2,
                                                    Photo3 = a.photo3,
                                                    Photo4 = a.photo4,
                                                    FileName1 = a.filename1,
                                                    FilePath1 = a.filepath1,
                                                    Grometry = a.grometry,
                                                    Volume = a.volume,
                                                    VLong = a.@long,
                                                    VHigh = a.high,
                                                    VWide = a.wide,
                                                    Materials = a.materials,
                                                    Curingunit = a.curingunit,
                                                    Superviseunit = a.superviseunit,
                                                    Createtime = a.createtime,
                                                    Createuserid = a.createuserid,
                                                    Status = a.status,
                                                    statusName = DateTime.Now >= a.enddate ? "到期" : "未到期",
                                                    Remark = a.remark,
                                                });

                //筛选
                if (!string.IsNullOrEmpty(name))
                {
                    queryable = queryable.Where(t => t.AdName.Contains(name));
                }
                if (!string.IsNullOrEmpty(type))
                {
                    if (!String.Equals(type, "qb"))
                    {
                        int types = int.Parse(type);
                        queryable = queryable.Where(t => t.TypeID == types);
                    }
                }
                list = queryable.OrderByDescending(t => t.Createtime).Skip((start - 1) * limit).Take(limit).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取列表的总条数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetAdvertAllCount(string name, string type)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<ad_outadverts> lists = db.ad_outadverts;
                if (!string.IsNullOrEmpty(name))
                {
                    lists = lists.Where(a => a.ad_name.Contains(name));
                }
                if (!string.IsNullOrEmpty(type))
                {
                    if (!String.Equals(type, "qb"))
                    {
                        int types = int.Parse(type);
                        lists = lists.Where(t => t.type_id == types);
                    }
                }

                return lists.Count();
            }
        }

        /// <summary>
        /// 展示平台显示数据详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Advert GetAdvertModelByID(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Advert> queryable = (from a in db.ad_outadverts
                                                join b_join in db.ad_classes on a.type_id equals b_join.type_id into bTemp
                                                from b in bTemp.DefaultIfEmpty()
                                                where a.status == 1
                                                select new Advert()
                                                {
                                                    ID = a.ad_id,
                                                    IDType = a.id_type,
                                                    AdName = a.ad_name,
                                                    TypeID = a.type_id,
                                                    TypeName = b.type_name,
                                                    UnitName = a.unit,
                                                    UnitPerson = a.unitperson,
                                                    UnitPhone = a.unitphone,
                                                    Producers = a.producers,
                                                    Prophone = a.prophone,
                                                    State = a.state,
                                                    ExamUnit = a.examunit,
                                                    ExamDate = a.examdate,
                                                    StartDate = a.startdate,
                                                    EndDate = a.enddate,
                                                    Address = a.address,
                                                    Photo1 = a.photo1,
                                                    Photo2 = a.photo2,
                                                    Photo3 = a.photo3,
                                                    Photo4 = a.photo4,
                                                    FileName1 = a.filename1,
                                                    FilePath1 = a.filepath1,
                                                    Grometry = a.grometry,
                                                    Volume = a.volume,
                                                    VLong = a.@long,
                                                    VHigh = a.high,
                                                    VWide = a.wide,
                                                    Materials = a.materials,
                                                    Curingunit = a.curingunit,
                                                    Superviseunit = a.superviseunit,
                                                    Createtime = a.createtime,
                                                    Createuserid = a.createuserid,
                                                    Status = a.status,
                                                    statusName = DateTime.Now >= a.enddate ? "到期" : "未到期",
                                                    Remark = a.remark,
                                                });
                Advert AdvertModel = queryable.FirstOrDefault(t => t.ID == id);
                if (String.IsNullOrEmpty(AdvertModel.Remark))
                {
                    AdvertModel.Remark = "";
                }
                AdvertModel.EndDate = Convert.ToDateTime(Convert.ToDateTime(AdvertModel.EndDate.ToString()).ToShortDateString());
                return AdvertModel;
            }
        }
        /// <summary>
        /// 获取列表的总条数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetAdvertListCount(string name, string type, int limit)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<ad_outadverts> lists = db.ad_outadverts;
                if (!string.IsNullOrEmpty(name))
                {
                    lists = lists.Where(a => a.ad_name.Contains(name));
                }
                if (!string.IsNullOrEmpty(type))
                {
                    if (!String.Equals(type, "qb"))
                    {
                        int types = int.Parse(type);
                        lists = lists.Where(t => t.type_id == types);
                    }
                }
                int count = lists.Count();
                int pagecount = 0;
                if (count % limit == 0)
                {
                    pagecount = count / limit;
                }
                else
                {
                    pagecount = (count / limit) + 1;
                }

                return pagecount;
            }
        }

        /// <summary>
        /// 获取最新4条户外广告
        /// </summary>
        /// <returns></returns>
        public List<Advert> GetNewAdvert()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<Advert> list = (from a in db.ad_outadverts
                                     orderby a.createtime descending
                                     where a.status == 1
                                     select new Advert()
                                     {
                                         AdName = a.ad_name,
                                         Address = a.address,
                                         EndDate = a.enddate
                                     }).Take(4).ToList();

                return list;
            }
        }
        /// <summary>
        /// 获取到期4条户外广告
        /// </summary>
        /// <returns></returns>
        public List<Advert> GetEndDateAdvert()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<Advert> list = (from a in db.ad_outadverts
                                     orderby a.enddate descending
                                     where a.status == 1
                                     select new Advert()
                                     {
                                         AdName = a.ad_name,
                                         Address = a.address,
                                         EndDate = a.enddate
                                     }).Take(4).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取户外广告类型个数
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetAdvertTypeNum()
        {
            using (hzwEntities db = new hzwEntities())
            {
                string sql = string.Format(@"select count(ao.ad_id) value,ac.type_name name from ad_classes ac
left join ad_outadverts ao on ac.type_id=ao.type_id and ao.status={0} 
GROUP BY ac.type_id LIMIT 4", (int)IsOnline.online);
                List<CommonModel> listresult = db.Database.SqlQuery<CommonModel>(sql).ToList();
                return listresult;
            }
        }
    }
}
