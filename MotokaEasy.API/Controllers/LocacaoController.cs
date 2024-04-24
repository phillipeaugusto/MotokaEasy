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
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Api.Controllers;

[Route("v1/locacao")]
public class LocacaoController: ControllerBase
{
    [HttpPost]
    [Authorize(Roles = RolesConstant.RoleAdmUser)]
    public async Task<ActionResult> Criar(
        CancellationToken ct,
        [FromServices] LocacaoService service,
        [FromBody] LocacaoInputDto input
    )
    
    {
        try
        {
            var serviceCommand = new CriarLocacaoCommand(input);
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
    
    [HttpPost]
    [Route("informar-data-devolucao")]
    [Authorize(Roles = RolesConstant.RoleAdmUser)]
    public async Task<ActionResult> InformarDataDevolucao(
        CancellationToken ct,
        [FromServices] LocacaoService service,
        [FromBody] InformarDataDevolucaoInputDto input
    )
    
    {
        try
        {
            var serviceCommand = new InformarDataDevolucaoLocacaoCommand(input);
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
    [Route("{locacaoId:guid}/detalhes-valores")]
    [Authorize(Roles = RolesConstant.RoleAdmUser)]
    public async Task<ActionResult> DetalhesValoresLocacao(
        CancellationToken ct,
        Guid locacaoId,
        [FromServices] LocacaoService service
    )
    
    {
        try
        {
            var serviceCommand = new ObterDetalhesValoresLocacaoCommand(locacaoId);
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
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> ObterTudoViaPaginacao(
        CancellationToken ct,
        [FromServices] LocacaoService service,
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
}