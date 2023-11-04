using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieHub.Utilities.Configurations;

namespace MovieHub.Data
{

    public class MovieHubContext<T>
    {
        private readonly IMongoDatabase _database;

        public MovieHubContext(IOptions<BaseDataConfiguration> configuration)
        {
            var connectionString = configuration.Value.ConnectionString;
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(configuration.Value.DatabaseName);
        }

        public IMongoCollection<T> GetEntities(string collectionName) 
            => _database.GetCollection<T>(collectionName);
    }
}
