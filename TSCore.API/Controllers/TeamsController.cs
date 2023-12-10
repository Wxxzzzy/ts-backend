using Microsoft.AspNetCore.Mvc;
using TSCore.API.Common.Authorization;
using TSCore.Domain.Enums.Authentication;

namespace TSCore.API.Controllers;

[AuthorizeMode(ERoles.Admin, ERoles.User)]
public class TeamsController : BaseController
{
    [HttpGet("my-teams")]
    public async Task GetUserTeams(int userId)
    {
        
    }

    [HttpGet("{id}")]
    public async Task GetTeam([FromRoute] int teamId)
    {
        
    }

    [HttpPost]
    public async Task CreateTeam()
    {
        
    }

    [HttpDelete]
    public async Task DeleteTeam()
    {
        
    }
}