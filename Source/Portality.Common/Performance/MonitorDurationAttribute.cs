using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Portality.Common.Performance
{
    /// <summary>
    /// Monitors the duration of executing for a specific ActionResult.
    /// </summary>
    /// <remarks>
    /// Once an ActionResult is decorated with the [MonitorDuration] attribute, you can
    /// easily feth the results through the HttpContext.Current.ElapsedTime property.
    /// This is a custom extension method, provided by Portality.
    /// </remarks>
    public class MonitorDurationAttribute : ActionFilterAttribute
    {
        protected Stopwatch _Stopwatch { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Initialize a new Stopwatch instance - and start it immediately
            _Stopwatch = new Stopwatch();
            _Stopwatch.Start();
            
            // Execute the normal behaviour
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // Finish up
            base.OnActionExecuted(filterContext);

            // End the monitoring, fetch elapsed time (in seconds)
            _Stopwatch.Stop();
            
            var timeElapsed = _Stopwatch.Elapsed.TotalSeconds;

            filterContext.HttpContext.Items.Add("_ElapsedTime", timeElapsed);
            filterContext.Controller.ViewBag.TimeElapsed = timeElapsed;
            filterContext.Controller.TempData["TimeElapsed"] = timeElapsed;
            filterContext.Controller.ViewBag.Something = "Darkside";
        }

    }
}
