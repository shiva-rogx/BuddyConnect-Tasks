using DropDownAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DropDownAjax.Controllers
{
    public class DropController : Controller
    {
        DropdownEntities db = new DropdownEntities();
        public ActionResult Index()
        {
            List<Country> CountryList = db.Countries.ToList();
            ViewBag.CountryList = new SelectList(CountryList, "CountryId", "CountryName");
            return View();
        }

        public JsonResult GetStateList(int CountryId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<State> StateList = db.States.Where(x => x.CountryId == CountryId).ToList();
            return Json(StateList, JsonRequestBehavior.AllowGet);

        }

    }
}
    