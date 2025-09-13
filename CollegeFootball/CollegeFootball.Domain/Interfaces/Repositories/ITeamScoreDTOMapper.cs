using CollegeFootball.Domain.DTO;
using CollegeFootball.Domain.Entities;

namespace CollegeFootball.Domain.Interfaces.Repositories
{
    public interface ITeamScoreDTOMapper
    {
        TeamScore MapToEntity(TeamScoreDTO teamScoreDTO);
    }
}
