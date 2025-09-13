using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Entities
{
    public class TeamScore
    {
        public int Id { get; set; }
        public int? Rank { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string MascotName { get; set; } = string.Empty;
        public string LastWinDate { get; set; } = string.Empty;
        public float? WinningPercentage { get; set; }
        public int? TotalLosses { get; set; }
        public int? TotalTies { get; set; }
        public int? TotalWins { get; set; }
        public int? TotalGames { get; set; }
    }
}
