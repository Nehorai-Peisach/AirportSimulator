using MongoDB.Driver;

namespace Airport.DAL.Databases
{
    class MyMongoDb
    {
        public IMongoDatabase Client;

        public MyMongoDb(string database)
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://Admin:airport123@airportdb.fjv5s.mongodb.net/AirportDb?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            Client = client.GetDatabase(database);
        }
    }
}
