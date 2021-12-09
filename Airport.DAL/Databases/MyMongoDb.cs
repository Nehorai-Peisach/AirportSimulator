using MongoDB.Driver;

namespace Airport.DAL.Databases
{
    class MyMongoDb
    {
        public IMongoDatabase Client;
        string connectionString = "mongodb+srv://Admin:airport123@airportdb.fjv5s.mongodb.net/AirportDb?retryWrites=true&w=majority";
        
        public MyMongoDb(string database)
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            var client = new MongoClient(settings);
            Client = client.GetDatabase(database);
        }
    }
}
