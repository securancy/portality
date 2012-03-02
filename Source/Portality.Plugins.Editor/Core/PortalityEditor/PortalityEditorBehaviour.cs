using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portality.Plugins.Editor;

namespace Portality.Web.Mvc
{
    public class PortalityEditorBehaviour
    {
        public IContentProvider Provider { get; set; }
        public ContentEditMode EditMode { get; set; }
        public Languages Language { get; set; }

        public PortalityEditorBehaviour(IContentProvider provider, ContentEditMode editMode, Languages language = Languages.DefaultOrUnspecified)
        {
            Provider = provider;
            EditMode = editMode;
            Language = language;
        }
    }
}
