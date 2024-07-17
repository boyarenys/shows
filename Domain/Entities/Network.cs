using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Domain.Entities
{
    public class Network
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public Country? Country { get; set; }

        [JsonProperty("officialSite")]
        public string? OfficialSite { get; set; }

    }
}
