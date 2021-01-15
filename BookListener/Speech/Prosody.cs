using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookListener.Speech
{
    public class Prosody
    {
        public string Pitch { get; set; }
        public string Contour { get; set; }
        public string Range { get; set; }
        public string Rate { get; set; }
        public string Duration { get; set; }
        public string Volume { get; set; }

        public string OpeningTag()
        {
            var sb = new StringBuilder();
            sb.Append("<prosody");

            if(!string.IsNullOrWhiteSpace(Pitch))
            {
                sb.Append(" pitch=\"");
                sb.Append(Pitch);
                sb.Append("\"");
            }

            if (!string.IsNullOrWhiteSpace(Contour))
            {
                sb.Append(" contour=\"");
                sb.Append(Contour);
                sb.Append("\"");
            }

            if (!string.IsNullOrWhiteSpace(Range))
            {
                sb.Append(" range=\"");
                sb.Append(Range);
                sb.Append("\"");
            }

            if (!string.IsNullOrWhiteSpace(Rate))
            {
                sb.Append(" rate=\"");
                sb.Append(Rate);
                sb.Append("\"");
            }

            if (!string.IsNullOrWhiteSpace(Duration))
            {
                sb.Append(" duration=\"");
                sb.Append(Duration);
                sb.Append("\"");
            }

            if (!string.IsNullOrWhiteSpace(Volume))
            {
                sb.Append(" volume=\"");
                sb.Append(Volume);
                sb.Append("\"");
            }

            sb.Append(">");

            return sb.ToString();
        }
    }
}
