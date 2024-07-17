using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Self
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("href")]
        public string href { get; set; }
        public ICollection<Link> Links { get; set; }
    }
}
