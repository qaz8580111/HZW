using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.DAL
{
    public class UserPositionDAL
    {
        public List<UserPosition> GetUserPositions()
        {
            List<UserPosition> list = new List<UserPosition>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UserPosition> queryable =
                    db.sys_userpositions
                    .OrderBy(t => t.USERPOSITIONID)
                    .Select(t => new UserPosition()
                    {
                        ID = t.USERPOSITIONID,
                        Name = t.USERPOSITIONNAME
                    });

                list = queryable.ToList();
            }

            return list;
        }

        public List<UserPosition> GetUserPositions(int start, int limit)
        {
            List<UserPosition> list = new List<UserPosition>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UserPosition> queryable =
                    db.sys_userpositions
                    .OrderBy(t => t.USERPOSITIONID)
                    .Select(t => new UserPosition()
                    {
                        ID = t.USERPOSITIONID,
                        Name = t.USERPOSITIONNAME,
                    });

                list = queryable.Skip(start).Take(limit).ToList();

                return list;
            }
        }

        public int GetUserPositionCount()
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<sys_userpositions> queryable = db.sys_userpositions.AsQueryable();
                return queryable.Count();
            }
        }

        public int AddUserPosition(UserPosition userPosition)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_userpositions newUserPosition = new sys_userpositions()
                {
                    USERPOSITIONID = userPosition.ID,
                    USERPOSITIONNAME = userPosition.Name,
                };
                db.sys_userpositions.Add(newUserPosition);

                return db.SaveChanges();
            }
        }

        public int EditUserPosition(UserPosition userPosition)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_userpositions newUserPosition = db.sys_userpositions.Find(userPosition.ID);

                if (newUserPosition != null)
                {
                    newUserPosition.USERPOSITIONNAME = userPosition.Name;

                    return db.SaveChanges();
                }
            }

            return 0;
        }

        public int DeleteUserPosition(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_userpositions userPosition = db.sys_userpositions.Find(id);

                if (userPosition != null)
                {
                    db.sys_userpositions.Remove(userPosition);
                }

                return db.SaveChanges();
            }
        }
    }
}
