using System;
using Umbraco.Cms.Core.Packaging;

namespace Our.Umbraco.XMLSitemap.Migrations
{
    public class XmlSitemapMigrationPlan : PackageMigrationPlan
    {
        public XmlSitemapMigrationPlan() 
            : base("Our.Umbraco.XMLSitemap")
        {
        }

        protected override void DefinePlan()
        {
            To<ImportPackageXmlMigration>(new Guid("5c72c44c-53b0-4af9-84a4-38b0d1ed35b0"));
        }
    }
}