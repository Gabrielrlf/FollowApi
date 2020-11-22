using System;
using System.Text.Json.Serialization;

namespace Api.Models
{
    public class InfectedDTO
    {
        public long Cpf {get;set;}
        
        [JsonIgnore]
        public bool Status { get; set; }
        
        public DateTime DateBith { get; set; }
        
        public string Genre { get; set; }
        
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}