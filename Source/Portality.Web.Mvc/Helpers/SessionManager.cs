using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Portality.Web.Mvc
{
    /// <summary>
    /// A helper class for quickly adding and removing strong typed objects to and from the Session container.
    /// </summary>
    public static class SessionManager
    {
        /// <summary>
        /// Adds a strong typed object to the current Session.
        /// </summary>
        /// <typeparam name="T">The Type of the object to add.</typeparam>
        /// <param name="name">The name e.g. "key" of the object to add.</param>
        /// <param name="value">The value e.g. object to add.</param>
        public static void Add<T>(string name, T value) where T : class
        {
            HttpContext.Current.Session.Add(name, value);
        }

        /// <summary>
        /// Clears the current session.
        /// </summary>
        /// <remarks>
        /// This is in fact identical to: HttpContext.Current.Session.Clear()
        /// </remarks>
        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// Removes a specific object from the Session.
        /// </summary>
        /// <param name="name">The object's name that is to be removed.</param>
        public static void Remove(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>
        /// Retrieves a specific object from the Session - and cast it to the provided Type.
        /// </summary>
        /// <typeparam name="T">The Type to cast the object to.</typeparam>
        /// <param name="name">The name of the object to retrieve.</param>
        /// <returns>Strong typed object from the Session store.</returns>
        public static T Retrieve<T>(string name) where T : class
        {
            return HttpContext.Current.Session[name] as T;
        }

    }
}
