using CollegeFootball.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Interfaces.Repositories
{
    public interface ISearchableColumnRepository
    {
        IEnumerable<SearchableColumn> GetAll();
    }
}
