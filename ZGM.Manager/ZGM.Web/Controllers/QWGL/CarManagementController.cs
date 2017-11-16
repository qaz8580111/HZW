using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZGM.Model;
using ZGM.BLL.QWGLBLLs;
using ZGM.Model.ViewModels;
using ZGM.BLL.UnitBLLs;
using Common;

namespace ZGM.Web.Controllers.QWGL
{
    public class CarManagementController : Controller
    {
        /// <summary>
        /// 车辆管理主页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            decimal ncbdduserid = SessionManager.User == null ? 0 : SessionManager.User.UserID;
            List<QWGL_CARTYPE> cars = CarBLL.GetCarType();
            List<SelectListItem> carsLlist = cars
                .Select(c => new SelectListItem()
                {
                    Text = c.CARTYPENAME,
                    Value = c.CARTYPEID.ToString()
                }).ToList();
            ViewBag.Type = carsLlist;

            List<SYS_UNITS> list_unit = UnitBLL.GetMidUnit().ToList();
            List<SelectListItem> list = list_unit.Select(a => new SelectListItem()
          {
              Text = a.UNITNAME,
              Value = a.UNITID.ToString()
          }).ToList();
            ViewBag.listUnit = list;

            return View();
        }

        /// <summary>
        /// 车辆查询并数据分页
        /// </summary>
        /// <returns></returns>
        public JsonResult CarManagement_Grid(int? iDisplayStart, int? iDisplayLength, int? secho)
        {
            //接收查询条件
            string cartype = Request["CarType"];
            string carnumber = Request["CarNumber"].Trim();
            IQueryable<VMCar> list = null;
            try
            {
                list = CarBLL.GetSearchCar(cartype, carnumber);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            int count = list != null ? list.Count() : 0;

            //筛选后的评价列表
            var data = list.Skip((int)iDisplayStart).Take((int)iDisplayLength);

            //返回json
            return Json(new
            {
                sEcho = secho,
                iTotalRecords = count,
                iTotalDisplayRecords = count,
                aaData = data
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存添加车辆
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitAddCar()
        {
            //接收添加数据
            string cartype = Request["CarType"];
            string carnumber = Request["CarNumber"];
            string cartel = Request["CarTel"];
            string remark = Request["Remark"];
            string unitid = Request["UnitID"];
            //区域实例数据
            QWGL_CARS model = new QWGL_CARS();
            if (!string.IsNullOrEmpty(cartype) && !string.IsNullOrEmpty(carnumber) && !string.IsNullOrEmpty(cartel) && !string.IsNullOrEmpty(unitid))
            {
                model.CARTYPEID = decimal.Parse(cartype);
                model.CARNUMBER = carnumber;
                model.CARTEL = cartel;
                model.REMARK = remark;
                model.STATE = 1;
                model.CREATEUSERID = SessionManager.User.UserID;
                model.CREATETIME = DateTime.Now;
                model.UNITID = decimal.Parse(unitid);
                //插入数据
                try
                {
                    CarBLL.AddCarList(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e); 
                }

                return Content("添加成功");
            }
            else
            {
                return Content("添加失败");
            }

          
        }

        /// <summary>
        /// 编辑车辆展示
        /// </summary>
        /// <returns></returns>
        public JsonResult EditCarShow()
        {
            decimal CarId = 0;
            decimal.TryParse(Request["CarID"],out CarId);
            QWGL_CARS model = new QWGL_CARS();
            try
            {
                model = CarBLL.GetCarByID(CarId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e); 
            }
            return Json(new
            {
                CARID = model.CARID,
                CARTYPEID = model.CARTYPEID,
                CARNUMBER = model.CARNUMBER,
                CARTEL = model.CARTEL,
                REMARK = model.REMARK,
                UNITID = model.UNITID,
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存修改车辆
        /// </summary>
        /// <returns></returns>
        public ContentResult SubmitEditCar()
        {
            //接收添加数据
            decimal CarId = 0;
            decimal.TryParse(Request["CarID"], out CarId);
            string cartype = Request["CarType"];
            string carnumber = Request["CarNumber"];
            string cartel = Request["CarTel"];
            string remark = Request["Remark"];
            string unitid = Request["UnitID"];
            QWGL_CARS model = new QWGL_CARS();

            //区域实例数据
            try
            {
                model = CarBLL.GetCarByID(CarId);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            if (!string.IsNullOrEmpty(cartype) && !string.IsNullOrEmpty(carnumber) && !string.IsNullOrEmpty(cartel) && !string.IsNullOrEmpty(unitid))
            {
                model.CARTYPEID = decimal.Parse(cartype);
                model.CARNUMBER = carnumber;
                model.CARTEL = cartel;
                model.REMARK = remark;
                model.UNITID = decimal.Parse(unitid);
                //修改数据
                try
                {
                    CarBLL.EditCarList(model);
                }
                catch (Exception e)
                {
                    Log4NetTools.WriteLog(e);
                }
                return Content("修改成功");
            }
            else
            {
                return Content("修改失败");
            }

           
        }

        /// <summary>
        /// 删除车辆
        /// </summary>
        /// <returns></returns>
        public ContentResult DeleteCar()
        {
            //接收添加数据
            decimal CarId = 0;
            decimal.TryParse(Request["CarId"],out CarId);
            string result;
            //删除数据
            try
            {
                CarBLL.DeleteCar(CarId);
                result = "1";
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
                result = "2";
            }

            return Content(result);
        }

        /// <summary>
        /// 车牌号唯一校验
        /// </summary>
        /// <returns></returns>
        public ContentResult CheckCarNumber()
        {
            string carnumber = Request["CarNumber"];
            string result = "";
            try
            {
                result = CarBLL.CheckCarNumber(carnumber);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }

            return Content(result);
        }

        /// <summary>
        /// 终端号码唯一校验
        /// </summary>
        /// <returns></returns>
        public ContentResult CheckCarTel()
        {
            string cartel = Request["CarTel"];
            string result = "";
            try
            {
                result = CarBLL.CheckCarTel(cartel);
            }
            catch (Exception e)
            {
                Log4NetTools.WriteLog(e);
            }
            return Content(result);
        }

    }
}
