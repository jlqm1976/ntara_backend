
using CollegeFootball.Domain.Entities;

namespace CollegeFootball.Domain.Interfaces.Services
{
    public interface ISearchableColumnService
    {
        List<SearchableColumn> GetAll();
    }
}
