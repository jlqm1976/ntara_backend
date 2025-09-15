using CollegeFootball.Domain.Interfaces.Repositories;
using CollegeFootball.Domain.Interfaces.Services;
using CollegeFootball.Repositories.DataContext;
using CollegeFootball.Repositories.Implementations;
using CollegeFootball.Repositories.Mappers;
using CollegeFootball.Services.Implementations;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TeamScoreDataContext>(options => {
    options.UseSqlite("Data Source=Database/FootballScores.db");
}, 
ServiceLifetime.Scoped);

builder.Services.AddScoped<ISearchableColumnRepository, SearchableColumnRepository>();
builder.Services.AddScoped<ITeamScoreSqlRepository, TeamScoreSqlRepository>();
builder.Services.AddScoped<ITeamScoreCsvRepository, TeamScoreCsvRepository>();
builder.Services.AddScoped<ITeamScoreDTOMapper, TeamScoreDTOMap>();

builder.Services.AddTransient<ITeamScoreService, TeamScoreService>();
builder.Services.AddTransient<ISearchableColumnService, SearchableColumnService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(sw =>{
        sw.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

//add cors
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
