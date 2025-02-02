using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models
{
    public class StoreInformation
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("Address2")]
        public string Address2 { get; set; }
        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("EmailAdres")]
        public string EmailAdres { get; set; }
        [JsonProperty("PostCode")]
        public string PostCode { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("Descriptions")]
        public string Descriptions { get; set; }
    }
}