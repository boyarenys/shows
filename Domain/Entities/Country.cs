using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Country
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("timezone")]
        public string? Timezone { get; set; }
        public ICollection<Network> Networks { get; set; }

        public ICollection<Show> Shows { get; set; }

    }
}
