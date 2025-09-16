using CollegeFootball.Domain.Entities;

namespace CollegeFootball.Domain.Interfaces.Repositories
{
    public interface ITeamScoreSqlRepository
    {
        List<TeamScore> GetAll();
        List<TeamScore> Search(string searchValue, IEnumerable<string> searchFields);
        void Add(TeamScore teamScore);
        void Update(TeamScore teamScore);
        void DeleteAll();
    }
}
