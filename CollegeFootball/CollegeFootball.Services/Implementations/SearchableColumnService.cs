using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Interfaces.Services;

namespace CollegeFootball.Services.Implementations
{
    public class SearchableColumnService : ISearchableColumnService
    {
        private readonly ISearchableColumnRepository scRepo;

        public SearchableColumnService(ISearchableColumnRepository _scRepo)
        {
            scRepo = _scRepo;
        }

        public List<SearchableColumn> GetAll()
        {
            return scRepo.GetAll();
        }
    }
}
