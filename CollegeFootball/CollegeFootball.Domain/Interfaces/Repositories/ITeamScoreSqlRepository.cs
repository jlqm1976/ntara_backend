using CollegeFootball.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Interfaces.Repositories
{
    public interface ITeamScoreSqlRepository
    {
        Task<IEnumerable<TeamScore>> GetAllAsync(Expression<Func<TeamScore, bool>> filter);
        Task AddAsync(TeamScore teamScore);
        Task UpdateAsync(TeamScore teamScore);
    }
}
