using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Airport.Models
{
    public class Station
    {
        [BsonId]
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public double StationDuration { get; set; }
        public Plane Plane { get; set; }
        public SemaphoreSlim Semaphore = new SemaphoreSlim(1);
    }
}
