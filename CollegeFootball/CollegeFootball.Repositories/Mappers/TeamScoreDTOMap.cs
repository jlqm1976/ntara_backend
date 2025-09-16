using CollegeFootball.Domain.DTO;
using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Repositories;

namespace CollegeFootball.Repositories.Mappers
{
    public class TeamScoreDTOMap : ITeamScoreDTOMapper
    {
        public TeamScore MapToEntity(TeamScoreDTO obj)
        {
            TeamScore ts = new TeamScore();

            if (int.TryParse(obj.Rank, out int _rank))
            {
                ts.Rank = _rank;
            }
            else
            {
                ts.Rank = null;
            }

            ts.TeamName = obj.TeamName ?? "";
            ts.MascotName = obj.MascotName ?? "";

            if (DateTime.TryParse(obj.LastWinDate, out DateTime _lastWinDate))
            {
                ts.LastWinDate = _lastWinDate.ToString("MM/dd/yyyy");
            }
            else
            {
                ts.LastWinDate = "";
            }

            // parse winning percentage
            if (float.TryParse(obj.WinningPercentage, out float _winningPercentage))
            {
                ts.WinningPercentage = _winningPercentage;
            }
            else
            {
                ts.WinningPercentage = null;
            }

            // parse total wins
            if (int.TryParse(obj.TotalWins, out int _totalWins))
            {
                ts.TotalWins = _totalWins;
            }
            else
            {
                ts.TotalWins = null;
            }

            // parse total losses
            if (int.TryParse(obj.TotalLosses, out int _totalLosses))
            {
                ts.TotalLosses = _totalLosses;
            }
            else
            {
                ts.TotalLosses = null;
            }

            // parse total ties
            if (int.TryParse(obj.TotalTies, out int _totalTies))
            {
                ts.TotalTies = _totalTies;
            }
            else
            {
                ts.TotalTies = null;
            }

            // parse total games
            if (int.TryParse(obj.TotalGames, out int _totalGames))
            {
                ts.TotalGames = _totalGames;
            }
            else
            {
                ts.TotalGames = null;
            }

            return ts;
        }
    }
}
