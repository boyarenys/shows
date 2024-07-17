using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Link
    {
        [Key]
        public int Id { get; set; }
        public int SelfId { get; set; }
        public Self Self { get; set; }

        [JsonProperty("previousepisodeId")]
        public int PreviousepisodeId { get; set; }
        public Previousepisode Previousepisode { get; set; }
    }
}
