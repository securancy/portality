using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portality.Common.Performance;

namespace Portality.Common.Mvc.Controllers
{
    public class PerformanceController : Controller
    {

        [MonitorDuration]
        public ActionResult MonitorDuration()
        {
            System.Threading.Thread.Sleep(2000);
            return View();
        }

        [MonitorDuration(Name = "MyCustomName")]
        public ActionResult MonitorDurationWithName()
        {
            System.Threading.Thread.Sleep(2000);
            return RedirectToAction("MonitorDurationWithNameResults");
        }

        public ActionResult MonitorDurationWithNameResults()
        {
            if (ViewBag.TimeElapsed == null)
                ViewBag.TimeElapsed = this.ElapsedTime("MyCustomName");

            return View();
        }

    }
}
