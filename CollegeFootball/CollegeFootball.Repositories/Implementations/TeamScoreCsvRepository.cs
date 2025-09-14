using CollegeFootball.Domain.DTO;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Repositories.Mappers;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Repositories.Implementations
{
    public class TeamScoreCsvRepository : ITeamScoreCsvRepository
    {
        public TeamScoreCsvRepository()
        {
        }

        public IEnumerable<TeamScoreDTO> ReadCsv(string filePath)
        {
            List<TeamScoreDTO> tsRecords = null!;

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file at {filePath} was not found.");
            }

            using (var fileReader = File.OpenText(filePath))
            {
                using (var csvReader = new CsvReader(fileReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<TeamScoreCsvMap>();

                    tsRecords = new List<TeamScoreDTO>();

                    foreach (var tsRecord in csvReader.GetRecords<TeamScoreDTO>())
                    {
                        tsRecords.Add(tsRecord);
                    }
                }
            }

            return tsRecords;
        }
    }
}
