using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portality.Common.Performance
{
    public static class PerformanceExtensions
    {
        /// <summary>
        /// Returns the elapsed time for ActionResults that are decorated
        /// with the [MonitorDuration] attribute.
        /// </summary>
        /// <returns>Returns the elapsed time in seconds.</returns>
        /// <remarks>
        /// Use the function in a Controller, like: 
        /// this.ElapsedTime();
        /// </remarks>
        public static decimal ElapsedTime(this Controller controller, string name = "")
        {
            // Fetch the elapsed time, if any, provided by the MonitorDurationAttribute
            var elapsed = controller.TempData["TimeElapsed" + name];
            
            if (elapsed != null)
                // MonitorDurationAttribute has set the _ElapsedTime property,
                // so cast it to "total seconds" and return
                return decimal.Parse(elapsed.ToString());
            else
                // No _ElapsedTime property found, so return nothing
                return -1;
        }

    }
}
