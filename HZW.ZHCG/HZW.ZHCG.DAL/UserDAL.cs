using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;

namespace HZW.ZHCG.DAL
{
    public class UserDAL
    {
        public User GetUserByID(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                User user = db.sys_users.Where(t => t.USERID == id)
                    .Select(t => new User()
                    {
                        ID = t.USERID,
                        UserName = t.USERNAME,
                        PassWord = t.PASSWORD,
                        UnitID = t.UNITID,                        
                        UserPositionID = t.USERPOSITIONID,
                        Phone = t.PHONE,
                        Sex = t.SEX,
                        BirthDay = t.BIRTHDAY,
                        SeqNO = t.SEQNO,
                    })
                    .SingleOrDefault();

                return user;
            }
        }

        public List<User> GetUsersByLoginName(string loginName)
        {
            List<User> list = new List<User>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<User> queryable = db.sys_users
                    .Where(t => t.ACCOUNT == loginName)
                    .Select(t => new User()
                    {
                        ID = t.USERID,
                        UserName = t.USERNAME,
                        PassWord = t.PASSWORD,
                        UnitID = t.UNITID,
                        UserPositionID = t.USERPOSITIONID,
                        Phone = t.PHONE,
                        Sex = t.SEX,
                        BirthDay = t.BIRTHDAY,
                        SeqNO = t.SEQNO,
                    });

                list = queryable.ToList();
            }

            return list;
        }

        public List<User> GetUsersByUnitID(int unitID)
        {
            List<User> list = new List<User>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<User> queryable = db.sys_users
                    .Where(t => t.UNITID == unitID)
                    .Select(t => new User()
                    {
                        ID = t.USERID,
                        UserName = t.USERNAME
                    });

                list = queryable.ToList();
            }

            return list;
        }

        public List<User> GetUsers(List<Filter> filters, int start, int limit)
        {
            List<User> list = new List<User>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<User> queryable =
                     from a in db.sys_users
                     join b_join in db.sys_userpositions on a.USERPOSITIONID equals b_join.USERPOSITIONID into bTemp
                     from b in bTemp.DefaultIfEmpty()
                     join c_join in db.sys_units on a.UNITID equals c_join.UNITID into cTemp
                     from c in cTemp.DefaultIfEmpty()
                     orderby a.SEQNO descending
                     select new User()
                     {
                         ID = a.USERID,
                         UserName = a.USERNAME,
                         PassWord = a.PASSWORD,
                         UnitID = a.UNITID,
                         UnitName = c.UNITNAME,
                         UserPositionID = a.USERPOSITIONID,
                         UserPositionName = b.USERPOSITIONNAME,
                         Phone = a.PHONE,
                         Sex = a.SEX,
                         BirthDay = a.BIRTHDAY,
                         SeqNO = a.SEQNO,
                     };

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "UserName":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.UserName.Contains(value));
                                break;
                            case "UnitID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.UnitID == unitID);
                                }
                                break;
                            case "UserPositionID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int UserPositionID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.UserPositionID == UserPositionID);
                                }
                                break;
                        }
                    }
                }

                queryable = queryable.OrderByDescending(t => t.UpdatedTime).Skip(start).Take(limit);

                list = queryable.ToList();
            }

            return list;
        }

        public int GetUserCount(List<Filter> filters)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<sys_users> queryable = db.sys_users.AsQueryable();

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "UserName":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.USERNAME.Contains(value));
                                break;
                            case "UserPositionID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int UserPositionID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.USERPOSITIONID == UserPositionID);
                                }
                                break;
                        }
                    }
                }

                return queryable.Count();
            }
        }

        public void AddUser(User user)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_users newUser = new sys_users()
                {
                    USERID = user.ID,
                    USERNAME = user.UserName,
                    PASSWORD = user.PassWord,
                    UNITID = user.UnitID,
                    USERPOSITIONID = user.UserPositionID,
                    PHONE = user.Phone,
                    SEX = user.Sex,
                    BIRTHDAY = user.BirthDay,
                    SEQNO = user.SeqNO,
                };

                db.sys_users.Add(newUser);
                db.SaveChanges();

                foreach (int roleID in user.RoleIDArr)
                {
                    sys_userroles newUserRole = new sys_userroles()
                    {
                        USERID = newUser.USERID,
                        ROLEID = roleID
                    };
                    db.sys_userroles.Add(newUserRole);
                }

                db.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_users newUser = db.sys_users.Find(user.ID);

                if (newUser != null)
                {
                    newUser.USERNAME = user.UserName;
                    newUser.UNITID = user.UnitID;
                    newUser.USERPOSITIONID = user.UserPositionID;
                    newUser.ACCOUNT = user.Account;

                    if (!string.IsNullOrEmpty(user.PassWord))
                        newUser.PASSWORD = user.PassWord;


                    //删除现有角色
                    List<sys_userroles> userRoleList = db.sys_userroles.Where(t => t.USERID == user.ID).ToList();

                    if (userRoleList.Count > 0)
                    {
                        foreach (var userRole in userRoleList)
                        {
                            db.sys_userroles.Remove(userRole);
                        }
                    }

                    //添加新角色
                    foreach (int roleID in user.RoleIDArr)
                    {
                        sys_userroles newUserRole = new sys_userroles()
                        {
                            USERID = newUser.USERID,
                            ROLEID = roleID
                        };
                        db.sys_userroles.Add(newUserRole);
                    }

                    db.SaveChanges();
                }
            }
        }

        public void EditUserLoginPwd(User user)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_users newUser = db.sys_users.Find(user.ID);

                if (newUser != null)
                {
                    newUser.ACCOUNT = user.Account;

                    db.SaveChanges();
                }
            }
        }


        public void DeleteUser(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                sys_users user = db.sys_users.Where(t => t.USERID == id).SingleOrDefault();

                if (user != null)
                {
                    //删除现有角色
                    List<sys_userroles> userRoleList = db.sys_userroles.Where(t => t.USERID == id).ToList();

                    if (userRoleList.Count > 0)
                    {
                        foreach (var userRole in userRoleList)
                        {
                            db.sys_userroles.Remove(userRole);
                        }
                    }

                    //删除角色
                    db.sys_users.Remove(user);
                }

                db.SaveChanges();
            }
        }
    }
}
