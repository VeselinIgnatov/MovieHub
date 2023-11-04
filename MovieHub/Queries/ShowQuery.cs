using HotChocolate.Authorization;
using MovieHub.Data.Models;
using MovieHub.Data.Repositories.Interfaces;

namespace MovieHub.Queries
{
    public class ShowQuery
    {
        [Authorize]
        [UsePaging]
        public IQueryable<Show> GetShows(IShowRepository showRepository, string genre, string name)
        {
            var shows = showRepository.GetShows();

            if (!string.IsNullOrEmpty(name))
            {
                shows = shows.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(genre))
            {
               shows = shows.Where(x => x.Genres.Contains(genre));
            }

            return shows;
        }
        [Authorize]
        public IEnumerable<string> GetDistinctGenres(IShowRepository showRepository)
        {
            var genres = showRepository.GetShows().SelectMany(show => show.Genres);

            var distinctGenres = genres.Distinct();

            return distinctGenres;
        }

    }
}
