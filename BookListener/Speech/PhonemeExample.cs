using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BookListener.Speech
{
    public class PhonemeExample : IXmlSerializable, IEquatable<PhonemeExample>
    {
        public string Prefix { get; private set; } = string.Empty;
        public string ExampleSound { get; private set; } = string.Empty;
        public string Suffix { get; private set; } = string.Empty;

        public string Word => Prefix + ExampleSound + Suffix;

        public PhonemeExample() { }

        public PhonemeExample(string prefix, string exampleSound, string suffix)
        {
            if (exampleSound is null)
                throw new ArgumentNullException(nameof(exampleSound));

            Prefix = prefix ?? string.Empty;
            ExampleSound = exampleSound;
            Suffix = suffix ?? string.Empty;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement();

            if (reader.Name == "Prefix")
                Prefix = reader.ReadElementContentAsString("Prefix", "");

            ExampleSound = reader.ReadElementContentAsString("ExampleSound", "");

            if (reader.NodeType != XmlNodeType.EndElement)
                Suffix = reader.ReadElementContentAsString("Suffix", "");

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(Prefix))
                writer.WriteElementString("Prefix", Prefix);

            writer.WriteElementString("ExampleSound", ExampleSound);

            if (!string.IsNullOrWhiteSpace(Suffix))
                writer.WriteElementString("Suffix", Suffix);
        }

        public bool Equals(PhonemeExample other)
        {
            if (other is null)
                return false;

            return Word == other.Word;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhonemeExample pe)
                return Equals(pe);

            return false;
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode();
        }
    }
}
