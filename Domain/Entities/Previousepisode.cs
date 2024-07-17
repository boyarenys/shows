using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Previousepisode
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public ICollection<Link> Links { get; set; }
    }
}
