using HZW.ZHCG.DAL.Enum;
using HZW.ZHCG.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HZW.ZHCG.DAL
{
    public class UserDAL
    {
        public User GetUserByID(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                User user = db.base_users.Where(t => t.id == id)
                    .Select(t => new User()
                    {
                        ID = t.id,
                        Code = t.code,
                        DisplayName = t.displayname,
                        LoginPwd = t.loginpwd,
                        UnitID = t.unitid,
                        UserTypeID = t.usertypeid
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
                IQueryable<User> queryable = db.base_users
                    .Where(t => t.loginname == loginName)
                    .Select(t => new User()
                    {
                        ID = t.id,
                        Code = t.code,
                        DisplayName = t.displayname,
                        LoginPwd = t.loginpwd,
                        UnitID = t.unitid,
                        UserTypeID = t.usertypeid
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
                IQueryable<User> queryable = db.base_users
                    .Where(t => t.unitid == unitID)
                    .Select(t => new User()
                    {
                        ID = t.id,
                        DisplayName = t.displayname
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
                     from a in db.base_users
                     join b_join in db.base_usertypes on a.usertypeid equals b_join.id into bTemp
                     from b in bTemp.DefaultIfEmpty()
                     join c_join in db.base_units on a.unitid equals c_join.id into cTemp
                     from c in cTemp.DefaultIfEmpty()
                     orderby a.updatedtime descending
                     select new User()
                     {
                         ID = a.id,
                         Code = a.code,
                         DisplayName = a.displayname,
                         UserTypeID = a.usertypeid,
                         UserTypeName = b == null ? "" : b.name,
                         UnitID = a.unitid,
                         UnitName = c == null ? "" : c.name,
                         LoginName = a.loginname,
                         CreatedTime = a.createdtime,
                         UpdatedTime = a.updatedtime
                     };

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.Code.Contains(value));
                                break;
                            case "DisplayName":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.DisplayName.Contains(value));
                                break;
                            case "UnitID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.UnitID == unitID);
                                }
                                break;
                            case "UserTypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitTypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.UserTypeID == unitTypeID);
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
                IQueryable<base_users> queryable = db.base_users.AsQueryable();

                if (filters != null && filters.Count > 0)
                {
                    foreach (Filter filter in filters)
                    {
                        string value = filter.value;
                        switch (filter.property)
                        {
                            case "Code":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.code.Contains(value));
                                break;
                            case "DisplayName":
                                if (!string.IsNullOrEmpty(value))
                                    queryable = queryable.Where(t => t.displayname.Contains(value));
                                break;
                            case "UserTypeID":
                                if (!string.IsNullOrEmpty(value))
                                {
                                    int unitTypeID = Convert.ToInt32(value);
                                    queryable = queryable.Where(t => t.usertypeid == unitTypeID);
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
                base_users newUser = new base_users()
                {
                    code = user.Code,
                    displayname = user.DisplayName,
                    unitid = user.UnitID,
                    usertypeid = user.UserTypeID,
                    loginname = user.LoginName,
                    loginpwd = user.LoginPwd,
                    createdtime = user.CreatedTime,
                    updatedtime = user.UpdatedTime
                };

                db.base_users.Add(newUser);
                db.SaveChanges();

                foreach (int roleID in user.RoleIDArr)
                {
                    base_userroles newUserRole = new base_userroles()
                    {
                        userid = newUser.id,
                        roleid = roleID
                    };
                    db.base_userroles.Add(newUserRole);
                }

                db.SaveChanges();
            }
        }

        public void EditUser(User user)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_users newUser = db.base_users.Find(user.ID);

                if (newUser != null)
                {
                    newUser.code = user.Code;
                    newUser.displayname = user.DisplayName;
                    newUser.unitid = user.UnitID;
                    newUser.usertypeid = user.UserTypeID;
                    newUser.loginname = user.LoginName;

                    if (!string.IsNullOrEmpty(user.LoginPwd))
                        newUser.loginpwd = user.LoginPwd;

                    newUser.updatedtime = user.UpdatedTime;

                    //删除现有角色
                    List<base_userroles> userRoleList = db.base_userroles.Where(t => t.userid == user.ID).ToList();

                    if (userRoleList.Count > 0)
                    {
                        foreach (var userRole in userRoleList)
                        {
                            db.base_userroles.Remove(userRole);
                        }
                    }

                    //添加新角色
                    foreach (int roleID in user.RoleIDArr)
                    {
                        base_userroles newUserRole = new base_userroles()
                        {
                            userid = newUser.id,
                            roleid = roleID
                        };
                        db.base_userroles.Add(newUserRole);
                    }

                    db.SaveChanges();
                }
            }
        }

        public void EditUserLoginPwd(User user)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_users newUser = db.base_users.Find(user.ID);

                if (newUser != null)
                {
                    newUser.loginpwd = user.LoginPwd;
                    newUser.updatedtime = user.UpdatedTime;

                    db.SaveChanges();
                }
            }
        }


        public void DeleteUser(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                base_users user = db.base_users.Where(t => t.id == id).SingleOrDefault();

                if (user != null)
                {
                    //删除现有角色
                    List<base_userroles> userRoleList = db.base_userroles.Where(t => t.userid == id).ToList();

                    if (userRoleList.Count > 0)
                    {
                        foreach (var userRole in userRoleList)
                        {
                            db.base_userroles.Remove(userRole);
                        }
                    }

                    //删除角色
                    db.base_users.Remove(user);
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        /// 展示平台显示人员详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserModel GetUserModelByID(int id)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UserModel> userList = from user in db.base_users
                                                 join aa in db.base_units on user.unitid equals aa.id into temp1
                                                 from unit in temp1.DefaultIfEmpty()
                                                 join bb in db.sys_regions on user.regionid equals bb.regionid into temp2
                                                 from region in temp2.DefaultIfEmpty()
                                                 select new UserModel
                                                 {
                                                     ID = user.id,
                                                     Code = user.code,
                                                     DisplayName = user.displayname,
                                                     LoginPwd = user.loginpwd,
                                                     UnitID = user.unitid,
                                                     UserTypeID = user.usertypeid,
                                                     UnitName = unit.name,
                                                     Avatar = user.avatar,
                                                     Regionid = user.regionid,
                                                     RegionidName = region.regionname,
                                                     MapElementDeviceType = user.mapelementdevicetype,
                                                     Sex = user.sex,
                                                     Birthday = user.birthday,
                                                     Address = user.address,
                                                     Mobile = user.mobile,
                                                     Telephone = user.telephone,
                                                     Email = user.email,
                                                     IsOnline = user.isonline,

                                                 };
                UserModel UserModel = userList.SingleOrDefault(t => t.ID == id);
                return UserModel;
            }
        }


        /// <summary>
        /// 获取列表的总页数
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public int GetUsersListCount(string name, int limit)
        {
            PageModel model = new PageModel();
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<base_users> lists = db.base_users;
                if (!string.IsNullOrEmpty(name))
                {
                    lists = lists.Where(a => a.displayname.Contains(name));
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


        public List<UserModel> GetUsersList(string name, int start, int limit)
        {
            List<UserModel> list = new List<UserModel>();

            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<UserModel> queryable =
                     from a in db.base_users
                     join b_join in db.base_usertypes on a.usertypeid equals b_join.id into bTemp
                     from b in bTemp.DefaultIfEmpty()
                     join c_join in db.base_units on a.unitid equals c_join.id into cTemp
                     from c in cTemp.DefaultIfEmpty()
                     orderby a.updatedtime descending
                     select new UserModel()
                     {
                         ID = a.id,
                         Code = a.code,
                         DisplayName = a.displayname,
                         UserTypeID = a.usertypeid,
                         UserTypeName = b == null ? "" : b.name,
                         UnitID = a.unitid,
                         UnitName = c == null ? "" : c.name,
                         LoginName = a.loginname,
                         CreatedTime = a.createdtime,
                         UpdatedTime = a.updatedtime,
                         IsOnline = a.isonline
                     };

                if (!string.IsNullOrEmpty(name))
                {
                    queryable = queryable.Where(a => a.DisplayName.Contains(name));
                }

                queryable = queryable.OrderByDescending(a => a.IsOnline).Skip((start - 1) * limit).Take(limit);

                list = queryable.ToList();
            }

            return list;
        }

        public int GetUsersListCount(string name)
        {
            using (hzwEntities db = new hzwEntities())
            {
                IQueryable<base_users> queryable = db.base_users.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    queryable = queryable.Where(a => a.displayname.Contains(name));
                }

                return queryable.Count();
            }
        }

        /// <summary>
        /// 返回同步人员信息
        /// </summary>
        /// <returns></returns>
        public List<usertableType> getall()
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<usertableType> list = new List<usertableType>();
                //信息采集员
                int cjuser = (int)userType.cjuser;
                //处置人员
                int czuser = (int)userType.czuser;
                //考聘人员
                int kpuser = (int)userType.kpuser;
                //执法人员
                int zfuser = (int)userType.zfuser;

                int cjusercount = db.fi_tcpatrols.Where(t => t.patroltypeid == cjuser).Count();
                int czusercount = db.fi_tcpatrols.Where(t => t.patroltypeid == czuser).Count();
                int kpusercount = db.fi_tcpatrols.Where(t => t.patroltypeid == kpuser).Count();
                int zfusercount = db.fi_tcpatrols.Where(t => t.patroltypeid == zfuser).Count();

                usertableType cjuserusertype = new usertableType();
                cjuserusertype.name = "信息采集员";
                cjuserusertype.value = cjusercount;
                list.Add(cjuserusertype);

                usertableType czuserusertype = new usertableType();
                czuserusertype.name = "处置人员";
                czuserusertype.value = czusercount;
                list.Add(czuserusertype);

                usertableType kpuserusertype = new usertableType();
                kpuserusertype.name = "考聘人员";
                kpuserusertype.value = kpusercount;
                list.Add(kpuserusertype);

                usertableType zfuseruserusertype = new usertableType();
                zfuseruserusertype.name = "执法人员";
                zfuseruserusertype.value = zfusercount;
                list.Add(zfuseruserusertype);

                return list;
            }
        }

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <returns></returns>
        public List<rytable> getalluser(string filter, int pageIndex, int pageSize)
        {
            using (hzwEntities db = new hzwEntities())
            {
                string sql = @"SELECT a.humanid,a.humanname,b.IS_ONLINE patrolstateid,a.humancode,a.telmobile,a.photourl 
from fi_tchumans as a 
left join fi_tcpatrols b on a.humanid=b.patrolid";
                List<rytable> list = new List<rytable>();
                if (!string.IsNullOrEmpty(filter))
                {
                    list = db.Database.SqlQuery<fi_tchumansleft>(sql).Select(t => new rytable
                    {
                        useId = t.humanid,
                        userName = t.humanname,
                        Status = t.patrolstateid,
                        usercode = t.humancode,
                        phone = t.telmobile,
                        photourl = t.photourl
                    }).Where(t => t.userName.Contains(filter)).OrderBy(t => t.useId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    list = db.Database.SqlQuery<fi_tchumansleft>(sql).Select(t => new rytable
                   {
                       useId = t.humanid,
                       userName = t.humanname,
                       Status = t.patrolstateid,
                       usercode = t.humancode,
                       phone = t.telmobile,
                       photourl = t.photourl
                   }).OrderBy(t => t.useId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                return list;
            }
        }

        /// <summary>
        /// 获取人员列表页码
        /// </summary>
        /// <returns></returns>
        public int getalluser(string filter, int pageSize)
        {
            using (hzwEntities db = new hzwEntities())
            {
                int count;
                string sql = @"SELECT a.humanid,a.humanname,b.IS_ONLINE patrolstateid,a.humancode,a.telmobile,a.photourl 
from fi_tchumans as a 
left join fi_tcpatrols b on a.humanid=b.patrolid";
                List<rytable> list = new List<rytable>();
                if (!string.IsNullOrEmpty(filter))
                {
                    list = db.Database.SqlQuery<fi_tchumansleft>(sql).Select(t => new rytable
                   {
                       useId = t.humanid,
                       userName = t.humanname,
                       Status = t.patrolstateid,
                       usercode = t.humancode,
                       phone = t.telmobile,
                       photourl = t.photourl
                   }).Where(t => t.userName.Contains(filter)).ToList();
                    count = list.Count();
                }
                else
                {
                    list = db.Database.SqlQuery<fi_tchumansleft>(sql).Select(t => new rytable
                    {
                        useId = t.humanid,
                        userName = t.humanname,
                        Status = t.patrolstateid,
                        usercode = t.humancode,
                        phone = t.telmobile,
                        photourl = t.photourl
                    }).ToList();
                    count = list.Count();
                }
                if (count % 2 == 0)
                {
                    return count / pageSize;
                }
                else
                {
                    return (count / pageSize) + 1;
                }
            }
        }




        /// <summary>
        /// 人员详情
        /// </summary>
        /// <param name="userid">人员Id</param>
        /// <returns></returns>
        public rytable getUserdetalis(int userid)
        {
            using (hzwEntities db = new hzwEntities())
            {
                string sql = string.Format(@"SELECT a.humanid,a.humanname,b.IS_ONLINE patrolstateid,a.humancode,a.telmobile,a.photourl, c.coordinatex,c.coordinatey,c.datainsertdate 
from fi_tchumans as a 
left join fi_tcpatrols b on a.humanid=b.patrolid
LEFT JOIN fi_topatrollatestpos c on a.humanid=c.patrolid
where a.humanid={0}", userid);
                rytable ry = new rytable();
                ry = db.Database.SqlQuery<fi_tchumansleft>(sql).Select(t => new rytable
                {
                    useId = t.humanid,
                    userName = t.humanname,
                    Status = t.patrolstateid,
                    usercode = t.humancode,
                    phone = t.telmobile,
                    photourl = t.photourl,
                    coordinatex = t.coordinatex,
                    coordinatey = t.coordinatey,
                    datainsertdate = t.datainsertdate
                }).FirstOrDefault();
                // rytable ry = list.Single();
                return ry;
            }
        }

        /// <summary>
        /// 获取今日在岗人数
        /// </summary>
        /// <returns></returns>
        public int GetCountUserNowDate()
        {
            using (hzwEntities db = new hzwEntities())
            {
                DateTime dt = DateTime.Now;
                int count = (from pos in db.fi_topatrollatestpos
                             from tcp in db.fi_tcpatrols
                             where pos.patrolid == tcp.patrolid
                            && tcp.is_online == 1
                            && pos.updatetime.Value.Year == dt.Year && pos.updatetime.Value.Month == dt.Month && pos.updatetime.Value.Day == dt.Day
                             select pos
                                 ).Count();
                return count;
            }
        }
    }
}
