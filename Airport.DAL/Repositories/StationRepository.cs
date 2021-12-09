using Airport.DAL.Databases;
using Airport.DAL.Interfaces;
using Airport.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Airport.DAL.Repositories
{
    public class StationRepository : IStationRepository
    {
        static string table = "Stations";
        static MyMongoDb db = new MyMongoDb("Airport");
        
        private IMongoCollection<Station> GetCollection()
        {
            return db.Client.GetCollection<Station>(table);
        }

        public void Add(Station station) => GetCollection().InsertOne(station);

        public void Remove(Station station) => GetCollection().DeleteOne(Builders<Station>.Filter.Eq(x => x.StationId, station.StationId));

        public void Update(Station station) => GetCollection().ReplaceOne(Builders<Station>.Filter.Eq(x => x.StationId, station.StationId), station);

        public Station Get(string id) => GetCollection().Find(Builders<Station>.Filter.Eq(x => x.StationName, id)).FirstOrDefault();

        public List<Station> GetAll => GetCollection().Find(new BsonDocument()).ToList();
    }
}
