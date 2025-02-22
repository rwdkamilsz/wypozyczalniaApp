using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wypozyczalniaApp
{
    public class Book : Item
    {
        public int ISBN { get; set; }
        public string? Author { get; set; }
        public bool IsBorrowed { get; set; }
    }
}
