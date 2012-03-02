using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Portality.Web.Mvc.Extensions.InnerExtensions
{
    public class HeaderIncludeRegistrar
    {
        private readonly string _format;
        private readonly List<string> _includes;

        public HeaderIncludeRegistrar(string format)
        {
            _format = format;
            _includes = new List<string>();
        }

        public HeaderIncludeRegistrar Add(string url)
        {
            //?? _includes.Insert(0, url);
            if (!_includes.Contains(url))
                _includes.Add(url);

            return this;
        }

        /// <summary>
        /// Renders the designated header includes.
        /// </summary>
        /// <param name="useCleanIndents">Renders by default 'clean' html.</param>
        /// <returns></returns>
        public IHtmlString Render(bool useCleanIndents = true)
        {
            var returnValue = new StringBuilder();

            foreach (var item in _includes)
            {
                if (useCleanIndents)
                    returnValue.Append(String.Format("\t{0}\r\n", 
                        String.Format(_format, item)));
                else
                    returnValue.AppendLine(String.Format(_format, item));
            }

            // Clear the cache for the next time?
            _includes.Clear();

            return new HtmlString(returnValue.ToString());
        }
    }

    public class HeaderIncludeFormatters
    {
        public const string StyleFormat = "<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\" />";
        public const string ScriptFormat = "<script type=\"text/javascript\" src=\"{0}\"></script>";
    }
}
