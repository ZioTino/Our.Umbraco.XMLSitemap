using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Our.Umbraco.XMLSitemap.Extensions;
using Our.Umbraco.XMLSitemap.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Our.Umbraco.XMLSitemap.Controllers
{
    public class XMLSitemapController : RenderController
    {
        private IPublishedContent _rootNode;
        private string[] _excludedDocumentTypes;
        private string _changeFreqDefault;

        public XMLSitemapController(
            ILogger<RenderController> logger, 
            ICompositeViewEngine compositeViewEngine, 
            IUmbracoContextAccessor umbracoContextAccessor) 
                : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        public override IActionResult Index()
        {
            if(!CurrentPage.HasProperty(Constants.Configuration.SitemapRootNodeDataTypeName)){
                throw new ArgumentNullException("The DocumentType XMLSitemap must have the property "+Constants.Configuration.SitemapRootNodeDataTypeName+"!");
            }
            if(!CurrentPage.HasProperty(Constants.Configuration.SitemapExcludedDocTypesDataTypeName)){
                throw new ArgumentNullException("The DocumentType XMLSitemap must have the property "+Constants.Configuration.SitemapExcludedDocTypesDataTypeName+"!");
            }
            if(!CurrentPage.HasProperty(Constants.Configuration.SitemapChangeFreqDefaultDataTypeName)){
                throw new ArgumentNullException("The DocumentType XMLSitemap must have the property "+Constants.Configuration.SitemapChangeFreqDefaultDataTypeName+"!");
            }

            _changeFreqDefault = CurrentPage.Value<string>(Constants.Configuration.SitemapChangeFreqDefaultDataTypeName);
            if(string.IsNullOrWhiteSpace(_changeFreqDefault))_changeFreqDefault="weekly";

            _rootNode = CurrentPage.Value<IPublishedContent>(Constants.Configuration.SitemapRootNodeDataTypeName);
            if (_rootNode == null)
                throw new ArgumentNullException("The Root Node value must be set for the XML Sitemap!");

            _excludedDocumentTypes = CurrentPage.Value<string>(Constants.Configuration.SitemapExcludedDocTypesDataTypeName).ToLower().Replace(" ",";").Replace(",",";").Split(';');

            XmlSitemap sitemap = new XmlSitemap();
            if(_rootNode.IsIncludedInSitemap(_excludedDocumentTypes)){
                sitemap.Items.AddRange(GetXmlSitemapAllowedSelfAndChildren(_rootNode, true));
            }
            string result;
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(XmlSitemap));
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            using (var ms = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(ms, settings))
                {
                    serializer.Serialize(writer, sitemap);
                    sb.Append(ms.ToString());
                }
                result = Encoding.UTF8.GetString(ms.ToArray());
            }

            return Content(result, "application/xml");
        }

        private List<XmlSitemapItem> GetXmlSitemapAllowedSelfAndChildren(IPublishedContent item, bool rootNode = false)
        {
            var result = new List<XmlSitemapItem>();
           
            result.Add(new XmlSitemapItem()
            {
                Location = item.Url(item.GetCultureFromDomains(), UrlMode.Absolute),
                LastModified = item.UpdateDate,
                Priority = item.Value<decimal>(Constants.Configuration.SitemapPriorityDataTypeName),
                ChangeFrequency = string.IsNullOrWhiteSpace(item.Value<string>(Constants.Configuration.SitemapChangeFreqDataTypeName)) ? _changeFreqDefault : item.Value<string>(Constants.Configuration.SitemapChangeFreqDataTypeName)
            });
            
            IEnumerable<IPublishedContent> childrens = item.Children().Where(x => x.IsPublished() && x.IsIncludedInSitemap(_excludedDocumentTypes) && !string.IsNullOrWhiteSpace(x.UrlSegment));
            
            if (childrens.Any())
                foreach (IPublishedContent children in childrens)
                    result.AddRange(GetXmlSitemapAllowedSelfAndChildren(children, false));

            return result;
        }
    }
}