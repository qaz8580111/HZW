using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HZW.ZHCG.Model;
using HZW.ZHCG.DAL.Enum;

namespace HZW.ZHCG.DAL
{
    public class CameraDAL
    {
        hzwEntities db = new hzwEntities();
        #region 同步
        /// <summary>
        /// 添加监控控制中心
        /// 返回值：1成功，0失败
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public int AddCameraUnit(CameraUnitModel unit)
        {
            db.fi_camera_unit.Add(new fi_camera_unit
            {
                unitid = unit.UnitId,
                unitname = unit.UnitName,
                parentid = unit.ParentId
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加监控区域
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public int AddCameraRegion(CameraRegionModel region)
        {
            db.fi_camera_regions.Add(new fi_camera_regions
            {
                regionid = region.RegionId,
                regionname = region.RegionName,
                unitid = region.UnitId
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// <summary>
        /// 添加监控
        /// 返回值：1成功，0失败
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public int AddCameraInfo(CameraInfoModel camera)
        {
            db.fi_camera_info.Add(new fi_camera_info
            {
                camera_id = camera.CameraId,
                name = camera.CameraName,
                device_id = camera.DeviceId,
                region_id = camera.RegionId,
                index_code = camera.IndexCode,
                camera_type = camera.CameraTypeId,
                parameter = camera.Parameter,
                playback = camera.PlayBack,
                longitude = camera.X,
                latitude = camera.Y
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        /// <summary>
        /// 添加监控设备
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public int AddCameraDev(CameraDevModel device)
        {
            db.fi_camera_dev.Add(new fi_camera_dev
            {
                deviceid = device.DeviceId,
                indexcode = device.IndexCode,
                typecode = device.TypeCode,
                name = device.Name
            });
            try
            {
                int result = db.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion
        #region api
        /// <summary>
        /// 获取监控部门
        /// 参数,-1：全部；null：Control层
        /// </summary>
        /// <returns></returns>
        public List<CameraUnitModel> GetCameraUnits(decimal? unitId)
        {
            IQueryable<fi_camera_unit> units = db.fi_camera_unit;
            if (unitId == null)
                units = units.Where(t => t.parentid == (decimal?)null);
            if (unitId != -1 && unitId != null)
                units = units.Where(t => t.parentid == unitId);

            IQueryable<CameraUnitModel> result = units
                .Select(t => new CameraUnitModel
                {
                    UnitId = t.unitid,
                    UnitName = t.unitname,
                    ParentId = t.parentid
                });
            return result.ToList();
        }

        /// <summary>
        /// 获取区域
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public IQueryable<CameraRegionModel> GetCameraRegions(decimal? unitId)
        {
            IQueryable<CameraRegionModel> result = db.fi_camera_regions
                .Where(t => t.unitid == unitId)
                .Select(t => new CameraRegionModel
                {
                    RegionId = t.regionid,
                    RegionName = t.regionname,
                    UnitId = t.unitid
                });
            return result;
        }
        /// <summary>
        /// 根据部门标识和监控名称获取监控
        /// 参数可选
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<CameraInfoModel> GetCameraInfos(decimal? unitId, string cameraName)
        {
            IQueryable<fi_camera_info> cameras = db.fi_camera_info;
            if (unitId != null)
                cameras = cameras.Where(t => t.region_id == unitId);
            if (!string.IsNullOrEmpty(cameraName))
                cameras = cameras.Where(t => t.name.Contains(cameraName));

            IQueryable<CameraInfoModel> result = cameras
                .Select(t => new CameraInfoModel
                {
                    CameraId = t.camera_id,
                    CameraName = t.name,
                    DeviceId = t.device_id,
                    RegionId = t.region_id,
                    IndexCode = t.index_code,
                    CameraTypeId = t.camera_type,
                    CameraTypeName = t.camera_type == 0 ? "枪机" : t.camera_type == 1 ? "半球" : t.camera_type == 2 ? "快球" : "云台",
                    X = t.longitude,
                    Y = t.latitude,
                    Scope = t.scope,
                    PlayBack = t.playback,
                    Parameter = t.parameter
                });
            return result.ToList();
        }
        /// <summary>
        /// 根据监控标识获取监控
        /// </summary>
        /// <param name="cameraid"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfo(decimal cameraid)
        {
            IQueryable<CameraInfoModel> cameras = db.fi_camera_info
                .Where(t => t.camera_id == cameraid)
                .Select(t => new CameraInfoModel
                {
                    CameraId = t.camera_id,
                    CameraName = t.name,
                    DeviceId = t.device_id,
                    RegionId = t.region_id,
                    IndexCode = t.index_code,
                    CameraTypeId = t.camera_type,
                    CameraTypeName = t.camera_type == 0 ? "枪机" : t.camera_type == 1 ? "半球" : t.camera_type == 2 ? "快球" : "云台",
                    X = t.longitude,
                    Y = t.latitude,
                    Parameter = t.parameter,
                    PlayBack = t.playback,
                    Scope = t.scope
                });
            if (cameras.Count() == 1)
                return cameras.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 根据专题标识获取监控信息
        /// </summary>
        /// <param name="tchId"></param>
        /// <returns></returns>
        public CameraInfoModel GetCameraInfoByTHCId(decimal tchId)
        {
            IQueryable<CameraInfoModel> cameras = from t in db.fi_camera_info
                                                  from h in db.fi_camera_item
                                                  where h.thcid == tchId
                                                  && h.cameraid == t.camera_id
                                                  select new CameraInfoModel
                                                  {
                                                      CameraId = t.camera_id,
                                                      CameraName = t.name,
                                                      DeviceId = t.device_id,
                                                      RegionId = t.region_id,
                                                      IndexCode = t.index_code,
                                                      CameraTypeId = t.camera_type,
                                                      CameraTypeName = t.camera_type == 0 ? "枪机" : t.camera_type == 1 ? "半球" : t.camera_type == 2 ? "快球" : "云台",
                                                      X = t.longitude,
                                                      Y = t.latitude,
                                                      Parameter = t.parameter,
                                                      PlayBack = t.playback,
                                                      Scope = t.scope
                                                  };
            if (cameras.Count() == 1)
                return cameras.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 获取监控专题
        /// P.S.:null为一级目录，-1为全部结构
        /// </summary>
        /// <param name="themeid"></param>
        /// <returns></returns>
        public IQueryable<CameraThemeModel> GetCameraThemes(decimal? themeid)
        {
            IQueryable<fi_camera_theme> themes = db.fi_camera_theme;
            if (themeid == null)
                themes = themes.Where(t => t.parentid == (decimal?)null);
            if (themeid != -1 && themeid != null)
                themes = themes.Where(t => t.parentid == themeid);

            IQueryable<CameraThemeModel> result = themes
                .Select(t => new CameraThemeModel
                {
                    ThemeId = t.themeid,
                    CameraName = t.name,
                    ParentId = t.parentid,
                    Isline = t.isline,
                    Note = t.note
                });
            return result;
        }
        /// <summary>
        /// 获取专题对象
        /// </summary>
        /// <param name="themeid"></param>
        /// <param name="cameraName"></param>
        /// <returns></returns>
        public IQueryable<CameraThemeItemModel> GetCameraThemeItems(decimal? themeid, string cameraName)
        {
            IQueryable<fi_camera_item> cameras = db.fi_camera_item;
            if (themeid != null)
                cameras = cameras.Where(t => t.themeid == themeid);
            if (!string.IsNullOrEmpty(cameraName))
                cameras = cameras.Where(t => t.name.Contains(cameraName));
            cameras = cameras.OrderBy(t => t.sortnum);

            IQueryable<CameraThemeItemModel> result = cameras
                .Select(t => new CameraThemeItemModel
                {
                    THCId = t.thcid,
                    ThemeId = t.themeid,
                    SortNum = t.sortnum,
                    CameraId = t.cameraid,
                    CameraName = t.name,
                    IndexCode = t.index_code,
                    CameraType = t.camera_type,
                    CameraTypeName = t.camera_type == 0 ? "枪机" : t.camera_type == 1 ? "半球" : t.camera_type == 2 ? "快球" : "云台",
                    X = t.longitude,
                    Y = t.latitude
                });
            return result;
        }
        /// <summary>
        /// 获取专题元素
        /// </summary>
        /// <param name="cameraThemeItemId"></param>
        /// <returns></returns>
        public CameraThemeItemModel GetCameraThemeItem(decimal cameraThemeItemId)
        {
            IQueryable<CameraThemeItemModel> result = db.fi_camera_item
                .Where(t => t.thcid == cameraThemeItemId)
                .Select(t => new CameraThemeItemModel
                {
                    THCId = t.thcid,
                    ThemeId = t.themeid,
                    SortNum = t.sortnum,
                    CameraId = t.cameraid,
                    CameraName = t.name,
                    IndexCode = t.index_code,
                    CameraType = t.camera_type,
                    CameraTypeName = t.camera_type == 0 ? "枪机" : t.camera_type == 1 ? "半球" : t.camera_type == 2 ? "快球" : "云台",
                    X = t.longitude,
                    Y = t.latitude
                });
            if (result.Count() == 1)
                return result.SingleOrDefault();
            return null;
        }

        /// <summary>
        /// 获取监控数量
        /// </summary>
        /// <returns></returns>
        public int GetCamerasCount()
        {
            int count = db.fi_camera_info.Count();
            return count;
        }
        #endregion


        /// <summary>
        /// 获取监控分页列表
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页索引</param>
        /// <returns>返回数据源</returns>
        public IQueryable<fi_camera_info> getcameraList(int pagesize, int pageindex)
        {
            //类型关联查询
            //IQueryable<ficamerainfo> query = (from camerainfo in db.fi_camera_info
            //                                 join cameradev in db.fi_camera_dev
            //                                 on camerainfo.index_code equals cameradev.indexcode
            //                                 select new ficamerainfo 
            //                                 {
            //                                     camera_id = camerainfo.camera_id,
            //                                     typecode = cameradev.typecode,
            //                                     name = camerainfo.name,
            //                                     status = camerainfo.status,
            //                                     longitude = camerainfo.longitude,
            //                                     latitude = camerainfo.latitude,
            //                                     parameter = camerainfo.parameter
            //                                 });

            //return query.OrderBy(t => t.camera_id).Skip((pageindex - 1) * pagesize).Take(pagesize);

            IQueryable<fi_camera_info> query = db.fi_camera_info.OrderBy(t => t.camera_id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return query;
        }

        /// <summary>
        /// 获取监控分页筛选列表
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="filtrateName">筛选字符</param>
        /// <returns>返回数据源</returns>
        public IQueryable<fi_camera_info> getcameraList(int pagesize, int pageindex, string filtrateName)
        {
            //类型关联查询
            //IQueryable<ficamerainfo> query = (from camerainfo in db.fi_camera_info
            //                                  join cameradev in db.fi_camera_dev
            //                                  on camerainfo.index_code equals cameradev.indexcode
            //                                  where camerainfo.name.Contains(filtrateName)
            //                                  select new ficamerainfo
            //                                  {
            //                                      camera_id = camerainfo.camera_id,
            //                                      typecode = cameradev.typecode,
            //                                      name = camerainfo.name,
            //                                      status = camerainfo.status,
            //                                      longitude = camerainfo.longitude,
            //                                      latitude = camerainfo.latitude,
            //                                      parameter = camerainfo.parameter
            //                                  });
            //return query.OrderBy(t => t.camera_id).Skip(pagesize * (pageindex - 1)).Take(pagesize);
            IQueryable<fi_camera_info> query;
            if (filtrateName != "null")
            {
                query = db.fi_camera_info
                  .OrderBy(t => t.camera_id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            }
            else
            {
                query = db.fi_camera_info
                  .Where(t => t.name.Contains(filtrateName))
                  .OrderBy(t => t.camera_id).Skip((pageindex - 1) * pagesize).Take(pagesize);
            }
            return query;
        }

        /// <summary>
        /// 获取监控详情
        /// </summary>
        /// <param name="id">主键唯一标识</param>
        /// <returns></returns>
        public fi_camera_info getdetails(int id)
        {
            fi_camera_info entity = db.fi_camera_info.Find(id);
            return entity;
        }


        /// <summary>
        /// 获取页总数
        /// </summary>
        /// <param name="pagesize">页大小</param>
        /// <param name="filtrateName">筛选字符</param>
        /// <returns>返回页码</returns>
        public int getcameraListPageIndex(int pagesize, string filtrateName)
        {
            int pageindex = 0;
            IQueryable<fi_camera_info> query = null;
            if (filtrateName != "null" && filtrateName != null)
            {
                //类型关联查询
                //query = (from camerainfo in db.fi_camera_info
                //         join cameradev in db.fi_camera_dev
                //         on camerainfo.index_code equals cameradev.indexcode
                //         where camerainfo.name.Contains(filtrateName)
                //         select new ficamerainfo
                //         {
                //             camera_id = camerainfo.camera_id,
                //             typecode = cameradev.typecode,
                //             name = camerainfo.name,
                //             status = camerainfo.status,
                //             longitude = camerainfo.longitude,
                //             latitude = camerainfo.latitude,
                //             parameter = camerainfo.parameter
                //         });

                query = db.fi_camera_info.Where(t => t.name.Contains(filtrateName));
            }
            else
            {
                //类型关联查询
                //query = (from camerainfo in db.fi_camera_info
                //         join cameradev in db.fi_camera_dev
                //         on camerainfo.index_code equals cameradev.indexcode
                //         select new ficamerainfo
                //         {
                //             camera_id = camerainfo.camera_id,
                //             typecode = cameradev.typecode,
                //             name = camerainfo.name,
                //             status = camerainfo.status,
                //             longitude = camerainfo.longitude,
                //             latitude = camerainfo.latitude,
                //             parameter = camerainfo.parameter
                //         });

                query = db.fi_camera_info;
            }

            int allsize = query.Count();
            if (allsize % pagesize == 0)
            {
                pageindex = allsize / pagesize;
            }
            else
            {
                pageindex = (allsize / pagesize) + 1;
            }
            return pageindex;
        }


        /// <summary>
        /// 获取监控树
        /// </summary>
        /// <returns></returns>
        public List<cameratree> getListTree(string name)
        {
            //所有的树节点
            List<cameratree> allnodes = new List<cameratree>();
            List<cameratree> midelist = new List<cameratree>();
            List<fi_camera_unit> unitlist = getCameraunit();
            foreach (fi_camera_unit unit in unitlist)
            {
                cameratree firstnode = new cameratree();
                firstnode.id = unit.unitid;
                firstnode.text = unit.unitname;
                firstnode.parentId = unit.parentid;
                firstnode.cameraId = null;
                List<fi_camera_regions> regionslist = getficameraByunitId(unit.unitid);
                foreach (fi_camera_regions region in regionslist)
                {
                    cameratree mide = new cameratree();
                    mide.id = region.unitid;
                    mide.text = region.regionname;
                    mide.parentId = region.regionid;
                    mide.cameraId = null;
                    midelist.Add(mide);
                    firstnode.nodes = midelist;
                    List<fi_camera_info> infolist = getChildNodes(region.regionid);
                    if (string.IsNullOrEmpty(name))
                    {
                        List<cameratree> lastlist = new List<cameratree>();
                        foreach (fi_camera_info info in infolist)
                        {
                            cameratree last = new cameratree();
                            last.id = info.camera_id;
                            last.parentId = info.region_id;
                            last.text = info.name;
                            last.cameraId = info.camera_id;
                            lastlist.Add(last);
                            mide.nodes = lastlist;
                        }
                    }
                    else
                    {
                        List<fi_camera_info> screellist = infolist.Where(t => t.name.Contains(name)).ToList();

                        List<cameratree> lastlist = new List<cameratree>();
                        foreach (fi_camera_info info in screellist)
                        {
                            cameratree last = new cameratree();
                            last.id = info.camera_id;
                            last.parentId = info.region_id;
                            last.text = info.name;
                            last.cameraId = info.camera_id;
                            lastlist.Add(last);
                            mide.nodes = lastlist;
                        }
                    }
                }
                allnodes.Add(firstnode);
            }
            return allnodes;
        }

        /// <summary>
        /// 获取监控父级
        /// </summary>
        /// <returns></returns>
        public List<fi_camera_unit> getCameraunit()
        {
            List<fi_camera_unit> list = db.fi_camera_unit.ToList();
            return list;
        }

        /// <summary>
        /// 获取监控子级别上一级
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<fi_camera_regions> getficameraByunitId(int? unitId)
        {
            List<fi_camera_regions> list = new List<fi_camera_regions>();
            list = db.fi_camera_regions.Where(t => t.unitid == unitId).ToList();
            return list;
        }

        /// <summary>
        /// 获取监控子级
        /// </summary>
        /// <param name="reginId"></param>
        /// <returns></returns>
        public List<fi_camera_info> getChildNodes(int? reginId)
        {
            List<fi_camera_info> list = new List<fi_camera_info>();
            list = db.fi_camera_info.Where(t => t.region_id == reginId).ToList();
            return list;
        }

        /// <summary>
        /// 获取监控所有区域
        /// </summary>
        /// <returns></returns>
        public List<string> getaregionlist()
        {
            List<string> regionNameList = new List<string>();
            List<fi_camera_regions> regionlist = db.fi_camera_regions.ToList();
            foreach (fi_camera_regions region in regionlist)
            {
                regionNameList.Add(region.regionname);
            }
            return regionNameList;
        }

        /// <summary>
        /// 获取一个长字符串监控区域对应监控数量
        /// </summary>
        /// <returns></returns>
        public List<int> getNumberByRegion()
        {
            List<int> regionvalueList = new List<int>();
            List<fi_camera_regions> region = db.fi_camera_regions.ToList();
            for (int i = 0; i < region.Count; i++)
            {
                var regionId = region[i].regionid;
                int count = db.fi_camera_info.Where(t => t.region_id == regionId).Count();
                regionvalueList.Add(count);
            }
            return regionvalueList;
        }

        /// <summary>
        /// 获取网格监控数据
        /// </summary>
        /// <returns></returns>
        public List<cameraGrid> getCameraGridData()
        {
            List<cameraGrid> list = new List<cameraGrid>();
            List<fi_camera_regions> regionList = db.fi_camera_regions.ToList();

            foreach (fi_camera_regions region in regionList)
            {
                var regionId = region.regionid;
                int count = db.fi_camera_info.Where(t => t.region_id == regionId).Count();
                cameraGrid grid = new cameraGrid();
                grid.text = region.regionname;
                grid.value = count;
                list.Add(grid);
            }

            return list;
        }


        /// <summary>
        /// 返回监控类型数据
        /// </summary>
        /// <returns></returns>
        public List<cameraPiecharts> getCameraTypeNumber()
        {
            List<cameraPiecharts> camerapieList = new List<cameraPiecharts>();
            //枪机
            int bolt = (int)cameraType.bolt;
            //快球
            int fastball = (int)cameraType.fastball;
            //半球
            int hemisphere = (int)cameraType.hemisphere;
            //云台枪机
            int withheadbolt = (int)cameraType.withheadbolt;

            int boltcount = db.fi_camera_info.Where(t => t.camera_type == bolt).Count();
            int fastballcount = db.fi_camera_info.Where(t => t.camera_type == fastball).Count();
            int hemispherecount = db.fi_camera_info.Where(t => t.camera_type == hemisphere).Count();
            int withheadboltcount = db.fi_camera_info.Where(t => t.camera_type == withheadbolt).Count();

            cameraPiecharts boltcountcamerpie = new cameraPiecharts();
            boltcountcamerpie.value = 22;// boltcount;
            boltcountcamerpie.name = "枪机";
            camerapieList.Add(boltcountcamerpie);

            cameraPiecharts fastballcamerpie = new cameraPiecharts();
            fastballcamerpie.value = 70;// fastballcount;
            fastballcamerpie.name = "球机";
            camerapieList.Add(fastballcamerpie);

            cameraPiecharts hemispherecamerpie = new cameraPiecharts();
            hemispherecamerpie.value = 4;// hemispherecount;
            hemispherecamerpie.name = "半球";
            camerapieList.Add(hemispherecamerpie);

            cameraPiecharts withheadboltcamerpie = new cameraPiecharts();
            withheadboltcamerpie.value = 7;// withheadboltcount;
            withheadboltcamerpie.name = "高空";
            camerapieList.Add(withheadboltcamerpie);

            return camerapieList;
        }

        /// <summary>
        /// 返回监控类型统计
        /// </summary>
        /// <returns></returns>
        public int getCameraTypeCount()
        {
            //枪机
            int bolt = (int)cameraType.bolt;
            //快球
            int fastball = (int)cameraType.fastball;
            //半球
            int hemisphere = (int)cameraType.hemisphere;
            //云台枪机
            int withheadbolt = (int)cameraType.withheadbolt;

            int boltcount = db.fi_camera_info.Where(t => t.camera_type == bolt).Count();
            int fastballcount = db.fi_camera_info.Where(t => t.camera_type == fastball).Count();
            int hemispherecount = db.fi_camera_info.Where(t => t.camera_type == hemisphere).Count();
            int withheadboltcount = db.fi_camera_info.Where(t => t.camera_type == withheadbolt).Count();

            return boltcount + fastballcount + hemispherecount + withheadboltcount;
        }

        /// <summary>
        /// 查询无人机列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<cameratree> getwrj(string filter, int pageIndex, int pageSize)
        {
            using (hzwEntities db = new hzwEntities())
            {
                List<cameratree> wrjcamera = new List<cameratree>();
                if (!string.IsNullOrEmpty(filter))
                {

                    wrjcamera = db.fi_camera_info.Where(t => t.region_id == 42 && t.name.Contains(filter)).Select(t => new
                        cameratree
                        {
                            cameraId = t.camera_id,
                            text = t.name
                        }).OrderBy(t => t.cameraId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    wrjcamera = wrjcamera = db.fi_camera_info.Where(t => t.region_id == 42).Select(t => new
                        cameratree
                    {
                        cameraId = t.camera_id,
                        text = t.name
                    }).OrderBy(t => t.cameraId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }

                return wrjcamera;
            }
        }

        /// <summary>
        /// 获取无人机页码
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public int getwrj(string filter, int pageSize)
        {
            int count;
            List<fi_camera_info> wrjcamera = new List<fi_camera_info>();
            if (!string.IsNullOrEmpty(filter))
            {
                wrjcamera = db.fi_camera_info.Where(t => t.region_id == 42 && t.name.Contains(filter)).ToList();
            }
            else
            {

                wrjcamera = db.fi_camera_info.Where(t => t.region_id == 42).ToList();
            }
            count = wrjcamera.Count;
            if (count % 2 == 0)
            {
                return count / pageSize;
            }
            else
            {
                return (count / pageSize) + 1;
            }
        }

        /// <summary>
        /// 查询周边监控
        /// </summary>
        /// <param name="cameraid"></param>
        /// <returns></returns>
        public List<fi_camera_info> getcamera(string cameraid)
        {
            string[] cameraArray = cameraid.Split(',');
            List<fi_camera_info> list = new List<fi_camera_info>();
            using (hzwEntities db = new hzwEntities())
            {
                foreach (string i in cameraArray)
                {
                    int id = Convert.ToInt32(i);
                    fi_camera_info camerainfo = db.fi_camera_info.SingleOrDefault(t => t.camera_id == id);
                    list.Add(camerainfo);
                }
                return list;
            }
        }

        /// <summary>
        /// 周边监控
        /// </summary>
        /// <param name="lon">经度</param>
        /// <param name="lat">纬度</param>
        /// <param name="r">周边距离</param>
        /// <returns></returns>
        public List<fi_camera_info> getcamera(double lon, double lat, double r)
        {
            List<fi_camera_info> list = new List<fi_camera_info>();
            using (hzwEntities db = new hzwEntities())
            {
                //第一个点
                double X1 = lon + r;
                double Y1 = lat + r;
                //第二个点
                double X2 = lon - r;
                double Y2 = lat - r;

                list = db.fi_camera_info
                    .Where(a => (a.longitude <= X1 && a.latitude <= Y1)
                        && (a.longitude <= X1 && a.latitude >= Y2)
                        && (a.longitude >= X2 && a.longitude <= Y1)
                        && (a.longitude >= X2 && a.latitude >= Y2))
                    .ToList();
                return list;
            }
        }
    }
}
