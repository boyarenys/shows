using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("average")]
        public double Average { get; set; }
    }
}
