using NPOI.XWPF.UserModel;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Extractors
{
    public class DocxExtractor : ITextExtractor
    {
        private string _filePath;

        public DocxExtractor(string file)
        {
            _filePath = file;
        }

        public Task<Book> GetBookAsync()
        {
            using(var file = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                var document = new XWPFDocument(file);
                var info = GetInfo(document);
                var book = new Book(info, document.Paragraphs.Select(p => p.Text));
                document.Close();
                return Task.FromResult(book);
            }
        }

        public Task<BookInfo> GetInfoAsync()
        {
            using(var file = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                var document = new XWPFDocument(file);
                var info = GetInfo(document);
                document.Close();
                return Task.FromResult(info);
            }
        }

        private BookInfo GetInfo(XWPFDocument document)
        {
            var props = document.GetProperties();
            var info = new BookInfo()
            {
                Title = props.CoreProperties.Title ?? Path.GetFileNameWithoutExtension(_filePath),
                Authors = new List<string>() { props.CoreProperties.Creator },
                Publisher = props.ExtendedProperties.Company,
                Published = props.CoreProperties.Modified ?? props.CoreProperties.Created,
                Tags = props.CoreProperties.Keywords?.Split(',').ToList()
            };

            return info;
        }
    }
}
