using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Interfaces.Services
{
    public interface ITeamScoreService
    {
        bool AddRecordsFromCsv(string csvFilePath);
    }
}
