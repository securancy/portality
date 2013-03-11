using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Portality.Common
{
    public static class MenuLinkExtensions
    {

        /// <summary>
        /// Allows the rendering of a regular ActionLink, but with an optional @class name (such as 'active' or 'selected')
        /// for usage in navigation menu's.
        /// </summary>
        /// <returns></returns>
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string className = "selected")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action").ToLower();
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller").ToLower();
            if (actionName.ToLower() == currentAction && controllerName.ToLower() == currentController)
            {
                return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = className
                    });
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }

    }
}
