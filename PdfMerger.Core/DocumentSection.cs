using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMerger.Core
{
    public class DocumentSection
    {
        public DocumentSection(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<int> Pages { get; set; } = new List<int>();
    }
}
