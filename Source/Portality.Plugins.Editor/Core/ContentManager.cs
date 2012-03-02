using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portality.Plugins.Editor.Core
{
    public class ContentManager
    {
        private IContentProvider Provider { get; set; }

        public ContentEditMode EditMode { get; private set; }

        public ContentManager(IContentProvider provider) : this(provider, ContentEditMode.ReadOnly) { }

        public ContentManager(IContentProvider provider, ContentEditMode editMode)
        {
            EditMode = editMode;

            if (EditMode == ContentEditMode.Editable)
            {
                // Register page javascript thingies

                // Register CSS information for the "cloudixEditable" tag

                // onmouseover="this.style.border='1px solid white'; this.style.padding='0px 0px 0px 0px';"
                // onmouseout="this.style.border='0px'; this.style.padding='1px 1px 1px 1px';"

                // TODO: Create a ContentProviderConfiguration, or something, to allow custom Javascript+CSS initialisations
                // or overloads.
            }
        }

        public virtual string LoadContent(Guid contentId, Languages language = Languages.DefaultOrUnspecified)
        {
            // Content is, by default, fetched through the ContentProvider
            string content = Provider.LoadContent(contentId, language);

            // Depending on the provided EditMode, we display the content in different ways
            if (EditMode == ContentEditMode.ReadOnly)
            {
                return String.Format("<div>{0}</div>", content);
            }
            else
            {
                return String.Format("<div class=\"cloudixEditable\">{0}</div>", content);
            }


        }

    }
}
