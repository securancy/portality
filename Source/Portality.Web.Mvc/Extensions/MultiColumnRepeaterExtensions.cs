using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Portality.Web.Mvc.Extensions
{
    public static class MultiColumnRepeaterExtensions
    {
        [Obsolete("Action()'s are not supported in MVC 3 RAZOR. Therefor, this method is considered obsolete.", false)]
        public static void MultiColumnRepeater<T>(this HtmlHelper helper, IEnumerable<T> model, int columnCount, Action rowHeader, Action<T> itemTemplate, Action rowFooter)
        {
            int currentColumn = 0;
            foreach (T item in model)
            {
                if (currentColumn == 0)
                    rowHeader();

                itemTemplate(item);

                ++currentColumn;

                if (currentColumn >= columnCount)
                {
                    rowFooter();
                    currentColumn = 0;
                }
            }
        }

    }
}
