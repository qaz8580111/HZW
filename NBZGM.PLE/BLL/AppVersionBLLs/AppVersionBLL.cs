using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taizhou.PLE.Model;
using Taizhou.PLE.Model.WebServiceModels;

namespace Taizhou.PLE.BLL.AppVersionBLLs
{
    public class AppVersionBLL
    {
        /// <summary>
        /// 获取最新应用版本对象
        /// </summary>
        /// <returns>最新应用版本对象</returns>
        public static AppVersion GetMaxAppVersion()
        {
            PLEEntities db = new PLEEntities();

            decimal versionCode = db.APPVERSIONS.Max(t => t.VERSIONCODE);

            AppVersion appVersion = (from app in db.APPVERSIONS
                                     where app.VERSIONCODE == versionCode
                                     select new AppVersion
                                     {
                                         versionCode = (int)app.VERSIONCODE,
                                         versionName = app.VERSIONNAME,
                                         versionURL = app.VERSIONURL
                                     }).SingleOrDefault();

            return appVersion;
        }
    }
}
