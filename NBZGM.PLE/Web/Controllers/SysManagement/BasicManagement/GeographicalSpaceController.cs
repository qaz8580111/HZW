using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Taizhou.PLE.BLL.GeogSpace;
using Taizhou.PLE.Common;
using Taizhou.PLE.Model;

namespace Web.Controllers.SysManagement.BasicManagement
{
    public class GeographicalSpaceController : Controller
    {
        //
        // GET: /GeographicalSpace/
        public const string THIS_VIEW_PATH = @"~/Views/SysManagement/BasicManagement/GeographicalSpace/";
        StringBuilder sbMes = new StringBuilder();

        /// <summary>
        /// 元素上报
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //元素列表
            List<SelectListItem> LAYERTYPEList = new LAYERTYPEBLL().GetLAYERTYPEList()
              .ToList().Select(c => new SelectListItem
              {
                  Text = c.NAME,
                  Value = c.ID.ToString()
              }).ToList();

            LAYERTYPEList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            ViewBag.LAYERTYPEList = LAYERTYPEList;
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        /// <summary>
        /// 分页显示执法队员巡查路线列表
        /// </summary>
        public JsonResult GetMAPINFO(int? iDisplayStart
            , int? iDisplayLength, int? secho)
        {
            string ELEMENTADDRESS = Request.QueryString["ELEMENTADDRESS"];



            string Operating = "<div>";

            Operating += "<a class='btn btn-primary btn-small' href='/GeographicalSpace/Edit?ID={0}' title='修改'><i class='icon-edit padding-null'></i></a>";
            Operating += "<a class='btn btn-danger btn-small' href='#' title='删除' onclick='Delete({0})'><i class='icon-trash padding-null'></i></a>";
            Operating += "</div>";

            
            IQueryable<MAPINFO> MAPINFO = new MAPINFOBLL().MAPINFOList();


            if (!string.IsNullOrEmpty(ELEMENTADDRESS))
            {
                MAPINFO = MAPINFO.Where(A => A.ELEMENTADDRESS.Contains(ELEMENTADDRESS));
            }

            List<MAPINFO> MAPINFOListC =
                (from m in MAPINFO
                     .OrderByDescending(t => t.CREATETIME)
                 select m).ToList();

            IList<MAPINFO> MAPINFOList = MAPINFO.ToList();
            var results = MAPINFOList.Select(a =>
                new
                {
                    ELEMENTID = a.ELEMENTID,
                    ELEMENTADDRESS = a.ELEMENTADDRESS,
                    LAYERID = a.LAYERID,
                    LayerName = string.IsNullOrEmpty(new LAYERTYPEBLL().getNameByLayerID(a.LAYERID)) ? "为选择" : new LAYERTYPEBLL().getNameByLayerID(a.LAYERID),
                    ID = a.ID
                });

            //var results = from c in MAPINFO
            //              select new
            //              {

            //                  ELEMENTID = c.ELEMENTID,
            //                  ELEMENTADDRESS = c.ELEMENTADDRESS,
            //                  LAYERID = c.LAYERID,

            //                  LayerName = string.IsNullOrEmpty(new LAYERTYPEBLL().getNameByLayerID(c.LAYERID)) ? "为选择" : new LAYERTYPEBLL().getNameByLayerID(c.LAYERID),
            //                  ID = c.ID
                           
            //              };

            return Json(new
            {
                sEcho = secho,
                iTotalRecords = MAPINFOListC.Count(),
                iTotalDisplayRecords = MAPINFOListC.Count(),
                aaData = results
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取相对应的点面线地图
        /// </summary>
        /// <returns></returns>
        public string getMap()
        {
            string num = Request["num"];
            sbMes.Append(" <div id='silverlightControlHost' style='height:322px;'>");
            sbMes.Append(" <object id='mapCtrl' data='data:application/x-silverlight-2,' type='application/x-silverlight-2'  height='100%' width='100%'>");
            sbMes.Append("<param name='source' value='/ClientBin/MapCtrl.xap' />");
            sbMes.Append(" <param name='background' value='white' />");
            sbMes.Append("<param name='minRuntimeVersion' value='5.0.61118.0' />");
            sbMes.Append("<param name='autoUpgrade' value='true' />");
            if (num == "1")
            {
                sbMes.Append(" <param id='initParams' name='initParams' value='IsGoogleMap=false,MapUrl=http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap,X1=121.409266152472,Y1=28.6559834674633,X2=121.423424005508,Y2=28.6614766315258,Mode=2,PinUrl=./Images/pin.png,OffsetX=24,OffsetY=48' />");
            }
            else if (num == "2")
            {
                sbMes.Append("<param id='initParams' name='initParams' value='IsGoogleMap=false,MapUrl=http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap,X1=121.409266152472,Y1=28.6559834674633,X2=121.423424005508,Y2=28.6614766315258,Mode=3,PinUrl=/Images/pin.png,OffsetX=24,OffsetY=48' />");
            }
            else
            {
                sbMes.Append("<param id='initParams' name='initParams' value='IsGoogleMap=false,MapUrl=http://tmap.tzsjs.gov.cn/services/tilecache/chinaemap,X1=121.409266152472,Y1=28.6559834674633,X2=121.423424005508,Y2=28.6614766315258,Mode=4,PinUrl=/Images/pin.png,OffsetX=24,OffsetY=48' />");
            }

            sbMes.Append("<a href='http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0' style='text-decoration: none'>");
            sbMes.Append(" <img src='http://go.microsoft.com/fwlink/?LinkId=161376' alt='Get Microsoft Silverlight' style='border-style: none' />");
            sbMes.Append(" </a>");
            sbMes.Append(" </object>");
            sbMes.Append(" </div>");
            return sbMes.ToString();
            //return PartialView();
        }

        public string getTypeValue()
        {
            string LAYERID = Request["LAYERID"];
            int LayerID = 0;
            int.TryParse(LAYERID, out LayerID);
            string TypeValue = new LAYERTYPEBLL().getTypeValue(LayerID);

            return TypeValue;

        }

        public ActionResult Create()
        {
            //元素列表
            List<SelectListItem> LAYERTYPEList = new LAYERTYPEBLL().GetLAYERTYPEList()
              .ToList().Select(c => new SelectListItem
              {
                  Text = c.NAME,
                  Value = c.ID.ToString()
              }).ToList();

            LAYERTYPEList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            ViewBag.LAYERTYPEList = LAYERTYPEList;
            //return PartialView();
            return View(THIS_VIEW_PATH + "Create.cshtml");
        }
        [HttpPost]
        public ActionResult CommitCreate()
        {
            string LayerID = Request["LayerID"];
            string ElementID = Request["ElementID"];
            string ElementAddress = Request["ElementAddress"];
            string MapType = Request["MapType"];
            string Geometry = Request["Geometry"];
            string returnJson = Request["returnJson"];
            string CONTAIN = Request["CONTAIN"];
            MAPINFO model = new MAPINFO();
            int LAYERID = 0;
            int.TryParse(LayerID, out LAYERID);
            decimal MAPTYPE = 0;
            decimal.TryParse(MapType, out MAPTYPE);
            model.LAYERID = LAYERID;
            model.ELEMENTID = ElementID;
            model.ELEMENTADDRESS = ElementAddress;
            model.MAPTYPE = MAPTYPE;
            model.LONGLAT = Geometry;
            model.VALUEDATE = returnJson;
            model.USERID = SessionManager.User.UserID;
            model.CONTAIN = CONTAIN;
            new MAPINFOBLL().SaveMAPINFO(model);
            return View(THIS_VIEW_PATH + "Index.cshtml", model);
        }

        public ActionResult Edit()
        {
            string id = Request["ID"];
            int Id = 0;
            int.TryParse(id, out Id);
            MAPINFO model = new MAPINFOBLL().GetMAPINFO(Id);
            //元素列表
            List<SelectListItem> LAYERTYPEList = new LAYERTYPEBLL().GetLAYERTYPEList()
              .ToList().Select(c => new SelectListItem
              {
                  Text = c.NAME,
                  Value = c.ID.ToString()
              }).ToList();

            LAYERTYPEList.Insert(0, new SelectListItem
            {
                Text = "请选择",
                Value = ""
            });
            ViewBag.LAYERTYPEList = LAYERTYPEList;
            return View(THIS_VIEW_PATH + "Edit.cshtml", model);
        }

        [HttpPost]
        public ActionResult CommitEdit()
        {
            string id = Request["ID"];
            int Id = 0;
            int.TryParse(id,out Id);
            string LayerID = Request["LayerID"];
            string ElementID = Request["ElementID"];
            string ElementAddress = Request["ElementAddress"];
            string MapType = Request["MapType"];
            string Geometry = Request["Geometry"];
            string TypeValues = Request["returnJson"];
            string CONTAIN = Request["CONTAIN"];
           // string[] TypeValueList = TypeValues.Split(',');
           // for (int i = 0; i < TypeValueList.Length - 1; i++)
           // {

           // }
            MAPINFO model =new MAPINFO();
            model.ID = Id;
            int LAYERID = 0;
            int.TryParse(LayerID, out LAYERID);
            decimal MAPTYPE = 0;
            decimal.TryParse(MapType, out MAPTYPE);
            model.LAYERID = LAYERID;
            model.ELEMENTID = ElementID;
            model.ELEMENTADDRESS = ElementAddress;
            model.MAPTYPE = MAPTYPE;
            model.LONGLAT = Geometry;
            model.VALUEDATE = TypeValues;
            model.USERID = SessionManager.User.UserID;
            model.CONTAIN = CONTAIN;
            new MAPINFOBLL().ModifyMAPINFO(model);
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public ActionResult Delete()
        {
            string id = Request["ID"];
            int Id = 0;
            int.TryParse(id, out Id);
            new MAPINFOBLL().DeleteMAPINFO(Id);
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

        public string getGEOMETRYByMapType()
        {
            string id = Request.QueryString["id"];
            int Id = 0;
            int.TryParse(id, out Id);
            MAPINFO model = new MAPINFOBLL().GetMAPINFO(Id);
            string GEOMETRY = null;
            if (model != null)
            {
                GEOMETRY = model.LONGLAT;
            }
            return GEOMETRY;
        }

        public string getGemetry() { 
            string s = Request.QueryString["s"];
            return s;
        }

    }
}
