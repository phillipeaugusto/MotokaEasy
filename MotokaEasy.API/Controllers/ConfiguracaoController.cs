using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotokaEasy.Application.Services;
using MotokaEasy.Core.Commands;
using MotokaEasy.Core.ConstantsApp;
using MotokaEasy.Core.Infrastructure.Pagination;

namespace MotokaEasy.Api.Controllers;

[Route("v1/configuracao")]
public class ConfiguracaoController: ControllerBase
{
    [HttpGet]
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> ObterTudoViaPaginacao(
        CancellationToken ct,
        [FromServices] ConfiguracaoService service,
        [FromQuery] PaginationParameters paginationParameters
    )
    
    {
        try
        {
            var serviceCommand = new GetAllPaginationCommand(paginationParameters);
            var result = await service.Service(serviceCommand, ct);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    
    [HttpGet]
    [Route("{id:guid}")]
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> BuscarViaId(
        [FromServices] ConfiguracaoService service,
        CancellationToken ct,
        Guid id
    )
    
    {
        try
        {
            var serviceCommand = new GetByIdCommand(id);
            var result = await service.Service(serviceCommand, ct);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}