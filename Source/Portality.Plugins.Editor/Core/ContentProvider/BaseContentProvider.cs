using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Portality.Plugins.Editor;
using System.Collections.Specialized;

namespace Portality.Web.Mvc
{
    /// <summary>
    /// Provides basic functionality to be inherited for derived (custom) ContentProviders.
    /// </summary>
    /// <remarks>
    /// Do note that this base class is abstract and therefor indicates that it shouldn't be
    /// initializable by design.
    /// </remarks>
    public abstract class BaseContentProvider : IContentProvider
    {
        //public ContentCatalog Catalog { get; set; }
        private ObservableCollection<ContentBlock> Catalog { get; set; }

        public BaseContentProvider() : this(null) { }

        public BaseContentProvider(params string[] args)
        {
            // Initialize the catalog
            this.Catalog = new ObservableCollection<ContentBlock>();

            // Hook up to the ObservableCollection's event handlers
            this.Catalog.CollectionChanged += new NotifyCollectionChangedEventHandler(ContentCatalogChanged);

            if (args == null)
                this.InitializeContentCatalog();
            else
                this.InitializeContentCatalog(args);
        }

        /// <summary>
        /// Overload this member and give an implementation to what should happen when items are Added, Deleted, Updated etc.
        /// from the Catalog.
        /// </summary>
        public virtual void ContentCatalogChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Empty by default
        }

        /// <summary>
        /// Adds a ContentBlock to the Catalog.
        /// </summary>
        /// <param name="content">The ContentBlock to add, or update.</param>
        protected void Add(ContentBlock content)
        {
            if (content.Id == null || content.Id == Guid.Empty)
            {
                // No identifier specified, so generate a new identifier
                content.Id = Guid.NewGuid();

                // ...and insert it in the Catalog
                this.Catalog.Add(content);
            }
            else
            {
                // An identifier has been specified, so verify if we can find it in the collection
                var original = Catalog.Where(c => c.Id == content.Id).FirstOrDefault();
                if (original == null)
                {
                    // original record cannot be found in Catalog, so still consider it an insert
                    this.Catalog.Add(content);
                }
                else
                {
                    // original record is found, so update it
                    original.Content = content.Content;
                    original.Language = content.Language;
                    original.Title = content.Title;

                }
            }

        }

        #region IContentProvider Members

        /// <summary>
        /// Override this method in any custom ContentProvider to initially load the contents from its source.
        /// </summary>
        public abstract void InitializeContentCatalog(params string[] args);

        public string LoadContent(Guid contentId, Languages language = Languages.DefaultOrUnspecified)
        {
            var items = Catalog.Where(c => c.Id == contentId);

            if (items == null)
                throw new ArgumentException("Could not find specified content.", "contentId");

            if (language == Languages.DefaultOrUnspecified)
            {
                return items.First().Content;
            }
            else
            {
                var subitem = items.Where(c => c.Language == language).FirstOrDefault();
                if (subitem == null)
                    throw new ArgumentException("Could not find specified content with the specified language.", "contentId");
                else
                    return subitem.Content;
            }

        }

        public void SaveContent(Guid contentId, string value, Languages language = Languages.DefaultOrUnspecified)
        {
            this.Add(new ContentBlock() { Id = contentId, Content = value });
        }

        #endregion

    }
}
