using System;
using System.Xml.Serialization;
using J2N.Collections.Generic;

namespace MetaMomentum.Models
{
    public class MetaValues
    {
        public string Title { get; set; }
		public string Description { get; set; }
		public bool NoIndex { get; set; }

		public string ShareTitle { get; set; }
		public string ShareDescription { get; set; }

		/// <summary>
		/// This value can be set in configuration under AppSettings Name = "MetaMomentum.OGSiteName"
		/// </summary>
		public string OGSiteName { get; set; }

		/// <summary>
		/// This value can be set in the Web.Config under AppSettings.json under `MetaMomentum.TwitterName`. Make sure that you include the @ symbol
		/// </summary>
		public string TwitterName { get; set; }

		/// <summary>
		/// This value can be set in the Web.Config under AppSettings.json under `MetaMomentum.TwitterName`. Make sure that you include the @ symbol
		/// </summary>
		public string FacebookAppId { get; set; }

		public string ShareImageUrl { get; set; }
    }
}