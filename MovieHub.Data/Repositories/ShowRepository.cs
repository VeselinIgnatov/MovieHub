using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieHub.Data.Models;
using MovieHub.Data.Repositories.Interfaces;
using MovieHub.Utilities.Configurations.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Data.Repositories
{
    public class ShowRepository : BaseRepository<Show>, IShowRepository
    {
        public ShowRepository(
            MovieHubContext<Show> context, 
            IOptions<ShowCollection> options,
            ILogger<ShowRepository> logger)
            : base(context, options.Value.CollectionName, logger)
        { }

        public async Task<Show> GetShow(int id)
        {
            var show = await _data.FindAsync(show => show.Id == id);
            return await show.FirstOrDefaultAsync();
        }

        public IQueryable<Show> GetShows()
        {
            return _data.AsQueryable();
        }

        public async Task AddShows(List<Show> newShows)
        {
            await _data.InsertManyAsync(newShows);
        }
    }
}
