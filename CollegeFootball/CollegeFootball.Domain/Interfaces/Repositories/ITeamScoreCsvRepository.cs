using CollegeFootball.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Interfaces.Repositories
{
    public interface ITeamScoreCsvRepository
    {
        Task<IEnumerable<TeamScoreDTO>> ReadCsvAsync(string filePath);
    }
}
