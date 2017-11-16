using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZGM.WUA.BLL;
using ZGM.WUA.ConceptualModel;

namespace UpdateAPP
{
    class Program
    {
        static void Main1(string[] args)
        {
            //CmsService.CmsServicePortTypeClient cs = new CmsService.CmsServicePortTypeClient();
            //CameraBLL cameraBLL = new CameraBLL();

            //CmsService.LoginResult lr = cs.userLogin("admin", "827CCB0EEA8A706C4C34A16891F84E7B", "", 0, "http://172.16.2.36");
            //Console.WriteLine("seerionId:" + lr.sessionId);
            //Console.WriteLine("errorCode:" + lr.errorCode);
            ////Console.ReadLine();
            //Console.WriteLine("-----------------------控制中心---------------------------");
            //CmsService.ControlUnitsResult cur = cs.getAllControlCenterForList(lr.sessionId);
            //CmsService.ControlUnitDTO[] cudtos = cur.controlUnitArray;
            //foreach (var item in cudtos)
            //{
            //    Console.WriteLine(item.controlUnitId + "," + item.name);
            //    CameraUnitModel unit = new CameraUnitModel()
            //    {
            //        UnitId = item.controlUnitId.Value,
            //        UnitName = item.name,
            //    };
            //    int result = cameraBLL.AddCameraUnit(unit);
            //    if (result == 0)
            //    {
            //        Console.WriteLine("监控部门添加失败，部门id：" + item.controlUnitId);
            //        cameraBLL = new CameraBLL();
            //    }
            //}
            //Console.WriteLine("-----------------------区域-------------------------");
            //CmsService.RegionInfoResult rir = cs.getAllRegionInfoForList(lr.sessionId);
            //CmsService.RegionInfoDTO[] ridtos = rir.regionInfoArray;
            //foreach (var item in ridtos)
            //{
            //    Console.WriteLine(item.regionId + "," + item.name + "," + item.controlUnitId);
            //    CameraUnitModel unit = new CameraUnitModel()
            //    {
            //        UnitId = item.regionId.Value,
            //        UnitName = item.name,
            //        ParentId = item.controlUnitId
            //    };
            //    int result = cameraBLL.AddCameraUnit(unit);
            //    if (result == 0)
            //    {
            //        Console.WriteLine("监控部门添加失败，区域id：" + item.regionId);
            //        cameraBLL = new CameraBLL();
            //    }
            //}
            //Console.WriteLine("-----------------------监控-------------------------");

            //CmsService.CameraInfoResult cir = cs.getAllCameraInfoForList(lr.sessionId);
            //CmsService.CameraInfoDTO[] cidtos = cir.cameraInfoDTOArray;
            //foreach (var item in cidtos)
            //{
            //    Console.WriteLine(item.name + "," + item.regionId);
            //    CameraInfoModel camera = new CameraInfoModel()
            //    {
            //        CameraId = item.cameraId.Value,
            //        CameraName = item.name,
            //        DeviceId = item.deviceId,
            //        RegionId = item.regionId,
            //        IndexCode = item.indexCode,
            //        CameraTypeId = item.cameraType
            //    };
            //    int result = cameraBLL.AddCameraInfo(camera);
            //    if (result == 0)
            //    {
            //        Console.WriteLine("监控添加失败，监控id：" + item.cameraId);
            //        cameraBLL = new CameraBLL();
            //    }
            //}
        }
    }
}
