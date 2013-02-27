using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portality.Web.Mvc
{
    /// <summary>
    /// Encapsulates the information that is required for registering routing and areas
    /// within an ASP.NET MVC application.
    /// </summary>
    /// <remarks>
    /// Inspiration for this class is from: 
    /// http://stackoverflow.com/questions/878578/how-can-i-have-lowercase-routes-in-asp-net-mvc
    /// </remarks>
    public class LowercaseRoute : Route
    {

        public LowercaseRoute(string url, IRouteHandler routeHandler)
            : base(url, routeHandler) { }
        public LowercaseRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler) { }
        public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler) { }
        public LowercaseRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler) { }
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData path = base.GetVirtualPath(requestContext, values);

            if (path != null)
                path.VirtualPath = path.VirtualPath.ToLowerInvariant();

            return path;
        }
    }

    public static class RouteCollectionExtension
    {
        /// <summary>
        /// Maps the specified URL route as lowercase and associates it with the area that is specified
        /// by the System.Web.Mvc.AreaRegistrationContext.AreaName property, using the 
        /// specified route default values.
        /// </summary>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <returns>A reference to the mapped route.</returns>
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return routes.MapLowerCaseRoute(name, url, defaults, null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified 
        /// by the System.Web.Mvc.AreaRegistrationContext.AreaName property, using the 
        /// specified route default values and constraint.
        /// </summary>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify valid values for a URL parameter.</param>
        /// <returns></returns>
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults, string constraints)
        {
            return routes.MapLowerCaseRoute(name, url, defaults, constraints, null);
        }

        /// <summary>
        /// Maps the specified URL route and associates it with the area that is specified
        /// by the System.Web.Mvc.AreaRegistrationContext.AreaName property, using the 
        /// specified namespaces.
        /// </summary>
        /// <param name="name">The name of the route.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify valid values for a URL parameter.</param>
        /// <param name="namespaces">An enumerable set of namespaces for the application.</param>
        /// <returns></returns>
        public static Route MapLowerCaseRoute(this RouteCollection routes, string name, string url, object defaults, string constraints, string[] namespaces)
        {
            Route route = new LowercaseRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if (namespaces != null && namespaces.Length > 0)
                route.DataTokens["Namespaces"] = namespaces;

            routes.Add(name, route);

            return route;
        }

    }
}
