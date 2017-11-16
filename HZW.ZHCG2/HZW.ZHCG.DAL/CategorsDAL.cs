using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;
using HZW.ZHCG.DAL.Enum;

namespace HZW.ZHCG.DAL
{
    public class CategorsDAL
    {
        /// <summary>
        /// 获取所有的栏目大类
        /// </summary>
        /// <returns></returns>
        public List<Categors> GetBigCategors()
        {
            List<Categors> list = new List<Categors>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Categors> iquaryable = from categors in db.mh_categors
                                                  where categors.status == 1
                                                  && categors.prentid == null
                                                  orderby categors.seqno descending
                                                  select new Categors
                                                  {
                                                      ID = categors.categoryid,
                                                      Name = categors.name,
                                                      SeqNo = categors.seqno
                                                  };
                list = iquaryable.ToList();
            }
            return list;
        }

        /// <summary>
        ///根据栏目大类ID获取栏目小类
        /// </summary>
        /// <returns></returns>
        public List<Categors> GetSmallCategors(int BigID)
        {
            List<Categors> list = new List<Categors>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Categors> iquaryable = from categors in db.mh_categors
                                                  where categors.status == 1
                                                  && categors.prentid == BigID
                                                  orderby categors.seqno descending
                                                  select new Categors
                                                  {
                                                      ID = categors.categoryid,
                                                      Name = categors.name,
                                                      SeqNo = categors.seqno
                                                  };
                list = iquaryable.ToList();
            }
            return list;
        }

        /// <summary>
        /// 查询所有类别
        /// </summary>
        /// <returns></returns>
        public List<Categors> GetAllSmallCategors(List<Filter> filters, int start, int limit)
        {
            List<Categors> list = new List<Categors>();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Categors> queryable = from categors in db.mh_categors
                                                 from bclass in db.mh_categors
                                                 where categors.prentid == bclass.categoryid
                                                  && categors.status == 1
                                                  && categors.prentid != null
                                                 select new Categors
                                             {
                                                 ID = categors.categoryid,
                                                 Name = categors.name,
                                                 BigName = bclass.name,
                                                 BigID = bclass.categoryid,
                                                 createdTime = categors.createtime,
                                                 isonline = categors.isonline,
                                                 SeqNo = categors.seqno
                                             };
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "BigID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int bigid = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.BigID.Value == bigid);
                                }
                                break;
                            case "ID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int id = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.ID == id);
                                }
                                break;
                            case "isonline":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int isonline = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.isonline == isonline);
                                }
                                break;
                        }
                    }
                }
                list = queryable.ToList();
            }
            list = list.OrderBy(t => t.isonline).Skip(start).Take(limit).ToList();
            return list;
        }

        /// <summary>
        /// 查询所有类别条数
        /// </summary>
        /// <returns></returns>
        public int GetAllSmallCategorsCount(List<Filter> filters)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<Categors> queryable = from categors in db.mh_categors
                                                 from bclass in db.mh_categors
                                                 where categors.prentid == bclass.categoryid
                                                  && categors.status == 1
                                                  && categors.prentid != null
                                                 select new Categors
                                                 {
                                                     ID = categors.categoryid,
                                                     Name = categors.name,
                                                     BigName = bclass.name,
                                                     BigID = bclass.categoryid,
                                                     createdTime = categors.createtime,
                                                     isonline = categors.isonline,
                                                     SeqNo = categors.seqno
                                                 };
                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "BigID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int bigid = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.BigID.Value == bigid);
                                }
                                break;
                            case "ID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int id = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.ID == id);
                                }
                                break;
                            case "isonline":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int isonline = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.isonline == isonline);
                                }
                                break;
                        }
                    }
                }
                return queryable.Count();
            }
        }

        /// <summary>
        /// 添加栏目小类
        /// </summary>
        /// <param name="Categors"></param>
        /// <returns></returns>
        public int AddCategors(Categors Categors)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_categors newCategors = new mh_categors()
                {
                    createtime = DateTime.Now,
                    isonline = Categors.isonline,
                    name = Categors.Name,
                    prentid = Categors.BigID,
                    seqno = Categors.SeqNo,
                    status = 1,
                };

                db.mh_categors.Add(newCategors);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 修改栏目小类
        /// </summary>
        /// <param name="Categors"></param>
        /// <returns></returns>
        public int EditCategors(Categors Categors)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_categors newCategor = db.mh_categors.Find(Categors.ID);
                if (newCategor != null)
                {
                    newCategor.createtime = DateTime.Now;
                    newCategor.isonline = Categors.isonline;
                    newCategor.name = Categors.Name;
                    newCategor.prentid = Categors.BigID;
                    newCategor.seqno = Categors.SeqNo;
                    return db.SaveChanges();
                }
            }
            return 0;
        }

        /// <summary>
        /// 删除一个栏目小类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCategor(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_categors newCategor = db.mh_categors.Find(id);
                if (newCategor != null)
                {
                    newCategor.status = 2;
                    return db.SaveChanges();
                }
            }
            return 0;
        }

        /// <summary>
        /// 更改在线下线状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int EditCategorOnLine(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                mh_categors newCategor = db.mh_categors.Find(id);
                if (newCategor != null)
                {
                    if (newCategor.isonline == 1)
                    {
                        newCategor.isonline = 2;
                    }
                    else if (newCategor.isonline == 2)
                    {
                        newCategor.isonline = 1;
                    }
                    else
                    {
                        newCategor.isonline = 1;
                    }
                    return db.SaveChanges();
                }
            }
            return 0;
        }


        /// <summary>
        /// 根据栏目Id查询栏目大类
        /// </summary>
        /// <param name="cateGoryId"></param>
        /// <returns></returns>
        public Categors GetmhcateGorsById(int cateGoryId)
        {
            Categors categors = new Categors();
            int status = (int)Judge.JudgeTrue;
            int onlineStatus = (int)IsOnline.online;
            using (hzwEntities db = new hzwEntities())
            {
                categors = db.mh_categors.Where(t => t.status == status && t.isonline == onlineStatus && t.categoryid == cateGoryId).Select(t => new Categors
                {
                    ID = t.categoryid,
                    Name = t.name,
                    BigID = t.prentid
                }).ToList().First(t => t.ID == cateGoryId);
            }
            return categors;
        }

        /// <summary>
        /// 根据栏目大类ParentId查询栏目小类
        /// </summary>
        /// <param name="cateGoryId"></param>
        /// <returns></returns>
        public List<Categors> GetmhcateGorsByParentId(int parentId, int takeNumber)
        {
            List<Categors> list = new List<Categors>();
            int status = (int)Judge.JudgeTrue;
            using (hzwEntities db = new hzwEntities())
            {
                if (takeNumber != 0)
                {

                    list = db.mh_categors.OrderBy(t => t.seqno).Where(t => t.prentid == parentId && t.status == status && t.isonline == status).Take(takeNumber).Select(t => new Categors
                    {
                        ID = t.categoryid,
                        BigID = t.prentid,
                        Name = t.name
                    }).ToList();
                }
                else
                {
                    list = db.mh_categors.OrderBy(t => t.seqno).Where(t => t.prentid == parentId && t.status == status && t.isonline == status).Select(t => new Categors
                    {
                        ID = t.categoryid,
                        BigID = t.prentid,
                        Name = t.name
                    }).ToList();
                }
            }
            return list;
        }
    }
}
