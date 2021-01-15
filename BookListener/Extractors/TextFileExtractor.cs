using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookListener.Extractors
{
    public class TextFileExtractor : ITextExtractor
    {
        private string _filePath;
        private Regex _paragraphSeparator;

        public TextFileExtractor(string file, Regex paragraphSeparator)
        {
            _filePath = file;
            _paragraphSeparator = paragraphSeparator;
        }

        public async Task<Book> GetBookAsync()
        {
            var info = GetInfo();
            using(var reader = new StreamReader(_filePath))
            {
                var text = await reader.ReadToEndAsync();
                var paragraphs = _paragraphSeparator.Split(text);
                return new Book(info, paragraphs.Where(p => !string.IsNullOrWhiteSpace(p)));
            }
        }

        public Task<BookInfo> GetInfoAsync()
        {
            return Task.FromResult(GetInfo());
        }

        private BookInfo GetInfo()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(_filePath);
            var fileInfo = new FileInfo(_filePath);

            return new BookInfo()
            {
                Title = versionInfo.ProductName ?? fileInfo.Name,
                Publisher = versionInfo.CompanyName,
                Published = fileInfo.LastWriteTimeUtc
            };
        }
    }
}
