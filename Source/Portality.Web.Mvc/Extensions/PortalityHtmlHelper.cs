using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Portality.Web.Mvc.Extensions;
using Portality.Web.Mvc.Extensions.InnerExtensions;

namespace Portality.Web.Mvc
{
    public static class PortalityHtmlHelper
    {
        /// <summary>
        /// A combined repository of Portality™ extension helpers.
        /// </summary>
        /// <example>
        /// Example usage would be:
        /// @Html.Portality() ... 
        /// </example>
        public static PortalityHelperExtensions Portality(this HtmlHelper htmlHelper)
        {
            return PortalityHelperExtensions.GetInstance(htmlHelper);
        }

    }

    public class PortalityHelperExtensions
    {
        #region -- Singleton plumbing for @Html.Portality() namespace --

        
        private static PortalityHelperExtensions _instance;
        private HtmlHelper _htmlHelper;

        public static PortalityHelperExtensions GetInstance(HtmlHelper htmlHelper)
        {
            if (_instance == null)
                _instance = new PortalityHelperExtensions();

            _instance.SetHtmlHelper(htmlHelper);

            return _instance;
        }

        public PortalityHelperExtensions()
        {
            Scripts = new HeaderIncludeRegistrar(HeaderIncludeFormatters.ScriptFormat);
            Styles = new HeaderIncludeRegistrar(HeaderIncludeFormatters.StyleFormat);
        }

        private void SetHtmlHelper(HtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }


        #endregion

        /// <summary>
        /// Currently registered stylesheets, which can be rendered in the <head>-tag.
        /// </summary>
        /// <example>
        /// Example usage in a View or Partial View:
        /// @{
        ///     Html.Portality().Styles.Add("/Dashboard/Content/Dashboard.css");
        ///  }
        /// 
        /// Example usage in a Layout ("Master") Page:
        /// <head>
        ///     @Html.Portality().Styles.Render()
        /// </head>
        /// 
        /// </example>
        public HeaderIncludeRegistrar Styles { get; private set; }

        /// <summary>
        /// Currently registered javascript includes, which can be rendered in the <head>-tag.
        /// </summary>
        /// <example>
        /// Example usage in a View or Partial View:
        /// @{
        ///     Html.Portality().Scripts.Add("/Dashboard/Content/Dashboard.js");
        ///  }
        /// 
        /// Example usage in a Layout ("Master") Page:
        /// <head>
        ///     @Html.Portality().Scripts.Render()
        /// </head>
        /// 
        /// </example>
        public HeaderIncludeRegistrar Scripts { get; private set; }

        public string PageTitle(string title)
        {
            return HtmlHelperExtensions.PageTitle(_htmlHelper, title);
        }

        /// <summary>
        /// Allows the rendering of a regular ActionLink, but with an optional @class name (such as 'active' or 'selected')
        /// for usage in navigation menu's.
        /// </summary>
        public MvcHtmlString MenuLink(string linkText, string actionName, string controllerName, string className = "selected")
        {
            return HtmlHelperExtensions.MenuLink(_htmlHelper, linkText, actionName, controllerName, className);
        }


    }


}
