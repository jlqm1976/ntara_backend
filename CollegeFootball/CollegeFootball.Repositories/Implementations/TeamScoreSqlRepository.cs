using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Repositories.DataContext;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace CollegeFootball.Repositories.Implementations
{
    public class TeamScoreSqlRepository : ITeamScoreSqlRepository
    {
        private readonly TeamScoreDataContext dbCtx;
        public TeamScoreSqlRepository(TeamScoreDataContext ctx)
        {
            dbCtx = ctx;
        }

        public async Task<IEnumerable<TeamScore>> GetAllAsync(Expression<Func<TeamScore, bool>> filter)
        {
            if (filter == null)
            {
                return await dbCtx.TeamScores.ToListAsync();
            }
            else
            {
                return await dbCtx.TeamScores.Where(filter).ToListAsync();
            }
        }

        public async Task AddAsync(TeamScore teamScore)
        {
            await dbCtx.TeamScores.AddAsync(teamScore);
            await dbCtx.SaveChangesAsync();
        }

        public async Task UpdateAsync(TeamScore teamScore)
        {
            dbCtx.TeamScores.Update(teamScore);
            await dbCtx.SaveChangesAsync();
        }
    }
}
