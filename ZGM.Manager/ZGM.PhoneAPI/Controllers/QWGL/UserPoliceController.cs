using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model;
using ZGM.Model.PhoneModel;
using ZGM.BLL.QWGLBLLs;

namespace ZGM.PhoneAPI.Controllers.QWGL
{
    public class UserPoliceController : ApiController
    {
        /// <summary>
        /// 查看我的超时报警
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserPoliceModel> GetUserOTPoliceList(PolicePostModel GetData)
        {
            List<UserPoliceModel> list = AlarmBLL.GetOTPoliceListByUserId(GetData);
            return list;
        }

        /// <summary>
        /// 查看我的越界报警
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserPoliceModel> GetUserOBPoliceList(PolicePostModel GetData)
        {
            List<UserPoliceModel> list = AlarmBLL.GetOBPoliceListByUserId(GetData);
            return list;
        }

        /// <summary>
        /// 查看他人超时报警
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserPoliceModel> GetUnitOTPoliceList(PolicePostModel GetData)
        {
            List<UserPoliceModel> list = AlarmBLL.GetOTPoliceListByUnitId(GetData);
            return list;
        }

        /// <summary>
        /// 查看他人越界报警
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<UserPoliceModel> GetUnitOBPoliceList(PolicePostModel GetData)
        {
            List<UserPoliceModel> list = AlarmBLL.GetOBPoliceListByUnitId(GetData);
            return list;
        }

        /// <summary>
        /// 根据报警标识获取报警信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserPoliceModel GetPoliceInfo(PolicePostModel GetData)
        {
            UserPoliceModel model = AlarmBLL.GetPoliceInfoByPoliceId(GetData);
            return model;
        }

        /// <summary>
        /// 报警结果申诉
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public UserPoliceModel PoliceAllege(PolicePostModel GetData)
        {
            UserPoliceModel model = AlarmBLL.PoliceAllegeByPoliceId(GetData);
            return model;
        }

    }
}
