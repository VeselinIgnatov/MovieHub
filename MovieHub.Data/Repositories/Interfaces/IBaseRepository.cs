using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHub.Data.Repositories.Interfaces
{
    public interface IBaseRepository
    {
        Task<bool> CollectionHasData();
    }
}
