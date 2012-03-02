using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Portality.Plugins.Editor;
using Portality.Plugins.Editor.Core.ContentProvider;

namespace Portality.Web.Mvc
{
    /// <summary>
    /// Defines the basic structure of a block of content.
    /// </summary>
    public class ContentBlock
    {
        /// <summary>
        /// The unique identifier of this content block.
        /// </summary>
        /// <remarks>
        /// When added to a ContentCatalog this property is <strong>automatically set</strong>.
        /// </remarks>
        [Required]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public Languages Language { get; set; }
    }
}
