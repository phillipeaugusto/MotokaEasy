using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotokaEasy.Application.Services;
using MotokaEasy.Core.ConstantsApp;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Api.Controllers;

[Route("v1/entregador")]
public class EntregadorController: ControllerBase
{
    [HttpPost]
    [Authorize(Roles = RolesConstant.RoleAdmUser)]
    public async Task<ActionResult> Criar(
        CancellationToken ct,
        [FromServices] EntregadorService service,
        [FromBody] EntregadorInputDto input
    )
    
    {
        try
        {
            var serviceCommand = new CriarEntregadorCommand(input);
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
    [Route("{entregadorId:guid}/upload-imagem")]
    [Authorize(Roles = RolesConstant.RoleAdmUser)]
    public async Task<ActionResult> UploadImagemEntregador(
        CancellationToken ct,
        Guid entregadorId,
        [FromServices] EntregadorService service,
        [Required] IFormFile file
    )
    
    {
        try
        {
            var serviceCommand = new UploadDocumentoEntregadorCommand(entregadorId, file);
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