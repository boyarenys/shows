using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        // Navigation property para los programas de TV que tienen esta imagen
        public List<Show> Shows { get; set; }
    }
}
