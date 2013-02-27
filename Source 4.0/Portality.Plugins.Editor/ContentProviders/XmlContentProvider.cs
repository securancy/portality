using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Portality.Web.Mvc
{
    /// <summary>
    /// A quick and dirty XmlContentProvider. This could do with a lot of
    /// cleaning, but for the time being it just works.
    /// </summary>
    public class XmlContentProvider : BaseContentProvider
    {
        public XmlContentProvider(string xmlFilePath)
            : base(xmlFilePath)
        {
            // Empty by design
        }

        #region IContentProvider Members

        public override void InitializeContentCatalog(params string[] args)
        {
            // Expect a string as XmlFilePath as first - and only - argument
            var xmlFilePath = args.FirstOrDefault();
            if (xmlFilePath == null)
                throw new ArgumentException("Argument missing. Filepath to Xml document is required as first argument.", "args");

            // TODO: Add more error handling in this method
            //       At this moment we *assume* it'll work...

            // Read the Xml document
            XDocument document = XDocument.Load(xmlFilePath, LoadOptions.None);

            try
            {
                var contentBlocks =
                    // Fetch a catalogue of all the <Content> XmlNodes
                    document.Elements("CloudixContent")
                        .Elements("ContentBlocks")
                        .Elements("Content")
                        .ToList();

                foreach (var block in contentBlocks)
                {
                    // Convert each XmlNode into a ContentBlock object
                    // and add it to the Catalog
                    this.Add(new ContentBlock()
                    {
                        Id = Guid.Parse(block.Attribute("Id").Value),
                        Content = block.Value
                    });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
