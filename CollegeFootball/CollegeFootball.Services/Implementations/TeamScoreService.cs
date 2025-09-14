using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Exceptions;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Interfaces.Services;

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

        public bool ImportRecordsFromCsv(string csvFilePath)
        {
            var dtoRecords = csvRepo.ReadCsv(csvFilePath);

            if (dtoRecords == null || !dtoRecords.Any())
                throw new NoRecordsInCsvException("No records found in the provided CSV file.");

            sqlRepo.DeleteAll();

            foreach (var dtoRec in dtoRecords)
            {
                var sqlRec = dtoMapper.MapToEntity(dtoRec);
                sqlRepo.Add(sqlRec);
            }

            return true;
        }

        public IEnumerable<TeamScore> GetAll()
        {
            return sqlRepo.GetAll();
        }
        public IEnumerable<TeamScore> Search(string searchValue, IEnumerable<string> searchFields)
        {
            return sqlRepo.Search(searchValue, searchFields);
        }
    }
}
