using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Interfaces.Services;
using CollegeFootball.Domain.Exceptions;

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
        private readonly ITeamScoreDTOMapper dtoMapper;

        public TeamScoreService(ITeamScoreSqlRepository _sqlRepo, ITeamScoreCsvRepository _csvRepo, ITeamScoreDTOMapper dtoMapper)
        {
            this.sqlRepo = _sqlRepo;
            this.csvRepo = _csvRepo;
            this.dtoMapper = dtoMapper;
        }

        public async Task<bool> ImportRecordsFromCsv(string csvFilePath)
        {
            var dtoRecords = await csvRepo.ReadCsvAsync(csvFilePath);

            if (dtoRecords == null || !dtoRecords.Any())
                throw new NoRecordsInCsvException("No records found in the provided CSV file.");

            await sqlRepo.DeleteAllAsync();

            foreach (var dtoRec in dtoRecords)
            {
                var sqlRec = dtoMapper.MapToEntity(dtoRec);
                await sqlRepo.AddAsync(sqlRec);
            }

            return true;
        }
    }
}
