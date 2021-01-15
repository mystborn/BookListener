using NPOI.SS.Formula.Functions;
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
    public enum PhonemeType
    {
        Consonant,
        Vowel,
        Prosody
    }

    public class Phoneme : IXmlSerializable, IEquatable<Phoneme>
    {
        public PhonemeType Type { get; private set; }
        public string Value { get; private set; }
        public PhonemeExample ExampleWord { get; private set; }
        public string ExampleTranscription { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

        public Phoneme() { }

        public Phoneme(PhonemeType type, string value, PhonemeExample exampleWord, string exampleTranscription)
            : this(type, value, exampleWord, exampleTranscription, string.Empty)
        {
        }

        public Phoneme(PhonemeType type, string value, PhonemeExample exampleWord, string exampleTranscription, string description)
        {
            Type = type;
            Value = value;

            if (exampleWord != null)
            {
                if (exampleTranscription is null)
                    throw new ArgumentNullException(nameof(exampleTranscription), $"{nameof(exampleTranscription)} cannot be null if {nameof(exampleWord)} is not null.");
                ExampleWord = exampleWord;
                ExampleTranscription = exampleTranscription;
            }

            Description = description;
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();

            var typeString = reader.GetAttribute("type");
            if(typeString is null)
                throw new InvalidOperationException($"Invalid {nameof(Phoneme)} XML: Expected type attribute");

            if(!Enum.TryParse<PhonemeType>(typeString, true, out var type))
                throw new InvalidOperationException($"Invalid {nameof(Phoneme)} XML: Invalid type attribute. Expected one of [{string.Join(", ", Enum.GetNames(typeof(PhonemeType)))}]");

            Type = type;

            reader.ReadStartElement();

            Value = reader.ReadElementContentAsString("Value", "");
            if(reader.NodeType != XmlNodeType.EndElement)
            {
                ExampleWord = new PhonemeExample();
                ExampleWord.ReadXml(reader);
                ExampleTranscription = reader.ReadElementContentAsString("ExampleTranscription", "");
            }

            if(reader.NodeType != XmlNodeType.EndElement)
            {
                Description = reader.ReadElementContentAsString("Description", "");
            }

            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("type", Enum.GetName(typeof(PhonemeType), Type));
            writer.WriteElementString("Value", Value);
            if(ExampleWord != null)
            {
                writer.WriteStartElement("ExampleWord");
                ExampleWord.WriteXml(writer);
                writer.WriteEndElement();

                writer.WriteElementString("ExampleTranscription", ExampleTranscription);
            }

            if (!string.IsNullOrWhiteSpace(Description))
                writer.WriteElementString("Description", Description);
        }

        public bool Equals(Phoneme other)
        {
            if (other is null)
                return false;

            return Type == other.Type && Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Phoneme other)
                return Equals(other);

            return false;
        }
    }
}
