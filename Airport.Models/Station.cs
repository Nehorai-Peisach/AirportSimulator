using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Airport.Models
{
    public class Station
    {
        [BsonId]
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public Plane CurrentPlane { get; set; }
    }
}
