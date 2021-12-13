using Airport.DAL.Databases;
using Airport.DAL.Interfaces;
using Airport.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Airport.DAL.Repositories
{
    public class PlaneRepository : IPlaneRepository
    {
        static string table = "Planes";
        static MyMongoDb db = new MyMongoDb("Airport");

        private IMongoCollection<Plane> GetCollection()
        {
            return db.Client.GetCollection<Plane>(table);
        }

        public void Add(Plane plane) => GetCollection().InsertOne(plane);

        public void Remove(Plane plane) => GetCollection().DeleteOne(Builders<Plane>.Filter.Eq(x => x.PlaneName, plane.PlaneName));

        public List<Plane> GetAll() => GetCollection().Find(new BsonDocument()).ToList();

        public Plane GetByName(string planeName)
        {
            var filter = Builders<Plane>.Filter.Eq(x => x.PlaneName, planeName);
            return GetCollection().Find(filter).FirstOrDefault();
        }
    }
}
