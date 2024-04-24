using System;
using MotokaEasy.Domain.Command;
using MotokaEasy.Domain.Dto.InputDto;
using Xunit;

namespace MotokaEasy.Tests.UnitaryTests.Commands;

public class CriarVeiculoCommandTests
{
    private readonly CriarVeiculoCommand _invalidComand = new(new VeiculoInputDto{ModeloVeiculoId = Guid.Empty, TipoVeiculoId = Guid.Empty, Ano = 0, Placa = ""});
    private readonly CriarVeiculoCommand _validComand = new(new VeiculoInputDto{ModeloVeiculoId = Guid.NewGuid(), TipoVeiculoId = Guid.NewGuid(), Ano = 2010, Placa = "AAA-1212"});
    
    [Fact]
    public void teste_criar_locacao_command_NoOk()
    {
        _invalidComand.Validate();
        Assert.True(_invalidComand.Invalid);
    }
        
    [Fact]
    public void teste_criar_locacao_command_Ok()
    {
        _validComand.Validate();
        Assert.True(_validComand.Valid);
    }
}