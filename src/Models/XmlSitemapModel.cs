using System;
using System.Xml.Serialization;
using J2N.Collections.Generic;

namespace Our.Umbraco.XMLSitemap.Models
{
    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class XmlSitemap
    {
        public XmlSitemap()
        {
            Items = new List<XmlSitemapItem>();
        }

        [XmlElement("url")]
        public List<XmlSitemapItem> Items { get; set; }
    }
    public class XmlSitemapItem
    {
        [XmlElement("loc")]
        public string Location { get; set; }

        [XmlElement("lastmod")]
        public DateTime LastModified { get; set; }

        [XmlElement("changefreq")]
        public string ChangeFrequency { get; set; }

        [XmlElement("priority")]
        public decimal Priority { get; set; }
    }
}