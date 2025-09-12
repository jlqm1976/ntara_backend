using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Interfaces.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Services.Implementations
{
    public class TeamScoreService:ITeamScoreService
    {
        private readonly ITeamScoreSqlRepository sqlRepo;
        private readonly ITeamScoreCsvRepository csvRepo;

        public TeamScoreService(ITeamScoreSqlRepository _sqlRepo, ITeamScoreCsvRepository _csvRepo)
        {
            this.sqlRepo = _sqlRepo;
            this.csvRepo = _csvRepo;
        }

        public async Task<bool> ImportRecordsFromCsv(string csvFilePath)
        {
            
        }
    }
}
