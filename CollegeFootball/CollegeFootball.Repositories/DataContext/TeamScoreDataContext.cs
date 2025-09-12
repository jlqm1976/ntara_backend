using CollegeFootball.Domain.Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Repositories.DataContext
{
    public class TeamScoreDataContext : DbContext
    {
        public TeamScoreDataContext(DbContextOptions<TeamScoreDataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TeamScore> TeamScores { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<TeamScore>(tb=>
            {
                tb.ToTable("TeamScore");
                tb.HasKey(ts => ts.Id);

                tb.Property(ts => ts.TeamName).IsRequired().HasMaxLength(100);
                tb.Property(ts => ts.MascotName).IsRequired().HasMaxLength(100);
                tb.Property(ts => ts.Rank);
                tb.Property(ts => ts.LastWinDate).IsRequired().HasMaxLength(10);
                tb.Property(ts => ts.WinningPercentage);
                tb.Property(ts => ts.TotalLosses);
                tb.Property(ts => ts.TotalTies);
                tb.Property(ts => ts.TotalWins);
                tb.Property(ts => ts.TotalGames);
            });
        }
    }
}
