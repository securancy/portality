using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Portality.Common
{
    /// <summary>
    /// A helper class for quickly adding and removing strong typed objects to and from the Session container.
    /// </summary>
    public static class SessionManager
    {

        public static void Add<T>(string name, T value) where T : class
        {
            HttpContext.Current.Session.Add(name, value);
        }

        public static void Clear()
        {
            HttpContext.Current.Session.Clear();
        }

        public static void Remove(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        public static T Retrieve<T>(string name) where T : class
        {
            return HttpContext.Current.Session[name] as T;
        }

    }
}
