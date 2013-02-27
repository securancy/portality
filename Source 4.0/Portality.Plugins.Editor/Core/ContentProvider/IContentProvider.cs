using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portality.Plugins.Editor
{
    public interface IContentProvider
    {
        void InitializeContentCatalog(params string[] args);

        string LoadContent(Guid contentId, Languages language = Languages.DefaultOrUnspecified);

        void SaveContent(Guid contentId, string value, Languages language = Languages.DefaultOrUnspecified);
    }
}
