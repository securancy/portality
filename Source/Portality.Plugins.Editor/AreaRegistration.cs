using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Portality.Plugins.Editor
{
    public class PortalityEditorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PortalityEditor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(
                "PortalityEditor_default",
                "PortalityEditor/{controller}/{action}/{id}",
                new { controller = "", action = "", id = UrlParameter.Optional }
            );

        }
    }
}
