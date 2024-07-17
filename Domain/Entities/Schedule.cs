using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
        public List<string> Days { get; set; }
    }
}
