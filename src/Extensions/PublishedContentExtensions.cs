using Lucene.Net.Util;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Our.Umbraco.XMLSitemap.Extensions
{
    public static class PublishedContentExtensions
    {
        public static bool IsIncludedInSitemap(this IPublishedContent content)
        {
            return content.Value<bool>(Constants.Configuration.SitemapHideDataTypeName, defaultValue: false);
        }
    }
}