using MongoDB.Driver;
using MovieHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Data.Repositories.Interfaces
{
    public interface IShowRepository : IBaseRepository
    {
        IQueryable<Show> GetShows();

        Task<Show> GetShow(int id);

        Task AddShows(List<Show> shows);
    }
}
