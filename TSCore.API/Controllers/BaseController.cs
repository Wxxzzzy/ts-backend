using System.Runtime.InteropServices.ComTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSCore.API.Swagger;
using TSCore.Application.Common.Models;

namespace TSCore.API.Controllers;

[ApiExplorerSettings(GroupName = SwaggerSetup.TSAPI)]
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    private AppConfiguration _configuration;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected AppConfiguration Configuration =>
        _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>().Get<AppConfiguration>();

    protected ActionResult<TResult> Ok<TResult>(TResult value)
    {
        return base.Ok(value);
    }
}