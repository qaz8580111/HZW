using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateAPP.OmpService;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace UpdateAPP
{
    class UpdteCamera
    {
        static void Main(string[] args)
        {
            //OmpService.OmpServicePortTypeClient omp = new OmpService.OmpServicePortTypeClient();
            //CmsService.CuServicePortTypeClient cms = new CmsService.CuServicePortTypeClient();
            OmpService.OmpServicePortTypeClient omp = new OmpService.OmpServicePortTypeClient();
            CmsService.CuServicePortTypeClient cms = new CmsService.CuServicePortTypeClient();
            CameraBLL cameraBLL = new CameraBLL();

            Console.WriteLine("-----------------------控制中心---------------------------");
            ControlUnitResult cur = omp.getAllControlCenter();
            ControlUnitDTO[] cudtos = cur.controlUnitDTOs;
            foreach (var item in cudtos)
            {
                //break;

                Console.WriteLine(item.controlUnitId + ":" + item.name);
                CameraUnitModel unit = new CameraUnitModel()
                {
                    UnitId = item.controlUnitId.Value,
                    UnitName = item.name
                };
                int result = cameraBLL.AddCameraUnit(unit);
                if (result == 0)
                {
                    Console.WriteLine("监控部门添加失败，部门id：" + item.controlUnitId);
                    cameraBLL = new CameraBLL();
                }
            }

            Console.WriteLine("-----------------------区域-------------------------");
            RegionInfoResult rir = omp.getAllRegionInfo();
            RegionInfoDTO[] ridtos = rir.regionInfoDTOArray;
            foreach (var item in ridtos)
            {
                //break;

                Console.WriteLine(item.regionId + "," + item.name + "," + item.controlUnitId);
                CameraRegionModel region = new CameraRegionModel()
                {
                    RegionId = item.regionId.Value,
                    RegionName = item.name,
                    UnitId = item.controlUnitId
                };
                int result = cameraBLL.AddCameraRegion(region);
                if (result == 0)
                {
                    Console.WriteLine("监控部门添加失败，区域id：" + item.regionId);
                    cameraBLL = new CameraBLL();
                }
            }

            Console.WriteLine("-----------------------监控点位-------------------------");
            int curPage = 1;
            int pageSize = 10;
            int totalPage = 1;
            for (; ; )
            {
                //break;

                if (totalPage < curPage)
                    break;
                CameraInfoResult cir = omp.getCameraInfoPage(pageSize * (curPage - 1), pageSize);
                Console.WriteLine(cir.result + "," + cir.allRow + "," + cir.totalPage + "," + cir.currentPage + "," + cir.pageSize);
                totalPage = cir.totalPage;
                curPage = cir.currentPage + 1;
                CameraInfoDTO[] cidtos = cir.cameraInfoDTOs;
                foreach (var item in cidtos)
                {
                    Console.WriteLine(item.name + "," + item.indexCode + "," + item.regionId);
                    //获取预览监控的参数
                    string parameter = cms.getStreamServiceByCameraIndexCodes("", 0, item.indexCode, 100001);
                    if (!string.IsNullOrWhiteSpace(parameter))
                    {
                        parameter = parameter.Replace("<Privilege Priority=\"0\" Code=\"0\" />", "<Privilege Priority=\"50\" Code=\"31\" />");
                        parameter = parameter.Replace("<Talk>null</Talk>", "<Talk>1</Talk>");
                        parameter = parameter.Replace("\r", "");
                        parameter = parameter.Replace("\n", "");
                        parameter = parameter.Replace("\"", "'");
                    }

                    string playback = cms.getVrmServiceByCameraIndexCodes("", item.indexCode, 100001, "23", 0);
                    if (!string.IsNullOrWhiteSpace(playback))
                    {
                        playback = playback.Replace("<Imp ip=\"172.172.100.10\" port=\"80\" />", "<Imp ip=\"172.172.100.10\" port=\"80\" userName=\"admin\" password=\"b213ceac2f1022023ef2699aa62599cf\"/>");
                        playback = playback.Replace("<Privilege>5</Privilege>", "<Privilege>7</Privilege>");
                        playback = playback.Replace("</StorageLocation>", "</StorageLocation><AutoPlay>0</AutoPlay>");
                        playback = playback.Replace("\r", "");
                        playback = playback.Replace("\n", "");
                        playback = playback.Replace("\"", "'");
                    }
                    double x = 0;
                    double y = 0;

                    if (item.longitude != null && item.latitude != null)
                    {
                        UtilityTools.WGS84ToCGCS2000(item.longitude.Value, item.latitude.Value, out x, out y);
                    }

                    CameraInfoModel camera = new CameraInfoModel()
                    {
                        CameraId = item.cameraId.Value,
                        CameraName = item.name,
                        DeviceId = item.deviceId,
                        RegionId = item.regionId,
                        IndexCode = item.indexCode,
                        Parameter = parameter,
                        PlayBack = playback,
                        X = Convert.ToDecimal(x),
                        Y = Convert.ToDecimal(y)
                    };
                    int result = cameraBLL.AddCameraInfo(camera);
                    if (result == 0)
                    {
                        Console.WriteLine("监控添加失败，监控id：" + item.cameraId);
                        cameraBLL = new CameraBLL();
                    }
                }
            }
            Console.WriteLine("----------------------设备-------------------------");
            curPage = 1;
            pageSize = 10;
            totalPage = 1;
            for (; ; )
            {
                //break;

                if (totalPage < curPage)
                    break;
                DeviceInfoPageResult ddr = omp.getDeviceInfoPage(pageSize * (curPage - 1), pageSize);
                Console.WriteLine(ddr.result + "," + ddr.allRow + "," + ddr.totalPage + "," + ddr.currentPage + "," + ddr.pageSize);
                totalPage = ddr.totalPage;
                curPage = ddr.currentPage + 1;
                DeviceInfoDTO[] didtos = ddr.deviceInfoDTO;
                foreach (var item in didtos)
                {
                    Console.WriteLine(item.name + "," + item.typeCode + "," + item.userName + "," + item.userPwd);

                    CameraDevModel dev = new CameraDevModel
                    {
                        DeviceId = item.deviceId.Value,
                        IndexCode = item.indexCode,
                        TypeCode = item.typeCode,
                        Name = item.name
                    };
                    int result = cameraBLL.AddCameraDev(dev);
                    if (result == 0)
                    {
                        Console.WriteLine("监控设备添加失败，监控id：" + item.deviceId);
                        cameraBLL = new CameraBLL();
                    }
                }
            }

            Console.WriteLine("---------------------测试--------------------------------");
            //OmpService.AlarmServerResult asr = omp.getAllAlarmServer();
            //OmpService.AlarmServerDTO[] asdtos = asr.alarmServerDTOArray;
            //foreach (var item in asdtos)
            //{
            //    Console.WriteLine(item.name);
            //}
            //获取预览监控的参数
            //string str = cms.getStreamServiceByCameraIndexCodes("", 0, "33021203001310016724", 100001);
            //Console.WriteLine(str);

            //回放的参数
            //string str2 = cms.getVrmServiceByCameraIndexCodes("", "33021203001310013618", 100001, "23", 0);
            //Console.WriteLine(str2);
            //Console.ReadLine();
        }
    }
}
