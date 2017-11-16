using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.ConceptualModel;

namespace ZGM.WUA.DAL
{
    public class MapDAL
    {
        ZGMEntities db = new ZGMEntities();

        /// <summary>
        /// 获取周边人员
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumUsers(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            IQueryable<UserModel> users = from ulps in db.QWGL_USERLATESTPOSITIONS
                                          from u in db.SYS_USERS
                                          where ulps.X2000 >= mapEnvelope.XMin
                                          && ulps.X2000 <= mapEnvelope.XMax
                                          && ulps.Y2000 >= mapEnvelope.YMin
                                          && ulps.Y2000 <= mapEnvelope.YMax
                                          && ulps.USERID == u.USERID
                                          && u.ISONLINE == 1
                                          select new UserModel
                                          {
                                              UserId = u.USERID,
                                              UserName = u.USERNAME,
                                              X2000 = ulps.X2000,
                                              Y2000 = ulps.Y2000,
                                              IsOnline = u.ISONLINE
                                          };

            List<MapElementModel> result = new List<MapElementModel>();
            foreach (UserModel user in users)
            {
                result.Add(new MapElementModel()
                {
                    Id = user.UserId.ToString(),
                    Name = user.UserName,
                    Type = "UserModel",
                    Circum = "UserModel,CarModel,TaskModel",
                    X = user.X2000,
                    Y = user.Y2000,
                    IsOnline = user.IsOnline == 0 ? "0" : "1"
                });
            }

            return result;
        }

        /// <summary>
        /// 获取周边车辆
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCars(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            IQueryable<CarModel> cars = from clps in db.QWGL_CARLATESTPOSITIONS
                                        from c in db.QWGL_CARS
                                        where clps.X2000 >= mapEnvelope.XMin
                                          && clps.X2000 <= mapEnvelope.XMax
                                          && clps.Y2000 >= mapEnvelope.YMin
                                          && clps.Y2000 <= mapEnvelope.YMax
                                          && clps.CARID == c.CARID
                                          && c.ISONLINE == 1
                                        select new CarModel
                                        {
                                            CarId = c.CARID,
                                            CarNumber = c.CARNUMBER,
                                            X2000 = clps.X2000,
                                            Y2000 = clps.Y2000
                                        };

            List<MapElementModel> result = new List<MapElementModel>();
            foreach (CarModel car in cars)
            {
                result.Add(new MapElementModel()
                {
                    Id = car.CarId.ToString(),
                    Name = car.CarNumber,
                    Type = "CarModel",
                    Circum = "UserModel,CarModel,TaskModel",
                    X = car.X2000,
                    Y = car.Y2000,
                });
            }

            return result;
        }

        /// <summary>
        /// 获取周边案件
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumTasks(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            IQueryable<MapElementModel> tasks = from t in db.XTGL_ZFSJS
                                                where t.X2000 >= mapEnvelope.XMin
                                                && t.X2000 <= mapEnvelope.XMax
                                                && t.Y2000 >= mapEnvelope.YMin
                                                && t.Y2000 <= mapEnvelope.YMax
                                                && t.FOUNDTIME > DateTime.Today
                                                select new MapElementModel
                                                {
                                                    Id = t.ZFSJID,
                                                    Name = t.EVENTTITLE,
                                                    Type = "TaskModel",
                                                    Circum = "UserModel,CarModel,TaskModel",
                                                    X = t.X2000,
                                                    Y = t.Y2000,
                                                };
            return tasks.ToList();

        }

        /// <summary>
        /// 获取周边监控
        /// </summary>
        /// <param name="mapElement"></param>
        /// <param name="mapEnvelope"></param>
        /// <returns></returns>
        public List<MapElementModel> GetCircumCameras(MapElementModel mapElement, MapElementModel mapEnvelope)
        {
            IQueryable<CameraInfoModel> cameras = from t in db.FI_CAMERA_INFO
                                                  where t.LONGITUDE >= mapEnvelope.XMin
                                                  && t.LONGITUDE <= mapEnvelope.XMax
                                                  && t.LATITUDE >= mapEnvelope.YMin
                                                  && t.LATITUDE <= mapEnvelope.YMax
                                                  select new CameraInfoModel
                                                  {
                                                      CameraId = t.CAMERA_ID,
                                                      CameraName = t.NAME,
                                                      DeviceId = t.DEVICE_ID,
                                                      RegionId = t.REGION_ID,
                                                      IndexCode = t.INDEX_CODE,
                                                      CameraTypeId = t.CAMERA_TYPE,
                                                      CameraTypeName = t.CAMERA_TYPE == 0 ? "枪机" : t.CAMERA_TYPE == 1 ? "半球" : t.CAMERA_TYPE == 2 ? "快球" : "云台",
                                                      X = t.LONGITUDE,
                                                      Y = t.LATITUDE,
                                                      Parameter = t.PARAMETER,
                                                      Scope = t.SCOPE,
                                                      PlayBack = t.PLAYBACK
                                                  };
            List<MapElementModel> result = new List<MapElementModel>();
            foreach (CameraInfoModel item in cameras)
            {
                result.Add(new MapElementModel()
                {
                    Id = item.CameraId.ToString(),
                    Name = item.CameraName,
                    Type = "CameraModel",
                    Circum = "UserModel,CarModel,TaskModel",
                    X = item.X,
                    Y = item.Y,
                    Content = item,
                    Note = item.Parameter
                    //Note = "<?xml version='1.0' encoding='UTF-8'?><Message><Camera><Id>" + item.CameraId
                    //+ "</Id><IndexCode>" + item.IndexCode
                    //+ "</IndexCode><Name>" + item.CameraName
                    //+ "</Name><ChanNo>0</ChanNo><Matrix Code='' Id='0' /></Camera><Dev regtype='0' devtype='10070'><Id>" + item.DeviceId
                    //+ "</Id><IndexCode>" + item.IndexCode
                    //+ "</IndexCode><Addr IP='172.16.7.101' Port='8000' /><Auth User='admin' Pwd='12345' /></Dev><Vag IP='172.16.2.36' Port='7302' /><Voice><Encode>2</Encode></Voice><Media Protocol='0' Stream='1'><Vtdu IP='172.16.2.37' Port='554' /></Media><Privilege Priority='50' Code='7' /><Option><Talk>1</Talk><PreviewType>0</PreviewType></Option></Message>"
                });
            }

            return result.ToList();
        }
    }
}
