using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BookListener
{
    public class Chapter : IXmlSerializable
    {
        public string Title { get; private set; }
        public int Number { get; private set; }
        public int StartingParagraph { get; private set; }
        public int EndingParagraph { get; private set; }
        public int ParagraphCount => EndingParagraph - StartingParagraph;

        public Chapter()
        {
        }

        public Chapter(string title, int number, int startingParagraph, int endingParagraph)
        {
            Title = title;
            Number = number;
            StartingParagraph = startingParagraph;
            EndingParagraph = endingParagraph;
        }

        public override string ToString()
        {
            if(Title is null)
            {
                return $"Chapter {Number}";
            }
            else if(Title.StartsWith("chapter", StringComparison.InvariantCultureIgnoreCase))
            {
                return Title;
            }
            else
            {
                return $"Chapter {Number}: {Title}";
            }
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement();

            if (reader.Name == "Title")
                Title = reader.ReadElementContentAsString();

            Number = reader.ReadElementContentAsInt();
            StartingParagraph = reader.ReadElementContentAsInt();
            EndingParagraph = reader.ReadElementContentAsInt();

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (Title != null)
                writer.WriteElementString("Title", Title);

            writer.WriteElementString("Number", Number.ToString());
            writer.WriteElementString("StartingParagraph", StartingParagraph.ToString());
            writer.WriteElementString("EndingParagraph", EndingParagraph.ToString());
        }
    }
}
