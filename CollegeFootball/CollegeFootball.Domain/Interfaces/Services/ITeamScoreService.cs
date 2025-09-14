using CollegeFootball.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Interfaces.Services
{
    public interface ITeamScoreService
    {
        bool ImportRecordsFromCsv(string csvFilePath);
        List<TeamScore> GetAll();
        List<TeamScore> Search(string searchValue, IEnumerable<string> searchFields);
    }
}
