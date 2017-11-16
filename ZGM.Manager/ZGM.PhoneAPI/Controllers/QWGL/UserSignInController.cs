using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model;
using ZGM.BLL.QWGLBLLs;
using ZGM.Common.Enums;
using ZGM.Model.PhoneModel;
using ZGM.Model.ViewModels;
using Common;

namespace ZGM.PhoneAPI.Controllers
{
    public class UserSignInController : ApiController
    {
        /// <summary>
        /// 判断用户是否在签到区域内
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserSignInModel GetUserSignInPage(UserSignInPostModel GetData)
        {
            List<SignInAreaPartModel> signinlist = UserSignInBLL.GetAreaGeometry(GetData.UserId);
            MapPoint geometry = new MapPoint();
            UserSignInModel backmodel = new UserSignInModel();
            geometry.X = double.Parse(GetData.Longitude);
            geometry.Y = double.Parse(GetData.Latitude);

            //string geo84 = double.Parse(GetData.Longitude) + "," + double.Parse(GetData.Latitude);
            //string geo2000 = MapXYConvent.WGS84ToCGCS2000(geo84);
            //string[] split = geo2000.Split(';');
            //if (!string.IsNullOrEmpty(split[0]))
            //{
            //    geometry.X = double.Parse(split[0].Split(',')[0]);
            //    geometry.Y = double.Parse(split[0].Split(',')[1]);
            //}

            foreach (var item in signinlist)
            {
                backmodel.AREAID = item.AREAID;
                backmodel.GEOMETRY = item.GEOMETRY;
                backmodel.AREANAME = item.AREANAME;
                backmodel.AREADESCRIPTION = item.AREADESCRIPTION;
            }
            if (!string.IsNullOrEmpty(backmodel.GEOMETRY))
            {
                string[] splitmap = backmodel.GEOMETRY.Split(';');
                List<MapPoint> listmp = new List<MapPoint>();
                for (int i = 0; i < splitmap.Length; i++)
                {
                    MapPoint mp = new MapPoint();
                    mp.X = double.Parse(splitmap[i].Split(',')[0]);
                    mp.Y = double.Parse(splitmap[i].Split(',')[1]);
                    listmp.Add(mp);
                }
                backmodel.SIGNINCOUNT = UserSignInBLL.GetUserSignInCount(GetData.UserId);

                //用户是否在签到区域内
                if (UserSignInBLL.PointInFences(geometry, listmp))
                    backmodel.ISINAREA = true;
                else
                    backmodel.ISINAREA = false;
            }
            return backmodel;
        }

        /// <summary>
        /// 队员签到
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public SignInAreaPartModel UserSignIn(UserSignInPostModel GetData)
        {
            QWGL_USERSIGNINS model = new QWGL_USERSIGNINS();
            SignInAreaPartModel pmodel = new SignInAreaPartModel();
            //MapPoint geometry = UserSignInBLL.Geo84ToGeo2000(GetData.Longitude,GetData.Latitude);
            model.USERID = GetData.UserId;
            model.SIGNINTIME = DateTime.Now;
            //model.X84 = decimal.Parse(GetData.Longitude);
            //model.Y84 = decimal.Parse(GetData.Latitude);
            model.X2000 = decimal.Parse(GetData.Longitude);
            model.Y2000 = decimal.Parse(GetData.Latitude);

            //修改签到状态
            int updatestatu = UserSignInBLL.UpdateSignInStatu(GetData.UserId);

            //添加队员签到表
            int result = UserSignInBLL.AddUserSignIn(model);
            if (result > 0 && updatestatu > 0)
                pmodel.ISSIGNIN = true;
            else
                pmodel.ISSIGNIN = false;

            return pmodel;
        }

        /// <summary>
        /// 查看我的签到
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserSignInModel> GetUserSignIn(UserSignInPostModel GetData)
        {
            List<UserSignInModel> list = UserSignInBLL.GetSignInListById(GetData.UserId, GetData.PageIndex, GetData.QueryUserName);
            return list;
        }

        /// <summary>
        /// 查看他人签到
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserSignInModel> GetOtherSignIn(UserSignInPostModel GetData)
        {
            List<UserSignInModel> list = UserSignInBLL.GetAllUserSignIn(GetData.UserId,GetData.UnitId, GetData.PageIndex, GetData.QueryUserName);
            return list;
        }

        /// <summary>
        /// 查看团队签到
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserSignInModel> GetUnitSignIn(UserSignInPostModel GetData)
        {
            List<UserSignInModel> list = UserSignInBLL.GetSignInListByUnitId(GetData.UnitId, GetData.PageIndex, GetData.QueryUserName);
            return list;
        }

        /// <summary>
        /// 查看团队未签到
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserSignInModel> GetUnitUnSignIn(UserSignInPostModel GetData)
        {
            List<UserSignInModel> list = UserSignInBLL.GetUnSignInListByUnitId(GetData.UnitId, GetData.PageIndex, GetData.QueryUserName);
            return list;
        }

        /// <summary>
        /// 根据签到标识获取签到信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserSignInModel GetExamineInfoByExamineId(UserSignInPostModel GetData)
        {
            UserSignInModel model = UserSignInBLL.GetSignInInfoByUserId(GetData.SGID);
            model.SIGNINDATE = ((DateTime)model.SIGNINALL).ToString("yyyy-MM-dd");
            model.PLANSIGNINTIMESTR = ((DateTime)model.PLANSIGNINTIME).ToString("HH:mm:ss");
            model.PLANSIGNOUTTIMESTR = ((DateTime)model.PLANSIGNOUTTIME).ToString("HH:mm:ss");
            model.ACSIGNINTIMESTR = model.ACSIGNINTIME == null ? "" : ((DateTime)model.ACSIGNINTIME).ToString("HH:mm:ss");
            model.ACSIGNOUTTIMESTR = model.ACSIGNOUTTIME == null ? "" : ((DateTime)model.ACSIGNOUTTIME).ToString("HH:mm:ss");
            return model;
        }

        /// <summary>
        /// 根据签到任务标识获取签到信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserSignInModel GetSignInInfoByTaskId(UserSignInPostModel GetData)
        {
            UserSignInModel model = UserSignInBLL.GetSignInInfoByTaskId(GetData.SGID);
            model.SIGNINDATE = ((DateTime)model.SIGNINALL).ToString("yyyy-MM-dd");
            model.PLANSIGNINTIMESTR = ((DateTime)model.PLANSIGNINTIME).ToString("HH:mm:ss");
            model.PLANSIGNOUTTIMESTR = ((DateTime)model.PLANSIGNOUTTIME).ToString("HH:mm:ss");
            model.ACSIGNINTIMESTR = model.ACSIGNINTIME == null ? "" : ((DateTime)model.ACSIGNINTIME).ToString("HH:mm:ss");
            model.ACSIGNOUTTIMESTR = model.ACSIGNOUTTIME == null ? "" : ((DateTime)model.ACSIGNOUTTIME).ToString("HH:mm:ss");
            return model;
        }

    }
}
