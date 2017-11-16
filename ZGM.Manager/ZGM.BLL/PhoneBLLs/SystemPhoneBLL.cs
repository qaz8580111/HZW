using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGM.Model;
using ZGM.Model.PhoneModel;

namespace ZGM.BLL.PhoneBLLs
{
    public class SystemPhoneBLL
    {
        /// <summary>
        /// 获取手机版本
        /// </summary>
        /// <returns></returns>
        public static APP_VERSIONS GetPhoneVersion()
        {
            Entities db = new Entities();
            APP_VERSIONS model = db.APP_VERSIONS.FirstOrDefault(t => t.VERSIONCODE == 1);
            return model;
        }

        /// <summary>
        /// 获取用户角色ID
        /// </summary>
        /// <returns></returns>
        public static PhoneRoleModel GetPhoneUserRoles(decimal UserId)
        {
            Entities db = new Entities();
            List<PhoneRoleModel> list = (from r in db.SYS_ROLES
                                      join ur in db.SYS_USERROLES
                                      on r.ROLEID equals (decimal)ur.ROLEID
                                      where ur.USERID == UserId
                                           select new PhoneRoleModel
                                      {
                                          ROLEID = r.ROLEID,
                                          UserId = (decimal)ur.USERID,
                                          ROLENAME = r.ROLENAME,
                                          DESCRIPTION = r.DESCRIPTION,                                          
                                      }).ToList();
            PhoneRoleModel model = new PhoneRoleModel();
            model.USERROLEID = "\\";
            foreach (PhoneRoleModel item in list)
            {
                model.USERROLEID += item.ROLEID + "\\";
                model.USERROLENAME += item.ROLENAME + ",";
            }
            return model;
        }

        /// <summary>
        /// 获取首页轮播图片信息
        /// </summary>
        /// <returns></returns>
        public static List<SYS_PHOTECAROUSELS> GetIndexPicturesInfo()
        {
            Entities db = new Entities();
            List<SYS_PHOTECAROUSELS> list = db.SYS_PHOTECAROUSELS.Where(t => t.STATUS == 1).ToList();
            return list;
        }

        /// <summary>
        /// 登录修改人员在线状态
        /// </summary>
        /// <returns></returns>
        public static void UpdateOnline(decimal UserId,decimal StyleNum)
        {
            Entities db = new Entities();
            SYS_USERS model = db.SYS_USERS.FirstOrDefault(t => t.USERID == UserId);
            if (StyleNum == 1)
                model.ISONLINE = 1;
            else
                model.ISONLINE = 0;
            db.SaveChanges();
        }

    }
}
