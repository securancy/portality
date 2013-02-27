using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Portality.Plugins.Editor;

namespace Portality.Web.Mvc
{
    public static class PortalityEditorExtensions
    {
        public static void RenderHtmlContenxt(this HtmlHelper htmlHelper, Guid contentId)
        {
            StringBuilder htmlContent = new StringBuilder();

            // Content is, by default, fetched through the ContentProvider
            string content = PortalityEditor.RenderRawContent(contentId);

            // Depending on the provided EditMode, we display the content in different ways
            if (PortalityEditor.Behaviour.EditMode == ContentEditMode.ReadOnly)
            {
                htmlContent.Append(String.Format("<div>{0}</div>", content));
            }
            else
            {
                htmlContent.Append(String.Format("<div id=\"{1}\" class=\"editable\">{0}</div>",
                    content, contentId));
            }

            // Render the output as Html
            htmlHelper.ViewContext.Writer.Write(htmlContent);
        }

        public static void RegisterClientSideIncludes(this HtmlHelper htmlHelper)
        {
            var htmlContent = new StringBuilder();

            // Read styles from embedded resource
            htmlContent.Append(@"<style type=""text/css"">");
            htmlContent.Append(Portality.Plugins.Editor.Assets.Resources.PortalityEditorStyles);
            htmlContent.Append("</style>\n");

            // Read scripts from embedded resource
            htmlContent.Append(@"<script type=""text/javascript"">");
            htmlContent.Append(Portality.Plugins.Editor.Assets.Resources.PortalityEditorScripts);
            htmlContent.Append("</script>\n");

            // Render the output as Html
            htmlHelper.ViewContext.Writer.Write(htmlContent);
        }
    }
}
