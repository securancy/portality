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

            return RedirectToAction("MonitorDurationResult");
        }

        public ActionResult MonitorDurationResult()
        {
            var value = ViewBag.Something;
            
            ViewBag.TimeElapsed = this.ElapsedTime();
            return View();
        }

    }
}
