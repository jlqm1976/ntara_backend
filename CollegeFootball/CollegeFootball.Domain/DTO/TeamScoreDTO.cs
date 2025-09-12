using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.DTO
{
    public class TeamScoreDTO
    {
        public string Rank { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public string MascotName { get; set; } = string.Empty;
        public string LastWinDate { get; set; } = string.Empty;
        public string WinningPercentage { get; set; } = string.Empty;
        public string TotalLosses { get; set; } = string.Empty;
        public string TotalTies { get; set; } = string.Empty;
        public string TotalWins { get; set; } = string.Empty;
        public string TotalGames { get; set; } = string.Empty;
    }
}
