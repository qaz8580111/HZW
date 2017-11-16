using HZW.ZHCG.DAL.Enum;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class StoreBasesDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public void Insert(StoreBaseSort store)
        {
            using (hzwEntities db = new hzwEntities())
            {
                store_bases addstore = new store_bases
                {
                    store_name = store.storename,
                    id_type = db.store_classes.First(t => t.type_id == store.typeid).firstletter,//GenerationNumber(store.typeid, "000001"),
                    type_id = Convert.ToInt32(store.typeid),
                    address = store.address,
                    grometry = store.grometry,
                    person = store.person,
                    businessperson = store.businessperson,
                    businesscontact = store.businesscontact,
                    registnum = store.registnum,
                    registname = store.registname,
                    registcontact = store.registcontact,
                    businessscope = store.businessscope,
                    //registdate =  Convert.ToDateTime(store.registdate),
                    //businessenddate = Convert.ToDateTime(store.businessenddate),
                    businesslicense = store.businesslicense,
                    healthcard = store.healthcard,
                    mqsbperson = store.mqsbperson,
                    gridnum = store.gridnum,
                    gridperson = store.gridperson,
                    gridcontact = store.gridcontact,
                    createtime = DateTime.Now,
                    createuserid = store.createuserid,
                    status = (int)IsDelete.nodele,
                    remark=store.remark,
                    remark2=store.remark2
                };
                if (!string.IsNullOrEmpty(store.registdate))
                    addstore.registdate = Convert.ToDateTime(store.registdate);
                if (!string.IsNullOrEmpty(store.businessenddate))
                    addstore.businessenddate = Convert.ToDateTime(store.businessenddate);
                db.store_bases.Add(addstore);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 根据类型Id生成编号
        /// </summary>
        /// <param name="storeTypeId">类型id</param>
        /// <param name="Number">需要生成的编号000001</param>
        /// <returns></returns>
        public string GenerationNumber(int? storeTypeId, string Number)
        {
            //查询数据库首字母
            using (hzwEntities db = new hzwEntities())
            {
                //获取首字母
                string Letter = db.store_classes.First(t => t.type_id == storeTypeId).firstletter;
                string sql = "select * from store_bases ORDER BY store_id DESC LIMIT 0,1";
                List<store_bases> list = db.Database.SqlQuery<store_bases>(sql).ToList();
                if (list.Count > 0)
                {
                    string newsString = list[0].id_type;
                    string lastNumber = newsString.Substring(Letter.Length, (newsString.Length - Letter.Length));
                    int result = Convert.ToInt32(lastNumber);
                    string newresult = (result + 1).ToString();
                    string ResultString = "";
                    for (int j = 0; j < (Number.Length - newresult.Length); j++)
                    {
                        ResultString += "0";
                    }
                    return Letter + ResultString + newresult;
                }
                else
                {
                    return Letter + Number;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="store_id"></param>
        /// <returns></returns>
        public int Delete(int store_id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                store_bases store = db.store_bases.First(t => t.store_id == store_id);
                store.status = (int)IsDelete.delete;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="store">实体对象</param>
        /// <returns></returns>
        public int Edit(StoreBases store)
        {
            using (hzwEntities db = new hzwEntities())
            {
                store_bases editstore = db.store_bases.First(t => t.store_id == store.storeid);
                editstore.id_type = db.store_classes.FirstOrDefault(t => t.type_id == store.typeid).firstletter;
                editstore.store_name = store.storename;
                editstore.type_id = store.typeid;
                editstore.address = store.address;
                editstore.businesscontact = store.businesscontact;
                if (store.businessenddate != null)
                    editstore.businessenddate = store.businessenddate;
                editstore.businesslicense = store.businesslicense;
                editstore.businessperson = store.businessperson;
                editstore.businessscope = store.businessscope;
                editstore.gridcontact = store.gridcontact;
                editstore.gridnum = store.gridnum;
                editstore.registnum = store.registnum;
                editstore.healthcard = store.healthcard;
                editstore.gridperson = store.gridperson;
                editstore.grometry = store.grometry;
                editstore.mqsbperson = store.mqsbperson;
                editstore.person = store.person;
                editstore.registcontact = store.registcontact;
                if (store.registdate != null)
                    editstore.registdate = store.registdate;
                editstore.registname = store.registname;
                editstore.remark = store.remark;
                editstore.remark2 = store.remark2;
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public List<StoreBases> Select()
        {
            int status = (int)IsDelete.nodele;
            List<StoreBases> list = new List<StoreBases>();
            using (hzwEntities db = new hzwEntities())
            {
                list = db.store_bases.Where(t => t.status == status).Select(t => new StoreBases()
                {
                    storeid = t.store_id,
                    idtype = t.id_type,
                    storename = t.store_name,
                    typeid = t.type_id,
                    address = t.address,
                    businesscontact = t.businesscontact,
                    businessenddate = t.businessenddate,
                    businesslicense = t.businesslicense,
                    businessperson = t.businessperson,
                    businessscope = t.businessscope,
                    createtime = t.createtime,
                    createuserid = t.createuserid,
                    gridcontact = t.gridcontact,
                    status = t.status,
                    gridnum = t.gridnum,
                    registnum = t.registnum,
                    healthcard = t.healthcard,
                    gridperson = t.gridperson,
                    grometry = t.grometry,
                    mqsbperson = t.mqsbperson,
                    person = t.person,
                    registcontact = t.registcontact,
                    registdate = t.registdate,
                    registname = t.registname,
                    typename = t.store_classes.type_name,
                    remark=t.remark,
                    remark2=t.remark2
                }).ToList();
            }
            return list;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<StoreBases> Select(List<Filter> filters, int start, int limit)
        {
            int status = (int)IsDelete.nodele;
            List<StoreBases> list = new List<StoreBases>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<StoreBases> query = db.store_bases.Where(t => t.status == status).Select(t => new StoreBases()
                {
                    storeid = t.store_id,
                    idtype = t.id_type,
                    storename = t.store_name,
                    typeid = t.type_id,
                    address = t.address,
                    businesscontact = t.businesscontact,
                    businessenddate = t.businessenddate,
                    businesslicense = t.businesslicense,
                    businessperson = t.businessperson,
                    businessscope = t.businessscope,
                    createtime = t.createtime,
                    createuserid = t.createuserid,
                    gridcontact = t.gridcontact,
                    status = t.status,
                    gridnum = t.gridnum,
                    registnum = t.registnum,
                    healthcard = t.healthcard,
                    gridperson = t.gridperson,
                    grometry = t.grometry,
                    mqsbperson = t.mqsbperson,
                    person = t.person,
                    registcontact = t.registcontact,
                    registdate = t.registdate,
                    registname = t.registname,
                    typename = t.store_classes.type_name,
                    remark=t.remark,
                    remark2=t.remark2
                });

                //筛选
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "idtype":
                                if (!string.IsNullOrEmpty(value))
                                    query = query.Where(t => t.idtype.Contains(value));
                                break;
                            case "storename":
                                if (!string.IsNullOrEmpty(value))
                                    query = query.Where(t => t.storename.Contains(value));
                                break;
                            case "typeid":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int type_id = Convert.ToInt32(value);
                                    query = query.Where(t => t.typeid == type_id);
                                }
                                break;
                        }
                    }
                }
                list = query.ToList();
                return list.OrderByDescending(t => t.createtime).Skip(start).Take(limit).ToList();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int SelectAllCount(List<Filter> filters)
        {
            int status = (int)IsDelete.nodele;
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<StoreBases> query = db.store_bases.Where(t => t.status == status).Select(t => new StoreBases()
                {
                    storeid = t.store_id,
                    idtype = t.id_type,
                    storename = t.store_name,
                    typeid = t.type_id,
                    address = t.address,
                    businesscontact = t.businesscontact,
                    businessenddate = t.businessenddate,
                    businesslicense = t.businesslicense,
                    businessperson = t.businessperson,
                    businessscope = t.businessscope,
                    createtime = t.createtime,
                    createuserid = t.createuserid,
                    gridcontact = t.gridcontact,
                    status = t.status,
                    gridnum = t.gridnum,
                    registnum = t.registnum,
                    healthcard = t.healthcard,
                    gridperson = t.gridperson,
                    grometry = t.grometry,
                    mqsbperson = t.mqsbperson,
                    person = t.person,
                    registcontact = t.registcontact,
                    registdate = t.registdate,
                    registname = t.registname,
                    typename = t.store_classes.type_name,
                    remark=t.remark,
                    remark2=t.remark2,
                });

                //筛选
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "idtype":
                                if (!string.IsNullOrEmpty(value))
                                    query = query.Where(t => t.idtype.Contains(value));
                                break;
                            case "storename":
                                if (!string.IsNullOrEmpty(value))
                                    query = query.Where(t => t.storename.Contains(value));
                                break;
                            case "typeid":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int type_id = Convert.ToInt32(value);
                                    query = query.Where(t => t.typeid == type_id);
                                }
                                break;
                        }
                    }
                }
                return query.Count();
            }
        }

        /// <summary>
        /// 返回单个实体对象
        /// </summary>
        /// <param name="store_id">主键Id</param>
        /// <returns></returns>
        public store_bases SelectSingle(int store_id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                store_bases store = db.store_bases.First(t => t.store_id == store_id);
                return store;
            }
        }


        /// <summary>
        /// 前段数据列表查询
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<StoreBases> SelectAllStore(string name, string type, int start, int limit)
        {
            int status = (int)IsDelete.nodele;
            List<StoreBases> list = new List<StoreBases>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<StoreBases> query = db.store_bases.Where(t => t.status == status).Select(t => new StoreBases()
                {
                    storeid = t.store_id,
                    idtype = t.id_type,
                    storename = t.store_name,
                    typeid = t.type_id,
                    address = t.address,
                    businesscontact = t.businesscontact,
                    businessenddate = t.businessenddate,
                    businesslicense = t.businesslicense,
                    businessperson = t.businessperson,
                    businessscope = t.businessscope,
                    createtime = t.createtime,
                    createuserid = t.createuserid,
                    gridcontact = t.gridcontact,
                    status = t.status,
                    gridnum = t.gridnum,
                    registnum = t.registnum,
                    healthcard = t.healthcard,
                    gridperson = t.gridperson,
                    grometry = t.grometry,
                    mqsbperson = t.mqsbperson,
                    person = t.person,
                    registcontact = t.registcontact,
                    registdate = t.registdate,
                    registname = t.registname,
                    typename = t.store_classes.type_name,
                    remark=t.remark,
                    remark2=t.remark2,
                });

                //筛选
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(t => t.storename.Contains(name));
                }
                if (!string.IsNullOrEmpty(type))
                {
 
                        int types = int.Parse(type);
                        query = query.Where(t => t.typeid == types);
                    
                }
                list = query.OrderByDescending(t => t.createtime).Skip((start - 1) * limit).Take(limit).ToList();
                return list;
            }
        }

        /// <summary>
        /// 获取列表的总条数  已修改
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetStoreAllCount(string name, string type)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<store_bases> lists = db.store_bases;
                if (!string.IsNullOrEmpty(name))
                {
                    lists = lists.Where(a => a.store_name.Contains(name));
                }
                if (!string.IsNullOrEmpty(type))
                {

                        int types = int.Parse(type);
                        lists = lists.Where(t => t.type_id == types);
                    
                }

                return lists.Count();
            }
        }

        /// <summary>
        /// 展示平台显示数据详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StoreBases GetStoreModelByID(int id)
        {
            int status = (int)IsDelete.nodele;
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<StoreBases> query = db.store_bases.Where(t => t.status == status).Select(t => new StoreBases()
                {
                    storeid = t.store_id,
                    idtype = t.id_type,
                    storename = t.store_name,
                    typeid = t.type_id,
                    address = t.address,
                    businesscontact = t.businesscontact,
                    businessenddate = t.businessenddate,
                    businesslicense = t.businesslicense,
                    businessperson = t.businessperson,
                    businessscope = t.businessscope,
                    createtime = t.createtime,
                    createuserid = t.createuserid,
                    gridcontact = t.gridcontact,
                    status = t.status,
                    gridnum = t.gridnum,
                    registnum = t.registnum,
                    healthcard = t.healthcard,
                    gridperson = t.gridperson,
                    grometry = t.grometry,
                    mqsbperson = t.mqsbperson,
                    person = t.person,
                    registcontact = t.registcontact,
                    registdate = t.registdate,
                    registname = t.registname,
                    typename = t.store_classes.type_name,
                    remark=t.remark,
                    remark2=t.remark2,
                });
                StoreBases StoreModel = query.FirstOrDefault(t => t.storeid == id);

                //修改
                #region 当浏览店家详细信息时，如果数据库里边是null，则在页面不显示
                if (String.IsNullOrEmpty(StoreModel.registnum))
                {
                    StoreModel.registnum = "";
                }
                if (String.IsNullOrEmpty(StoreModel.registname))
                {
                    StoreModel.registname = "";
                }
                if (String.IsNullOrEmpty(StoreModel.gridnum))
                {
                    StoreModel.gridnum = "";
                }
                if (String.IsNullOrEmpty(StoreModel.address))
                {
                    StoreModel.address = "";
                }
                if (String.IsNullOrEmpty(StoreModel.person))
                {
                    StoreModel.person = "";
                }
                if (String.IsNullOrEmpty(StoreModel.mqsbperson))
                {
                    StoreModel.mqsbperson = "";
                }
                if (String.IsNullOrEmpty(StoreModel.businessperson))
                {
                    StoreModel.businessperson = "";
                }
                if (String.IsNullOrEmpty(StoreModel.businesscontact))
                {
                    StoreModel.businesscontact = "";
                }
                if (String.IsNullOrEmpty(StoreModel.typename))
                {
                    StoreModel.typename = "";
                }
                if (String.IsNullOrEmpty(StoreModel.remark2))
                {
                    StoreModel.remark2 = "";
                }
                if (String.IsNullOrEmpty(StoreModel.remark))
                {
                    StoreModel.remark = "";
                }
                #endregion
                return StoreModel;
            }
        }

        /// <summary>
        /// 获取列表的总条数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetStoreListCount(string name, string type, int limit)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                int status = (int)IsDelete.nodele;
                IQueryable<store_bases> lists = db.store_bases;
                if (!string.IsNullOrEmpty(name))
                {
                    lists = lists.Where(a => a.store_name.Contains(name) && a.status == status);
                }
                if (!string.IsNullOrEmpty(type))
                {

                        int types = int.Parse(type);
                        lists = lists.Where(t => t.type_id == types && t.status == status);

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
        /// 获取沿街店家类型个数(前台展示)
        /// </summary>
        /// <returns></returns>
        public List<CommonModel> GetStoreTypeNum()
        {
            using (hzwEntities db = new hzwEntities())
            {
                string sql = string.Format(@"select COUNT(sb.store_id) value,sc.type_name name from store_classes sc 
left join store_bases sb 
on sc.type_id=sb.type_id and sb.status={0}
GROUP BY sc.type_id", (int)IsDelete.nodele);
                List<CommonModel> listresult = db.Database.SqlQuery<CommonModel>(sql).ToList();
                CommonModel model = new CommonModel();
                return listresult;
            }

        }

        /// <summary>
        /// 获取店家总数
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            using (hzwEntities db = new hzwEntities())
            {
                int status=(int)IsDelete.nodele;
                int count = db.store_bases.Where(t => t.status == status).Count();
                return count;
            }
        }

    }
}
