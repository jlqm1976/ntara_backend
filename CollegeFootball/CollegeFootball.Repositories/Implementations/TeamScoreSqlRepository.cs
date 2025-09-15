using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Exceptions;
using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Utils;
using CollegeFootball.Repositories.DataContext;
using System.Linq.Expressions;

namespace CollegeFootball.Repositories.Implementations
{
    public class TeamScoreSqlRepository : ITeamScoreSqlRepository
    {
        #region Private Members

        private readonly TeamScoreDataContext dbCtx;

        private Expression<Func<TeamScore, bool>> GetLamdaSearchExpression(string searchTerm, IEnumerable<string> propertiesToSearch)
        {
            var parameter = Expression.Parameter(typeof(TeamScore), "ts");
            Expression finalExpr = Expression.Constant(false);

            foreach (var propName in propertiesToSearch)
            {
                Expression expr = null!;

                switch (propName)
                {
                    case "LastWinDate":
                        var winDateInfo = MiscUtils.GetDateValue(searchTerm);

                        if (winDateInfo.HasValue)
                        {
                            var propWinDate = Expression.Property(parameter, "LastWinDate");
                            expr = Expression.Equal(propWinDate, Expression.Constant(winDateInfo.Value.ToString("MM/dd/yyyy"), propWinDate.Type));
                        }

                        break;

                    case "TeamName":
                        var propTeamName = Expression.Property(parameter, "TeamName");
                        var toLowerCallTeamName = Expression.Call(propTeamName, nameof(string.ToLower), Type.EmptyTypes);
                        expr = Expression.Call(toLowerCallTeamName, nameof(string.Contains), Type.EmptyTypes, Expression.Constant(searchTerm));

                        break;

                    case "MascotName":
                        var propMascotName = Expression.Property(parameter, "MascotName");
                        var toLowerCallMascotName = Expression.Call(propMascotName, nameof(string.ToLower), Type.EmptyTypes);
                        expr = Expression.Call(toLowerCallMascotName, nameof(string.Contains), Type.EmptyTypes, Expression.Constant(searchTerm));

                        break;

                    case "Rank":
                        var rankInfo = MiscUtils.GetIntValue(searchTerm);

                        if (rankInfo.HasValue)
                        {
                            var property = Expression.Property(parameter, "Rank"); // int?
                            expr = Expression.Equal(property, Expression.Constant(rankInfo.Value, property.Type));
                        }

                        break;

                    case "TotalLosses":
                        var lossesInfo = MiscUtils.GetIntValue(searchTerm);

                        if (lossesInfo.HasValue)
                        {
                            var property = Expression.Property(parameter, "TotalLosses"); // int?
                            expr = Expression.Equal(property, Expression.Constant(lossesInfo.Value, property.Type));
                        }

                        break;

                    case "TotalGames":
                        var gamesInfo = MiscUtils.GetIntValue(searchTerm);

                        if (gamesInfo.HasValue)
                        {
                            var property = Expression.Property(parameter, "TotalGames"); // int?
                            expr = Expression.Equal(property, Expression.Constant(gamesInfo.Value, property.Type));
                        }

                        break;

                    case "TotalTies":
                        var tiesInfo = MiscUtils.GetIntValue(searchTerm);

                        if (tiesInfo.HasValue)
                        {
                            var property = Expression.Property(parameter, "TotalTies"); // int?
                            expr = Expression.Equal(property, Expression.Constant(tiesInfo.Value, property.Type));
                        }

                        break;

                    case "TotalWins":
                        var winsInfo = MiscUtils.GetIntValue(searchTerm);

                        if (winsInfo.HasValue)
                        {
                            var property = Expression.Property(parameter, "TotalWins"); // int?
                            expr = Expression.Equal(property, Expression.Constant(winsInfo.Value, property.Type));
                        }

                        break;

                    case "WinningPercentage":
                        var winPctInfo = MiscUtils.GetDoubleValue(searchTerm);

                        if (winPctInfo.HasValue)
                        {
                            var property = Expression.Property(parameter, "WinningPercentage"); // float?
                            expr = Expression.Equal(property, Expression.Constant(winPctInfo.Value, property.Type));
                        }

                        break;

                    default:
                        break;

                } // End of switch

                if (expr != null)
                {
                    finalExpr = Expression.OrElse(finalExpr, expr);
                }

            } // End of foreach
            
            if(finalExpr == Expression.Constant(false))
            {
                return null!;
            }
            else
            {
                var lambda = Expression.Lambda<Func<TeamScore, bool>>(finalExpr, parameter);

                return lambda;
            }
        }

        #endregion

        #region Public Members

        public TeamScoreSqlRepository(TeamScoreDataContext ctx)
        {
            dbCtx = ctx;
        }

        public List<TeamScore> GetAll()
        {
            return dbCtx.TeamScores.ToList();
        }

        public List<TeamScore> Search(string searchValue, IEnumerable<string> columnNames)
        {
            string searchTerm = searchValue.ToLower() ?? "";

            if (string.IsNullOrEmpty(searchTerm))
            {
                throw new SearchValueMissingException("Search value must be provided.");
            }
            else
            {
                IQueryable<TeamScore> query = dbCtx.TeamScores;

                // If no specific columns are provided then get all properties names in TeamScore class
                var propertiesToSearch = columnNames?.Count() > 0 ? columnNames
                    : typeof(TeamScore).GetProperties().Where(p => p.CanWrite).Select(p => p.Name);

                var lambda = GetLamdaSearchExpression(searchTerm, propertiesToSearch);

                return query.Where(lambda).ToList();
            }
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

        #endregion
    }
}
