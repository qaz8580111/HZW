using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZGM.Model;
using ZGM.BLL;
using ZGM.Model.ViewModels;
using ZGM.BLL.QWGLBLLs;

namespace ZGM.PhoneAPI.Controllers.XTSZ
{
    public class CarManagementController : ApiController
    {
        public List<VMCar> GetAllCarList()
        {
            return BLL.QWGLBLLs.CarBLL.GetSearchCar(null, null).Select(t => new VMCar
            {
                CARID = t.CARID,
                CARNUMBER = t.CARNUMBER,
                CARTYPEID = (decimal)t.CARTYPEID,
                CARTYPENAME = CarBLL.GetCarTypeByID((decimal)t.CARTYPEID).CARTYPENAME,
                CARTEL = t.CARTEL,
                ISONLINE = 1,
                REMARK = t.REMARK
            }).ToList();
        }
    }
}
