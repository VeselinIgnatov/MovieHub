using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MovieHub.Data.Models;
using MovieHub.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Data.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected readonly IMongoCollection<T> _data;
        protected readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(
            MovieHubContext<T> context, 
            string collectionName, 
            ILogger<BaseRepository<T>> logger) 
        { 
            _data = context.GetEntities(collectionName);
            _logger = logger;
        }

        public async Task<bool> CollectionHasData() => await _data.EstimatedDocumentCountAsync() > 0;
    }
}
