using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class UserTypeDAL
    {
        public List<UserType> GetUserTypes()
        {
            List<UserType> list = new List<UserType>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UserType> queryable =
                    db.base_usertypes
                    .OrderBy(t => t.seqno)
                    .Select(t => new UserType()
                    {
                        ID = t.id,
                        Name = t.name
                    });

                list = queryable.ToList();
            }

            return list;
        }

        public List<UserType> GetUserTypes(int start, int limit)
        {
            List<UserType> list = new List<UserType>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UserType> queryable =
                    db.base_usertypes
                    .OrderBy(t => t.seqno)
                    .Select(t => new UserType()
                    {
                        ID = t.id,
                        Name = t.name,
                        SeqNo = t.seqno
                    });

                list = queryable.Skip(start).Take(limit).ToList();

                return list;
            }
        }

        public int GetUserTypeCount()
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<base_usertypes> queryable = db.base_usertypes.AsQueryable();
                return queryable.Count();
            }
        }

        public int AddUserType(UserType userType)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_usertypes newUserType = new base_usertypes()
                {
                    name = userType.Name,
                    seqno = userType.SeqNo
                };
                db.base_usertypes.Add(newUserType);

                return db.SaveChanges();
            }
        }

        public int EditUserType(UserType userType)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_usertypes newUserType = db.base_usertypes.Find(userType.ID);

                if (newUserType != null)
                {
                    newUserType.name = userType.Name;
                    newUserType.seqno = userType.SeqNo;

                    return db.SaveChanges();
                }
            }

            return 0;
        }

        public int DeleteUserType(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_usertypes userType = db.base_usertypes.Find(id);

                if (userType != null)
                {
                    db.base_usertypes.Remove(userType);
                }

                return db.SaveChanges();
            }
        }
    }
}
