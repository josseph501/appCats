using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cats.DTOs
{
    public class BreedDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Temperament { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string LifeSpan { get; set; } = null!;
        public string ImageId { get; set; } = null!;

    }
}
