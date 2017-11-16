using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Taizhou.PLE.BLL.GeogSpace;

namespace Web.Controllers.CoordinationCentre
{
    public class ArticlesPublishedController : Controller
    {
        public const string THIS_VIEW_PATH = @"~/Views/PersonalCentre/MessageCentre/";

        public ActionResult Index()
        {
            List<SelectListItem> LAYERTYPEList = new LAYERTYPEBLL().GetLAYERTYPEList()
              .Select(c => new SelectListItem
              {
                  Text = c.NAME,
                  Value = c.ID.ToString()
              }).ToList();
            LAYERTYPEList.Insert(0, new SelectListItem()
            {
                Selected = true,
                Text = "请选择",
                Value = "0"
            });
            ViewBag.LAYERTYPEList = LAYERTYPEList;
            return View(THIS_VIEW_PATH + "Index.cshtml");
        }

    }
}
