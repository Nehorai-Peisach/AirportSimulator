using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Airport.Models
{
    public class Plane
    {
        [BsonId]
        public Guid PlaneId { get; set; }
        public string PlaneName { get; set; }
    }
}
