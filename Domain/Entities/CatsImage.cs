
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CatsImage
    {
        public string id { get; set; } = null!;
        public string url { get; set; } = null!;
        public int width { get; set; }
        public int height { get; set; }
    }
}
