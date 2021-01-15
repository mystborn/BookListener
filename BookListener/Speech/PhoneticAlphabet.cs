using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BookListener.Speech
{
    public partial class PhoneticAlphabet : IXmlSerializable
    {
        public string DisplayName { get; private set; }
        public string AlphabetName { get; private set; }
        public string Separator { get; private set; }
        public IReadOnlyList<Phoneme> Consonants { get; private set; }
        public IReadOnlyList<Phoneme> Vowels { get; private set; }
        public IReadOnlyList<Phoneme> Prosodies { get; private set; }

        public PhoneticAlphabet() { }

        public PhoneticAlphabet(
            string name,
            string alphabet,
            string separator,
            IEnumerable<Phoneme> consonants, 
            IEnumerable<Phoneme> vowels,
            IEnumerable<Phoneme> prosodies)
        {
            DisplayName = name;
            AlphabetName = alphabet;
            Separator = separator;
            Consonants = new List<Phoneme>(consonants);
            Vowels = new List<Phoneme>(vowels);
            Prosodies = new List<Phoneme>(prosodies);
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();

            DisplayName = reader.GetAttribute("name");
            if (DisplayName is null)
                throw new InvalidOperationException($"Invalid {nameof(PhoneticAlphabet)} XML: Expected name attribute");

            AlphabetName = reader.GetAttribute("alphabet");
            if (AlphabetName is null)
                throw new InvalidOperationException($"Invalid {nameof(PhoneticAlphabet)} XML: Expected alphabet attribute");

            reader.ReadStartElement();

            if(reader.Name == "Separator")
            {
                if(!reader.IsEmptyElement)
                {
                    Separator = reader.ReadElementContentAsString();
                }
                else
                {
                    Separator = string.Empty;
                    reader.ReadStartElement();
                }
            }

            var consonants = new List<Phoneme>();
            var vowels = new List<Phoneme>();
            var prosodies = new List<Phoneme>();

            if(reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement("Consonants");

                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var phoneme = new Phoneme();
                    phoneme.ReadXml(reader);
                    consonants.Add(phoneme);
                }

                reader.ReadEndElement();
            }

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement("Vowels");

                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var phoneme = new Phoneme();
                    phoneme.ReadXml(reader);
                    vowels.Add(phoneme);
                }

                reader.ReadEndElement();
            }

            if (reader.IsEmptyElement)
            {
                reader.ReadStartElement();
            }
            else
            {
                reader.ReadStartElement("Prosodies");

                while (reader.NodeType != XmlNodeType.EndElement)
                {
                    var phoneme = new Phoneme();
                    phoneme.ReadXml(reader);
                    prosodies.Add(phoneme);
                }

                reader.ReadEndElement();
            }

            Consonants = consonants;
            Vowels = vowels;
            Prosodies = prosodies;
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("name", DisplayName);
            writer.WriteAttributeString("alphabet", AlphabetName);

            if(!string.IsNullOrEmpty(Separator))
            {
                writer.WriteStartElement("Separator");
                writer.WriteAttributeString("xml:space", "preserve");
                writer.WriteValue(Separator);
                writer.WriteEndElement();
            }

            writer.WriteStartElement("Consonants");
            foreach (var phoneme in Consonants)
            {
                writer.WriteStartElement("Phoneme");
                phoneme.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Vowels");
            foreach (var phoneme in Vowels)
            {
                writer.WriteStartElement("Phoneme");
                phoneme.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement("Prosodies");
            foreach (var phoneme in Prosodies)
            {
                writer.WriteStartElement("Phoneme");
                phoneme.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
    }
}
