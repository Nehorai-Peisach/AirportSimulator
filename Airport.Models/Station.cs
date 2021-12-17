using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Threading;

namespace Airport.Models
{
    public class Station
    {
        [BsonId]
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public TimeSpan StationDuration { get; set; }
        public Plane Plane { get; set; }

        public object Loker = new object();
    }
}
