using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotokaEasy.Application.Services;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Dto.InputDto;

namespace MotokaEasy.Api.Controllers;

[Route("v1/login")]
public class LoginController: ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Login(
        CancellationToken ct,
        [FromServices] UsuarioService service,
        [FromBody] UsuarioInputLogin usuarioInputLogin
    )

    {
        try
        {
            var serviceCommand = new ObterTokenUsuarioCommand(usuarioInputLogin);
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