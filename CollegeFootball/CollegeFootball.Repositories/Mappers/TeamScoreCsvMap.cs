using CollegeFootball.Domain.DTO;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Repositories.Mappers
{
    public class TeamScoreCsvMap : ClassMap<TeamScoreDTO>
    {
        public TeamScoreCsvMap()
        {
            Map(m => m.Rank).Name("Rank");
            Map(m => m.TeamName).Name("Team");
            Map(m => m.MascotName).Name("Mascot");
            Map(m => m.LastWinDate).Name("Date of Last Win");
            Map(m => m.MascotName).Name("Mascot");
            Map(m => m.WinningPercentage).Name("Winning Percetnage");
            Map(m => m.TotalWins).Name("Wins");
            Map(m => m.TotalLosses).Name("Losses");
            Map(m => m.TotalTies).Name("Ties");
            Map(m => m.TotalGames).Name("Games");
        }
    }
}
