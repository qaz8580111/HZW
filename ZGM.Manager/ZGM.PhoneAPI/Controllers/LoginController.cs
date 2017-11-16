using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.BLL.UserBLLs;
using ZGM.Model.PhoneModel;
using ZGM.Model.CustomModels;
using Common.Enums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model;
using ZGM.BLL.PhoneBLLs;

namespace ZGM.PhoneAPI.Controllers
{
    public class LoginController : ApiController
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserLoginModel UserLogin(UserLoginPostModel GetData)
        {
            UserInfo model = new UserInfo();
            UserLoginModel user = new UserLoginModel();
            //登录校验
            if (!string.IsNullOrEmpty(GetData.Account) && !string.IsNullOrEmpty(GetData.PassWord))
                model = UserBLL.Login(GetData.Account.ToUpper(), GetData.PassWord);

            user.ERRORTYPE = (decimal)PhoneErrorEnum.ErrorTypeEnum.Normal;
            //用户名不存在或密码错误
            if (model == null)
                user.ERRORTYPE = (decimal)PhoneErrorEnum.ErrorTypeEnum.UserOrPassWord;
            //手机时间格式不正确
            DateTime tempTime = DateTime.Now;
            DateTime dt = DateTime.Parse(GetData.PhoneTime);
            if (!DateTime.TryParse(GetData.PhoneTime, out tempTime))
            {
                user.ERRORTYPE = (decimal)PhoneErrorEnum.ErrorTypeEnum.PhoneTimeType;
            }

            //登录成功后修改数据
            if (model != null && user.ERRORTYPE == 0)
            {
                UserLatestPositionBLL.UpdateLastPosition(model.UserID, GetData.IMEICode);
                SystemPhoneBLL.UpdateOnline(model.UserID,1);
            }
            user.USERID = model.UserID;
            user.USERNAME = model.UserName;
            user.UNITID = model.UnitID;
            user.UNITNAME = model.UnitName;
           
            if (SystemPhoneBLL.GetPhoneUserRoles(model.UserID) != null)
            {
                user.USERROLEID = SystemPhoneBLL.GetPhoneUserRoles(model.UserID).USERROLEID;
                user.USERROLENAME = SystemPhoneBLL.GetPhoneUserRoles(model.UserID).USERROLENAME;
            }
            user.ACCOUNT = GetData.Account;
            user.PASSWORD = GetData.PassWord;
            user.PHONE = model.Phone;
            user.PHONEIMEI = GetData.IMEICode;
            user.AVATAR = model.Avatar;
            user.LASTLOGINTIME = DateTime.Now;
            user.USERPOSITIONID = model.PositionID;
            user.USERPOSITIONNAME = model.PositionName;

            return user;
        }

        /// <summary>
        /// 检测版本更新
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserLoginModel CheckUpdate(UserLoginPostModel GetData)
        {
            UserLoginModel umodel = new UserLoginModel();
            APP_VERSIONS amodel = SystemPhoneBLL.GetPhoneVersion();
            if (GetData.Vision == amodel.VERSIONNAME)
                umodel.ERRORTYPE = (decimal)PhoneErrorEnum.ErrorTypeEnum.Normal;
            else
            {
                umodel.ERRORTYPE = (decimal)PhoneErrorEnum.ErrorTypeEnum.Vision;
                umodel.VISION = amodel.VERSIONNAME;
                umodel.VISIONURL = amodel.VERSIONURL;
                umodel.VISIONSIZE = amodel.VERSIONSIZE;
            }

            return umodel;
        }
        /// <summary>
        /// 修改密码，验证原始密码
        /// </summary>
        /// <param name="EditModel"></param>
        /// <returns>false原始密码错误true修改成功</returns>
        public bool ModifyUserPassword(UserPassWordEdit EditModel)
        {
            return UserBLL.ModifyUserPassword(EditModel.OLDPassword, EditModel.NEWPassword, EditModel.userId);
        }

        /// <summary>
        /// 退出修改人员离线状态
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void UpdateOnline(UserPassWordEdit GetData)
        {
            SystemPhoneBLL.UpdateOnline(GetData.userId, 0);
        }

    }
}
