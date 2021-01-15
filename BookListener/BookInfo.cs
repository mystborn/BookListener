using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookListener
{
    [XmlRoot(ElementName = "Book")]
    public class BookInfo
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Series { get; set; }

        [XmlArray(ElementName = "Authors")]
        [XmlArrayItem(ElementName = "Author")]
        public List<string> Authors { get; set; }

        [XmlArray(ElementName = "Tags")]
        [XmlArrayItem(ElementName = "Tag")]
        public List<string> Tags { get; set; }
        public DateTime? Published { get; set; }

        [XmlArray(ElementName = "Chapters")]
        [XmlArrayItem(ElementName = "Chapter")]
        public List<Chapter> Chapters { get; set; }
    }
}
