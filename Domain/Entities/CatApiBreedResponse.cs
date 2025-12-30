using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CatApiBreedResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Origin { get; set; } = null!;
        public string Temperament { get; set; } = null!;
        public string Description { get; set; } = null!;

        [JsonPropertyName("life_span")]
        public string LifeSpan { get; set; } = null!;

        [JsonPropertyName("reference_image_id")]
        public string ReferenceImageId { get; set; } = null!;
    }
}
