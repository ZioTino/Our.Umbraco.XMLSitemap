# Our.Umbraco.XMLSitemap
An Umbraco 9 package for creating XML sitemaps for your website.

## Installation
```
dotnet add package Our.Umbraco.XMLSitemap
```

## Description
The package includes two `Document Type`s, used to configure the sitemap (or more than one if needed) and to control how each content item is showing inside it.  

### `XMLSitemapSettings` Document Type
This document type is used only as a composition, to control how each page is displayed inside the sitemap.  
XMLSitemapSettings is created using the new tabs feature available in v9. If you want to step out of it, the only thing you have to do is refactor the **same** document type outside of a tab.

#### `XMLSitemapSettings` Properties
| Property | Relative property in sitemap XML | Default value | How it works? |
| -- | -- | -- | -- |
| **Hide from Sitemap** | -- | Content is showing in the sitemap. | Hides the content from the sitemap. |
| **Change frequency** | `<changefreq>` | `weekly` | How often the content of this page changes, for **Google site map**. If left blank will inherit the value from the plugin settings. |
| **Relative priority** | `<priority>` | Based on `IPublishedContent.Level` | Relative **priority** of this page between **0.1** and **1.0**, where **1.0 is the most important page** on the site and **0.1 isn't**. |
| **Last modified** | `<lastmod>` | -- | The value is taken automatically from the original UpdateDate value of the relative `IPublishedContent`. |
| **Url** | `<loc>` | -- | Takes the `Url` of the `IPublishedContent` as an **Absolute Url**. |

#### `XMLSitemapSettings` Usage  
Simply include the document type as a composition of another, here is an example.  
First in your target document type click on compositions.
![Step 1](assets/docs/xmlsitemapsettings-step1.png?raw=true)  
As next, select the `XMLSitemapSettings` composition and click save.  
![Step 2](assets/docs/xmlsitemapsettings-step2.png?raw=true)  
Hooray! You successfully added the `XMLSitemapSettings` composition to your document type.
![Step 3](assets/docs/xmlsitemapsettings-step3.png?raw=true)

#### `XMLSitemapSettings` Additional informations
Keep in mind that **every content item that don't have the `XmlSitemapSettings` composition in its document type will be automatically included in the sitemap with default setttings**.  

### `XMLSitemap` Document type
This document type is used to create the sitemap. **You can create more than one sitemap** based on your needs, specifying the root node from where the sitemap begins (usually the root node is a top-level node or a node that has at least one hostname set).

#### `XMLSitemap` Properties
| Property | Description | How it works? |
| -- | -- | -- |
| **Excluded Document Types** | A comma delimited list of document type alias to exclude from the XML Sitemap. | Every document type alias that is listed here will be **completely excluded** from the sitemap. |
| **Root Node** | The main node from which to start the XML Sitemap. This node is usually a top-level node or a node that has a hostname set. | Every child node under the root node (and the root node itself too) will be included in the sitemap. |  

#### `XMLSitemap` Usage
The usage of the `XMLSitemap` Document type is simple. You just need to create a new content item of this type, configure it and you will be all set!  
**NOTE**: You might have to modify some document type permission to allow `XMLSitemap` to be created as a child item.