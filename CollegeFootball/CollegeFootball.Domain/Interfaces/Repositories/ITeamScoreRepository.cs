using CollegeFootball.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Interfaces.Repositories
{
    public interface ITeamScoreRepository
    {
        List<TeamScore> GetAll();
        TeamScore GetById(int id);
        bool Add(TeamScore teamScore);
        bool Update(TeamScore teamScore);
    }
}
