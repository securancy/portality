using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Portality.Web.Mvc;

namespace Portality.Plugins.Editor.Controllers
{
    public class ContentController : Controller
    {
        [HttpPost]
        public string Load(Guid contentId)
        {
            return PortalityEditor.Behaviour.Provider.LoadContent(contentId, Languages.DefaultOrUnspecified);
        }

        /// <summary>
        /// Signals the Cloudix ContentProvider to store the data.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="value">The contents.</param>
        /// <remarks>
        /// This method is called from the client. 
        /// </remarks>
        [HttpPost]
        public void Save(Guid contentId, string value)
        {
            _Save(contentId, value);
        }

        /// <summary>
        /// The internal Save routine for storing content to the provider.
        /// </summary>
        /// <param name="contentId">The content identifier.</param>
        /// <param name="value">The contents.</param>
        private void _Save(Guid contentId, string value)
        {
            try
            {
                PortalityEditor.Behaviour.Provider.SaveContent(contentId, value, Languages.DefaultOrUnspecified);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
