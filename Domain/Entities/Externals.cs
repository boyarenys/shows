using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Externals
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("tvrage")]
        public int Tvrage { get; set; }

        [JsonProperty("thetvdb")]
        public int Thetvdb { get; set; }

        [JsonProperty("imdb")]
        public string? Imdb { get; set; }
    }
}
