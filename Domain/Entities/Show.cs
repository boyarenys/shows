using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Show
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
       
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
       
        [JsonProperty("runtime")]
        public int? Runtime { get; set; }
        
        [JsonProperty("averageRuntime")]
        public int? AverageRuntime { get; set; }

        [JsonProperty("premiered")]
        public DateTime? Premiered { get; set; }

        [JsonProperty("ended")]
        public DateTime? Ended { get; set; }

        [JsonProperty("officialSite")]
        public string? OfficialSite { get; set; }

        [JsonProperty("schedule")]
        public Schedule Schedule { get; set; }

        [JsonProperty("rating")]
        public Rating Rating { get; set; }
        
        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("network")]
        public Network? Network { get; set; }

        [JsonProperty("webChannel")]
        public WebChannel WebChannel { get; set; }

        [JsonProperty("dvdCountry")]
        public Country DvdCountry  { get; set; }

        [JsonProperty("externals")]
        public Externals Externals { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }
        public string? Summary { get; set; }
        public long Updated { get; set; }

        [JsonProperty("_links")]
        public Link Links { get; set; }

        // Relationships
        public int? NetworkId { get; set; }   
        public int ScheduleId { get; set; }
        
        public int RatingId { get; set; }
        
        public int ExternalsId { get; set; }

        public int ImageId { get; set; }       

        public int LinkId { get; set; }
       
        public int? WebChannelId { get; set; }
        public int? CountryId { get; set; }



    }
}
