using System;
using System.Text.Json.Serialization;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collection
{
    public class Infected
    {
        public long Cpf { get; set; }
        
        [JsonIgnore]
        public bool Status {get; set;}
        
        public DateTime DateBith {get; set;}
        
        public string Genre { get; set; }
        
        public GeoJson2DGeographicCoordinates Localization {get; set;}
        
        public Infected(long cpf)
        {
            Cpf = cpf;
        }
        public Infected(long cpf, DateTime dateBirth, string genre, double latitude, double longitude){
            Cpf = cpf;
            DateBith = dateBirth;
            Genre = genre;
            this.Localization = new GeoJson2DGeographicCoordinates(latitude, longitude);    
        }
    }
}