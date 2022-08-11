using System.Linq;
using Lucene.Net.Util;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Our.Umbraco.XMLSitemap.Extensions
{
    public static class PublishedContentExtensions
    {
        public static bool IsIncludedInSitemap(this IPublishedContent content,string[] _excludedDocumentTypes)
        {
            return 
                content.IsPublished() &&
                (!content.HasProperty(Constants.Configuration.SitemapHideDataTypeName) || !content.Value<bool>(Constants.Configuration.SitemapHideDataTypeName)) &&
                (!content.HasProperty("properties") || content.Value<MetaMomentum.Models.MetaValues>("properties") == null || !content.Value<MetaMomentum.Models.MetaValues>("properties").NoIndex) &&
                !string.IsNullOrWhiteSpace(content.UrlSegment) &&
                !_excludedDocumentTypes.Contains(content.ContentType.Alias);
        }
    }
}