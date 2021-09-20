using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Teste
{
    public class WeatherForecast
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [ReadOnly(true)]
        [DefaultValue(null)]
        public string Id { get; set; }

        [ReadOnly(true)]
        public DateTime Date { get; set; }

        [Required]
        [DefaultValue(null)]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required]
        [DefaultValue(null)]
        public string Summary { get; set; }

    }
}
