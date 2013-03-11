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
        protected Stopwatch stopwatch { get; set; }

        /// <summary>
        /// Provide a name, for distincting multiple MonitorDuration methods.
        /// </summary>
        public string Name { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Initialize a new Stopwatch instance - and start it immediately
            stopwatch = new Stopwatch();
            stopwatch.Start();
            
            // Execute the normal behaviour
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // End the monitoring, 
            stopwatch.Stop();

            // Fetch elapsed time (in seconds)
            var timeElapsed = stopwatch.Elapsed.TotalSeconds;

            // Store the result in the ViewBag. Note that this will
            // not be available when you redirect to another action.
            // In this case, the viewResult will be null.
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult != null)
                viewResult.ViewBag.TimeElapsed = timeElapsed;

            // Always store the result in TempData
            filterContext.Controller.TempData["TimeElapsed" + Name] = timeElapsed;

            // Finish up, and provide the results to the Controller
            base.OnActionExecuted(filterContext);
        }

    }
}
