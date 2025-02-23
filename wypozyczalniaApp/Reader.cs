using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wypozyczalniaApp
{
    public class Reader : Person
    {
        public string? Phone { get; set; }
        public int BooksBorrowed { get; set; }
    }
}
