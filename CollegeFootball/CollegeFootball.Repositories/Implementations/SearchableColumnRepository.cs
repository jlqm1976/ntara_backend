using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Repositories.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Repositories.Implementations
{
    public class SearchableColumnRepository : ISearchableColumnRepository
    {
        private readonly TeamScoreDataContext dbCtx;
        public SearchableColumnRepository(TeamScoreDataContext ctx)
        {
            dbCtx = ctx;
        }

        public IEnumerable<SearchableColumn> GetAll()
        {
            return dbCtx.SearchableColumns.OrderBy(c => c.DisplayOrder).ToList();
        }
    }
}
