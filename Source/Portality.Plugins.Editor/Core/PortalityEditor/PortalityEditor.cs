using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Portality.Plugins.Editor;
using Portality.Web.Mvc;
using System.Web.Mvc;

namespace Portality.Web.Mvc
{
    public sealed class PortalityEditor
    {
        public static PortalityEditorBehaviour Behaviour
        {
            get { return SessionManager.Retrieve<PortalityEditorBehaviour>("contentBehaviour"); }
            private set { SessionManager.Add<PortalityEditorBehaviour>("contentBehaviour", value); }
        }

        public static void Register(IContentProvider customProvider, ContentEditMode editMode, Languages language = Languages.DefaultOrUnspecified)
        {
            Register(new PortalityEditorBehaviour(customProvider, editMode, language));
        }

        public static void Register(PortalityEditorBehaviour behaviour)
        {
            Behaviour = behaviour;
        }

        [Obsolete("Refactored to MVC 3 Razor to use a HtmlExtension: please use Html.RegisterClientSideIncludes.", false)]
        /// <summary>
        /// Returns the basic literal value without any (Html) markup.
        /// </summary>
        public static string RenderRawContent(Guid contentId)
        {
            try
            {
                return Behaviour.Provider.LoadContent(contentId, Behaviour.Language);
            }
            catch (Exception)
            {
                // TODO: We should catch exceptions more decently here...
                throw;
            }
        }

        #region --- OBSOLETE --- 

        public static void RegisterClientSideIncludes()
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
            HttpContext.Current.Response.Write(htmlContent.ToString());
        }
        

        /// <summary>
        /// Renders the value with Cloudix' default Html markup and injects it into the HttpResponse.
        /// </summary>
        [Obsolete("Refactored to MVC 3 Razor to use a HtmlExtension: please use Html.RenderHtmlContent.", false)]
        public static void RenderHtmlContent(Guid contentId)
        {
            StringBuilder htmlContent = new StringBuilder();

            // Content is, by default, fetched through the ContentProvider
            string content = RenderRawContent(contentId);

            // Depending on the provided EditMode, we display the content in different ways
            if (Behaviour.EditMode == ContentEditMode.ReadOnly)
            {
                htmlContent.Append(String.Format("<div>{0}</div>", content));
            }
            else
            {
                htmlContent.Append(String.Format("<div id=\"{1}\" class=\"editable\">{0}</div>",
                    content, contentId));
            }

            // Render the output as Html
            HttpContext.Current.Response.Write(htmlContent.ToString());
        }


        #endregion
    }
}
