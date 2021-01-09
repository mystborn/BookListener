using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Extractors
{
    public interface ITextExtractor
    {
        Book ExtractBook();
    }
}
