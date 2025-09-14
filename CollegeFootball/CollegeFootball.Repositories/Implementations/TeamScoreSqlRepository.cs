using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Utils;
using CollegeFootball.Repositories.DataContext;

namespace CollegeFootball.Repositories.Implementations
{
    public class TeamScoreSqlRepository : ITeamScoreSqlRepository
    {
        private readonly TeamScoreDataContext dbCtx;
        public TeamScoreSqlRepository(TeamScoreDataContext ctx)
        {
            dbCtx = ctx;
        }

        public IEnumerable<TeamScore> GetAll()
        {
            return dbCtx.TeamScores.ToList();
        }

        public IEnumerable<TeamScore> Search(string searchValue, IEnumerable<string> columnNames)
        {
            string searchTerm = searchValue.ToLower() ?? "";
            IQueryable<TeamScore> query = dbCtx.TeamScores;

            // If no specific columns are provided then get all properties names in TeamScore class
            var propertiesToSearch = columnNames?.Count() > 0 ? columnNames
                : typeof(TeamScore).GetProperties().Where(p => p.CanWrite).Select(p => p.Name);

            Func<TeamScore, bool> predicate = ts => false;

            foreach (var propName in propertiesToSearch)
            {
                switch (propName)
                {
                    case "LastWinDate":
                        var winDateInfo = MiscUtils.GetDateValue(searchTerm);

                        if (winDateInfo.HasValue)
                        {
                            string dateValue = winDateInfo.Value.ToString("MM/dd/yyyy");
                            predicate = ts => predicate(ts) || (!string.IsNullOrEmpty(ts.LastWinDate) && ts.LastWinDate == dateValue);
                        }

                        break;

                    case "TeamName":
                        predicate = ts => predicate(ts) || (!string.IsNullOrEmpty(ts.TeamName) && ts.TeamName.ToLower().Contains(searchTerm));
                        break;

                    case "MascotName":
                        predicate = ts => predicate(ts) || (!string.IsNullOrEmpty(ts.MascotName) && ts.MascotName.ToLower().Contains(searchTerm));
                        break;

                    case "Rank":
                        var rankInfo = MiscUtils.GetIntValue(searchTerm);

                        if (rankInfo.HasValue)
                        {
                            predicate = ts => predicate(ts) || (ts.Rank == rankInfo.Value);
                        }

                        break;

                    case "TotalLosses":
                        var lossesInfo = MiscUtils.GetIntValue(searchTerm);
                        
                        if (lossesInfo.HasValue)
                        {
                            predicate = ts => predicate(ts) || (ts.TotalLosses == lossesInfo.Value);
                        }

                        break;

                    case "TotalGames":
                        var gamesInfo = MiscUtils.GetIntValue(searchTerm);

                        if (gamesInfo.HasValue)
                        {
                            predicate = ts => predicate(ts) || (ts.TotalGames == gamesInfo.Value);
                        }

                        break;

                    case "TotalTies":
                        var tiesInfo = MiscUtils.GetIntValue(searchTerm);

                        if (tiesInfo.HasValue)
                        {
                            predicate = ts => predicate(ts) || (ts.TotalTies == tiesInfo.Value);
                        }

                        break;

                    case "TotalWins":
                        var winsInfo = MiscUtils.GetIntValue(searchTerm);

                        if (winsInfo.HasValue)
                        {
                            predicate = ts => predicate(ts) || (ts.TotalWins == winsInfo.Value);
                        }

                        break;

                    case "WinningPercentage":
                        var winPctInfo = MiscUtils.GetFloatValue(searchTerm);

                        if (winPctInfo.HasValue)
                        {
                            predicate = ts => predicate(ts) || (ts.WinningPercentage == winPctInfo.Value);
                        }

                        break;

                    default:
                        break;
                }
            }

            var result = query.Where(predicate).ToList();

            return result;
        }

        public void Add(TeamScore teamScore)
        {
            dbCtx.TeamScores.Add(teamScore);
            dbCtx.SaveChanges();
        }

        public void Update(TeamScore teamScore)
        {
            dbCtx.TeamScores.Update(teamScore);
            dbCtx.SaveChanges();
        }

        public void DeleteAll()
        {
            dbCtx.TeamScores.RemoveRange(dbCtx.TeamScores);
            dbCtx.SaveChanges();
        }
    }
}
