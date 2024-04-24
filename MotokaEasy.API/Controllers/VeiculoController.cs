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

[Route("v1/veiculo")]
public class VeiculoController: ControllerBase
{
    [HttpPost]
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> Criar(
        CancellationToken ct,
        [FromServices] VeiculoService service,
        [FromBody] VeiculoInputDto input
    )
    
    {
        try
        {
            var serviceCommand = new CriarVeiculoCommand(input);
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
        [FromServices] VeiculoService service,
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
    [Route("buscar-via-placa/{placa}")]
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> BuscarPelaPlaca(
        CancellationToken ct,
        string placa,
        [FromServices] VeiculoService service
    )
    
    {
        try
        {
            var serviceCommand = new BuscarDadosDoVeiculoViaPlacaCommand(placa);
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
    
    [HttpPut]
    [Route("alterar-placa/{veiculoId:guid}")]
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> AlterarPlaca(
        CancellationToken ct,
        Guid veiculoId,
        [FromServices] VeiculoService service,
        [FromBody] AlterarPlacaInputDto alterarPlacaInputDto
    )
    
    {
        try
        {
            var serviceCommand = new AlterarPlacaVeiculoCommand(veiculoId, alterarPlacaInputDto);
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
    
    [HttpDelete]
    [Route("remover/{id:guid}")]
    [Authorize(Roles = RolesConstant.RoleAdministrator)]
    public async Task<ActionResult> RemoverVeiculo(
        CancellationToken ct,
        Guid id,
        [FromServices] VeiculoService service
    )
    
    {
        try
        {
            var serviceCommand = new RemoverVeiculoCommand(id);
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